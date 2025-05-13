using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SnipIt.Managers;
using SnipIt.Models;

namespace SnipIt.User_Controls
{
    public partial class Snips : UserControl
    {
        // Colors
        private Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        private Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue
        private Color color3 = ColorTranslator.FromHtml("#505081"); // Muted Blue
        private Color color4 = ColorTranslator.FromHtml("#0F0E47"); // Deep Navy Blue
        private Color accentColor = ColorTranslator.FromHtml("#FF6B6B"); // Accent color for pins

        private List<Snippet> allSnippets = new List<Snippet>();
        private string currentSearchTerm = "";
        private string currentLanguageFilter = "All";
        private string currentCodeTypeFilter = "All";
        private bool showPinnedOnly = false;
        private bool sortByNewest = true; 

        private Button currentActiveFilterButton;
        private Dictionary<string, List<string>> languageCodeTypes = new Dictionary<string, List<string>>();

        public Snips()
        {
            InitializeComponent();
            InitializeCodeTypeMapping();
            SetupControlBehaviors();
            LoadSnippets();
        }

        private void InitializeCodeTypeMapping()
        {
            // Set up the code type mappings for each language
            languageCodeTypes["All"] = new List<string> { "All", "Program", "Function", "Class", "Header", "Utility", "Library", "Script", "Module" };
            languageCodeTypes["c"] = new List<string> { "All", "Program", "Function", "Header", "Utility", "Library" };
            languageCodeTypes["cpp"] = new List<string> { "All", "Program", "Function", "Class", "Header", "Utility" };
            languageCodeTypes["python"] = new List<string> { "All", "Script", "Function", "Class", "Module", "Utility" };
        }

        private void SetupControlBehaviors()
        {
            SetActiveFilterButton(btnFilterDefault);
            UpdatePinnedButtonAppearance();
            SetupSearchBoxPlaceholder();

            // hardcoded just to ensure; was messy prior to this one
            btnSearch.Click += (sender, e) => ApplyFilters();


            btnFilterDefault.Click += (sender, e) => FilterButtonClicked(btnFilterDefault, "All");
            btnFilterC.Click += (sender, e) => FilterButtonClicked(btnFilterC, "c");
            btnFilterCpp.Click += (sender, e) => FilterButtonClicked(btnFilterCpp, "cpp");
            btnFilterPython.Click += (sender, e) => FilterButtonClicked(btnFilterPython, "python");

            btnPinnedOnly.Click += (sender, e) => TogglePinnedOnly();

            // date filter
            btnFilterDate.FlatStyle = FlatStyle.Flat;
            btnFilterDate.FlatAppearance.BorderSize = 0;
            btnFilterDate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnFilterDate.ImageAlign = ContentAlignment.MiddleLeft;
            btnFilterDate.TextAlign = ContentAlignment.MiddleRight;
            btnFilterDate.Text = "";
            btnFilterDate.Padding = new Padding(5);

            try
            {
                btnFilterDate.Image = sortByNewest ?
                    Properties.Resources.filterdate_recent :
                    Properties.Resources.filterdate_oldest;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing button image: {ex.Message}");
            }

            btnFilterDate.Text = sortByNewest ? "" : "";
            btnFilterDate.Click -= btnFilterDate_Click; 
            btnFilterDate.Click += btnFilterDate_Click;

            cmbCodeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCodeType.BackColor = Color.White;
            cmbCodeType.ForeColor = Color.Black;
            cmbCodeType.FlatStyle = FlatStyle.Flat;

            PopulateCodeTypeComboBox("All");
            cmbCodeType.SelectedIndex = 0;

            cmbCodeType.SelectedIndexChanged += (sender, e) =>
            {
                if (cmbCodeType.SelectedItem != null)
                {
                    currentCodeTypeFilter = cmbCodeType.SelectedItem.ToString();
                    ApplyFilters();
                }
            };
        }

        private void PopulateCodeTypeComboBox(string language)
        {
            cmbCodeType.Items.Clear();

            if (languageCodeTypes.ContainsKey(language))
            {
                foreach (var codeType in languageCodeTypes[language])
                {
                    cmbCodeType.Items.Add(codeType);
                }
            }
            else
            {
                // Fallback to all code types if language not found
                foreach (var codeType in languageCodeTypes["All"])
                {
                    cmbCodeType.Items.Add(codeType);
                }
            }

            // Set the first item as selected
            if (cmbCodeType.Items.Count > 0)
            {
                cmbCodeType.SelectedIndex = 0;
                currentCodeTypeFilter = "All";
            }
        }

        private void ToggleDateSorting()
        {
            sortByNewest = !sortByNewest;

            try
            {
                btnFilterDate.BackgroundImage = sortByNewest ?
                    Properties.Resources.filterdate_recent :
                    Properties.Resources.filterdate_oldest;

                btnFilterDate.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting date filter button image: {ex.Message}");
            }

            btnFilterDate.Invalidate();
            ApplyFilters();
        }

        private void UpdateDateFilterButton()
        {
            // a bit of jank here regarding the image, will try to find fixes in the future
            try
            {
                btnFilterDate.Image = sortByNewest
                    ? Properties.Resources.filterdate_recent
                    : Properties.Resources.filterdate_oldest;

                btnFilterDate.Text = sortByNewest ? "" : "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating date filter button: {ex.Message}");
                btnFilterDate.Text = sortByNewest ? "" : "";
            }
        }

        private void FilterButtonClicked(Button clickedButton, string filterValue)
        {
            if (clickedButton == currentActiveFilterButton)
                return;

            SetActiveFilterButton(clickedButton);
            currentLanguageFilter = filterValue;
            PopulateCodeTypeComboBox(filterValue);

            ApplyFilters();
        }

        private void SetActiveFilterButton(Button button)
        {
            if (currentActiveFilterButton != null)
            {
                currentActiveFilterButton.BackColor = color1;
                currentActiveFilterButton.ForeColor = Color.White;
                currentActiveFilterButton.FlatAppearance.BorderColor = Color.DarkGray;
            }

            button.BackColor = GetHighlightColorForButton(button);
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderColor = accentColor;

            currentActiveFilterButton = button;
        }

        private Color GetHighlightColorForButton(Button button)
        {
            // custom highlight colors for each button so they look nicer
            if (button == btnFilterDefault)
                return Color.FromArgb(50, 54, 62); 
            else if (button == btnFilterC)
                return Color.FromArgb(30, 90, 150); 
            else if (button == btnFilterCpp)
                return Color.FromArgb(40, 80, 140); 
            else if (button == btnFilterPython)
                return Color.FromArgb(50, 50, 100); 
            else if (button == btnPinnedOnly && showPinnedOnly)
                return Color.FromArgb(180, 130, 70);

            return Color.FromArgb(60, 64, 72);
        }

        private void TogglePinnedOnly()
        {
            // pinned only state
            showPinnedOnly = !showPinnedOnly;

            UpdatePinnedButtonAppearance();
            ApplyFilters();
        }

        private void UpdatePinnedButtonAppearance()
        {
            if (showPinnedOnly)
            {
                btnPinnedOnly.BackColor = Color.FromArgb(180, 130, 70);
                btnPinnedOnly.ForeColor = Color.White;
                btnPinnedOnly.FlatAppearance.BorderColor = Color.Gold;
            }
            else
            {
                btnPinnedOnly.BackColor = Color.FromArgb(40, 44, 52); // Default dark background
                btnPinnedOnly.ForeColor = Color.White;
                btnPinnedOnly.FlatAppearance.BorderColor = Color.DarkGray;
            }
        }

        private void SetupSearchBoxPlaceholder()
        {
            txtSearch.Text = "Search snippets...";
            txtSearch.ForeColor = Color.Gray;


            txtSearch.GotFocus += (sender, e) =>
            {
                if (txtSearch.Text == "Search snippets...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };

            txtSearch.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search snippets...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };

            txtSearch.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    currentSearchTerm = txtSearch.Text == "Search snippets..." ? "" : txtSearch.Text;
                    ApplyFilters();
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void LoadSnippets()
        {
            string userId = timer.SessionManager.UserId.ToString();
            allSnippets = SnippetManager.LoadUserSnippets(userId);

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            flowPanel.Controls.Clear();
            currentSearchTerm = txtSearch.Text == "Search snippets..." ? "" : txtSearch.Text.ToLower();
            var filteredSnippets = allSnippets;

            if (currentLanguageFilter != "All")
            {
                string normalizedFilter = NormalizeLanguageName(currentLanguageFilter);
                filteredSnippets = filteredSnippets.Where(s =>
                    NormalizeLanguageName(s.Language) == normalizedFilter).ToList();
            }

            if (currentCodeTypeFilter != "All")
            {
                filteredSnippets = filteredSnippets.Where(s =>
                    s.CodeType == currentCodeTypeFilter).ToList();
            }

            if (!string.IsNullOrWhiteSpace(currentSearchTerm))
            {
                filteredSnippets = filteredSnippets.Where(s =>
                    s.Name.ToLower().Contains(currentSearchTerm) ||
                    s.Content.ToLower().Contains(currentSearchTerm)).ToList();
            }

            if (showPinnedOnly)
            {
                filteredSnippets = filteredSnippets.Where(s =>
                    s.IsPinned).ToList();
            }

            // date
            if (sortByNewest)
            {

                filteredSnippets = filteredSnippets
                    .OrderByDescending(s => s.LastModified)
                    .ToList();
            }
            else
            {
                // Oldest first - ascending order by LastModified date
                filteredSnippets = filteredSnippets
                    .OrderBy(s => s.LastModified)
                    .ToList();
            }

            // always show pinned
            if (!showPinnedOnly)
            {
                var pinnedSnippets = filteredSnippets.Where(s => s.IsPinned).ToList();
                var unpinnedSnippets = filteredSnippets.Where(s => !s.IsPinned).ToList();

                if (sortByNewest)
                {
                    pinnedSnippets = pinnedSnippets.OrderByDescending(s => s.LastModified).ToList();
                }
                else
                {
                    pinnedSnippets = pinnedSnippets.OrderBy(s => s.LastModified).ToList();
                }

                filteredSnippets = pinnedSnippets.Concat(unpinnedSnippets).ToList();
            }

            if (filteredSnippets.Count == 0)
            {
                Label noSnippetsLabel = new Label
                {
                    Text = "No snippets found with the current filters",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 14),
                    ForeColor = Color.White,
                    Margin = new Padding(10)
                };
                flowPanel.Controls.Add(noSnippetsLabel);
            }
            else
            {
                foreach (var snippet in filteredSnippets)
                {
                    AddSnippetCard(snippet);
                }
            }
        }

        private void AddSnippetCard(Snippet snippet)
        {
            Panel snippetPanel = new RoundedPanel
            {
                Size = new Size(350, 250),
                Margin = new Padding(15),
                BackColor = Color.FromArgb(40, 44, 52)
            };

            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(30, 34, 42)
            };

            PictureBox pbLanguage = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(10, 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = GetLanguageColor(snippet.Language),
                BorderStyle = BorderStyle.FixedSingle
            };

            try
            {
                Image languageIcon = SetLanguageIcon(snippet.Language);
                if (languageIcon != null)
                {
                    pbLanguage.Image = languageIcon;
                    pbLanguage.BackColor = Color.Transparent;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting language icon: {ex.Message}");
            }

            Label lblLanguage = new Label
            {
                Text = GetLanguageDisplayName(snippet.Language),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 220, 220),
                AutoSize = true,
                Location = new Point(40, 12)
            };

            Label lblCodeType = new Label
            {
                Text = snippet.CodeType ?? "Unknown",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(180, 180, 180),
                AutoSize = true,
                Location = new Point(lblLanguage.Right + 15, 13)
            };

            Label lblTitle = new Label
            {
                Text = snippet.Name,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 50),
                MaximumSize = new Size(330, 0)
            };

            RichTextBox txtPreview = new RichTextBox
            {
                Text = GetSnippetPreview(snippet.Content, 100),
                Font = new Font("Consolas", 10),
                BackColor = Color.FromArgb(40, 44, 52),
                ForeColor = Color.FromArgb(220, 220, 220),
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                Location = new Point(10, 80),
                Size = new Size(330, 120),
                DetectUrls = false
            };

            ApplyBasicSyntaxHighlighting(txtPreview, snippet.Language);

            Panel actionsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.FromArgb(30, 34, 42)
            };

            PictureBox pbOpen = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(10, 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.MediumSeaGreen,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            PictureBox pbPin = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(44, 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = snippet.IsPinned ? Color.Gold : Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            PictureBox pbDelete = new PictureBox
            {
                Size = new Size(24, 24),
                Location = new Point(316, 8),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Tomato,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand
            };

            try
            {
                // ensure resources
                pbOpen.Image = Properties.Resources.edit_icon;    // Edit/pencil icon for the edit button
                pbPin.Image = Properties.Resources.pin_icon;      // Pin icon for the pin button
                pbDelete.Image = Properties.Resources.trash_icon; // Trash icon for delete button

                pbOpen.SizeMode = PictureBoxSizeMode.StretchImage;
                pbPin.SizeMode = PictureBoxSizeMode.StretchImage;
                pbDelete.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting button icons: {ex.Message}");
  
            }

            // Date indicator
            Label lblDate = new Label
            {
                Text = snippet.LastModified.ToString("MM/dd/yyyy"),
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(200, 14)
            };

            pbOpen.Click += (sender, e) => OpenSnippet(snippet);
            pbPin.Click += (sender, e) => ToggleSnippetPin(snippet, pbPin);
            pbDelete.Click += (sender, e) => DeleteSnippet(snippet);
            snippetPanel.Click += (sender, e) => OpenSnippet(snippet);
            txtPreview.Click += (sender, e) => OpenSnippet(snippet);

            headerPanel.Controls.Add(pbLanguage);
            headerPanel.Controls.Add(lblLanguage);
            headerPanel.Controls.Add(lblCodeType);

            actionsPanel.Controls.Add(pbOpen);
            actionsPanel.Controls.Add(pbPin);
            actionsPanel.Controls.Add(pbDelete);
            actionsPanel.Controls.Add(lblDate);

            snippetPanel.Controls.Add(actionsPanel);
            snippetPanel.Controls.Add(headerPanel);
            snippetPanel.Controls.Add(lblTitle);
            snippetPanel.Controls.Add(txtPreview);

            flowPanel.Controls.Add(snippetPanel);
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

                return resource as Image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting language icon: {ex.Message}");
                return null;
            }
        }

        private void ToggleSnippetPin(Snippet snippet, PictureBox pinButton)
        {
            snippet.IsPinned = !snippet.IsPinned;
            pinButton.BackColor = snippet.IsPinned ? Color.Gold : Color.LightGray;

            try
            {
                pinButton.Image = Properties.Resources.pin_icon;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error resetting pin icon: {ex.Message}");
            }

            SnippetManager.SaveSnippet(snippet);

            if (showPinnedOnly)
            {
                ApplyFilters();
            }
        }

        private void DebugAvailableResources()
        {
            try
            {
                var availableResources = new System.Text.StringBuilder();
                var resourceSet = Properties.Resources.ResourceManager.GetResourceSet(
                    System.Globalization.CultureInfo.CurrentCulture, true, true);

                if (resourceSet != null)
                {
                    foreach (System.Collections.DictionaryEntry entry in resourceSet)
                    {
                        availableResources.AppendLine($"{entry.Key} ({entry.Value?.GetType().Name})");
                    }

                    MessageBox.Show(availableResources.ToString(), "Available Resources");
                }
                else
                {
                    MessageBox.Show("No resources found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting resources: {ex.Message}");
            }
        }

        private void OpenSnippet(Snippet snippet)
        {
            CodePlayground playground = new CodePlayground();
            playground.LoadSnippet(snippet);
            playground.Show();

            if (this.ParentForm != null)
            {
                this.ParentForm.Hide();
            }
        }

        private void DeleteSnippet(Snippet snippet)
        {
            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete '{snippet.Name}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SnippetManager.DeleteSnippet(snippet.Id);
                allSnippets.Remove(snippet);

                ApplyFilters();
            }
        }

        private void OpenCodePlayground()
        {
            CodePlayground playground = new CodePlayground();
            playground.Show();

            if (this.ParentForm != null)
            {
                this.ParentForm.Hide();
            }
        }

        private string GetSnippetPreview(string content, int maxLength)
        {
            // preview content
            if (content.Length > maxLength)
            {
                return content.Substring(0, maxLength) + "...";
            }
            return content;
        }

        private void ApplyBasicSyntaxHighlighting(RichTextBox rtb, string language)
        {
            if (string.IsNullOrEmpty(rtb.Text)) return;

            int selectionStart = rtb.SelectionStart;
            int selectionLength = rtb.SelectionLength;

            List<string> keywords = new List<string>();
            List<string> types = new List<string>();

            switch (language.ToLower())
            {
                case "c":
                case "cpp":
                    keywords.AddRange(new[] { "if", "else", "for", "while", "return", "break", "continue", "switch", "case" });
                    types.AddRange(new[] { "int", "float", "double", "char", "void", "bool", "struct", "class" });
                    break;
                case "python":
                    keywords.AddRange(new[] { "if", "else", "for", "while", "return", "import", "from", "def", "class", "try", "except" });
                    types.AddRange(new[] { "int", "float", "str", "list", "dict", "tuple", "set", "bool" });
                    break;
            }

            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.TextLength;
            rtb.SelectionColor = Color.FromArgb(220, 220, 220);

            HighlightPattern(rtb, "\".*?\"", Color.FromArgb(152, 195, 121));

            // keywords
            foreach (string keyword in keywords)
            {
                HighlightWholeWord(rtb, keyword, Color.FromArgb(198, 120, 221)); // Purple for keywords
            }

            // types
            foreach (string type in types)
            {
                HighlightWholeWord(rtb, type, Color.FromArgb(224, 108, 117)); // Red for types
            }

            // comments
            if (language.ToLower() == "python")
            {
                HighlightPattern(rtb, "#.*$", Color.FromArgb(92, 99, 112)); // Gray for comments
            }
            else
            {
                HighlightPattern(rtb, "//.*$", Color.FromArgb(92, 99, 112)); // Gray for comments
            }

            // Restore selection
            rtb.SelectionStart = selectionStart;
            rtb.SelectionLength = selectionLength;
        }

        private void HighlightPattern(RichTextBox rtb, string pattern, Color color)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            foreach (System.Text.RegularExpressions.Match match in regex.Matches(rtb.Text))
            {
                rtb.SelectionStart = match.Index;
                rtb.SelectionLength = match.Length;
                rtb.SelectionColor = color;
            }
        }

        private void HighlightWholeWord(RichTextBox rtb, string word, Color color)
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

        private Color GetLanguageColor(string language)
        {
            switch (language.ToLower())
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

        private string GetLanguageDisplayName(string language)
        {
            switch (language.ToLower())
            {
                case "c":
                    return "C";
                case "cpp":
                    return "C++";
                case "python":
                    return "Python";
                default:
                    return language;
            }
        }

        // custom rounded panel for the snippet cards so they look nicer 
        private class RoundedPanel : Panel
        {
            public RoundedPanel()
            {
                this.DoubleBuffered = true;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Create rounded rectangle
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                GraphicsPath path = new GraphicsPath();

                // tl corner
                path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);

                // tr corner
                path.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);

                // br corner
                path.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);

                // bl corner
                path.AddArc(rect.X, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);

                path.CloseAllFigures();

                this.Region = new Region(path);
                g.FillPath(new SolidBrush(this.BackColor), path);
                g.DrawPath(new Pen(Color.FromArgb(50, 50, 50), 1), path);

                base.OnPaint(e);
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            OpenCodePlayground();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void flowPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        public SnippetControl SnippetControl
        {
            get => default;
            set
            {
            }
        }

        private string NormalizeLanguageName(string language)
        {
            if (string.IsNullOrEmpty(language)) return "";

            switch (language.ToLower())
            {
                case "c++":
                case "cpp":
                    return "cpp";
                case "c":
                    return "c";
                case "python":
                    return "python";
                default:
                    return language.ToLower();
            }
        }

        private void btnFilterDate_Click(object sender, EventArgs e)
        {
            ToggleDateSorting();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadSnippets();
        }
    }
}