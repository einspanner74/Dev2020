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
    public partial class ucCogEllipseFind : UserControl
    {
        private CogEllipseAlgo CogEllipseAlgoRcp = new CogEllipseAlgo();

        private double OriginX = 0;
        private double OriginY = 0;
        private double ResolutionX = 0.005;
        private double ResolutionY = 0.005;
        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;

        private bool AlgoInitFlag = false;

        public delegate void ApplyEllipseValueHandler(CogEllipseAlgo _CogEllipseAlgo, ref CogEllipseResult _CogEllipseResult);
        public event ApplyEllipseValueHandler ApplyEllipseValueEvent;

        public delegate void DrawEllipseCaliperHandler(CogEllipseAlgo _CogEllipseAlgo);
        public event DrawEllipseCaliperHandler DrawEllipseCaliperEvent;

        #region Initialize & DeInitialize
        public ucCogEllipseFind()
        {
            InitializeComponent();
        }

        public void Initialize()
        {

        }

        private void InitializeControl()
        {

        }

        public void DeInitialize()
        {

        }
        #endregion Initialize & DeInitialize

        #region Control Event
        private void btnSetting_Click(object sender, EventArgs e)
        {
            ApplySettingValue();
        }

        private void btnDrawCaliper_Click(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void rbSearchDirection_MouseUp(object sender, MouseEventArgs e)
        {
            RadioButton _RadioDirection = (RadioButton)sender;
            int _Direction = Convert.ToInt32(_RadioDirection.Tag);
            SetSearchDirection(_Direction);
            graLabelSearchDirection.Text = _Direction.ToString();
            //ApplySettingValue();
            DrawEllipseFindCaliper();
        }

        private void rbCaliperPolarityD_MouseUp(object sender, MouseEventArgs e)
        {
            RadioButton _RadioPolarity = (RadioButton)sender;
            int _Polarity = Convert.ToInt32(_RadioPolarity.Tag);
            SetPolarity(_Polarity);
            graLabelPolarity.Text = _Polarity.ToString();
            DrawEllipseFindCaliper();
        }

        private void numUpDownCaliperNumber_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownSearchLength_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownProjectionLength_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownArcCenterX_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownArcCenterY_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownArcRadiusX_ValueChanged(object sender, EventArgs e)
        {
            numUpDownArcRadiusY.Value = numUpDownArcRadiusX.Value;
            DrawEllipseFindCaliper();
        }

        private void numUpDownArcRadiusY_ValueChanged(object sender, EventArgs e)
        {
            numUpDownArcRadiusX.Value = numUpDownArcRadiusY.Value;
            DrawEllipseFindCaliper();
        }

        private void numUpDownAngleStart_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }

        private void numUpDownAngleSpan_ValueChanged(object sender, EventArgs e)
        {
            DrawEllipseFindCaliper();
        }
        #endregion Control Event

        public void SetAlgoRecipe(Object _Algorithm, double _BenchMarkOffsetX, double _BenchMarkOffsetY, double _ResolutionX, double _ResolutionY)
        {
            if (_Algorithm != null)
            {
                AlgoInitFlag = false;

                CogEllipseAlgoRcp = _Algorithm as CogEllipseAlgo;

                ResolutionX = _ResolutionX;
                ResolutionY = _ResolutionY;
                BenchMarkOffsetX = _BenchMarkOffsetX;
                BenchMarkOffsetY = _BenchMarkOffsetY;
                numUpDownCaliperNumber.Value = Convert.ToDecimal(CogEllipseAlgoRcp.CaliperNumber / 2);
                numUpDownSearchLength.Value = Convert.ToDecimal(CogEllipseAlgoRcp.CaliperSearchLength);
                numUpDownProjectionLength.Value = Convert.ToDecimal(CogEllipseAlgoRcp.CaliperProjectionLength);
                numUpDownIgnoreNumber.Value = Convert.ToDecimal(CogEllipseAlgoRcp.CaliperIgnoreNumber);
                numUpDownArcCenterX.Value = Convert.ToDecimal(CogEllipseAlgoRcp.ArcCenterX - BenchMarkOffsetX);
                numUpDownArcCenterY.Value = Convert.ToDecimal(CogEllipseAlgoRcp.ArcCenterY - BenchMarkOffsetY);
                numUpDownArcRadiusX.Value = Convert.ToDecimal(CogEllipseAlgoRcp.ArcRadiusX);
                numUpDownArcRadiusY.Value = Convert.ToDecimal(CogEllipseAlgoRcp.ArcRadiusY);
                numUpDownAngleSpan.Value = Convert.ToDecimal(CogEllipseAlgoRcp.ArcAngleSpan);
                textBoxCenterX.Text = (CogEllipseAlgoRcp.OriginX * ResolutionX).ToString("F3");
                textBoxCenterY.Text = (CogEllipseAlgoRcp.OriginY * ResolutionY).ToString("F3");
                textBoxRadiusX.Text = CogEllipseAlgoRcp.OriginRadiusX.ToString("F3");
                textBoxRadiusY.Text = CogEllipseAlgoRcp.OriginRadiusY.ToString("F3");

                graLabelSearchDirection.Text = CogEllipseAlgoRcp.CaliperSearchDirection.ToString();
                graLabelPolarity.Text = CogEllipseAlgoRcp.CaliperPolarity.ToString();
                SetSearchDirection(CogEllipseAlgoRcp.CaliperSearchDirection);
                SetPolarity(CogEllipseAlgoRcp.CaliperPolarity);

                numUpDownDiameterSize.Value = Convert.ToDecimal(CogEllipseAlgoRcp.DiameterSize);
                numUpDownDiameterMinus.Value = Convert.ToDecimal(CogEllipseAlgoRcp.DiameterMinus);
                numUpDownDiameterPlus.Value = Convert.ToDecimal(CogEllipseAlgoRcp.DiameterPlus);

                AlgoInitFlag = true;
            }

            else
            {
                //LOG
            }
        }

        public void SaveAlgoRecipe()
        {
            if (textBoxCenterX.Text == "-") { MessageBox.Show("Check Ellipse CenterX"); return; }
            else if (textBoxCenterY.Text == "-") { MessageBox.Show("Check Ellipse CenterY"); return; }
            else if (textBoxRadiusX.Text == "-") { MessageBox.Show("Check Ellipse RadiusX"); return; }
            else if (textBoxRadiusY.Text == "-") { MessageBox.Show("Check Ellipse RadiusY"); return; }

            CogEllipseAlgoRcp.CaliperNumber = Convert.ToInt32(numUpDownCaliperNumber.Value) * 2;
            CogEllipseAlgoRcp.CaliperSearchLength = Convert.ToDouble(numUpDownSearchLength.Value);
            CogEllipseAlgoRcp.CaliperProjectionLength = Convert.ToDouble(numUpDownProjectionLength.Value);
            CogEllipseAlgoRcp.CaliperSearchDirection = Convert.ToInt32(graLabelSearchDirection.Text);
            CogEllipseAlgoRcp.CaliperIgnoreNumber = Convert.ToInt32(numUpDownIgnoreNumber.Value);
            CogEllipseAlgoRcp.CaliperPolarity = Convert.ToInt32(graLabelPolarity.Text);
            CogEllipseAlgoRcp.ArcCenterX = Convert.ToDouble(numUpDownArcCenterX.Value) + BenchMarkOffsetX;
            CogEllipseAlgoRcp.ArcCenterY = Convert.ToDouble(numUpDownArcCenterY.Value) + BenchMarkOffsetY;
            CogEllipseAlgoRcp.ArcRadiusX = Convert.ToDouble(numUpDownArcRadiusX.Value);
            CogEllipseAlgoRcp.ArcRadiusY = Convert.ToDouble(numUpDownArcRadiusY.Value);
            CogEllipseAlgoRcp.ArcAngleSpan = Convert.ToDouble(numUpDownAngleSpan.Value);
            CogEllipseAlgoRcp.OriginX = OriginX;
            CogEllipseAlgoRcp.OriginY = OriginY;
            CogEllipseAlgoRcp.OriginRadiusX = Convert.ToDouble(textBoxRadiusX.Text);
            CogEllipseAlgoRcp.OriginRadiusY = Convert.ToDouble(textBoxRadiusY.Text);
            CogEllipseAlgoRcp.DiameterSize = Convert.ToDouble(numUpDownDiameterSize.Text);
            CogEllipseAlgoRcp.DiameterPlus = Convert.ToDouble(numUpDownDiameterPlus.Text);
            CogEllipseAlgoRcp.DiameterMinus = Convert.ToDouble(numUpDownDiameterMinus.Text);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Teaching EllipseFind SaveAlgoRecipe", CLogManager.LOG_LEVEL.MID);
        }

        public void SetCaliper(int _CaliperNumber, double _SearchLength, double _ProjectionLength, eSearchDirection _eSearchDir, ePolarity _ePolarity)
        {
            numUpDownCaliperNumber.Value = Convert.ToDecimal(_CaliperNumber / 2);
            numUpDownSearchLength.Value = Convert.ToDecimal(_SearchLength);
            numUpDownProjectionLength.Value = Convert.ToDecimal(_ProjectionLength);
            SetSearchDirection(Convert.ToInt32(_eSearchDir));
            SetPolarity(Convert.ToInt32(_ePolarity));
        }

        public void SetEllipticalArc(double _CenterX, double _CenterY, double _RadiusX, double _RadiusY, double _AngleSpan)
        {
            numUpDownArcCenterX.Value = Convert.ToDecimal(_CenterX);
            numUpDownArcCenterY.Value = Convert.ToDecimal(_CenterY);
            numUpDownArcRadiusX.Value = Convert.ToDecimal(_RadiusX);
            numUpDownArcRadiusY.Value = Convert.ToDecimal(_RadiusY);
            numUpDownAngleSpan.Value = Convert.ToDecimal(_AngleSpan);
        }

        private void SetSearchDirection(int _Direction)
        {
            rbSearchDirectionIn.Checked = false;
            rbSearchDirectionOut.Checked = false;

            switch ((eSearchDirection)_Direction)
            {
                case eSearchDirection.IN_WARD: rbSearchDirectionIn.Checked = true; break;
                case eSearchDirection.OUT_WARD: rbSearchDirectionOut.Checked = true; break;
            }
        }

        private void SetPolarity(int _Polarity)
        {
            rbCaliperPolarityDarkToLight.Checked = false;
            rbCaliperPolarityLightToDark.Checked = false;

            switch ((ePolarity)_Polarity)
            {
                case ePolarity.DARK_TO_LIGHT: rbCaliperPolarityDarkToLight.Checked = true; break;
                case ePolarity.LIGHT_TO_DARK: rbCaliperPolarityLightToDark.Checked = true; break;
            }
        }

        private void ApplySettingValue()
        {
            CogEllipseResult _CogEllipseResult = new CogEllipseResult();
            CogEllipseAlgo _CogEllipseAlgoRcp = new CogEllipseAlgo();
            _CogEllipseAlgoRcp.CaliperNumber = Convert.ToInt32(numUpDownCaliperNumber.Value) * 2;
            _CogEllipseAlgoRcp.CaliperSearchLength = Convert.ToDouble(numUpDownSearchLength.Value);
            _CogEllipseAlgoRcp.CaliperProjectionLength = Convert.ToDouble(numUpDownProjectionLength.Value);
            _CogEllipseAlgoRcp.CaliperSearchDirection = Convert.ToInt32(graLabelSearchDirection.Text);
            _CogEllipseAlgoRcp.CaliperIgnoreNumber = Convert.ToInt32(numUpDownIgnoreNumber.Value);
            _CogEllipseAlgoRcp.CaliperPolarity = Convert.ToInt32(graLabelPolarity.Text);
            _CogEllipseAlgoRcp.ArcCenterX = Convert.ToDouble(numUpDownArcCenterX.Value);
            _CogEllipseAlgoRcp.ArcCenterY = Convert.ToDouble(numUpDownArcCenterY.Value);
            _CogEllipseAlgoRcp.ArcRadiusX = Convert.ToDouble(numUpDownArcRadiusX.Value);
            _CogEllipseAlgoRcp.ArcRadiusY = Convert.ToDouble(numUpDownArcRadiusY.Value);
            _CogEllipseAlgoRcp.ArcAngleSpan = Convert.ToDouble(numUpDownAngleSpan.Value);

            var _ApplyEllipseValueEvent = ApplyEllipseValueEvent;
            if (_ApplyEllipseValueEvent != null)
                _ApplyEllipseValueEvent(_CogEllipseAlgoRcp, ref _CogEllipseResult);

            if (_CogEllipseResult.IsGood)
            {
                textBoxCenterX.Text = (_CogEllipseResult.CenterXReal).ToString("F3");
                textBoxCenterY.Text = (_CogEllipseResult.CenterYReal).ToString("F3");
                textBoxRadiusX.Text = (_CogEllipseResult.RadiusXReal).ToString("F3");
                textBoxRadiusY.Text = (_CogEllipseResult.RadiusYReal).ToString("F3");

                OriginX = _CogEllipseResult.CenterX;
                OriginY = _CogEllipseResult.CenterY;
            }

            else
            {
                textBoxCenterX.Text = "-";
                textBoxCenterY.Text = "-";
                textBoxRadiusX.Text = "-";
                textBoxRadiusY.Text = "-";

                _CogEllipseAlgoRcp.OriginX = 0;
                _CogEllipseAlgoRcp.OriginY = 0;
            }
        }

        private void DrawEllipseFindCaliper()
        {
            if (!AlgoInitFlag) return;

            CogEllipseAlgo _CogEllipseAlgoRcp = new CogEllipseAlgo();
            _CogEllipseAlgoRcp.CaliperNumber = Convert.ToInt32(numUpDownCaliperNumber.Value) * 2;
            _CogEllipseAlgoRcp.CaliperSearchLength = Convert.ToDouble(numUpDownSearchLength.Value);
            _CogEllipseAlgoRcp.CaliperProjectionLength = Convert.ToDouble(numUpDownProjectionLength.Value);
            _CogEllipseAlgoRcp.CaliperSearchDirection = Convert.ToInt32(graLabelSearchDirection.Text);
            _CogEllipseAlgoRcp.CaliperIgnoreNumber = Convert.ToInt32(numUpDownIgnoreNumber.Value);
            _CogEllipseAlgoRcp.CaliperPolarity = Convert.ToInt32(graLabelPolarity.Text);
            _CogEllipseAlgoRcp.ArcCenterX = Convert.ToDouble(numUpDownArcCenterX.Value);
            _CogEllipseAlgoRcp.ArcCenterY = Convert.ToDouble(numUpDownArcCenterY.Value);
            _CogEllipseAlgoRcp.ArcRadiusX = Convert.ToDouble(numUpDownArcRadiusX.Value);
            _CogEllipseAlgoRcp.ArcRadiusY = Convert.ToDouble(numUpDownArcRadiusY.Value);
            _CogEllipseAlgoRcp.ArcAngleSpan = Convert.ToDouble(numUpDownAngleSpan.Value);

            var _DrawEllipseCaliperEvent = DrawEllipseCaliperEvent;
            if (_DrawEllipseCaliperEvent != null)
                _DrawEllipseCaliperEvent(_CogEllipseAlgoRcp);
        }
    }
}
