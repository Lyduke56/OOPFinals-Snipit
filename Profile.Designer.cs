namespace SnipIt
{
    partial class UserProfileControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            pnlContent = new Panel();
            pnlAccountActions = new Panel();
            btnClearSnips = new Button();
            btnLogout = new Button();
            btnDeleteAccount = new Button();
            label6 = new Label();
            pnlDivider1 = new Panel();
            pnlSecurity = new Panel();
            chkShowpass = new CheckBox();
            btnChangePassword = new Button();
            tbxConfirmPassword = new TextBox();
            label5 = new Label();
            tbxNewPassword = new TextBox();
            label4 = new Label();
            tbxCurrentPassword = new TextBox();
            label3 = new Label();
            label7 = new Label();
            pnlProfile = new Panel();
            btnUpdateEmail = new Button();
            tbxEmail = new TextBox();
            label8 = new Label();
            btnUpdateUsername = new Button();
            tbxUsername = new TextBox();
            label2 = new Label();
            lblWelcome = new Label();
            pnlHeader = new Panel();
            label1 = new Label();
            pnlMain.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlAccountActions.SuspendLayout();
            pnlSecurity.SuspendLayout();
            pnlProfile.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.White;
            pnlMain.Controls.Add(pnlContent);
            pnlMain.Controls.Add(pnlHeader);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Margin = new Padding(4, 3, 4, 3);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(875, 750);
            pnlMain.TabIndex = 0;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(pnlAccountActions);
            pnlContent.Controls.Add(pnlDivider1);
            pnlContent.Controls.Add(pnlSecurity);
            pnlContent.Controls.Add(pnlProfile);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 81);
            pnlContent.Margin = new Padding(4, 3, 4, 3);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(35, 23, 35, 23);
            pnlContent.Size = new Size(875, 669);
            pnlContent.TabIndex = 1;
            // 
            // pnlAccountActions
            // 
            pnlAccountActions.Controls.Add(btnClearSnips);
            pnlAccountActions.Controls.Add(btnLogout);
            pnlAccountActions.Controls.Add(btnDeleteAccount);
            pnlAccountActions.Controls.Add(label6);
            pnlAccountActions.Dock = DockStyle.Top;
            pnlAccountActions.Location = new Point(35, 440);
            pnlAccountActions.Margin = new Padding(4, 3, 4, 3);
            pnlAccountActions.Name = "pnlAccountActions";
            pnlAccountActions.Size = new Size(805, 133);
            pnlAccountActions.TabIndex = 3;
            // 
            // btnClearSnips
            // 
            btnClearSnips.BackColor = Color.FromArgb(79, 79, 127);
            btnClearSnips.FlatAppearance.BorderSize = 0;
            btnClearSnips.FlatStyle = FlatStyle.Flat;
            btnClearSnips.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearSnips.ForeColor = Color.White;
            btnClearSnips.Location = new Point(23, 57);
            btnClearSnips.Margin = new Padding(4, 3, 4, 3);
            btnClearSnips.Name = "btnClearSnips";
            btnClearSnips.Size = new Size(175, 44);
            btnClearSnips.TabIndex = 9;
            btnClearSnips.Text = "CLEAR ALL SNIPPETS";
            btnClearSnips.UseVisualStyleBackColor = false;
            btnClearSnips.Click += btnClearSnips_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(79, 79, 127);
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(617, 57);
            btnLogout.Margin = new Padding(4, 3, 4, 3);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(175, 44);
            btnLogout.TabIndex = 8;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnDeleteAccount
            // 
            btnDeleteAccount.BackColor = Color.Crimson;
            btnDeleteAccount.FlatAppearance.BorderSize = 0;
            btnDeleteAccount.FlatStyle = FlatStyle.Flat;
            btnDeleteAccount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteAccount.ForeColor = Color.White;
            btnDeleteAccount.Location = new Point(406, 57);
            btnDeleteAccount.Margin = new Padding(4, 3, 4, 3);
            btnDeleteAccount.Name = "btnDeleteAccount";
            btnDeleteAccount.Size = new Size(189, 44);
            btnDeleteAccount.TabIndex = 7;
            btnDeleteAccount.Text = "Delete Account";
            btnDeleteAccount.UseVisualStyleBackColor = false;
            btnDeleteAccount.Click += btnDeleteAccount_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(64, 64, 64);
            label6.Location = new Point(4, 12);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(157, 25);
            label6.TabIndex = 0;
            label6.Text = "Account Actions:";
            // 
            // pnlDivider1
            // 
            pnlDivider1.BackColor = Color.FromArgb(224, 224, 224);
            pnlDivider1.Dock = DockStyle.Top;
            pnlDivider1.Location = new Point(35, 428);
            pnlDivider1.Margin = new Padding(4, 3, 4, 3);
            pnlDivider1.Name = "pnlDivider1";
            pnlDivider1.Size = new Size(805, 12);
            pnlDivider1.TabIndex = 2;
            // 
            // pnlSecurity
            // 
            pnlSecurity.Controls.Add(chkShowpass);
            pnlSecurity.Controls.Add(btnChangePassword);
            pnlSecurity.Controls.Add(tbxConfirmPassword);
            pnlSecurity.Controls.Add(label5);
            pnlSecurity.Controls.Add(tbxNewPassword);
            pnlSecurity.Controls.Add(label4);
            pnlSecurity.Controls.Add(tbxCurrentPassword);
            pnlSecurity.Controls.Add(label3);
            pnlSecurity.Controls.Add(label7);
            pnlSecurity.Dock = DockStyle.Top;
            pnlSecurity.Location = new Point(35, 197);
            pnlSecurity.Margin = new Padding(4, 3, 4, 3);
            pnlSecurity.Name = "pnlSecurity";
            pnlSecurity.Size = new Size(805, 231);
            pnlSecurity.TabIndex = 1;
            // 
            // chkShowpass
            // 
            chkShowpass.AutoSize = true;
            chkShowpass.BackColor = Color.Transparent;
            chkShowpass.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowpass.ForeColor = Color.Black;
            chkShowpass.Location = new Point(338, 161);
            chkShowpass.Name = "chkShowpass";
            chkShowpass.Size = new Size(119, 19);
            chkShowpass.TabIndex = 16;
            chkShowpass.Text = "Show Password";
            chkShowpass.UseVisualStyleBackColor = false;
            chkShowpass.CheckedChanged += chkShowpass_CheckedChanged;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.FromArgb(79, 79, 127);
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(588, 83);
            btnChangePassword.Margin = new Padding(4, 3, 4, 3);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(204, 44);
            btnChangePassword.TabIndex = 7;
            btnChangePassword.Text = "Change Password";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // tbxConfirmPassword
            // 
            tbxConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            tbxConfirmPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxConfirmPassword.Location = new Point(166, 130);
            tbxConfirmPassword.Margin = new Padding(4, 3, 4, 3);
            tbxConfirmPassword.Name = "tbxConfirmPassword";
            tbxConfirmPassword.Size = new Size(291, 25);
            tbxConfirmPassword.TabIndex = 5;
            tbxConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 133);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(117, 17);
            label5.TabIndex = 4;
            label5.Text = "Confirm Password:";
            // 
            // tbxNewPassword
            // 
            tbxNewPassword.BorderStyle = BorderStyle.FixedSingle;
            tbxNewPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxNewPassword.Location = new Point(166, 95);
            tbxNewPassword.Margin = new Padding(4, 3, 4, 3);
            tbxNewPassword.Name = "tbxNewPassword";
            tbxNewPassword.Size = new Size(291, 25);
            tbxNewPassword.TabIndex = 3;
            tbxNewPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 97);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(97, 17);
            label4.TabIndex = 2;
            label4.Text = "New Password:";
            // 
            // tbxCurrentPassword
            // 
            tbxCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
            tbxCurrentPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxCurrentPassword.Location = new Point(166, 58);
            tbxCurrentPassword.Margin = new Padding(4, 3, 4, 3);
            tbxCurrentPassword.Name = "tbxCurrentPassword";
            tbxCurrentPassword.Size = new Size(291, 25);
            tbxCurrentPassword.TabIndex = 1;
            tbxCurrentPassword.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 60);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(114, 17);
            label3.TabIndex = 0;
            label3.Text = "Current Password:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(64, 64, 64);
            label7.Location = new Point(4, 12);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(86, 25);
            label7.TabIndex = 0;
            label7.Text = "Security:";
            // 
            // pnlProfile
            // 
            pnlProfile.Controls.Add(btnUpdateEmail);
            pnlProfile.Controls.Add(tbxEmail);
            pnlProfile.Controls.Add(label8);
            pnlProfile.Controls.Add(btnUpdateUsername);
            pnlProfile.Controls.Add(tbxUsername);
            pnlProfile.Controls.Add(label2);
            pnlProfile.Controls.Add(lblWelcome);
            pnlProfile.Dock = DockStyle.Top;
            pnlProfile.Location = new Point(35, 23);
            pnlProfile.Margin = new Padding(4, 3, 4, 3);
            pnlProfile.Name = "pnlProfile";
            pnlProfile.Size = new Size(805, 174);
            pnlProfile.TabIndex = 0;
            // 
            // btnUpdateEmail
            // 
            btnUpdateEmail.BackColor = Color.FromArgb(79, 79, 127);
            btnUpdateEmail.FlatAppearance.BorderSize = 0;
            btnUpdateEmail.FlatStyle = FlatStyle.Flat;
            btnUpdateEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdateEmail.ForeColor = Color.White;
            btnUpdateEmail.Location = new Point(588, 96);
            btnUpdateEmail.Margin = new Padding(4, 3, 4, 3);
            btnUpdateEmail.Name = "btnUpdateEmail";
            btnUpdateEmail.Size = new Size(204, 44);
            btnUpdateEmail.TabIndex = 6;
            btnUpdateEmail.Text = "Update Email";
            btnUpdateEmail.UseVisualStyleBackColor = false;
            btnUpdateEmail.Click += btnUpdateEmail_Click;
            // 
            // tbxEmail
            // 
            tbxEmail.BorderStyle = BorderStyle.FixedSingle;
            tbxEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxEmail.Location = new Point(166, 108);
            tbxEmail.Margin = new Padding(4, 3, 4, 3);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(291, 25);
            tbxEmail.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(6, 110);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(94, 17);
            label8.TabIndex = 4;
            label8.Text = "Email Address:";
            // 
            // btnUpdateUsername
            // 
            btnUpdateUsername.BackColor = Color.FromArgb(79, 79, 127);
            btnUpdateUsername.FlatAppearance.BorderSize = 0;
            btnUpdateUsername.FlatStyle = FlatStyle.Flat;
            btnUpdateUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdateUsername.ForeColor = Color.White;
            btnUpdateUsername.Location = new Point(588, 46);
            btnUpdateUsername.Margin = new Padding(4, 3, 4, 3);
            btnUpdateUsername.Name = "btnUpdateUsername";
            btnUpdateUsername.Size = new Size(204, 44);
            btnUpdateUsername.TabIndex = 3;
            btnUpdateUsername.Text = "Update Username";
            btnUpdateUsername.UseVisualStyleBackColor = false;
            btnUpdateUsername.Click += btnUpdateUsername_Click;
            // 
            // tbxUsername
            // 
            tbxUsername.BorderStyle = BorderStyle.FixedSingle;
            tbxUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxUsername.Location = new Point(166, 58);
            tbxUsername.Margin = new Padding(4, 3, 4, 3);
            tbxUsername.Name = "tbxUsername";
            tbxUsername.Size = new Size(291, 25);
            tbxUsername.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(4, 60);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 17);
            label2.TabIndex = 1;
            label2.Text = "Username:";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = Color.FromArgb(64, 64, 64);
            lblWelcome.Location = new Point(4, 12);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(131, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, {0}!";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(79, 79, 127);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 3, 4, 3);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(875, 81);
            pnlHeader.TabIndex = 0;
            pnlHeader.Paint += pnlHeader_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(29, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(148, 32);
            label1.TabIndex = 0;
            label1.Text = "User Profile";
            // 
            // UserProfileControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlMain);
            Margin = new Padding(4, 3, 4, 3);
            Name = "UserProfileControl";
            Size = new Size(875, 750);
            pnlMain.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlAccountActions.ResumeLayout(false);
            pnlAccountActions.PerformLayout();
            pnlSecurity.ResumeLayout(false);
            pnlSecurity.PerformLayout();
            pnlProfile.ResumeLayout(false);
            pnlProfile.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlHeader;
        private Label label1;
        private Panel pnlContent;
        private Panel pnlProfile;
        private Label lblWelcome;
        private Button btnUpdateUsername;
        private TextBox tbxUsername;
        private Label label2;
        private Panel pnlSecurity;
        private Label label3;
        private TextBox tbxCurrentPassword;
        private TextBox tbxConfirmPassword;
        private Label label5;
        private TextBox tbxNewPassword;
        private Label label4;
        private Button btnChangePassword;
        private Panel pnlDivider1;
        private Panel pnlAccountActions;
        private Button btnDeleteAccount;
        private Label label6;
        private Button btnLogout;
        private Label label7;
        private Button btnClearSnips;
        private CheckBox chkShowpass;
        private TextBox tbxEmail;
        private Label label8;
        private Button btnUpdateEmail;
    }
}