using System;
using System.Windows.Forms;
using fieldtool.View;

namespace fieldtool
{
    static class Program
    {
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FrmWorkerStatus frm = new FrmWorkerStatus();
            frm.ShowDialog();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
