using System;
using System.IO;
using System.Windows.Forms;

namespace SnipIt
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Ensure database directory exists for login system
            string dbDirectory = Path.Combine(Application.StartupPath, "databases");
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            // Ensure data directory exists for JSON snippets
            string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            Application.Run(new timer());
        }
    }
}