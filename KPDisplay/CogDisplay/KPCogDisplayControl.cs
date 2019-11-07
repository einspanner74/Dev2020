using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;

using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;

using LogMessageManager;

namespace KPDisplay
{
    public partial class KPCogDisplayControl: UserControl
    {
        private  Point MousePoint = new Point();
        private  CogRectangle InteractiveRectGraphic = new CogRectangle();
        private  CogRectangle StaticRectGraphic = new CogRectangle();
        private  CogRectangleAffine InteractiveRectAffineGraphic = new CogRectangleAffine();
        private  CogRectangleAffine StaticRectAffineGraphic = new CogRectangleAffine();
        private  CogPointMarker InteractivePointMarker = new CogPointMarker();
        private  CogPointMarker StaticPointMarker = new CogPointMarker();
        private  CogGraphicLabel cogLabel = new CogGraphicLabel();
        private  CogLineSegment staticGraphicHor = new CogLineSegment();
        private  CogLineSegment staticGraphicVer = new CogLineSegment();
        private  CogLineSegment staticGraphicDistance = new CogLineSegment();
        private  CogCircle StaticCircleGraphic = new CogCircle();
        private  CogEllipse StaticEllipseGraphic = new CogEllipse();
        private  CogPolygon StaticPolygonGraphic = new CogPolygon();
        
        private  CogLineSegment StaticArrow = new CogLineSegment();
        private  CogLineSegment StaticLineSegment = new CogLineSegment();
        
        private  CogFindCircleTool CircleCaliperTool = new CogFindCircleTool();
        private  CogCircularArc InteractiveCircularArc = new CogCircularArc();

        private  CogFindEllipseTool EllipseCaliperTool = new CogFindEllipseTool();
        private  CogEllipticalArc InteractiveEllipticalArc = new CogEllipticalArc();

        private  CogFindLineTool LineCaliperTool = new CogFindLineTool();
        private  CogLineSegment InteractiveLine = new CogLineSegment();

        #region Color Define
        public CogColorConstants[] ColorDefine = new CogColorConstants[256];
        private CogColorConstants[] ColorDefineOneCycle = new CogColorConstants[14] {   CogColorConstants.Red,          CogColorConstants.Orange,       CogColorConstants.Yellow,       CogColorConstants.Green,
                                                                                        CogColorConstants.Blue,         CogColorConstants.Purple,       CogColorConstants.Magenta,      CogColorConstants.DarkRed,
                                                                                        CogColorConstants.Grey,         CogColorConstants.DarkGreen,    CogColorConstants.Cyan,         CogColorConstants.White,
                                                                                        CogColorConstants.DarkGrey,     CogColorConstants.LightGrey };
        #endregion Color Define

        //public delegate void CogDisplayMouseUpHandler(CogFindCircleTool _CircleCaliperTool);
        public delegate void CogDisplayMouseUpHandler(object _CaliperTool);
        public event CogDisplayMouseUpHandler CogDisplayMouseUpEvent;

        [Category("UseStatusBar"), Browsable(true)]
        public bool UseStatusBar
        {
            get 
            { 
                return this.cogDisplayStatusBarV2.Visible; 
            }
            set 
            { 
                this.cogDisplayStatusBarV2.Visible = value;
                if (false == value) {   this.kCogDisplay.Dock = DockStyle.Fill; }
                else {  this.kCogDisplay.Dock = DockStyle.None; this.kCogDisplay.Size = new Size(this.Width, this.Height - 22); }
            }
        }

        public delegate void MousePointHandler(Point _MousePoint);
        public event MousePointHandler MousePointEvent;

        public KPCogDisplayControl()
        {
            InitializeComponent();
            InitializeControl();
        }

        private void InitializeControl()
        {
            Size _WindowSize = this.Size;
            kCogDisplay.Width = _WindowSize.Width;
            kCogDisplay.Height = _WindowSize.Height - cogDisplayStatusBarV2.Height;
            cogDisplayStatusBarV2.Width = _WindowSize.Width;
            cogDisplayStatusBarV2.Height = 22;   //기본 Size

            kCogDisplay.Location = new Point(0, 0);
            cogDisplayStatusBarV2.Location = new Point(0, kCogDisplay.Height);

            cogDisplayStatusBarV2.Display = kCogDisplay;

            for (int iLoopCount = 0; iLoopCount < ColorDefine.Length; ++iLoopCount)
                ColorDefine[iLoopCount] = ColorDefineOneCycle[iLoopCount % ColorDefineOneCycle.Length];
        }

        private void KPCogDisplayControl_Resize(object sender, EventArgs e)
        {
            InitializeControl();
        }

        #region Display Clear
        /// <summary>
        /// Clear Display
        /// </summary>
        public void ClearDisplay(bool _ImageClear = false)
        {
            kCogDisplay.ClearDisplay(_ImageClear);
        }

        /// <summary>
        /// Clear Display
        /// </summary>
        public void ClearDisplay(bool _StaticClear, bool _InteractiveClear)
        {
            kCogDisplay.ClearDisplay(_StaticClear, _InteractiveClear);
        }

        /// <summary>
        /// Groupname Clear Display
        /// </summary>
        /// <param name="_groupName"></param>
        public void ClearDisplay(string _groupName)
        {
            kCogDisplay.ClearDisplay(_groupName);
        }

        public void ClearDisplay(string[] _ExceptGroupName)
        {
            kCogDisplay.ClearDisplay(_ExceptGroupName);
        }
        #endregion Display Clear

        public void DrawMaskingShape(CogRectangle _cogRect, string _groupName)
        {
            CogMaskGraphic _CogMaskGraphic = new CogMaskGraphic();
            //CogMaskPixelConstants

            StaticRectGraphic = new CogRectangle();
            StaticRectGraphic = _cogRect;
            StaticRectGraphic.Color = CogColorConstants.None;
            StaticRectGraphic.LineStyle = CogGraphicLineStyleConstants.Dash;

            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticRectGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        #region Draw Shape Display Window
        /// <summary>
        /// Display에 그리기
        /// </summary>
        /// <param name="_cogRectAffine">CogRectangleAffine 영역</param>
        /// <param name="_Interactive">Interactive = true, Static = false</param>
        public void DrawShape(CogRectangleAffine _cogRectAffine, string _groupName, CogColorConstants _color, bool _Interactive)
        {
            if (_Interactive == true) DrawInterActiveShape(_cogRectAffine, _groupName, _color);
            else DrawStaticShape(_cogRectAffine, _groupName, _color);
        }

        /// <summary>
        /// Display에 그리기
        /// </summary>
        /// <param name="_cogRectAffine">CogRectangleAffine 영역</param>
        /// <param name="_Interactive">Interactive = true, Static = false</param>
        public void DrawShape(CogPointMarker _cogPointMarker, string _groupName, CogColorConstants _color, bool _Interactive)
        {
            if (_Interactive == true) DrawInterActiveShape(_cogPointMarker, _groupName, _color);
            else DrawStaticShape(_cogPointMarker, _groupName, _color);
        }

        /// <summary>
        /// 수정이 가능한 도형 그리기
        /// </summary>
        /// <param name="_cogRectAffine">RectAffine 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawInterActiveShape(CogRectangleAffine _cogRectAffine, string _groupName, CogColorConstants _color)
        {
            InteractiveRectAffineGraphic = new CogRectangleAffine();
            InteractiveRectAffineGraphic = _cogRectAffine;
            InteractiveRectAffineGraphic.Interactive = true;
            InteractiveRectAffineGraphic.Color = _color;
            InteractiveRectAffineGraphic.LineWidthInScreenPixels = 2;
            InteractiveRectAffineGraphic.GraphicDOFEnable = CogRectangleAffineDOFConstants.All;
            
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.InteractiveGraphics.Add(InteractiveRectAffineGraphic, _groupName, true);
        }

        /// <summary>
        /// 수정이 가능한 도형 그리기
        /// </summary>
        /// <param name="_cogRectAffine">RectAffine 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawInterActiveShape(CogRectangle _cogRect, string _groupName, CogColorConstants _color)
        {
            InteractiveRectGraphic = new CogRectangle();
            InteractiveRectGraphic = _cogRect;
            InteractiveRectGraphic.Interactive = true;
            InteractiveRectGraphic.Color = _color;
            InteractiveRectGraphic.LineWidthInScreenPixels = 2;
            InteractiveRectGraphic.GraphicDOFEnable = CogRectangleDOFConstants.All;

            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.InteractiveGraphics.Add(InteractiveRectGraphic, _groupName, true);
        }

        /// <summary>
        /// RectangleAffine의 선택 여부를 설정하는 Draw 함수
        /// </summary>
        /// <param name="_cogRectAffine"></param>
        /// <param name="_groupName"></param>
        /// <param name="_color"></param>
        /// <param name="_DOFConstants"></param>
        public void DrawInterActiveShape(CogRectangleAffine _cogRectAffine, string _groupName, CogColorConstants _color, CogRectangleAffineDOFConstants _DOFConstants, bool IsSelected = false)
        {
            InteractiveRectAffineGraphic = new CogRectangleAffine();
            InteractiveRectAffineGraphic = _cogRectAffine;
            InteractiveRectAffineGraphic.Interactive = true;
            InteractiveRectAffineGraphic.Color = _color;
            InteractiveRectAffineGraphic.SelectedLineWidthInScreenPixels = 3;
            InteractiveRectAffineGraphic.LineWidthInScreenPixels = 3;
            InteractiveRectAffineGraphic.GraphicDOFEnable = _DOFConstants;
            InteractiveRectAffineGraphic.Selected = IsSelected;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.InteractiveGraphics.Add(InteractiveRectAffineGraphic, _groupName, true);
        }

        /// <summary>
        /// 수정이 불 가능한 고정 도형 그리기
        /// </summary>
        /// <param name="_cogRectAffine">RectAffine 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawStaticShape(CogRectangleAffine _cogRectAffine, string _groupName, CogColorConstants _color, int _LineSize = 2)
        {
            StaticRectAffineGraphic = new CogRectangleAffine();
            StaticRectAffineGraphic = _cogRectAffine;
            StaticRectAffineGraphic.Color = _color;
            StaticRectAffineGraphic.LineWidthInScreenPixels = _LineSize;
            StaticRectAffineGraphic.Selected = true;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticRectAffineGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        /// <summary>
        /// 수정이 불 가능한 고정 도형 그리기
        /// </summary>
        /// <param name="_cogRectAffine">RectAffine 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawStaticShape(CogRectangle _cogRect, string _groupName, CogColorConstants _color, int _LineSize = 2)
        {
            StaticRectGraphic = new CogRectangle();
            StaticRectGraphic = _cogRect;
            StaticRectGraphic.Color = _color;
            StaticRectGraphic.LineWidthInScreenPixels = _LineSize;
            if (_groupName == "InspRegion" || _groupName == "AlgoRegion")
                StaticRectGraphic.LineStyle = CogGraphicLineStyleConstants.Dash;

            StaticRectGraphic.Selected = false;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticRectGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        public void DrawStaticShape(CogRectangle _cogRect, string _groupName, CogColorConstants _color, int _LineSize, CogGraphicLineStyleConstants _Style)
        {
            StaticRectGraphic = new CogRectangle();
            StaticRectGraphic = _cogRect;
            StaticRectGraphic.Color = _color;
            StaticRectGraphic.LineWidthInScreenPixels = _LineSize;
            StaticRectGraphic.LineStyle = _Style;

            StaticRectGraphic.Selected = false;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticRectGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        public void DrawStaticShapeNotClear(CogRectangle _cogRect, string _groupName, CogColorConstants _color, int _LineSize = 2)
        {
            StaticRectGraphic = new CogRectangle();
            StaticRectGraphic = _cogRect;
            StaticRectGraphic.Color = _color;
            StaticRectGraphic.LineWidthInScreenPixels = _LineSize;
            StaticRectGraphic.Selected = false;
            kCogDisplay.StaticGraphics.Add(StaticRectGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        public void DrawStaticShape(CogRectangle _cogRect, string _groupName, bool ClearFlag = false)
        {
            StaticRectGraphic = new CogRectangle();
            StaticRectGraphic = _cogRect;
            StaticRectGraphic.Color = CogColorConstants.Green;
            StaticRectGraphic.LineWidthInScreenPixels = 2;
            StaticRectGraphic.Selected = false;
            kCogDisplay.ClearDisplay("DistanceDrawRegion");
            kCogDisplay.StaticGraphics.Add(StaticRectGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        /// <summary>
        /// 수정이 불 가능한 고정 도형 그리기
        /// </summary>
        /// <param name="_cogPolygon">Polygon 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawStaticShape(CogPolygon _cogPolygon, string _groupName, CogColorConstants _color, int _LineSize = 2)
        {
            StaticPolygonGraphic = new CogPolygon();
            StaticPolygonGraphic = _cogPolygon;
            StaticPolygonGraphic.Color = _color;
            StaticPolygonGraphic.LineWidthInScreenPixels = _LineSize;
            StaticPolygonGraphic.Selected = false;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticPolygonGraphic.CopyBase(CogCopyShapeConstants.All), _groupName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_cogPointMarker"></param>
        /// <param name="_groupName"></param>
        /// <param name="_color"></param>
        public void DrawInterActiveShape(CogPointMarker _cogPointMarker, string _groupName, CogColorConstants _color, int _LineSize = 12, bool _ClearFlag = true)
        {
            InteractivePointMarker = new CogPointMarker();
            InteractivePointMarker = _cogPointMarker;
            InteractivePointMarker.Interactive = true;
            InteractivePointMarker.Color = _color;
            InteractivePointMarker.LineWidthInScreenPixels = 2;
            InteractivePointMarker.SizeInScreenPixels = _LineSize;
            InteractivePointMarker.GraphicType = CogPointMarkerGraphicTypeConstants.Cross;
            InteractivePointMarker.GraphicDOFEnable = CogPointMarkerDOFConstants.All;
            if(_ClearFlag) kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.InteractiveGraphics.Add(InteractivePointMarker, _groupName, true);
        }

        /// <summary>
        /// 수정이 불가능한 고정 도형 그리기
        /// </summary>
        /// <param name="_cogRectAffine">RectAffine 객체</param>
        /// <param name="_groupName">그려지는 그룹명</param>
        /// <param name="_color">색상</param>
        public void DrawStaticShape(CogPointMarker _cogPointMarker, string _groupName, CogColorConstants _color, int _LineSize = 12, bool ClearFlag = true)
        {
            StaticPointMarker = new CogPointMarker();
            StaticPointMarker = _cogPointMarker;
            StaticPointMarker.Color = _color;
            StaticPointMarker.LineWidthInScreenPixels = 2;
            StaticPointMarker.SizeInScreenPixels = _LineSize;
            StaticPointMarker.GraphicType = CogPointMarkerGraphicTypeConstants.Cross;
            if(ClearFlag == true) kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticPointMarker, _groupName);
        }

        public void DrawStaticShape(CogCircle _CogCircle, string _groupName, CogColorConstants _color, int _LineSize = 2)
        {
            StaticCircleGraphic = new CogCircle();
            StaticCircleGraphic = _CogCircle;
            StaticCircleGraphic.Color = _color;
            StaticCircleGraphic.LineWidthInScreenPixels = _LineSize;
            StaticCircleGraphic.GraphicDOFEnable = CogCircleDOFConstants.All;
            kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticCircleGraphic, _groupName);
        }

        public void DrawStaticShape(CogEllipse _CogEllipse, string _groupName, CogColorConstants _color, int _LineSize = 2, bool ClearFlag = true)
        {
            StaticEllipseGraphic = new CogEllipse();
            StaticEllipseGraphic = _CogEllipse;
            StaticEllipseGraphic.Rotation = _CogEllipse.Rotation;
            StaticEllipseGraphic.Color = _color;
            StaticEllipseGraphic.LineWidthInScreenPixels = _LineSize;
            StaticEllipseGraphic.GraphicDOFEnable = CogEllipseDOFConstants.All;
            if (ClearFlag == true) kCogDisplay.ClearDisplay(_groupName);
            kCogDisplay.StaticGraphics.Add(StaticEllipseGraphic, _groupName);
        }

        public void DrawStaticArrow(int _StartX, int _StartY, int _Length, double _Rotate, int _Tickness, string _GroupName, CogColorConstants _Color)
        {
            StaticArrow.Color = _Color;
            StaticArrow.Interactive = false;
            StaticArrow.LineStyle = CogGraphicLineStyleConstants.Solid;
            StaticArrow.EndPointAdornment = CogLineSegmentAdornmentConstants.SolidArrow;
            StaticArrow.LineWidthInScreenPixels = _Tickness;
            StaticArrow.SetStartLengthRotation(_StartX, _StartY, _Length, _Rotate);
            kCogDisplay.StaticGraphics.Add(StaticArrow, _GroupName);
        }

        public void DrawStaticLine(CogLineSegment _Line, string _GroupName, CogColorConstants _Color, CogGraphicLineStyleConstants _LineStyle = CogGraphicLineStyleConstants.Solid, int _Tickness = 2)
        {
            StaticLineSegment = _Line;
            StaticLineSegment.Color = _Color;
            StaticLineSegment.Interactive = false;
            StaticLineSegment.LineStyle = _LineStyle;
            StaticLineSegment.LineWidthInScreenPixels = _Tickness;
            kCogDisplay.StaticGraphics.Add(StaticLineSegment, _GroupName);
        }

        public void DrawStaticLine(double _StartX, double _StartY, double _Length, double _Rotate, int _Tickness, string _GroupName, CogColorConstants _Color)
        {
            StaticLineSegment.Color = _Color;
            StaticLineSegment.Interactive = false;
            StaticLineSegment.LineStyle = CogGraphicLineStyleConstants.Solid;
            StaticLineSegment.LineWidthInScreenPixels = _Tickness;
            StaticLineSegment.SetStartLengthRotation(_StartX, _StartY, _Length, _Rotate);
            kCogDisplay.StaticGraphics.Add(StaticLineSegment, _GroupName);
        }

        public void DrawCross(int _StartX, int _StartY, int _LengthX, int _LengthY, string _GroupName, CogColorConstants _Color)
        {
            if (_LengthX == 0 || _LengthY == 0) return;

            staticGraphicHor.Color = staticGraphicVer.Color = _Color;
            staticGraphicHor.Interactive = staticGraphicVer.Interactive = false;
            staticGraphicHor.GraphicDOFEnable = staticGraphicVer.GraphicDOFEnable = CogLineSegmentDOFConstants.BothPoints;
            staticGraphicHor.LineStyle = staticGraphicVer.LineStyle = CogGraphicLineStyleConstants.Solid;
            staticGraphicHor.LineWidthInScreenPixels = staticGraphicVer.LineWidthInScreenPixels = 1;
            staticGraphicHor.SetStartLengthRotation(_StartX - _LengthX / 2, _StartY, _LengthX, 0);
            staticGraphicVer.SetStartLengthRotation(_StartX, _StartY - _LengthY / 2, _LengthY, 0 + 1.57);

            if (null == _GroupName)
            {
                kCogDisplay.StaticGraphics.Add(staticGraphicHor, "CrossHor");
                kCogDisplay.StaticGraphics.Add(staticGraphicVer, "CrossVer");
            }

            else
            {
                kCogDisplay.StaticGraphics.Add(staticGraphicHor, _GroupName + "Hor");
                kCogDisplay.StaticGraphics.Add(staticGraphicVer, _GroupName + "Ver");
            }
        }

        public void DrwaInterActiveCross(int _StartX, int _StartY, int _Thickness, int _CrossSize, string _GroupName, CogColorConstants _Color)
        {
            InteractivePointMarker = new CogPointMarker();
            InteractivePointMarker.Interactive = true;
            InteractivePointMarker.Color = _Color;
            InteractivePointMarker.X = _StartX;
            InteractivePointMarker.Y = _StartY;
            InteractivePointMarker.LineWidthInScreenPixels = _Thickness;
            InteractivePointMarker.SizeInScreenPixels = _CrossSize;
            InteractivePointMarker.GraphicType = CogPointMarkerGraphicTypeConstants.Cross;
            InteractivePointMarker.GraphicDOFEnable = CogPointMarkerDOFConstants.All;
            kCogDisplay.ClearDisplay(_GroupName);
            kCogDisplay.InteractiveGraphics.Add(InteractivePointMarker, _GroupName, true);
        }

        public void DrawStaticCross(double _StartX, double _StartY, int _Thickness, int _CrossSize, string _GroupName, CogColorConstants _Color)
        {
            StaticPointMarker = new CogPointMarker();
            StaticPointMarker.Interactive = false;
            StaticPointMarker.Color = _Color;
            StaticPointMarker.X = _StartX;
            StaticPointMarker.Y = _StartY;
            StaticPointMarker.LineWidthInScreenPixels = _Thickness;
            StaticPointMarker.SizeInScreenPixels = _CrossSize;
            StaticPointMarker.GraphicType = CogPointMarkerGraphicTypeConstants.Cross;
            StaticPointMarker.GraphicDOFEnable = CogPointMarkerDOFConstants.All;
            //kCogDisplay.ClearDisplay(_GroupName);
            kCogDisplay.StaticGraphics.Add(StaticPointMarker, _GroupName);
        }

        public void DrawDistance(int _StartX, int _StartY, int _EndX, int _EndY, string _DistanceValueName, string _GroupName, CogColorConstants _Color)
        {
            staticGraphicDistance.Color = _Color;
            staticGraphicDistance.Interactive = false;
            staticGraphicDistance.GraphicDOFEnable = CogLineSegmentDOFConstants.BothPoints;
            staticGraphicDistance.LineStyle = CogGraphicLineStyleConstants.Solid;
            staticGraphicDistance.LineWidthInScreenPixels = 1;
            staticGraphicDistance.SetStartEnd(_StartX, _StartY, _EndX, _EndY);

            if (null == _GroupName) kCogDisplay.StaticGraphics.Add(staticGraphicDistance, "DistanceLine");
            else                    kCogDisplay.StaticGraphics.Add(staticGraphicDistance, _GroupName + "DistanceLine");

            if (_StartY > _EndY)
                DrawText(_DistanceValueName, _StartX + 10, _StartY - 50, _Color);

            else
                DrawText(_DistanceValueName, _StartX + 10, _StartY + 50, _Color);
        }

        public void DrawBlobResult(CogCompositeShape ResGraphic, string GroupName)
        {
            ResGraphic.Color = CogColorConstants.Blue;
            ResGraphic.LineWidthInScreenPixels = 2;
            kCogDisplay.StaticGraphics.Add(ResGraphic, GroupName);
        }

        public void DrawFindCircleCaliper(CogFindCircle _CogFindCircle = null)
        {
            ICogRecord _Record;
            CogGraphicCollection _Region;
            
            CircleCaliperTool.InputImage = (CogImage8Grey)kCogDisplay.Image;
            CircleCaliperTool.RunParams = _CogFindCircle;

            _Record = CircleCaliperTool.CreateCurrentRecord();
            InteractiveCircularArc = (CogCircularArc)_Record.SubRecords["InputImage"].SubRecords["ExpectedShapeSegment"].Content;
            _Region = (CogGraphicCollection)_Record.SubRecords["InputImage"].SubRecords["CaliperRegions"].Content;

            kCogDisplay.InteractiveGraphics.Add(InteractiveCircularArc, "CircleArc", false);
            foreach (ICogGraphic _ICogGra in _Region)
                kCogDisplay.InteractiveGraphics.Add((ICogGraphicInteractive)_ICogGra, "", false);
            GC.Collect();
        }

        public void DrawFindEllipseCaliper(CogFindEllipse _CogFindEllipse = null)
        {
            ICogRecord _Record;
            CogGraphicCollection _Region;

            EllipseCaliperTool.InputImage = (CogImage8Grey)kCogDisplay.Image;
            EllipseCaliperTool.RunParams = _CogFindEllipse;

            _Record = EllipseCaliperTool.CreateCurrentRecord();
            InteractiveEllipticalArc = (CogEllipticalArc)_Record.SubRecords["InputImage"].SubRecords["ExpectedShapeSegment"].Content;
            _Region = (CogGraphicCollection)_Record.SubRecords["InputImage"].SubRecords["CaliperRegions"].Content;

            kCogDisplay.InteractiveGraphics.Add(InteractiveEllipticalArc, "EllipseArc", false);
            foreach (ICogGraphic _ICogGra in _Region)
                kCogDisplay.InteractiveGraphics.Add((ICogGraphicInteractive)_ICogGra, "", false);
            GC.Collect();
        }

        //public void DrawFindLineCaliper(CogLineSegment _CogFindLine = null)
        public void DrawFindLineCaliper(CogFindLine _CogFindLine = null)
        {
            ICogRecord _Record;
            CogGraphicCollection _Region;

            LineCaliperTool.InputImage = (CogImage8Grey)kCogDisplay.Image;
            LineCaliperTool.RunParams = _CogFindLine;

            _Record = LineCaliperTool.CreateCurrentRecord();
            InteractiveLine = (CogLineSegment)_Record.SubRecords["InputImage"].SubRecords["ExpectedShapeSegment"].Content;
            _Region = (CogGraphicCollection)_Record.SubRecords["InputImage"].SubRecords["CaliperRegions"].Content;

            kCogDisplay.InteractiveGraphics.Add(InteractiveLine, "Line", false);
            foreach (ICogGraphic _ICogGra in _Region)
                kCogDisplay.InteractiveGraphics.Add((ICogGraphicInteractive)_ICogGra, "", false);
            GC.Collect();
        }

        public CogRectangleAffine GetStaticRectangleAffine()
        {
            CogRectangleAffine cogRectangleAffine = new CogRectangleAffine();
            double CenterX, CenterY, Width, Height, Rotate, Skew;
            StaticRectAffineGraphic.GetCenterLengthsRotationSkew(out CenterX, out CenterY, out Width, out Height, out Rotate, out Skew);
            cogRectangleAffine.SetCenterLengthsRotationSkew(CenterX, CenterY, Width, Height, Rotate, Skew);
            return cogRectangleAffine;
        }

        public CogPointMarker GetInterActivePoint()
        {
            CogPointMarker cogPointMarker = new CogPointMarker();
            double CenterX, CenterY, Rotate;
            int Pixel;
            InteractivePointMarker.GetCenterRotationSize(out CenterX, out CenterY, out Rotate, out Pixel);
            cogPointMarker.SetCenterRotationSize(CenterX, CenterY, Rotate, Pixel);
            return cogPointMarker;
        }

        public CogRectangle GetInterActiveRectangle()
        {
            CogRectangle cogRectangle = new CogRectangle();
            double CenterX, CenterY, Width, Height;
            InteractiveRectGraphic.GetCenterWidthHeight(out CenterX, out CenterY, out Width, out Height);
            cogRectangle.SetCenterWidthHeight(CenterX, CenterY, Width, Height);
            return cogRectangle;
        }

        public CogRectangleAffine GetInterActiveRectangleAffine()
        {
            CogRectangleAffine cogRectangleAffine = new CogRectangleAffine();
            double CenterX, CenterY, Width, Height, Rotate, Skew;
            InteractiveRectAffineGraphic.GetCenterLengthsRotationSkew(out CenterX, out CenterY, out Width, out Height, out Rotate, out Skew);
            cogRectangleAffine.SetCenterLengthsRotationSkew(CenterX, CenterY, Width, Height, Rotate, Skew);
            return cogRectangleAffine;
        }

        public Point GetMousePoint()
        {
            return MousePoint;
        }

        public void DrawText(string _Message, double _StartX, double _StartY, CogColorConstants _Color, int _FontSize = 9, CogGraphicLabelAlignmentConstants _Align = CogGraphicLabelAlignmentConstants.TopLeft)
        {
            System.Drawing.Font _Font = new System.Drawing.Font("나눔바른고딕", _FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            cogLabel.Color = _Color;
            cogLabel.Font = _Font;
            cogLabel.Alignment = _Align;
            cogLabel.SetXYText(_StartX, _StartY, _Message);
            kCogDisplay.StaticGraphics.Add(cogLabel, "Message");
        }

        public void DrawText(string _GroupName, string _Message, double _StartX, double _StartY, CogColorConstants _Color, int _FontSize = 9, CogGraphicLabelAlignmentConstants _Align = CogGraphicLabelAlignmentConstants.TopLeft)
        {
            System.Drawing.Font _Font = new System.Drawing.Font("나눔바른고딕", _FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            cogLabel.Color = _Color;
            cogLabel.Font = _Font;
            cogLabel.Alignment = _Align;
            cogLabel.SetXYText(_StartX, _StartY, _Message);
            kCogDisplay.StaticGraphics.Add(cogLabel, _GroupName);
        }

        public void DrawText(string _Message, double _StartX, double _StartY, CogColorConstants _TextColor, CogColorConstants _BackColor, int _FontSize = 20, CogGraphicLabelAlignmentConstants _Align = CogGraphicLabelAlignmentConstants.TopLeft)
        {
            System.Drawing.Font _Font = new System.Drawing.Font("나눔바른고딕", _FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            CogGraphicLabel AlgoNumLabel = new CogGraphicLabel();

            AlgoNumLabel.BackgroundColor = _BackColor;
            AlgoNumLabel.Color = _TextColor;
            AlgoNumLabel.Font = _Font;
            AlgoNumLabel.Alignment = _Align;
            AlgoNumLabel.SetXYText(_StartX, _StartY, _Message);
            kCogDisplay.StaticGraphics.Add(AlgoNumLabel, "AlgoNumber");
        }

        #endregion Draw Shape Display Window

        #region Display Image get/set
        public class SafeMalloc : SafeBuffer
        {
            public SafeMalloc(int size) : base(true)
            {
                this.SetHandle(Marshal.AllocHGlobal(size));
                this.Initialize((ulong)size);
            }
            protected override bool ReleaseHandle()
            {
                Marshal.FreeHGlobal(this.handle);
                return true;
            }
            public static implicit operator IntPtr(SafeMalloc h)
            {
                return h.handle;
            }
        }

        private void SetDisplayInvoke(KPCogDisplay _Display, CogImage8Grey _DispImage)
        {
            if (_Display.InvokeRequired)
            {
                _Display.Invoke(new MethodInvoker(delegate()
                {
                    //_Display.ClearDisplay();
                    CogImage8Grey _OriginImage = new CogImage8Grey(_DispImage.ToBitmap());
                    _Display.Image = _OriginImage;
                }
                ));
            }
            else
            {
                //_Display.ClearDisplay();
                CogImage8Grey _OriginImage = new CogImage8Grey(_DispImage.ToBitmap());
                _Display.Image = _OriginImage;
            }
        }

        //Color Image Display 용
        private void SetDisplayInvoke(KPCogDisplay _Display, CogImage24PlanarColor _DispImage)
        {
            if (_Display.InvokeRequired)
            {
                _Display.Invoke(new MethodInvoker(delegate ()
                {
                    _Display.ClearDisplay();
                    CogImage24PlanarColor _OriginImage = new CogImage24PlanarColor(_DispImage);
                    _Display.Image = _OriginImage;
                }
                ));
            }
            else
            {
                _Display.ClearDisplay();
                CogImage24PlanarColor _OriginImage = new CogImage24PlanarColor(_DispImage.ToBitmap());
                _Display.Image = _OriginImage;
            }
        }

        /// <summary>
        /// Display control 이미지 추가
        /// </summary>
        /// <param name="_cogDisplayImage">Display 할 이미지</param>
        public void SetDisplayImage(CogImage8Grey _cogDisplayImage)
        {
            try
            {
                SetDisplayInvoke(kCogDisplay, _cogDisplayImage);
            }
            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImage(CogImage8Grey) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        /// <summary>
        /// Display control Color 이미지 추가
        /// </summary>
        /// <param name="_cogDisplayImage">Display 할 이미지</param>
        public void SetDisplayColorImage(string _ImageFilePath)
        {
            CogImageFileTool LoadImageFileTool = new CogImageFileTool();
            CogImage24PlanarColor LoadImage;

            try
            {
                LoadImageFileTool.Operator.Open(_ImageFilePath, CogImageFileModeConstants.Read);
                LoadImageFileTool.Run();
                LoadImage = (CogImage24PlanarColor)LoadImageFileTool.OutputImage;

                SetDisplayInvoke(kCogDisplay, LoadImage);
            }
            catch(Exception ex)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImage(CogImage8Grey) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void SetDisplayImage(byte[] _DisplayImageArray, int _Width, int _Height, double _Rotate)
        {
            try
            {
                var _Buffer = new SafeMalloc(_Width * _Height);
                Marshal.Copy(_DisplayImageArray, 0, _Buffer, _Width * _Height);

                var cogRoot = new CogImage8Root();
                cogRoot.Initialize(_Width, _Height, _Buffer, _Width, _Buffer);

                var _CogImage = new CogImage8Grey();
                _CogImage.SetRoot(cogRoot);

                //LJH 2019.05.13 Rotate 기능 추가
                CogIPOneImageTool _CogOneImageTool = new CogIPOneImageTool();
                CogIPOneImageFlipRotate _FlipRotate = new CogIPOneImageFlipRotate();

                if (_Rotate > 0)
                {
                    if (_Rotate == 90)       _FlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate90Deg;
                    else if (_Rotate == 270) _FlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate270Deg;
                    else if (_Rotate == 180) _FlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate180Deg;

                    _CogOneImageTool.Operators.Add(_FlipRotate);
                    _CogOneImageTool.InputImage = _CogImage;
                    _CogOneImageTool.Run();

                    //_CogOneImageTool.OutputImage.SelectedSpaceName = "#";
                    _CogImage = (CogImage8Grey)_CogOneImageTool.OutputImage;
                }

                SetDisplayInvoke(kCogDisplay, _CogImage);

                _CogOneImageTool.Dispose();
                GC.Collect();
            }
            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImage(byte[]) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void SetDisplayImage(byte[] _DisplayImageArray, int _Width, double _Height)
        {
            try
            {
                UInt32 _BuffSize = (UInt32)(_Width * _Height);
                byte[] _ImageBuff = new byte[_BuffSize];
                _ImageBuff = _DisplayImageArray;
                Bitmap _Bitmap = new Bitmap(_Width, (int)_Height, PixelFormat.Format8bppIndexed);
                BitmapData bmpData = _Bitmap.LockBits(new Rectangle(0, 0, _Bitmap.Width, _Bitmap.Height), ImageLockMode.WriteOnly, _Bitmap.PixelFormat);
                Marshal.Copy(_ImageBuff, 0, bmpData.Scan0, _ImageBuff.Length);
                _Bitmap.UnlockBits(bmpData);

                var _CogImage = new CogImage8Grey(_Bitmap);
                SetDisplayInvoke(kCogDisplay, _CogImage);
                GC.Collect();
            }
            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImage(byte[]) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        /// <summary>
        /// Display의 배율을 설정
        /// </summary>
        /// <param name="_Value"></param>
        public void SetDisplayZoom(double _Value)
        {
            if (kCogDisplay == null) return;
            if (_Value <= 0) _Value = 0.01;
            kCogDisplay.Zoom = _Value;
        }

        public void SetDisplayPanX(double _Value)
        {
            if (kCogDisplay == null) return;
            kCogDisplay.PanX = _Value;
        }

        public void SetDisplayPanY(double _Value)
        {
            if (kCogDisplay == null) return;
            kCogDisplay.PanY = _Value;
        }

        /// <summary>
        /// Display Image 가져오기
        /// </summary>
        /// <returns></returns>
        public ICogImage GetDisplayImage()
        {
            return kCogDisplay.Image;
        }

        /// <summary>
        /// 결과 Display가 포함된 Image 가져오기 
        /// </summary>
        public Image GetResultImage()
        {
            Image _Image = kCogDisplay.CreateContentBitmap(CogDisplayContentBitmapConstants.Image, null);
            return _Image;
        }

        /// <summary>
        /// 현재 Display된 Image 저장하기 
        /// </summary>
        public void SaveDisplayImage(string _DirectoryPath, CogImage8Grey _SaveImage = null)
        {
            DateTime dateTime = DateTime.Now;
            string ImageSaveFolder = _DirectoryPath;
            if (false == Directory.Exists(ImageSaveFolder)) Directory.CreateDirectory(ImageSaveFolder);
            ImageSaveFolder = String.Format("{0}\\{1:D4}\\{2:D2}\\{3:D2}", ImageSaveFolder, dateTime.Year, dateTime.Month, dateTime.Day);
            if (false == Directory.Exists(ImageSaveFolder)) Directory.CreateDirectory(ImageSaveFolder);

            string ImageSaveFile;
            ImageSaveFile = String.Format("{0}\\{1:D2}{2:D2}{3:D2}{4:D3}.bmp", ImageSaveFolder, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
            
            try
            {
                ICogImage _CogSaveImage;
                if (_SaveImage != null)_CogSaveImage = _SaveImage;
                else                   _CogSaveImage = kCogDisplay.Image;
                CogImageFile _CogImageFile = new CogImageFile();
                
                if (_CogSaveImage == null)
                {
                    //MessageBox.Show(new Form{TopMost = true}, "영상이 없습니다.");
                }
                else
                {
                    _CogImageFile.Open(ImageSaveFile, CogImageFileModeConstants.Write);
                    _CogImageFile.Append(_CogSaveImage);
                    _CogImageFile.Close();
                }
            }
            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImage(string) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        /// <summary>
        /// 현재 Display된 Image jpg로 저장하기 
        /// </summary>
        public void SaveDisplayImageJPG(string _DirectoryPath)
        {
            DateTime dateTime = DateTime.Now;
            string ImageSaveFolder = _DirectoryPath;
            if (false == Directory.Exists(ImageSaveFolder)) Directory.CreateDirectory(ImageSaveFolder);
            ImageSaveFolder = String.Format("{0}\\{1:D4}\\{2:D2}\\{3:D2}", ImageSaveFolder, dateTime.Year, dateTime.Month, dateTime.Day);
            if (false == Directory.Exists(ImageSaveFolder)) Directory.CreateDirectory(ImageSaveFolder);

            string ImageSaveFile;
            ImageSaveFile = String.Format("{0}\\{1:D2}{2:D2}{3:D2}{4:D3}.jpg", ImageSaveFolder, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);

            try
            {
                ICogImage _CogSaveImage = kCogDisplay.Image;
                CogImageFile _CogImageFile = new CogImageFile();

                if (_CogSaveImage == null)
                {
                    //MessageBox.Show(new Form{TopMost = true}, "영상이 없습니다.");
                }
                else
                {
                    _CogImageFile.Open(ImageSaveFile, CogImageFileModeConstants.Write);
                    _CogImageFile.Append(_CogSaveImage);
                    _CogImageFile.Close();
                }
            }
            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "SetDisplayImageJPG(string) Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        /// <summary>
        /// Display 
        /// </summary>
        /// <returns></returns>
        public double GetDisplayZoom()
        {
            if (kCogDisplay == null) return 0;
            return kCogDisplay.Zoom;
        }

        public double GetDisplayPanX()
        {
            if (kCogDisplay == null) return 0;
            return kCogDisplay.PanX;
        }

        public double GetDisplayPanY()
        {
            if (kCogDisplay == null) return 0;
            return kCogDisplay.PanY;
        }

        /// <summary>
        /// 마우스 위치의 픽셀 정보를 얻어온다.
        /// </summary>
        /// <returns></returns>
        public int GetPixelValue(int _X, int _Y)
        {
            //kCogDisplay.GetImagePanAnchor
            return kCogDisplay.Image.ToBitmap().GetPixel(_X, _Y).ToArgb();
        }

        /// <summary>
        /// Display Image 유무 확인
        /// </summary>
        /// <returns></returns>
        public bool IsDisplayImage()
        {
            if (kCogDisplay.Image == null) return false;
            else return true;
        }
        #endregion Display Image get/set

        public void FitDisplayControl()
        {
            kCogDisplay.Dock = DockStyle.Fill;
            cogDisplayStatusBarV2.Visible = false;
        }

        public ICogTransform2D GetTransform(string _ToSpace, string _FromSpace)
        {
            ICogTransform2D _TransForm = kCogDisplay.GetTransform(_ToSpace, _FromSpace);
            return _TransForm;
        }

        private void kCogDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            double MousePointX = 0, MousePointY = 0;
            ICogTransform2D _TransForm = kCogDisplay.GetTransform("#", "*");
            _TransForm.MapPoint(e.X, e.Y, out MousePointX, out MousePointY);
            MousePoint.X = Convert.ToInt32(MousePointX);
            MousePoint.Y = Convert.ToInt32(MousePointY);

            var _MousePointEvent = MousePointEvent;
            if (_MousePointEvent != null) _MousePointEvent(MousePoint);
        }

        private void kCogDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            var _CogDisplayMouseUpEvent = CogDisplayMouseUpEvent;
            if (_CogDisplayMouseUpEvent != null)
            {
                //CircleCaliperTool.CreateCurrentRecord();
                //CogDisplayMouseUpEvent(CircleCaliperTool);
                if (CircleCaliperTool.InputImage != null)
                {
                    CircleCaliperTool.CreateCurrentRecord();
                    CogDisplayMouseUpEvent(CircleCaliperTool);
                }

                else if (LineCaliperTool.InputImage != null)
                {
                    LineCaliperTool.CreateCurrentRecord();
                    CogDisplayMouseUpEvent(LineCaliperTool);
                }

                else if (EllipseCaliperTool.InputImage != null)
                {
                    EllipseCaliperTool.CreateCurrentRecord();
                    CogDisplayMouseUpEvent(EllipseCaliperTool);
                }
            }
        }
    }
}
