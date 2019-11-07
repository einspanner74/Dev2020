using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using ParameterManager;

namespace MapDataManager
{
    
    public partial class MapDataWindow : Form
    {
        private enum eSearchType { NORMAL = 0, NORMAL_REV, ZIGZAG, ZIGZAG_REV };

        private MapDataParameter MapDataParam;
        private MapDataParameter MapDataParamBackup;
        private int SearchDirectionType;

        private CogImage8Grey OriginImage;
        private InspectionPattern InspPattern;
        private InspectionBlobReference InspBlobRefer;

        private CogRectangle SelectingRect;
        private CogRectangle SelectedRect;
        private string SelectingRectName;
        private string SelectedRectName;
        private bool IsDrawPatterns;

        private int UnitRowTotalCountBackup = 1;
        private int UnitColTotalCountBackup = 1;

        public delegate void MapDataParameterSaveHandler(MapDataParameter _MapDataParam, int _ID);
        public event MapDataParameterSaveHandler MapDataParameterSaveEvent;

        #region Initialize & DeInitialize
        public MapDataWindow()
        {
            InitializeComponent();
        }

        public void Initialize(MapDataParameter _MapDataParam)
        {
            SetMapDataParameter(_MapDataParam);
            InspPattern = new InspectionPattern();
            InspBlobRefer = new InspectionBlobReference();

            SelectingRect = null;
            SelectedRect = null;
            kpTeachDisplay.MousePointEvent += new KPDisplay.KPCogDisplayControl.MousePointHandler(TeachDisplayDownEventFunction);
        }

        public void DeInitialize()
        {

        }

        private void SetMapDataParameter(MapDataParameter _MapDataParam)
        {
            MapDataParam = new MapDataParameter();
            MapDataParamBackup = new MapDataParameter();
            CParameterManager.RecipeCopy(_MapDataParam, ref MapDataParam);
            CParameterManager.RecipeCopy(_MapDataParam, ref MapDataParamBackup);

            numUpDownUnitRowCount.Value         = Convert.ToDecimal(MapDataParam.Info.UnitRowCount);
            numUpDownUnitColumnCount.Value      = Convert.ToDecimal(MapDataParam.Info.UnitColumnCount);
            numUpDownSectionRowCount.Value      = Convert.ToDecimal(MapDataParam.Info.SectionRowCount);
            numUpDownSectionColumnCount.Value   = Convert.ToDecimal(MapDataParam.Info.SectionColumnCount);
            numericUpDownFindCount.Value        = Convert.ToDecimal(MapDataParam.Info.FindCount);
            numericUpDownFindScore.Value        = Convert.ToDecimal(MapDataParam.Info.FindScore);
            numericUpDownAngleLimit.Value       = Convert.ToDecimal(MapDataParam.Info.AngleLimit);

            hScrollBarThreshold.Value           = MapDataParam.MapID.SearchThreshold;
            hScrollBarWidthSizeMin.Value        = MapDataParam.MapID.SearchSizeMin;
            hScrollBarWidthSizeMax.Value        = MapDataParam.MapID.SearchSizeMax;
            graLabelThresholdValue.Text         = MapDataParam.MapID.SearchThreshold.ToString();
            graLabelWidthMin.Text               = MapDataParam.MapID.SearchSizeMin.ToString();
            graLabelWidthMax.Text               = MapDataParam.MapID.SearchSizeMax.ToString();

            ckMapIDUsable.Checked               = MapDataParam.MapID.IsUsableMapID;
            chAreaAutoSearch.Checked            = Convert.ToBoolean(MapDataParam.Info.MapDataTeachingMode);
            chAreaManualSearch.Checked          = !Convert.ToBoolean(MapDataParam.Info.MapDataTeachingMode);

            SetSearchType(MapDataParam.Info.SearchType);

            if (MapDataParam.Info.UnitPattern != null && MapDataParam.Info.UnitPattern.Trained == true)
                kpPatternDisplay.SetDisplayImage((CogImage8Grey)MapDataParam.Info.UnitPattern.GetTrainedPatternImage());
        }

        public MapDataParameter GetCurrentMapDataParameter()
        {
            return MapDataParam;
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion Control Default Event

        #region Control Event
        public void SetMapDataImage(CogImage8Grey _OriginImage)
        {
            OriginImage = _OriginImage;
            kpTeachDisplay.SetDisplayImage(_OriginImage);
        }

        private void btnMapIDSearchAreaShow_Click(object sender, EventArgs e)
        {
            CogRectangle _MapIDRegion = new CogRectangle();
            _MapIDRegion.SetCenterWidthHeight(MapDataParam.MapID.SearchAreaCenterX, MapDataParam.MapID.SearchAreaCenterY, MapDataParam.MapID.SearchAreaWidth, MapDataParam.MapID.SearchAreaHeight);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawInterActiveShape(_MapIDRegion, "MapIDRegion", CogColorConstants.Green);
        }

        private void btnMapIDSet_Click(object sender, EventArgs e)
        {
            CogRectangle _MapIDRegion = kpTeachDisplay.GetInterActiveRectangle();
            kpTeachDisplay.DrawStaticShape(_MapIDRegion, "PatternRegion", CogColorConstants.Green, 2, CogGraphicLineStyleConstants.Dash);

            MapDataParam.MapID.SearchAreaCenterX = _MapIDRegion.CenterX;
            MapDataParam.MapID.SearchAreaCenterY = _MapIDRegion.CenterY;
            MapDataParam.MapID.SearchAreaWidth = _MapIDRegion.Width;
            MapDataParam.MapID.SearchAreaHeight = _MapIDRegion.Height;
        }

        private void btnUnitPatternAreaShow_Click(object sender, EventArgs e)
        {
            #region Button Status
            btnUnitPatternSearchAreaShow.Enabled = false;   btnUnitPatternSearchAreaShow.BackColor = Color.Gray;
            btnUnitPatternSearchAreaSet.Enabled = false;    btnUnitPatternSearchAreaSet.BackColor = Color.Gray;
            btnUnitPatternOriginCenterSet.Enabled = true;   btnUnitPatternOriginCenterSet.BackColor = Color.PaleGreen;
            btnUnitPatternAreaShow.Enabled = false;         btnUnitPatternAreaShow.BackColor = Color.Gray;
            btnUnitPatternAreaSet.Enabled = true;           btnUnitPatternAreaSet.BackColor = Color.PaleGreen;
            //btnUnitPatternAreaCancel.Enabled = true;
            #endregion Button Status

            CogRectangle _PatternRegion = new CogRectangle();
            CogRectangle _PatternSearchRegion = new CogRectangle();
            _PatternRegion.SetCenterWidthHeight(MapDataParam.Unit.PatternAreaCenterX, MapDataParam.Unit.PatternAreaCenterY, MapDataParam.Unit.PatternAreaWidth, MapDataParam.Unit.PatternAreaHeight);
            _PatternSearchRegion.SetCenterWidthHeight(MapDataParam.Unit.SearchAreaCenterX, MapDataParam.Unit.SearchAreaCenterY, MapDataParam.Unit.SearchAreaWidth, MapDataParam.Unit.SearchAreaHeight);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawInterActiveShape(_PatternRegion, "PatternRegion", CogColorConstants.Green);
            kpTeachDisplay.DrawStaticShape(_PatternSearchRegion, "PatternSearchRegion", CogColorConstants.Orange, 2, CogGraphicLineStyleConstants.Dash);
        }

        private void btnUnitPatternAreaSet_Click(object sender, EventArgs e)
        {
            #region Button Status
            btnUnitPatternSearchAreaShow.Enabled = true;        btnUnitPatternSearchAreaShow.BackColor = Color.SandyBrown;
            btnUnitPatternSearchAreaSet.Enabled = false;        btnUnitPatternSearchAreaSet.BackColor = Color.Gray;
            btnUnitPatternOriginCenterSet.Enabled = false;      btnUnitPatternOriginCenterSet.BackColor = Color.Gray;
            btnUnitPatternAreaShow.Enabled = true;              btnUnitPatternAreaShow.BackColor = Color.PaleGreen;
            btnUnitPatternAreaSet.Enabled = false;              btnUnitPatternAreaSet.BackColor = Color.Gray;
            //btnUnitPatternAreaCancel.Enabled = false;
            #endregion Button Status

            CogRectangle _PatternRegion = kpTeachDisplay.GetInterActiveRectangle();
            CogPointMarker _PatternOrigin = new CogPointMarker();
            _PatternOrigin.SetCenterRotationSize(_PatternRegion.CenterX, _PatternRegion.CenterY, 0, 2);

            kpTeachDisplay.ClearDisplay("PatternRegion");
            kpTeachDisplay.ClearDisplay("PatternOriginPoint");
            kpTeachDisplay.DrawStaticShape(_PatternRegion, "PatternRegion", CogColorConstants.Green, 2, CogGraphicLineStyleConstants.Dash);
            kpTeachDisplay.DrawStaticShape(_PatternOrigin, "PatternOriginPoint", CogColorConstants.Green, 14);

            MapDataParam.Unit.PatternAreaCenterX = _PatternRegion.CenterX;
            MapDataParam.Unit.PatternAreaCenterY = _PatternRegion.CenterY;
            MapDataParam.Unit.PatternAreaWidth = _PatternRegion.Width;
            MapDataParam.Unit.PatternAreaHeight = _PatternRegion.Height;
            MapDataParam.Unit.PatternAreaOriginX = _PatternOrigin.X;
            MapDataParam.Unit.PatternAreaOriginY = _PatternOrigin.Y;

            //Pattern 등록
            CogPMAlignPattern _Pattern = InspPattern.GetPatternReference(OriginImage, _PatternRegion, _PatternOrigin.X, _PatternOrigin.Y);
            kpPatternDisplay.SetDisplayImage((CogImage8Grey)_Pattern.GetTrainedPatternImage());
            MapDataParam.Info.UnitPattern = _Pattern;

            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = false;
        }

        private void btnUnitPatternOriginCenterSet_Click(object sender, EventArgs e)
        {
            CogRectangle _PatternRegion = kpTeachDisplay.GetInterActiveRectangle();
            CogPointMarker _PatternOrigin = new CogPointMarker();
            _PatternOrigin.SetCenterRotationSize(_PatternRegion.CenterX, _PatternRegion.CenterY, 0, 2);

            kpTeachDisplay.ClearDisplay("PatternOriginPoint");
            kpTeachDisplay.DrawInterActiveShape(_PatternOrigin, "PatternOriginPoint", CogColorConstants.Green, 14);
        }

        private void btnUnitPatternSearchAreaShow_Click(object sender, EventArgs e)
        {
            #region Button Status
            btnUnitPatternSearchAreaShow.Enabled = false;       btnUnitPatternSearchAreaShow.BackColor = Color.Gray;
            btnUnitPatternSearchAreaSet.Enabled = true;         btnUnitPatternSearchAreaSet.BackColor = Color.SandyBrown;
            btnUnitPatternOriginCenterSet.Enabled = false;      btnUnitPatternOriginCenterSet.BackColor = Color.Gray;
            btnUnitPatternAreaShow.Enabled = false;             btnUnitPatternAreaShow.BackColor = Color.Gray;
            btnUnitPatternAreaSet.Enabled = false;              btnUnitPatternAreaSet.BackColor = Color.Gray;
            //btnUnitPatternAreaCancel.Enabled = false;
            #endregion Button Status

            CogRectangle _PatternRegion = new CogRectangle();
            CogRectangle _PatternSearchRegion = new CogRectangle();
            _PatternRegion.SetCenterWidthHeight(MapDataParam.Unit.PatternAreaCenterX, MapDataParam.Unit.PatternAreaCenterY, MapDataParam.Unit.PatternAreaWidth, MapDataParam.Unit.PatternAreaHeight);
            _PatternSearchRegion.SetCenterWidthHeight(MapDataParam.Unit.SearchAreaCenterX, MapDataParam.Unit.SearchAreaCenterY, MapDataParam.Unit.SearchAreaWidth, MapDataParam.Unit.SearchAreaHeight);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawInterActiveShape(_PatternSearchRegion, "PatternSearchRegion", CogColorConstants.Orange);
            kpTeachDisplay.DrawStaticShape(_PatternRegion, "PatternRegion", CogColorConstants.Green, 2, CogGraphicLineStyleConstants.Dash);
        }

        private void btnUnitPatternSearchAreaSet_Click(object sender, EventArgs e)
        {
            #region Button Status
            btnUnitPatternSearchAreaShow.Enabled = true;        btnUnitPatternSearchAreaShow.BackColor = Color.SandyBrown;
            btnUnitPatternSearchAreaSet.Enabled = false;        btnUnitPatternSearchAreaSet.BackColor = Color.Gray;
            btnUnitPatternOriginCenterSet.Enabled = false;      btnUnitPatternOriginCenterSet.BackColor = Color.Gray;
            btnUnitPatternAreaShow.Enabled = true;              btnUnitPatternAreaShow.BackColor = Color.PaleGreen;
            btnUnitPatternAreaSet.Enabled = false;              btnUnitPatternAreaSet.BackColor = Color.Gray;
            //btnUnitPatternAreaCancel.Enabled = false;
            #endregion Button Status

            CogRectangle _PatternSearchRegion = kpTeachDisplay.GetInterActiveRectangle();
            kpTeachDisplay.ClearDisplay("PatternSearchRegion");
            kpTeachDisplay.DrawStaticShape(_PatternSearchRegion, "PatternSearchRegion", CogColorConstants.Orange, 2, CogGraphicLineStyleConstants.Dash);
            MapDataParam.Unit.SearchAreaCenterX = _PatternSearchRegion.CenterX;
            MapDataParam.Unit.SearchAreaCenterY = _PatternSearchRegion.CenterY;
            MapDataParam.Unit.SearchAreaWidth = _PatternSearchRegion.Width;
            MapDataParam.Unit.SearchAreaHeight = _PatternSearchRegion.Height;

            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = false;
        }

        private void btnUnitPatternAreaCancel_Click(object sender, EventArgs e)
        {
            #region Button Status
            btnUnitPatternSearchAreaShow.Enabled = true;         btnUnitPatternSearchAreaShow.BackColor = Color.SandyBrown;
            btnUnitPatternSearchAreaSet.Enabled = false;         btnUnitPatternSearchAreaSet.BackColor = Color.Gray;
            btnUnitPatternOriginCenterSet.Enabled = false;       btnUnitPatternOriginCenterSet.BackColor = Color.Gray;
            btnUnitPatternAreaShow.Enabled = true;               btnUnitPatternAreaShow.BackColor = Color.PaleGreen;
            btnUnitPatternAreaSet.Enabled = false;               btnUnitPatternAreaSet.BackColor = Color.Gray;
            //btnUnitPatternAreaCancel.Enabled = false;
            #endregion Button Status

            kpTeachDisplay.ClearDisplay();
            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = false;
        }

        private void btnFindSearchArea_Click(object sender, EventArgs e)
        {
            double _OffsetX = MapDataParam.Unit.PatternAreaOriginX - MapDataParam.Unit.PatternAreaCenterX;
            double _OffsetY = MapDataParam.Unit.PatternAreaOriginY - MapDataParam.Unit.PatternAreaCenterY;

            InspPattern.SetPatternReference(MapDataParam.Info.UnitPattern);
            InspPattern.SetMatchingParameter(MapDataParam.Info.FindCount, MapDataParam.Info.FindScore);

            if (false == InspPattern.Run(OriginImage)) { MessageBox.Show("Pattern Not Found"); return; }

            CogPMAlignResults _PatternResult = InspPattern.GetResults();
            if (null == _PatternResult) return;

            MapDataParam.Info.UnitListCenterX.Clear();
            MapDataParam.Info.UnitListCenterY.Clear();
            MapDataParam.Info.UnitListWidth.Clear();
            MapDataParam.Info.UnitListHeight.Clear();
            if (_PatternResult.Count > 0)
            {
                double[] _OriginX, _OriginY, _CenterX, _CenterY, _Width, _Height;
                _OriginX = new double[_PatternResult.Count];
                _OriginY = new double[_PatternResult.Count];
                _CenterX = new double[_PatternResult.Count];
                _CenterY = new double[_PatternResult.Count];
                _Width   = new double[_PatternResult.Count];
                _Height  = new double[_PatternResult.Count];

                uint _RowCount = Convert.ToUInt32(numUpDownUnitRowCount.Value);
                uint _ColCount = Convert.ToUInt32(numUpDownUnitColumnCount.Value);
                CenterPoint[] _CenterPointArray = new CenterPoint[_RowCount * _ColCount];
                for (int iLoopCount = 0; iLoopCount < _PatternResult.Count; ++iLoopCount)
                {
                    _OriginX[iLoopCount] = _PatternResult[iLoopCount].GetPose().TranslationX;
                    _OriginY[iLoopCount] = _PatternResult[iLoopCount].GetPose().TranslationY;
                    _CenterX[iLoopCount] = _PatternResult[iLoopCount].GetPose().TranslationX - _OffsetX;
                    _CenterY[iLoopCount] = _PatternResult[iLoopCount].GetPose().TranslationY - _OffsetY;
                    _Width[iLoopCount]  = _PatternResult.GetTrainArea().Width;
                    _Height[iLoopCount] = _PatternResult.GetTrainArea().Height;

                    CogRectangle _FindPattern = new CogRectangle();
                    _FindPattern.SetCenterWidthHeight(_CenterX[iLoopCount], _CenterY[iLoopCount], MapDataParam.Unit.SearchAreaWidth, MapDataParam.Unit.SearchAreaHeight);
                    kpTeachDisplay.DrawStaticShape(_FindPattern, "SearchArea" + (iLoopCount + 1));

                    CogPointMarker _OriginPoint = new CogPointMarker();
                    _OriginPoint.SetCenterRotationSize(_OriginX[iLoopCount], _OriginY[iLoopCount], 0, 2);
                    kpTeachDisplay.DrawStaticShape(_OriginPoint, "PatternOrigin" + (iLoopCount + 1), CogColorConstants.Green, 12);

                    MapDataParam.Info.UnitListCenterX.Add(_CenterX[iLoopCount]);
                    MapDataParam.Info.UnitListCenterY.Add(_CenterY[iLoopCount]);
                    MapDataParam.Info.UnitListWidth.Add(_Width[iLoopCount]);
                    MapDataParam.Info.UnitListHeight.Add(_Height[iLoopCount]);

                    if (_CenterPointArray.Length > iLoopCount)
                    {
                        _CenterPointArray[iLoopCount] = new CenterPoint();
                        _CenterPointArray[iLoopCount].X = _CenterX[iLoopCount];
                        _CenterPointArray[iLoopCount].Y = _CenterY[iLoopCount];
                    }
                }

                if ((_RowCount * _ColCount) != _PatternResult.Count)
                {
                    MessageBox.Show("Unit 개수와 Find Result 개수가 맞지 않습니다.");
                    return;
                }

                else
                {
                    CenterPoint[,] _SortedCenterPoint = CenterPointSort(_RowCount, _ColCount, _CenterPointArray);

                    MapDataParam.Info.UnitListCenterX.Clear();
                    MapDataParam.Info.UnitListCenterY.Clear();
                    for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
                    {
                        for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                        {
                            MapDataParam.Info.UnitListCenterX.Add(_SortedCenterPoint[iLoopCount, jLoopCount].X);
                            MapDataParam.Info.UnitListCenterY.Add(_SortedCenterPoint[iLoopCount, jLoopCount].Y);
                        }

                    }

                    SelectingRectName = SelectedRectName = "";
                    IsDrawPatterns = true;
                    MessageBox.Show("Pattern Find Complete");
                }
            }
        }

        private void btnShowSearchArea_Click(object sender, EventArgs e)
        {
            if (MapDataParam.Info.UnitListCenterX == null || MapDataParam.Info.UnitListCenterY == null) return;
            if (MapDataParam.Info.UnitListCenterX.Count == 0 || MapDataParam.Info.UnitListCenterY.Count == 0) return;
            if (MapDataParam.Info.UnitListCenterX.Count != MapDataParam.Info.UnitListCenterY.Count) return;

            uint _RowCount = Convert.ToUInt32(numUpDownUnitRowCount.Value);
            uint _ColCount = Convert.ToUInt32(numUpDownUnitColumnCount.Value);
            int _TotalCount = MapDataParam.Info.UnitListCenterX.Count;
            CenterPoint[] _CenterPointArray = new CenterPoint[_TotalCount];

            if (_RowCount * _ColCount != _CenterPointArray.Length)  {   MessageBox.Show("설정 환경이 일치하지 않음."); return; }
            for (int iLoopCount = 0; iLoopCount < _TotalCount; ++iLoopCount)
            {
                _CenterPointArray[iLoopCount] = new CenterPoint();
                _CenterPointArray[iLoopCount].X = MapDataParam.Info.UnitListCenterX[iLoopCount];
                _CenterPointArray[iLoopCount].Y = MapDataParam.Info.UnitListCenterY[iLoopCount];
            }
            CenterPoint[,] _SortedCenterPoint = CenterPointSort((uint)_RowCount, (uint)_ColCount, _CenterPointArray);
            MapDataParam.Info.UnitListCenterX.Clear();
            MapDataParam.Info.UnitListCenterY.Clear();
            for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
            {
                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                {
                    MapDataParam.Info.UnitListCenterX.Add(_SortedCenterPoint[iLoopCount, jLoopCount].X);
                    MapDataParam.Info.UnitListCenterY.Add(_SortedCenterPoint[iLoopCount, jLoopCount].Y);
                }
            }

            kpTeachDisplay.ClearDisplay();
            for (int iLoopCount = 0; iLoopCount < MapDataParam.Info.UnitListCenterX.Count; ++iLoopCount)
            {
                CogRectangle _FindPattern = new CogRectangle();
                _FindPattern.SetCenterWidthHeight(MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount], 
                                                  MapDataParam.Info.UnitListWidth[iLoopCount], MapDataParam.Info.UnitListHeight[iLoopCount]);
                kpTeachDisplay.DrawStaticShape(_FindPattern, "SearchArea" + (iLoopCount + 1));

                CogPointMarker _OriginPoint = new CogPointMarker();
                _OriginPoint.SetCenterRotationSize(MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount], 0, 2);
                kpTeachDisplay.DrawStaticShape(_OriginPoint, "PatternOrigin" + (iLoopCount + 1), CogColorConstants.Green, 12);

                kpTeachDisplay.DrawText((iLoopCount + 1).ToString(), MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount] - 15, CogColorConstants.Green, 10);
            }

            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int _ID = 0; //ID 확인 UI 추가 필요

            MapDataParam.Info.UnitRowCount = Convert.ToUInt32(numUpDownUnitRowCount.Value);
            MapDataParam.Info.UnitColumnCount = Convert.ToUInt32(numUpDownUnitColumnCount.Value);
            MapDataParam.Info.SectionRowCount = Convert.ToUInt32(numUpDownSectionRowCount.Value);
            MapDataParam.Info.SectionColumnCount = Convert.ToUInt32(numUpDownSectionColumnCount.Value);
            MapDataParam.Info.SearchType = SearchDirectionType;
            MapDataParam.Info.FindCount = Convert.ToUInt32(numericUpDownFindCount.Value);
            MapDataParam.Info.FindScore = Convert.ToDouble(numericUpDownFindScore.Value);
            MapDataParam.Info.AngleLimit = Convert.ToDouble(numericUpDownAngleLimit.Value);
            MapDataParam.Info.UnitTotalCount = Convert.ToUInt32(txtUnitTotalCount.Text);
            MapDataParam.Info.MapDataTeachingMode = Convert.ToInt32(chAreaAutoSearch.Checked);

            MapDataParam.MapID.SearchThreshold = Convert.ToInt32(graLabelThresholdValue.Text);
            MapDataParam.MapID.SearchSizeMin = Convert.ToInt32(graLabelWidthMin.Text);
            MapDataParam.MapID.SearchSizeMax = Convert.ToInt32(graLabelWidthMax.Text);
            MapDataParam.MapID.BlobAreaSizeMin = 100;
            MapDataParam.MapID.BlobAreaSizeMax = 50000;

            MapDataParam.MapID.IsUsableMapID = ckMapIDUsable.Checked;

            var _MapDataParameterSaveEvent = MapDataParameterSaveEvent;
            _MapDataParameterSaveEvent?.Invoke(MapDataParam, _ID);
        }

        private void btnManualSearchArea_Click(object sender, EventArgs e)
        {
            CogRectangle _WholePatternRegion = new CogRectangle();
            if (MapDataParam != null)
            {
                _WholePatternRegion.SetCenterWidthHeight(MapDataParam.Whole.SearchAreaCenterX, MapDataParam.Whole.SearchAreaCenterY,
                                                         MapDataParam.Whole.SearchAreaWidth, MapDataParam.Whole.SearchAreaHeight);
            }

            else
            {
                MapDataParam = new MapDataParameter();
                _WholePatternRegion.SetCenterWidthHeight(800, 800, 500, 500);
            }

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawInterActiveShape(_WholePatternRegion, "WholePatternRegion", CogColorConstants.Orange);
        }

        private void btnManualSearchAreaSet_Click(object sender, EventArgs e)
        {
            uint _RowCount = Convert.ToUInt32(numUpDownUnitRowCount.Value);
            uint _ColCount = Convert.ToUInt32(numUpDownUnitColumnCount.Value);

            CogRectangle _WholePatternRegion = kpTeachDisplay.GetInterActiveRectangle();
            MapDataParam.Whole.SearchAreaCenterX = _WholePatternRegion.CenterX;
            MapDataParam.Whole.SearchAreaCenterY = _WholePatternRegion.CenterY;
            MapDataParam.Whole.SearchAreaWidth = _WholePatternRegion.Width;
            MapDataParam.Whole.SearchAreaHeight = _WholePatternRegion.Height;

            double _Width, _Height;
            _Width = _WholePatternRegion.Width / _ColCount;
            _Height = _WholePatternRegion.Height / _RowCount;

            double _StartCenterPointX, _StartCenterPointY, _CenterPointX, _CenterPointY;
            _StartCenterPointX = _WholePatternRegion.X + _Width / 2;
            _StartCenterPointY = _WholePatternRegion.Y + _Height / 2;

            kpTeachDisplay.ClearDisplay();
            MapDataParam.Info.UnitListCenterX.Clear();
            MapDataParam.Info.UnitListCenterY.Clear();
            MapDataParam.Info.UnitListWidth.Clear();
            MapDataParam.Info.UnitListHeight.Clear();
            CogRectangle[,] _PatternRegions = new CogRectangle[_RowCount, _ColCount];
            CenterPoint[] _CenterPointArray = new CenterPoint[_RowCount * _ColCount];
            int _Index = 0;
            for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
            {
                _CenterPointY = _StartCenterPointY + (_Height * iLoopCount);
                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                {
                    _CenterPointX = _StartCenterPointX + (_Width * jLoopCount);

                    _PatternRegions[iLoopCount, jLoopCount] = new CogRectangle();
                    _PatternRegions[iLoopCount, jLoopCount].SetCenterWidthHeight(_CenterPointX, _CenterPointY, _Width, _Height);

                    MapDataParam.Info.UnitListCenterX.Add(_PatternRegions[iLoopCount, jLoopCount].CenterX);
                    MapDataParam.Info.UnitListCenterY.Add(_PatternRegions[iLoopCount, jLoopCount].CenterY);
                    MapDataParam.Info.UnitListWidth.Add(_PatternRegions[iLoopCount, jLoopCount].Width);
                    MapDataParam.Info.UnitListHeight.Add(_PatternRegions[iLoopCount, jLoopCount].Height);

                    _CenterPointArray[_Index] = new CenterPoint();
                    _CenterPointArray[_Index].X = _PatternRegions[iLoopCount, jLoopCount].CenterX;
                    _CenterPointArray[_Index].Y = _PatternRegions[iLoopCount, jLoopCount].CenterY;
                    _Index++;
                }
            }

            CenterPoint[,] _SortedCenterPoint = CenterPointSort(_RowCount, _ColCount, _CenterPointArray);
            MapDataParam.Info.UnitListCenterX.Clear();
            MapDataParam.Info.UnitListCenterY.Clear();
            for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
            {
                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                {
                    MapDataParam.Info.UnitListCenterX.Add(_SortedCenterPoint[iLoopCount, jLoopCount].X);
                    MapDataParam.Info.UnitListCenterY.Add(_SortedCenterPoint[iLoopCount, jLoopCount].Y);
                }
            }

            btnShowSearchArea_Click(null, null);
        }

        private void btnUnitManualAreaCancel_Click(object sender, EventArgs e)
        {
            kpTeachDisplay.ClearDisplay();
            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = false;
        }

        private void numUpDownUnitRowCount_ValueChanged(object sender, EventArgs e)
        {
            int _RowCount = Convert.ToInt32(numUpDownUnitRowCount.Value);
            int _ColCount = Convert.ToInt32(numUpDownUnitColumnCount.Value);

            if ((_RowCount * _ColCount) > 20 && _RowCount < _ColCount)
            {
                MessageBox.Show("Row Count가 Column Count보다 작습니다.");
                numUpDownUnitRowCount.Value = UnitRowTotalCountBackup;
                return;
            }

            txtUnitTotalCount.Text = (Convert.ToInt32(numUpDownUnitRowCount.Value) * Convert.ToInt32(numUpDownUnitColumnCount.Value)).ToString();
            UnitRowTotalCountBackup = Convert.ToInt32(numUpDownUnitRowCount.Value);
        }

        private void numUpDownUnitColumnCount_ValueChanged(object sender, EventArgs e)
        {
            int _RowCount = Convert.ToInt32(numUpDownUnitRowCount.Value);
            int _ColCount = Convert.ToInt32(numUpDownUnitColumnCount.Value);

            if ((_RowCount * _ColCount) > 20 && _RowCount < _ColCount)
            {
                MessageBox.Show("Column Count가 Row Count보다 큽니다.");
                numUpDownUnitColumnCount.Value = UnitColTotalCountBackup;
                return;
            }

            txtUnitTotalCount.Text = (Convert.ToInt32(numUpDownUnitRowCount.Value) * Convert.ToInt32(numUpDownUnitColumnCount.Value)).ToString();
            UnitColTotalCountBackup = Convert.ToInt32(numUpDownUnitColumnCount.Value);
        }

        private void chAreaAutoSearch_CheckedChanged(object sender, EventArgs e)
        {
            chAreaManualSearch.Checked = !chAreaAutoSearch.Checked;

            if (true == chAreaAutoSearch.Checked)
            {
                //panel2.Visible = true;
                //panel3.Visible = false;
                //panel2.Location = new Point(639, 404);
            }
        }

        private void chAreaManualSearch_CheckedChanged(object sender, EventArgs e)
        {
            chAreaAutoSearch.Checked = !chAreaManualSearch.Checked;

            if (true == chAreaManualSearch.Checked)
            {
                //panel2.Visible = false;
                //panel3.Visible = true;
                //panel3.Location = new Point(639, 404);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CParameterManager.RecipeCopy(MapDataParamBackup, ref MapDataParam);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion Control Event

        private void ShowSearchArea()
        {
            if (MapDataParam.Info.UnitListCenterX == null || MapDataParam.Info.UnitListCenterY == null) return;
            if (MapDataParam.Info.UnitListCenterX.Count == 0 || MapDataParam.Info.UnitListCenterY.Count == 0) return;
            if (MapDataParam.Info.UnitListCenterX.Count != MapDataParam.Info.UnitListCenterY.Count) return;

            uint _RowCount = Convert.ToUInt32(numUpDownUnitRowCount.Value);
            uint _ColCount = Convert.ToUInt32(numUpDownUnitColumnCount.Value);
            int _TotalCount = MapDataParam.Info.UnitListCenterX.Count;

            if (_TotalCount != (_RowCount * _ColCount)) { MessageBox.Show(string.Format("Total : {0}, Row : {1}, Column : {2}", _TotalCount, _RowCount, _ColCount)); return; }
            CenterPoint[] _CenterPointArray = new CenterPoint[_TotalCount];
            for (int iLoopCount = 0; iLoopCount < _TotalCount; ++iLoopCount)
            {
                _CenterPointArray[iLoopCount] = new CenterPoint();
                _CenterPointArray[iLoopCount].X = MapDataParam.Info.UnitListCenterX[iLoopCount];
                _CenterPointArray[iLoopCount].Y = MapDataParam.Info.UnitListCenterY[iLoopCount];
            }
            CenterPoint[,] _SortedCenterPoint = CenterPointSort((uint)_RowCount, (uint)_ColCount, _CenterPointArray);
            MapDataParam.Info.UnitListCenterX.Clear();
            MapDataParam.Info.UnitListCenterY.Clear();
            for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
            {
                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                {
                    MapDataParam.Info.UnitListCenterX.Add(_SortedCenterPoint[iLoopCount, jLoopCount].X);
                    MapDataParam.Info.UnitListCenterY.Add(_SortedCenterPoint[iLoopCount, jLoopCount].Y);
                }
            }

            kpTeachDisplay.ClearDisplay();
            for (int iLoopCount = 0; iLoopCount < MapDataParam.Info.UnitListCenterX.Count; ++iLoopCount)
            {
                CogRectangle _FindPattern = new CogRectangle();
                _FindPattern.SetCenterWidthHeight(MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount],
                                                  MapDataParam.Info.UnitListWidth[iLoopCount], MapDataParam.Info.UnitListHeight[iLoopCount]);
                kpTeachDisplay.DrawStaticShape(_FindPattern, "SearchArea" + (iLoopCount + 1));

                CogPointMarker _OriginPoint = new CogPointMarker();
                _OriginPoint.SetCenterRotationSize(MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount], 0, 2);
                kpTeachDisplay.DrawStaticShape(_OriginPoint, "PatternOrigin" + (iLoopCount + 1), CogColorConstants.Green, 12);

                kpTeachDisplay.DrawText((iLoopCount + 1).ToString(), MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount] - 15, CogColorConstants.Green, 10);
            }

            SelectingRectName = SelectedRectName = "";
            IsDrawPatterns = true;
        }

        private CenterPoint[,] CenterPointSort(uint _RowCount, uint _ColCount, CenterPoint[] _CenterPointArray)
        {
            int _Index = 0;
            CenterPoint[,] _SortedCenterPoint = new CenterPoint[_RowCount, _ColCount];

            //Y 축 방향으로 오름차순 정렬
            Array.Sort(_CenterPointArray, delegate (CenterPoint _First, CenterPoint _Second) { return _First.Y.CompareTo(_Second.Y); });
            
            for (int iLoopCount = 0; iLoopCount < _RowCount; ++iLoopCount)
            {
                var _CenterPointReferY = new CenterPoint[_ColCount];
                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                    _CenterPointReferY[jLoopCount] = _CenterPointArray[_Index++];

                //if (iLoopCount % 2 == 1)    Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _Second.X.CompareTo(_First.X); }); //지그재그
                //else                        Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _First.X.CompareTo(_Second.X); }); //노멀

                if (eSearchType.NORMAL == (eSearchType)MapDataParam.Info.SearchType)
                {
                    Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _First.X.CompareTo(_Second.X); }); //노멀
                }

                else if (eSearchType.NORMAL_REV == (eSearchType)MapDataParam.Info.SearchType)
                {
                    Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _Second.X.CompareTo(_First.X); }); //노멀
                }

                else if (eSearchType.ZIGZAG == (eSearchType)MapDataParam.Info.SearchType)
                {
                    if (iLoopCount % 2 == 1)    Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _Second.X.CompareTo(_First.X); });
                    else                        Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _First.X.CompareTo(_Second.X); });
                }

                else if (eSearchType.ZIGZAG_REV == (eSearchType)MapDataParam.Info.SearchType)
                {
                    if (iLoopCount % 2 == 0)    Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _Second.X.CompareTo(_First.X); }); //지그재그
                    else                        Array.Sort(_CenterPointReferY, delegate (CenterPoint _First, CenterPoint _Second) { return _First.X.CompareTo(_Second.X); }); //노멀
                }

                for (int jLoopCount = 0; jLoopCount < _ColCount; ++jLoopCount)
                {
                    _SortedCenterPoint[iLoopCount, jLoopCount] = new CenterPoint();
                    _SortedCenterPoint[iLoopCount, jLoopCount] = _CenterPointReferY[jLoopCount];
                }
            }

            return _SortedCenterPoint;
        }

        private void TeachDisplayDownEventFunction(Point _MousePoint)
        {
            if (false == IsDrawPatterns) return;
            if (MapDataParam.Info.UnitListCenterX == null || MapDataParam.Info.UnitListCenterY == null) return;
            if (MapDataParam.Info.UnitListCenterX.Count == 0 || MapDataParam.Info.UnitListCenterY.Count == 0) return;
            if (MapDataParam.Info.UnitListCenterX.Count != MapDataParam.Info.UnitListCenterY.Count) return;

            for (int iLoopCount = 0; iLoopCount < MapDataParam.Info.UnitListCenterX.Count; ++iLoopCount)
            {
                Rectangle _Rect = new Rectangle((int)MapDataParam.Info.UnitListCenterX[iLoopCount] - (int)(MapDataParam.Unit.SearchAreaWidth / 2), (int)(MapDataParam.Info.UnitListCenterY[iLoopCount] - MapDataParam.Unit.SearchAreaHeight / 2), 
                                                (int)MapDataParam.Info.UnitListWidth[iLoopCount], (int)MapDataParam.Info.UnitListHeight[iLoopCount]);
                if (true == _Rect.Contains(_MousePoint.X, _MousePoint.Y))
                {
                    //현재 클릭한 Rect 정보
                    SelectingRect = new CogRectangle();
                    SelectingRect.SetCenterWidthHeight(MapDataParam.Info.UnitListCenterX[iLoopCount], MapDataParam.Info.UnitListCenterY[iLoopCount], 
                                                       MapDataParam.Info.UnitListWidth[iLoopCount], MapDataParam.Info.UnitListHeight[iLoopCount]);
                    SelectingRectName = "SearchArea" + (iLoopCount + 1);

                    if (SelectingRectName != SelectedRectName)
                    {
                        kpTeachDisplay.DrawStaticShape(SelectingRect, SelectingRectName, CogColorConstants.Orange);

                        if (SelectedRectName != "" && SelectedRectName != null)
                            kpTeachDisplay.DrawStaticShape(SelectedRect, SelectedRectName, CogColorConstants.Green);

                        SelectedRect = new CogRectangle(SelectingRect);
                        SelectedRectName = SelectingRectName;
                    }
                }
            }
        }

        private void rbSearchType_MouseUp(object sender, MouseEventArgs e)
        {
            RadioButton _RadioSearchType = (RadioButton)sender;
            int _SearchType = Convert.ToInt32(_RadioSearchType.Tag);
            SetSearchType(_SearchType);
            ShowSearchArea();
        }

        private void picSearchTypeChange_Click(object sender, EventArgs e)
        {
            PictureBox _PicSearchType = (PictureBox)sender;
            int _SearchType = Convert.ToInt32(_PicSearchType.Tag);
            SetSearchType(_SearchType); 
            ShowSearchArea();
        }

        private void SetSearchType(int _SearchType)
        {
            rbNormalSearch.Checked = false;
            rbNormalReverseSearch.Checked = false;
            rbZigzagSearch.Checked = false;
            rbZigzagReverseSearch.Checked = false;

            switch ((eSearchType)_SearchType)
            {
                case eSearchType.NORMAL:        rbNormalSearch.Checked = true;        break;
                case eSearchType.NORMAL_REV:    rbNormalReverseSearch.Checked = true; break;
                case eSearchType.ZIGZAG:        rbZigzagSearch.Checked = true;        break;
                case eSearchType.ZIGZAG_REV:    rbZigzagReverseSearch.Checked = true; break;
            }

            SearchDirectionType = _SearchType;
            MapDataParam.Info.SearchType = _SearchType;
        }

        private void ApplyMapIDReference()
        {
            CogBlobReferenceAlgo _BlobReferAlgo = new CogBlobReferenceAlgo();
            _BlobReferAlgo.ThresholdMin = Convert.ToInt32(graLabelThresholdValue.Text);
            _BlobReferAlgo.BlobAreaMin = 100;
            _BlobReferAlgo.BlobAreaMax = 50000;

            _BlobReferAlgo.Width = 25100;
            _BlobReferAlgo.WidthPos = 25000;
            _BlobReferAlgo.WidthNeg = 25000;
            _BlobReferAlgo.Height = 25100;
            _BlobReferAlgo.HeightPos = 25000;
            _BlobReferAlgo.HeightNeg = 25000;

            _BlobReferAlgo.ForeGround = 1;

            if (null == OriginImage) return;
            kpTeachDisplay.ClearDisplay();

            CogRectangleAffine _ReferRegionAffine = new CogRectangleAffine();
            _ReferRegionAffine.SetCenterLengthsRotationSkew(MapDataParam.MapID.SearchAreaCenterX, MapDataParam.MapID.SearchAreaCenterY, MapDataParam.MapID.SearchAreaWidth, MapDataParam.MapID.SearchAreaHeight, 0, 0);

            CogBlobReferenceResult _BlobReferResult = new CogBlobReferenceResult();
            bool _Result = InspBlobRefer.Run(OriginImage, _ReferRegionAffine, _BlobReferAlgo, ref _BlobReferResult);

            List<MapIDRectInfo> _MapIDRectInfoList = new List<MapIDRectInfo>();
            for (int iLoopCount = 0; iLoopCount < _BlobReferResult.BlobCount; ++iLoopCount)
            {
                double _Width = _BlobReferResult.Width[iLoopCount];
                double _Height = _BlobReferResult.Height[iLoopCount];

                if ((_Width > _BlobReferAlgo.Width - Math.Abs(_BlobReferAlgo.WidthNeg)) && (_Width < _BlobReferAlgo.Width + _BlobReferAlgo.WidthPos))
                {
                    MapIDRectInfo _RectInfo = new MapIDRectInfo();
                    _RectInfo.CenterPt.X = _BlobReferResult.BlobCenterX[iLoopCount];
                    _RectInfo.CenterPt.Y = _BlobReferResult.BlobCenterY[iLoopCount];
                    _RectInfo.Width = _BlobReferResult.Width[iLoopCount];
                    _RectInfo.Height = _BlobReferResult.Height[iLoopCount];
                    _MapIDRectInfoList.Add(_RectInfo);
                }
            }

            _MapIDRectInfoList.Sort(delegate (MapIDRectInfo _First, MapIDRectInfo _Second) { return _First.CenterPt.Y.CompareTo(_Second.CenterPt.Y); });
            for (int iLoopCount = 0; iLoopCount < _MapIDRectInfoList.Count; ++iLoopCount)
            {
                CogRectangle _MapRect = new CogRectangle();
                _MapRect.SetCenterWidthHeight(_MapIDRectInfoList[iLoopCount].CenterPt.X, _MapIDRectInfoList[iLoopCount].CenterPt.Y, _MapIDRectInfoList[iLoopCount].Width, _MapIDRectInfoList[iLoopCount].Height);
                kpTeachDisplay.DrawStaticShape(_MapRect, "BlobRect" + (iLoopCount + 1), CogColorConstants.Green);
                kpTeachDisplay.DrawText("BlobRect" + iLoopCount, _MapIDRectInfoList[iLoopCount].CenterPt.X + 10, _MapIDRectInfoList[iLoopCount].CenterPt.Y + 10, CogColorConstants.Green);
            }

            if (_MapIDRectInfoList.Count != MapDataParam.Info.UnitTotalCount)
            {
                //MessageBox.Show("Unit 갯수와 Map ID 갯수가 일치하지 않습니다.");
                return;
            }

            else
            {
                MapDataParam.MapID.MapIDInfoList.Clear();
                for (int iLoopCount = 0; iLoopCount < _MapIDRectInfoList.Count; ++iLoopCount)
                {
                    MapIDRectInfo _RectInfo = new MapIDRectInfo();
                    _RectInfo.CenterPt.X = _MapIDRectInfoList[iLoopCount].CenterPt.X;
                    _RectInfo.CenterPt.Y = _MapIDRectInfoList[iLoopCount].CenterPt.Y;
                    _RectInfo.Width = _MapIDRectInfoList[iLoopCount].Width;
                    _RectInfo.Height = _MapIDRectInfoList[iLoopCount].Height;
                    MapDataParam.MapID.MapIDInfoList.Add(_RectInfo);
                }
            }
        }

        private void hScrollBarThreshold_Scroll(object sender, ScrollEventArgs e)
        {
            graLabelThresholdValue.Text = hScrollBarThreshold.Value.ToString();
        }

        private void hScrollBarWidthSizeMin_Scroll(object sender, ScrollEventArgs e)
        {
            graLabelWidthMin.Text = hScrollBarWidthSizeMin.Value.ToString();
        }

        private void hScrollBarWidthSizeMax_Scroll(object sender, ScrollEventArgs e)
        {
            graLabelWidthMax.Text = hScrollBarWidthSizeMax.Value.ToString();
        }

        private void graLabelThresholdValue_TextChanged(object sender, EventArgs e)
        {
            ApplyMapIDReference();
        }

        private void graLabelWidthMin_TextChanged(object sender, EventArgs e)
        {
            ApplyMapIDReference();
        }

        private void graLabelWidthMax_TextChanged(object sender, EventArgs e)
        {
            ApplyMapIDReference();
        }
    }
}
