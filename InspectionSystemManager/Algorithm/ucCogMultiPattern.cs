using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ParameterManager;
using LogMessageManager;
using KPDisplay;

using Cognex.VisionPro;

namespace InspectionSystemManager
{
    public partial class ucCogMultiPattern : UserControl
    {
        private CogMultiPatternAlgo CogMultiPatternAlgoRcp = new CogMultiPatternAlgo();
        private References ReferenceBackup = new References();

        private double ResolutionX = 0.005;
        private double ResolutionY = 0.005;
        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;

        Button[] btnNewArea;
        Button[] btnAdd;
        Button[] btnModify;
        Button[] btnFind;
        KPCogDisplayControl[] PatternDisplay; 

        private int SelectedPattern = 0;

        public delegate void DrawReferRegionHandler(CogRectangle _Region, double _OriginX, double _OriginY, CogColorConstants _Color);
        public event DrawReferRegionHandler DrawReferRegionEvent;

        public delegate void ReferenceActionHandler(eReferAction _ReferAction, int _Index = 0, bool _MultiFlag = true);
        public event ReferenceActionHandler ReferenceActionEvent;

        public delegate void ApplyMultiPatternValueHandler(CogMultiPatternAlgo _CogPatternAlgo, ref CogMultiPatternResult _CogPatternResult);
        public event ApplyMultiPatternValueHandler ApplyMultiPatternValueEvent;

        public ucCogMultiPattern()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            btnNewArea = new Button[2] { btnShowAreaTop, btnShowAreaBottom };
            btnAdd = new Button[2] { btnPatternAddTop, btnPatternAddBottom };
            btnModify = new Button[2] { btnPatternModifyTop, btnPatternModifyBottom };
            btnFind = new Button[2] { btnFindTop, btnFindBottom };
            PatternDisplay = new KPCogDisplayControl[2] { kpPatternDisplay, kpPatternDisplay1 };

            SetPatternButton(1, false);
        }

        private void SetPatternButton(int BtnNum, bool _EnableFlag)
        {
            btnNewArea[BtnNum].Enabled = _EnableFlag;
            btnAdd[BtnNum].Enabled = _EnableFlag;
            btnModify[BtnNum].Enabled = _EnableFlag;
            btnNewArea[BtnNum].Enabled = _EnableFlag;
        }

        public void SetAlgoRecipe(Object _Algorithm, double _BenchMarkOffsetX, double _BenchMarkOffsetY, double _ResolutionX, double _ResolutionY)
        {
            if (null == _Algorithm) return;

            CogMultiPatternAlgoRcp = _Algorithm as CogMultiPatternAlgo;
            ReferenceBackup.Clear();
            for (int iLoopCount = 0; iLoopCount < CogMultiPatternAlgoRcp.ReferenceInfoList.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = CogMultiPatternAlgoRcp.ReferenceInfoList[iLoopCount];
                ReferenceBackup.Add(_ReferInfo);
            }

            ResolutionX = _ResolutionX;
            ResolutionY = _ResolutionY;
            BenchMarkOffsetX = _BenchMarkOffsetX;
            BenchMarkOffsetY = _BenchMarkOffsetY;

            numericUpDownFindScore.Value = Convert.ToDecimal(CogMultiPatternAlgoRcp.MatchingScore);
            numericUpDownFindCount.Value = Convert.ToDecimal(CogMultiPatternAlgoRcp.MatchingCount);
            numericUpDownAngleLimit.Value = Convert.ToDecimal(CogMultiPatternAlgoRcp.MatchingAngle);
            txtBoxAngle.Text = CogMultiPatternAlgoRcp.TwoPointAngle.ToString("F2");
            //txtBoxAngle.Text = "0.0";

            if (CogMultiPatternAlgoRcp.ReferenceInfoList.Count > 0)
            {
                for (int iLoopCount = 0; iLoopCount < CogMultiPatternAlgoRcp.ReferenceInfoList.Count; iLoopCount++)
                {
                    ShowPatternImageArea(iLoopCount + 1);
                    ShowPatternImage(iLoopCount + 1);
                }
                
                SetPatternButton(1, true);
            }
            else ShowPatternImage(0);
        }

        #region Control Event
        private void btnShowArea_Click(object sender, EventArgs e)
        {
            int Num = Convert.ToInt32(((Button)sender).Tag);

            CogRectangle _Region = new CogRectangle();
            _Region.SetCenterWidthHeight(200, 200, 200, 200);

            var _DrawReferRegionEvent = DrawReferRegionEvent;
            _DrawReferRegionEvent?.Invoke(_Region, _Region.CenterX, _Region.CenterY, CogColorConstants.Cyan);
        }

        private void btnPatternAdd_Click(object sender, EventArgs e)
        {
            int Num = Convert.ToInt32(((Button)sender).Tag);

            if (CogMultiPatternAlgoRcp.ReferenceInfoList.Count >= Num + 1) { MessageBox.Show("이미 추가된 Pattern이 있습니다."); return; }

            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.ADD);

            SelectedPattern = Num;
            ShowPatternImage(Num + 1);
            ShowPatternImageArea(Num + 1);

            if (Num == 0) SetPatternButton(1, true);
        }

        private void btnPatternModify_Click(object sender, EventArgs e)
        {
            int Num = Convert.ToInt32(((Button)sender).Tag);
            SelectedPattern = Num;

            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.MODIFY, SelectedPattern);

            ShowPatternImage(SelectedPattern + 1);
            ShowPatternImageArea(SelectedPattern + 1);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            int Num = Convert.ToInt32(((Button)sender).Tag);
            SelectedPattern = Num;

            if (CogMultiPatternAlgoRcp.ReferenceInfoList.Count <= Num) { MessageBox.Show("등록된 Pattern이 없습니다."); return; }

            CogMultiPatternResult _CogMultiPatternResult = new CogMultiPatternResult();
            CogMultiPatternAlgo _CogMultiPatternAlgoRcp = new CogMultiPatternAlgo();
            _CogMultiPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            _CogMultiPatternAlgoRcp.MatchingCount = Convert.ToInt32(numericUpDownFindCount.Value);
            _CogMultiPatternAlgoRcp.MatchingAngle = Convert.ToDouble(numericUpDownAngleLimit.Value);

            _CogMultiPatternAlgoRcp.ReferenceInfoList = new References();

            int ReferenceCount = 1;
            if (SelectedPattern == -1) ReferenceCount = CogMultiPatternAlgoRcp.ReferenceInfoList.Count;

            for (int iLoopCount = 0; iLoopCount < ReferenceCount; iLoopCount++)
            {
                ReferenceInformation _ReferInfo = new ReferenceInformation();

                if (SelectedPattern != -1) _ReferInfo = CogMultiPatternAlgoRcp.ReferenceInfoList[SelectedPattern];
                else                       _ReferInfo = CogMultiPatternAlgoRcp.ReferenceInfoList[iLoopCount];

                _CogMultiPatternAlgoRcp.ReferenceInfoList.Add(_ReferInfo);
            }

            var _ApplyPatternMatchingValueEvent = ApplyMultiPatternValueEvent;
            _ApplyPatternMatchingValueEvent?.Invoke(_CogMultiPatternAlgoRcp, ref _CogMultiPatternResult);

            txtBoxAngle.Text = (_CogMultiPatternResult.IsGood) ? _CogMultiPatternResult.TwoPointAngle.ToString("N2") : "0.0";
        }
        #endregion Control Event

        public void SaveAlgoRecipe()
        {
            CogMultiPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            CogMultiPatternAlgoRcp.MatchingCount = Convert.ToInt32(numericUpDownFindCount.Value);
            CogMultiPatternAlgoRcp.MatchingAngle = Convert.ToDouble(numericUpDownAngleLimit.Value);
            CogMultiPatternAlgoRcp.PatternCount = CogMultiPatternAlgoRcp.ReferenceInfoList.Count;
            CogMultiPatternAlgoRcp.TwoPointAngle = Convert.ToDouble(txtBoxAngle.Text);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Teaching CogPattern SaveAlgoRecipe", CLogManager.LOG_LEVEL.MID);
        }

        private void ShowPatternImageArea(int _PatternNumber)
        {
            if (_PatternNumber <= 0) return;
            if (_PatternNumber > CogMultiPatternAlgoRcp.ReferenceInfoList.Count || CogMultiPatternAlgoRcp.ReferenceInfoList.Count == 0) return;

            _PatternNumber = _PatternNumber - 1;
            CogRectangle _Region = new CogRectangle();
            _Region.SetCenterWidthHeight(CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterX, CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterY, CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Width, CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Height);
            var _DrawReferRegionEvent = DrawReferRegionEvent;
            _DrawReferRegionEvent.Invoke(_Region,
                                        CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterX - CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].OriginPointOffsetX,
                                        CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].CenterY - CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].OriginPointOffsetY, CogColorConstants.Yellow);
        }

        private void ShowPatternImage(int _PatternNumber)
        {
            if (_PatternNumber <= 0)
            {
                for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
                {
                    PatternDisplay[iLoopCount].SetDisplayImage(null);
                }
                return;
            }
            if (_PatternNumber > CogMultiPatternAlgoRcp.ReferenceInfoList.Count || CogMultiPatternAlgoRcp.ReferenceInfoList.Count == 0) return;

            _PatternNumber = _PatternNumber - 1;
            PatternDisplay[_PatternNumber].SetDisplayImage((CogImage8Grey)CogMultiPatternAlgoRcp.ReferenceInfoList[_PatternNumber].Reference.GetTrainedPatternImage());
        }
    }
}
