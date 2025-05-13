using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace SnipIt
{
    public partial class Report : UserControl
    {
        Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue
        Color color3 = ColorTranslator.FromHtml("#505081"); // Muted Blue
        Color color4 = ColorTranslator.FromHtml("#0F0E47"); // Deep Navy Blue

        private List<AttachedFile> attachedFiles = new List<AttachedFile>();
        private const long MAX_FILE_SIZE = 52428800; // 50 * 1024 * 1024 (50 mb file size limit)

        public Report()
        {
            InitializeComponent();
            SetupControls();
            SetupFilesPanel();
        }

        private class AttachedFile
        {
            public string FilePath { get; set; }
            public string FileName { get; set; }
            public long FileSize { get; set; }

            public string DisplayName
            {
                get
                {
                    string sizeString = FileSize < 1024 ? $"{FileSize} B" :
                                      FileSize < 1048576 ? $"{FileSize / 1024.0:F1} KB" :
                                      $"{FileSize / 1048576.0:F1} MB";
                    return $"{FileName} ({sizeString})";
                }
            }
        }

        private void SetupFilesPanel()
        {
            pnlFiles.BorderStyle = BorderStyle.FixedSingle;
            pnlFiles.AutoScroll = true;
            pnlFiles.BackColor = Color.White;

            if (!Controls.OfType<Label>().Any(l => l.Name == "lblFilesTitle"))
            {
                Label lblFilesTitle = new Label
                {
                    Name = "lblFilesTitle",
                    Text = "Attached Files:",
                    Font = new Font(Font.FontFamily, 9, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(pnlFiles.Left, pnlFiles.Top - 20)
                };

                this.Controls.Add(lblFilesTitle);
            }

            Label filesLabel = Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblFilesTitle");
            if (filesLabel != null) filesLabel.Visible = false;
            pnlFiles.Visible = false;
        }

        private void SetupControls()
        {
            listPurpose.Items.Clear();
            listPurpose.Items.AddRange(new string[] {
                "Report",
                "Feedback",
                "Bug",
                "Suggestion",
                "Feature Request",
                "OTHER"
            });
            listPurpose.SelectedIndex = 0; // Default to "REPORT"

            listReason.Items.Clear();
            listReason.Items.AddRange(new string[] {
                "General Issue",
                "Performance Problem",
                "User Interface",
                "Login/Account Issues",
                "Connectivity Issues",
                "Data Loss/Corruption",
                "Security Concern",
                "Application Crash",
                "Missing Feature",
                "Confusing Feature",
                "Other"
            });
            listReason.SelectedIndex = 0; // Default to "General Issue"

            btnSubmit.Click += BtnSubmit_Click;
            btnAttach.Click += BtnAttach_Click;

            rtbDesc.Clear();
        }

        private void UpdateFilesPanel()
        {
            pnlFiles.Controls.Clear();

            bool hasFiles = attachedFiles.Count > 0;
            pnlFiles.Visible = hasFiles;

            Label filesLabel = Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lblFilesTitle");
            if (filesLabel != null) filesLabel.Visible = hasFiles;

            if (!hasFiles) return;

            int yPos = 5;
            foreach (var file in attachedFiles)
            {
                Label lblFile = new Label
                {
                    Text = TruncateFileName(file.DisplayName, 12),
                    ForeColor = color3, 
                    AutoSize = true,
                    Location = new Point(5, yPos + 2),
                    Tag = file,  
                };


                Button btnRemove = new Button
                {
                    Text = "X",
                    Size = new Size(24, 24),
                    BackColor = color1,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(pnlFiles.Width - 35, yPos),
                    Tag = file  
                };
                btnRemove.FlatAppearance.BorderSize = 0;

                btnRemove.Click += (sender, e) =>
                {
                    var fileToRemove = (AttachedFile)((Button)sender).Tag;
                    attachedFiles.Remove(fileToRemove);
                    UpdateFilesPanel();
                };

                pnlFiles.Controls.Add(lblFile);
                pnlFiles.Controls.Add(btnRemove);

                yPos += 30;
            }
        }

        private void Report_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void BtnAttach_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Multiselect = true;
                    openFileDialog.Filter = "All Files (*.*)|*.*";
                    openFileDialog.Title = "Select Files to Attach";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        int filesAdded = 0;
                        long totalSize = GetCurrentAttachedFilesSize();

                        foreach (string fileName in openFileDialog.FileNames)
                        {
                            FileInfo fileInfo = new FileInfo(fileName);

                            if (!fileInfo.Exists)
                                continue;

                            if (attachedFiles.Any(f => f.FilePath == fileName))
                                continue;

                            // Check if adding this file would exceed the total size limit
                            if (totalSize + fileInfo.Length > MAX_FILE_SIZE)
                            {
                                MessageBox.Show($"Cannot attach {fileInfo.Name} as it would exceed the 50MB total limit.",
                                    "File Too Large", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                continue;
                            }

                            attachedFiles.Add(new AttachedFile
                            {
                                FilePath = fileName,
                                FileName = fileInfo.Name,
                                FileSize = fileInfo.Length
                            });

                            totalSize += fileInfo.Length;
                            filesAdded++;
                        }

                        // Update the attached files panel
                        UpdateFilesPanel();

                        // Show confirmation with the number of attached files
                        if (filesAdded > 0)
                        {
                            MessageBox.Show($"{filesAdded} file(s) attached successfully.",
                                "Files Attached", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error attaching files: {ex.Message}",
                    "Attachment Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long GetCurrentAttachedFilesSize()
        {
            return attachedFiles.Sum(f => f.FileSize);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Validate the form
            if (string.IsNullOrWhiteSpace(rtbDesc.Text))
            {
                MessageBox.Show("Please provide a description for your report/feedback.",
                    "Description Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string purpose = listPurpose.SelectedItem?.ToString() ?? "REPORT";
            string reason = listReason.SelectedItem?.ToString() ?? "General Issue";
            string description = rtbDesc.Text;
            string email = tbxEmail.Text.Trim();

            try
            {
                Cursor = Cursors.WaitCursor;
                SendReport(purpose, reason, description, email);

                rtbDesc.Clear();
                tbxEmail.Clear();
                attachedFiles.Clear();
                UpdateFilesPanel();

                // Reset cursor
                Cursor = Cursors.Default;

                MessageBox.Show("Your report/feedback has been submitted successfully!",
                    "Submission Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show($"Error submitting report/feedback: {ex.Message}",
                    "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatFileSize(long bytes)
        {
            return bytes < 1024 ? $"{bytes} B" :
                   bytes < 1048576 ? $"{bytes / 1024.0:F1} KB" :
                   $"{bytes / 1048576.0:F1} MB";
        }

        private void SendReport(string purpose, string reason, string description, string userEmail)
        {
            string senderEmail = "codesnipit@gmail.com";
            string senderPassword = "icgi mvtn ijkb adfw"; // App password

            try
            {
                string subject = $"PURPOSE: {purpose} | REASON: {reason}";
                StringBuilder body = new StringBuilder();

                body.AppendLine("=============================================================");
                body.AppendLine($"                SNIPIT REPORT: {purpose}");
                body.AppendLine("=============================================================");
                body.AppendLine();
                body.AppendLine($"PURPOSE: {purpose}");
                body.AppendLine($"REASON: {reason}");
                body.AppendLine($"SUBMITTED: {DateTime.Now}");

                if (!string.IsNullOrEmpty(userEmail))
                {
                    body.AppendLine($"FROM: {userEmail}");
                }

                body.AppendLine();
                body.AppendLine("-------------------------------------------------------------");
                body.AppendLine("DESCRIPTION:");
                body.AppendLine(description);
                body.AppendLine();

                body.AppendLine("=============================================================");
                body.AppendLine("  This report was sent automatically by the SnipIt application");
                body.AppendLine("=============================================================");

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(senderEmail);
                    mail.To.Add(senderEmail); // send to itself (codesnipit@gmail.com)
                    mail.Subject = subject;
                    mail.Body = body.ToString();
                    mail.IsBodyHtml = false;

                    foreach (var file in attachedFiles)
                    {
                        if (File.Exists(file.FilePath))
                        {
                            mail.Attachments.Add(new Attachment(file.FilePath));
                        }
                    }

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                    {
                        smtp.Port = 587;
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = true;

                        // Send the email
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }

        private string TruncateFileName(string fileName, int maxLength)
        {
            if (fileName.Length <= maxLength)
                return fileName;

            int sizeStartPos = fileName.LastIndexOf('(');
            if (sizeStartPos <= 0)
                return fileName.Substring(0, maxLength - 3) + "...";

            string sizeInfo = fileName.Substring(sizeStartPos);

            int nameLength = maxLength - sizeInfo.Length - 3;
            if (nameLength < 5) nameLength = 5;

            return fileName.Substring(0, nameLength) + "..." + sizeInfo;
        }
        private void listPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listReason_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rtbDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
  
        }
    }
}