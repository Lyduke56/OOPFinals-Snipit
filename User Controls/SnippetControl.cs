using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SnipIt.Managers;
using SnipIt.Models;

namespace SnipIt.User_Controls
{
    // snippet model tho might just use the ones in my snips.cs for the final one for easier access? will keep for future reference
    public partial class SnippetControl : UserControl
    {
        private Snippet _snippet;


        public SnippetControl()
        {
            InitializeComponent();

        }

        public SnippetControl(Snippet snippet) : this()
        {
            _snippet = snippet;
            SetupSnippetData();
            
        }

        private void SetupSnippetData()
        {
            if (_snippet == null) return;
            lblSnippetTitle.Text = _snippet.Name;
            string language = _snippet.Language ?? "c";
            languageIndicator.BackColor = GetLanguageColor(language);

            try
            {
                Image languageIcon = SetLanguageIcon(language);

                if (languageIcon != null)
                {
                    languageIndicator.BackgroundImage = languageIcon;
                    languageIndicator.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting language icon: {ex.Message}");
            }
            try
            {
                btnEdit.Image = Properties.Resources.edit_icon;
                btnEdit.SizeMode = PictureBoxSizeMode.StretchImage;
                btnEdit.BackColor = Color.MediumSeaGreen;

                btnPin.Image = Properties.Resources.pin_icon;
                btnPin.SizeMode = PictureBoxSizeMode.StretchImage;
                btnPin.BackColor = _snippet.IsPinned ? Color.Gold : Color.LightGray;

                btnDelete.Image = Properties.Resources.trash_icon;
                btnDelete.SizeMode = PictureBoxSizeMode.StretchImage;
                btnDelete.BackColor = Color.Tomato;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting button icons: {ex.Message}");

                btnEdit.BackColor = Color.MediumSeaGreen;
                btnPin.BackColor = _snippet.IsPinned ? Color.Gold : Color.LightGray;
                btnDelete.BackColor = Color.Tomato;
            }

            if (!string.IsNullOrEmpty(_snippet.CodeType))
            {
                Label existingLabel = Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == "CodeTypeLabel");

                if (existingLabel != null)
                {
                    existingLabel.Text = _snippet.CodeType;
                }
                else
                {
                    Label lblCodeType = new Label
                    {
                        Text = _snippet.CodeType,
                        Font = new Font("Segoe UI", 8, FontStyle.Italic),
                        ForeColor = Color.LightGray,
                        AutoSize = true,
                        Location = new Point(languageIndicator.Right + 5, languageIndicator.Top),
                        Tag = "CodeTypeLabel"
                    };

                    this.Controls.Add(lblCodeType);
                    lblCodeType.BringToFront();
                }
            }

            codePreviewBox.Text = GetSnippetPreview(_snippet.Content, 500);
            ApplyBasicSyntaxHighlighting(codePreviewBox, language);

            lblDateCreated.Text = _snippet.LastModified.ToString("MM/dd/yyyy");
        }


        private Image SetLanguageIcon(string language)
        {
            try
            {
                language = language?.ToLower().Trim();

                string resourceName;
                switch (language)
                {
                    case "c":
                        resourceName = "c_icon";
                        break;
                    case "cpp":
                        resourceName = "cpp_icon";
                        break;
                    case "python":
                        resourceName = "python_icon";
                        break;
                    default:
                        resourceName = "c_icon";
                        break;
                }

                var resource = Properties.Resources.ResourceManager.GetObject(resourceName);

                if (resource == null)
                {
                    resource = Properties.Resources.ResourceManager.GetObject($"{language}_icon");
                }

                Image icon = resource as Image ?? Properties.Resources.c_icon;
                return icon;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting language icon: {ex.Message}");
                return Properties.Resources.c_icon;
            }
        }


        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            // Create rounded corners for the main panel
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded rectangle
            int radius = 10;
            Rectangle rect = new Rectangle(0, 0, this.mainPanel.Width - 1, this.mainPanel.Height - 1);
            GraphicsPath path = new GraphicsPath();

            // Top left corner
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);

            // Top right corner
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);

            // Bottom right corner
            path.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);

            // Bottom left corner
            path.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);

            path.CloseAllFigures();

            // Set the region for the rounded shape
            mainPanel.Region = new Region(path);

            // Draw border if needed
            using (Pen pen = new Pen(Color.FromArgb(50, 50, 50), 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private string GetSnippetPreview(string content, int maxLength)
        {
            if (string.IsNullOrEmpty(content)) return "";

            if (content.Length > maxLength)
            {
                return content.Substring(0, maxLength) + "...";
            }

            return content;
        }

        private Color GetLanguageColor(string language)
        {
            switch (language?.ToLower())
            {
                case "c":
                    return Color.DodgerBlue;
                case "cpp":
                    return Color.RoyalBlue;
                case "python":
                    return Color.MediumPurple;
                default:
                    return Color.SlateGray;
            }
        }

        private void ApplyBasicSyntaxHighlighting(RichTextBox rtb, string language)
        {
            if (string.IsNullOrEmpty(rtb.Text)) return;

            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            string[] keywords = null;
            string[] types = null;

            switch (language?.ToLower())
            {
                case "c":
                case "cpp":
                    keywords = new[] { "if", "else", "for", "while", "return", "break", "continue", "switch", "case" };
                    types = new[] { "int", "float", "double", "char", "void", "bool", "struct", "class" };
                    break;
                case "python":
                    keywords = new[] { "if", "else", "for", "while", "return", "import", "from", "def", "class", "try", "except" };
                    types = new[] { "int", "float", "str", "list", "dict", "tuple", "set", "bool" };
                    break;
            }

            if (keywords == null) return;

            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.TextLength;
            rtb.SelectionColor = Color.FromArgb(220, 220, 220);

            HighlightPattern(rtb, "\".*?\"", Color.FromArgb(152, 195, 121));

            foreach (string keyword in keywords)
            {
                HighlightWholeWord(rtb, keyword, Color.FromArgb(198, 120, 221));
            }

            foreach (string type in types)
            {
                HighlightWholeWord(rtb, type, Color.FromArgb(224, 108, 117));
            }

            if (language?.ToLower() == "python")
            {
                HighlightPattern(rtb, "#.*$", Color.FromArgb(92, 99, 112), RegexOptions.Multiline);
            }
            else
            {
                HighlightPattern(rtb, "//.*$", Color.FromArgb(92, 99, 112), RegexOptions.Multiline);
            }

            rtb.SelectionStart = selectionStart;
            rtb.SelectionLength = selectionLength;
        }

        private void HighlightPattern(RichTextBox rtb, string pattern, Color color, RegexOptions options = RegexOptions.None)
        {
            try
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, options);
                foreach (System.Text.RegularExpressions.Match match in regex.Matches(rtb.Text))
                {
                    rtb.SelectionStart = match.Index;
                    rtb.SelectionLength = match.Length;
                    rtb.SelectionColor = color;
                }
            }
            catch
            {

            }
        }

        private void HighlightWholeWord(RichTextBox rtb, string word, Color color)
        {
            try
            {
                string pattern = $"\\b{word}\\b";
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                foreach (System.Text.RegularExpressions.Match match in regex.Matches(rtb.Text))
                {
                    rtb.SelectionStart = match.Index;
                    rtb.SelectionLength = match.Length;
                    rtb.SelectionColor = color;
                }
            }
            catch
            {

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_snippet == null) return;

            OpenSnippet();
        }

        private void btnPin_Click(object sender, EventArgs e)
        {
            if (_snippet == null) return;

            _snippet.IsPinned = !_snippet.IsPinned;

            btnPin.BackColor = _snippet.IsPinned ? Color.Gold : Color.LightGray;

            SnippetManager.SaveSnippet(_snippet);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_snippet == null) return;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{_snippet.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SnippetManager.DeleteSnippet(_snippet.Id);

                OnSnippetDeleted();
            }
        }

        private void OpenSnippet()
        {
            try
            {
                CodePlayground playground = new CodePlayground();
                playground.LoadSnippet(_snippet);
                playground.Show();

                if (this.ParentForm != null)
                {
                    this.ParentForm.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening snippet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public event EventHandler SnippetDeleted;

        protected virtual void OnSnippetDeleted()
        {
            SnippetDeleted?.Invoke(this, EventArgs.Empty);
        }

        private void languageIndicator_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblSnippetTitle_Click(object sender, EventArgs e)
        {

        }

        internal Snips Snips
        {
            get => default;
            set
            {
            }
        }
    }
}