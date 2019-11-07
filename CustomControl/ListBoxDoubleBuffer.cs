using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControl
{
    public class ListBoxDoubleBuffer : ListBox
    {
        public ListBoxDoubleBuffer()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
            this.DoubleBuffered = true;
        }

        protected override void OnNotifyMessage(System.Windows.Forms.Message m)
        {
            if (m.Msg != 0x0014) // WM_ERASEBKGND == 0X0014         
            {
                base.OnNotifyMessage(m);
            }
        } 
    }
}
