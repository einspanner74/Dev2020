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

using LogMessageManager;

namespace KPVisionInspectionFramework
{
    public partial class ResultConditionAlign : Form
    {
        public bool IsShowResultConditionAlignWindow = false;
        string ProjectItemParameterFullPath;
        public delegate void CalibrationHandler(int _AlignDefine);
        public event CalibrationHandler CalibrationEvent;

        public ResultConditionAlign(int _StageID)
        {
            InitializeComponent();

            ProjectItemParameterFullPath = string.Format(@"D:\VisionInspectionData\Common\ProjectItemParameter{0}.Sys", _StageID);
            Initialize();
        }

        public bool Initialize()
        {
            bool _Result = true;

    		ReadProjectItemParameter();

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

        public void SetCondition()
        {

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

        private void btnCalculation_Click(object sender, EventArgs e)
        {
            var _CalibrationEvent = CalibrationEvent;
            _CalibrationEvent?.Invoke(AL_DEF.CAL1);
            WriteProjectItemParameter();
        }

        private void btnCalculation2_Click(object sender, EventArgs e)
        {
            var _CalibrationEvent = CalibrationEvent;
            _CalibrationEvent?.Invoke(AL_DEF.CAL2);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsShowResultConditionAlignWindow = false;
            this.Hide();
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
                        case "AlignMarkDistance": AlignResultData.AlignMarkDistance = Convert.ToDouble(_Node.InnerText); break;
                        case "AlignRotateCenterX": AlignResultData.AlignRotateCenterX = Convert.ToDouble(_Node.InnerText); break;
                        case "AlignRotateCenterY": AlignResultData.AlignRotateCenterY = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorX": AlignResultData.MarginError.X = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorY": AlignResultData.MarginError.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorT": AlignResultData.MarginError.T = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorU": AlignResultData.MarginError.U = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorV": AlignResultData.MarginError.V = Convert.ToDouble(_Node.InnerText); break;
                        case "MarginErrorW": AlignResultData.MarginError.W = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueX": AlignResultData.OffsetValue.X = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueY": AlignResultData.OffsetValue.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueT": AlignResultData.OffsetValue.T = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueU": AlignResultData.OffsetValue.U = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueV": AlignResultData.OffsetValue.V = Convert.ToDouble(_Node.InnerText); break;
                        case "OffsetValueW": AlignResultData.OffsetValue.W = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginX": AlignResultData.FirstCamOrigin.X = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginY": AlignResultData.FirstCamOrigin.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginX": AlignResultData.SecondCamOrigin.X = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginY": AlignResultData.SecondCamOrigin.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginTempX": AlignResultData.FirstCamOriginTemp.X = Convert.ToDouble(_Node.InnerText); break;
                        case "FirstCamOriginTempY": AlignResultData.FirstCamOriginTemp.Y = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginTempX": AlignResultData.SecondCamOriginTemp.X = Convert.ToDouble(_Node.InnerText); break;
                        case "SecondCamOriginTempY": AlignResultData.SecondCamOriginTemp.Y = Convert.ToDouble(_Node.InnerText); break;
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
                _XmlWriter.WriteElementString("AlignMarkDistance", AlignResultData.AlignMarkDistance.ToString());
                _XmlWriter.WriteElementString("AlignRotateCenterX", AlignResultData.AlignRotateCenterX.ToString());
                _XmlWriter.WriteElementString("AlignRotateCenterY", AlignResultData.AlignRotateCenterY.ToString());
                _XmlWriter.WriteElementString("MarginErrorX", AlignResultData.MarginError.X.ToString());
                _XmlWriter.WriteElementString("MarginErrorY", AlignResultData.MarginError.Y.ToString());
                _XmlWriter.WriteElementString("MarginErrorT", AlignResultData.MarginError.T.ToString());
                _XmlWriter.WriteElementString("MarginErrorU", AlignResultData.MarginError.U.ToString());
                _XmlWriter.WriteElementString("MarginErrorV", AlignResultData.MarginError.V.ToString());
                _XmlWriter.WriteElementString("MarginErrorW", AlignResultData.MarginError.W.ToString());
                _XmlWriter.WriteElementString("OffsetValueX", AlignResultData.OffsetValue.X.ToString());
                _XmlWriter.WriteElementString("OffsetValueY", AlignResultData.OffsetValue.Y.ToString());
                _XmlWriter.WriteElementString("OffsetValueT", AlignResultData.OffsetValue.T.ToString());
                _XmlWriter.WriteElementString("OffsetValueU", AlignResultData.OffsetValue.U.ToString());
                _XmlWriter.WriteElementString("OffsetValueV", AlignResultData.OffsetValue.V.ToString());
                _XmlWriter.WriteElementString("OffsetValueW", AlignResultData.OffsetValue.W.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginX", AlignResultData.FirstCamOrigin.X.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginY", AlignResultData.FirstCamOrigin.Y.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginX", AlignResultData.SecondCamOrigin.X.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginY", AlignResultData.SecondCamOrigin.Y.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginTempX", AlignResultData.FirstCamOriginTemp.X.ToString());
                _XmlWriter.WriteElementString("FirstCamOriginTempY", AlignResultData.FirstCamOriginTemp.Y.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginTempX", AlignResultData.SecondCamOriginTemp.X.ToString());
                _XmlWriter.WriteElementString("SecondCamOriginTempY", AlignResultData.SecondCamOriginTemp.Y.ToString());
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
        public const double PI = 3.1415926535;
        public const double PXResolution1 = 0.01432;
        public const double PXResolution2 = 0.01432;

        public static double AlignMarkDistance = 0;
        public static double AlignRotateCenterX = 0;
        public static double AlignRotateCenterY = 0;

        public static AlignAxis MarginError = new AlignAxis();
        public static AlignAxis OffsetValue = new AlignAxis();
        public static PointD FirstCamOrigin = new PointD();      //First Camera Origin
        public static PointD SecondCamOrigin = new PointD();     //Second Camera Origin
        public static PointD FirstCamOriginTemp = new PointD();  //Second Camera Origin Temp
        public static PointD SecondCamOriginTemp = new PointD();  //Second Camera Origin Temp

        public static double RealRadius = 0;
        public static double CenterToVAxisRealAngleU = 0;
        public static double CenterToVAxisRealAngleV = 0;
        public static double CenterToVAxisRealAngleW = 0;

        public static PointD DirectionU = new PointD(1, 1);
        public static PointD DirectionV = new PointD(1, 1);
        public static PointD DirectionW = new PointD(1, 1);

        public static AlignAxis CurrentPosition = new AlignAxis();
        public static double CurrentStageAngle = 0;
        public static double LastDegree = 0;

        public static double X_AXIS_MOTION_RESOLUTION = 0;
        public static double Y_AXIS_MOTION_RESOLUTION = 0;
        public static double T_AXIS_MOTION_RESOLUTION = 0;  
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
