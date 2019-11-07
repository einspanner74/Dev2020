using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIOControlManager
{
    public partial class DIONamingWindow : Form
    {
        public delegate void ChangeNameHandler(string _Name);
        public event ChangeNameHandler ChangeNameEvent;

        public DIONamingWindow()
        {
            InitializeComponent();
        }

        #region Control Default Event
        private void DIONamingWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            if (s.Tag == null) return;
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
        #endregion Control Default Event

        public void SetCurrentName(string _Name)
        {
            txtNaming.Text = _Name;
        }

        public void ShowWindow(Point _Position)
        {
            this.Show();
            this.Location = _Position;
            //this.ActiveControl = txtNaming;
            txtNaming.Focus();
            txtNaming.SelectAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ChangeNameEvent(txtNaming.Text);
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
