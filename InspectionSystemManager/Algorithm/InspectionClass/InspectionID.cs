using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cognex.VisionPro;
using Cognex.VisionPro.ID;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    class InspectionID
    {
        CogID IDProc;
        CogIDResult IDResult;
        CogIDResults IDResults;

        public InspectionID()
        {
            IDProc = new CogID();
            IDResult = new CogIDResult();
            IDResults = new CogIDResults();
        }

        public void Initialize()
        {

        }

        public void DeInitialize()
        {
            IDResults.Dispose();
            IDResult.Dispose();
            IDProc.Dispose();
        }

        public bool Run(CogImage8Grey _SrcImage, CogRectangle _InspRegion, CogBarCodeIDAlgo _CogBarCodeIDAlgo, ref CogBarCodeIDResult _CogBarcodeIDResult)
        {
            bool _Result = true;
            SetIDMode(_CogBarCodeIDAlgo);

            if (true == Inspection(_SrcImage, _InspRegion)) GetResult();

            //결과가 없을 시 영상을 180 회전하여 검사한다.
            if (IDResults.Count == 0)
            {
                Inspection(_SrcImage, _InspRegion, true);
                GetResult();
            }

            if (IDResults != null && IDResults.Count > 0) _CogBarcodeIDResult.IsGood = true;
            else _CogBarcodeIDResult.IsGood = false;

            if(!_CogBarcodeIDResult.IsGood)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Reading Fail!!", CLogManager.LOG_LEVEL.MID);
                _CogBarcodeIDResult.IDResult = new string[1];
                _CogBarcodeIDResult.IDCenterX = new double[1];
                _CogBarcodeIDResult.IDCenterY = new double[1];
                _CogBarcodeIDResult.IDAngle = new double[1];

                _CogBarcodeIDResult.IDCount = 0;
                _CogBarcodeIDResult.IDCenterX[0] = 0.0;
                _CogBarcodeIDResult.IDCenterY[0] = 0.0;
                _CogBarcodeIDResult.NgType = eNgType.ID;
            }
            else
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Reading Complete", CLogManager.LOG_LEVEL.MID);
                _CogBarcodeIDResult.IDCount = IDResults.Count;
                _CogBarcodeIDResult.IDResult = new string[IDResults.Count];
                _CogBarcodeIDResult.IDCenterX = new double[IDResults.Count];
                _CogBarcodeIDResult.IDCenterY = new double[IDResults.Count];
                _CogBarcodeIDResult.IDAngle = new double[IDResults.Count];
                _CogBarcodeIDResult.IDPolygon = new CogPolygon[IDResults.Count];

                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Reading Count : " + IDResults.Count.ToString(), CLogManager.LOG_LEVEL.MID);
                for (int iLoopCount = 0; iLoopCount < IDResults.Count; iLoopCount++)
                {
                    _CogBarcodeIDResult.IDResult[iLoopCount] = IDResults[iLoopCount].DecodedData.DecodedString.ToString();
                    _CogBarcodeIDResult.IDCenterX[iLoopCount] = IDResults[iLoopCount].CenterX;
                    _CogBarcodeIDResult.IDCenterY[iLoopCount] = IDResults[iLoopCount].CenterY;
                    _CogBarcodeIDResult.IDAngle[iLoopCount] = IDResults[iLoopCount].Angle;
                    _CogBarcodeIDResult.IDPolygon[iLoopCount] = IDResults[iLoopCount].BoundsPolygon;

                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Reading Code : " + IDResults[iLoopCount].DecodedData.DecodedString.ToString(), CLogManager.LOG_LEVEL.MID);
                }

                if(IDResults.Count != _CogBarCodeIDAlgo.FindCount) _CogBarcodeIDResult.IsGood = false;
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, " - Result : " + _CogBarcodeIDResult.IsGood.ToString(), CLogManager.LOG_LEVEL.MID);
            return _Result;
        }

        private bool Inspection(CogImage8Grey _SrcImage, CogRectangle _InspArea, bool _IsRotate = false)
        {
            bool _Result = true;

            try
            {
                if (true == _IsRotate)
                {
                    CogRectangleAffine _Area = new CogRectangleAffine();
                    _Area.SetCenterLengthsRotationSkew(_InspArea.CenterX, _InspArea.CenterY, _InspArea.Width, _InspArea.Height, -3.14, 0);
                    IDResults = IDProc.Execute(_SrcImage, _Area);
                }
                else
                    IDResults = IDProc.Execute(_SrcImage, _InspArea);
            }
            catch (Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionID - Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        private CogIDResults GetResult()
        {
            return IDResults;
        }

        private void SetIDMode(CogBarCodeIDAlgo _CogBarCodeIDAlgo)
        {
            IDProc.ProcessingMode = CogIDProcessingModeConstants.IDMax;
            IDProc.DecodedStringCodePage = CogIDCodePageConstants.ANSILatin1;
            IDProc.NumToFind = _CogBarCodeIDAlgo.FindCount;
            SetSymbologyFalse();

            switch (_CogBarCodeIDAlgo.Symbology)
            {
                case "DataMatrix":
                    {
                        IDProc.DataMatrix.Enabled = true;
                        IDProc.DataMatrix.IgnorePolarity = true;
                        IDProc.DataMatrix.ProcessControlMetrics = CogIDDataMatrixProcessControlMetricsConstants.None;
                    }
                    break;
                case "QRCode":
                    {
                        IDProc.QRCode.Enabled = true;
                        IDProc.QRCode.MaxGridSize = 49;
                    }
                    break;

                default:
                    {
                        //Default는 DataMatrix로
                        IDProc.DataMatrix.Enabled = true;
                        IDProc.DataMatrix.ProcessControlMetrics = CogIDDataMatrixProcessControlMetricsConstants.None;
                    }
                    break;
            }
        }

        private void SetSymbologyFalse()
        {
            IDProc.DataMatrix.Enabled = false;
            IDProc.Codabar.Enabled = false;
            IDProc.QRCode.Enabled = false;
            IDProc.Code128.Enabled = false;
            IDProc.Code39.Enabled = false;
            IDProc.Code93.Enabled = false;
            IDProc.UpcEan.Enabled = false;
        }
    }
}
