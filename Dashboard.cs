using SnipIt.Managers;
using SnipIt.Models;
using SnipIt.User_Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnipIt
{
    public partial class Dashboard : Form
    {

        private DraggableFormHelper draggableHelper;
        private int borderSize = 2;
        private Size formSize;
        Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue
        Color color3 = ColorTranslator.FromHtml("#505081"); // Muted Blue
        Color color4 = ColorTranslator.FromHtml("#0F0E47"); // Deep Navy Blue

        List<Snippet> snippets = SnippetManager.LoadUserSnippets("userId");

        public Dashboard()
        {
            InitializeComponent();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(98, 102, 244);
            formSize = this.ClientSize;
            this.DoubleBuffered = true;

            parrotDashboard.BottomLeft = color1;
            parrotDashboard.BottomRight = color1;
            parrotDashboard.TopLeft = color1;
            parrotDashboard.TopRight = color1;
            parrotDashboard.PrimerColor = color1;
            pnlTitlebar.BackColor = color4;

            foreach (Control ctrl in parrotDashboard.Controls)
            {
                if (ctrl is Button btn)
                {
                    Utilities.StyleButton(btn, "#272757");
                }
                else if (ctrl is PictureBox pic)
                {
                    Utilities.StylePictureBox(pic, "#272757");
                }
            }


            mainInterface.BackColor = color2;
            Snips snip = new Snips();
            addUC(snip);
        }

        public timer timer
        {
            get => default;
            set
            {
            }
        }

        public UserProfileControl UserProfileControl
        {
            get => default;
            set
            {
            }
        }

        public Snips Snips
        {
            get => default;
            set
            {
            }
        }

        public Settings Settings
        {
            get => default;
            set
            {
            }
        }

        public CodePlayground CodePlayground
        {
            get => default;
            set
            {
            }
        }

        public timer LoginToDashboard
        {
            get => default;
            set
            {
            }
        }

        public Report Report
        {
            get => default;
            set
            {
            }
        }

        public About About
        {
            get => default;
            set
            {
            }
        }

        public Stats Stats
        {
            get => default;
            set
            {
            }
        }

        internal About About1
        {
            get => default;
            set
            {
            }
        }

        internal Report Report1
        {
            get => default;
            set
            {
            }
        }

        internal Snips Snips1
        {
            get => default;
            set
            {
            }
        }

        internal Loading Loading
        {
            get => default;
            set
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserSnippets();
        }

        private void LoadUserSnippets()
        {
            pnlUC.Controls.Clear();

            // hardcoded just to ensure; will try to look for the error sometime soon maybe after the presentation
            FlowLayoutPanel flowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(10)
            };

            string userId = timer.SessionManager.UserId.ToString();
            List<Snippet> snippets = SnippetManager.LoadUserSnippets(userId);

            if (snippets.Count == 0)
            {
                // if no available snippets
                Label noSnippetsLabel = new Label
                {
                    Text = "No code snippets found. Click the + button to create your first snippet!",
                    AutoSize = true,
                    Font = new Font("Arial", 12),
                    ForeColor = Color.White,
                    Location = new Point(20, 20)
                };
                flowPanel.Controls.Add(noSnippetsLabel);
            }
            else
            {
                foreach (var snippet in snippets)
                {
                    SnippetControl snippetControl = new SnippetControl(snippet);
                    flowPanel.Controls.Add(snippetControl);
                }
            }

            pnlUC.Controls.Add(flowPanel);
        }


        // for draggable form thingy CLYDE DO NOT TOUCH OR MAKE CHANGES
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;  // Standard Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020;
            const int SC_RESTORE = 0xF120;
            const int WM_NCHITTEST = 0x0084;   // Detects mouse position for resizing
            const int resizeAreaSize = 10;

            // Hit test values for resizing
            const int HTCLIENT = 1;
            const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12, HTTOPLEFT = 13, HTTOPRIGHT = 14;
            const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;

            if (m.Msg == WM_NCHITTEST && this.WindowState == FormWindowState.Normal)
            {
                base.WndProc(ref m);
                if ((int)m.Result == HTCLIENT)
                {
                    Point screenPoint = new Point(m.LParam.ToInt32());
                    Point clientPoint = this.PointToClient(screenPoint);

                    if (clientPoint.Y <= resizeAreaSize)
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTTOPLEFT;
                        else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTTOPRIGHT;
                        else m.Result = (IntPtr)HTTOP;
                    }
                    else if (clientPoint.Y >= this.Height - resizeAreaSize)
                    {
                        if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTBOTTOM;
                    }
                    else if (clientPoint.X <= resizeAreaSize) m.Result = (IntPtr)HTLEFT;
                    else if (clientPoint.X >= this.Width - resizeAreaSize) m.Result = (IntPtr)HTRIGHT;
                }
                return;
            }

            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                m.Result = IntPtr.Zero;
                return;
            }

            if (m.Msg == WM_SYSCOMMAND)
            {
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)
                    this.Size = formSize;
            }

            base.WndProc(ref m);
        }
        private void addUC(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnlUC.Controls.Clear();
            pnlUC.Controls.Add(userControl);
            userControl.BringToFront();
        }


        private void btnMouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Cursor = Cursors.Hand;
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Cursor = Cursors.Default;
            }
        }

        private void btnMouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor; // Prevent button click highlight
            }
        }

        private void ptbMouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox ptb)
            {
                ptb.Cursor = Cursors.Hand;
            }
        }

        private void ptbMouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox ptb)
            {
                ptb.Cursor = Cursors.Default;
            }
        }

        private void ptbMouseDown(object sender, EventArgs e)
        {
            if (sender is PictureBox ptb)
            {
            }
        }

        private void flpDashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSnippet_Click(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {

        }

        private void btnStats_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {

        }

        private void btnAbout_Click(object sender, EventArgs e)
        {

        }

        private void btnDashboard_MouseEnter(object sender, EventArgs e)
        {

        }

        private void btnDashboard_Leave(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ptbMenu_Click(object sender, EventArgs e)
        {

        }

        private void ptbClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
            Close();
        }

        private void ptbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ptbSnips_Click(object sender, EventArgs e)
        {
            Snips snip = new Snips();
            addUC(snip);
        }

        private void ptbSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            addUC(settings);
        }

        private void ptbAbout_Click(object sender, EventArgs e)
        {

        }

        private void ptbStats_Click(object sender, EventArgs e)
        {
            Stats stats = new Stats();
            addUC(stats);
        }

        private void moonCheckBox1_CheckedChanged(object sender)
        {

        }

        private void pnlTitlebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void parrotDashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
        }

        private void mainInterface_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
            timer login = new timer();
            login.Show();
            Visible = false;
        }

        private void pnlUser_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ptbMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // add button
            CodePlayground codePlayground = new CodePlayground();
            codePlayground.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void ptbProfile_Click(object sender, EventArgs e)
        {
            UserProfileControl userProfileControl = new UserProfileControl();
            SnipIt.Managers.UserManager userManager = new SnipIt.Managers.UserManager();

            try
            {
                SnipIt.Managers.User currentUser = userManager.GetCurrentUser();
                userProfileControl.Initialize(currentUser, userManager);

                userProfileControl.LogoutRequested += (s, args) =>
                {
                    Close();
                    timer login = new timer();
                    login.Show();
                    Visible = false;
                };

                userProfileControl.ProfileUpdated += (s, args) =>
                {
                    timer.SessionManager.Username = currentUser.Username;
                };

                addUC(userProfileControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void pnlUC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ptbAbout_Click_1(object sender, EventArgs e)
        {
            About about = new About();
            addUC(about);
        }

        private void ptbReport_Click(object sender, EventArgs e)
        {
            Report report = new Report();
            addUC(report);
        }
    }
}
