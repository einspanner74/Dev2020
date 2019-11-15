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
        private bool LastResultFlag;

        private bool CalModeFlag;
        private int CalModeInspCnt;
        private int VisionStageNum;

        public delegate void PLCResultHandler(object _Command, string _PLCResult);
        public event PLCResultHandler PLCResultEvent;

        public delegate void ResultWndHandler(object _Command);
        public event ResultWndHandler SendResultWndEvent;

        private Thread ThreadInspCompleteCheck;
        private bool IsThreadInspCompleteCheckExit = false;
        private bool IsThreadInspCompleteCheckTrigger = false;

        private ResultConditionAlign ResConAlignWnd;
        private UVW UVWProcess;

        private AlignAxis UVWCurrentPosition = new AlignAxis();
        
        private object lockObject = new object();

        public ucMainResultSolarAlign(string[] _LastRecipeName, int _StageID)
        {
            LastRecipeName = new string[_LastRecipeName.Count()];
            SetLastRecipeName(_LastRecipeName);

            ResConAlignWnd = new ResultConditionAlign(_StageID);

            UVWProcess = new UVW();
            UVWProcess.Initialize(1,1,1);

            InitializeComponent();

            if (_StageID == 0) { gradientLabelTitle.Text = " Left Align Result"; }
            else { gradientLabelTitle.Text = " Right Align Result"; }

            FindPointX = new double[2];
            FindPointY = new double[2];
            InspCompleteFlag = new bool[2];

            for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
            {
                ClearResultParam(iLoopCount);
            }

            SetCalMode(false);

            TextBoxCamOriginX = new TextBox[2] { textBoxCam1X, textBoxCam2X };
            TextBoxCamOriginY = new TextBox[2] { textBoxCam1Y, textBoxCam2Y };

            LastResultFlag = true;
            ClearResultUI();

            ThreadInspCompleteCheck = new Thread(ThreadInspCompleteFunction);
            IsThreadInspCompleteCheckExit = false;
            IsThreadInspCompleteCheckTrigger = false;
            ThreadInspCompleteCheck.Start();

            //LJH, 2019.11.01, ResultCondition 설정
            ResConAlignWnd.Initialize(/*초기화값*/);
        }

        public void SetLastRecipeName(string[] _LastRecipeName)
        {
            for (int iLoopCount = 0; iLoopCount < _LastRecipeName.Count(); iLoopCount++)
            {
                LastRecipeName[iLoopCount] = _LastRecipeName[iLoopCount];
            }
        }

        public void SetCalMode(bool _ModeFlag = true)
        {
            CalModeFlag = _ModeFlag;
            CalModeInspCnt = 0;
        }

        public void SetCurrentPosition(AlignAxis _PositionValue)
        {
            UVWCurrentPosition.U = _PositionValue.U / 1000;
            UVWCurrentPosition.V = _PositionValue.V / 1000;
            UVWCurrentPosition.W = _PositionValue.W / 1000;
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

        private void GetAlignData(AlignResultData _AlignResultData, ref double _SendAlignDataX, ref double _SendAlignDataY, ref double _SendAlignDataT)
        {
            //double _PointX1 = FinalResultDatas[0].ResultPositionX;
            //double _PointY1 = FinalResultDatas[0].ResultPositionY;
            //double _PointX2 = FinalResultDatas[1].ResultPositionX;
            //double _PointY2 = FinalResultDatas[1].ResultPositionY;
                       
            //...
            double _MarkLength = _AlignResultData.AlignMarkDistance;
            double _AlignRotateCenterX = _AlignResultData.AlignRotateCenterX;
            double _AlignRotateCenterY = _AlignResultData.AlignRotateCenterY;

            //Teaching 한 CenterX
            //double _OriginCenterX1 = FinalResultDatas[0].OriginPositionX;
            //double _OriginCenterY1 = FinalResultDatas[0].OriginPositionY;
            //double _OriginCenterX2 = FinalResultDatas[1].OriginPositionX;
            //double _OriginCenterY2 = FinalResultDatas[1].OriginPositionY;

            double _OriginCenterX1 = _AlignResultData.FirstCamOrigin.X;
            double _OriginCenterY1 = _AlignResultData.FirstCamOrigin.Y;
            double _OriginCenterX2 = _AlignResultData.SecondCamOrigin.X;
            double _OriginCenterY2 = _AlignResultData.SecondCamOrigin.Y;

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
            double _ValX1 = (_FindCenterX1 - _OriginCenterX1) * _AlignResultData.PXResolution1;
            double _PreOffsetX1 = _ValX1;

            double _ValY1 = -(_FindCenterY1 - _OriginCenterY1) * _AlignResultData.PXResolution1;
            double _PreOffsetY1 = _ValY1;

            double _ValX2 = (_FindCenterX2 - _OriginCenterX2) * _AlignResultData.PXResolution2;
            double _PreOffsetX2 = _ValX2;

            double _ValY2 = -(_FindCenterY2 - _OriginCenterY2) * _AlignResultData.PXResolution2;
            double _PreOffsetY2 = _ValY2;

            double _ResultAngle = Math.Asin((_PreOffsetY2 - _PreOffsetY1) / _MarkLength) * (180 / _AlignResultData.PI);
            double _NewCenterXY = Math.Sqrt((Math.Pow(_AlignRotateCenterX, 2) + Math.Pow(_AlignRotateCenterY, 2)));

            double _ThetaC = Math.Atan2(_AlignRotateCenterY, _AlignRotateCenterX) * (180 / _AlignResultData.PI);
            double _ThetaD = 90 - Math.Abs(_ResultAngle);
            double _LineA = (_NewCenterXY * Math.Cos(_ThetaD * (_AlignResultData.PI / 180)));
            //_LineA = _LineA * 2;

            double _ThetaA = 180 - Math.Abs(_ThetaC) - Math.Abs(_ThetaD);
            double _ThetaB = 90 - _ThetaA;
            double _TempYD = _LineA * Math.Sin(_ThetaA * (_AlignResultData.PI / 180));
            double _TempXD = _LineA * Math.Cos(_ThetaA * (_AlignResultData.PI / 180));

            double _ResultX = 0, _ResultY = 0;
            if (_ResultAngle <= 0)
            {
                //_ResultX = _PreOffsetX1 - Math.Abs(_TempXD);
                _ResultX = _PreOffsetX1 + Math.Abs(_TempXD);
                _ResultY = _PreOffsetY1 - Math.Abs(_TempYD);
            }
            else
            {
                //_ResultX = _PreOffsetX1 + Math.Abs(_TempXD);
                _ResultX = _PreOffsetX1 - Math.Abs(_TempXD);
                _ResultY = _PreOffsetY1 + Math.Abs(_TempYD);
            }

            // 이동량에 Offset 값 적용 전
            _SendAlignDataX = _ResultX;
            _SendAlignDataY = _ResultY;
            _SendAlignDataT = _ResultAngle;
        }

        public void SetResultData(SendResultParameter _ResultParam)
        {
            lock (lockObject)
            {
                if (_ResultParam.ID != 4 && _ResultParam.ID != 5)
                {
                    int _CamNum;                    

                    if (_ResultParam.ID < 2)
                    {
                        if (_ResultParam.ID == 0) VisionStageNum = 1;
                        else VisionStageNum = 2;
                        _CamNum = 0;
                    }
                    else _CamNum = 1;
                    ClearResultParam(_CamNum);

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
                            FindPointX[_CamNum] = _ResultData.IntersectionX;
                            FindPointY[_CamNum] = _ResultData.IntersectionY;
                            
                            ControlInvoke.TextBoxText(TextBoxCamOriginX[_CamNum], FindPointX[_CamNum].ToString("F3"));
                            ControlInvoke.TextBoxText(TextBoxCamOriginY[_CamNum], FindPointY[_CamNum].ToString("F3"));

                            //CalMode 일때 
                            if (CalModeFlag)
                            {
                                switch (CalModeInspCnt)
                                {
                                    case 0: ResConAlignWnd.SetCalFirstPosition(_CamNum, FindPointX[_CamNum], FindPointY[_CamNum]); break;
                                    case 1: ResConAlignWnd.SetCalSecondPosition(_CamNum, FindPointX[_CamNum], FindPointY[_CamNum]); break;
                                }
                            }

                            LastResultFlag &= _ResultParam.IsGood;
                        }
                        else
                        {
                            LastResultFlag = false;
                        }
                    }

                    InspCompleteFlag[_CamNum] = true;

                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, string.Format("SetResultData : {0} = {1}", _CamNum, LastResultFlag.ToString()), CLogManager.LOG_LEVEL.LOW);

                    if (!InspCompleteFlag.Contains(false))
                    {
                        SetGradientLabelResult(gradientLabelFindResult, LastResultFlag);

                        IsThreadInspCompleteCheckTrigger = true;
                    }
                }
            }
        }

        public void SetResultAlignData()
        {
            bool AlignResultFlag = true;
            int LastAlignResult = 0;

            if (LastResultFlag)
            {
                AlignAxis _SendAlignData = new AlignAxis();
                _SendAlignData.X = 0.0;
                _SendAlignData.Y = 0.0;
                _SendAlignData.T = 0.0;

                AlignAxis _SendAlignOffsetData = new AlignAxis();
                _SendAlignOffsetData.X = 0.0;
                _SendAlignOffsetData.Y = 0.0;
                _SendAlignOffsetData.T = 0.0;

                AlignAxis _GetCalUVWData = new AlignAxis();
                _GetCalUVWData.U = 0.0;
                _GetCalUVWData.V = 0.0;
                _GetCalUVWData.W = 0.0;

                AlignAxis _GetCalOffsetUVWData = new AlignAxis();
                _GetCalOffsetUVWData.U = 0.0;
                _GetCalOffsetUVWData.V = 0.0;
                _GetCalOffsetUVWData.W = 0.0;

                ControlInvoke.TextBoxText(textBoxAlignX, "-");
                ControlInvoke.TextBoxText(textBoxAlignY, "-");
                ControlInvoke.TextBoxText(textBoxAlignT, "-");

                ControlInvoke.TextBoxText(textBoxAlignU, "-");
                ControlInvoke.TextBoxText(textBoxAlignV, "-");
                ControlInvoke.TextBoxText(textBoxAlignW, "-");

                AlignResultData AlignResultTemp = ResConAlignWnd.GetResultConditionAlignData();

                if (!CalModeFlag)
                {
                    GetAlignData(AlignResultTemp, ref _SendAlignData.X, ref _SendAlignData.Y, ref _SendAlignData.T);

                    ControlInvoke.TextBoxText(textBoxAlignX, _SendAlignData.X.ToString("F3"));
                    ControlInvoke.TextBoxText(textBoxAlignY, _SendAlignData.Y.ToString("F3"));
                    ControlInvoke.TextBoxText(textBoxAlignT, _SendAlignData.T.ToString("F3"));

                    //LDH, 2019.10.29, 이동량에 Offset 값 적용
                    _SendAlignOffsetData.X = (_SendAlignData.X + AlignResultTemp.OffsetValue.X);
                    _SendAlignOffsetData.Y = (_SendAlignData.Y + AlignResultTemp.OffsetValue.Y);
                    _SendAlignOffsetData.T = (_SendAlignData.T + AlignResultTemp.OffsetValue.T);

                    if (AlignResultTemp.MarginError.X < Math.Abs(_SendAlignData.X) || AlignResultTemp.MarginError.Y < Math.Abs(_SendAlignData.Y) || AlignResultTemp.MarginError.T < Math.Abs(_SendAlignData.T))
                    {
                        //LDH, 2019.11.08, Spec Out일 때만 UVW 이동식계산
                        UVWProcess.GetUVW(_SendAlignData.X, _SendAlignData.Y, _SendAlignData.T, ref _GetCalUVWData.U, ref _GetCalUVWData.V, ref _GetCalUVWData.W);
                        //UVWProcess.GetUVW(0, 0, -0.5, ref _GetCalUVWData.U, ref _GetCalUVWData.V, ref _GetCalUVWData.W);

                        AlignResultFlag = false;
                        LastAlignResult = VTPDefine.MOVE;
                    }
                    else
                    {
                        UVWProcess.GetUVW(0, 0, 0, ref _GetCalUVWData.U, ref _GetCalUVWData.V, ref _GetCalUVWData.W);
                        LastAlignResult = VTPDefine.OK;
                    }

                    //LDH, 2019.11.06, UVW Offset 식 계산
                    UVWProcess.GetUVW(_SendAlignOffsetData.X, _SendAlignOffsetData.Y, _SendAlignOffsetData.T, ref _GetCalOffsetUVWData.U, ref _GetCalOffsetUVWData.V, ref _GetCalOffsetUVWData.W);
                    //UVWProcess.GetUVW(0.11, 0.01, 0.01, ref _GetCalOffsetUVWData.U, ref _GetCalOffsetUVWData.V, ref _GetCalOffsetUVWData.W);

                    //ControlInvoke.TextBoxText(textBoxAlignU, (_GetCalUVWData.U + UVWCurrentPosition.U).ToString("F3"));
                    //ControlInvoke.TextBoxText(textBoxAlignV, (_GetCalUVWData.V + UVWCurrentPosition.V).ToString("F3"));
                    //ControlInvoke.TextBoxText(textBoxAlignW, (_GetCalUVWData.W + UVWCurrentPosition.W).ToString("F3"));
                    ControlInvoke.TextBoxText(textBoxAlignU, (_GetCalUVWData.U + UVWCurrentPosition.U).ToString("F3"));
                    ControlInvoke.TextBoxText(textBoxAlignV, (_GetCalUVWData.V + UVWCurrentPosition.V).ToString("F3"));
                    ControlInvoke.TextBoxText(textBoxAlignW, (_GetCalUVWData.W + UVWCurrentPosition.W).ToString("F3"));
                }
                else
                {
                    //LDH, Cal Mode일 경우에 임의로 0.5도 회전
                    UVWProcess.GetUVW(0, 0, 1, ref _GetCalUVWData.U, ref _GetCalUVWData.V, ref _GetCalUVWData.W);
                    UVWProcess.GetUVW(0, 0, 0, ref _GetCalOffsetUVWData.U, ref _GetCalOffsetUVWData.V, ref _GetCalOffsetUVWData.W);
                    LastAlignResult = VTPDefine.MOVE;
                }

                if (VisionStageNum == 1)
                {
                    PLCResultEvent(VTPAddr.V1_ALIGN_POS_U, ((_GetCalUVWData.U + UVWCurrentPosition.U) * AlignResultTemp.X_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V1_ALIGN_POS_V, ((_GetCalUVWData.V + UVWCurrentPosition.V) * AlignResultTemp.Y_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V1_ALIGN_POS_W, ((_GetCalUVWData.W + UVWCurrentPosition.W) * AlignResultTemp.T_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V1_LAST_POS_U, ((_GetCalOffsetUVWData.U + UVWCurrentPosition.U) * AlignResultTemp.X_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V1_LAST_POS_V, ((_GetCalOffsetUVWData.V + UVWCurrentPosition.V) * AlignResultTemp.Y_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V1_LAST_POS_W, ((_GetCalOffsetUVWData.W + UVWCurrentPosition.W) * AlignResultTemp.T_AXIS_MOTION_RESOLUTION).ToString());
                }
                else if (VisionStageNum == 2)
                {
                    PLCResultEvent(VTPAddr.V2_ALIGN_POS_U, ((_GetCalUVWData.U + UVWCurrentPosition.U) * AlignResultTemp.X_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V2_ALIGN_POS_V, ((_GetCalUVWData.V + UVWCurrentPosition.V) * AlignResultTemp.Y_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V2_ALIGN_POS_W, ((_GetCalUVWData.W + UVWCurrentPosition.W) * AlignResultTemp.T_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V2_LAST_POS_U, ((_GetCalOffsetUVWData.U + UVWCurrentPosition.U) * AlignResultTemp.X_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V2_LAST_POS_V, ((_GetCalOffsetUVWData.V + UVWCurrentPosition.V) * AlignResultTemp.Y_AXIS_MOTION_RESOLUTION).ToString());
                    PLCResultEvent(VTPAddr.V2_LAST_POS_W, ((_GetCalOffsetUVWData.W + UVWCurrentPosition.W) * AlignResultTemp.T_AXIS_MOTION_RESOLUTION).ToString());
                }

                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, string.Format("SetResultAlignData : U = {0}, V = {1}, W = {2}", _GetCalUVWData.U, _GetCalUVWData.V, _GetCalUVWData.W), CLogManager.LOG_LEVEL.LOW);
            }
            else
            {
                LastAlignResult = VTPDefine.NG;
                AlignResultFlag = false;
            }

            switch (VisionStageNum)
            {
                case 1: PLCResultEvent(VTPAddr.V1_INSP_RESULT, LastAlignResult.ToString()); break;
                case 2: PLCResultEvent(VTPAddr.V2_INSP_RESULT, LastAlignResult.ToString()); break;
            }

            SetGradientLabelResult(gradientLabelAlignResult, AlignResultFlag);

            for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++) InspCompleteFlag[iLoopCount] = false;

            if (CalModeFlag)
            {
                if (CalModeInspCnt == 1) { SendResultWndEvent(eSysMode.MANUAL_MODE); CalModeFlag = false; }
                else CalModeInspCnt++;
            }

            LastResultFlag = true;
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
            if (false == ResConAlignWnd.IsShowResultConditionAlignWindow) ResConAlignWnd.ShowResultConditionAlignWindow();
            else ResConAlignWnd.SetResultConditionAlignWindowTopMost(true);
        }
    }
}
