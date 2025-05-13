namespace SnipIt
{
    partial class timer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(timer));
            panel1 = new Panel();
            panel2 = new Panel();
            label5 = new Label();
            pictureBox3 = new PictureBox();
            chkShowpass = new CheckBox();
            tbxPassword = new TextBox();
            tbxUser = new TextBox();
            pictureBox2 = new PictureBox();
            ptbSubmit = new PictureBox();
            linkForgot = new LinkLabel();
            linkCreate = new LinkLabel();
            label3 = new Label();
            pnlTitlebar = new Panel();
            pbxMinimize = new PictureBox();
            pbxClose = new PictureBox();
            label1 = new Label();
            panel3 = new Panel();
            label4 = new Label();
            lblTime = new Label();
            label2 = new Label();
            imgPanel = new Panel();
            ptb2nd = new PictureBox();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timer1 = new System.Windows.Forms.Timer(components);
            timeReal = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbSubmit).BeginInit();
            pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxClose).BeginInit();
            panel3.SuspendLayout();
            imgPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptb2nd).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gray;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pictureBox3);
            panel1.Controls.Add(chkShowpass);
            panel1.Controls.Add(tbxPassword);
            panel1.Controls.Add(tbxUser);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(ptbSubmit);
            panel1.Controls.Add(linkForgot);
            panel1.Controls.Add(linkCreate);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(325, 537);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(label5);
            panel2.Location = new Point(325, 89);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 388);
            panel2.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 0;
            label5.Text = "label5";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(114, 221);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(99, 28);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 16;
            pictureBox3.TabStop = false;
            // 
            // chkShowpass
            // 
            chkShowpass.AutoSize = true;
            chkShowpass.BackColor = Color.Transparent;
            chkShowpass.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowpass.ForeColor = Color.White;
            chkShowpass.Location = new Point(169, 325);
            chkShowpass.Name = "chkShowpass";
            chkShowpass.Size = new Size(119, 19);
            chkShowpass.TabIndex = 15;
            chkShowpass.Text = "Show Password";
            chkShowpass.UseVisualStyleBackColor = false;
            chkShowpass.CheckedChanged += chkShowpass_CheckedChanged;
            // 
            // tbxPassword
            // 
            tbxPassword.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxPassword.Location = new Point(37, 290);
            tbxPassword.Name = "tbxPassword";
            tbxPassword.Size = new Size(251, 29);
            tbxPassword.TabIndex = 14;
            tbxPassword.Text = "PASSWORD";
            tbxPassword.Enter += tbxLogin_Enter;
            tbxPassword.Leave += tbxLogin_Leave;
            // 
            // tbxUser
            // 
            tbxUser.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxUser.Location = new Point(37, 255);
            tbxUser.Name = "tbxUser";
            tbxUser.Size = new Size(251, 29);
            tbxUser.TabIndex = 13;
            tbxUser.Text = "USERNAME";
            tbxUser.TextChanged += tbxUser_TextChanged;
            tbxUser.Enter += tbxLogin_Enter;
            tbxUser.Leave += tbxLogin_Leave;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 107);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(325, 93);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // ptbSubmit
            // 
            ptbSubmit.BackColor = Color.Transparent;
            ptbSubmit.Image = (Image)resources.GetObject("ptbSubmit.Image");
            ptbSubmit.Location = new Point(126, 390);
            ptbSubmit.Name = "ptbSubmit";
            ptbSubmit.Size = new Size(75, 75);
            ptbSubmit.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbSubmit.TabIndex = 10;
            ptbSubmit.TabStop = false;
            ptbSubmit.Click += ptbSubmit_Click;
            ptbSubmit.MouseEnter += ptbSubmit_MouseEnter;
            ptbSubmit.MouseLeave += ptbSubmit_MouseLeave;
            // 
            // linkForgot
            // 
            linkForgot.AutoSize = true;
            linkForgot.BackColor = Color.Transparent;
            linkForgot.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkForgot.LinkBehavior = LinkBehavior.HoverUnderline;
            linkForgot.LinkColor = Color.FromArgb(128, 255, 255);
            linkForgot.Location = new Point(87, 357);
            linkForgot.Name = "linkForgot";
            linkForgot.Size = new Size(143, 21);
            linkForgot.TabIndex = 6;
            linkForgot.TabStop = true;
            linkForgot.Text = "Forgot Password?";
            linkForgot.LinkClicked += linkForgot_LinkClicked;
            // 
            // linkCreate
            // 
            linkCreate.AutoSize = true;
            linkCreate.BackColor = Color.Transparent;
            linkCreate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkCreate.LinkBehavior = LinkBehavior.HoverUnderline;
            linkCreate.LinkColor = Color.FromArgb(128, 255, 255);
            linkCreate.Location = new Point(165, 493);
            linkCreate.Name = "linkCreate";
            linkCreate.Size = new Size(96, 21);
            linkCreate.TabIndex = 5;
            linkCreate.TabStop = true;
            linkCreate.Text = "Create one.";
            linkCreate.LinkClicked += linkCreate_LinkClicked;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(64, 493);
            label3.Name = "label3";
            label3.Size = new Size(106, 21);
            label3.TabIndex = 4;
            label3.Text = "No Account?";
            // 
            // pnlTitlebar
            // 
            pnlTitlebar.BackColor = Color.DimGray;
            pnlTitlebar.BackgroundImage = (Image)resources.GetObject("pnlTitlebar.BackgroundImage");
            pnlTitlebar.Controls.Add(pbxMinimize);
            pnlTitlebar.Controls.Add(pbxClose);
            pnlTitlebar.Dock = DockStyle.Top;
            pnlTitlebar.Location = new Point(0, 0);
            pnlTitlebar.Name = "pnlTitlebar";
            pnlTitlebar.Size = new Size(1008, 38);
            pnlTitlebar.TabIndex = 1;
            pnlTitlebar.Paint += panel2_Paint;
            // 
            // pbxMinimize
            // 
            pbxMinimize.BackColor = Color.Transparent;
            pbxMinimize.Image = (Image)resources.GetObject("pbxMinimize.Image");
            pbxMinimize.Location = new Point(939, 12);
            pbxMinimize.Name = "pbxMinimize";
            pbxMinimize.Size = new Size(15, 15);
            pbxMinimize.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxMinimize.TabIndex = 17;
            pbxMinimize.TabStop = false;
            pbxMinimize.Click += pbxMinimize_Click;
            // 
            // pbxClose
            // 
            pbxClose.BackColor = Color.Transparent;
            pbxClose.Image = (Image)resources.GetObject("pbxClose.Image");
            pbxClose.InitialImage = (Image)resources.GetObject("pbxClose.InitialImage");
            pbxClose.Location = new Point(981, 12);
            pbxClose.Name = "pbxClose";
            pbxClose.Size = new Size(15, 15);
            pbxClose.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxClose.TabIndex = 16;
            pbxClose.TabStop = false;
            pbxClose.Click += pbxClose_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(18, 18);
            label1.Name = "label1";
            label1.Size = new Size(377, 21);
            label1.TabIndex = 19;
            label1.Text = "</>   SnipIt - Code Snippet Management System";
            label1.TextAlign = ContentAlignment.TopRight;
            label1.Click += label1_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BackgroundImageLayout = ImageLayout.Zoom;
            panel3.Controls.Add(label4);
            panel3.Controls.Add(lblTime);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(imgPanel);
            panel3.Location = new Point(325, 38);
            panel3.Name = "panel3";
            panel3.Size = new Size(683, 499);
            panel3.TabIndex = 3;
            panel3.Paint += panel3_Paint;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(19, 461);
            label4.Name = "label4";
            label4.Size = new Size(54, 21);
            label4.TabIndex = 23;
            label4.Text = "v1.0.0";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // lblTime
            // 
            lblTime.AutoSize = true;
            lblTime.BackColor = Color.Transparent;
            lblTime.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTime.ForeColor = Color.White;
            lblTime.Location = new Point(542, 12);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(129, 30);
            lblTime.TabIndex = 22;
            lblTime.Text = "hh:mm:ss tt";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(173, 239);
            label2.Name = "label2";
            label2.Size = new Size(0, 21);
            label2.TabIndex = 21;
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // imgPanel
            // 
            imgPanel.Controls.Add(ptb2nd);
            imgPanel.Location = new Point(0, 54);
            imgPanel.MaximumSize = new Size(683, 388);
            imgPanel.MinimumSize = new Size(3, 388);
            imgPanel.Name = "imgPanel";
            imgPanel.Size = new Size(683, 388);
            imgPanel.TabIndex = 19;
            // 
            // ptb2nd
            // 
            ptb2nd.Dock = DockStyle.Fill;
            ptb2nd.Image = (Image)resources.GetObject("ptb2nd.Image");
            ptb2nd.Location = new Point(0, 0);
            ptb2nd.MaximumSize = new Size(683, 388);
            ptb2nd.MinimumSize = new Size(3, 388);
            ptb2nd.Name = "ptb2nd";
            ptb2nd.Size = new Size(683, 388);
            ptb2nd.SizeMode = PictureBoxSizeMode.StretchImage;
            ptb2nd.TabIndex = 0;
            ptb2nd.TabStop = false;
            ptb2nd.Click += ptb2nd_Click;
            // 
            // timer1
            // 
            timer1.Tick += imgTick;
            // 
            // timeReal
            // 
            timeReal.Enabled = true;
            timeReal.Interval = 1000;
            timeReal.Tick += timeReal_Tick;
            // 
            // timer
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1008, 537);
            Controls.Add(panel3);
            Controls.Add(pnlTitlebar);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "timer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SnipIt - Login";
            DragDrop += Login_DragDrop;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbSubmit).EndInit();
            pnlTitlebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxClose).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            imgPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ptb2nd).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private LinkLabel linkForgot;
        private LinkLabel linkCreate;
        private Label label3;
        private Panel pnlTitlebar;
        private Panel panel3;
        private PictureBox ptbSubmit;
        private PictureBox pictureBox2;
        private PictureBox pbxMinimize;
        private PictureBox pbxClose;
        private TextBox tbxPassword;
        private TextBox tbxUser;
        private CheckBox chkShowpass;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private PictureBox pictureBox3;
        private Panel panel2;
        private Panel imgPanel;
        private PictureBox ptb2nd;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Label label2;
        private Label lblTime;
        private System.Windows.Forms.Timer timeReal;
        private Label label4;
        private Label label5;
    }
}
