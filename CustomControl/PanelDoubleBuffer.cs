using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControl
{
    public class PanelDoubleBuffer : Panel
    {
        public PanelDoubleBuffer()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true); 
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true); 
            this.SetStyle(System.Windows.Forms.ControlStyles.EnableNotifyMessage, true);
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
