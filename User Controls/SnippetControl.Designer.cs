namespace SnipIt.User_Controls
{
    partial class SnippetControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnippetControl));
            mainPanel = new Panel();
            languageIndicator = new Panel();
            lblDateCreated = new Label();
            btnDelete = new PictureBox();
            lblSnippetTitle = new Label();
            codePreviewBox = new RichTextBox();
            footerPanel = new Panel();
            btnPin = new PictureBox();
            btnEdit = new PictureBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnDelete).BeginInit();
            footerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)btnPin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnEdit).BeginInit();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(40, 44, 52);
            mainPanel.Controls.Add(languageIndicator);
            mainPanel.Controls.Add(lblDateCreated);
            mainPanel.Controls.Add(lblSnippetTitle);
            mainPanel.Controls.Add(codePreviewBox);
            mainPanel.Controls.Add(footerPanel);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(350, 250);
            mainPanel.TabIndex = 0;
            mainPanel.Paint += mainPanel_Paint;
            // 
            // languageIndicator
            // 
            languageIndicator.BackColor = Color.DodgerBlue;
            languageIndicator.Location = new Point(15, 15);
            languageIndicator.Name = "languageIndicator";
            languageIndicator.Size = new Size(20, 20);
            languageIndicator.TabIndex = 4;
            languageIndicator.Paint += languageIndicator_Paint;
            // 
            // lblDateCreated
            // 
            lblDateCreated.AutoSize = true;
            lblDateCreated.ForeColor = Color.Gray;
            lblDateCreated.Location = new Point(241, 225);
            lblDateCreated.Name = "lblDateCreated";
            lblDateCreated.Size = new Size(65, 15);
            lblDateCreated.TabIndex = 3;
            lblDateCreated.Text = "04/01/2025";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Tomato;
            btnDelete.BackgroundImage = (Image)resources.GetObject("btnDelete.BackgroundImage");
            btnDelete.BackgroundImageLayout = ImageLayout.Stretch;
            btnDelete.BorderStyle = BorderStyle.FixedSingle;
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.Image = (Image)resources.GetObject("btnDelete.Image");
            btnDelete.Location = new Point(312, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(24, 24);
            btnDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            btnDelete.TabIndex = 2;
            btnDelete.TabStop = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblSnippetTitle
            // 
            lblSnippetTitle.AutoSize = true;
            lblSnippetTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSnippetTitle.ForeColor = Color.White;
            lblSnippetTitle.Location = new Point(41, 13);
            lblSnippetTitle.Name = "lblSnippetTitle";
            lblSnippetTitle.Size = new Size(116, 21);
            lblSnippetTitle.TabIndex = 2;
            lblSnippetTitle.Text = "Hello World C";
            lblSnippetTitle.Click += lblSnippetTitle_Click;
            // 
            // codePreviewBox
            // 
            codePreviewBox.BackColor = Color.FromArgb(40, 44, 52);
            codePreviewBox.BorderStyle = BorderStyle.None;
            codePreviewBox.Font = new Font("Consolas", 9F);
            codePreviewBox.ForeColor = Color.FromArgb(220, 220, 220);
            codePreviewBox.Location = new Point(15, 50);
            codePreviewBox.Name = "codePreviewBox";
            codePreviewBox.ReadOnly = true;
            codePreviewBox.Size = new Size(320, 160);
            codePreviewBox.TabIndex = 1;
            codePreviewBox.Text = "// C Compiler\n#include <stdio.h>\n\nint main() {\n// Write your code here\n    printf(\"Hello, World!\\n\");\n    return 0;\n}";
            // 
            // footerPanel
            // 
            footerPanel.BackColor = Color.FromArgb(30, 34, 42);
            footerPanel.Controls.Add(btnPin);
            footerPanel.Controls.Add(btnEdit);
            footerPanel.Controls.Add(btnDelete);
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Location = new Point(0, 215);
            footerPanel.Name = "footerPanel";
            footerPanel.Size = new Size(350, 35);
            footerPanel.TabIndex = 0;
            // 
            // btnPin
            // 
            btnPin.BackColor = Color.Gray;
            btnPin.BackgroundImage = (Image)resources.GetObject("btnPin.BackgroundImage");
            btnPin.BackgroundImageLayout = ImageLayout.Stretch;
            btnPin.BorderStyle = BorderStyle.FixedSingle;
            btnPin.Cursor = Cursors.Hand;
            btnPin.Image = (Image)resources.GetObject("btnPin.Image");
            btnPin.Location = new Point(41, 6);
            btnPin.Name = "btnPin";
            btnPin.Size = new Size(24, 24);
            btnPin.SizeMode = PictureBoxSizeMode.StretchImage;
            btnPin.TabIndex = 1;
            btnPin.TabStop = false;
            btnPin.Click += btnPin_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.MediumSeaGreen;
            btnEdit.BackgroundImage = (Image)resources.GetObject("btnEdit.BackgroundImage");
            btnEdit.BackgroundImageLayout = ImageLayout.Stretch;
            btnEdit.BorderStyle = BorderStyle.FixedSingle;
            btnEdit.Cursor = Cursors.Hand;
            btnEdit.Image = (Image)resources.GetObject("btnEdit.Image");
            btnEdit.Location = new Point(11, 6);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(24, 24);
            btnEdit.SizeMode = PictureBoxSizeMode.StretchImage;
            btnEdit.TabIndex = 0;
            btnEdit.TabStop = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // SnippetControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mainPanel);
            Name = "SnippetControl";
            Size = new Size(350, 250);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)btnDelete).EndInit();
            footerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)btnPin).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnEdit).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.RichTextBox codePreviewBox;
        private System.Windows.Forms.Label lblSnippetTitle;
        private System.Windows.Forms.Label lblDateCreated;
        private System.Windows.Forms.Panel languageIndicator;
        private System.Windows.Forms.PictureBox btnEdit;
        private System.Windows.Forms.PictureBox btnPin;
        private System.Windows.Forms.PictureBox btnDelete;
    }
}