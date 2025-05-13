namespace SnipIt
{
    partial class CodePlayground
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodePlayground));
            pnlTitlebar = new Panel();
            ptbMaximize = new PictureBox();
            label1 = new Label();
            ptbBack = new PictureBox();
            pnlSettings = new Panel();
            btnCopy = new Button();
            btnExport = new Button();
            btnSave = new Button();
            btnRun = new Button();
            btnPython = new Button();
            btnCPP = new Button();
            btnC = new Button();
            splitContainer1 = new SplitContainer();
            pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbMaximize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbBack).BeginInit();
            pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitlebar
            // 
            pnlTitlebar.BackColor = Color.Transparent;
            pnlTitlebar.Controls.Add(ptbMaximize);
            pnlTitlebar.Controls.Add(label1);
            pnlTitlebar.Controls.Add(ptbBack);
            pnlTitlebar.Dock = DockStyle.Top;
            pnlTitlebar.Location = new Point(0, 0);
            pnlTitlebar.Name = "pnlTitlebar";
            pnlTitlebar.Size = new Size(1248, 38);
            pnlTitlebar.TabIndex = 0;
            // 
            // ptbMaximize
            // 
            ptbMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ptbMaximize.Image = (Image)resources.GetObject("ptbMaximize.Image");
            ptbMaximize.Location = new Point(1221, 12);
            ptbMaximize.Name = "ptbMaximize";
            ptbMaximize.Size = new Size(15, 15);
            ptbMaximize.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbMaximize.TabIndex = 8;
            ptbMaximize.TabStop = false;
            ptbMaximize.Click += ptbMaximize_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(36, 10);
            label1.Name = "label1";
            label1.Size = new Size(76, 18);
            label1.TabIndex = 7;
            label1.Text = "Go Back";
            // 
            // ptbBack
            // 
            ptbBack.BackColor = Color.Transparent;
            ptbBack.Image = (Image)resources.GetObject("ptbBack.Image");
            ptbBack.Location = new Point(11, 9);
            ptbBack.Name = "ptbBack";
            ptbBack.Size = new Size(20, 20);
            ptbBack.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbBack.TabIndex = 6;
            ptbBack.TabStop = false;
            ptbBack.Click += ptbBack_Click;
            // 
            // pnlSettings
            // 
            pnlSettings.BackColor = Color.Transparent;
            pnlSettings.Controls.Add(btnCopy);
            pnlSettings.Controls.Add(btnExport);
            pnlSettings.Controls.Add(btnSave);
            pnlSettings.Controls.Add(btnRun);
            pnlSettings.Controls.Add(btnPython);
            pnlSettings.Controls.Add(btnCPP);
            pnlSettings.Controls.Add(btnC);
            pnlSettings.Dock = DockStyle.Left;
            pnlSettings.Location = new Point(0, 38);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Size = new Size(50, 604);
            pnlSettings.TabIndex = 1;
            // 
            // btnCopy
            // 
            btnCopy.BackColor = Color.Transparent;
            btnCopy.BackgroundImage = (Image)resources.GetObject("btnCopy.BackgroundImage");
            btnCopy.BackgroundImageLayout = ImageLayout.Stretch;
            btnCopy.FlatStyle = FlatStyle.Popup;
            btnCopy.Location = new Point(9, 389);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(35, 35);
            btnCopy.TabIndex = 9;
            btnCopy.UseVisualStyleBackColor = false;
            btnCopy.Click += btnCopy_Click;
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.Transparent;
            btnExport.BackgroundImage = (Image)resources.GetObject("btnExport.BackgroundImage");
            btnExport.BackgroundImageLayout = ImageLayout.Stretch;
            btnExport.FlatStyle = FlatStyle.Popup;
            btnExport.Location = new Point(8, 501);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(35, 35);
            btnExport.TabIndex = 8;
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Click += btnExport_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Transparent;
            btnSave.BackgroundImage = (Image)resources.GetObject("btnSave.BackgroundImage");
            btnSave.BackgroundImageLayout = ImageLayout.Stretch;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Location = new Point(8, 542);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(35, 35);
            btnSave.TabIndex = 6;
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.Transparent;
            btnRun.BackgroundImage = (Image)resources.GetObject("btnRun.BackgroundImage");
            btnRun.BackgroundImageLayout = ImageLayout.Stretch;
            btnRun.FlatStyle = FlatStyle.Popup;
            btnRun.Location = new Point(8, 460);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(35, 35);
            btnRun.TabIndex = 5;
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // btnPython
            // 
            btnPython.BackColor = Color.Transparent;
            btnPython.BackgroundImage = (Image)resources.GetObject("btnPython.BackgroundImage");
            btnPython.BackgroundImageLayout = ImageLayout.Stretch;
            btnPython.FlatStyle = FlatStyle.Popup;
            btnPython.Location = new Point(8, 128);
            btnPython.Name = "btnPython";
            btnPython.Size = new Size(35, 35);
            btnPython.TabIndex = 2;
            btnPython.UseVisualStyleBackColor = false;
            btnPython.Click += btnPython_Click;
            // 
            // btnCPP
            // 
            btnCPP.BackColor = Color.Transparent;
            btnCPP.BackgroundImage = (Image)resources.GetObject("btnCPP.BackgroundImage");
            btnCPP.BackgroundImageLayout = ImageLayout.Stretch;
            btnCPP.FlatStyle = FlatStyle.Popup;
            btnCPP.Location = new Point(8, 77);
            btnCPP.Name = "btnCPP";
            btnCPP.Size = new Size(35, 35);
            btnCPP.TabIndex = 1;
            btnCPP.UseVisualStyleBackColor = false;
            btnCPP.Click += btnCPP_Click;
            // 
            // btnC
            // 
            btnC.BackColor = Color.Transparent;
            btnC.BackgroundImage = (Image)resources.GetObject("btnC.BackgroundImage");
            btnC.BackgroundImageLayout = ImageLayout.Stretch;
            btnC.FlatStyle = FlatStyle.Popup;
            btnC.Location = new Point(8, 27);
            btnC.Name = "btnC";
            btnC.Size = new Size(35, 35);
            btnC.TabIndex = 0;
            btnC.UseVisualStyleBackColor = false;
            btnC.Click += btnC_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(50, 38);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AccessibleName = "scintillaEditor";
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            splitContainer1.Panel1MinSize = 302;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AccessibleName = "scintillaOutput";
            splitContainer1.Panel2MinSize = 302;
            splitContainer1.Size = new Size(1198, 604);
            splitContainer1.SplitterDistance = 603;
            splitContainer1.TabIndex = 2;
            // 
            // CodePlayground
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1248, 642);
            Controls.Add(splitContainer1);
            Controls.Add(pnlSettings);
            Controls.Add(pnlTitlebar);
            Name = "CodePlayground";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += CodePlayground_Load;
            Resize += CodePlayground_Resize;
            pnlTitlebar.ResumeLayout(false);
            pnlTitlebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)ptbMaximize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbBack).EndInit();
            pnlSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTitlebar;
        private Panel pnlSettings;
        private Button btnPython;
        private Button btnCPP;
        private Button btnC;
        private Label label1;
        private PictureBox ptbBack;
        private Button btnRun;
        private SplitContainer splitContainer1;
        private Button btnSave;
        private PictureBox ptbMaximize;
        private Button btnExport;
        private Button btnCopy;
    }
}