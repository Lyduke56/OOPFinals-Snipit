using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SnipIt.Managers
{
    public static class Utilities
    {
        public static void StyleButton(Button button, string hexColor)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = GetTextColor(color);
            button.Font = new Font("Arial", 10, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        public static void StylePictureBox(PictureBox pictureBox, string hexColor)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
            pictureBox.BackColor = color;
            pictureBox.Cursor = Cursors.Hand;
        }

        public static void StyleTextBox(TextBox textBox, string hexColor)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.White;
            textBox.Font = new Font("Arial", 10);
            textBox.ForeColor = Color.Black;
        }

        public static void StyleForm(Form form, string hexColor)
        {
            Color color = ColorTranslator.FromHtml(hexColor);
            form.BackColor = color;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        // Calculate appropriate text color (black or white) based on background color
        private static Color GetTextColor(Color backgroundColor)
        {
            double luminance = (0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B) / 255;
            return luminance > 0.5 ? Color.Black : Color.White;
        }
    }

    public class DraggableFormHelper
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern int SendMessage(nint hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private readonly Form targetForm;
        private readonly Control dragControl;

        public DraggableFormHelper(Form form, Control control)
        {
            targetForm = form ?? throw new ArgumentNullException(nameof(form));
            dragControl = control ?? throw new ArgumentNullException(nameof(control));

            dragControl.MouseDown += DragControl_MouseDown;
        }

        private void DragControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(targetForm.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
