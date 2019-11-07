using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControl
{
    public class GradientPanel : Label
    {
        public Color ColorTop { get; set; }
        public Color ColorBottom { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush _Brush = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, 90);
            Graphics _Graphic = e.Graphics;
            _Graphic.FillRectangle(_Brush, this.ClientRectangle);
            base.OnPaint(e);
        }
    }
}
