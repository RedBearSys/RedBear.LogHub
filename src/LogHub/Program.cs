using System;
using System.Windows.Forms;
using NLog;

namespace LogHub
{
    static class Program
    {
        private static readonly Logger Logger = LogManager.GetLogger("LogHub");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool created;
            var mutexName = $"Global\\LogHub~{Environment.UserDomainName}~{Environment.UserName}";

            using (new System.Threading.Mutex(true, mutexName, out created))
            {
                if (!created) return;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                Application.Run(new MainForm());
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error((Exception)e.ExceptionObject);
        }
    }
}
