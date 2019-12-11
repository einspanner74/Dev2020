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
using System.Xml;

using CustomControl;
using LogMessageManager;

namespace KPVisionInspectionFramework
{
    public partial class ResultConditionAlign : Form
    {
        public bool IsShowResultConditionAlignWindow = false;
        string ProjectItemParameterFullPath;

        AlignResultData ResConAlignData = new AlignResultData();

        int StageID;

        public delegate void MaskAlignHandler(object _Command);
        public event MaskAlignHandler MaskAlignEvent;

        public delegate void GetMaskPositionHandler(int _AlignDefine);
        public event GetMaskPositionHandler GetMaskPositionEvent;

        public ResultConditionAlign(int _StageID)
        {
            InitializeComponent();

            StageID = _StageID;

            ProjectItemParameterFullPath = string.Format(@"D:\VisionInspectionData\Common\ProjectItemParameter{0}.Sys", _StageID);            
            Initialize();
        }

        public bool Initialize()
        {
            bool _Result = true;
            
    		ReadProjectItemParameter();
            SetPositionValue();

            return _Result;
        }

        public void DeInitialize()
        {

        }

        public void ShowResultConditionAlignWindow()
        {
            IsShowResultConditionAlignWindow = true;
            this.Show();
        }

        public void SetResultConditionAlignWindowTopMost(bool _IsTopMost)
        {
            this.TopMost = _IsTopMost;
        }

        public void SetMaskPosition(double _MaskPositionX, double _MaskPositionY)
        {
            ResConAlignData.MaskPosition.X = _MaskPositionX;
            ResConAlignData.MaskPosition.Y = _MaskPositionY;
        }

        public void SetCalFirstPosition(int _CamNum, double _CamOriginX, double _CamOriginY)
        {
            switch(_CamNum)
            {
                case 0:
                    {                        
                        ResConAlignData.FirstCamOrigin.X = _CamOriginX;
                        ResConAlignData.FirstCamOrigin.Y = _CamOriginY;
                    }break;
                case 1:
                    {
                        ResConAlignData.SecondCamOrigin.X = _CamOriginX;
                        ResConAlignData.SecondCamOrigin.Y = _CamOriginY;
                    }break;
            }
        }

        public void SetCalSecondPosition(int _CamNum, double _CamMoveX, double _CamMoveY)
        {
            switch (_CamNum)
            {
                case 0:
                    {
                        ResConAlignData.FirstCamMove.X = _CamMoveX;
                        ResConAlignData.FirstCamMove.Y = _CamMoveY;
                    }
                    break;
                case 1:
                    {
                        ResConAlignData.SecondCamMove.X = _CamMoveX;
                        ResConAlignData.SecondCamMove.Y = _CamMoveY;
                    }
                    break;
            }

            CalibrationStageCenterXY();
        }

        private void CalibrationStageCenterXY()
        {
            double MarkDistance = Convert.ToDouble(numericUpDownMarkDistance.Value);

            //Image Size 설정하기!!!!!!!!!!!!
            int _ImageWidth = 2464;
            int _ImageHeight = 2056;

            double _OrgCam1X = (ResConAlignData.FirstCamOrigin.X - (_ImageWidth * 0.5)) * ResConAlignData.PXResolution1;
            double _OrgCam1Y = (ResConAlignData.FirstCamOrigin.Y - (_ImageHeight * 0.5)) * ResConAlignData.PXResolution1;
            double _OrgCam2X = (ResConAlignData.SecondCamOrigin.X - (_ImageWidth * 0.5)) * ResConAlignData.PXResolution2;
            double _OrgCam2Y = (ResConAlignData.SecondCamOrigin.Y - (_ImageHeight * 0.5)) * ResConAlignData.PXResolution2;
            double _MoveCam1X = (ResConAlignData.FirstCamMove.X - (_ImageWidth * 0.5)) * ResConAlignData.PXResolution1;
            double _MoveCam1Y = (ResConAlignData.FirstCamMove.Y - (_ImageHeight * 0.5)) * ResConAlignData.PXResolution1;
            double _MoveCam2X = (ResConAlignData.SecondCamMove.X - (_ImageWidth * 0.5)) * ResConAlignData.PXResolution2;
            double _MoveCam2Y = (ResConAlignData.SecondCamMove.Y - (_ImageHeight * 0.5)) * ResConAlignData.PXResolution2;

            PointD FirstCamOriginReal = new PointD();
            PointD SecondCamOriginReal = new PointD();

            FirstCamOriginReal.Y = ResConAlignData.FirstCamOrigin.Y * ResConAlignData.PXResolution1;
            SecondCamOriginReal.Y = ResConAlignData.SecondCamOrigin.Y * ResConAlignData.PXResolution2;

            #region Test
            //double _OrgCam1X = FirstRealPositionCam1X;
            //double _OrgCam1Y = FirstRealPositionCam1Y;
            //double _OrgCam2X = FirstRealPositionCam2X;
            //double _OrgCam2Y = FirstRealPositionCam2Y;
            //double _MoveCam1X = MoveRealPositionCam1X;
            //double _MoveCam1Y = MoveRealPositionCam1Y;
            //double _MoveCam2X = MoveRealPositionCam2X;
            //double _MoveCam2Y = MoveRealPositionCam2Y;
            #endregion

            double _FirstPosAngle = Math.Asin((_OrgCam2Y - _OrgCam1Y) / MarkDistance) * (180 / ResConAlignData.PI);
            double _MovePosAngle = Math.Asin((_MoveCam2Y - _MoveCam1Y) / MarkDistance) * (180 / ResConAlignData.PI);
            double _DAngle = _FirstPosAngle - _MovePosAngle;

            double _dX = _MoveCam1X - _OrgCam1X;       //??
            double _dY = _MoveCam1Y - _OrgCam1Y;
            double _dXY = Math.Sqrt(Math.Pow(_dX, 2) + Math.Pow(_dY, 2));       //직선거리

            double _LineA, _LineB, _LineC;
            _LineA = _dXY / 2;
            double _Theta1 = Math.Atan2(_dY, _dX) * 180 / ResConAlignData.PI;
            double _ThetaD = 90 - Math.Abs(_Theta1);
            double _Theta2 = 90 - Math.Abs(_DAngle) / 2;
            double _Theta3 = 0;

            //각도 비교 확인 필요
            double _ZAngle = Math.Asin((ResConAlignData.SecondCamOrigin.Y - ResConAlignData.FirstCamOrigin.Y) / MarkDistance) * (180 / ResConAlignData.PI);
            double _ZAngleTest = Math.Asin((SecondCamOriginReal.Y - FirstCamOriginReal.Y) / MarkDistance) * (180 / ResConAlignData.PI);
            if (_DAngle <= 0) _Theta3 = 180 - Math.Abs(_Theta2) - Math.Abs(_ThetaD) - _ZAngle;
            else _Theta3 = 180 - Math.Abs(_Theta2) - Math.Abs(_ThetaD) + _ZAngle;

            _LineB = (_dXY / 2) * Math.Tan(_Theta2 * ResConAlignData.PI / 180);
            _LineC = Math.Sqrt(Math.Pow(_LineA, 2) + Math.Pow(_LineB, 2));

            ResConAlignData.AlignRotateCenterX = _LineC * Math.Sin(_Theta3 * ResConAlignData.PI / 180);
            ResConAlignData.AlignRotateCenterY = _LineC * Math.Cos(_Theta3 * ResConAlignData.PI / 180);

            ControlInvoke.TextBoxText(textBoxCetnerX, ResConAlignData.AlignRotateCenterX.ToString("F3"));
            ControlInvoke.TextBoxText(textBoxCetnerY, ResConAlignData.AlignRotateCenterY.ToString("F3"));
        }

        #region Control Default Event
        private void ucResultConditionAlign_KeyDown(object sender, KeyEventArgs e)
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

        private void btnMaskSet_Click(object sender, EventArgs e)
        {
            MaskAlignEvent(8);
        }

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            var _GetMaskPositionEvent = GetMaskPositionEvent;

            if(StageID == 0) _GetMaskPositionEvent?.Invoke(AL_DEF.CAL2);
            else             _GetMaskPositionEvent?.Invoke(AL_DEF.CAL1);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IsShowResultConditionAlignWindow = false;
            this.Hide();
        }

        private void btnPositionSave_Click(object sender, EventArgs e)
        {
            ResConAlignData.AlignMarkDistance = Convert.ToDouble(numericUpDownMarkDistance.Value);
            ResConAlignData.AlignRotateCenterX = Convert.ToDouble(textBoxCetnerX.Text);
            ResConAlignData.AlignRotateCenterY = Convert.ToDouble(textBoxCetnerY.Text);

            ResConAlignData.UVWRadius = Convert.ToDouble(numericUpDownRadius.Value);
            ResConAlignData.PinJointPosition.U = Convert.ToDouble(numericUpDownPinJointU.Value);
            ResConAlignData.PinJointPosition.V = Convert.ToDouble(numericUpDownPinJointV.Value);
            ResConAlignData.PinJointPosition.W = Convert.ToDouble(numericUpDownPinJointW.Value);

            ResConAlignData.MarginError.X = Convert.ToDouble(numericUpDownXPermitValue.Value);
            ResConAlignData.MarginError.Y = Convert.ToDouble(numericUpDownYPermitValue.Value);
            ResConAlignData.MarginError.T = Convert.ToDouble(numericUpDownTPermitValue.Value);

            ResConAlignData.OffsetValue.X = Convert.ToDouble(numericUpDownXOffsetValue.Value);
            ResConAlignData.OffsetValue.Y = Convert.ToDouble(numericUpDownYOffsetValue.Value);
            ResConAlignData.OffsetValue.T = Convert.ToDouble(numericUpDownTOffsetValue.Value);

            ResConAlignData.UVWCurrentPos.U = Convert.ToDouble(textBoxCurrentPosU.Text);
            ResConAlignData.UVWCurrentPos.V = Convert.ToDouble(textBoxCurrentPosV.Text);
            ResConAlignData.UVWCurrentPos.W = Convert.ToDouble(textBoxCurrentPosW.Text);

            WriteProjectItemParameter();
        }

        private void SetPositionValue()
        {
            numericUpDownMarkDistance.Value = Convert.ToDecimal(ResConAlignData.AlignMarkDistance);
            textBoxCetnerX.Text = ResConAlignData.AlignRotateCenterX.ToString();
            textBoxCetnerY.Text = ResConAlignData.AlignRotateCenterY.ToString();

            numericUpDownRadius.Value = Convert.ToDecimal(ResConAlignData.UVWRadius);
            numericUpDownPinJointU.Value = Convert.ToDecimal(ResConAlignData.PinJointPosition.U);
            numericUpDownPinJointV.Value = Convert.ToDecimal(ResConAlignData.PinJointPosition.V);
            numericUpDownPinJointW.Value = Convert.ToDecimal(ResConAlignData.PinJointPosition.W);

            numericUpDownXPermitValue.Value = Convert.ToDecimal(ResConAlignData.MarginError.X);
            numericUpDownYPermitValue.Value = Convert.ToDecimal(ResConAlignData.MarginError.Y);
            numericUpDownTPermitValue.Value = Convert.ToDecimal(ResConAlignData.MarginError.T);
            
            numericUpDownXOffsetValue.Value = Convert.ToDecimal(ResConAlignData.OffsetValue.X);
            numericUpDownYOffsetValue.Value = Convert.ToDecimal(ResConAlignData.OffsetValue.Y);
            numericUpDownTOffsetValue.Value = Convert.ToDecimal(ResConAlignData.OffsetValue.T);

            textBoxCurrentPosU.Text = ResConAlignData.UVWCurrentPos.U.ToString();
            textBoxCurrentPosV.Text = ResConAlignData.UVWCurrentPos.V.ToString();
            textBoxCurrentPosW.Text = ResConAlignData.UVWCurrentPos.W.ToString();
        }

        public AlignResultData GetResultConditionAlignData()
        {
            return ResConAlignData;
        }

        #region Read & Write ProjectItemParameter
        private XmlNodeList GetNodeList(string _XmlFilePath)
        {
            XmlNodeList _XmlNodeList = null;

            try
            {
                XmlDocument _XmlDocument = new XmlDocument();
                _XmlDocument.Load(_XmlFilePath);
                XmlElement _XmlRoot = _XmlDocument.DocumentElement;
                _XmlNodeList = _XmlRoot.ChildNodes;
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "GetNodeList Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _XmlNodeList = null;
            }

            return _XmlNodeList;
        }

        public bool ReadProjectItemParameter()
        {
            bool _Result = true;

            try
            {
                if (false == File.Exists(ProjectItemParameterFullPath))
                {
                    File.Create(ProjectItemParameterFullPath).Close();
                    WriteProjectItemParameter();
                    System.Threading.Thread.Sleep(100);
                }

                XmlNodeList _XmlNodeList = GetNodeList(ProjectItemParameterFullPath);
                if (null == _XmlNodeList) return false;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return false;
                    switch (_Node.Name)
                    {
                        #region AlignResult Parameter
                        case "AlignMarkDistance": ResConAlignData.AlignMarkDistance = Convert.ToDouble(_Node.InnerText); break;
                        case "AlignRotateCenterX": ResConAlignData.AlignRotateCenterX = Convert.ToDouble(_Node.InnerText); break;
                        case "AlignRotateCenterY": ResConAlignData.AlignRotateCenterY = Convert.ToDouble(_Node.InnerText); break;
                        case "UVWRadius": ResConAlignData.UVWRadius = Convert.ToDouble(_Node.InnerText); break;
                        case "PinJointPositionU": ResConAlignData.PinJointPosition.U = Convert.ToDouble(_Node.InnerText); break;
                        case "PinJointPositionV": ResConAlignData.PinJointPosition.V = Convert.ToDouble(_Node.InnerText); break;
                        case "PinJointPositionW": ResConAlignData.PinJointPosition.W = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorX": ResConAlignData.MarginError.X = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorY": ResConAlignData.MarginError.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorT": ResConAlignData.MarginError.T = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueX": ResConAlignData.OffsetValue.X = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueY": ResConAlignData.OffsetValue.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueT": ResConAlignData.OffsetValue.T = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginX": ResConAlignData.FirstCamOrigin.X = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginY": ResConAlignData.FirstCamOrigin.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginX": ResConAlignData.SecondCamOrigin.X = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginY": ResConAlignData.SecondCamOrigin.Y = Convert.ToDouble(_Node.InnerText); break;
                        #endregion
                    }
                }
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ReadProjectItemParameter Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        /// <summary>
        /// 프로젝트 아이템 별로 별도의 조건을 가져가는 경우 (Align 정밀도 등)
        /// </summary>
        /// <param name="_ID"></param>
        /// <param name="_RecipeName"></param>
        public void WriteProjectItemParameter()
        {
            XmlTextWriter _XmlWriter = new XmlTextWriter(ProjectItemParameterFullPath, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("ProjectItemParameter");
            {
                #region AlignResult Parameter
                _XmlWriter.WriteElementString("AlignMarkDistance", ResConAlignData.AlignMarkDistance.ToString());
                _XmlWriter.WriteElementString("AlignRotateCenterX", ResConAlignData.AlignRotateCenterX.ToString());
                _XmlWriter.WriteElementString("AlignRotateCenterY", ResConAlignData.AlignRotateCenterY.ToString());
                _XmlWriter.WriteElementString("UVWRadius", ResConAlignData.UVWRadius.ToString());
                _XmlWriter.WriteElementString("PinJointPositionU", ResConAlignData.PinJointPosition.U.ToString());
                _XmlWriter.WriteElementString("PinJointPositionV", ResConAlignData.PinJointPosition.V.ToString());
                _XmlWriter.WriteElementString("PinJointPositionW", ResConAlignData.PinJointPosition.W.ToString());
                _XmlWriter.WriteElementString("MarginErrorX", ResConAlignData.MarginError.X.ToString());
                _XmlWriter.WriteElementString("MarginErrorY", ResConAlignData.MarginError.Y.ToString());
                _XmlWriter.WriteElementString("MarginErrorT", ResConAlignData.MarginError.T.ToString());
                _XmlWriter.WriteElementString("OffsetValueX", ResConAlignData.OffsetValue.X.ToString());
                _XmlWriter.WriteElementString("OffsetValueY", ResConAlignData.OffsetValue.Y.ToString());
                _XmlWriter.WriteElementString("OffsetValueT", ResConAlignData.OffsetValue.T.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginX", ResConAlignData.FirstCamOrigin.X.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginY", ResConAlignData.FirstCamOrigin.Y.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginX", ResConAlignData.SecondCamOrigin.X.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginY", ResConAlignData.SecondCamOrigin.Y.ToString());
                #endregion
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write ProjectItemParameter
    }

    //LDH, 2019.10.29, Align 전용 변수
    public class AlignResultData
    {
        public double PI = 3.1415926535;
        public double PXResolution1 = 0.01432;
        public double PXResolution2 = 0.01432;

        public double UVWRadius = 0;

        public double AlignMarkDistance = 0;
        public double AlignRotateCenterX = 0;
        public double AlignRotateCenterY = 0;

        public AlignAxis PinJointPosition = new AlignAxis();
        public AlignAxis MarginError = new AlignAxis();
        public AlignAxis OffsetValue = new AlignAxis();
        public AlignAxis UVWCurrentPos = new AlignAxis();

        public PointD FirstCamOrigin = new PointD();      //First Camera Origin
        public PointD SecondCamOrigin = new PointD();     //Second Camera Origin
        public PointD FirstCamOriginTemp = new PointD();  //Second Camera Origin Temp
        public PointD SecondCamOriginTemp = new PointD();  //Second Camera Origin Temp

        public PointD MaskPosition = new PointD();

        public static PointD DirectionU = new PointD(1, 1);
        public static PointD DirectionV = new PointD(1, 1);
        public static PointD DirectionW = new PointD(1, 1);

        public static double CurrentStageAngle = 0;
        public static double LastDegree = 0;

        public PointD FirstCamMove = new PointD();
        public PointD SecondCamMove = new PointD();

        public double X_AXIS_MOTION_RESOLUTION = 1000;
        public double Y_AXIS_MOTION_RESOLUTION = 1000;
        public double T_AXIS_MOTION_RESOLUTION = 1000;  
    }

    public class AlignAxis
    {
        public double X;
        public double Y;
        public double T;
        public double U;
        public double V;
        public double W;
    }

    public class PointD
    {
        public double X;
        public double Y;

        public PointD()
        {
            X = 0;
            Y = 0;
        }

        public PointD(double _X, double _Y)
        {
            X = _X;
            Y = _Y;
        }
    }

    public static class AL_DEF
    {
        public const int CAL1 = 1;
        public const int CAL2 = 2;
    }
}
