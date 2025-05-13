namespace SnipIt
{
    partial class Loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loading));
            pnlBackground = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            aloneProgressBar1 = new ReaLTaiizor.Controls.AloneProgressBar();
            pnlBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlBackground
            // 
            pnlBackground.Controls.Add(pictureBox2);
            pnlBackground.Controls.Add(panel2);
            pnlBackground.Controls.Add(aloneProgressBar1);
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.Location = new Point(0, 0);
            pnlBackground.Name = "pnlBackground";
            pnlBackground.Size = new Size(600, 400);
            pnlBackground.TabIndex = 2;
            pnlBackground.Paint += pnlBackground_Paint;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(470, 337);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(127, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(600, 331);
            panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-102, -82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(802, 553);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // aloneProgressBar1
            // 
            aloneProgressBar1.BackColor = Color.Transparent;
            aloneProgressBar1.BackgroundColor = Color.Goldenrod;
            aloneProgressBar1.BaseColor = Color.FromArgb(45, 45, 48);
            aloneProgressBar1.BorderColor = Color.DodgerBlue;
            aloneProgressBar1.Location = new Point(3, 370);
            aloneProgressBar1.Maximum = 100;
            aloneProgressBar1.Minimum = 0;
            aloneProgressBar1.Name = "aloneProgressBar1";
            aloneProgressBar1.Size = new Size(594, 27);
            aloneProgressBar1.Stripes = Color.White;
            aloneProgressBar1.TabIndex = 0;
            aloneProgressBar1.Text = "aloneProgressBar1";
            aloneProgressBar1.Value = 100;
            // 
            // Loading
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 400);
            Controls.Add(pnlBackground);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Loading";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Loading";
            Load += Loading_Load;
            pnlBackground.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlBackground;
        private ReaLTaiizor.Controls.AloneProgressBar aloneProgressBar1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}