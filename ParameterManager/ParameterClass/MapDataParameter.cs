using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cognex.VisionPro.PMAlign;

namespace ParameterManager
{

    public class MapDataParameter
    {
        public MapDataInfomation        Info;   // Map Data Information
        public UnitPatternComponent     Unit;   // Auto Search Mode == Pattern으로 찾기
        public ManualSearchComponent    Whole;  // Manual Search Mode == 영역지정으로 설정하기
        public MapIDComponent           MapID;  // Map ID Search 용    

        public MapDataParameter()
        {
            Info = new MapDataInfomation();
            MapID = new MapIDComponent();
            Unit = new UnitPatternComponent();
            Whole = new ManualSearchComponent();
        }
    }

    public class MapDataInfomation
    {
        public CogPMAlignPattern UnitPattern;
        public string UnitPatternPath;
        public uint UnitRowCount;
        public uint UnitColumnCount;
        public uint SectionRowCount;
        public uint SectionColumnCount;
        public uint UnitTotalCount
        {
            get { return UnitRowCount * UnitColumnCount; }
            set { value = UnitRowCount * UnitColumnCount; }
        }

        public int SearchType;

        /// <summary>
        /// 0 : ManualMode / 1 : Auto MOde
        /// </summary>
        public int MapDataTeachingMode;

        public List<double> UnitListCenterX;
        public List<double> UnitListCenterY;
        public List<double> UnitListWidth;
        public List<double> UnitListHeight;

        public uint FindCount;
        public double FindScore;
        public double AngleLimit;

        public MapDataInfomation()
        {
            UnitTotalCount = 1;
            UnitRowCount = 1;
            UnitColumnCount = 1;
            SectionRowCount = 1;
            SectionColumnCount = 1;
            SearchType = 0;

            MapDataTeachingMode = 0;

            UnitListCenterX = new List<double>();
            UnitListCenterY = new List<double>();
            UnitListWidth = new List<double>();
            UnitListHeight = new List<double>();

            FindCount = UnitTotalCount;
            FindScore = 75;
            AngleLimit = 5;
        }
    }

    /// <summary>
    /// Manual Area Search 용 Class
    /// </summary>
    public class ManualSearchComponent
    {
        public double SearchAreaCenterX;
        public double SearchAreaCenterY;
        public double SearchAreaWidth;
        public double SearchAreaHeight;

        public ManualSearchComponent()
        {
            SearchAreaCenterX = 800;
            SearchAreaCenterY = 800;
            SearchAreaWidth = 500;
            SearchAreaHeight = 700;
        }
    }

    /// <summary>
    /// Unit Pattern Find 용 Class
    /// </summary>
    public class UnitPatternComponent
    {
        public double SearchAreaCenterX;
        public double SearchAreaCenterY;
        public double SearchAreaWidth;
        public double SearchAreaHeight;
        public double PatternAreaOriginX;
        public double PatternAreaOriginY;
        public double PatternAreaCenterX;
        public double PatternAreaCenterY;
        public double PatternAreaWidth;
        public double PatternAreaHeight;

        public UnitPatternComponent()
        {
            SearchAreaCenterX = 500;
            SearchAreaCenterY = 500;
            SearchAreaWidth = 300;
            SearchAreaHeight = 300;
            PatternAreaOriginX = 500;
            PatternAreaOriginY = 500;
            PatternAreaCenterX = 500;
            PatternAreaCenterY = 500;
            PatternAreaWidth = 200;
            PatternAreaHeight = 200;
        }
    }

    /// <summary>
    /// Map ID용 Class
    /// </summary>
    public class MapIDComponent
    {
        public bool   IsUsableMapID;
        public double SearchAreaCenterX;
        public double SearchAreaCenterY;
        public double SearchAreaWidth;
        public double SearchAreaHeight;

        public int SearhDirection;
        public int SearchThreshold;
        public int SearchSizeMin;
        public int SearchSizeMax;
        public int BlobAreaSizeMin;
        public int BlobAreaSizeMax;

        public List<MapIDRectInfo> MapIDInfoList;

        public MapIDComponent()
        {
            MapIDInfoList = new List<MapIDRectInfo>(); 

            IsUsableMapID = false;
            SearchAreaCenterX = 500;
            SearchAreaCenterY = 500;
            SearchAreaWidth = 400;
            SearchAreaHeight = 400;

            SearhDirection = 0;
            SearchThreshold = 220;
            SearchSizeMin = 100;
            SearchSizeMax = 500;
            BlobAreaSizeMin = 50;
            BlobAreaSizeMax = 200;
        }
    }
}
