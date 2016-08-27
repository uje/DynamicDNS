using DynamicDNS.Core;
using System;
using System.Windows.Forms;

namespace DynamicDNS.Settings {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppHelper.KillAppIfIsRun();
            Application.Run(new frmMain());
        }
    }
}
