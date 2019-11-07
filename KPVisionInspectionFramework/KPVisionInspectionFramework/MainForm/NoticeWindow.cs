using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPVisionInspectionFramework
{
    public partial class NoticeWindow : Form
    {
        bool WindowShowFlag = false;

        public NoticeWindow()
        {
            InitializeComponent();
        }

        public void ShowWindow()
        {
            WindowShowFlag = true;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(638, 46);

            this.Show();
        }

        public void HideWindow()
        {
            WindowShowFlag = false;

            this.Hide();
        }

        public bool GetWindowStatus()
        {
            return WindowShowFlag;           
        }
    }
}
