using System;
using System.Drawing;
using System.Windows.Forms;
using SnipIt.Managers;

namespace SnipIt
{
    public partial class Loading : Form
    {
        private System.Windows.Forms.Timer progressTimer;
        private int progressValue = 0;
        private const int MaxProgress = 100;
        Color color1 = ColorTranslator.FromHtml("#272757"); // Dark Blue
        Color color2 = ColorTranslator.FromHtml("#8686AC"); // Soft Blue
        Color color3 = ColorTranslator.FromHtml("#505081"); // Muted Blue
        Color color4 = ColorTranslator.FromHtml("#0F0E47"); // Deep Navy Blue

        public Loading()
        {
            InitializeComponent();
            InitializeProgressTimer();
            aloneProgressBar1.Value = 0;
            pnlBackground.BackColor = color3;
            panel2.BackColor = color1;
        }

        public Dashboard Dashboard
        {
            get => default;
            set
            {
            }
        }

        private void InitializeProgressTimer()
        {
            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 60; // update per 60 ms
            progressTimer.Tick += ProgressTimer_Tick;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            progressTimer.Start();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            progressValue += 1;
            aloneProgressBar1.Value = progressValue;
            if (progressValue >= MaxProgress)
            {
                progressTimer.Stop();
                Application.DoEvents();

                this.Close();
            }
        }
        public void ShowLoadingFor(int milliseconds)
        {
            progressValue = 0;
            aloneProgressBar1.Value = 0;

            int ticksNeeded = milliseconds / progressTimer.Interval;
            double incrementPerTick = (double)MaxProgress / ticksNeeded;

            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 60;
            progressTimer.Tick += (s, e) =>
            {
                progressValue += (int)Math.Ceiling(incrementPerTick);
                if (progressValue > MaxProgress)
                    progressValue = MaxProgress;

                aloneProgressBar1.Value = progressValue;

                if (progressValue >= MaxProgress)
                {
                    progressTimer.Stop();
                    Application.DoEvents();
                    this.Close();
                }
            };

            progressTimer.Start();
        }

        private void pnlBackground_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}