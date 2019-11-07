using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControl
{
    public static class ControlInvoke
    {
        public static void GradientLabelText(GradientLabel _Control, string _Text)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Text = _Text; }));
            }
            else
            {
                _Control.Text = _Text;
            }
        }

        public static void GradientLabelText(GradientLabel _Control, Color _FontColor, Color _BackColor)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () 
                {
                    _Control.ForeColor = _FontColor;
                    _Control.ColorTop = _BackColor;
                    _Control.ColorBottom = _BackColor;

                }));
            }
            else
            {
                _Control.ForeColor = _FontColor;
                _Control.ColorTop = _BackColor;
                _Control.ColorBottom = _BackColor;
            }
        }

        public static void GradientLabelText(GradientLabel _Control, string _Text, Color _FontColor)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Text = _Text; _Control.ForeColor = _FontColor; _Control.Refresh(); }));
            }
            else
            {
                _Control.Text = _Text; _Control.ForeColor = _FontColor; _Control.Refresh();
            }
        }

        public static void GradientLabelColor(GradientLabel _Control, Color _ColorTop, Color _ColorBottom)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.ColorTop = _ColorTop; _Control.ColorBottom = _ColorBottom; _Control.Refresh(); }));
            }
            else
            {
                _Control.ColorTop = _ColorTop; _Control.ColorBottom = _ColorBottom; _Control.Refresh();
            }
        }

        public static void GridViewCellText(DataGridView _Control, int _Row, int _Cell, string _Data)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Rows[_Row].Cells[_Cell].Value = _Data; _Control.Refresh(); }));
            }
            else
            {
                _Control.Rows[_Row].Cells[_Cell].Value = _Data; _Control.Refresh();
            }
        }

        public static void GridViewRowsColor(DataGridView _Control, int _Row, Color _BackColor, Color _ForeColor)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Rows[_Row].DefaultCellStyle.BackColor = _BackColor; _Control.Rows[_Row].DefaultCellStyle.ForeColor = _ForeColor; _Control.Refresh(); }));
            }
            else
            {
                _Control.Rows[_Row].DefaultCellStyle.BackColor = _BackColor; _Control.Rows[_Row].DefaultCellStyle.ForeColor = _ForeColor; _Control.Refresh();
            }
        }

        public static void TextBoxText(TextBox _Control, string _Text)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Text = _Text; }));
            }
            else
            {
                _Control.Text = _Text;
            }
        }
    }
}
