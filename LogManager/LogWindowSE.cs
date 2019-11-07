using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LogMessageManager
{
    public partial class LogWindowSE : Form
    {
        private string ProjectName;
        private bool ResizingFlag = false;
        private bool IsResizing = false;
        private Point LastPosition = new Point(0, 0);

        public delegate void LogWindoCloserwHandler();
        public LogWindoCloserwHandler LogWindoCloserEvent;

        LogSettingWindow LogSettingWnd;

        public LogWindowSE()
        {
            InitializeComponent();
            LastPosition.X = this.Location.X;
            LastPosition.Y = this.Location.Y;

            LogSettingWnd = new LogSettingWindow();
        }

        public void Initialize(string _ProjectName)
        {
            ProjectName = _ProjectName;
        }

        #region Control Default Event
        private void LogWindowSE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }

        private void LogWindowSE_MouseMove(object sender, MouseEventArgs e)
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

        private void LogWindowSE_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsResizing = true;
            this.LastPosition = e.Location;
        }

        private void LogWindowSE_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsResizing = false;
        }

        private void LogWindowSE_Resize(object sender, EventArgs e)
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

            listBoxConfigLog.Size = new Size(panelMain.Size.Width - 13, panelMain.Size.Height - 11);
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

        private void labelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ResizingFlag = !ResizingFlag;
            if (true == ResizingFlag) labelTitle.ForeColor = Color.Chocolate;
            else labelTitle.ForeColor = Color.WhiteSmoke;
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

        #region "Control Invoke"
        /// <summary>
        /// 컨트롤 Text 입력 Invoke
        /// </summary>
        /// <param name="_Control">Control </param>
        /// <param name="_msg">text</param>
        public static void ListBoxInvoke(ListBox _Control, string _Msg)
        {
            int LogCount = 300;

            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate()
                {

                    if (_Control.Items.Count > LogCount) _Control.Items.RemoveAt(LogCount);
                    _Control.Items.Insert(0, _Msg);
                }
                ));
            }
            else
            {
                if (_Control.Items.Count > LogCount) _Control.Items.RemoveAt(LogCount);
                _Control.Items.Insert(0, _Msg);
            }
        }
        #endregion

        #region Control Event
        private void labelLogClear_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxConfigLog.Items.Clear();
        }

        private void btnLogFolderOpen_Click(object sender, EventArgs e)
        {
            string _LogFileFolderPath = String.Format(@"D:\VisionInspectionData\{0}\Log", ProjectName);

            LogWindoCloserEvent();

            System.Diagnostics.Process.Start(_LogFileFolderPath);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            LogWindoCloserEvent();
        }
        #endregion Control Event

        public void AddLogMessage(string _LogMessage)
        {
            if (_LogMessage == null) return;

            ListBoxInvoke(listBoxConfigLog, _LogMessage);
        }

        private void btnSetLog_Click(object sender, EventArgs e)
        {
            LogSettingWnd.ShowDialog();
        }
    }
}
