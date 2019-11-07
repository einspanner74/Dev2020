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

using Cognex.VisionPro;

namespace InspectionSystemManager
{
    public partial class ucCogPattern : UserControl
    {
        private CogPatternAlgo CogPatternAlgoRcp = new CogPatternAlgo();
        private References ReferenceBackup = new References();

        private int CurrentPattern = 0;
        private double ResolutionX = 0.005;
        private double ResolutionY = 0.005;
        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;

        public delegate void DrawReferRegionHandler(CogRectangle _Region, double _OriginX, double _OriginY, CogColorConstants _Color);
        public event DrawReferRegionHandler DrawReferRegionEvent;

        public delegate void ReferenceActionHandler(eReferAction _ReferAction, int _Index = 0, bool MultiFlag = false);
        public event ReferenceActionHandler ReferenceActionEvent;

        public delegate void ApplyPatternMatchingValueHandler(CogPatternAlgo _CogPatternAlgo, ref CogPatternResult _CogPatternResult);
        public event ApplyPatternMatchingValueHandler ApplyPatternMatchingValueEvent;

        #region Initialize & DeInitialize
        public ucCogPattern()
        {
            InitializeComponent();
        }

        public void Initialize()
        {

        }

        public void DeInitialize()
        {

        }
        #endregion Initialize & DeInitialize

        #region Control Event
        private void btnPrev_Click(object sender, EventArgs e)
        {
            CurrentPattern--;
            //if (CurrentPattern <= 0) { CurrentPattern = 1; return; }
            if (CurrentPattern <= 0) CurrentPattern++;

            ShowPatternImage(CurrentPattern);
            ShowPatternImageArea(CurrentPattern);
            UpdatePatternCount();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            CurrentPattern++;
            if (CurrentPattern > CogPatternAlgoRcp.ReferenceInfoList.Count) { CurrentPattern = CogPatternAlgoRcp.ReferenceInfoList.Count; return; }
            //if (CurrentPattern > CogPatternAlgoRcp.ReferenceInfoList.Count) CurrentPattern--; 

            ShowPatternImage(CurrentPattern);
            ShowPatternImageArea(CurrentPattern);
            UpdatePatternCount();
        }

        private void btnShowArea_Click(object sender, EventArgs e)
        {
            CogRectangle _Region = new CogRectangle();
            _Region.SetCenterWidthHeight(200, 200, 200, 200);

            var _DrawReferRegionEvent = DrawReferRegionEvent;
            _DrawReferRegionEvent?.Invoke(_Region, _Region.CenterX, _Region.CenterY, CogColorConstants.Cyan);
        }

        private void btnPatternAdd_Click(object sender, EventArgs e)
        {
            if (CogPatternAlgoRcp.ReferenceInfoList.Count == 1) { MessageBox.Show("이미 추가된 패턴이 있습니다"); return; }

            //LDH, 2018.11.28, CurrentPattern 숫자 에러 방지용
            int PattenrCnt = CogPatternAlgoRcp.ReferenceInfoList.Count;

            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.ADD);

            if(CogPatternAlgoRcp.ReferenceInfoList.Count > PattenrCnt) CurrentPattern++;

            ShowPatternImage(CurrentPattern);
            ShowPatternImageArea(CurrentPattern);
            UpdatePatternCount();
        }

        private void btnPatternDel_Click(object sender, EventArgs e)
        {
            if (CogPatternAlgoRcp.ReferenceInfoList.Count < 1) { MessageBox.Show("삭제할 패턴이 없습니다."); return; }

            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.DEL, CurrentPattern - 1);

            CurrentPattern--;
            if (CurrentPattern <= 0 && CogPatternAlgoRcp.ReferenceInfoList.Count > 0) CurrentPattern = 1;
            ShowPatternImage(CurrentPattern);
            ShowPatternImageArea(CurrentPattern);
            UpdatePatternCount();
        }

        private void btnPatternModify_Click(object sender, EventArgs e)
        {
            if (CogPatternAlgoRcp.ReferenceInfoList.Count < 1) { MessageBox.Show("먼저 패턴을 추가하세요."); return; }

            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.MODIFY, CurrentPattern - 1);

            ShowPatternImage(CurrentPattern);
            ShowPatternImageArea(CurrentPattern);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            CogPatternResult _CogPatternResult = new CogPatternResult();
            CogPatternAlgo _CogPatternAlgoRcp = new CogPatternAlgo();
            _CogPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            _CogPatternAlgoRcp.MatchingCount = Convert.ToInt32(numericUpDownFindCount.Value);
            _CogPatternAlgoRcp.MatchingAngle = Convert.ToDouble(numericUpDownAngleLimit.Value);
            _CogPatternAlgoRcp.IsShift = Convert.ToBoolean(checkBoxShift.Checked);
            _CogPatternAlgoRcp.AllowedShiftX = Convert.ToDouble(numericUpDownAllowedShiftX.Value);
            _CogPatternAlgoRcp.AllowedShiftY = Convert.ToDouble(numericUpDownAllowedShiftY.Value);
            _CogPatternAlgoRcp.PatternCount = CogPatternAlgoRcp.ReferenceInfoList.Count;

            _CogPatternAlgoRcp.ReferenceInfoList = new References();
            for (int iLoopCount = 0; iLoopCount < CogPatternAlgoRcp.ReferenceInfoList.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = new ReferenceInformation();
                _ReferInfo = CogPatternAlgoRcp.ReferenceInfoList[iLoopCount];
                _CogPatternAlgoRcp.ReferenceInfoList.Add(_ReferInfo);
            }

            var _ApplyPatternMatchingValueEvent = ApplyPatternMatchingValueEvent;
            _ApplyPatternMatchingValueEvent?.Invoke(_CogPatternAlgoRcp, ref _CogPatternResult);
        }
        #endregion Control Event

        public void SetAlgoRecipe(Object _Algorithm, double _BenchMarkOffsetX, double _BenchMarkOffsetY, double _ResolutionX, double _ResolutionY)
        {
            if (null == _Algorithm) return;
            CurrentPattern = 0;

            CogPatternAlgoRcp = _Algorithm as CogPatternAlgo;
            ReferenceBackup.Clear();
            for (int iLoopCount = 0; iLoopCount < CogPatternAlgoRcp.ReferenceInfoList.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = CogPatternAlgoRcp.ReferenceInfoList[iLoopCount];
                ReferenceBackup.Add(_ReferInfo);
            }

            ResolutionX = _ResolutionX;
            ResolutionY = _ResolutionY;
            BenchMarkOffsetX = _BenchMarkOffsetX;
            BenchMarkOffsetY = _BenchMarkOffsetY;

            numericUpDownFindScore.Value = Convert.ToDecimal(CogPatternAlgoRcp.MatchingScore);
            numericUpDownFindCount.Value = Convert.ToDecimal(CogPatternAlgoRcp.MatchingCount);
            numericUpDownAngleLimit.Value = Convert.ToDecimal(CogPatternAlgoRcp.MatchingAngle);
            numericUpDownAllowedShiftX.Value = Convert.ToDecimal(CogPatternAlgoRcp.AllowedShiftX);
            numericUpDownAllowedShiftY.Value = Convert.ToDecimal(CogPatternAlgoRcp.AllowedShiftY);
            checkBoxShift.Checked = CogPatternAlgoRcp.IsShift;
            labelPatternCount.Text = CogPatternAlgoRcp.ReferenceInfoList.Count.ToString();

            CogPatternAlgoRcp.PatternCount = CogPatternAlgoRcp.ReferenceInfoList.Count;
            if (CogPatternAlgoRcp.ReferenceInfoList.Count > 0)
            {
                if (CurrentPattern == 0) CurrentPattern = 1;
                ShowPatternImageArea(CurrentPattern);
                ShowPatternImage(CurrentPattern);
                UpdatePatternCount();
            }
            else ShowPatternImage(0);
        }

        public void CancelAlgoRecipe()
        {
            CogPatternAlgoRcp.ReferenceInfoList.Clear();
            for (int iLoopCount = 0; iLoopCount < ReferenceBackup.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = ReferenceBackup[iLoopCount];
                CogPatternAlgoRcp.ReferenceInfoList.Add(_ReferInfo);
            }
        }

        public void SaveAlgoRecipe()
        {
            CogPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            CogPatternAlgoRcp.MatchingCount = Convert.ToInt32(numericUpDownFindCount.Value);
            CogPatternAlgoRcp.MatchingAngle = Convert.ToDouble(numericUpDownAngleLimit.Value);
            CogPatternAlgoRcp.IsShift = Convert.ToBoolean(checkBoxShift.Checked);
            CogPatternAlgoRcp.AllowedShiftX = Convert.ToDouble(numericUpDownAllowedShiftX.Value);
            CogPatternAlgoRcp.AllowedShiftY = Convert.ToDouble(numericUpDownAllowedShiftY.Value);
            CogPatternAlgoRcp.PatternCount = CogPatternAlgoRcp.ReferenceInfoList.Count;

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Teaching CogPattern SaveAlgoRecipe", CLogManager.LOG_LEVEL.MID);
        }

        private void ShowPatternImageArea(int _PatternNumber)
        {
            if (_PatternNumber <= 0) return;
            if (_PatternNumber > CogPatternAlgoRcp.ReferenceInfoList.Count || CogPatternAlgoRcp.ReferenceInfoList.Count == 0) return;

            _PatternNumber = _PatternNumber - 1;
            CogRectangle _Region = new CogRectangle();
            _Region.SetCenterWidthHeight(CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterX, CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterY, CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Width, CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Height);
            var _DrawReferRegionEvent = DrawReferRegionEvent;
            _DrawReferRegionEvent.Invoke(_Region, 
                                        CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterX - CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].OriginPointOffsetX, 
                                        CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterY - CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].OriginPointOffsetY, CogColorConstants.Yellow);
        }

        private void ShowPatternImage(int _PatternNumber)
        {
            if (_PatternNumber <= 0) { kpPatternDisplay.SetDisplayImage(null); return; }
            if (_PatternNumber > CogPatternAlgoRcp.ReferenceInfoList.Count || CogPatternAlgoRcp.ReferenceInfoList.Count == 0) return;

            _PatternNumber = _PatternNumber - 1;
            kpPatternDisplay.SetDisplayImage((CogImage8Grey)CogPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Reference.GetTrainedPatternImage());
        }

        private void UpdatePatternCount()
        {
            string _CurrentPatternString = string.Format($"{CurrentPattern}/{CogPatternAlgoRcp.ReferenceInfoList.Count}");
            labelPatternCount.Text = _CurrentPatternString;
        }
    }
}
