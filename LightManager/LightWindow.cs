using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using LogMessageManager;

enum LightCommand { LightOn, LightOff, LightAllOn, LightAllOff, SaveValue }
namespace LightManager
{
    partial class LightWindow : Form
    {
        public delegate void SaveLightValueHandler(int WindowNum, int[] _LightValues);
        public event SaveLightValueHandler SaveLightValueEvent;

        public delegate void SetLightCommandHandler(int _LightNum, LightCommand _Command, int _LightValue = 0);
        public event SetLightCommandHandler SetLightCommandEvent;

        List<int> LightValue;

        public LightWindow()
        {
            InitializeComponent();
        }

        public void Initialize(int[] _LightValue, int MaxLightValue = 255)
        {
            LightValue = new List<int>();

            comboBoxLight.Items.Clear();
            numericUpDownLightValue.Maximum = MaxLightValue;

            SetLightCombobox(_LightValue);
        }

        public void DeInitialize()
        {
            
        }

        #region Control Default Event
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
        #endregion Control Default Event

        private void btnOn_Click(object sender, EventArgs e)
        {
            var _SetLightCommandEvent = SetLightCommandEvent;
            _SetLightCommandEvent?.Invoke(comboBoxLight.SelectedIndex, LightCommand.LightOn, Convert.ToInt32(numericUpDownLightValue.Value));
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            var _SetLightCommandEvent = SetLightCommandEvent;
            _SetLightCommandEvent?.Invoke(comboBoxLight.SelectedIndex, LightCommand.LightOff);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LightValue[comboBoxLight.SelectedIndex] = Convert.ToInt32(numericUpDownLightValue.Value);
            SetLightCommandEvent(comboBoxLight.SelectedIndex, LightCommand.SaveValue, LightValue[comboBoxLight.SelectedIndex]);
            System.Threading.Thread.Sleep(100);

            var _SetLightCommandEvent = SetLightCommandEvent;
            _SetLightCommandEvent?.Invoke(comboBoxLight.SelectedIndex, LightCommand.LightAllOff);
        } 

        private void btnClose_Click(object sender, EventArgs e)
        {
            var _SetLightCommandEvent = SetLightCommandEvent;
            _SetLightCommandEvent?.Invoke(comboBoxLight.SelectedIndex, LightCommand.LightAllOff);
            this.Hide();
        }

        private void SetLightCombobox(int[] _LightValue)
        {
            try
            {
                for (int iLoopCount = 0; iLoopCount < _LightValue.Count(); iLoopCount++)
                {
                    LightValue.Add(_LightValue[iLoopCount]);
                    comboBoxLight.Items.Add("Light" + (iLoopCount + 1).ToString());
                }

                comboBoxLight.SelectedIndex = 0;
                numericUpDownLightValue.Value = LightValue[0];
            }
            catch(Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("LightWindow : SetLightCombobox"));
            }
        }

        private void comboBoxLight_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDownLightValue.Value = LightValue[comboBoxLight.SelectedIndex];
        }
    }
}
