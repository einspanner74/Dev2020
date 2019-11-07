using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace LogMessageManager
{
    public partial class LogSettingWindow : Form
    {
        private bool ResizingFlag = false;
        private bool IsResizing = false;
        private Point LastPosition = new Point(0, 0);

        Button[] btnLogLevel;

        public delegate void LogWindoCloserwHandler();
        public LogWindoCloserwHandler LogWindoCloserEvent;

        public LogSettingWindow()
        {
            InitializeComponent();
            LastPosition.X = this.Location.X;
            LastPosition.Y = this.Location.Y;

            btnLogLevel = new Button[3]{ btnHigh, btnMid, btnLow };
            InitializeLogLevel();
        }

        private void InitializeLogLevel()
        {
            RegistryKey _RegLogLevel = Registry.CurrentUser.CreateSubKey(@"KPVision\LogLevel");

            if (null == _RegLogLevel.GetValue("Value")) { _RegLogLevel.SetValue("Value", "2", RegistryValueKind.DWord); }

            SetLogLevel(Convert.ToInt32(_RegLogLevel.GetValue("Value")));
        }

        #region Control Default Event
        private void LogSettingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }

        private void LogSettingWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            if (!IsResizing) // handle cursor type
            {
                bool resize_x = e.X > (this.Width - 8);
                bool resize_y = e.Y > (this.Height - 8);

                //if (e.X > 0 && e.X < 2) resize_x = true;
                //if (e.Y > 0 && e.Y < 2) resize_y = true;

                if (resize_x && resize_y) this.Cursor = Cursors.SizeNWSE;
                else if (resize_x) this.Cursor = Cursors.SizeWE;
                else if (resize_y) this.Cursor = Cursors.SizeNS;
                else this.Cursor = Cursors.Default;
            }
            else // handle resize
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

                int w = this.Size.Width;
                int h = this.Size.Height;

                if (this.Cursor.Equals(Cursors.SizeNWSE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h + (e.Location.Y - this.LastPosition.Y));
                else if (this.Cursor.Equals(Cursors.SizeWE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h);
                else if (this.Cursor.Equals(Cursors.SizeNS)) this.Size = new Size(w, h + (e.Location.Y - this.LastPosition.Y));

                this.LastPosition = e.Location;
            }
        }

        private void LogSettingWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsResizing = true;
            this.LastPosition = e.Location;
        }

        private void LogSettingWindow_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsResizing = false;
        }

        private void LogSettingWindow_Resize(object sender, EventArgs e)
        {
            panelMain.Invalidate();
            labelTitle.Invalidate();

            Size _TitleSize = labelTitle.Size;
            _TitleSize.Width = this.Size.Width;

            Point _Location = panelMain.Location;

            labelTitle.Size = new Size(_TitleSize.Width - 6, _TitleSize.Height);
            labelTitle.Location = new Point(3, 3);

            panelMain.Size = new Size(_TitleSize.Width - 6, this.Size.Height - _TitleSize.Height - 6);
            panelMain.Location = new Point(3, labelTitle.Location.Y + labelTitle.Height + 1);
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            //if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

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

        private void panelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            this.Cursor = Cursors.Default;
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion Control Default Event

        #region Control Event
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnLogLevel_Click(object sender, EventArgs e)
        {
            Button btnLevel = (Button)sender;
            SetLogLevel(Convert.ToInt32(btnLevel.Tag));
        }

        private void SetLogLevel(int _LevelNum)
        {
            for(int iLoopCount = 0; iLoopCount < btnLogLevel.Count(); iLoopCount++)
            {
                if (_LevelNum == iLoopCount) btnLogLevel[iLoopCount].BackColor = Color.Orange;
                else                         btnLogLevel[iLoopCount].BackColor = Color.Gainsboro;
            }

            string _RegKeyLogLevelName = String.Format(@"KPVision\LogLevel");
            RegistryKey _RegKeyLogLevel = Registry.CurrentUser.CreateSubKey(_RegKeyLogLevelName);
            _RegKeyLogLevel.SetValue("Value", _LevelNum, RegistryValueKind.DWord);

            CLogManager.SetLogLevel(_LevelNum);
        }
        #endregion Control Event
    }
}
