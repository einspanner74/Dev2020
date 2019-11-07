using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Dimensioning;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    class InspectionMultiPattern
    {
        private CogPMAlignTool PatternProc;
        private CogPMAlignResults PatternResults;
        private CogAnglePointPointTool AnglePointProc;

        public InspectionMultiPattern()
        {
            PatternProc = new CogPMAlignTool();
            PatternResults = new CogPMAlignResults();
            AnglePointProc = new CogAnglePointPointTool();
        }

        public void Initialize()
        {

        }

        public void DeInitialize()
        {
            PatternProc.Dispose();
        }

        public bool Run(CogImage8Grey _SrcImage, CogRectangle _InspRegion, CogMultiPatternAlgo _CogMultiPatternAlgo, ref CogMultiPatternResult _CogMultiPatternResult)
        {
            bool _Result = true;

            PatternProc.RunParams.AcceptThreshold = _CogMultiPatternAlgo.MatchingScore / 100;
            PatternProc.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;
            PatternProc.RunParams.ZoneAngle.Low = _CogMultiPatternAlgo.MatchingAngle * -1;
            PatternProc.RunParams.ZoneAngle.High = _CogMultiPatternAlgo.MatchingAngle * 1;

            _CogMultiPatternResult.Score = new double[2];
            _CogMultiPatternResult.Scale = new double[2];
            _CogMultiPatternResult.Angle = new double[2];
            _CogMultiPatternResult.CenterX = new double[2];
            _CogMultiPatternResult.CenterY = new double[2];
            _CogMultiPatternResult.OriginPointX = new double[2];
            _CogMultiPatternResult.OriginPointY = new double[2];
            _CogMultiPatternResult.Width = new double[2];
            _CogMultiPatternResult.Height = new double[2];

            for (int iLoopCount = 0; iLoopCount < _CogMultiPatternAlgo.ReferenceInfoList.Count; ++iLoopCount)
            {
                if (false == PatternInspection(_SrcImage, _InspRegion, _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Reference)) continue;

                if (PatternResults != null && PatternResults.Count > 0)
                {
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Find Count : " + PatternResults.Count.ToString(), CLogManager.LOG_LEVEL.MID);

                    int TopScoreIndex = 0;

                    for (int jLoopCount = 0; jLoopCount < PatternResults.Count; ++jLoopCount)
                    {
                        if(PatternResults[TopScoreIndex].Score < PatternResults[jLoopCount].Score) TopScoreIndex = jLoopCount;   
                    }

                    _CogMultiPatternResult.Score[iLoopCount] = PatternResults[TopScoreIndex].Score;
                    _CogMultiPatternResult.Scale[iLoopCount] = PatternResults[TopScoreIndex].GetPose().Scaling;
                    _CogMultiPatternResult.Angle[iLoopCount] = PatternResults[TopScoreIndex].GetPose().Rotation;
                    _CogMultiPatternResult.OriginPointX[iLoopCount] = PatternResults[TopScoreIndex].GetPose().TranslationX;
                    _CogMultiPatternResult.OriginPointY[iLoopCount] = PatternResults[TopScoreIndex].GetPose().TranslationY;
                    _CogMultiPatternResult.CenterX[iLoopCount] = _CogMultiPatternResult.OriginPointX[iLoopCount] + _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetX;
                    _CogMultiPatternResult.CenterY[iLoopCount] = _CogMultiPatternResult.OriginPointY[iLoopCount] + _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].OriginPointOffsetY;
                    _CogMultiPatternResult.Width[iLoopCount] = _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Width;
                    _CogMultiPatternResult.Height[iLoopCount] = _CogMultiPatternAlgo.ReferenceInfoList[iLoopCount].Height;

                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Find Score : " + (_CogMultiPatternResult.Score[iLoopCount] * 100).ToString("F2"), CLogManager.LOG_LEVEL.MID);

                    _CogMultiPatternResult.FindCount++;
                }
                else
                {
                    _CogMultiPatternResult.IsGood = false;

                    _CogMultiPatternResult.Score[iLoopCount] = 0;
                    _CogMultiPatternResult.Scale[iLoopCount] = 0;
                    _CogMultiPatternResult.Angle[iLoopCount] = 0;
                    _CogMultiPatternResult.OriginPointX[iLoopCount] = _InspRegion.CenterX;
                    _CogMultiPatternResult.OriginPointY[iLoopCount] = _InspRegion.CenterY;
                    _CogMultiPatternResult.CenterX[iLoopCount] = _InspRegion.CenterX;
                    _CogMultiPatternResult.CenterY[iLoopCount] = _InspRegion.CenterY;
                    _CogMultiPatternResult.Width[iLoopCount] = _InspRegion.Width;
                    _CogMultiPatternResult.Height[iLoopCount] = _InspRegion.Height;

                    _Result = false;
                }
            }

            if (_CogMultiPatternResult.FindCount == 2)
            {
                double[] StartPoint = new double[2];
                double[] EndPoint = new double[2];

                StartPoint[0] = _CogMultiPatternResult.OriginPointX[0];
                StartPoint[1] = _CogMultiPatternResult.OriginPointY[0];
                EndPoint[0] = _CogMultiPatternResult.OriginPointX[1];
                EndPoint[1] = _CogMultiPatternResult.OriginPointY[1];

                if (false == AngleInspection(_SrcImage, _InspRegion, StartPoint, EndPoint, ref _CogMultiPatternResult.TwoPointAngle)) _Result = false;

                //LJH 2018.11.28 기존값을 기준으로 틀어준다.
                _CogMultiPatternResult.TwoPointAngle = _CogMultiPatternResult.TwoPointAngle - _CogMultiPatternAlgo.TwoPointAngle;
                _CogMultiPatternResult.IsGood = true;
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Result : " + (_CogMultiPatternResult.IsGood).ToString(), CLogManager.LOG_LEVEL.MID);

            return _Result;
        }

        public bool PatternInspection(CogImage8Grey _SrcImage, CogRectangle _Region, CogPMAlignPattern _Pattern)
        {
            bool _Result = true;

            try
            {
                PatternProc.InputImage = _SrcImage;
                PatternProc.SearchRegion = _Region;
                PatternProc.Pattern = _Pattern;
                PatternProc.Run();
                GetResult();
            }
            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionMultiPattern - Pattern Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        private bool AngleInspection(CogImage8Grey _SrcImage, CogRectangle _Region, double[] _StartPoint, double[] _EndPoint, ref double _TwoPointAngle)
        {
            bool _Result = true;

            try
            {
                AnglePointProc.InputImage = _SrcImage;
                AnglePointProc.StartX = _StartPoint[0];
                AnglePointProc.StartY = _StartPoint[1];
                AnglePointProc.EndX = _EndPoint[0];
                AnglePointProc.EndY = _EndPoint[1];
                AnglePointProc.Run();

                _TwoPointAngle = AnglePointProc.Angle * 180 / Math.PI;
            }
            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionMultiPattern - Angle Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        private void GetResult()
        {
            PatternResults = new CogPMAlignResults();
            PatternResults = PatternProc.Results;
        }

    }
}
