namespace SnipIt.User_Controls
{
    partial class Settings
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
            lblTitle = new Label();
            lblTheme = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(39, 39, 87);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(788, 56);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Settings";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTheme
            // 
            lblTheme.AutoSize = true;
            lblTheme.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTheme.ForeColor = Color.White;
            lblTheme.Location = new Point(22, 73);
            lblTheme.Name = "lblTheme";
            lblTheme.Size = new Size(304, 37);
            lblTheme.TabIndex = 1;
            lblTheme.Text = "Theme and Design";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(22, 110);
            label1.Name = "label1";
            label1.Size = new Size(393, 74);
            label1.TabIndex = 2;
            label1.Text = "• Dark/Light Mode\r\n• Change Snippet Colors";
            label1.Click += label1_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(134, 134, 172);
            Controls.Add(label1);
            Controls.Add(lblTheme);
            Controls.Add(lblTitle);
            Name = "Settings";
            Size = new Size(788, 656);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private Label lblTheme;
        private Label label1;
    }
}