using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace CustomControl
{
    public class PanelButton : Panel
    {
        private Bitmap BackgroundButtonImage;

        [Category("MouseDownImage"), DefaultValue(null), Description("Mouse Down Image")]
        public Bitmap MouseDownImage
        {
            get { return mouseDownImage; }
            set { mouseDownImage = value; }
        }
        private Bitmap mouseDownImage;

        [Category("MouseOverImage"), DefaultValue(null), Description("Mouse Over Image")]
        public Bitmap MouseOverImage
        {
            get { return mouseOverImage; }
            set { mouseOverImage = value; }
        }
        private Bitmap mouseOverImage;

        [Category("DisableImage"), DefaultValue(null), Description("Diable Image")]
        public Bitmap DisableImage
        {
            get { return disableImage;  }
            set { disableImage = value; }
        }
        private Bitmap disableImage;

        [Category("BorderColor"), Browsable(true), DefaultValue(typeof(Color), "Black"), Description("Border Color")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        private Color borderColor;

        [Category("BorderEnable"), Browsable(true), Description("Border Enable")]
        public bool BorderEnable
        {
            get { return borderEnable; }
            set { borderEnable = value; }
        }
        private bool borderEnable;

        public PanelButton()
        {
            this.MouseDown += new MouseEventHandler(this.PanelButton_MouseDown);
            this.MouseUp += new MouseEventHandler(this.PanelButton_MouseUp);
            this.MouseLeave += new EventHandler(this.PanelButton_MouseLeave);
            this.MouseMove += new MouseEventHandler(this.PanelButton_MouseMove);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (null == BackgroundButtonImage) BackgroundButtonImage = (Bitmap)this.BackgroundImage;
            if (null == MouseDownImage) MouseDownImage = (Bitmap)this.BackgroundImage;
            if (null == MouseOverImage) MouseOverImage = (Bitmap)this.BackgroundImage;
            if (true == BorderEnable) ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
        }

        private void PanelButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = mouseDownImage;
        }

        private void PanelButton_MouseUp(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = BackgroundButtonImage;
        }

        private void PanelButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = BackgroundButtonImage;
        }

        private void PanelButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.BackgroundImage = MouseOverImage;
        }
    }
}
