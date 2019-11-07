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
    public partial class ucMainResultNavien : UserControl
    {
        private DateTime CycleStartTime;
        public DateTime InspStartTime
        {
            set { CycleStartTime = value; }
        }

        #region Count & Yield Variable

        private bool AutoModeFlag = false;

        #endregion Count & Yield Variable

        double PXResolution = 0.01432;

        private string[] LastRecipeName;

        string[] GridViewHeaderName;

        string ProductCode = "";

        string[] CSVData = new string[22];
        string[] CSVHeader;

        bool[] ResultUseFlag;
        bool[] InspCompleteFlag;
        bool[] LastIsGoodFlag;

        GradientLabel[] GradientLabelResult;

        public delegate void ScreenshotHandler(string ScreenshotImagePath, Size ScreenshotSize);
        public event ScreenshotHandler ScreenshotEvent;

        public delegate void DIOResultHandler(bool _LastResult);
        public event DIOResultHandler DIOResultEvent;

        public delegate bool RecipeChangeHandler(int _ID, string _RecipeCode);
        public event RecipeChangeHandler RecipeChangeEvent;

        private object lockObject = new object();

        public ucMainResultNavien(string[] _LastRecipeName)
        {
            InitializeComponent();

            LastRecipeName = new string[_LastRecipeName.Count()];
            SetLastRecipeName(LastRecipeName);

            GridViewHeaderName = new string[] { "Num", "Min", "Max", "Length" };
            CSVHeader = new string[22] { "바코드번호", "검사일자", "검사시작시작", "CT(초)", "검사여부",
                                         "오리피스1", "오리피스1 상한 Spec", "오리피스1 하한 Spec", "오리피스1 결과",
                                         "오리피스2", "오리피스2 상한 Spec", "오리피스2 하한 Spec", "오리피스2 결과",
                                         "오리피스3", "오리피스3 상한 Spec", "오리피스3 하한 Spec", "오리피스3 결과",
                                         "오리피스4", "오리피스4 상한 Spec", "오리피스4 하한 Spec", "오리피스4 결과", "종합판정"};
            GradientLabelResult = new GradientLabel[] { gradientLabelResult1, gradientLabelResult2, gradientLabelResult3, gradientLabelResult4, gradientLabelTotalResult };
           
            SetGridViewHeader(dataGridViewResult, GridViewHeaderName.Count(), GridViewHeaderName);
            SetGridViewRowInit(dataGridViewResult, 4);
            dataGridViewResult.ClearSelection();

            LastIsGoodFlag = new bool[2];
            for (int iLoopCount = 0; iLoopCount < LastIsGoodFlag.Count(); iLoopCount++) { LastIsGoodFlag[iLoopCount] = true; }

            ResultUseFlag = new bool[GradientLabelResult.Count() - 1];
            for (int iLoopCount = 0; iLoopCount < ResultUseFlag.Count(); iLoopCount++) { ResultUseFlag[iLoopCount] = true; }

            InspCompleteFlag = new bool[2];
            for (int iLoopCount = 0; iLoopCount <2; iLoopCount++) { InspCompleteFlag[iLoopCount] = false; }
        }

        #region Initialize & Clear
        public void SetLOTNum(string[] _ProductInfo)
        {
            textBoxKITCode.Text = _ProductInfo[0];
            textBoxProduct.Text = _ProductInfo[1];
        }

        private void SetGridViewHeader(DataGridView _GridView, int _HeaderCount, string[] _HeaderName)
        {
            _GridView.ColumnCount = _HeaderCount;

            for (int iLoopCount = 0; iLoopCount < _HeaderCount; iLoopCount++)
            {
                _GridView.Columns[iLoopCount].Name = _HeaderName[iLoopCount];

                if (iLoopCount == 0) _GridView.Columns[iLoopCount].Width = 50;
                else _GridView.Columns[iLoopCount].Width = 115;
            }

            _GridView.Refresh();
        }

        private void SetGridViewRowInit(DataGridView _GridView, int _RowCount)
        {
            for (int iLoopCount = 0; iLoopCount < _RowCount; iLoopCount++)
            {
                _GridView.Rows.Add();
                _GridView.Rows[iLoopCount].Cells[0].Value = iLoopCount + 1;
            }

            _GridView.Refresh();
        }

        public void ClearResult(string _GridViewNum)
        {
            if (_GridViewNum == "") _GridViewNum = "0";

            char[] _GridViewNumArr = _GridViewNum.ToCharArray();
            
            if(_GridViewNumArr.Count() == 1)
            {
                ClearGridView(int.Parse(_GridViewNumArr[0].ToString()) - 1);
            }
            else
            {
                ClearGridView(-1);
                SetUseResultFlag(_GridViewNumArr);

                for (int iLoopCount = 0; iLoopCount < CSVData.Count(); iLoopCount++)
                {
                    CSVData[iLoopCount] = null;
                }
            }
        }

        private void ClearGridView(int _RowIndex)
        {            
            if (_RowIndex != -1)
            {
                for (int jLoopCount = 1; jLoopCount < dataGridViewResult.ColumnCount; jLoopCount++)
                {
                    dataGridViewResult.Rows[_RowIndex].Cells[jLoopCount].Value = "-";
                }

                ControlInvoke.GridViewRowsColor(dataGridViewResult, _RowIndex, Color.FromKnownColor(KnownColor.Window), Color.Black);
            }
            else
            {
                for(int iLoopCount = 0; iLoopCount < 2; iLoopCount++) LastIsGoodFlag[iLoopCount] = true;

                for (int iLoopCount = 0; iLoopCount < dataGridViewResult.RowCount; iLoopCount++)
                {
                    for (int jLoopCount = 1; jLoopCount < dataGridViewResult.ColumnCount; jLoopCount++)
                    {
                        dataGridViewResult.Rows[iLoopCount].Cells[jLoopCount].Value = "-";
                    }

                    ControlInvoke.GridViewRowsColor(dataGridViewResult, iLoopCount, Color.FromKnownColor(KnownColor.Window), Color.Black);
                }
            }
        }

        private void ClearDataArr()
        {
            //검사완료 Flag Arr Clear
            for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
            {
                InspCompleteFlag[iLoopCount] = false;
            }

            //CSV Data Arr Clear
            for (int iLoopCount = 0; iLoopCount < CSVData.Count(); iLoopCount++)
            {
                CSVData[iLoopCount] = null;
            }
        }

        private void ClearBarcode()
        {
            if (textBoxBarcode.InvokeRequired)
            {
                textBoxBarcode.Invoke(new MethodInvoker(delegate () { textBoxBarcode.Clear(); }));
            }
            else
            {
                textBoxBarcode.Clear();
            }
        }
        #endregion Initialize & Clear

        #region Control Event
        private void dataGridViewResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewResult.ClearSelection();
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            ProductCode = textBoxBarcode.Text;

            if (ProductCode.Length > 17)
            {
                for (int iLoopCount = 0; iLoopCount < LastRecipeName.Count(); iLoopCount++)
                {
                    bool _Result = RecipeChangeEvent(iLoopCount, ProductCode.Substring(0, 5));

                    if (!_Result) { ClearBarcode(); break; }
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                panelLeft.Focus();
            }
        }

        private void gradientLabelResult_DoubleClick(object sender, EventArgs e)
        {
            int _Tag = Convert.ToInt32(((GradientLabel)sender).Tag);

            ResultUseFlag[_Tag] = !ResultUseFlag[_Tag];
            SetGradientLabelUse(_Tag);
        }
        #endregion Control Event

        public void SetAutoMode(bool _AutoModeFlag)
        {
            AutoModeFlag = _AutoModeFlag;
        }

        public bool GetBarcodeStatus()
        {
            bool _Result = false;
            if (textBoxBarcode.Text != "") _Result = true;

            return _Result;
        }

        private void SetUseResultFlag(char[] _UseResultFlag)
        {
            int _FlagTemp = 0;
            for(int iLoopCount = 0; iLoopCount < ResultUseFlag.Count(); iLoopCount++)
            {
                try   { _FlagTemp = Convert.ToInt32(_UseResultFlag[iLoopCount].ToString()); }
                catch { _FlagTemp = 1; }

                ResultUseFlag[iLoopCount] = Convert.ToBoolean(_FlagTemp);
                SetGradientLabelUse(iLoopCount);
            }
        }

        public void GetUseResultFlag(out string _UseResultFlag)
        {
            string UseFlagTemp = "";

            for (int iLoopCount = 0; iLoopCount < ResultUseFlag.Count(); iLoopCount++)
            {
                UseFlagTemp = UseFlagTemp + Convert.ToInt16(ResultUseFlag[iLoopCount]);
            }

            _UseResultFlag = UseFlagTemp;
        }

        private void SetGradientLabelUse(int _ResultNum)
        {
            if (ResultUseFlag[_ResultNum])
            {
                ControlInvoke.GradientLabelColor(GradientLabelResult[_ResultNum], Color.White, Color.White);
                ControlInvoke.GradientLabelText(GradientLabelResult[_ResultNum], "-", Color.Black);
            }
            else
            {
                ControlInvoke.GradientLabelColor(GradientLabelResult[_ResultNum], Color.White, Color.DarkGray);
                ControlInvoke.GradientLabelText(GradientLabelResult[_ResultNum], "X", Color.Black);
            }
        }

        private void SetGradientLabelResult(int _ResultIndex, bool _ResultFlag)
        {
            if (_ResultFlag)
            {
                ControlInvoke.GradientLabelText(GradientLabelResult[_ResultIndex], "OK", Color.White);
                ControlInvoke.GradientLabelColor(GradientLabelResult[_ResultIndex], Color.DarkGreen, Color.FromArgb(0, 44, 0));
            }
            else
            {
                ControlInvoke.GradientLabelText(GradientLabelResult[_ResultIndex], "NG", Color.White);
                ControlInvoke.GradientLabelColor(GradientLabelResult[_ResultIndex], Color.Maroon, Color.FromArgb(49, 0, 0));
            }
        }

        public void SetLastRecipeName(string[] _LastRecipeName)
        {
            for (int iLoopCount = 0; iLoopCount < _LastRecipeName.Count(); iLoopCount++)
            {
                LastRecipeName[iLoopCount] = _LastRecipeName[iLoopCount];
            }
        }

        public void SetResult(SendResultParameter _ResultParam)
        {
            //lock
            SetResultData(_ResultParam);
        }

        private void WriteCSVFile()
        {
            CSVManagerStringArr SaveCSVControl = new CSVManagerStringArr();

            DateTime dateTime = DateTime.Now;
            string SaveCSVFilePath = @"D:\VisionInspectionData\NavienInspection\CSVData";
            if (false == Directory.Exists(SaveCSVFilePath)) Directory.CreateDirectory(SaveCSVFilePath);
            SaveCSVFilePath = String.Format("{0}\\{1:D4}\\{2:D2}", SaveCSVFilePath, dateTime.Year, dateTime.Month);
            if (false == Directory.Exists(SaveCSVFilePath)) Directory.CreateDirectory(SaveCSVFilePath);
            SaveCSVFilePath = String.Format("{0}\\VisionData_{1:D4}{2:D2}{3:D2}.csv", SaveCSVFilePath, dateTime.Year, dateTime.Month, dateTime.Day);

            CSVData[0] = ProductCode;
            CSVData[1] = String.Format("{0:D4}{1:D2}{2:D2}", dateTime.Year, dateTime.Month, dateTime.Day);
            CSVData[2] = String.Format("{0:D2}{1:D2}{2:D2}{3:D3}", CycleStartTime.Hour, CycleStartTime.Minute, CycleStartTime.Second, CycleStartTime.Millisecond);

            //LDH, 2019.09.17, CycleTime 계산
            double CycleTime = 0.0;
            TimeSpan _TimeSpan = dateTime - CycleStartTime;
                        
            if (_TimeSpan.Minutes != 0) CycleTime = Convert.ToDouble(_TimeSpan.Minutes * 60);
            CycleTime = CycleTime + Convert.ToDouble(_TimeSpan.Seconds) + (Convert.ToDouble(_TimeSpan.Milliseconds) / 1000);
            CSVData[3] = String.Format("{0:F3}", CycleTime);

            string _UseResultFlag = "";
            GetUseResultFlag(out _UseResultFlag);
            CSVData[4] = _UseResultFlag;

            SaveCSVControl.SaveStringArrAll(CSVHeader, CSVData, SaveCSVFilePath);


            //LDH, 209.09.17, MES용 CSVData 따로 저장
            string SaveMESFilePath = @"D:\VisionInspectionData\NavienInspection\MESData";
            if (false == Directory.Exists(SaveMESFilePath)) Directory.CreateDirectory(SaveMESFilePath);
            SaveMESFilePath = String.Format("{0}\\VisionMESData_{1:D4}{2:D2}{3:D2}{4:D2}{5:D2}{6:D2}{7:D3}.csv", SaveMESFilePath, dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            SaveCSVControl.SaveStringArrAll(CSVHeader, CSVData, SaveMESFilePath);
        }

        private void CalculateDiameter(double[] _FoundPointX, double[] _FoundPointY, ref List<double> _DiameterResultList)
        {
            int _Cnt = _FoundPointX.Count() / 2;
            
            for (int iLoopCount = 0; iLoopCount < _Cnt; iLoopCount++)
            {
                if (_FoundPointX[iLoopCount] != 0 && _FoundPointX[iLoopCount + _Cnt] != 0)
                {
                    double _DiameterTemp = 0.0;
                    double _distanceX = _FoundPointX[iLoopCount + _Cnt] - _FoundPointX[iLoopCount];
                    double _distanceY = _FoundPointY[iLoopCount + _Cnt] - _FoundPointY[iLoopCount];

                    _DiameterTemp = Math.Sqrt(Math.Pow(_distanceX, 2) + Math.Pow(_distanceY, 2));

                    _DiameterResultList.Add(_DiameterTemp);
                }
            }

            _DiameterResultList.Sort();
        }

        public void SetResultData(SendResultParameter _ResultParam)
        {
            lock (lockObject)
            {
                bool[] LastResultFlag = new bool[GradientLabelResult.Count() - 1];

                if (_ResultParam.ID == 0)
                {
                    for (int ResultCnt = 0; ResultCnt < 3; ResultCnt++)
                    {
                        LastResultFlag[ResultCnt] = true;
                        ClearResult((ResultCnt + 1).ToString());
                    }                    
                }
                else
                {
                    LastResultFlag[3] = true;
                    ClearResult("4");
                }

                LastIsGoodFlag[_ResultParam.ID] = true;

                if (!InspCompleteFlag.Contains(true)) ClearDataArr();

                for (int iLoopCount = 0; iLoopCount < _ResultParam.SendResultList.Count(); iLoopCount++)
                {
                    var _ResultData = _ResultParam.SendResultList[iLoopCount] as SendMeasureResult;
                    int _ResultIndex = _ResultData.NGAreaNum - 1;

                    InspCompleteFlag[_ResultParam.ID] = true;

                    if (ResultUseFlag[_ResultIndex])
                    {
                        if (_ResultParam.AlgoTypeList[iLoopCount] == eAlgoType.C_ELLIPSE)
                        {
                            List<double> _DiameterResultList = new List<double>();
                            double _DiameterMin = 0.0;
                            double _DiameterMax = 0.0;
                            double _DiameterEvg = 0.0;

                            CalculateDiameter(_ResultData.CaliperPointX, _ResultData.CaliperPointY, ref _DiameterResultList);

                            if (_DiameterResultList.Count > 2)
                            {
                                _DiameterMin = _DiameterResultList[1] * PXResolution;
                                _DiameterMax = _DiameterResultList[_DiameterResultList.Count - 1] * PXResolution;

                                if (_DiameterMin < _ResultData.DiameterMinAlgo) { _DiameterEvg = _DiameterMin; _ResultData.IsGoodAlgo = false; }
                                else if (_DiameterMax > _ResultData.DiameterMaxAlgo) { _DiameterEvg = _DiameterMax; _ResultData.IsGoodAlgo = false; }
                                else
                                {
                                    double _SumDiameter = 0.0;

                                    for (int ListLoopCnt = 1; ListLoopCnt < _DiameterResultList.Count - 1; ListLoopCnt++)
                                    {
                                        _SumDiameter = _SumDiameter + _DiameterResultList[ListLoopCnt];
                                    }

                                    _DiameterEvg = _SumDiameter / (_DiameterResultList.Count - 2) * PXResolution;
                                }

                                CSVData[(_ResultIndex * 4) + 6] = _ResultData.DiameterMinAlgo.ToString();
                                CSVData[(_ResultIndex * 4) + 7] = _ResultData.DiameterMaxAlgo.ToString();
                            }

                            ControlInvoke.GridViewCellText(dataGridViewResult, _ResultIndex, 3, string.Format("{0:F3}", _DiameterEvg));
                        }

                        LastResultFlag[_ResultIndex] &= _ResultData.IsGoodAlgo;

                        if (!_ResultData.IsGoodAlgo)
                        {
                            CSVData[(_ResultIndex * 4) + 8] = "NG";
                            ControlInvoke.GridViewRowsColor(dataGridViewResult, _ResultIndex, Color.Maroon, Color.White);
                        }
                        else
                        {
                            CSVData[(_ResultIndex * 4) + 8] = "OK";
                            ControlInvoke.GridViewRowsColor(dataGridViewResult, _ResultIndex, Color.FromKnownColor(KnownColor.Window), Color.Black);
                        }

                        SetGradientLabelResult(_ResultIndex, LastResultFlag[_ResultIndex]);

                        LastIsGoodFlag[_ResultParam.ID] &= LastResultFlag[_ResultIndex];
                    }
                }

                //for (int jLoopCount = 0; jLoopCount < LastResultFlag.Count(); jLoopCount++)
                //{
                //    if (ResultUseFlag[jLoopCount])
                //    {
                //        switch (LastResultFlag[jLoopCount])
                //        {
                //            case true:
                //                {
                //                    ControlInvoke.GradientLabelText(GradientLabelResult[jLoopCount], "OK", Color.White);
                //                    ControlInvoke.GradientLabelColor(GradientLabelResult[jLoopCount], Color.DarkGreen, Color.FromArgb(0, 44, 0));
                //                }
                //                break;
                //            case false:
                //                {
                //                    ControlInvoke.GradientLabelText(GradientLabelResult[jLoopCount], "NG", Color.White);
                //                    ControlInvoke.GradientLabelColor(GradientLabelResult[jLoopCount], Color.Maroon, Color.FromArgb(49, 0, 0));
                //                }
                //                break;
                //        }

                //        LastIsGoodFlag &= LastResultFlag[jLoopCount];
                //    }
                //}

                //오리피스 Result Data
                CSVData[5] = dataGridViewResult[3, 0].Value.ToString();
                CSVData[9] = dataGridViewResult[3, 1].Value.ToString();
                CSVData[13] = dataGridViewResult[3, 2].Value.ToString();
                CSVData[17] = dataGridViewResult[3, 3].Value.ToString();
                
                if (CParameterManager.SystemMode == eSysMode.AUTO_MODE && !InspCompleteFlag.Contains(false))
                {
                    if (textBoxBarcode.Text != "") WriteCSVFile();

                    bool LastResult = true;
                    for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++) LastResult &= LastIsGoodFlag[iLoopCount];

                    if (LastResult) CSVData[21] = "OK";
                    else            CSVData[21] = "NG";

                    SetGradientLabelResult(4, LastResult);

                    if (LastResult) DIOResultEvent(true);
                    else            DIOResultEvent(false);
                    CParameterManager.SystemMode = eSysMode.MANUAL_MODE;

                    ClearDataArr();
                    ClearBarcode();
                }

                if (CParameterManager.SystemMode != eSysMode.AUTO_MODE) InspCompleteFlag[_ResultParam.ID] = false;

                dataGridViewResult.ClearSelection();
            }
        }
    }
}
