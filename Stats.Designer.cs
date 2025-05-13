namespace SnipIt
{
    partial class Stats
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
            pnlDonut = new Panel();
            pnlCdist = new Panel();
            pnlCppdist = new Panel();
            pnlPydist = new Panel();
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
            lblTitle.Size = new Size(1214, 56);
            lblTitle.TabIndex = 5;
            lblTitle.Text = "Statistics and Code Distribution";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlDonut
            // 
            pnlDonut.Location = new Point(3, 59);
            pnlDonut.Name = "pnlDonut";
            pnlDonut.Size = new Size(576, 581);
            pnlDonut.TabIndex = 6;
            pnlDonut.Paint += pnlDonut_Paint;
            // 
            // pnlCdist
            // 
            pnlCdist.Location = new Point(585, 59);
            pnlCdist.Name = "pnlCdist";
            pnlCdist.Size = new Size(626, 190);
            pnlCdist.TabIndex = 7;
            // 
            // pnlCppdist
            // 
            pnlCppdist.Location = new Point(585, 254);
            pnlCppdist.Name = "pnlCppdist";
            pnlCppdist.Size = new Size(626, 190);
            pnlCppdist.TabIndex = 8;
            // 
            // pnlPydist
            // 
            pnlPydist.Location = new Point(585, 450);
            pnlPydist.Name = "pnlPydist";
            pnlPydist.Size = new Size(626, 190);
            pnlPydist.TabIndex = 9;
            // 
            // Stats
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(134, 134, 172);
            Controls.Add(pnlCppdist);
            Controls.Add(pnlPydist);
            Controls.Add(pnlCdist);
            Controls.Add(pnlDonut);
            Controls.Add(lblTitle);
            Name = "Stats";
            Size = new Size(1214, 643);
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitle;
        private Panel pnlDonut;
        private Panel pnlCdist;
        private Panel pnlCppdist;
        private Panel pnlPydist;
    }
}
