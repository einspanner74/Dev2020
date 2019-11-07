using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cognex.VisionPro;
using Cognex.VisionPro.Blob;

using ParameterManager;
using LogMessageManager;
using Cognex.VisionPro.ImageProcessing;

namespace InspectionSystemManager
{
    class InspectionBlobReference
    {
        private CogBlob         BlobProc;
        private CogBlobResults  BlobResults;
        private CogBlobResult   BlobResult;
        private CogBlobReferenceResult InspResults;

        #region Initialize & Deinitialize
        public InspectionBlobReference()
        {
            BlobProc = new CogBlob();
            BlobResults = new CogBlobResults();
            BlobResult = new CogBlobResult();
            InspResults = new CogBlobReferenceResult();

            BlobProc.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            BlobProc.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            BlobProc.SegmentationParams.HardFixedThreshold = 100;
            BlobProc.ConnectivityMode = CogBlobConnectivityModeConstants.GreyScale;
            BlobProc.ConnectivityCleanup = CogBlobConnectivityCleanupConstants.Fill;
            BlobProc.ConnectivityMinPixels = 10;
        }

        public void Initialize()
        {

        }

        public void DeInitialize()
        {
            BlobResults.Dispose();
            BlobResult.Dispose();
            BlobProc.Dispose();
        }
        #endregion Initialize & Deinitialize

        public double GetHistogramStandardDeviatioValue(CogImage8Grey _SrcImage, CogRectangle _InspRegion)
        {
            CogHistogramTool _CogHistoTool = new CogHistogramTool();
            _CogHistoTool.InputImage = _SrcImage;
            _CogHistoTool.Region = _InspRegion;
            _CogHistoTool.Run();
            double _HistoDeviation = _CogHistoTool.Result.StandardDeviation;

            return _HistoDeviation;
        }

        public bool Run(CogImage8Grey _SrcImage, CogRectangleAffine _InspRegion, CogBlobReferenceAlgo _CogBlobReferAlgo, ref CogBlobReferenceResult _CogBlobReferResult, int _NgNumber = 0)
        {
            bool _Result = true;
            double _X = 0, _Y = 0;

            SetHardFixedThreshold(_CogBlobReferAlgo.ThresholdMin);
            SetConnectivityMinimum((int)_CogBlobReferAlgo.BlobAreaMin);
            SetPolarity(Convert.ToBoolean(_CogBlobReferAlgo.ForeGround));
            SetMeasurement(CogBlobMeasureConstants.Area, CogBlobMeasureModeConstants.Filter, CogBlobFilterModeConstants.IncludeBlobsInRange, _CogBlobReferAlgo.BlobAreaMin, _CogBlobReferAlgo.BlobAreaMax);
            if (true == Inspection(_SrcImage, _InspRegion)) GetResult(true);

            List<int> _ResultIndexList = new List<int>();
            if (GetResults().BlobCount > 0)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Blob Total Count : " + GetResults().BlobCount.ToString(), CLogManager.LOG_LEVEL.MID);

                CogBlobReferenceResult _CogBlobReferResultTemp = new CogBlobReferenceResult();
                _CogBlobReferResultTemp = GetResults();

                double _ResolutionX = _CogBlobReferAlgo.ResolutionX;
                double _ResolutionY = _CogBlobReferAlgo.ResolutionY;
                for (int iLoopCount = 0; iLoopCount < _CogBlobReferResultTemp.BlobCount; ++iLoopCount)
                {
                    double _ResultRealWidth = _CogBlobReferResultTemp.Width[iLoopCount] * _ResolutionX;
                    double _ResultRealHeight = _CogBlobReferResultTemp.Height[iLoopCount] * _ResolutionY;

                    double _RealWidth       = _CogBlobReferAlgo.Width;
                    double _RealWidthPos    = _CogBlobReferAlgo.WidthPos;
                    double _RealWidthNeg    = _CogBlobReferAlgo.WidthNeg;
                    double _RealHeight      = _CogBlobReferAlgo.Height;
                    double _RealHeightPos   = _CogBlobReferAlgo.HeightPos;
                    double _RealHeightNeg   = _CogBlobReferAlgo.HeightNeg;

                    if ((_ResultRealWidth > _RealWidth - Math.Abs(_RealWidthNeg)) && (_ResultRealWidth < _RealWidth + _RealWidthPos) && (_ResultRealHeight > _RealHeight - Math.Abs(_RealHeightNeg)) && (_ResultRealHeight < _RealHeight + _RealHeightPos))
                    {
                        _CogBlobReferResultTemp.IsGoods[iLoopCount] = true;

                        #region UseBodyArea/UseBodyWidth/UseBodyHeight Filer
                        if (_CogBlobReferAlgo.UseBodyArea)
                        {
                            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyArea Flag True", CLogManager.LOG_LEVEL.MID);
                            double _BodyAreaGap = Math.Abs(_CogBlobReferResultTemp.BlobArea[iLoopCount] - _CogBlobReferAlgo.BodyArea);
                            if ((_CogBlobReferAlgo.BodyArea * _CogBlobReferAlgo.BodyAreaPermitPercent / 100) > _BodyAreaGap)
                            {
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyArea Result Fail!!", CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyArea Result : {0}", _BodyAreaGap.ToString("F2")), CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyArea Condition : {0}", (_CogBlobReferAlgo.BodyArea * _CogBlobReferAlgo.BodyAreaPermitPercent / 100).ToString("F2")), CLogManager.LOG_LEVEL.MID);

                                _CogBlobReferResultTemp.IsGoods[iLoopCount] = false;
                                continue;
                            }
                        }

                        if (_CogBlobReferAlgo.UseBodyWidth)
                        {
                            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyWidth Flag True", CLogManager.LOG_LEVEL.MID);
                            double _BodyWidthGap = Math.Abs(_CogBlobReferResultTemp.Width[iLoopCount] - _CogBlobReferAlgo.BodyWidth);
                            if ((_CogBlobReferAlgo.BodyWidth * _CogBlobReferAlgo.BodyWidthPermitPercent / 100) > _BodyWidthGap)
                            {
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyWidth Result Fail!!", CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyWidth Result : {0}", _BodyWidthGap.ToString("F2")), CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyWidth Condition : {0}", (_CogBlobReferAlgo.BodyWidth * _CogBlobReferAlgo.BodyWidthPermitPercent / 100).ToString("F2")), CLogManager.LOG_LEVEL.MID);

                                _CogBlobReferResultTemp.IsGoods[iLoopCount] = false;
                                continue;
                            }
                        }

                        if (_CogBlobReferAlgo.UseBodyHeight)
                        {
                            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyHeight Flag True", CLogManager.LOG_LEVEL.MID);
                            double _BodyHeightGap = Math.Abs(_CogBlobReferResultTemp.Height[iLoopCount] - _CogBlobReferAlgo.BodyHeight);
                            if ((_CogBlobReferAlgo.BodyHeight * _CogBlobReferAlgo.BodyHeightPermitPercent / 100) > _BodyHeightGap)
                            {
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - UseBodyHeight Result Fail!!", CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyHeight Result : {0}", _BodyHeightGap.ToString("F2")), CLogManager.LOG_LEVEL.MID);
                                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - UseBodyHeight Condition : {0}", (_CogBlobReferAlgo.BodyHeight * _CogBlobReferAlgo.BodyHeightPermitPercent / 100).ToString("F2")), CLogManager.LOG_LEVEL.MID);

                                _CogBlobReferResultTemp.IsGoods[iLoopCount] = false;
                                continue;
                            }
                        }
                        #endregion

                        CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - W : {0}mm, H : {1}mm", _ResultRealWidth, _ResultRealHeight), CLogManager.LOG_LEVEL.MID);

                        eBenchMarkPosition _eBenchMark = (eBenchMarkPosition)_CogBlobReferAlgo.BenchMarkPosition;
                        if (eBenchMarkPosition.TL == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMinX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMinY[iLoopCount]; }
                        else if (eBenchMarkPosition.TC == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobCenterX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMinY[iLoopCount]; }
                        else if (eBenchMarkPosition.TR == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMaxX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMinY[iLoopCount]; }
                        else if (eBenchMarkPosition.ML == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMinX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobCenterY[iLoopCount]; }
                        else if (eBenchMarkPosition.MC == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobCenterX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobCenterY[iLoopCount]; }
                        else if (eBenchMarkPosition.MR == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMaxX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobCenterY[iLoopCount]; }
                        else if (eBenchMarkPosition.BL == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMinX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMaxY[iLoopCount]; }
                        else if (eBenchMarkPosition.BC == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobCenterX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMaxY[iLoopCount]; }
                        else if (eBenchMarkPosition.BR == _eBenchMark) { _X = _CogBlobReferResultTemp.BlobMaxX[iLoopCount]; _Y = _CogBlobReferResultTemp.BlobMaxY[iLoopCount]; }
                        _CogBlobReferResultTemp.OriginX[iLoopCount] = _X;
                        _CogBlobReferResultTemp.OriginY[iLoopCount] = _Y;

                        _ResultIndexList.Add(iLoopCount);
                    }
                }

                if (_ResultIndexList.Count > 0)
                {
                    if (_CogBlobReferAlgo.UseDummyValue)
                    {
                        //Histogram check
                        CogHistogramTool _CogHistoTool = new CogHistogramTool();
                        _CogHistoTool.InputImage = _SrcImage;
                        _CogHistoTool.Region = _InspRegion;
                        _CogHistoTool.Run();
                        double _HistoAvg = _CogHistoTool.Result.StandardDeviation;
                        _CogBlobReferResult.HistogramAvg = _HistoAvg;
                        if (_CogBlobReferAlgo.DummyHistoMeanValue + 5 > _HistoAvg)// && _CogBlobReferAlgo.DummyHistoMeanValue - 5 < _HistoAvg)
                            _CogBlobReferResult.DummyStatus = true;

                        CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Dummy Deviation : " + _HistoAvg.ToString("F2"), CLogManager.LOG_LEVEL.MID);
                        CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Dummy Status : " + _CogBlobReferResult.DummyStatus.ToString(), CLogManager.LOG_LEVEL.MID);
                    }

                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Blob Condition Count : " + _ResultIndexList.Count.ToString(), CLogManager.LOG_LEVEL.MID);
                    #region _CogBlobReferResult 할당
                    int _Count = _ResultIndexList.Count;
                    _CogBlobReferResult.BlobCount = _Count;
                    _CogBlobReferResult.BlobMessCenterX = new double[_Count];
                    _CogBlobReferResult.BlobMessCenterY = new double[_Count];
                    _CogBlobReferResult.BlobCenterX = new double[_Count];
                    _CogBlobReferResult.BlobCenterY = new double[_Count];
                    _CogBlobReferResult.BlobMinX = new double[_Count];
                    _CogBlobReferResult.BlobMaxX = new double[_Count];
                    _CogBlobReferResult.BlobMinY = new double[_Count];
                    _CogBlobReferResult.BlobMaxY = new double[_Count];
                    _CogBlobReferResult.Width = new double[_Count];
                    _CogBlobReferResult.Height = new double[_Count];
                    _CogBlobReferResult.BlobRatio = new double[_Count];
                    _CogBlobReferResult.Angle = new double[_Count];
                    _CogBlobReferResult.BlobXMinYMax = new double[_Count];
                    _CogBlobReferResult.BlobXMaxYMin = new double[_Count];
                    _CogBlobReferResult.BlobArea = new double[_Count];
                    _CogBlobReferResult.OriginX = new double[_Count];
                    _CogBlobReferResult.OriginY = new double[_Count];
                    _CogBlobReferResult.IsGoods = new bool[_Count];
                    _CogBlobReferResult.ResultGraphic = new CogCompositeShape[_Count];
                    #endregion _CogBlobReferResult 할당

                    for (int iLoopCount = 0; iLoopCount < _Count; ++iLoopCount)
                    {
                        _CogBlobReferResult.BlobMessCenterX[iLoopCount] = _CogBlobReferResultTemp.BlobMessCenterX[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobMessCenterY[iLoopCount] = _CogBlobReferResultTemp.BlobMessCenterY[_ResultIndexList[iLoopCount]];

                        _CogBlobReferResult.BlobCenterX[iLoopCount] = _CogBlobReferResultTemp.BlobCenterX[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobCenterY[iLoopCount] = _CogBlobReferResultTemp.BlobCenterY[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobMinX[iLoopCount] = _CogBlobReferResultTemp.BlobMinX[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobMaxX[iLoopCount] = _CogBlobReferResultTemp.BlobMaxX[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobMinY[iLoopCount] = _CogBlobReferResultTemp.BlobMinY[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobMaxY[iLoopCount] = _CogBlobReferResultTemp.BlobMaxY[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.Width[iLoopCount] = _CogBlobReferResultTemp.Width[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.Height[iLoopCount] = _CogBlobReferResultTemp.Height[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.BlobArea[iLoopCount] = _CogBlobReferResultTemp.BlobArea[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.OriginX[iLoopCount] = _CogBlobReferResultTemp.OriginX[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.OriginY[iLoopCount] = _CogBlobReferResultTemp.OriginY[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.IsGoods[iLoopCount] = _CogBlobReferResultTemp.IsGoods[_ResultIndexList[iLoopCount]];
                        _CogBlobReferResult.ResultGraphic[iLoopCount] = _CogBlobReferResultTemp.ResultGraphic[iLoopCount];
                    }
                    _CogBlobReferResult.IsGood = true;
                }

                else
                {
                    _CogBlobReferResult.BlobMinX = new double[1];
                    _CogBlobReferResult.BlobMinY = new double[1];
                    _CogBlobReferResult.BlobCenterX = new double[1];
                    _CogBlobReferResult.BlobCenterY = new double[1];
                    _CogBlobReferResult.Width = new double[1];
                    _CogBlobReferResult.Height = new double[1];
                    _CogBlobReferResult.OriginX = new double[1];
                    _CogBlobReferResult.OriginY = new double[1];
                    _CogBlobReferResult.IsGoods = new bool[1];
                    _CogBlobReferResult.BlobMinX[0] = _InspRegion.CenterX - (_InspRegion.SideXLength / 2);
                    _CogBlobReferResult.BlobMinY[0] = _InspRegion.CenterY - (_InspRegion.SideYLength / 2);
                    _CogBlobReferResult.BlobCenterX[0] = _InspRegion.CenterX;
                    _CogBlobReferResult.BlobCenterY[0] = _InspRegion.CenterY;
                    _CogBlobReferResult.Width[0] = _InspRegion.SideXLength;
                    _CogBlobReferResult.Height[0] = _InspRegion.SideXLength;
                    _CogBlobReferResult.OriginX[0] = _InspRegion.CenterX;
                    _CogBlobReferResult.OriginY[0] = _InspRegion.CenterY;

                    _CogBlobReferResult.IsGood = false;
                }
            }

            else
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Blob not found!!", CLogManager.LOG_LEVEL.MID);

                _CogBlobReferResult.BlobMinX = new double[1];
                _CogBlobReferResult.BlobMinY = new double[1];
                _CogBlobReferResult.BlobCenterX = new double[1];
                _CogBlobReferResult.BlobCenterY = new double[1];
                _CogBlobReferResult.Width = new double[1];
                _CogBlobReferResult.Height = new double[1];
                _CogBlobReferResult.OriginX = new double[1];
                _CogBlobReferResult.OriginY = new double[1];
                _CogBlobReferResult.IsGoods = new bool[1];
                _CogBlobReferResult.BlobMinX[0] = _InspRegion.CenterX - (_InspRegion.SideXLength / 2);
                _CogBlobReferResult.BlobMinY[0] = _InspRegion.CenterX - (_InspRegion.SideYLength / 2);
                _CogBlobReferResult.BlobCenterX[0] = _InspRegion.CenterX;
                _CogBlobReferResult.BlobCenterY[0] = _InspRegion.CenterY;
                _CogBlobReferResult.Width[0] = _InspRegion.SideXLength;
                _CogBlobReferResult.Height[0] = _InspRegion.SideXLength;
                _CogBlobReferResult.OriginX[0] = _InspRegion.CenterX;
                _CogBlobReferResult.OriginY[0] = _InspRegion.CenterY;

                _CogBlobReferResult.IsGood = false;
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Result : " + _CogBlobReferResult.IsGood.ToString(), CLogManager.LOG_LEVEL.MID);
            return _Result;
        }

        private bool Inspection(CogImage8Grey _SrcImage, CogRectangleAffine _InspArea)
        {
            bool _Result = true;

            try
            {
                BlobResults = BlobProc.Execute(_SrcImage, _InspArea);
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionBlobReference - Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        private CogBlobReferenceResult GetResults()
        {
            return InspResults;
        }

        private CogCompositeShape[] GetGraphicResult()
        {
            CogCompositeShape[] _GraphicResult = new CogCompositeShape[BlobResults.GetBlobs().Count];
            CogBlobResult _BlobResult = new CogBlobResult();

            for (int iLoopCount = 0; iLoopCount < BlobResults.GetBlobs().Count; ++iLoopCount)
            {
                BlobResult = BlobResults.GetBlobByID(iLoopCount);
                _GraphicResult[iLoopCount] = BlobResult.CreateResultGraphics(CogBlobResultGraphicConstants.Boundary);
            }

            return _GraphicResult;
        }

        private bool GetResult(bool _IsGraphicResult)
        {
            bool _Result = true;

            if (null == BlobResults || BlobResults.GetBlobs().Count < 0) return false;
            InspResults.BlobCount = BlobResults.GetBlobs().Count;
            InspResults.BlobArea = new double[BlobResults.GetBlobs().Count];
            InspResults.Width = new double[BlobResults.GetBlobs().Count];
            InspResults.Height = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMinX = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMinY = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMaxX = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMaxY = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobCenterX = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobCenterY = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMessCenterX = new double[BlobResults.GetBlobs().Count];
            InspResults.BlobMessCenterY = new double[BlobResults.GetBlobs().Count];
            InspResults.OriginX = new double[BlobResults.GetBlobs().Count];
            InspResults.OriginY = new double[BlobResults.GetBlobs().Count];
            InspResults.IsGoods = new bool[BlobResults.GetBlobs().Count];
            if (_IsGraphicResult) InspResults.ResultGraphic = new CogCompositeShape[InspResults.BlobCount];

            for (int iLoopCount = 0; iLoopCount < InspResults.BlobCount; ++iLoopCount)
            {
                BlobResult = BlobResults.GetBlobByID(iLoopCount);

                InspResults.BlobArea[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.Area, iLoopCount);
                InspResults.Width[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeWidth, iLoopCount);
                InspResults.Height[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeHeight, iLoopCount);
                InspResults.BlobMinX[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeMinX, iLoopCount);
                InspResults.BlobMinY[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeMinY, iLoopCount);
                InspResults.BlobMaxX[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeMaxX, iLoopCount);
                InspResults.BlobMaxY[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.BoundingBoxPixelAlignedNoExcludeMaxY, iLoopCount);
                InspResults.BlobCenterX[iLoopCount] = (InspResults.BlobMaxX[iLoopCount] + InspResults.BlobMinX[iLoopCount]) / 2;
                InspResults.BlobCenterY[iLoopCount] = (InspResults.BlobMaxY[iLoopCount] + InspResults.BlobMinY[iLoopCount]) / 2;
                InspResults.BlobMessCenterX[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.CenterMassX, iLoopCount);
                InspResults.BlobMessCenterY[iLoopCount] = BlobResults.GetBlobMeasure(CogBlobMeasureConstants.CenterMassY, iLoopCount);

                if (_IsGraphicResult) InspResults.ResultGraphic[iLoopCount] = BlobResult.CreateResultGraphics(CogBlobResultGraphicConstants.Boundary);
            }

            return _Result;
        }

        private void SetMeasurement(CogBlobMeasureConstants _Properties, CogBlobMeasureModeConstants _Filter, CogBlobFilterModeConstants _Range, double _RangeLow, double _RangeHigh, bool _IsNew = true)
        {
            if (_IsNew) BlobProc.RunTimeMeasures.Clear();

            CogBlobMeasure _BlobMeasure = new CogBlobMeasure();
            _BlobMeasure.Measure = _Properties;
            _BlobMeasure.Mode = _Filter;
            _BlobMeasure.FilterMode = _Range;
            _BlobMeasure.FilterRangeLow = _RangeLow;
            _BlobMeasure.FilterRangeHigh = _RangeHigh;
            BlobProc.RunTimeMeasures.Add(_BlobMeasure);
        }

        private void SetPolarity(bool _IsMode)
        {
            if (_IsMode) BlobProc.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            else BlobProc.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.DarkBlobs;
        }

        private void SetHardFixedThreshold(int _ThresholdValue)
        {
            BlobProc.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            BlobProc.SegmentationParams.HardFixedThreshold = _ThresholdValue;
        }

        private void SetConnectivityMinimum(int _MinValue)
        {
            BlobProc.ConnectivityMinPixels = _MinValue;
        }

        private void SetMorphology(eMorphologyMode _OperationMode)
        {
            switch (_OperationMode)
            {
                case eMorphologyMode.OPEN: BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.OpenSquare); break;
                case eMorphologyMode.CLOSE: BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.CloseSquare); break;
                case eMorphologyMode.DILATE: BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.DilateHorizontal); break;
                case eMorphologyMode.ERODE: BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.ErodeHorizontal); break;
            }
        }

        private void SetMorphologyHorizon(int _Time)
        {
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.ErodeHorizontal);
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.DilateHorizontal);
        }

        private void SetMorphologyVertical(int _Time)
        {
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.ErodeVertical);
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.DilateVertical);
        }

        private void SetMorphologySquare(int _Time)
        {
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.ErodeSquare);
            for (int iLoopCount = 0; iLoopCount < _Time; ++iLoopCount)
                BlobProc.MorphologyOperations.Add(CogBlobMorphologyConstants.DilateSquare);
        }
    }
}
