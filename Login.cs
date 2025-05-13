using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using SnipIt.Managers;

namespace SnipIt
{
    public partial class timer : Form
    {
        // *****DISCLAIMER*****
        // really messy code as i was learning things throughout the whole project so uhh i apologize in advance xd

        // TO DO IN THE FUTURE
        // clean the whole code clyde because this is a mess from how it is either hard coded or manually in the designer
        // get rid of unused lines but for now keep as is in case it messes things up
        // optimize the whole thing and find ways to implement a better hybrid database
        // MAKE IT LOOK BETTER as some parts are rushed

        // special thanks to
        // archiel gilhang - tester, suggestions, and some help with the snipit to ide exporting as he has some experience already
        // mary nichole petalcorin - motivational tambay speaker
        // ado - fave singer in my spotify playlist 24/7 as i was coding and tweaking (helped me keep my sanity intact)
        // cpe classmates - suggestions, help and testers
        // stackoverflow, reddit, youtube - other materials

        private DraggableFormHelper draggableHelper;
        private bool isUserEntered = false;
        private bool isPasswordEntered = false;
        bool imageNext; // unused
        bool isNext; // unused
        private bool loginSuccessful = false;

        public timer()
        {
            InitializeComponent();
            draggableHelper = new DraggableFormHelper(this, pnlTitlebar);
            timer1.Interval = 1;
            timer1.Start();
            DoubleBuffered = true;

            // set defaults might switch to placeholder text idk
            tbxUser.Font = new Font("Arial Rounded MT", 14, FontStyle.Bold);
            tbxUser.ForeColor = Color.Gray;
            tbxUser.Text = "USERNAME";

            tbxPassword.Font = new Font("Arial Rounded MT", 14, FontStyle.Bold);
            tbxPassword.ForeColor = Color.Gray;
            tbxPassword.Text = "PASSWORD";

        }


        public static class SessionManager 
        {
            // get user id and username 
            public static int UserId { get; set; } 
            public static string Username { get; set; } = string.Empty;
        }

        private void tbxLogin_Enter(object sender, EventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx == null) return;

            if (tbx == tbxUser && !isUserEntered)
            {
                isUserEntered = true;
                tbx.Text = "";
                tbx.ForeColor = Color.Black;
                tbx.Font = new Font("Arial Rounded MT", 14, FontStyle.Regular);
            }

            else if (tbx == tbxPassword && !isPasswordEntered)
            {
                isPasswordEntered = true;
                tbx.Text = "";
                tbx.ForeColor = Color.Black;
                tbx.Font = new Font("Arial Rounded MT", 14, FontStyle.Regular);
                tbx.UseSystemPasswordChar = true;

            }
        }
        private void tbxLogin_Leave(object sender, EventArgs e)
        {
            TextBox tbx = sender as TextBox;

            if (tbx == tbxUser && String.IsNullOrEmpty(tbx.Text))
            {
                tbxUser.Font = new Font("Arial Rounded MT", 14, FontStyle.Bold);
                tbxUser.ForeColor = Color.Gray;
                tbxUser.Text = "USERNAME";
                isUserEntered = false;
            }
            else if (tbx == tbxPassword && String.IsNullOrEmpty(tbx.Text))
            {
                tbxPassword.Font = new Font("Arial Rounded MT", 14, FontStyle.Bold);
                tbxPassword.ForeColor = Color.Gray;
                tbxPassword.Text = "PASSWORD";
                isPasswordEntered = false;
                tbx.UseSystemPasswordChar = false;
            }

        }

        private void pbxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void pbxMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void tbxUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPass forgotPass = new ForgotPass();
            forgotPass.Show();
            Visible = false;
        }

        private void linkCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateAcc createAcc = new CreateAcc();
            createAcc.Show();
            Visible = false;
        }

        private void ptbSubmit_Click(object sender, EventArgs e)
        {
            string username = tbxUser.Text.Trim();
            string password = tbxPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loginSuccessful = true;

                this.Hide();
                Loading loading = new Loading();
                loading.ShowLoadingFor(4500);
                loading.ShowDialog();
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ptbSubmit_MouseEnter(object sender, EventArgs e)
        {
            ptbSubmit.Cursor = Cursors.Hand;
        }

        private void ptbSubmit_MouseLeave(object sender, EventArgs e)
        {
            ptbSubmit.Cursor = Cursors.Default;
        }

        private void chkShowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (tbxPassword.ForeColor == Color.Gray)
            {
                chkShowpass.Checked = false;
            }
            else if (chkShowpass.Checked)
            {
                tbxPassword.UseSystemPasswordChar = false;
            }
            else if (!chkShowpass.Checked)
            {
                tbxPassword.UseSystemPasswordChar = true;
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ptbImgNext_Click(object sender, EventArgs e)
        {
            timer1.Start();
            isNext = true;
        }

        private void imgTick(object sender, EventArgs e)
        {
            if (imageNext && isNext)
            {
                imgPanel.Width -= 680;
                ptb2nd.Width -= 680;
                if (imgPanel.Width == imgPanel.MinimumSize.Width)
                {
                    imageNext = false;
                    timer1.Stop();

                }

            }

            if (imageNext == false && isNext == false)
            {
                imgPanel.Width += 680;
                ptb2nd.Width += 680;
                if (imgPanel.Width == imgPanel.MaximumSize.Width)
                {
                    imageNext = true;
                    timer1.Stop();
                }

            }
        }

        private void ptbImgBack_Click(object sender, EventArgs e)
        {
            timer1.Start();
            isNext = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timeReal_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");


        }

        private void ptb2nd_Click(object sender, EventArgs e)
        {

        }
        private bool ValidateUser(string username, string password)
        {
            string dbPath = Path.Combine(Application.StartupPath, "databases", "SnipIt.accdb");

            if (!File.Exists(dbPath))
            {
                MessageBox.Show($"Database not found at: {dbPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};";

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT UserID, PasswordHash FROM Users WHERE [Username] = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OleDbParameter("?", OleDbType.VarChar)).Value = username;

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = reader.GetInt32(0);
                                string storedHash = reader.GetString(1);
                                string enteredHash = HashPassword(password);

                                if (storedHash == enteredHash)
                                {
                                    SessionManager.UserId = userId;
                                    SessionManager.Username = username;
                                    return true;
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username not found.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
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
    }
}
