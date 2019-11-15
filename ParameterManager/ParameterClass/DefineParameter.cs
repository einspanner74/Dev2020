    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParameterManager
{
    public enum eSysMode { AUTO_MODE = 1, MANUAL_MODE = 2, ONESHOT_MODE = 3, TEACH_MODE = 4, LIVE_MODE = 5, RCP_MANUAL_CHANGE = 6, CAL_MODE = 7 }

    /// <summary>
    /// Project Type
    /// </summary>
    public enum eProjectType { NONE = 0, SCALIGN, NAVIEN };

    /// <summary>
    /// Project Item
    /// </summary>
    public enum eProjectItem { NONE = 0, ALIGN, MEASURE};

    /// <summary>
    /// Algorithm Type
    /// </summary>
    public enum eAlgoType   { C_NONE = -1, C_PATTERN, C_BLOB_REFER, C_BLOB, C_ID, C_LINE_FIND, C_MULTI_PATTERN, C_AUTO_PATTERN, C_ELLIPSE }

    /// <summary>
    /// Camera Model Type
    /// </summary>
    public enum eCameraType { Dalsa, Euresys, EuresysIOTA, BaslerGE }

    /// <summary>
    /// 
    /// </summary>
    public enum eMainProcCmd { TRG = 1, REQUEST, RCP_CHANGE, RECV_DATA, ACK_COMPLETE, ACK_FAIL, START, CAL }

    /// <summary>
    /// Inspection Window Command
    /// </summary>
    public enum eIWCMD      { TEACHING = 1, TEACH_OK, TEACH_SAVE, ONESHOT_INSP, SET_RESULT, SEND_DATA, INSP_COMPLETE, LIGHT_CONTROL, NOTICE_WINDOW }

    /// <summary>
    /// Inspection System Manager To Main Command
    /// </summary>
    public enum eISMCMD     { TEACHING_STATUS = 1, TEACHING_SAVE, MAPDATA_SAVE, SET_RESULT, SEND_DATA, INSP_COMPLETE, LIGHT_CONTROL, NOTICE_WINDOW }

    /// <summary>
    /// Teaching Step
    /// </summary>
    public enum eTeachStep  { AREA_SELECT = 0, AREA_SET, AREA_CLEAR, ALGO_SELECT, ALGO_SET, ALGO_CLEAR };

    /// <summary>
    /// NG Type
    /// </summary>
    public enum eNgType     { GOOD = 0, NONE, DUMMY, REF_NG, DEFECT, CRACK, ID, EMPTY, M_REF, MEASURE }

    public enum eMorphologyMode { ERODE = 0, DILATE, OPEN, CLOSE, }

    public enum eBenchMarkPosition { TL = 0, TC, TR, ML, MC, MR, BL, BC, BR, GC };

    public enum eBodyPosition       { TL = 0, TR, BL, BR };

    public enum eForeColor          { BLACK = 0, WHITE = 1 };

    public enum eSearchDirection    { IN_WARD = 0, OUT_WARD = 1 };

    public enum ePolarity           { DARK_TO_LIGHT = 1, LIGHT_TO_DARK = 2 }

    public enum eInspMode           { TRI_INSP = 0, ONE_INSP, SINGLE_INSP, SIMUL_INSP };

    public enum eReferAction        { ADD = 0, DEL, MODIFY };

    public enum eSaveMode           { ALL = 0, ONLY_NG };

    public enum eLanguage           { KR = 0, EN };
    
    public static class DIO_DEF
    {
        public const int NONE = -1;

        public const int OUT_LIVE = 0;
        public const int OUT_AUTO = 1;
        public const int OUT_ALARM = 2;
        public const int OUT_READY      = 3;
        public const int OUT_COMPLETE   = 4;
        public const int OUT_ERROR      = 5;
        public const int OUT_GOOD       = 6;
        public const int OUT_READY_2    = 7;
        public const int OUT_COMPLETE_2 = 8;
        public const int OUT_RESULT     = 9;

        public const int IN_LIVE        = 0;
        public const int IN_ALARM_OFF   = 1;
        public const int IN_MODE        = 2;
        public const int IN_RESET       = 3;
        public const int IN_TRG         = 4;
        public const int IN_REQUEST     = 5;
        public const int IN_RESET_2     = 6;
        public const int IN_TRG_2       = 7;
        public const int IN_REQUEST_2   = 8;
    }

    public static class SIGNAL
    {
        public const int OFF = 0;
        public const int ON = 1;
    }

    public static class LIGHT
    {
        public const int OFF = 0;
        public const int ON = 1;
    }

    public struct CenterPoint
    {
        public double X;
        public double Y;
    }

    public struct PointD
    {
        public double X;
        public double Y;
    }

    /// <summary>
    /// double형 Rectangle
    /// </summary>
    public class RectangleD
    {
        public double CenterX;
        public double CenterY;
        public double Width;
        public double Height;
        
        public RectangleD()
        {
            CenterX = 100;
            CenterY = 100;
            Width = 150;
            Height = 200;
        }

        public void SetCenterWidthHeight(double _X, double _Y, double _W, double _H)
        {
            CenterX = _X;
            CenterY = _Y;
            Width = _W;
            Height = _H;
        }
    }

    public class MapIDRectInfo
    {
        public CenterPoint CenterPt;
        public double Width;
        public double Height;

        public MapIDRectInfo()
        {
            CenterPt = new CenterPoint();
            Width = 0;
            Height = 0;
        }
    }

    public class EthernetRecvInfo
    {
        public int PortNumber;
        public string[] RecvData;

        public EthernetRecvInfo()
        {
            PortNumber = 5000;
        }

        public void SetRecvInfo(int _PortNumber, string[] _RecvData)
        {
            PortNumber = _PortNumber;
            RecvData = _RecvData;
        }
    }

    //LAT Mitsubishi Communication Define
    /// <summary>
    /// PLC To Vision Communication Address
    /// </summary>
    public static class PTVAddr
    {
        public const int ALIVE              = 0x00;
        public const int V1_CURRENT_POS_U   = 0x02;
        public const int V1_CURRENT_POS_V   = 0x04;
        public const int V1_CURRENT_POS_W   = 0x06;
        public const int V2_CURRENT_POS_U   = 0x0A;
        public const int V2_CURRENT_POS_V   = 0x0C;
        public const int V2_CURRENT_POS_W   = 0x0E;
        public const int V1_INSP_REQ        = 0x18;
        public const int V1_RETRY_CNT       = 0x19;
        public const int V2_INSP_REQ        = 0x1A;
        public const int V2_RETRY_CNT       = 0x1B;
    }

    public static class PTVDefine
    {
        public const int V_STATUS_NOT   = 0;
        public const int V_STATUS_INSP  = 1;
        public const int V_STATUS_CAL   = 2;
        public const int V_STATUS_CLEAR = 5;
    }

    /// <summary>
    /// Vision To PLC Communication Address
    /// </summary>
    public static class VTPAddr
    {
        public const int ALIVE              = 0x00;
        public const int MODE_STATUS        = 0x01;
        public const int V1_RETRY_COUNT     = 0x11;
        public const int V1_ALIGN_POS_U     = 0X12;
        public const int V1_ALIGN_POS_V     = 0x14;
        public const int V1_ALIGN_POS_W     = 0x16;
        public const int V1_LAST_POS_U      = 0x18;
        public const int V1_LAST_POS_V      = 0x1A;
        public const int V1_LAST_POS_W      = 0x1C;
        public const int V1_INSP_RESULT     = 0x1E;
        public const int V2_RETRY_COUNT     = 0x1F;
        public const int V2_ALIGN_POS_U     = 0X20;
        public const int V2_ALIGN_POS_V     = 0x22;
        public const int V2_ALIGN_POS_W     = 0x24;
        public const int V2_LAST_POS_U      = 0x26;
        public const int V2_LAST_POS_V      = 0x28;
        public const int V2_LAST_POS_W      = 0x2A;
        public const int V2_INSP_RESULT     = 0x2C;
    }

    public static class VTPDefine
    {
        //0-Nothing, 1-Manual, 2-Auto, 3-Calibration
        public const int MANUAL_MODE = 1;
        public const int AUTO_MODE = 2;
        public const int CAL_MODE = 3;

        public const int NG = 1;
        public const int OK = 2;
        public const int MOVE = 3;
    }
}
