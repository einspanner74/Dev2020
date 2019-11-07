using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

using CustomControl;
using ParameterManager;
using LogMessageManager;
using HistoryManager;

namespace KPVisionInspectionFramework
{
    public partial class ucMainResultNone : UserControl
    {
        #region Count & Yield Variable
        private uint TotalCount
        {
            set { SegmentValueInvoke(SevenSegTotal, value.ToString()); }
            get { return Convert.ToUInt32(SevenSegTotal.Value); }
        }

        private uint GoodCount
        {
            set { SegmentValueInvoke(SevenSegGood, value.ToString()); }
            get { return Convert.ToUInt32(SevenSegGood.Value); }
        }

        private uint NgCount
        {
            set { SegmentValueInvoke(SevenSegNg, value.ToString()); }
            get { return Convert.ToUInt32(SevenSegNg.Value); }
        }

        private double Yield
        {
            set { SegmentValueInvoke(SevenSegYield, value.ToString()); }
            get { return Convert.ToDouble(SevenSegYield.Value); }
        }
        #endregion Count & Yield Variable

        #region Count & Yield Registry Variable
        private RegistryKey RegTotalCount;
        private RegistryKey RegGoodCount;
        private RegistryKey RegNgCount;
        private RegistryKey RegYield;

        private string RegTotalCountPath = String.Format(@"KPVision\ResultCount\TotalCount");
        private string RegGoodCountPath = String.Format(@"KPVision\ResultCount\GoodCount");
        private string RegNgCountPath = String.Format(@"KPVision\ResultCount\NgCount");
        private string RegYieldPath = String.Format(@"KPVision\ResultCount\Yield");
        #endregion Count & Yield Registry Variable

        private string[] HistoryParam;
        private string[] LastRecipeName;
        private string LastResult;

        public delegate void ScreenshotHandler(string ScreenshotImagePath);
        public event ScreenshotHandler ScreenshotEvent;

        #region Initialize & DeInitialize
        public ucMainResultNone(string[] _LastRecipeName)
        {
            LastRecipeName = new string[_LastRecipeName.Count()];
            SetLastRecipeName(_LastRecipeName);

            InitializeComponent();
            InitializeControl();
            this.Location = new Point(1, 1);

            RegTotalCount = Registry.CurrentUser.CreateSubKey(RegTotalCountPath);
            RegGoodCount = Registry.CurrentUser.CreateSubKey(RegGoodCountPath);
            RegNgCount = Registry.CurrentUser.CreateSubKey(RegNgCountPath);
            RegYield = Registry.CurrentUser.CreateSubKey(RegYieldPath);
            LoadResultCount();
        }

        private void InitializeControl()
        {

            HistoryParam = new string[4];
            for (int iLoopCount = 0; iLoopCount < HistoryParam.Count(); iLoopCount++)
                HistoryParam[iLoopCount] = "-";
        }

        public void DeInitialize()
        {

        }

        private void LoadResultCount()
        {
            TotalCount  = Convert.ToUInt32(RegTotalCount.GetValue("Value"));
            GoodCount   = Convert.ToUInt32(RegGoodCount.GetValue("Value"));
            NgCount     = Convert.ToUInt32(RegNgCount.GetValue("Value"));
            Yield       = Convert.ToDouble(RegYield.GetValue("Value"));

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Load Result Count");
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("TotalCount : {0}, GoodCount : {1}, NgCount : {2}, Yield : {3:F3}", TotalCount, GoodCount, NgCount, Yield));
        }

        private void SaveResultCount()
        {
            RegTotalCount.SetValue("Value", TotalCount, RegistryValueKind.String);
            RegGoodCount.SetValue("Value", GoodCount, RegistryValueKind.String);
            RegNgCount.SetValue("Value", NgCount, RegistryValueKind.String);
            RegYield.SetValue("Value", Yield, RegistryValueKind.String);

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Save Result Count");
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("TotalCount : {0}, GoodCount : {1}, NgCount : {2}, Yield : {3:F3}", TotalCount, GoodCount, NgCount, Yield));
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void SegmentValueInvoke(DmitryBrant.CustomControls.SevenSegmentArray _Control, string _Value)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate () { _Control.Value = _Value; }));
            }
            else
            {
                _Control.Value = _Value;
            }
        }
        /// <summary>
        /// Label Update
        /// </summary>
        /// <param name="_Control">Label Conrol</param>
        /// <param name="_Msg">Label Text</param>
        /// <param name="_BackColor">Label BackColor</param>
        /// <param name="_FontColor">Label ForeColor</param>
        private void LabelUpdateInvoke(Label _Control, string _Msg, Color _BackColor, Color _FontColor)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate ()
                {
                    _Control.Text = _Msg;
                    _Control.BackColor = _BackColor;
                    _Control.ForeColor = _FontColor;
                }
                ));
            }

            else
            {
                _Control.Text = _Msg;
                _Control.BackColor = _BackColor;
                _Control.ForeColor = _FontColor;
            }
        }

        /// <summary>
        /// Label Update
        /// </summary>
        /// <param name="_Control">Label Conrol</param>
        /// <param name="_Msg">Label Text</param>
        /// <param name="_BackColor">Label BackColor</param>
        /// <param name="_FontColor">Label ForeColor</param>
        private void LabelUpdateInvoke(Label _Control, string _Msg)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate ()
                {
                    _Control.Text = _Msg;
                }
                ));
            }

            else
            {
                _Control.Text = _Msg;
            }
        }

        private void gradientLabelTotalCount_DoubleClick(object sender, EventArgs e)
        {
            DialogResult _DlgResult = MessageBox.Show(new Form { TopMost = true }, "Clear Result Count ?", "Clear Count", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (_DlgResult != DialogResult.Yes) return;

            TotalCount = 0;
            GoodCount = 0;
            NgCount = 0;
            Yield = 0;

            SaveResultCount();
        }

        private void gradientLabelGoodCount_DoubleClick(object sender, EventArgs e)
        {
            DialogResult _DlgResult = MessageBox.Show(new Form { TopMost = true }, "Clear Result Count ?", "Clear Count", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (_DlgResult != DialogResult.Yes) return;

            GoodCount = 0;
            Yield = (double)GoodCount / (double)TotalCount * 100;

            SaveResultCount();
        }

        private void gradientLabelNgCount_DoubleClick(object sender, EventArgs e)
        {
            DialogResult _DlgResult = MessageBox.Show(new Form { TopMost = true }, "Clear Result Count ?", "Clear Count", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (_DlgResult != DialogResult.Yes) return;

            NgCount = 0;

            SaveResultCount();
        }
        #endregion Control Default Event

        public void SetAutoMode(bool _AutoModeFlag)
        {

        }

        public void SetLastRecipeName(string[] _LastRecipeName)
        {
            for (int iLoopCount = 0; iLoopCount < _LastRecipeName.Count(); iLoopCount++)
            {
                LastRecipeName[iLoopCount] = _LastRecipeName[iLoopCount];
            }
        }

        public void ClearResult()
        {

        }

        public void SetNoneResultData(SendResultParameter _ResultParam)
        {
            var _Result = _ResultParam.SendResult as SendMeasureResult;

            if (_Result != null)
            {
                if (_ResultParam.IsGood)
                {
                    if (CParameterManager.SystemMode == eSysMode.AUTO_MODE)
                    {
                        TotalCount++;
                        GoodCount++;
                        Yield = (double)GoodCount / (double)TotalCount * 100;
                        SegmentValueInvoke(SevenSegTotal, TotalCount.ToString());
                        SegmentValueInvoke(SevenSegGood, GoodCount.ToString());
                        SegmentValueInvoke(SevenSegYield, Yield.ToString("F2"));
                    }

                    LastResult = "GOOD";
                    ControlInvoke.GradientLabelText(gradientLabelResult, "GOOD", Color.Lime);
                }

                else
                {
                    if (CParameterManager.SystemMode == eSysMode.AUTO_MODE)
                    {
                        TotalCount++;
                        NgCount++;
                        Yield = (double)GoodCount / (double)TotalCount * 100;
                        SegmentValueInvoke(SevenSegTotal, TotalCount.ToString());
                        SegmentValueInvoke(SevenSegNg, NgCount.ToString());
                        SegmentValueInvoke(SevenSegYield, Yield.ToString("F2"));
                    }

                    if (eNgType.NONE == _ResultParam.NgType)
                    {
                        LastResult = "NG";
                        ControlInvoke.GradientLabelText(gradientLabelResult, "NG", Color.Red);
                    }

                    else
                    {
                        LastResult = "NG";
                        ControlInvoke.GradientLabelText(gradientLabelResult, "NG", Color.Red);
                    }
                }
            }

            SaveResultCount();
            InspectionHistory(_ResultParam.ID, LastResult);
        }

        public void InspectionHistory(int _ID, string _Result)
        {

        }
    }
}
