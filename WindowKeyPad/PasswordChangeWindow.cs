using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowKeyPad
{
    public partial class PasswordChangeWindow : Form
    {
        private string CurrentPassword;

        #region Initialize
        public PasswordChangeWindow(string _CurrentPassword)
        {
            InitializeComponent();
            CurrentPassword = _CurrentPassword;
        }
        #endregion

        #region Control Default Event
        private void PasswordChangeWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
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
        #endregion Control Default Event

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (CurrentPassword != textBoxCurrentPassword.Text)
            {
                MessageBox.Show("Current password is incorrect.");
                return;
            }

            if (textBoxNewPassword.Text != textBoxNewPasswordCheck.Text)
            {
                MessageBox.Show("New password is incorrect.");
                return;
            }

            CurrentPassword = textBoxNewPassword.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public string GetPassword()
        {
            return CurrentPassword;
        }
    }
}
