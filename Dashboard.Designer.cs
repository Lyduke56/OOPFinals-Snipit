namespace SnipIt
{
    partial class Dashboard
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            pnlTitlebar = new Panel();
            ptbMaximize = new PictureBox();
            ptbMinimize = new PictureBox();
            ptbClose = new PictureBox();
            sidebarTimer = new System.Windows.Forms.Timer(components);
            parrotDashboard = new ReaLTaiizor.Controls.ParrotSlidingPanel();
            ptbReport = new PictureBox();
            ptbAbout = new PictureBox();
            ptbAddsnippet = new PictureBox();
            ptbProfile = new PictureBox();
            ptbSnips = new PictureBox();
            ptbSettings = new PictureBox();
            pnlUC = new Panel();
            mainInterface = new Panel();
            pnlTitlebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbMaximize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).BeginInit();
            parrotDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ptbReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbAbout).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbAddsnippet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbProfile).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbSnips).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptbSettings).BeginInit();
            mainInterface.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitlebar
            // 
            pnlTitlebar.BackColor = Color.Transparent;
            pnlTitlebar.Controls.Add(ptbMaximize);
            pnlTitlebar.Controls.Add(ptbMinimize);
            pnlTitlebar.Controls.Add(ptbClose);
            pnlTitlebar.Dock = DockStyle.Top;
            pnlTitlebar.Location = new Point(0, 0);
            pnlTitlebar.Name = "pnlTitlebar";
            pnlTitlebar.Size = new Size(1264, 38);
            pnlTitlebar.TabIndex = 3;
            pnlTitlebar.Paint += pnlTitlebar_Paint;
            pnlTitlebar.MouseDown += panelTitleBar_MouseDown;
            // 
            // ptbMaximize
            // 
            ptbMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ptbMaximize.Image = (Image)resources.GetObject("ptbMaximize.Image");
            ptbMaximize.Location = new Point(1194, 12);
            ptbMaximize.Name = "ptbMaximize";
            ptbMaximize.Size = new Size(15, 15);
            ptbMaximize.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbMaximize.TabIndex = 2;
            ptbMaximize.TabStop = false;
            ptbMaximize.Click += ptbMaximize_Click;
            // 
            // ptbMinimize
            // 
            ptbMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            ptbMinimize.Image = (Image)resources.GetObject("ptbMinimize.Image");
            ptbMinimize.Location = new Point(1152, 12);
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
            ptbClose.Image = (Image)resources.GetObject("ptbClose.Image");
            ptbClose.Location = new Point(1236, 12);
            ptbClose.Name = "ptbClose";
            ptbClose.Size = new Size(15, 15);
            ptbClose.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbClose.TabIndex = 0;
            ptbClose.TabStop = false;
            ptbClose.Click += ptbClose_Click;
            // 
            // parrotDashboard
            // 
            parrotDashboard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            parrotDashboard.BottomLeft = Color.Black;
            parrotDashboard.BottomRight = Color.Indigo;
            parrotDashboard.CollapseControl = null;
            parrotDashboard.Collapsed = false;
            parrotDashboard.CompositingQualityType = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            parrotDashboard.Controls.Add(ptbReport);
            parrotDashboard.Controls.Add(ptbAbout);
            parrotDashboard.Controls.Add(ptbAddsnippet);
            parrotDashboard.Controls.Add(ptbProfile);
            parrotDashboard.Controls.Add(ptbSnips);
            parrotDashboard.Controls.Add(ptbSettings);
            parrotDashboard.Dock = DockStyle.Left;
            parrotDashboard.HideControls = false;
            parrotDashboard.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            parrotDashboard.Location = new Point(0, 0);
            parrotDashboard.Name = "parrotDashboard";
            parrotDashboard.PanelWidthCollapsed = 50;
            parrotDashboard.PanelWidthExpanded = 50;
            parrotDashboard.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            parrotDashboard.PrimerColor = Color.DimGray;
            parrotDashboard.Size = new Size(50, 643);
            parrotDashboard.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            parrotDashboard.Style = ReaLTaiizor.Controls.ParrotGradientPanel.GradientStyle.Corners;
            parrotDashboard.TabIndex = 20;
            parrotDashboard.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            parrotDashboard.TopLeft = Color.Black;
            parrotDashboard.TopRight = Color.Black;
            // 
            // ptbReport
            // 
            ptbReport.BackColor = Color.Transparent;
            ptbReport.Image = (Image)resources.GetObject("ptbReport.Image");
            ptbReport.Location = new Point(7, 543);
            ptbReport.Name = "ptbReport";
            ptbReport.Size = new Size(35, 35);
            ptbReport.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbReport.TabIndex = 36;
            ptbReport.TabStop = false;
            ptbReport.Click += ptbReport_Click;
            // 
            // ptbAbout
            // 
            ptbAbout.BackColor = Color.Transparent;
            ptbAbout.Image = (Image)resources.GetObject("ptbAbout.Image");
            ptbAbout.Location = new Point(7, 596);
            ptbAbout.Name = "ptbAbout";
            ptbAbout.Size = new Size(35, 35);
            ptbAbout.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbAbout.TabIndex = 35;
            ptbAbout.TabStop = false;
            ptbAbout.Click += ptbAbout_Click_1;
            // 
            // ptbAddsnippet
            // 
            ptbAddsnippet.BackColor = Color.Transparent;
            ptbAddsnippet.Image = (Image)resources.GetObject("ptbAddsnippet.Image");
            ptbAddsnippet.Location = new Point(7, 215);
            ptbAddsnippet.Name = "ptbAddsnippet";
            ptbAddsnippet.Size = new Size(35, 35);
            ptbAddsnippet.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbAddsnippet.TabIndex = 31;
            ptbAddsnippet.TabStop = false;
            ptbAddsnippet.Click += pictureBox2_Click;
            // 
            // ptbProfile
            // 
            ptbProfile.Image = (Image)resources.GetObject("ptbProfile.Image");
            ptbProfile.Location = new Point(7, 17);
            ptbProfile.Name = "ptbProfile";
            ptbProfile.Size = new Size(35, 35);
            ptbProfile.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbProfile.TabIndex = 0;
            ptbProfile.TabStop = false;
            ptbProfile.Click += ptbProfile_Click;
            // 
            // ptbSnips
            // 
            ptbSnips.BackColor = Color.Transparent;
            ptbSnips.Image = (Image)resources.GetObject("ptbSnips.Image");
            ptbSnips.Location = new Point(7, 81);
            ptbSnips.Name = "ptbSnips";
            ptbSnips.Size = new Size(35, 35);
            ptbSnips.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbSnips.TabIndex = 16;
            ptbSnips.TabStop = false;
            ptbSnips.Click += ptbSnips_Click;
            // 
            // ptbSettings
            // 
            ptbSettings.BackColor = Color.Transparent;
            ptbSettings.Image = (Image)resources.GetObject("ptbSettings.Image");
            ptbSettings.Location = new Point(7, 148);
            ptbSettings.Name = "ptbSettings";
            ptbSettings.Size = new Size(35, 35);
            ptbSettings.SizeMode = PictureBoxSizeMode.StretchImage;
            ptbSettings.TabIndex = 18;
            ptbSettings.TabStop = false;
            ptbSettings.Click += ptbStats_Click;
            // 
            // pnlUC
            // 
            pnlUC.AutoScroll = true;
            pnlUC.Dock = DockStyle.Fill;
            pnlUC.Location = new Point(50, 0);
            pnlUC.Name = "pnlUC";
            pnlUC.Size = new Size(1214, 643);
            pnlUC.TabIndex = 21;
            pnlUC.Paint += pnlUC_Paint;
            // 
            // mainInterface
            // 
            mainInterface.BackColor = Color.Transparent;
            mainInterface.Controls.Add(pnlUC);
            mainInterface.Controls.Add(parrotDashboard);
            mainInterface.Dock = DockStyle.Fill;
            mainInterface.Location = new Point(0, 38);
            mainInterface.Name = "mainInterface";
            mainInterface.Size = new Size(1264, 643);
            mainInterface.TabIndex = 4;
            mainInterface.Paint += mainInterface_Paint;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(1264, 681);
            Controls.Add(mainInterface);
            Controls.Add(pnlTitlebar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SnipIt - Code Snippet Database Manager";
            Load += Dashboard_Load;
            pnlTitlebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ptbMaximize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbMinimize).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbClose).EndInit();
            parrotDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ptbReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbAbout).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbAddsnippet).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbProfile).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbSnips).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptbSettings).EndInit();
            mainInterface.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlTitlebar;
        private System.Windows.Forms.Timer sidebarTimer;
        private PictureBox ptbMinimize;
        private PictureBox ptbClose;
        private ReaLTaiizor.Controls.ParrotSlidingPanel parrotDashboard;
        private PictureBox ptbProfile;
        private PictureBox ptbSnips;
        private PictureBox ptbSettings;
        private Panel pnlUC;
        private Panel mainInterface;
        private PictureBox ptbMaximize;
        private PictureBox ptbAddsnippet;
        private PictureBox ptbAbout;
        private PictureBox ptbReport;
    }
}