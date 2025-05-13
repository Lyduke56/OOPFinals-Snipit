using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SnipIt.User_Controls
{
    public partial class Settings : UserControl
    {
        private Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        private Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue

        public Settings()
        {
            InitializeComponent();
            SetupIconSettings();
        }

        public Dashboard settings
        {
            get => default;
            set
            {
            }
        }

        private void SetupIconSettings()
        {
            // Add table headers
            AddTableHeader("Icon Type");
            AddTableHeader("Current Icon");
            AddTableHeader("Change");
            AddTableHeader("Reset");

            // Add icon rows with colored placeholders instead of images
            AddIconRow("C Language", "c", Color.DodgerBlue);
            AddIconRow("C++ Language", "cpp", Color.RoyalBlue);
            AddIconRow("Python Language", "python", Color.MediumPurple);
            AddIconRow("Generic Code", "code", Color.SlateGray);
            AddIconRow("Edit Button", "edit", Color.MediumSeaGreen);
            AddIconRow("Pin Outline", "pin_outline", Color.LightGray);
            AddIconRow("Pin Filled", "pin_filled", Color.Gold);
            AddIconRow("Delete Button", "delete", Color.Tomato);
        }

        private void AddTableHeader(string text)
        {
            Label header = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
        }

        private void AddIconRow(string displayName, string iconType, Color placeholderColor)
        {

            // Icon name label
            Label lblName = new Label
            {
                Text = displayName,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.White,
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Current icon display - using a colored panel as placeholder
            Panel iconPlaceholder = new Panel
            {
                BackColor = placeholderColor,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(32, 32),
                Dock = DockStyle.Fill,
                Margin = new Padding(10)
            };

            // Change button
            Button btnChange = new Button
            {
                Text = "Change",
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = color1,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };

            btnChange.Click += (sender, e) => ChangeIcon(iconType, iconPlaceholder);

            // Reset button
            Button btnReset = new Button
            {
                Text = "Reset",
                Font = new Font("Segoe UI", 9),
                FlatStyle = FlatStyle.Flat,
                BackColor = color1,
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };

            btnReset.Click += (sender, e) => ResetIcon(iconType, iconPlaceholder);

        }

        private void ChangeIcon(string iconType, Panel iconPlaceholder)
        {
            // Show a message indicating this is a placeholder for now
            MessageBox.Show(
                $"This is a placeholder for changing the '{iconType}' icon. Once you add your custom icons, this will allow users to customize them.",
                "Icon Placeholder",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // For demonstration, change the panel's color to show an action happened
            iconPlaceholder.BackColor = Color.FromArgb(
                new Random().Next(100, 255),
                new Random().Next(100, 255),
                new Random().Next(100, 255)
            );
        }

        private void ResetIcon(string iconType, Panel iconPlaceholder)
        {
            // Show a message indicating this is a placeholder for now
            MessageBox.Show(
                $"This is a placeholder for resetting the '{iconType}' icon to default. Once you add your custom icons, this will reset to the default.",
                "Icon Placeholder",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Reset to original color
            switch (iconType)
            {
                case "c":
                    iconPlaceholder.BackColor = Color.DodgerBlue;
                    break;
                case "cpp":
                    iconPlaceholder.BackColor = Color.RoyalBlue;
                    break;
                case "python":
                    iconPlaceholder.BackColor = Color.MediumPurple;
                    break;
                case "code":
                    iconPlaceholder.BackColor = Color.SlateGray;
                    break;
                case "edit":
                    iconPlaceholder.BackColor = Color.MediumSeaGreen;
                    break;
                case "pin_outline":
                    iconPlaceholder.BackColor = Color.LightGray;
                    break;
                case "pin_filled":
                    iconPlaceholder.BackColor = Color.Gold;
                    break;
                case "delete":
                    iconPlaceholder.BackColor = Color.Tomato;
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}