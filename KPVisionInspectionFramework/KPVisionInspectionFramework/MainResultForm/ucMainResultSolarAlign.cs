using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using CustomControl;
using ParameterManager;
using LogMessageManager;
using HistoryManager;

namespace KPVisionInspectionFramework
{
    public partial class ucMainResultSolarAlign : UserControl
    {
        private string[] HistoryParam;
        private string[] LastRecipeName;
                
        private bool[] InspCompleteFlag;
        private double[] FindPointX;
        private double[] FindPointY;

        TextBox[] TextBoxCamOriginX;
        TextBox[] TextBoxCamOriginY;

        private string LastResult;

        public delegate void PLCResultHandler(string[] _PLCResult);
        public event PLCResultHandler PLCResultEvent;

        private Thread ThreadInspCompleteCheck;
        private bool IsThreadInspCompleteCheckExit = false;
        private bool IsThreadInspCompleteCheckTrigger = false;

        ResultConditionAlign ResConAlignWnd;
        
        private object lockObject = new object();

        public ucMainResultSolarAlign(string[] _LastRecipeName, int _StageID)
        {
            LastRecipeName = new string[_LastRecipeName.Count()];
            SetLastRecipeName(_LastRecipeName);

            ResConAlignWnd = new ResultConditionAlign(_StageID);

            InitializeComponent();

            if (_StageID == 0) { gradientLabelTitle.Text = " Left Align Result"; }
            else { gradientLabelTitle.Text = " Right Align Result"; }

            FindPointX = new double[2];
            FindPointY = new double[2];
            InspCompleteFlag = new bool[2];

            for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
            {
                FindPointX[iLoopCount] = 0.0;
                FindPointY[iLoopCount] = 0.0;
                InspCompleteFlag[iLoopCount] = false;
            }

            TextBoxCamOriginX = new TextBox[2] { textBoxCam1X, textBoxCam2X };
            TextBoxCamOriginY = new TextBox[2] { textBoxCam1Y, textBoxCam2Y };

            ClearResultUI();

            ThreadInspCompleteCheck = new Thread(ThreadInspCompleteFunction);
            IsThreadInspCompleteCheckExit = false;
            IsThreadInspCompleteCheckTrigger = false;
            ThreadInspCompleteCheck.Start();

            //LJH, 2019.11.01, ResultCondition 설정
            ResConAlignWnd.Initialize(/*초기화값*/);
            ResConAlignWnd.SetCondition(/*ConditionValue*/);
            ResConAlignWnd.CalibrationEvent += new ResultConditionAlign.CalibrationHandler(CalibrationEventFunc);
        }

        public void SetLastRecipeName(string[] _LastRecipeName)
        {
            for (int iLoopCount = 0; iLoopCount < _LastRecipeName.Count(); iLoopCount++)
            {
                LastRecipeName[iLoopCount] = _LastRecipeName[iLoopCount];
            }
        }

        public void ClearResultParam(int _CamNum)
        {
            FindPointX[_CamNum] = 0;
            FindPointY[_CamNum] = 0;
            InspCompleteFlag[_CamNum] = false;
        }

        public void ClearResultUI()
        {
            for(int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
            {
                ControlInvoke.TextBoxText(TextBoxCamOriginX[iLoopCount], "0.000");
                ControlInvoke.TextBoxText(TextBoxCamOriginY[iLoopCount], "0.000");
            }

            ControlInvoke.TextBoxText(textBoxAlignX, "0.000");
            ControlInvoke.TextBoxText(textBoxAlignY, "0.000");
            ControlInvoke.TextBoxText(textBoxAlignT, "0.000");
            ControlInvoke.TextBoxText(textBoxAlignU, "0.000");
            ControlInvoke.TextBoxText(textBoxAlignV, "0.000");
            ControlInvoke.TextBoxText(textBoxAlignW, "0.000");

            ControlInvoke.GradientLabelText(gradientLabelFindResult, "-", Color.LightGreen);
            ControlInvoke.GradientLabelColor(gradientLabelFindResult, Color.White, Color.White);

            ControlInvoke.GradientLabelText(gradientLabelAlignResult, "-", Color.LightGreen);
            ControlInvoke.GradientLabelColor(gradientLabelAlignResult, Color.White, Color.White);
        }

        private void SetGradientLabelResult(GradientLabel _GradientLabelResult, bool _ResultFlag)
        {
            if (_ResultFlag)
            {
                ControlInvoke.GradientLabelText(_GradientLabelResult, "OK", Color.White);
                ControlInvoke.GradientLabelColor(_GradientLabelResult, Color.DarkGreen, Color.FromArgb(0, 44, 0));
            }
            else
            {
                ControlInvoke.GradientLabelText(_GradientLabelResult, "NG", Color.White);
                ControlInvoke.GradientLabelColor(_GradientLabelResult, Color.Maroon, Color.FromArgb(49, 0, 0));
            }
        }

        private void GetAlignData(ref double _SendAlignDataX, ref double _SendAlignDataY, ref double _SendAlignDataT)
        {
            //double _PointX1 = FinalResultDatas[0].ResultPositionX;
            //double _PointY1 = FinalResultDatas[0].ResultPositionY;
            //double _PointX2 = FinalResultDatas[1].ResultPositionX;
            //double _PointY2 = FinalResultDatas[1].ResultPositionY;

            //...
            double _MarkLength = AlignResultData.AlignMarkDistance;
            double _AlignRotateCenterX = AlignResultData.AlignRotateCenterX;
            double _AlignRotateCenterY = AlignResultData.AlignRotateCenterY;

            //Teaching 한 CenterX
            //double _OriginCenterX1 = FinalResultDatas[0].OriginPositionX;
            //double _OriginCenterY1 = FinalResultDatas[0].OriginPositionY;
            //double _OriginCenterX2 = FinalResultDatas[1].OriginPositionX;
            //double _OriginCenterY2 = FinalResultDatas[1].OriginPositionY;

            double _OriginCenterX1 = AlignResultData.FirstCamOrigin.X;
            double _OriginCenterY1 = AlignResultData.FirstCamOrigin.Y;
            double _OriginCenterX2 = AlignResultData.SecondCamOrigin.X;
            double _OriginCenterY2 = AlignResultData.SecondCamOrigin.Y;

            double _FindCenterX1 = FindPointX[0];
            double _FindCenterY1 = FindPointY[0];
            double _FindCenterX2 = FindPointX[1];
            double _FindCenterY2 = FindPointY[1];

            //X,Y값 동일하게하여 직접 지정
            //double _ResolutionX1 = ParamManager.InspSysManagerParam[0].ResolutionX / 1000;
            //double _ResolutionY1 = ParamManager.InspSysManagerParam[0].ResolutionY / 1000;
            //double _ResolutionX2 = ParamManager.InspSysManagerParam[1].ResolutionX / 1000;
            //double _ResolutionY2 = ParamManager.InspSysManagerParam[1].ResolutionY / 1000;

            //Origin으로 등록한 좌표와 Align 하기 위해 찾은 좌표와의 Gap 연산
            double _ValX1 = (_FindCenterX1 - _OriginCenterX1) * AlignResultData.PXResolution1;
            double _PreOffsetX1 = _ValX1;

            double _ValY1 = -(_FindCenterY1 - _OriginCenterY1) * AlignResultData.PXResolution1;
            double _PreOffsetY1 = _ValY1;

            double _ValX2 = (_FindCenterX2 - _OriginCenterX2) * AlignResultData.PXResolution2;
            double _PreOffsetX2 = _ValX2;

            double _ValY2 = -(_FindCenterY2 - _OriginCenterY2) * AlignResultData.PXResolution2;
            double _PreOffsetY2 = _ValY2;

            double _ResultAngle = Math.Asin((_PreOffsetY2 - _PreOffsetY1) / _MarkLength) * (180 / AlignResultData.PI);
            double _NewCenterXY = Math.Sqrt((Math.Pow(_AlignRotateCenterX, 2) + Math.Pow(_AlignRotateCenterY, 2)));

            double _ThetaC = Math.Atan2(_AlignRotateCenterY, _AlignRotateCenterX) * (180 / AlignResultData.PI);
            double _ThetaD = 90 - Math.Abs(_ResultAngle);
            double _LineA = (_NewCenterXY * Math.Cos(_ThetaD * (AlignResultData.PI / 180)));
            //_LineA = _LineA * 2;

            double _ThetaA = 180 - Math.Abs(_ThetaC) - Math.Abs(_ThetaD);
            double _ThetaB = 90 - _ThetaA;
            double _TempYD = _LineA * Math.Sin(_ThetaA * (AlignResultData.PI / 180));
            double _TempXD = _LineA * Math.Cos(_ThetaA * (AlignResultData.PI / 180));

            double _ResultX = 0, _ResultY = 0;
            if (_ResultAngle <= 0)
            {
                _ResultX = _PreOffsetX1 - Math.Abs(_TempXD);
                _ResultY = _PreOffsetY1 - Math.Abs(_TempYD);
            }
            else
            {
                _ResultX = _PreOffsetX1 + Math.Abs(_TempXD);
                _ResultY = _PreOffsetY1 + Math.Abs(_TempYD);
            }

            //LJH 20170713 이동량에 Offset 값 적용
            _SendAlignDataX = _ResultX * AlignResultData.X_AXIS_MOTION_RESOLUTION;
            _SendAlignDataY = _ResultY * AlignResultData.Y_AXIS_MOTION_RESOLUTION;
            _SendAlignDataT = _ResultAngle * AlignResultData.T_AXIS_MOTION_RESOLUTION;
        }

        public void SetResultData(SendResultParameter _ResultParam)
        {
            if (CParameterManager.SystemMode == eSysMode.AUTO_MODE) ;
            else if (CParameterManager.SystemMode == eSysMode.CAL1) ;

            lock (lockObject)
            {
                if (_ResultParam.ID != 4 && _ResultParam.ID != 5)
                {
                    //Pattern을 사용 하는 경우
                    /*
                    for (int iLoopCount = 0; iLoopCount < _ResultParam.SendResultList.Count(); iLoopCount++)
                    {
                        var _ResultData = _ResultParam.SendResultList[iLoopCount] as SendMeasureResult;

                        int _CamNum;

                        if (_ResultParam.ID < 2) _CamNum = 0;
                        else _CamNum = 1;

                        ClearResultParam(_CamNum);

                        FindPointX[_CamNum] = _ResultData.PointX;
                        FindPointY[_CamNum] = _ResultData.PointY;
                        InspCompleteFlag[_CamNum] = true;

                        ControlInvoke.TextBoxText(TextBoxCamOriginX[_CamNum], FindPointX[_CamNum].ToString("F3"));
                        ControlInvoke.TextBoxText(TextBoxCamOriginY[_CamNum], FindPointY[_CamNum].ToString("F3"));

                        if (!InspCompleteFlag.Contains(false))
                        {
                            IsThreadInspCompleteCheckTrigger = true;
                        }
                    }
                    */

                    //Find Line을 사용하는 경우
                    if (_ResultParam.SendResultList.Length == 2)
                    {
                        var _ResultData = _ResultParam.SendResultList[1] as SendMeasureResult;

                        if (_ResultData != null)
                        {
                            int _CamNum;

                            if (_ResultParam.ID < 2) _CamNum = 0;
                            else _CamNum = 1;

                            ClearResultParam(_CamNum);

                            FindPointX[_CamNum] = _ResultData.IntersectionX;
                            FindPointY[_CamNum] = _ResultData.IntersectionY;
                            InspCompleteFlag[_CamNum] = true;

                            ControlInvoke.TextBoxText(TextBoxCamOriginX[_CamNum], FindPointX[_CamNum].ToString("F3"));
                            ControlInvoke.TextBoxText(TextBoxCamOriginY[_CamNum], FindPointY[_CamNum].ToString("F3"));

                            if (!InspCompleteFlag.Contains(false))
                            {
                                IsThreadInspCompleteCheckTrigger = true;
                            }
                        }
                    }
                }
            }
        }

        public void SetResultAlignData()
        {
            AlignAxis _SendAlignData = new AlignAxis();
            _SendAlignData.X = 0.0;
            _SendAlignData.Y = 0.0;
            _SendAlignData.T = 0.0;

            AlignAxis _SendAlignOffsetData = new AlignAxis();
            _SendAlignOffsetData.X = 0.0;
            _SendAlignOffsetData.Y = 0.0;
            _SendAlignOffsetData.T = 0.0;

            GetAlignData(ref _SendAlignData.X, ref _SendAlignData.Y, ref _SendAlignData.T);

            ControlInvoke.TextBoxText(textBoxAlignX, _SendAlignData.X.ToString());
            ControlInvoke.TextBoxText(textBoxAlignY, _SendAlignData.Y.ToString());
            ControlInvoke.TextBoxText(textBoxAlignT, _SendAlignData.T.ToString());

            //LDH, 2019.10.29, 이동량에 Offset 값 적용
            _SendAlignOffsetData.X = (_SendAlignData.X + AlignResultData.OffsetValue.X) * AlignResultData.X_AXIS_MOTION_RESOLUTION;
            _SendAlignOffsetData.Y = (_SendAlignData.Y + AlignResultData.OffsetValue.Y) * AlignResultData.Y_AXIS_MOTION_RESOLUTION;
            _SendAlignOffsetData.T = (_SendAlignData.T + AlignResultData.OffsetValue.T) * AlignResultData.T_AXIS_MOTION_RESOLUTION;

            //UVW 이동식계산
        }

        private void ThreadInspCompleteFunction()
        {
            try
            {
                while (false == IsThreadInspCompleteCheckExit)
                {
                    if (true == IsThreadInspCompleteCheckTrigger)
                    {
                        IsThreadInspCompleteCheckTrigger = false;

                        SetResultAlignData();
                        Thread.Sleep(50);
                    }

                    Thread.Sleep(10);
                }
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadInspectionFunction Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void btnAlignSetting_Click(object sender, EventArgs e)
        {
            if (false == ResConAlignWnd.IsShowResultConditionAlignWindow)   ResConAlignWnd.ShowResultConditionAlignWindow();
            else                                                            ResConAlignWnd.SetResultConditionAlignWindowTopMost(true);
        }
		
		private void CalibrationEventFunc(int _AlignDefine)
        {
            if (_AlignDefine == AL_DEF.CAL1)
            {
                MessageBox.Show("CAL1");
            }

            else if (_AlignDefine == AL_DEF.CAL2)
            {
                MessageBox.Show("CAL2");
            }
        }
    }
}
