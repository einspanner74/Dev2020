using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ImageProcessing;

namespace MapDataManager
{
    class InspectionPattern
    {
        private CogPMAlignTool PatternProc;
        private CogPMAlignResults PatternResults;

        public InspectionPattern()
        {
            PatternProc = new CogPMAlignTool();
            PatternProc.Pattern.TrainAlgorithm = CogPMAlignTrainAlgorithmConstants.PatQuick;
            PatternProc.RunParams.ContrastThreshold = 5;
            PatternResults = new CogPMAlignResults();
        }

        public void Initialize()
        {

        }

        public void DeInitialize()
        {
            PatternProc.Dispose();
        }

        public void SetPatternReference(CogPMAlignPattern _Pattern)
        {
            PatternProc.Pattern = _Pattern;
        }

        public void SetMatchingParameter(uint _FindCount, double _Score)
        {
            PatternProc.RunParams.ApproximateNumberToFind = (int)_FindCount;
            PatternProc.RunParams.AcceptThreshold = _Score / 100;

            PatternProc.RunParams.ZoneAngle.Configuration = CogPMAlignZoneConstants.LowHigh;
            PatternProc.RunParams.ZoneAngle.Low = 8 * -1 * Math.PI / 180;
            PatternProc.RunParams.ZoneAngle.High = 8 * 1 * Math.PI / 180;
        }

        public CogPMAlignPattern GetPatternReference(CogImage8Grey _SrcImage, CogRectangle _Region, double _OriginX, double _OriginY)
        {
            CogPMAlignPattern _Pattern = new CogPMAlignPattern();
            CogRectangleAffine _PatternRegionAffine = new CogRectangleAffine();
            _PatternRegionAffine.SetCenterLengthsRotationSkew(_Region.CenterX, _Region.CenterY, _Region.Width, _Region.Height, 0, 0);

            CogAffineTransformTool _AffineTool = new CogAffineTransformTool();
            _AffineTool.InputImage = _SrcImage;
            _AffineTool.Region = _PatternRegionAffine;
            _AffineTool.Run();

            _Pattern.TrainImage = (CogImage8Grey)_AffineTool.OutputImage;
            _Pattern.TrainRegion = _PatternRegionAffine;
            _Pattern.Origin.TranslationX = _OriginX;
            _Pattern.Origin.TranslationY = _OriginY;
            _Pattern.Train();

            return _Pattern;
        }

        public CogPMAlignResults GetResults()
        {
            return PatternProc.Results;
        }

        private bool Inspection(CogImage8Grey _SrcImage, CogRectangle _Region)
        {
            bool _Result = true;

            try
            {
                CogSerializer.SaveObjectToFile(PatternProc, @"D:\Pattern.vpp");
                PatternProc.InputImage = _SrcImage;
                PatternProc.SearchRegion = _Region;
                PatternProc.Run();
                //GetResults();

                
            }

            catch (System.Exception ex)
            {
                //CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionPattern - Inspection Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                _Result = false;
            }

            return _Result;
        }

        public bool Run(CogImage8Grey _SrcImage, CogRectangle _SearchRegion = null)
        {
            return Inspection(_SrcImage, _SearchRegion);
        }
    }
}
