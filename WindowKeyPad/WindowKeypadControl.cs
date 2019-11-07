using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowKeyPad
{
    public partial class WindowKeypadControl : Form
    {
        static int CHARACTOR_COUNT = 52;
        Button[] btnKeyPads = new Button[CHARACTOR_COUNT];
        bool IsCapsLock = false;
        bool IsKeyEnabledFlag = false;
        bool IsEnablePassword = false;

        public string KeyPadCharactor = "";

        private string Password;

        #region Initialize
        public WindowKeypadControl(bool _IsKeyEnabled = false)
        {
            InitializeComponent();
            Initialize(_IsKeyEnabled);
        }

        public void Initialize(bool _IsKeyEnabled, bool _IsEnablePassword = false)
        {
            btnKeyPads = new Button[]   {   btnA,           btnB,       btnC,               btnD,               btnE,           btnF,           btnG,       btnH,       btnI,       btnJ,       btnK,       btnL,       btnN,       btnM,               btnO,
                                            btnP,           btnQ,       btnR,               btnS,               btnT,           btnU,           btnV,       btnW,       btnX,       btnY,       btnZ,       btnMinus,   btnEqual,   btnSemi,            btnComma,   
                                            btnSlash,       btnWon,     btnRightBlacket,    btnLeftBlacket,     btnQuotation,   btnCaps,        btnShift,   btnShift2,  btnEnter,   btnPoint,   btnOK,      btnCancel,   
                                            btn1,           btn2,       btn3,               btn4,               btn5,           btn6,           btn7,       btn8,       btn9,       btn0,   };
            IsKeyEnabledFlag = _IsKeyEnabled;
            IsEnablePassword = _IsEnablePassword;
            EnableCharactorKey();

            //Motion 부분 때문에 Minus Keypad 활성화
            btnMinus.Enabled = true;

            labelKeyPadCharactor.Text = "";
            KeyPadCharactor = "";

            btnOK.Focus();
        }

        public void SetPassword(string _Password)
        {
            Password = _Password;
        }

        public string GetPassword()
        {
            return Password;
        }
        #endregion

        #region Control Default Event
        private void WindowKeypadControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4)  e.Handled = true;
            if (e.KeyCode == Keys.Enter)        PressEnter();  
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

        private void EnableCharactorKey()
        {
            //for (int iLoopCount = 0; iLoopCount < CHARACTOR_COUNT; ++iLoopCount) btnKeyPads[iLoopCount].Enabled = true;
            for (int iLoopCount = 0; iLoopCount < CHARACTOR_COUNT - 13; ++iLoopCount) btnKeyPads[iLoopCount].Enabled = IsKeyEnabledFlag;
        }

        public void SetKeyPadValue(string strValue)
        {
            if (false == IsEnablePassword)
            {
                KeyPadCharactor = strValue;
                labelKeyPadCharactor.Text = strValue;
            }

            else
            {
                KeyPadCharactor = "";
                labelKeyPadCharactor.Text = "";
            }
        }

        public void PressKeyDown(string strKey)
        {
            if (false == IsEnablePassword)
            {
                KeyPadCharactor += strKey;
                labelKeyPadCharactor.Text = KeyPadCharactor;
            }

            else
            {
                KeyPadCharactor += strKey;
                labelKeyPadCharactor.Text = "";
                for (int iLoopCount = 0; iLoopCount < KeyPadCharactor.Length; ++iLoopCount)
                    labelKeyPadCharactor.Text += "*";
            }
        }

        public void PressKeyButtonDown(string strKey)
        {
            if (true == IsCapsLock) strKey = strKey.ToUpper();
            else if (false == IsCapsLock) strKey = strKey.ToLower();
            KeyPadCharactor += strKey;
            
            if (false == IsEnablePassword)
            {
                labelKeyPadCharactor.Text = KeyPadCharactor;
            }

            else
            {
                labelKeyPadCharactor.Text = "";
                for (int iLoopCount = 0; iLoopCount < KeyPadCharactor.Length; ++iLoopCount)
                    labelKeyPadCharactor.Text += "*";
            }
        }

        public void PressBackSpace()
        {
            if (KeyPadCharactor.Length == 0) return;
            KeyPadCharactor = KeyPadCharactor.Substring(0, KeyPadCharactor.Length - 1);

            if (false == IsEnablePassword)
            {
                labelKeyPadCharactor.Text = KeyPadCharactor;
            }

            else
            {
                labelKeyPadCharactor.Text = "";
                for (int iLoopCount = 0; iLoopCount < KeyPadCharactor.Length; ++iLoopCount)
                    labelKeyPadCharactor.Text += "*";
            }
        }

        public void PressKeyClear()
        {
            KeyPadCharactor = "";
            labelKeyPadCharactor.Text = "";
        }

        private void WindowKeyPad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) PressBackSpace();
            else if (false == IsKeyEnabledFlag && (e.KeyChar < 48 || e.KeyChar > 57))    return;
            else
            {
                string strCharactor = e.KeyChar.ToString();
                if ((int)Keys.Escape == e.KeyChar) PressKeyClear();
                else if ((int)Keys.Enter == e.KeyChar) PressEnter();
                else PressKeyDown(strCharactor);
            }

            btnOK.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PressEnter();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            PressCancel();
        }

        private void PressEnter()
        {
            if (false == IsKeyEnabledFlag && "" == KeyPadCharactor) KeyPadCharactor = "0";
            labelKeyPadCharactor.Text = "";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void PressCancel()
        {
            this.DialogResult = DialogResult.Cancel;
            labelKeyPadCharactor.Text = "";
            this.Close();
        }

        private void PressShift()
        {
            IsCapsLock = !IsCapsLock;
            if (true == IsCapsLock) { btnShift.ForeColor = Color.Blue; btnShift2.ForeColor = Color.Blue; }
            else if (false == IsCapsLock) { btnShift.ForeColor = Color.Black; btnShift2.ForeColor = Color.Black; }
        }

        private void ButtonsClickEvent(object sender, EventArgs e)
        {
            Button KeyPadButton = (Button)sender;
            switch (KeyPadButton.Text)
            {
                case "Shift":
                case "Shift2":  PressShift();       break;
                case "Back":    PressBackSpace();   break;
                case "ESC":     PressKeyClear();    break;
                case "Enter":   PressEnter();       break;
                case "OK":      PressEnter();       break;
                case "Cancel":  PressCancel();      break;
                default:        PressKeyButtonDown(KeyPadButton.Text); break;
            }
        }

        private void btnPasswordChange_Click(object sender, EventArgs e)
        {
            PasswordChangeWindow _PasswordChangeWnd = new PasswordChangeWindow(Password);
            _PasswordChangeWnd.ShowDialog();
            if (_PasswordChangeWnd.DialogResult == DialogResult.Cancel) return;

            Password = _PasswordChangeWnd.GetPassword();
        }
    }
}
