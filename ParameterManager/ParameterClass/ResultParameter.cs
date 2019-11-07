using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cognex.VisionPro;

namespace ParameterManager
{
    #region AreaResultParameterList
    public class AreaResultParameter
    {
        public double OffsetX;
        public double OffsetY;
        public double OffsetT;

        public AreaResultParameter()
        {
            OffsetX = 0;
            OffsetY = 0;
            OffsetT = 0;
        }
    }

    public class AreaResultParameterList : List<AreaResultParameter>
    {

    }
    #endregion AreaResultParameterList

    #region AlgoResultParameterList
    public class AlgoResultParameter
    {
        public Object ResultParam;
        public eAlgoType ResultAlgoType;
        public double OffsetX;
        public double OffsetY;
        public double OffsetT;
        public int NgAreaNumber;

        public double TeachOriginX;
        public double TeachOriginY;

        public AlgoResultParameter()
        {
            OffsetX = 0;
            OffsetY = 0;
            ResultAlgoType = eAlgoType.C_NONE;
            ResultParam = null;

            TeachOriginX = 0;
            TeachOriginY = 0;
        }

        public AlgoResultParameter(eAlgoType _AlgoType, Object _ResultParam)
        {
            ResultParam = _ResultParam;
            ResultAlgoType = _AlgoType;

            OffsetX = 0;
            OffsetY = 0;
            TeachOriginX = 0;
            TeachOriginY = 0;
        }
    }

    public class AlgoResultParameterList : List<AlgoResultParameter>
    {

    }
    #endregion AlgoResultParameterList

    #region Inspection Result Parameter
    public class Result
    {
        public bool         IsGood;
        public eNgType      NgType;
        public RectangleD   SearchArea;
    }

    public class CogPatternResult : Result
    {
        public int FindCount;

        public double[] Score;
        public double[] Scale;
        public double[] Angle;
        public double[] CenterX;
        public double[] CenterY;
        public double[] OriginPointX;
        public double[] OriginPointY;
        public double[] Width;
        public double[] Height;
        public bool[] IsGoods;

        public CogPatternResult()
        {
            IsGood = true;
            NgType = eNgType.GOOD;

            FindCount = 0;
        }
    }

    public class CogMultiPatternResult : Result
    {
        public int FindCount;

        public double[] Score;
        public double[] Scale;
        public double[] Angle;
        public double[] CenterX;
        public double[] CenterY;
        public double[] OriginPointX;
        public double[] OriginPointY;
        public double[] Width;
        public double[] Height;
        public double TwoPointAngle;

        public CogMultiPatternResult()
        {
            IsGood = true;
            NgType = eNgType.GOOD;

            FindCount = 0;

            TwoPointAngle = 0.0;
        }
    }

    public class CogAutoPatternResult : Result
    {
        public double Score;
        public double Scale;
        public double Angle;
        public double CenterX;
        public double CenterY;
        public double OriginPointX;
        public double OriginPointY;
        public double Width;
        public double Height;

        public CogAutoPatternResult()
        {
            IsGood = true;
            NgType = eNgType.GOOD;
        }
    }

    public class CogBlobReferenceResult : Result
    {
        public int      BlobCount;
        public double[] BlobMessCenterX;
        public double[] BlobMessCenterY;
        
        public double[] BlobCenterX;
        public double[] BlobCenterY;
        public double[] BlobMinX;
        public double[] BlobMinY;
        public double[] BlobMaxX;
        public double[] BlobMaxY;
        public double[] Width;
        public double[] Height;
        public double[] BlobRatio;
        public double[] Angle;
        public double[] BlobXMinYMax;
        public double[] BlobXMaxYMin;
        public double[] BlobArea;
        public double[] OriginX;
        public double[] OriginY;
        public bool[] IsGoods;
        public CogCompositeShape[] ResultGraphic;
        public double HistogramAvg;
        public bool DummyStatus;
    }

    public class CogEllipseResult : Result
    {
        public double CenterX;
        public double CenterY;
        public double OriginX;
        public double OriginY;
        public double RadiusX;
        public double RadiusY;

        public double CenterXReal;
        public double CenterYReal;
        public double OriginXReal;
        public double OriginYReal;
        public double RadiusXReal;
        public double RadiusYReal;

        public double Rotation;

        public double DiameterMinAlgo;
        public double DiameterMaxAlgo;

        public int PointFoundCount;

        public double[] PointPosXInfo;
        public double[] PointPosYInfo;
        public bool[] PointStatusInfo;
    }

    public class CogBarCodeIDResult : Result
    {
        public int IDCount;
        public string[] IDResult;
        public double[] IDCenterX;
        public double[] IDCenterY;
        public double[] IDAngle;
        public CogPolygon[] IDPolygon;
    }

    public class CogLineFindResult : Result
    {
        public double StartX;
        public double StartY;
        public double EndX;
        public double EndY;
        public double Length;
        public double Rotation;
        public double LineRotation;
        public int PointCount;
        public bool[] PointStatus;

        public double IntersectionX;
        public double IntersectionY;

        public CogLine LineResult;
    }
    #endregion Inspection Result Parameter

    #region Last Send Result Parameter
    public class SendResultParameter
    { 
        public eProjectItem ProjectItem;
        public eInspMode InspMode;
        public int ID;
        public bool IsGood;
        public RectangleD SearchArea;
        public eNgType NgType;

        public object SendResult;

        //LDH, 2019.05.15, ProjectItem Measure용
        public object[] SendResultList;
        public eAlgoType[] AlgoTypeList;
    }

    public class SendMeasureResult
    {
        public int NGAreaNum;
        public bool IsGoodAlgo;

        public double[] CaliperPointX;
        public double[] CaliperPointY;

        //Ellipse
        public double RadiusX;
        public double RadiusY;
        public double DiameterMinAlgo;
        public double DiameterMaxAlgo;

        //Blob
        public double MeasureData;

        //Pattern
        public double MatchingScore;
        public double PointX;
        public double PointY;

        //ID
        public string ReadCode;

        //Finde Line
        public double IntersectionX;
        public double IntersectionY;
    }
    #endregion Last Send Result Parameter
}
