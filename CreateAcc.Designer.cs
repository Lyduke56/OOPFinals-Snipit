namespace SnipIt
{
    partial class CreateAcc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAcc));
            pnlTitlebar = new Panel();
            label1 = new Label();
            ptbBack = new PictureBox();
            ptbMinimize = new PictureBox();
            ptbClose = new PictureBox();
            tbxUser = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            tbxPass = new TextBox();
            tbxConfirm = new TextBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            label6 = new Label();
            tbxEmail = new TextBox();
            taibtnCreateAcc = new ReaLTaiizor.Controls.DungeonButtonLeft();
            chkShowpass = new CheckBox();
            pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitlebar
            // 
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
            pnlTitlebar.Paint += panel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(37, 10);
            label1.Name = "label1";
            label1.Size = new Size(76, 18);
            label1.TabIndex = 4;
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
            ptbBack.TabIndex = 3;
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
            ptbMinimize.TabIndex = 2;
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
            // tbxUser
            // 
            tbxUser.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxUser.Location = new Point(195, 261);
            tbxUser.Name = "tbxUser";
            tbxUser.Size = new Size(177, 29);
            tbxUser.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Rounded MT Bold", 12F);
            label2.ForeColor = Color.White;
            label2.Location = new Point(87, 266);
            label2.Name = "label2";
            label2.Size = new Size(102, 18);
            label2.TabIndex = 5;
            label2.Text = "USERNAME";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Rounded MT Bold", 12F);
            label3.ForeColor = Color.White;
            label3.Location = new Point(84, 310);
            label3.Name = "label3";
            label3.Size = new Size(105, 18);
            label3.TabIndex = 6;
            label3.Text = "PASSWORD";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Rounded MT Bold", 12F);
            label4.ForeColor = Color.White;
            label4.Location = new Point(84, 348);
            label4.Name = "label4";
            label4.Size = new Size(105, 36);
            label4.TabIndex = 7;
            label4.Text = "CONFIRM\r\nPASSWORD";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(6, -70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(414, 240);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tbxPass
            // 
            tbxPass.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxPass.Location = new Point(195, 305);
            tbxPass.Name = "tbxPass";
            tbxPass.Size = new Size(177, 29);
            tbxPass.TabIndex = 12;
            tbxPass.TextChanged += tbxPass_TextChanged;
            // 
            // tbxConfirm
            // 
            tbxConfirm.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxConfirm.Location = new Point(195, 352);
            tbxConfirm.Name = "tbxConfirm";
            tbxConfirm.Size = new Size(177, 29);
            tbxConfirm.TabIndex = 13;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(0, 186);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(426, 77);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 14;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 35);
            panel1.Name = "panel1";
            panel1.Size = new Size(426, 157);
            panel1.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Location = new Point(3, 158);
            panel2.Name = "panel2";
            panel2.Size = new Size(423, 345);
            panel2.TabIndex = 18;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Rounded MT Bold", 12F);
            label6.ForeColor = Color.White;
            label6.Location = new Point(137, 429);
            label6.Name = "label6";
            label6.Size = new Size(146, 18);
            label6.TabIndex = 17;
            label6.Text = "EMAIL ACCOUNT";
            label6.TextAlign = ContentAlignment.MiddleRight;
            label6.Click += label6_Click;
            // 
            // tbxEmail
            // 
            tbxEmail.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbxEmail.Location = new Point(97, 454);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(228, 29);
            tbxEmail.TabIndex = 18;
            // 
            // taibtnCreateAcc
            // 
            taibtnCreateAcc.BackColor = Color.Transparent;
            taibtnCreateAcc.BorderColor = Color.FromArgb(180, 180, 180);
            taibtnCreateAcc.Font = new Font("Impact", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            taibtnCreateAcc.Image = null;
            taibtnCreateAcc.ImageAlign = ContentAlignment.MiddleLeft;
            taibtnCreateAcc.InactiveColorA = Color.FromArgb(253, 252, 252);
            taibtnCreateAcc.InactiveColorB = Color.FromArgb(239, 237, 236);
            taibtnCreateAcc.Location = new Point(125, 495);
            taibtnCreateAcc.Name = "taibtnCreateAcc";
            taibtnCreateAcc.PressedColorA = Color.FromArgb(226, 226, 226);
            taibtnCreateAcc.PressedColorB = Color.FromArgb(237, 237, 237);
            taibtnCreateAcc.PressedContourColorA = Color.FromArgb(167, 167, 167);
            taibtnCreateAcc.PressedContourColorB = Color.FromArgb(167, 167, 167);
            taibtnCreateAcc.Size = new Size(177, 30);
            taibtnCreateAcc.TabIndex = 20;
            taibtnCreateAcc.Text = "CREATE ACCOUNT";
            taibtnCreateAcc.TextAlignment = StringAlignment.Center;
            taibtnCreateAcc.Click += taibtnCreateAcc_Click;
            // 
            // chkShowpass
            // 
            chkShowpass.AutoSize = true;
            chkShowpass.BackColor = Color.Transparent;
            chkShowpass.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkShowpass.ForeColor = Color.White;
            chkShowpass.Location = new Point(253, 387);
            chkShowpass.Name = "chkShowpass";
            chkShowpass.Size = new Size(119, 19);
            chkShowpass.TabIndex = 21;
            chkShowpass.Text = "Show Password";
            chkShowpass.UseVisualStyleBackColor = false;
            chkShowpass.CheckedChanged += chkShowpass_CheckedChanged;
            // 
            // CreateAcc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(426, 537);
            Controls.Add(chkShowpass);
            Controls.Add(taibtnCreateAcc);
            Controls.Add(tbxEmail);
            Controls.Add(label6);
            Controls.Add(panel1);
            Controls.Add(tbxConfirm);
            Controls.Add(tbxPass);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(tbxUser);
            Controls.Add(pnlTitlebar);
            Controls.Add(pictureBox2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CreateAcc";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SnipIt - Create Account";
            Load += CreateAcc_Load;
            pnlTitlebar.ResumeLayout(false);
            pnlTitlebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ptbBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTitlebar;
        private PictureBox ptbClose;
        private PictureBox ptbMinimize;
        private Label label1;
        private PictureBox ptbBack;
        private TextBox tbxUser;
        private Label label2;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
        private TextBox tbxPass;
        private TextBox tbxConfirm;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Panel panel2;
        private Label label6;
        private TextBox tbxEmail;
        private ReaLTaiizor.Controls.DungeonButtonLeft taibtnCreateAcc;
        private CheckBox chkShowpass;
    }
}