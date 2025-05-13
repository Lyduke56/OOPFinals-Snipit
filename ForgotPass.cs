using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.IO;
using SnipIt.Managers;
using System.Security.Policy;

namespace SnipIt
{
    public partial class ForgotPass : Form
    {
        private DraggableFormHelper draggableHelper;
        private string verificationCode = "";
        private string userEmail = "";
        private bool isVerified = false;
        private Random random = new Random();

        public ForgotPass()
        {
            InitializeComponent();
            draggableHelper = new DraggableFormHelper(this, pnlTitlebar);
            tbxNewPass.PasswordChar = '•';
            tbxConfirmNewPass.PasswordChar = '•';

            tbxNewPass.ReadOnly = true;
            tbxConfirmNewPass.ReadOnly = true;

            btnVerify.Click += BtnVerify_Click;
            btnContinue.Click += BtnContinue_Click;

        }

        public timer LoginToForgot
        {
            get => default;
            set
            {
            }
        }

        private void ForgotPass_Load(object sender, EventArgs e)
        {
        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void ptbMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ptbBack_Click(object sender, EventArgs e)
        {
            timer login = new timer();
            login.Show();
            Visible = false;
        }

        private void ptbBack_MouseEnter(object sender, EventArgs e)
        {
            ptbBack.Cursor = Cursors.Hand;
        }

        private void ptbBack_MouseLeave(object sender, EventArgs e)
        {
            ptbBack.Cursor = Cursors.Default;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RequestVerificationCode();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            TogglePasswordVisibility();
        }

        private void pnlTitlebar_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private bool CheckEmailExists(string email)
        {
            bool exists = false;
            string dbPath = Path.Combine(Application.StartupPath, "databases", "SnipIt.accdb");
            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Gmail = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", email);
                        int count = (int)command.ExecuteScalar();
                        exists = (count > 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return exists;
        }

        private string GenerateVerificationCode()
        {
            return random.Next(100000, 999999).ToString();
        }

        private void RequestVerificationCode()
        {
            userEmail = tbxEmail.Text.Trim();

            if (string.IsNullOrEmpty(userEmail))
            {
                MessageBox.Show("Please enter your email address.", "Email Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CheckEmailExists(userEmail))
            {
                verificationCode = GenerateVerificationCode();

                try
                {
                    SendVerificationEmail(userEmail, verificationCode);
                    MessageBox.Show("Verification code has been sent to your email.",
                        "Verification Code Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error sending email: {ex.Message}\n\nYour verification code is: {verificationCode}",
                        "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Email not found in our records.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendVerificationEmail(string recipientEmail, string code)
        {
            string senderEmail = "codesnipit@gmail.com";
            string senderPassword = "icgi mvtn ijkb adfw\r\n"; // Use an app password for Gmail

            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(recipientEmail);
            mail.Subject = "SnipIt Password Reset Verification Code";
            mail.Body = $"Your verification code for password reset is: {code}\n\n" +
                        "This code will expire in 15 minutes.\n\n" +
                        "If you did not request a password reset, please ignore this email.";

            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            smtpClient.Send(mail);
        }
        private void BtnVerify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(verificationCode))
            {
                MessageBox.Show("Please request a verification code first.", "Verification Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tbxVerification.Text.Trim() == verificationCode)
            {
                isVerified = true;
                tbxNewPass.ReadOnly = false;
                tbxConfirmNewPass.ReadOnly = false;
                MessageBox.Show("Verification successful! You can now reset your password.",
                    "Verification Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid verification code. Please try again.",
                    "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (!isVerified)
            {
                MessageBox.Show("Please verify your email first.", "Verification Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPassword = tbxNewPass.Text;
            string confirmPassword = tbxConfirmNewPass.Text;

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please enter and confirm your new password.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords don't match. Please try again.", "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UpdatePassword(userEmail, newPassword))
            {
                MessageBox.Show("Password reset successful! You can now log in with your new password.",
                    "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);

                timer login = new timer();
                login.Show();
                Visible = false;
            }
            else
            {
                MessageBox.Show("Failed to reset password. Please try again later.",
                    "Reset Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdatePassword(string email, string newPassword)
        {
            bool success = false;
            string dbPath = Path.Combine(Application.StartupPath, "databases", "SnipIt.accdb");
            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            string hashedPassword = HashPassword(newPassword);

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET PasswordHash = ? WHERE Gmail = ?";

                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", hashedPassword);
                        command.Parameters.AddWithValue("?", email);
                        int rowsAffected = command.ExecuteNonQuery();
                        success = (rowsAffected > 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return success;
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

        private void BtnShowPassword_Click(object sender, EventArgs e)
        {
            if (tbxNewPass.PasswordChar == '•')
            {
                tbxNewPass.PasswordChar = '\0';
                tbxConfirmNewPass.PasswordChar = '\0';
            }
            else
            {
                tbxNewPass.PasswordChar = '•';
                tbxConfirmNewPass.PasswordChar = '•';
            }
        }

        private void TogglePasswordVisibility()
        {
            if (tbxNewPass.PasswordChar == '•')
            {
                tbxNewPass.PasswordChar = '\0';
                tbxConfirmNewPass.PasswordChar = '\0';
            }
            else
            {
                tbxNewPass.PasswordChar = '•';
                tbxConfirmNewPass.PasswordChar = '•';
            }
        }

        private void btnSendCode_Click(object sender, EventArgs e)
        {
            RequestVerificationCode();
        }

        private void chkShowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkShowpass.Checked)
            {
                tbxNewPass.UseSystemPasswordChar = true;
            }
            else if (chkShowpass.Checked)
            {
                tbxNewPass.UseSystemPasswordChar = false;
            }
        }

        private void tbxNewPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}