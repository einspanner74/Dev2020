using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    public partial class ucCogID : UserControl
    {
        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;

        private CogBarCodeIDAlgo CogBarCodeIDAlgoRcp = new CogBarCodeIDAlgo();

        public delegate void ApplyBarCodeIDInspValueHandler(CogBarCodeIDAlgo _CogBarCodeIDAlgo, ref CogBarCodeIDResult _CogBarCodeIDResult);
        public event ApplyBarCodeIDInspValueHandler ApplyBarCodeIDInspValueEvent;

        public ucCogID()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            comboBoxSymbology.Items.Clear();
            comboBoxSymbology.Items.Add("DataMatrix");
            comboBoxSymbology.Items.Add("QRCode");
        }

        public void DeInitialize()
        {

        }

        public void SetAlgoRecipe(Object _Algorithm, double _BenchMarkOffsetX, double _BenchMarkOffsetY)
        {
            BenchMarkOffsetX = _BenchMarkOffsetX;
            BenchMarkOffsetY = _BenchMarkOffsetY;

            CogBarCodeIDAlgoRcp = _Algorithm as CogBarCodeIDAlgo;
            comboBoxSymbology.Text = "";
            comboBoxSymbology.SelectedText = CogBarCodeIDAlgoRcp.Symbology;
            numUpDownNumtoFind.Value = CogBarCodeIDAlgoRcp.FindCount;
        }

        public void SaveAlgoRecipe()
        {
            CogBarCodeIDAlgoRcp.Symbology = comboBoxSymbology.Text;
            CogBarCodeIDAlgoRcp.FindCount = Convert.ToInt32(numUpDownNumtoFind.Value);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Teaching CogID SaveAlgoRecipe", CLogManager.LOG_LEVEL.MID);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ApplySettingValue();
        }

        private void ApplySettingValue()
        {
            CogBarCodeIDResult _CogBarCodeIDResult = new CogBarCodeIDResult();
            CogBarCodeIDAlgo _CogBarCodeIDAlgoRcp = new CogBarCodeIDAlgo();
            _CogBarCodeIDAlgoRcp.Symbology = comboBoxSymbology.Text;
            _CogBarCodeIDAlgoRcp.FindCount = Convert.ToInt32(numUpDownNumtoFind.Value);

            var _ApplyBarCodeIDInspValueEvent = ApplyBarCodeIDInspValueEvent;
            if (_ApplyBarCodeIDInspValueEvent != null)
                _ApplyBarCodeIDInspValueEvent(_CogBarCodeIDAlgoRcp, ref _CogBarCodeIDResult);
        }
    }
}
