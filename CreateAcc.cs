using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnipIt.Managers;

namespace SnipIt
{
    public partial class CreateAcc : Form
    {
        private DraggableFormHelper draggableHelper;

        public CreateAcc()
        {
            InitializeComponent();
            draggableHelper = new DraggableFormHelper(this, pnlTitlebar);

            tbxPass.UseSystemPasswordChar = true;
            tbxConfirm.UseSystemPasswordChar = true;
        }

        private void CreateAcc_Load(object sender, EventArgs e)
        {

        }

        public timer timer
        {
            get => default;
            set
            {
            }
        }
        public timer LoginToCreate
        {
            get => default;
            set
            {
            }
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ptbBack_MouseEnter(object sender, EventArgs e)
        {
            ptbBack.Cursor = Cursors.Hand;
        }

        private void ptbBack_MouseLeave(object sender, EventArgs e)
        {
            ptbBack.Cursor = Cursors.Default;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void taibtnCreateAcc_Click(object sender, EventArgs e)
        {
            string username = tbxUser.Text.Trim();
            string password = tbxPass.Text;
            string confirmPassword = tbxConfirm.Text;
            string gmail = tbxEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(gmail))
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string hashedPassword = HashPassword(password);
            string dbPath = Path.Combine(Application.StartupPath, "databases", "SnipIt.accdb");
            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    // check if it already exists otherwise proceed with acc creation
                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = ? OR Gmail = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("?", username);
                        checkCmd.Parameters.AddWithValue("?", gmail);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Username or Email already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // add a new user
                    string query = "INSERT INTO Users (Username, PasswordHash, Gmail, Created) VALUES (?, ?, ?, ?)";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", username);
                        cmd.Parameters.AddWithValue("?", hashedPassword);
                        cmd.Parameters.AddWithValue("?", gmail);
                        cmd.Parameters.Add("?", OleDbType.Date).Value = DateTime.Now;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            timer login = new timer();
            login.Show();
            this.Hide();
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void chkShowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkShowpass.Checked)
            {
                tbxConfirm.UseSystemPasswordChar = true;
                tbxPass.UseSystemPasswordChar = true;
            }
            else if (chkShowpass.Checked)
            {
                tbxConfirm.UseSystemPasswordChar = false;
                tbxPass.UseSystemPasswordChar = false;
            }
        }

        private void tbxPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
