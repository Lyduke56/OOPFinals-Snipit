namespace SnipIt.User_Controls
{
    partial class Snips
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Snips));
            flowPanel = new FlowLayoutPanel();
            topPanel = new Panel();
            btnRefresh = new Button();
            cmbCodeType = new ComboBox();
            btnFilterDate = new Button();
            label1 = new Label();
            lblPin = new Label();
            btnPinnedOnly = new Button();
            btnFilterPython = new Button();
            btnFilterCpp = new Button();
            btnFilterDefault = new Button();
            btnFilterC = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            lblTitle = new Label();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowPanel
            // 
            flowPanel.AutoScroll = true;
            flowPanel.BackColor = Color.FromArgb(134, 134, 172);
            flowPanel.Dock = DockStyle.Fill;
            flowPanel.Location = new Point(0, 131);
            flowPanel.Name = "flowPanel";
            flowPanel.Padding = new Padding(18, 19, 18, 19);
            flowPanel.Size = new Size(1214, 512);
            flowPanel.TabIndex = 0;
            flowPanel.Paint += flowPanel_Paint;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(80, 80, 129);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(cmbCodeType);
            topPanel.Controls.Add(btnFilterDate);
            topPanel.Controls.Add(label1);
            topPanel.Controls.Add(lblPin);
            topPanel.Controls.Add(btnPinnedOnly);
            topPanel.Controls.Add(btnFilterPython);
            topPanel.Controls.Add(btnFilterCpp);
            topPanel.Controls.Add(btnFilterDefault);
            topPanel.Controls.Add(btnFilterC);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnSearch);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 56);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(9);
            topPanel.Size = new Size(1214, 75);
            topPanel.TabIndex = 1;
            topPanel.Paint += topPanel_Paint;
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.FromArgb(39, 39, 87);
            btnRefresh.BackgroundImage = (Image)resources.GetObject("btnRefresh.BackgroundImage");
            btnRefresh.BackgroundImageLayout = ImageLayout.Zoom;
            btnRefresh.Cursor = Cursors.Hand;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(415, 26);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(22, 22);
            btnRefresh.TabIndex = 14;
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // cmbCodeType
            // 
            cmbCodeType.FormattingEnabled = true;
            cmbCodeType.Location = new Point(1010, 29);
            cmbCodeType.Name = "cmbCodeType";
            cmbCodeType.Size = new Size(123, 23);
            cmbCodeType.TabIndex = 13;
            // 
            // btnFilterDate
            // 
            btnFilterDate.BackColor = Color.FromArgb(39, 39, 87);
            btnFilterDate.BackgroundImage = Properties.Resources.filterdate_oldest;
            btnFilterDate.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilterDate.Cursor = Cursors.Hand;
            btnFilterDate.FlatAppearance.BorderSize = 0;
            btnFilterDate.FlatStyle = FlatStyle.Flat;
            btnFilterDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilterDate.ForeColor = Color.White;
            btnFilterDate.Location = new Point(930, 21);
            btnFilterDate.Name = "btnFilterDate";
            btnFilterDate.Size = new Size(35, 35);
            btnFilterDate.TabIndex = 12;
            btnFilterDate.UseVisualStyleBackColor = false;
            btnFilterDate.Click += btnFilterDate_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(693, 26);
            label1.Name = "label1";
            label1.Size = new Size(67, 22);
            label1.TabIndex = 11;
            label1.Text = "Filters";
            // 
            // lblPin
            // 
            lblPin.AutoSize = true;
            lblPin.Font = new Font("Arial Rounded MT Bold", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPin.ForeColor = Color.White;
            lblPin.Location = new Point(463, 26);
            lblPin.Name = "lblPin";
            lblPin.Size = new Size(107, 22);
            lblPin.TabIndex = 10;
            lblPin.Text = "Toggle Pin";
            // 
            // btnPinnedOnly
            // 
            btnPinnedOnly.BackColor = Color.FromArgb(39, 39, 87);
            btnPinnedOnly.BackgroundImage = (Image)resources.GetObject("btnPinnedOnly.BackgroundImage");
            btnPinnedOnly.BackgroundImageLayout = ImageLayout.Zoom;
            btnPinnedOnly.Cursor = Cursors.Hand;
            btnPinnedOnly.FlatAppearance.BorderSize = 0;
            btnPinnedOnly.FlatStyle = FlatStyle.Flat;
            btnPinnedOnly.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPinnedOnly.ForeColor = Color.White;
            btnPinnedOnly.Location = new Point(576, 20);
            btnPinnedOnly.Name = "btnPinnedOnly";
            btnPinnedOnly.Size = new Size(35, 35);
            btnPinnedOnly.TabIndex = 9;
            btnPinnedOnly.UseVisualStyleBackColor = false;
            // 
            // btnFilterPython
            // 
            btnFilterPython.BackColor = Color.FromArgb(39, 39, 87);
            btnFilterPython.BackgroundImage = Properties.Resources.python_icon;
            btnFilterPython.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilterPython.Cursor = Cursors.Hand;
            btnFilterPython.FlatAppearance.BorderSize = 0;
            btnFilterPython.FlatStyle = FlatStyle.Flat;
            btnFilterPython.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilterPython.ForeColor = Color.White;
            btnFilterPython.Location = new Point(889, 21);
            btnFilterPython.Name = "btnFilterPython";
            btnFilterPython.Size = new Size(35, 35);
            btnFilterPython.TabIndex = 8;
            btnFilterPython.UseVisualStyleBackColor = false;
            // 
            // btnFilterCpp
            // 
            btnFilterCpp.BackColor = Color.FromArgb(39, 39, 87);
            btnFilterCpp.BackgroundImage = Properties.Resources.cpp_icon;
            btnFilterCpp.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilterCpp.Cursor = Cursors.Hand;
            btnFilterCpp.FlatAppearance.BorderSize = 0;
            btnFilterCpp.FlatStyle = FlatStyle.Flat;
            btnFilterCpp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilterCpp.ForeColor = Color.White;
            btnFilterCpp.Location = new Point(848, 21);
            btnFilterCpp.Name = "btnFilterCpp";
            btnFilterCpp.Size = new Size(35, 35);
            btnFilterCpp.TabIndex = 6;
            btnFilterCpp.UseVisualStyleBackColor = false;
            // 
            // btnFilterDefault
            // 
            btnFilterDefault.BackColor = Color.FromArgb(39, 39, 87);
            btnFilterDefault.BackgroundImage = (Image)resources.GetObject("btnFilterDefault.BackgroundImage");
            btnFilterDefault.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilterDefault.Cursor = Cursors.Hand;
            btnFilterDefault.FlatAppearance.BorderSize = 0;
            btnFilterDefault.FlatStyle = FlatStyle.Flat;
            btnFilterDefault.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFilterDefault.ForeColor = Color.White;
            btnFilterDefault.Location = new Point(766, 21);
            btnFilterDefault.Name = "btnFilterDefault";
            btnFilterDefault.Size = new Size(35, 35);
            btnFilterDefault.TabIndex = 5;
            btnFilterDefault.TextAlign = ContentAlignment.TopCenter;
            btnFilterDefault.UseVisualStyleBackColor = false;
            // 
            // btnFilterC
            // 
            btnFilterC.BackColor = Color.FromArgb(39, 39, 87);
            btnFilterC.BackgroundImage = Properties.Resources.c_icon;
            btnFilterC.BackgroundImageLayout = ImageLayout.Zoom;
            btnFilterC.Cursor = Cursors.Hand;
            btnFilterC.FlatAppearance.BorderSize = 0;
            btnFilterC.FlatStyle = FlatStyle.Flat;
            btnFilterC.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFilterC.ForeColor = Color.White;
            btnFilterC.Location = new Point(807, 21);
            btnFilterC.Name = "btnFilterC";
            btnFilterC.Size = new Size(35, 35);
            btnFilterC.TabIndex = 7;
            btnFilterC.UseVisualStyleBackColor = false;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.WhiteSmoke;
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Font = new Font("Segoe UI", 12F);
            txtSearch.Location = new Point(31, 26);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(350, 22);
            txtSearch.TabIndex = 1;
            txtSearch.Text = "Search snippets...";
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 39, 87);
            btnSearch.BackgroundImage = (Image)resources.GetObject("btnSearch.BackgroundImage");
            btnSearch.BackgroundImageLayout = ImageLayout.Zoom;
            btnSearch.Cursor = Cursors.Hand;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(387, 26);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(22, 22);
            btnSearch.TabIndex = 2;
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.FromArgb(39, 39, 87);
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Arial Rounded MT Bold", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1214, 56);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "My Code Snippets";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Snips
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(134, 134, 172);
            Controls.Add(flowPanel);
            Controls.Add(topPanel);
            Controls.Add(lblTitle);
            Name = "Snips";
            Size = new Size(1214, 643);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblTitle;
        private Button btnFilterPython;
        private Button btnFilterCpp;
        private Button btnFilterDefault;
        private Button btnFilterC;
        private Button btnPinnedOnly;
        private Label label1;
        private Label lblPin;
        private ComboBox cmbCodeType;
        private Button btnFilterDate;
        private Button btnRefresh;
    }
}