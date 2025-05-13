namespace SnipIt
{
    partial class Report
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
            pnlReport = new Panel();
            pnlFiles = new Panel();
            listReason = new ComboBox();
            listPurpose = new ComboBox();
            btnAttach = new Button();
            tbxEmail = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnSubmit = new Button();
            rtbDesc = new RichTextBox();
            pnlReport.SuspendLayout();
            SuspendLayout();
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
            lblTitle.TabIndex = 3;
            lblTitle.Text = "Reports and Feedbacks";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlReport
            // 
            pnlReport.BackColor = Color.FromArgb(134, 134, 172);
            pnlReport.Controls.Add(pnlFiles);
            pnlReport.Controls.Add(listReason);
            pnlReport.Controls.Add(listPurpose);
            pnlReport.Controls.Add(btnAttach);
            pnlReport.Controls.Add(tbxEmail);
            pnlReport.Controls.Add(label4);
            pnlReport.Controls.Add(label3);
            pnlReport.Controls.Add(label2);
            pnlReport.Controls.Add(label1);
            pnlReport.Controls.Add(btnSubmit);
            pnlReport.Controls.Add(rtbDesc);
            pnlReport.Dock = DockStyle.Fill;
            pnlReport.Location = new Point(0, 56);
            pnlReport.Name = "pnlReport";
            pnlReport.Size = new Size(1214, 587);
            pnlReport.TabIndex = 4;
            pnlReport.Paint += panel1_Paint;
            // 
            // pnlFiles
            // 
            pnlFiles.Location = new Point(1009, 139);
            pnlFiles.Name = "pnlFiles";
            pnlFiles.Size = new Size(173, 386);
            pnlFiles.TabIndex = 18;
            // 
            // listReason
            // 
            listReason.DropDownStyle = ComboBoxStyle.DropDownList;
            listReason.FormattingEnabled = true;
            listReason.Location = new Point(469, 30);
            listReason.Name = "listReason";
            listReason.Size = new Size(534, 23);
            listReason.TabIndex = 17;
            listReason.SelectedIndexChanged += listReason_SelectedIndexChanged;
            // 
            // listPurpose
            // 
            listPurpose.DropDownStyle = ComboBoxStyle.DropDownList;
            listPurpose.FormattingEnabled = true;
            listPurpose.Location = new Point(139, 30);
            listPurpose.Name = "listPurpose";
            listPurpose.Size = new Size(232, 23);
            listPurpose.TabIndex = 16;
            listPurpose.SelectedIndexChanged += listPurpose_SelectedIndexChanged;
            // 
            // btnAttach
            // 
            btnAttach.Location = new Point(1009, 97);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new Size(173, 36);
            btnAttach.TabIndex = 12;
            btnAttach.Text = "ATTACH FILES";
            btnAttach.UseVisualStyleBackColor = true;
            // 
            // tbxEmail
            // 
            tbxEmail.Font = new Font("Segoe UI", 13F);
            tbxEmail.Location = new Point(307, 537);
            tbxEmail.Name = "tbxEmail";
            tbxEmail.Size = new Size(288, 31);
            tbxEmail.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Rounded MT Bold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(37, 542);
            label4.Name = "label4";
            label4.Size = new Size(264, 20);
            label4.TabIndex = 10;
            label4.Text = "CONTACT EMAIL (OPTIONAL) :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Rounded MT Bold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(48, 72);
            label3.Name = "label3";
            label3.Size = new Size(132, 20);
            label3.TabIndex = 8;
            label3.Text = "DESCRIPTION:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(37, 33);
            label2.Name = "label2";
            label2.Size = new Size(96, 20);
            label2.TabIndex = 6;
            label2.Text = "PURPOSE:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 12.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(377, 33);
            label1.Name = "label1";
            label1.Size = new Size(86, 20);
            label1.TabIndex = 5;
            label1.Text = "REASON:";
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(827, 535);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(176, 36);
            btnSubmit.TabIndex = 3;
            btnSubmit.Text = "SUBMIT REPORT/FEEDBACK";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click_1;
            // 
            // rtbDesc
            // 
            rtbDesc.Font = new Font("Segoe UI", 11F);
            rtbDesc.Location = new Point(37, 97);
            rtbDesc.Name = "rtbDesc";
            rtbDesc.Size = new Size(966, 428);
            rtbDesc.TabIndex = 0;
            rtbDesc.Text = "";
            rtbDesc.TextChanged += rtbDesc_TextChanged;
            // 
            // Report
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlReport);
            Controls.Add(lblTitle);
            Name = "Report";
            Size = new Size(1214, 643);
            Load += Report_Load;
            pnlReport.ResumeLayout(false);
            pnlReport.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Panel pnlReport;
        private Button btnSubmit;
        private RichTextBox rtbDesc;
        private Label label1;
        private Label label2;
        private Label label4;
        private Label label3;
        private TextBox tbxEmail;
        private Button btnAttach;
        private ComboBox listReason;
        private ComboBox listPurpose;
        private Panel pnlFiles;
    }
}
