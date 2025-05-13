using System;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SnipIt.Managers
{
    public class UserManager
    {   
        // Update user email
        private string GetDatabasePath()
        {
            string dbPath = Path.Combine(Application.StartupPath, "databases", "SnipIt.accdb");
            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"Database not found at: {dbPath}");
            }
            return dbPath;
        }

        private string GetConnectionString()
        {
            return $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GetDatabasePath()};";
        }

        public User GetCurrentUser()
        {
            int userId = timer.SessionManager.UserId;

            if (userId <= 0)
            {
                throw new InvalidOperationException("No user is currently logged in.");
            }

            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT UserID, Username, Gmail, Created FROM Users WHERE UserID = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                User user = new User
                                {
                                    UserId = reader.GetInt32(0),
                                    Username = reader.GetString(1),
                                    Email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    CreatedDate = reader.IsDBNull(3) ? DateTime.Now.AddDays(-30) : reader.GetDateTime(3)
                                };
                                return user;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // just a fall back if ever it messes up
            return new User
            {
                UserId = userId,
                Username = timer.SessionManager.Username,
                Email = "user@example.com",
                CreatedDate = DateTime.Now.AddDays(-30)
            };
        }

        public bool UsernameExists(string username)
        {
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = ? AND UserID <> ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = username;
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = timer.SessionManager.UserId;

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking username: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public void UpdateUser(User user)
        {
            // Update user information in the database
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET Username = ?, Gmail = ? WHERE UserID = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = user.Username;
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = user.Email ?? (object)DBNull.Value;
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = user.UserId;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            throw new Exception("Failed to update user information.");
                        }

                        // update sesh
                        timer.SessionManager.Username = user.Username;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        public bool VerifyPassword(int userId, string password)
        {
            // Verify the user's password
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE UserID = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            string storedHash = result.ToString();
                            string inputHash = HashPassword(password);

                            return storedHash == inputHash;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error verifying password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }

        public void ChangePassword(int userId, string newPassword)
        {
            // Change the user's password
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET PasswordHash = ? WHERE UserID = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        string passwordHash = HashPassword(newPassword);
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = passwordHash;
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            throw new Exception("Failed to update password.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error changing password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        public void DeleteUser(int userId)
        {
            // Delete the user account and all related data
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();

                    OleDbTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Delete snippets
                        string deleteSnippetsQuery = "DELETE FROM Snippets WHERE UserID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(deleteSnippetsQuery, conn, transaction))
                        {
                            cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;
                            cmd.ExecuteNonQuery();
                        }

                        // actual deletion na sa user
                        string deleteUserQuery = "DELETE FROM Users WHERE UserID = ?";
                        using (OleDbCommand cmd = new OleDbCommand(deleteUserQuery, conn, transaction))
                        {
                            cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected <= 0)
                            {
                                throw new Exception("Failed to delete user account.");
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        // rollback
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool UpdateEmail(int userId, string newEmail)
        {
            // Update user's email in the database
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Users SET Gmail = ? WHERE UserID = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        // Handle null or empty email
                        if (string.IsNullOrWhiteSpace(newEmail))
                        {
                            cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = newEmail;
                        }

                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.Integer)).Value = userId;

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating email: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}