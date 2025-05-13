namespace SnipIt
{
    partial class ForgotPass
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPass));
            pnlTitlebar = new Panel();
            label1 = new Label();
            ptbBack = new PictureBox();
            ptbMinimize = new PictureBox();
            ptbClose = new PictureBox();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            tbxEmail = new TextBox();
            label3 = new Label();
            btnContinue = new ReaLTaiizor.Controls.DungeonButtonLeft();
            label5 = new Label();
            tbxVerification = new TextBox();
            tbxNewPass = new TextBox();
            tbxConfirmNewPass = new TextBox();
            label7 = new Label();
            label4 = new Label();
            label6 = new Label();
            btnVerify = new ReaLTaiizor.Controls.DungeonButtonLeft();
            btnSendCode = new ReaLTaiizor.Controls.DungeonButtonLeft();
            chkShowpass = new CheckBox();
            pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pnlTitlebar
            // 
            pnlTitlebar.BackColor = Color.Transparent;
            pnlTitlebar.BackgroundImage = (Image)resources.GetObject("pnlTitlebar.BackgroundImage");
            pnlTitlebar.Controls.Add(label1);
            pnlTitlebar.Controls.Add(ptbBack);
            pnlTitlebar.Controls.Add(ptbMinimize);
            pnlTitlebar.Controls.Add(ptbClose);
            pnlTitlebar.Dock = DockStyle.Top;
            pnlTitlebar.Location = new Point(0, 0);
            pnlTitlebar.Name = "pnlTitlebar";
            pnlTitlebar.Size = new Size(426, 38);
            pnlTitlebar.TabIndex = 0;
            pnlTitlebar.Paint += pnlTitlebar_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(37, 10);
            label1.Name = "label1";
            label1.Size = new Size(76, 18);
            label1.TabIndex = 3;
            label1.Text = "Go Back";
            // 
            // ptbBack
            // 
            ptbBack.BackColor = Color.Transparent;
            ptbBack.Image = (Image)resources.GetObject("ptbBack.Image");
            ptbBack.Location = new Point(12, 9);
            ptbBack.Name = "ptbBack";
            ptbBack.Size = new Size(20, 20);
            ptbBack.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbBack.TabIndex = 2;
            ptbBack.TabStop = false;
            ptbBack.Click += ptbBack_Click;
            ptbBack.MouseEnter += ptbBack_MouseEnter;
            ptbBack.MouseLeave += ptbBack_MouseLeave;
            // 
            // ptbMinimize
            // 
            ptbMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ptbMinimize.BackColor = Color.Transparent;
            ptbMinimize.Image = (Image)resources.GetObject("ptbMinimize.Image");
            ptbMinimize.Location = new Point(357, 12);
            ptbMinimize.Name = "ptbMinimize";
            ptbMinimize.Size = new Size(15, 15);
            ptbMinimize.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbMinimize.TabIndex = 1;
            ptbMinimize.TabStop = false;
            ptbMinimize.Click += ptbMinimize_Click;
            // 
            // ptbClose
            // 
            ptbClose.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ptbClose.BackColor = Color.Transparent;
            ptbClose.Image = (Image)resources.GetObject("ptbClose.Image");
            ptbClose.Location = new Point(399, 12);
            ptbClose.Name = "ptbClose";
            ptbClose.Size = new Size(15, 15);
            ptbClose.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbClose.TabIndex = 0;
            ptbClose.TabStop = false;
            ptbClose.Click += ptbClose_Click;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 35);
            panel1.Name = "panel1";
            panel1.Size = new Size(426, 157);
            panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(6, -83);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(414, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 176);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(426, 86);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // tbxEmail
            // 
            tbxEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxEmail.Location = new Point(203, 249);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(211, 29);
            tbxEmail.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(133, 254);
            label3.Name = "label3";
            label3.Size = new Size(64, 18);
            label3.TabIndex = 8;
            label3.Text = "EMAIL:";
            label3.TextAlign = ContentAlignment.TopCenter;
            label3.Click += label3_Click;
            // 
            // btnContinue
            // 
            btnContinue.BackColor = Color.Transparent;
            btnContinue.BorderColor = Color.FromArgb(180, 180, 180);
            btnContinue.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnContinue.Image = null;
            btnContinue.ImageAlign = ContentAlignment.MiddleLeft;
            btnContinue.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnContinue.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnContinue.Location = new Point(149, 495);
            btnContinue.Name = "btnContinue";
            btnContinue.PressedColorA = Color.FromArgb(226, 226, 226);
            btnContinue.PressedColorB = Color.FromArgb(237, 237, 237);
            btnContinue.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnContinue.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnContinue.Size = new Size(122, 30);
            btnContinue.TabIndex = 9;
            btnContinue.Text = "CONTINUE";
            btnContinue.TextAlignment = StringAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(14, 290);
            label5.Name = "label5";
            label5.Size = new Size(183, 18);
            label5.TabIndex = 12;
            label5.Text = "VERIFICATION CODE:";
            label5.TextAlign = ContentAlignment.TopCenter;
            // 
            // tbxVerification
            // 
            tbxVerification.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxVerification.Location = new Point(203, 285);
            tbxVerification.Name = "tbxVerification";
            tbxVerification.Size = new Size(211, 29);
            tbxVerification.TabIndex = 11;
            // 
            // tbxNewPass
            // 
            tbxNewPass.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxNewPass.Location = new Point(203, 380);
            tbxNewPass.Name = "tbxNewPass";
            tbxNewPass.Size = new Size(211, 29);
            tbxNewPass.TabIndex = 13;
            tbxNewPass.TextChanged += tbxNewPass_TextChanged;
            // 
            // tbxConfirmNewPass
            // 
            tbxConfirmNewPass.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxConfirmNewPass.Location = new Point(203, 416);
            tbxConfirmNewPass.Name = "tbxConfirmNewPass";
            tbxConfirmNewPass.Size = new Size(211, 29);
            tbxConfirmNewPass.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Rounded MT Bold", 12F);
            label7.ForeColor = Color.White;
            label7.Location = new Point(136, 361);
            label7.Name = "label7";
            label7.Size = new Size(0, 18);
            label7.TabIndex = 17;
            label7.TextAlign = ContentAlignment.TopCenter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(45, 385);
            label4.Name = "label4";
            label4.Size = new Size(152, 18);
            label4.TabIndex = 18;
            label4.Text = "NEW PASSWORD:";
            label4.TextAlign = ContentAlignment.TopCenter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(6, 421);
            label6.Name = "label6";
            label6.Size = new Size(191, 18);
            label6.TabIndex = 19;
            label6.Text = "CONFIRM PASSWORD:";
            label6.TextAlign = ContentAlignment.TopCenter;
            // 
            // btnVerify
            // 
            btnVerify.BackColor = Color.Transparent;
            btnVerify.BorderColor = Color.FromArgb(180, 180, 180);
            btnVerify.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnVerify.Image = null;
            btnVerify.ImageAlign = ContentAlignment.MiddleLeft;
            btnVerify.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnVerify.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnVerify.Location = new Point(223, 329);
            btnVerify.Name = "btnVerify";
            btnVerify.PressedColorA = Color.FromArgb(226, 226, 226);
            btnVerify.PressedColorB = Color.FromArgb(237, 237, 237);
            btnVerify.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnVerify.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnVerify.Size = new Size(122, 30);
            btnVerify.TabIndex = 20;
            btnVerify.Text = "VERIFY";
            btnVerify.TextAlignment = StringAlignment.Center;
            // 
            // btnSendCode
            // 
            btnSendCode.BackColor = Color.Transparent;
            btnSendCode.BorderColor = Color.FromArgb(180, 180, 180);
            btnSendCode.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSendCode.Image = null;
            btnSendCode.ImageAlign = ContentAlignment.MiddleLeft;
            btnSendCode.InactiveColorA = Color.FromArgb(253, 252, 252);
            btnSendCode.InactiveColorB = Color.FromArgb(239, 237, 236);
            btnSendCode.Location = new Point(95, 329);
            btnSendCode.Name = "btnSendCode";
            btnSendCode.PressedColorA = Color.FromArgb(226, 226, 226);
            btnSendCode.PressedColorB = Color.FromArgb(237, 237, 237);
            btnSendCode.PressedContourColorA = Color.FromArgb(167, 167, 167);
            btnSendCode.PressedContourColorB = Color.FromArgb(167, 167, 167);
            btnSendCode.Size = new Size(122, 30);
            btnSendCode.TabIndex = 21;
            btnSendCode.Text = "SEND CODE";
            btnSendCode.TextAlignment = StringAlignment.Center;
            btnSendCode.Click += btnSendCode_Click;
            // 
            // chkShowpass
            // 
            chkShowpass.AutoSize = true;
            chkShowpass.BackColor = Color.Transparent;
            chkShowpass.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowpass.ForeColor = Color.White;
            chkShowpass.Location = new Point(295, 451);
            chkShowpass.Name = "chkShowpass";
            chkShowpass.Size = new Size(119, 19);
            chkShowpass.TabIndex = 22;
            chkShowpass.Text = "Show Password";
            chkShowpass.UseVisualStyleBackColor = false;
            chkShowpass.CheckedChanged += chkShowpass_CheckedChanged;
            // 
            // ForgotPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(426, 537);
            Controls.Add(chkShowpass);
            Controls.Add(btnSendCode);
            Controls.Add(btnVerify);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label7);
            Controls.Add(tbxConfirmNewPass);
            Controls.Add(tbxNewPass);
            Controls.Add(label5);
            Controls.Add(tbxVerification);
            Controls.Add(btnContinue);
            Controls.Add(label3);
            Controls.Add(tbxEmail);
            Controls.Add(panel1);
            Controls.Add(pnlTitlebar);
            Controls.Add(pictureBox2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ForgotPass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SnipIt - Forgot Password";
            Load += ForgotPass_Load;
            pnlTitlebar.ResumeLayout(false);
            pnlTitlebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ptbBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTitlebar;
        private PictureBox ptbMinimize;
        private PictureBox ptbClose;
        private PictureBox ptbBack;
        private Label label1;
        private Panel panel1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox tbxEmail;
        private Label label3;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnContinue;
        private Label label5;
        private TextBox tbxVerification;
        private TextBox tbxNewPass;
        private TextBox tbxConfirmNewPass;
        private Label label7;
        private Label label4;
        private Label label6;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnVerify;
        private ReaLTaiizor.Controls.DungeonButtonLeft btnSendCode;
        private CheckBox chkShowpass;
    }
}