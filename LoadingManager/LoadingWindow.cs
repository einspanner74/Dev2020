using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoadingManager
{
    public partial class LoadingWindow : Form
    {
        private Timer FormTopMostTimer = new Timer();
        private Timer FormSlideTimer = new Timer();
        private Timer FormSlideCloseTimer = new Timer();
        private Timer FormCloseTimer = new Timer();

        public bool IsFormShow = false;
        public bool FormClose = false;

        public delegate void FormCloseHandler(int _DelayTime = 50);
        public FormCloseHandler FormCloseEvent;

        public LoadingWindow()
        {
            InitializeComponent();

            this.AllowTransparency = true;
            this.Opacity = 0;

            FormTopMostTimer.Tick += new EventHandler(FormTopMostTimer_Tick);
            FormTopMostTimer.Interval = 50;

            FormSlideTimer.Tick += new EventHandler(FormSlideTimer_Tick);
            FormSlideTimer.Interval = 50;

            FormSlideCloseTimer.Tick += new EventHandler(FormSlideCloseTimer_Tick);
            FormSlideCloseTimer.Interval = 50;

            FormCloseTimer.Tick += new EventHandler(FormCloseTimer_Tick);
            FormCloseTimer.Interval = 50;
        }

        private void LoadingWindow_Load(object sender, EventArgs e)
        {
            IsFormShow = true;
            this.Opacity = 1;
            //FormSlideTimer.Start();
            FormCloseTimer.Start();
        }

        #region Timer Setting
        private void FormTopMostTimer_Tick(object sender, EventArgs e)
        {
            if (true == IsFormShow)
            {
                Application.DoEvents();
                this.BringToFront();
            }
        }

        private void FormCloseTimer_Tick(object sender, EventArgs e)
        {
            if (true == FormClose)
            {
                FormCloseTimer.Stop();
                this.Dispose();
            }
        }

        private void FormSlideTimer_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.3;
            if (this.Opacity >= 1.0) { this.Opacity = 1; FormSlideTimer.Stop(); }
        }

        private void FormSlideCloseTimer_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.5;
            if (this.Opacity <= 0) { this.Opacity = 0; FormSlideCloseTimer.Stop(); IsFormShow = false; this.Close(); }
        }
        #endregion Timer Setting

        public void ShowLoadingWindow(string _Title, string _Message)
        {
            //FormSlideCloseTimer.Stop();
            this.Text = _Title;
            this.labelTitle.Text = _Title;
            this.labelMessage.Text = _Message;

            //this.Show();
        }

        public void HideLoadingWindow()
        {
            //FormSlideTimer.Stop();
            //FormSlideCloseTimer.Start();

            //if ()
        }

        private void LoadingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X) FormCloseEvent();
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
    }
}
