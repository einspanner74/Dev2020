using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CustomControl
{
    public class GradientLabel : Label
    {
        public enum Direction { Vertical = 0, Horizon }

        public Color ColorTop { get; set; }
        public Color ColorBottom { get; set; }
        public Direction GradientDirection { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            float _Angle = 0;

            if (GradientDirection == Direction.Vertical) _Angle = 0;
            else if (GradientDirection == Direction.Horizon) _Angle = 90;

            LinearGradientBrush _Brush = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, _Angle);
            Graphics _Graphic = e.Graphics;
            _Graphic.FillRectangle(_Brush, this.ClientRectangle);
            base.OnPaint(e);
        }
    }
}
