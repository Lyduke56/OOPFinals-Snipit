using System;
using System.Drawing;
using System.Windows.Forms;
using SnipIt.Managers;

namespace SnipIt
{
    public partial class UserProfileControl : UserControl
    {
        private SnipIt.Managers.User _currentUser;
        private SnipIt.Managers.UserManager _userManager;

        public event EventHandler LogoutRequested;
        public event EventHandler ProfileUpdated;
        Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue
        Color color3 = ColorTranslator.FromHtml("#505081"); // Muted Blue
        Color color4 = ColorTranslator.FromHtml("#0F0E47"); // Deep Navy Blue

        public UserProfileControl()
        {
            InitializeComponent();
            pnlHeader.BackColor = color1;
        }

        public void Initialize(SnipIt.Managers.User user, SnipIt.Managers.UserManager userManager)
        {
            _currentUser = user;
            _userManager = userManager;

            lblWelcome.Text = string.Format("Welcome, {0}!", _currentUser.Username);
            tbxUsername.Text = _currentUser.Username;

            if (tbxEmail != null)
            {
                tbxEmail.Text = _currentUser.Email ?? string.Empty;
            }
        }

        private void btnUpdateUsername_Click(object sender, EventArgs e)
        {
            try
            {
                string newUsername = tbxUsername.Text.Trim();

                // Validate username
                if (string.IsNullOrWhiteSpace(newUsername))
                {
                    MessageBox.Show("Username cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if username already exists
                if (newUsername != _currentUser.Username && _userManager.UsernameExists(newUsername))
                {
                    MessageBox.Show("Username already exists. Please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update username
                _currentUser.Username = newUsername;
                _userManager.UpdateUser(_currentUser);

                // Update welcome message
                lblWelcome.Text = string.Format("Welcome, {0}!", _currentUser.Username);

                MessageBox.Show("Username updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Notify profile update
                ProfileUpdated?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating username: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string currentPassword = tbxCurrentPassword.Text;
                string newPassword = tbxNewPassword.Text;
                string confirmPassword = tbxConfirmPassword.Text;

                // Validate inputs
                if (string.IsNullOrWhiteSpace(currentPassword) ||
                    string.IsNullOrWhiteSpace(newPassword) ||
                    string.IsNullOrWhiteSpace(confirmPassword))
                {
                    MessageBox.Show("All password fields are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Verify current password
                if (!_userManager.VerifyPassword(_currentUser.UserId, currentPassword))
                {
                    MessageBox.Show("Current password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if new password matches confirmation
                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("New password and confirmation do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate password strength
                if (newPassword.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Change password
                _userManager.ChangePassword(_currentUser.UserId, newPassword);

                // Clear password fields
                tbxCurrentPassword.Clear();
                tbxNewPassword.Clear();
                tbxConfirmPassword.Clear();

                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            // Ask for confirmation
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete your account?\nThis action cannot be undone and all your data will be permanently lost.",
                "Delete Account Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // First, delete all snippets for this user
                    SnippetManager.DeleteAllUserSnippets(_currentUser.UserId);

                    // Then delete the user account
                    _userManager.DeleteUser(_currentUser.UserId);

                    MessageBox.Show("Your account has been deleted successfully.", "Account Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Trigger logout
                    LogoutRequested?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Trigger logout event
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Header label click event - either leave empty or remove
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClearSnips_Click(object sender, EventArgs e)
        {
            // Ask for confirmation with a warning message
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete ALL your snippets?\nThis action cannot be undone and all your snippets will be permanently lost.",
                "Delete All Snippets Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete all snippets for the current user
                    SnipIt.Managers.SnippetManager.DeleteAllUserSnippets(_currentUser.UserId);

                    MessageBox.Show("All your snippets have been deleted successfully.", "Snippets Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting snippets: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkShowpass_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkShowpass.Checked)
            {
                tbxCurrentPassword.UseSystemPasswordChar = true;
                tbxNewPassword.UseSystemPasswordChar = true;
                tbxConfirmPassword.UseSystemPasswordChar = true;
            }
            else
            {
                tbxCurrentPassword.UseSystemPasswordChar = false;
                tbxNewPassword.UseSystemPasswordChar = false;
                tbxConfirmPassword.UseSystemPasswordChar = false;
            }
        }

        private void btnUpdateEmail_Click(object sender, EventArgs e)
        {
            try
            {
                string newEmail = tbxEmail.Text.Trim();

                // Basic email validation
                if (!string.IsNullOrWhiteSpace(newEmail))
                {
                    // Simple email format validation
                    if (!newEmail.Contains("@") || !newEmail.Contains(".") || newEmail.Length < 5)
                    {
                        MessageBox.Show("Please enter a valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update email
                bool success = _userManager.UpdateEmail(_currentUser.UserId, newEmail);

                if (success)
                {
                    // Update the user object with the new email
                    _currentUser.Email = newEmail;

                    MessageBox.Show("Email updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Notify profile update
                    ProfileUpdated?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to update email. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Dashboard profile
        {
            get => default;
            set
            {
            }
        }

        internal Dashboard Dashboard
        {
            get => default;
            set
            {
            }
        }
    }
}