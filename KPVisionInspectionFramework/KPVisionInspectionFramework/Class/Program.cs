using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

using CustomMsgBoxManager;

namespace KPVisionInspectionFramework
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool _IsNew;
            Mutex _Mutex = new Mutex(true, "KPVISION Program", out _IsNew);
            if (_IsNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                _Mutex.ReleaseMutex();
            }
            else
            {
                CMsgBoxManager.Initialize();
                CMsgBoxManager.Show("The program is running.", "", false, 1000);
                //MessageBox.Show(new Form { TopMost = true }, "The program is running.");
                Application.Exit();
            }
        }
    }
}
