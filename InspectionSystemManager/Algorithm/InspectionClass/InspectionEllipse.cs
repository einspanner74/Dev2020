using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Blob;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    class InspectionEllipse
    {
        private CogFindEllipseTool      FindEllipseProc;
        private CogFindEllipseResults   FindEllipseResults;

        private double EllipseCenterOffsetX;
        private double EllipseCenterOffsetY;

        public InspectionEllipse()
        {
            FindEllipseProc = new CogFindEllipseTool();
            FindEllipseResults = new CogFindEllipseResults();
        }

        public void DeInitialize()
        {

        }

        public bool Run(CogImage8Grey _SrcImage, CogRectangle _InspRegion, CogEllipseAlgo _CogEllipseAlgo, ref CogEllipseResult _CogEllipseResult, double _OffsetX = 0, double _OffsetY = 0, int _NgNumber = 0)
        {
            bool _Result = true;

            #region Caliper Center XY 구하기 -> Blob으로 Center 위치 Search
            #region GetAutoThresholdValue
            CogHistogramTool _HistoTool = new CogHistogramTool();
            _HistoTool.InputImage = _SrcImage;
            _HistoTool.Region = _InspRegion;
            _HistoTool.Run();

            int[] _HistoValue = _HistoTool.Result.GetHistogram();
            double _TotSize = _InspRegion.Width * _InspRegion.Height;
            double _ThresholdSum = 0;
            int _ThresholdValue = 0;
            for (int iLoopCount = 0; iLoopCount < 256; ++iLoopCount)  _ThresholdSum += iLoopCount * _HistoValue[iLoopCount];

            double _ThresholdSum2 = 0;
            double _WeightBack = 0, _WeightFore = 0, _VarMax = 0;
            for (int iLoopCount = 0; iLoopCount < 256; ++iLoopCount)
            {
                _WeightBack += _HistoValue[iLoopCount];
                if (0 == _WeightBack) continue;

                _WeightFore = _TotSize - _WeightBack;
                if (0 == _WeightFore) break;

                _ThresholdSum2 += (double)(iLoopCount * _HistoValue[iLoopCount]);

                double _MeanBack = _ThresholdSum2 / _WeightBack;
                double _MeanFore = (_ThresholdSum - _ThresholdSum2) / _WeightFore;

                double _VarBetween = _WeightBack * _WeightFore * Math.Pow((_MeanBack - _MeanFore), 2);

                if (_VarBetween > _VarMax)
                {
                    _VarMax = _VarBetween;
                    _ThresholdValue = iLoopCount;
                }
            }
            #endregion

            #region Blob Search
            CogBlobTool _BlobTool = new CogBlobTool();
            _BlobTool.InputImage = _SrcImage;
            _BlobTool.Region = _InspRegion;
            _BlobTool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            _BlobTool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            _BlobTool.RunParams.ConnectivityMode = CogBlobConnectivityModeConstants.GreyScale;
            _BlobTool.RunParams.ConnectivityCleanup = CogBlobConnectivityCleanupConstants.Fill;
            _BlobTool.RunParams.SegmentationParams.HardFixedThreshold = _ThresholdValue;
            _BlobTool.RunParams.ConnectivityMinPixels = 10000;
            _BlobTool.Run();

            CogBlobResults _BlobResults = _BlobTool.Results;
            double _MaxSize = 0;
            double _CaliperCenterX = 0;
            double _CaliperCenterY = 0;
            if (_BlobResults.GetBlobs().Count > 0)
            {
                for (int iLoopCount = 0; iLoopCount < _BlobResults.GetBlobs().Count; ++iLoopCount)
                {
                    CogBlobResult _BlobResult = _BlobResults.GetBlobByID(iLoopCount);
                    if (_BlobResult.Area > _MaxSize)
                    {
                        _MaxSize = _BlobResult.Area;
                        _CaliperCenterX = _BlobResult.CenterOfMassX;
                        _CaliperCenterY = _BlobResult.CenterOfMassY;
                    }
                }
            }

            else
            {
                _CaliperCenterX = _CogEllipseAlgo.ArcCenterX - _OffsetX;
                _CaliperCenterY = _CogEllipseAlgo.ArcCenterY - _OffsetY;
            }
            //CogSerializer.SaveObjectToFile(_BlobTool, string.Format(@"D:\CircleBlob.vpp"));
            #endregion
            #endregion

            SetCaliperDirection(_CogEllipseAlgo.CaliperSearchDirection, _CogEllipseAlgo.CaliperPolarity);
            SetCaliper(_CogEllipseAlgo.CaliperNumber, _CogEllipseAlgo.CaliperSearchLength, _CogEllipseAlgo.CaliperProjectionLength, _CogEllipseAlgo.CaliperIgnoreNumber);

            //LJH 2019.05.23 Caliper Center 기준점 변경
            //SetEllipticalArc(_CogEllipseAlgo.ArcCenterX - _OffsetX, _CogEllipseAlgo.ArcCenterY - _OffsetY, _CogEllipseAlgo.ArcRadiusX, _CogEllipseAlgo.ArcRadiusY, _CogEllipseAlgo.ArcAngleSpan);
            SetEllipticalArc(_CaliperCenterX, _CaliperCenterY, _CogEllipseAlgo.ArcRadiusX, _CogEllipseAlgo.ArcRadiusY, _CogEllipseAlgo.ArcAngleSpan);

            if (true == Inspection(_SrcImage)) GetResult();

            if (FindEllipseResults != null && FindEllipseResults.Count > 0) _CogEllipseResult.IsGood = true;
            else _CogEllipseResult.IsGood = false;

            if (!_CogEllipseResult.IsGood)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Ellipse Find Fail!!", CLogManager.LOG_LEVEL.MID);
                _CogEllipseResult.CenterX = _CogEllipseAlgo.ArcCenterX;
                _CogEllipseResult.CenterY = _CogEllipseAlgo.ArcCenterY;
                _CogEllipseResult.RadiusX = _CogEllipseAlgo.ArcRadiusX;
                _CogEllipseResult.RadiusY = _CogEllipseAlgo.ArcRadiusY;
                _CogEllipseResult.OriginX = 0;
                _CogEllipseResult.OriginY = 0;
                _CogEllipseResult.Rotation = 0;
            }

            else
            {
                if (FindEllipseResults.GetEllipse() != null)
                {
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Ellipse Complete", CLogManager.LOG_LEVEL.MID);

                    _CogEllipseResult.PointFoundCount = FindEllipseResults.NumPointsFound;
                    _CogEllipseResult.CenterX = FindEllipseResults.GetEllipse().CenterX;
                    _CogEllipseResult.CenterY = FindEllipseResults.GetEllipse().CenterY;
                    _CogEllipseResult.RadiusX = FindEllipseResults.GetEllipse().RadiusX;
                    _CogEllipseResult.RadiusY = FindEllipseResults.GetEllipse().RadiusY;
                    _CogEllipseResult.OriginX = FindEllipseResults.GetEllipse().CenterX;
                    _CogEllipseResult.OriginY = FindEllipseResults.GetEllipse().CenterY;
                    _CogEllipseResult.Rotation = FindEllipseResults.GetEllipse().Rotation;

                    _CogEllipseResult.PointPosXInfo = new double[FindEllipseResults.Count];
                    _CogEllipseResult.PointPosYInfo = new double[FindEllipseResults.Count];
                    _CogEllipseResult.PointStatusInfo = new bool[FindEllipseResults.Count];
                    for (int iLoopCount = 0; iLoopCount < FindEllipseResults.Count; ++iLoopCount)
                    {
                        if (true == FindEllipseResults[iLoopCount].Found)
                        {
                            _CogEllipseResult.PointPosXInfo[iLoopCount] = FindEllipseResults[iLoopCount].X;
                            _CogEllipseResult.PointPosYInfo[iLoopCount] = FindEllipseResults[iLoopCount].Y;
                        }
                        _CogEllipseResult.PointStatusInfo[iLoopCount] = FindEllipseResults[iLoopCount].Used;
                    }

                    _CogEllipseResult.DiameterMinAlgo = _CogEllipseAlgo.DiameterSize - _CogEllipseAlgo.DiameterMinus;
                    _CogEllipseResult.DiameterMaxAlgo = _CogEllipseAlgo.DiameterSize + _CogEllipseAlgo.DiameterPlus;

                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - Center X : {0}, Y : {1}", _CogEllipseResult.CenterX.ToString("F2"), _CogEllipseResult.CenterY.ToString("F2")), CLogManager.LOG_LEVEL.MID);
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - Radius X : {0}, Y : {1}", _CogEllipseResult.RadiusX.ToString("F2"), _CogEllipseResult.RadiusY.ToString("F2")), CLogManager.LOG_LEVEL.MID);
                }

                else
                {
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Ellipse Find Fail!!", CLogManager.LOG_LEVEL.MID);

                    _CogEllipseResult.CenterX = _CogEllipseAlgo.ArcCenterX;
                    _CogEllipseResult.CenterY = _CogEllipseAlgo.ArcCenterY;
                    _CogEllipseResult.RadiusX = _CogEllipseAlgo.ArcRadiusX;
                    _CogEllipseResult.RadiusY = _CogEllipseAlgo.ArcRadiusY;
                    _CogEllipseResult.OriginX = 0;
                    _CogEllipseResult.OriginY = 0;

                    _CogEllipseResult.IsGood = false;
                }
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Result : " + _CogEllipseResult.IsGood.ToString(), CLogManager.LOG_LEVEL.MID);

            return _Result;
        }

        private bool Inspection(CogImage8Grey _SrcImage)
        {
            bool _Result = true;

            try
            {
                FindEllipseProc.InputImage = _SrcImage;
                FindEllipseProc.Run();
            }
            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionEllipseFind - Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }
            
            return _Result;
        }

        private void SetCaliper(int _CaliperNumber, double _SearchLength, double _ProjectionLength, int _CaliperIgnoreNumber)
        {
            FindEllipseProc.RunParams.NumCalipers = _CaliperNumber;
            FindEllipseProc.RunParams.NumToIgnore = _CaliperIgnoreNumber;
            FindEllipseProc.RunParams.CaliperSearchLength = _SearchLength;
            FindEllipseProc.RunParams.CaliperProjectionLength = _ProjectionLength;
        }

        private void SetCaliperDirection(int _eSearchDir, int _eEdgePolarity = (int)CogCaliperPolarityConstants.DarkToLight)
        {
            FindEllipseProc.RunParams.CaliperSearchDirection = (CogFindEllipseSearchDirectionConstants)_eSearchDir;
            FindEllipseProc.RunParams.CaliperRunParams.Edge0Polarity = (CogCaliperPolarityConstants)_eEdgePolarity;
        }

        private void SetEllipticalArc(double _CenterX, double _CenterY, double _RadiusX, double _RadiusY, double _AngleSpan)
        {
            FindEllipseProc.RunParams.ExpectedEllipticalArc.CenterX = _CenterX;
            FindEllipseProc.RunParams.ExpectedEllipticalArc.CenterY = _CenterY;
            FindEllipseProc.RunParams.ExpectedEllipticalArc.RadiusX = _RadiusX;
            FindEllipseProc.RunParams.ExpectedEllipticalArc.RadiusY = _RadiusY;
            FindEllipseProc.RunParams.ExpectedEllipticalArc.AngleStart = 0;
            FindEllipseProc.RunParams.ExpectedEllipticalArc.AngleSpan = 6.28319;
        }

        private void GetResult()
        {
            FindEllipseResults = FindEllipseProc.Results;
        }
    }
}
