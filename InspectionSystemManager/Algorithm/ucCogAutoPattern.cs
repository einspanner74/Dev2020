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

using Cognex.VisionPro;

namespace InspectionSystemManager
{
    public partial class ucCogAutoPattern : UserControl
    {
        private CogAutoPatternAlgo CogAutoPatternAlgoRcp = new CogAutoPatternAlgo();

        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;

        public delegate void DrawReferRegionHandler(CogRectangle _Region, double _OriginX, double _OriginY, CogColorConstants _Color);
        public event DrawReferRegionHandler DrawReferRegionEvent;

        public delegate void ReferenceActionHandler(eReferAction _ReferAction, int _Index = 0, bool MultiFlag = false);
        public event ReferenceActionHandler ReferenceActionEvent;

        public delegate void ApplyAutoPatternFindValueHandler(CogAutoPatternAlgo _CogAutoPatternAlgo, ref CogAutoPatternResult _CogAutoPatternResult);
        public event ApplyAutoPatternFindValueHandler ApplyAutoPatternFindValueEvent;

        public ucCogAutoPattern()
        {
            InitializeComponent();
        }

        #region Control Event
        private void btnPatternModify_Click(object sender, EventArgs e)
        {
            var _ReferenceActionEvent = ReferenceActionEvent;
            _ReferenceActionEvent?.Invoke(eReferAction.MODIFY);

            ShowPatternImage();
            ShowPatternImageArea();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            CogAutoPatternResult _CogAutoPatternResult = new CogAutoPatternResult();
            CogAutoPatternAlgo _CogAutoPatternAlgoRcp = new CogAutoPatternAlgo();
            _CogAutoPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            _CogAutoPatternAlgoRcp.MatchingCount = 1;

            _CogAutoPatternAlgoRcp.ReferenceInfoList = new References();
            for (int iLoopCount = 0; iLoopCount < CogAutoPatternAlgoRcp.ReferenceInfoList.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = new ReferenceInformation();
                _ReferInfo = CogAutoPatternAlgoRcp.ReferenceInfoList[iLoopCount];
                _CogAutoPatternAlgoRcp.ReferenceInfoList.Add(_ReferInfo);
            }

            var _ApplyAutoPatternFindValueEvent = ApplyAutoPatternFindValueEvent;
            _ApplyAutoPatternFindValueEvent?.Invoke(_CogAutoPatternAlgoRcp, ref _CogAutoPatternResult);
        }
        #endregion Control Event

        public void SetAlgoRecipe(Object _Algorithm, double _BenchMarkOffsetX, double _BenchMarkOffsetY, double _ResolutionX, double _ResolutionY)
        {
            if (null == _Algorithm) return;

            CogAutoPatternAlgoRcp = _Algorithm as CogAutoPatternAlgo;
            for (int iLoopCount = 0; iLoopCount < CogAutoPatternAlgoRcp.ReferenceInfoList.Count; ++iLoopCount)
            {
                ReferenceInformation _ReferInfo = CogAutoPatternAlgoRcp.ReferenceInfoList[iLoopCount];
            }
            
            BenchMarkOffsetX = _BenchMarkOffsetX;
            BenchMarkOffsetY = _BenchMarkOffsetY;

            numericUpDownFindScore.Value = Convert.ToDecimal(CogAutoPatternAlgoRcp.MatchingScore);
            numericUpDownThreshold.Value = Convert.ToDecimal(CogAutoPatternAlgoRcp.PatternThreshold);
            
            if (CogAutoPatternAlgoRcp.ReferenceInfoList.Count > 0)
            {
                ShowPatternImageArea();
                ShowPatternImage();
            }
        }

        public void SaveAlgoRecipe()
        {
            CogAutoPatternAlgoRcp.MatchingScore = Convert.ToDouble(numericUpDownFindScore.Value);
            CogAutoPatternAlgoRcp.MatchingCount = 1;
            CogAutoPatternAlgoRcp.PatternThreshold = Convert.ToInt32(numericUpDownThreshold.Value);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Teaching CogPattern SaveAlgoRecipe", CLogManager.LOG_LEVEL.MID);
        }

        private void ShowPatternImageArea()
        {
            if (CogAutoPatternAlgoRcp.ReferenceInfoList.Count == 0) return;
            
            CogRectangle _Region = new CogRectangle();
            _Region.SetCenterWidthHeight(CogAutoPatternAlgoRcp.ReferenceInfoList[0].CenterX, CogAutoPatternAlgoRcp.ReferenceInfoList[0].CenterY, CogAutoPatternAlgoRcp.ReferenceInfoList[0].Width, CogAutoPatternAlgoRcp.ReferenceInfoList[0].Height);
            var _DrawReferRegionEvent = DrawReferRegionEvent;
            _DrawReferRegionEvent.Invoke(_Region,
                                        CogAutoPatternAlgoRcp.ReferenceInfoList[0].CenterX - CogAutoPatternAlgoRcp.ReferenceInfoList[0].OriginPointOffsetX,
                                        CogAutoPatternAlgoRcp.ReferenceInfoList[0].CenterY - CogAutoPatternAlgoRcp.ReferenceInfoList[0].OriginPointOffsetY, CogColorConstants.Yellow);
        }

        private void ShowPatternImage()
        {
            if (CogAutoPatternAlgoRcp.ReferenceInfoList.Count <= 0) { kpPatternDisplay.SetDisplayImage(null); return; }
            if (CogAutoPatternAlgoRcp.ReferenceInfoList.Count == 0) return;

            kpPatternDisplay.SetDisplayImage((CogImage8Grey)CogAutoPatternAlgoRcp.ReferenceInfoList[0].Reference.GetTrainedPatternImage());
        }
    }
}
