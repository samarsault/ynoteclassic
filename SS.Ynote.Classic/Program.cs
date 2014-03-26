using System;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(args.Length != 0 ? new MainForm(args[0]) : new MainForm(null));
        }
    }
}