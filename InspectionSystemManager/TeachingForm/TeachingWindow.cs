using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cognex.VisionPro;

using ParameterManager;
using LogMessageManager;

namespace InspectionSystemManager
{
    public partial class TeachingWindow : Form
    {
        private enum eAreaList  { ID = 0, NAME, BENCHMARK, ENABLE, NGNUMBER };
        private enum eAlgoList  { ID = 0, NAME, BENCHMARK, ENABLE };
        private enum eTeachStep { NONE = 0, AREA_SELECT = 1, AREA_SET, AREA_CLEAR, ALGO_SELECT, ALGO_SET, ALGO_CLEAR };

        private CogImage8Grey InspectionImage = new CogImage8Grey();
        private int InspectionImageWidth = 0;
        private int InspectionImageHeight = 0;
        private double InspectionAreaWidth;
        private double InspectionAreaHeight;

        //검사 Algorithm Class
        private InspectionPattern           InspPatternProcess;
        private InspectionBlobReference     InspBlobReferProcess;
        private InspectionID                InspIDProcess;
        private InspectionLineFind          InspLineFindProcess;
        private InspectionMultiPattern      InspMultiPatternProcess;
        private InspectionAutoPattern       InspAutoPatternProcess;
        private InspectionEllipse           InspEllipseFindProcess;

        //검사 Algorithm Teaching UI
        private ucCogPattern            ucCogPatternWnd;
        private ucCogBlobReference      ucCogBlobReferWnd;
        private ucCogBlob               ucCogBlobWnd;
        private ucCogID                 ucCogIDInspWnd;
        private ucCogLineFind           ucCogLineFindWnd;
        private ucCogMultiPattern       ucCogMultiPatternWnd;
        private ucCogAutoPattern        ucCogAutoPatternWnd;
        private ucCogEllipseFind        ucCogEllipseFindWnd;

        private ContextMenu     ContextMenuAlgo;
        private eTeachStep      CurrentTeachStep;
        private eProjectType    ProjectType;
        private eProjectItem    ProjectItem;

        private InspectionParameter InspParam;
        private MapDataParameter MapDataParam;
        private int InspAreaSelected = -1;
        private int InspAlgoSelected = -1;
        private eAlgoType CurrentAlgoType;
        private CogRectangle AreaRegionRectangle;
        private CogRectangle AlgoRegionRectangle;

        private AreaResultParameterList AreaResParamList;   //검사 결과를 Teaching시에 적용하기 위해
        private AlgoResultParameterList AlgoResParamList;   //검사 결과를 Teaching시에 적용하기 위해
        private double AreaOffsetX;
        private double AreaOffsetY;
        private double AlgoOffsetX;
        private double AlgoOffsetY;

        private double ChangeAreaCenterOffsetX = 0;
        private double ChangeAreaCenterOffsetY = 0;

        private double ResolutionX;
        private double ResolutionY;

        private bool    IsWindowMoving = false;

        #region Initialize & DeInitialize
        public TeachingWindow()
        {
            InitializeComponent();
            InitializeContextMenu();
            InitializeLanguage();
        }

        public void SetParameters(InspectionParameter _InspParam, MapDataParameter _MapDataParam)
        {
            SetMapDataParameter(_MapDataParam);
            SetInspectionParameter(_InspParam);
            SetResolution();

            GridViewAreaAndAlgoClear();
            UpdateInspectionAreaList();
            gridViewArea.ShowCellToolTips = false;
            gridViewAlgo.ShowCellToolTips = false;
        }

        public void Initialize(int _ID = 0, eProjectType _ProjectType = eProjectType.NONE, eProjectItem _ProjectItem = eProjectItem.NONE)
        {
            //string _WindowTextName = String.Format(" Vision{0} - Teaching Window", (_ID + 1));
            string _WindowTextName = String.Format(" {0}{1} - {2}", LanguageResource.Vision, _ID + 1, LanguageResource.TeachingWindow);
            this.labelTitle.Text = _WindowTextName;
            this.labelStatus.Text = "";

            ProjectType = _ProjectType;
            ProjectItem = _ProjectItem;

            ucCogPatternWnd = new ucCogPattern();
            ucCogBlobWnd = new ucCogBlob();
            ucCogBlobReferWnd = new ucCogBlobReference();
            ucCogIDInspWnd = new ucCogID();
            ucCogLineFindWnd = new ucCogLineFind();
            ucCogMultiPatternWnd = new ucCogMultiPattern();
            ucCogAutoPatternWnd = new ucCogAutoPattern();
            ucCogEllipseFindWnd = new ucCogEllipseFind();

            if (_ProjectItem == eProjectItem.NONE)                  ucCogBlobReferWnd.Initialize(false);
            else if (_ProjectItem == eProjectItem.MEASURE)          ucCogBlobReferWnd.Initialize(false);

            InspPatternProcess = new InspectionPattern();
            InspAutoPatternProcess = new InspectionAutoPattern();
            InspMultiPatternProcess = new InspectionMultiPattern();
            InspBlobReferProcess = new InspectionBlobReference();
            InspIDProcess = new InspectionID();
            InspLineFindProcess = new InspectionLineFind();
            InspEllipseFindProcess = new InspectionEllipse();

            InspAreaSelected = -1;
            InspAlgoSelected = -1;
            CurrentTeachStep = eTeachStep.NONE;

            foreach (DataGridViewColumn _dataGridView in gridViewArea.Columns)  _dataGridView.SortMode = DataGridViewColumnSortMode.NotSortable;
            foreach (DataGridViewColumn _dataGridView in gridViewAlgo.Columns)  _dataGridView.SortMode = DataGridViewColumnSortMode.NotSortable;            

            InitializeEvent();
            InitializeContextMenu();
            InitializeAreaControl(_ProjectType);
                        
            //gridViewArea.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //gridViewArea.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //gridViewArea.EnableHeadersVisualStyles = false;
            //gridViewArea.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //gridViewArea.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //gridViewArea.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void InitializeLanguage()
        {
            //labelTitle.Text = LanguageResource.Vision;
            gradientLabelSettingWindowTitle.Text = LanguageResource.AreaSettingWindowTitle;
            gradientLabelTeaching.Text = LanguageResource.TeachingWindow;
            labelInspectionArea.Text = LanguageResource.InspectionArea;
            labelAlgorithmArea.Text = LanguageResource.AlgorithmArea;
            tabPageInspectionArea.Text = LanguageResource.InspectionArea;
            tabPageAlgorithmArea.Text = LanguageResource.AlgorithmArea;

            btnInspectionAreaSet.Text = LanguageResource.InspectionAreaSet;
            btnInspectionAreaAdd.Text = LanguageResource.InspectionAreaPlus;
            btnInspectionAreaDel.Text = LanguageResource.InspectionAreaMinus;
            btnInspectionAreaCopy.Text = LanguageResource.InspectionAreaCopy;
            btnMapDataApplyInspectionArea.Text = LanguageResource.ApplyMapData;
            btnMapDataAlgorithmSet.Text = LanguageResource.SetMapData;


            btnInspectionAlgoSet.Text = LanguageResource.AlgorithmSet;

            btnShowAllArea.Text = LanguageResource.ShowAllArea;
            btnSave.Text = LanguageResource.Save;
            btnConfirm.Text = LanguageResource.Confirm;
            btnCancel.Text = LanguageResource.Cancel;
        }

        public void DeInitialize()
        {
            DeInitializeEvent();

            ucCogPatternWnd.Dispose();
            ucCogBlobWnd.Dispose();
            ucCogBlobReferWnd.Dispose();
            ucCogIDInspWnd.Dispose();
            ucCogLineFindWnd.Dispose();
            ucCogMultiPatternWnd.Dispose();
            ucCogAutoPatternWnd.Dispose();

            InspBlobReferProcess.DeInitialize();
            InspPatternProcess.DeInitialize();
            InspMultiPatternProcess.DeInitialize();
            InspAutoPatternProcess.DeInitialize();
            InspIDProcess.DeInitialize();
            InspLineFindProcess.DeInitialize();
            InspEllipseFindProcess.DeInitialize();
        }

        private void InitializeContextMenu()
        {
            ContextMenuAlgo = new ContextMenu();
            ContextMenuAlgo.MenuItems.Clear();

            if (ProjectItem == eProjectItem.NONE)
            {
                ContextMenuAlgo.MenuItems.Add("Find a defect", new EventHandler(BlobAlgorithm));
                ContextMenuAlgo.MenuItems.Add("Search a Pattern reference", new EventHandler(PatternFindAlgorithm));
                ContextMenuAlgo.MenuItems.Add("Search Multi Pattern", new EventHandler(MultiPatternFindAlgorithm));
            }
            else
            {
                ContextMenuAlgo.MenuItems.Add("Search a Pattern reference", new EventHandler(PatternFindAlgorithm));
                ContextMenuAlgo.MenuItems.Add("Search a body reference", new EventHandler(BlobReferenceAlgorithm));
                //ContextMenuAlgo.MenuItems.Add("Find a defect", new EventHandler(BlobAlgorithm));
                ContextMenuAlgo.MenuItems.Add("제품 라인 검사", new EventHandler(LineFineAlgorithm));
                //ContextMenuAlgo.MenuItems.Add("Search a needle circle", new EventHandler(NeedleCircleFindAlgorithm));
                //ContextMenuAlgo.MenuItems.Add("Search a BarCode", new EventHandler(BarCodeIDAlgorithm));
                //ContextMenuAlgo.MenuItems.Add("Search Multi Pattern", new EventHandler(MultiPatternFindAlgorithm));
				//ContextMenuAlgo.MenuItems.Add("Search Auto Pattern", new EventHandler(AutoPatternFindAlgorithm));
				ContextMenuAlgo.MenuItems.Add("타원 검사", new EventHandler(EllipseAlgorithm));
            }
        }

        /// <summary>
        /// Project로 구분하여 Area 추가/삭제/복사 버튼의 활성화 비활성화를 설정
        /// </summary>
        /// <param name="_ProjectItem">Project Item</param>
        private void InitializeAreaControl(eProjectType _ProjectType)
        {
            switch (_ProjectType)
            {
                case eProjectType.NAVIEN:
                    btnInspectionAreaAdd.Visible = false;
                    btnInspectionAreaDel.Visible = false;
                    btnInspectionAreaCopy.Visible = false;
                    btnInspectionAlgoAdd.Visible = false;
                    btnInspectionAlgoDel.Visible = false;
                    btnShowAllArea.Visible = false;
                    btnMapDataApplyInspectionArea.Visible = false;
                    btnMapDataAlgorithmSet.Visible = false;
                    break;
                default:
                    break;
            }
        }

        private void SetInspectionParameter(InspectionParameter _InspParam = null)
        {
            InspParam = new InspectionParameter();
            CParameterManager.RecipeCopy(_InspParam, ref InspParam);

            //Reference File Copy
        }

        private void SetMapDataParameter(MapDataParameter _MapDataParam)
        {
            MapDataParam = new MapDataParameter();
            CParameterManager.RecipeCopy(_MapDataParam, ref MapDataParam);
        }

        private void SetResolution()
        {   
            ResolutionX = InspParam.ResolutionX;
            ResolutionY = InspParam.ResolutionY;
        }

        public InspectionParameter GetInspectionParameter()
        {
            return InspParam;
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == IsWindowMoving) return;

            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion

        #region Conext Menu Function
        private void PatternFindAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_PATTERN, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_PATTERN);
        }

        private void MultiPatternFindAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_MULTI_PATTERN, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_MULTI_PATTERN);
        }
        
        private void AutoPatternFindAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_AUTO_PATTERN, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_AUTO_PATTERN);
        }

        private void BlobReferenceAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_BLOB_REFER, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_BLOB_REFER);
        }

        private void BlobAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_BLOB, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_BLOB);
        }

        private void EllipseAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_ELLIPSE, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_ELLIPSE);
        }

        private void BarCodeIDAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_ID, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_ID);
        }

        private void LineFineAlgorithm(object sender, EventArgs e)
        {
            InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter(eAlgoType.C_LINE_FIND, ResolutionX, ResolutionY);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Add(_InspAlgoParam);
            UpdateInspectionAlgoList(InspAreaSelected, true);
            UpdateAlgoResultListAddAlgorithm(eAlgoType.C_LINE_FIND);
        }
        #endregion Conext Menu Function

        #region Button Event
        private void btnInspectionAreaAdd_Click(object sender, EventArgs e)
        {
            kpTeachDisplay.ClearDisplay();
            panelTeaching.Controls.Clear();
            panelTeaching.Controls.Add(gradientLabelTeaching);

            InspectionAreaParameter _InspAreaParam = new InspectionAreaParameter();
            //_InspAreaParam.BaseIndexNumber = InspParam.InspAreaParam.Count;
            _InspAreaParam.BaseIndexNumber = gridViewArea.RowCount;
            InspParam.InspAreaParam.Add(_InspAreaParam);

            GridViewAreaAndAlgoClear();
            UpdateInspectionAreaList(InspParam.InspAreaParam.Count - 1);

            UpdateTeachingStatus(eTeachStep.AREA_CLEAR);
            gridViewAlgo.ClearSelection();
            gridViewAlgo.Rows.Clear();

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAreaAdd", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAreaDel_Click(object sender, EventArgs e)
        {
            if (gridViewArea.SelectedRows.Count == 0) { MessageBox.Show("Not selected inspection area."); return; }
            int _SelectedAreaNum = Convert.ToInt32(gridViewArea.SelectedRows[0].Cells[(int)eAreaList.ID].Value) - 1;
            if (_SelectedAreaNum < 0) { MessageBox.Show("Not selected inspection area."); return; }
            InspAreaSelected = _SelectedAreaNum;

            kpTeachDisplay.ClearDisplay();
            panelTeaching.Controls.Clear();
            panelTeaching.Controls.Add(gradientLabelTeaching);
            gridViewAlgo.Rows.Clear();

            ////연결되어 있는 BenchMark가 있는 경우 삭제 금지
            bool _IsDeleteContinue = true;
            int _BenchMark = InspAreaSelected + 1;
            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                int _DeleteAlgoBenchMark = InspParam.InspAreaParam[iLoopCount].AreaBenchMark;
                if (_BenchMark == _DeleteAlgoBenchMark) { _IsDeleteContinue = false; break; }
            }

            if (false == _IsDeleteContinue)
            {
                string _Message = "Select area can not be deleted due to the \"BenchMark\" setting.";
                MessageBox.Show(_Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            //Map Data를 사용안할 시 삭제
            if (false == InspParam.InspAreaParam[InspAreaSelected].IsUseMapData || MapDataParam.Info.UnitTotalCount < 20)
            {
                int _RowNextSelect = 0;
                int _RowCount = gridViewArea.RowCount;
                int _RowSelect = gridViewArea.CurrentCell.RowIndex;

                GridViewAreaAndAlgoClear();

                //Remove algorithms
                int _EndAlgorithm = 0;
                for (int iLoopCount = 0; iLoopCount < InspAreaSelected; ++iLoopCount)
                {
                    if (InspParam.InspAreaParam[iLoopCount].Enable == true)
                    {
                        for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                        {
                            if (InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable == true)
                                _EndAlgorithm++;
                        }
                    }
                }

                int _StartAlgorithm = 0;
                for (int iLoopCount = 0; iLoopCount <= InspAreaSelected; ++iLoopCount)
                {
                    if (InspParam.InspAreaParam[iLoopCount].Enable == true)
                    {
                        for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                        {
                            if (InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable == true)
                                _StartAlgorithm++;
                        }
                    }
                }

                for (int iLoopCount = _StartAlgorithm - 1; iLoopCount > _EndAlgorithm - 1; --iLoopCount)
                {
                    if (AlgoResParamList.Count > iLoopCount)
                        AlgoResParamList.RemoveAt(iLoopCount);
                }

                for (int iLoopCount = _RowSelect + 1; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
                {
                    if (InspParam.InspAreaParam[iLoopCount].BaseIndexNumber != 0)
                        InspParam.InspAreaParam[iLoopCount].BaseIndexNumber--;
                    if (InspParam.InspAreaParam[iLoopCount].MapDataStartNumber != 1)
                        InspParam.InspAreaParam[iLoopCount].MapDataStartNumber--;
                    if (InspParam.InspAreaParam[iLoopCount].MapDataEndNumber != 1)
                        InspParam.InspAreaParam[iLoopCount].MapDataEndNumber--;
                }
                InspParam.InspAreaParam.RemoveAt(_RowSelect);

                if (_RowSelect == 0 && _RowCount > 1) _RowNextSelect = 0;
                if (_RowSelect > 1) _RowNextSelect = _RowSelect - 1;
                if (_RowCount == 1) _RowSelect = -1;
                if (_RowSelect != -1) UpdateInspectionAreaList(_RowNextSelect);
            }

            else
            {
                int _RowNextSelect = 0;
                int _RowCount = gridViewArea.RowCount;
                int _RowSelect = gridViewArea.CurrentCell.RowIndex;

                GridViewAreaAndAlgoClear();

                int _StartIndex = InspParam.InspAreaParam[_RowSelect].MapDataStartNumber - 1;
                int _EndIndex = InspParam.InspAreaParam[_RowSelect].MapDataEndNumber - 1;

                for (int iLoopCount = _EndIndex + 1; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
                {
                    InspParam.InspAreaParam[iLoopCount].BaseIndexNumber--;
                    if (InspParam.InspAreaParam[iLoopCount].MapDataStartNumber != 1)
                        InspParam.InspAreaParam[iLoopCount].MapDataStartNumber--;
                    if (InspParam.InspAreaParam[iLoopCount].MapDataEndNumber != 1)
                        InspParam.InspAreaParam[iLoopCount].MapDataEndNumber--;
                }
                for (int iLoopCount = _EndIndex; iLoopCount >= _StartIndex; --iLoopCount)
                    InspParam.InspAreaParam.RemoveAt(iLoopCount);

                if (_RowSelect == 0 && _RowCount > 1) _RowNextSelect = 0;
                if (_RowSelect > 1) _RowNextSelect = _RowSelect - 1;
                if (_RowCount == 1) _RowSelect = -1;
                if (_RowSelect != -1) UpdateInspectionAreaList(_RowNextSelect);
            }

            InspAreaSelected = -1;
            UpdateTeachingStatus(eTeachStep.AREA_CLEAR);
            gridViewAlgo.ClearSelection();
            gridViewAlgo.Rows.Clear();

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAreaDel", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAreaCopy_Click(object sender, EventArgs e)
        {
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAreaCopy", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAreaSet_Click(object sender, EventArgs e)
        {
            if (CurrentTeachStep == eTeachStep.NONE)        return;
            if (CurrentTeachStep == eTeachStep.ALGO_SET)    return;
            if (CurrentTeachStep == eTeachStep.ALGO_SELECT) return;
            if (gridViewArea.SelectedRows.Count == 0) { MessageBox.Show("Not selected inspection area."); return; }
            int _SelectedAreaNum = Convert.ToInt32(gridViewArea.SelectedRows[0].Cells[(int)eAreaList.ID].Value) - 1;
            int _GridViewAreaSelectNum = Convert.ToInt32(gridViewArea.SelectedRows[0].Index);
            if (_SelectedAreaNum < 0) { MessageBox.Show("Not selected inspection area."); return; }
            if (InspectionImageWidth == 0 || InspectionImageHeight == 0) return;

            InspAreaSelected = _SelectedAreaNum;

            //BenchMark Setting
            //DataGridViewComboBoxCell _ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.BENCHMARK), InspAreaSelected];
            DataGridViewComboBoxCell _ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.BENCHMARK), _GridViewAreaSelectNum];
            InspParam.InspAreaParam[InspAreaSelected].AreaBenchMark = _ComboCell.Items.IndexOf(_ComboCell.Value);

            //_ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.NGNUMBER), InspAreaSelected];
            _ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.NGNUMBER), _GridViewAreaSelectNum];
            InspParam.InspAreaParam[InspAreaSelected].NgAreaNumber = _ComboCell.Items.IndexOf(_ComboCell.Value) + 1;

            //DataGridViewCheckBoxCell _CheckCell = (DataGridViewCheckBoxCell)gridViewArea[Convert.ToInt32(eAreaList.ENABLE), InspAreaSelected];
            DataGridViewCheckBoxCell _CheckCell = (DataGridViewCheckBoxCell)gridViewArea[Convert.ToInt32(eAreaList.ENABLE), _GridViewAreaSelectNum];
            InspParam.InspAreaParam[InspAreaSelected].Enable = Convert.ToBoolean(_CheckCell.Value);

            CogRectangle _InspRegion = new CogRectangle();
            CogRectangle _Boundary = new CogRectangle();
            _Boundary.SetXYWidthHeight(0, 0, InspectionImageWidth, InspectionImageHeight);
            if (false == GetCorrectionRectangle(kpTeachDisplay, _Boundary, ref _InspRegion)) { MessageBox.Show("The rectangle is outside the inspection area."); return; }
            InspectionAreaWidth = Convert.ToInt32(_InspRegion.Width);
            InspectionAreaHeight = Convert.ToInt32(_InspRegion.Height);

            //Area 영역 설정 시 Area Offset 값 얻어오기
            GetAreaResultDataOffset(InspAreaSelected);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawStaticShape(_InspRegion, "InspRegion", CogColorConstants.Green);
            kpTeachDisplay.DrawText("InspRegion", _InspRegion.X, _InspRegion.Y - 35, CogColorConstants.Green, 10);
            AreaRegionRectangle = new CogRectangle(_InspRegion);
            panelTeaching.Controls.Clear();
            panelTeaching.Controls.Add(gradientLabelTeaching);

            //LJH ADD 2019.1.10
            ChangeAreaCenterOffsetX = (_InspRegion.CenterX - AreaOffsetX) - InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterX;
            ChangeAreaCenterOffsetY = (_InspRegion.CenterY - AreaOffsetY) - InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterY;

            //Area Setting
            InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterX = _InspRegion.CenterX - AreaOffsetX;
            InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterY = _InspRegion.CenterY - AreaOffsetY;
            InspParam.InspAreaParam[InspAreaSelected].AreaRegionWidth   = _InspRegion.Width;
            InspParam.InspAreaParam[InspAreaSelected].AreaRegionHeight  = _InspRegion.Height;

            UpdateInspectionAreaList(InspAreaSelected);
            gridViewArea.Rows[_GridViewAreaSelectNum].Selected = true;
            //gridViewArea.Rows[InspAreaSelected].Selected = true;

            UpdateInspectionAlgoList(InspAreaSelected);
            UpdateTeachingStatus(eTeachStep.AREA_SET);
        }

        private void btnInspectionAlgoAdd_MouseUp(object sender, MouseEventArgs e)
        {
            Point _Position = new Point(e.X, e.Y);

            if (labelStatus.Text.Contains("Area Set") == false) { MessageBox.Show("Not set inspection area."); return; }

            ContextMenuAlgo.Show(btnInspectionAlgoAdd, _Position);

            UpdateTeachingStatus(eTeachStep.ALGO_CLEAR);

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAlgoAdd", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAlgoDel_Click(object sender, EventArgs e)
        {
            if (-1 == InspAlgoSelected) { MessageBox.Show("Not selected algorithm area."); return; }
            if (null == gridViewAlgo.CurrentCell) return;

            int _RowNextSelect = 0;
            int _RowCount = gridViewAlgo.RowCount;
            int _RowSelect = gridViewAlgo.CurrentCell.RowIndex;

            //연결되어 있는 BenchMark가 있는 경우 삭제 금지
            bool _IsDeleteContinue = true;
            int _BenchMark = InspAlgoSelected + 1;
            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Count; ++iLoopCount)
            {
                int _DeleteAlgoBenchMark = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark;
                if (_BenchMark == _DeleteAlgoBenchMark) { _IsDeleteContinue = false; break; }
            }

            if (false == _IsDeleteContinue)
            {
                string _Message = "Select algorithm can not be deleted due to the \"BenchMark\" setting.";
                MessageBox.Show(_Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }

            kpTeachDisplay.ClearDisplay();
            panelTeaching.Controls.Clear();
            panelTeaching.Controls.Add(gradientLabelTeaching);
            gridViewAlgo.Rows.Clear();


            //FreeReferenceEvent(ref InspParam, InspAreaSelected, _RowSelect);
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.RemoveAt(_RowSelect);

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Count; ++iLoopCount)
            {
                int BenchMark = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark;
                if (BenchMark != 0 && BenchMark > _RowSelect) InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark = BenchMark - 1;
            }

            if (_RowSelect == 0 && _RowCount > 1) _RowNextSelect = 0;
            if (_RowSelect > 1) _RowNextSelect = _RowSelect - 1;
            if (_RowCount == 1) _RowSelect = -1;
            if (_RowSelect != -1) UpdateInspectionAlgoList(InspAreaSelected, true);

            InspAlgoSelected = -1;
            UpdateTeachingStatus(eTeachStep.ALGO_CLEAR);

            //Remove Algorithm
            int _ResultIndex = 0;
            for (int iLoopCount = 0; iLoopCount < InspAreaSelected; ++iLoopCount)
            {
                if (InspParam.InspAreaParam[iLoopCount].Enable == true)
                {
                    for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                    {
                        if (InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable == true)
                            _ResultIndex++;
                    }
                }
            }

            _ResultIndex = _ResultIndex + InspAlgoSelected - 1;
            if (AlgoResParamList.Count > _ResultIndex && _ResultIndex >= 0)
                AlgoResParamList.RemoveAt(_ResultIndex);

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAlgoDel", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAlgoCopy_Click(object sender, EventArgs e)
        {
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "TeachingWindow - InspectionAlgoCopy", CLogManager.LOG_LEVEL.MID);
        }

        private void btnInspectionAlgoSet_Click(object sender, EventArgs e)
        {
            if (gridViewArea.SelectedRows.Count == 0) { MessageBox.Show("Not selected inspection area."); return; }
            int _SelectedAreaNum = Convert.ToInt32(gridViewArea.SelectedRows[0].Cells[(int)eAreaList.ID].Value) - 1;
            if (_SelectedAreaNum < 0) { MessageBox.Show("Not selected inspection area."); return; }

            if (gridViewAlgo.SelectedRows.Count == 0) { MessageBox.Show("Not Selected Inspection Algorithm."); return; }
            int _SelectedAlgoNum = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
            if (_SelectedAlgoNum < 0) { MessageBox.Show("Not Selected Inspection Algorithm."); return; }

            InspAreaSelected = _SelectedAreaNum;
            InspAlgoSelected = _SelectedAlgoNum;

            if (-1 == InspAreaSelected) { MessageBox.Show("Select Inspection Area"); return; }
            if (-1 == InspAlgoSelected) { MessageBox.Show("Select Algorithm Area"); return; }
            if (eTeachStep.ALGO_SELECT != CurrentTeachStep) return;
            //if (eTeachStep.ALGO_SET == CurrentTeachStep) return;

            //Benchmark Setting
            DataGridViewComboBoxCell _ComboCell = (DataGridViewComboBoxCell)gridViewAlgo[Convert.ToInt32(eAreaList.BENCHMARK), InspAlgoSelected];
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoBenchMark = _ComboCell.Items.IndexOf(_ComboCell.Value);

            DataGridViewCheckBoxCell _CheckCell = (DataGridViewCheckBoxCell)gridViewAlgo[Convert.ToInt32(eAreaList.ENABLE), InspAlgoSelected];
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoEnable = Convert.ToBoolean(_CheckCell.Value);


            CogRectangle _AlgoRegion = new CogRectangle();
            CogRectangle _Boundary = new CogRectangle();
            _Boundary.SetCenterWidthHeight(InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterX, InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterY,
                                            InspParam.InspAreaParam[InspAreaSelected].AreaRegionWidth, InspParam.InspAreaParam[InspAreaSelected].AreaRegionHeight);
            if (false == GetCorrectionRectangle(kpTeachDisplay, _Boundary, ref _AlgoRegion)) { MessageBox.Show("The rectangle is outside the inspection area."); return; }

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawStaticShape(_Boundary, "InspRegion", CogColorConstants.Green, 2);
            kpTeachDisplay.DrawText("InspRegion", _Boundary.X, _Boundary.Y - 35, CogColorConstants.Green, 10);
            kpTeachDisplay.DrawStaticShape(_AlgoRegion, "AlgoRegion", CogColorConstants.Orange, 2);
            kpTeachDisplay.DrawText("AlgoRegion", _AlgoRegion.X, _AlgoRegion.Y - 35, CogColorConstants.Orange, 10);


            //Algorithm 영역 설정 시 Algorithm offset 값 얻어오기
            //GetAlgoResultDataOffset(InspAreaSelected, InspAlgoSelected);
            //AlgorithmAreaStartX = _AlgoRegion.X;
            //AlgorithmAreaStartY = _AlgoRegion.Y;
            //AlgorithmAreaWidth = _AlgoRegion.Width;
            //AlgorithmAreaHeight = _AlgoRegion.Height;

            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].BenchMarkOffsetX = AreaOffsetX + AlgoOffsetX;
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].BenchMarkOffsetY = AreaOffsetY + AlgoOffsetY;

            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterX = _AlgoRegion.CenterX + AreaOffsetX;
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterY = _AlgoRegion.CenterY + AreaOffsetY;
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionWidth = _AlgoRegion.Width;
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionHeight = _AlgoRegion.Height;

            if (InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterX < 0 || InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterY < 0)
                InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterX = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[InspAlgoSelected].AlgoRegionCenterX;

            //Algorithm Area Crop Set
            //AlgoCropStartX = _AlgoRegion.X;
            //AlgoCropStartY = _AlgoRegion.Y;

            AreaRegionRectangle = new CogRectangle(_Boundary);
            AlgoRegionRectangle = new CogRectangle(_AlgoRegion);

            UpdateTeachingStatus(eTeachStep.ALGO_SET);

            UpdateInspectionAlgoTeachingWindow(InspAlgoSelected);
        }

        private void gridViewArea_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int _Index = (int)eAreaList.ENABLE;
                bool _Flag = Convert.ToBoolean(gridViewArea.SelectedRows[0].Cells[_Index].Value);
                gridViewArea.SelectedRows[0].Cells[_Index].Value = !_Flag;
            }
        }

        private void gridViewArea_CurrentCellChanged(object sender, EventArgs e)
        {
            if (gridViewArea.RowCount <= 0) return;
            if (gridViewArea.SelectedRows.Count == 0) return;

            gridViewArea.SelectedRows[0].Selected = true;
            int _ID = Convert.ToInt32(gridViewArea.SelectedRows[0].Cells[(int)eAreaList.ID].Value) - 1;

            kpTeachDisplay.ClearDisplay();
            panelTeaching.Controls.Clear();
            panelTeaching.Controls.Add(gradientLabelTeaching);
            gridViewAlgo.Rows.Clear();

            GetAreaResultDataOffset(_ID);
            UpdateInspectionArea(_ID);

            gridViewAlgo.ClearSelection();
            InspAreaSelected = _ID;

            AreaRegionRectangle = null;
            UpdateTeachingStatus(eTeachStep.AREA_SELECT);
        }

        private void gridViewAlgo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 2 && e.ColumnIndex != 3)
            {
                if (gridViewAlgo.RowCount <= 0) return;
                if (gridViewAlgo.SelectedRows.Count == 0) return;

                panelTeaching.Controls.Clear();
                panelTeaching.Controls.Add(gradientLabelTeaching);

                int _ID = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
                GetAlgoResultDataOffset(InspAreaSelected, _ID);

                //UpdateInspectionAlgoTeachingWindow(_ID);
                UpdateInspectionAlgorithmAreaDraw(_ID);
                InspAlgoSelected = _ID;

                AlgoRegionRectangle = null;
                UpdateTeachingStatus(eTeachStep.ALGO_SELECT);
            }
        }

        private void gridViewAlgo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int _Index = (int)eAlgoList.ENABLE;
                bool _Flag = Convert.ToBoolean(gridViewAlgo.SelectedRows[0].Cells[_Index].Value);
                gridViewAlgo.SelectedRows[0].Cells[_Index].Value = !_Flag;
            }
        }

        private void gridViewAlgo_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void gridViewAlgo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex != 2 && e.ColumnIndex != 3)
            //{
            //    if (gridViewAlgo.RowCount <= 0) return;
            //    if (gridViewAlgo.SelectedRows.Count == 0) return;
            //
            //    int _ID = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
            //    GetAlgoResultDataOffset(InspAreaSelected, _ID);
            //
            //    //UpdateInspectionAlgoTeachingWindow(_ID);
            //    if (InspAlgoSelected > -1) gridViewAlgo.Rows[InspAlgoSelected].DefaultCellStyle.BackColor = Color.White;
            //    UpdateInspectionAlgorithmAreaDraw(_ID);
            //    InspAlgoSelected = _ID;
            //
            //    AlgoRegionRectangle = null;
            //    UpdateTeachingStatus(eTeachStep.ALGO_SELECT);
            //
            //    gridViewAlgo.Rows[InspAlgoSelected].DefaultCellStyle.BackColor = Color.Yellow;
            //    gridViewAlgo.ClearSelection();
            //}
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            for (int iLoopCount = 0; iLoopCount < gridViewArea.RowCount; ++iLoopCount)
            {
                DataGridViewComboBoxCell _ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.BENCHMARK), iLoopCount];
                InspParam.InspAreaParam[iLoopCount].AreaBenchMark = _ComboCell.Items.IndexOf(_ComboCell.Value);

                _ComboCell = (DataGridViewComboBoxCell)gridViewArea[Convert.ToInt32(eAreaList.NGNUMBER), iLoopCount];
                InspParam.InspAreaParam[iLoopCount].NgAreaNumber = _ComboCell.Items.IndexOf(_ComboCell.Value) + 1;

                DataGridViewCheckBoxCell _CheckCell = (DataGridViewCheckBoxCell)gridViewArea[Convert.ToInt32(eAreaList.ENABLE), iLoopCount];
                InspParam.InspAreaParam[iLoopCount].Enable = Convert.ToBoolean(_CheckCell.Value);
            }

            if (InspAreaSelected != -1)
            {
                for (int iLoopCount = 0; iLoopCount < gridViewAlgo.RowCount; ++iLoopCount)
                {
                    DataGridViewComboBoxCell _ComboCell = (DataGridViewComboBoxCell)gridViewAlgo[Convert.ToInt32(eAreaList.BENCHMARK), iLoopCount];
                    InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark = _ComboCell.Items.IndexOf(_ComboCell.Value);

                    DataGridViewCheckBoxCell _CheckCell = (DataGridViewCheckBoxCell)gridViewAlgo[Convert.ToInt32(eAreaList.ENABLE), iLoopCount];
                    InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoEnable = Convert.ToBoolean(_CheckCell.Value);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (CurrentAlgoType)
            {
                case eAlgoType.C_PATTERN: ucCogPatternWnd.CancelAlgoRecipe(); break;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (CurrentAlgoType)
            {
                case eAlgoType.C_PATTERN:       ucCogPatternWnd.SaveAlgoRecipe();    break;
                case eAlgoType.C_BLOB_REFER:    ucCogBlobReferWnd.SaveAlgoRecipe();  break;
                case eAlgoType.C_BLOB:          ucCogBlobWnd.SaveAlgoRecipe();       break;
                case eAlgoType.C_ID:            ucCogIDInspWnd.SaveAlgoRecipe();     break;
                case eAlgoType.C_LINE_FIND:     ucCogLineFindWnd.SaveAlgoRecipe();   break;
				case eAlgoType.C_MULTI_PATTERN: ucCogMultiPatternWnd.SaveAlgoRecipe(); break;
                case eAlgoType.C_AUTO_PATTERN:  ucCogAutoPatternWnd.SaveAlgoRecipe();  break;
                case eAlgoType.C_ELLIPSE:       ucCogEllipseFindWnd.SaveAlgoRecipe(); break;
            }
        }

        private void btnAlgorithmIndexMoveUp_Click(object sender, EventArgs e)
        {
            if (gridViewArea.RowCount <= 0) return;
            if (gridViewArea.SelectedRows.Count == 0) return;

            int _ID = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
            if (0 == _ID) return;

            List<int> _BenchMarkList = new List<int>();
            for (int iLoopCount = 0; iLoopCount < gridViewArea.RowCount; ++iLoopCount)
            {
                int _Benchmark = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark;

                if (false == _BenchMarkList.Contains(_Benchmark) && _Benchmark != 0)
                    _BenchMarkList.Add(_Benchmark);
            }

            int _IDChange = _ID - 1;

            InspectionAlgorithmParameter _Temp = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID];
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID] = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_IDChange];
            InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_IDChange] = _Temp;

            UpdateInspectionAlgoList(InspAreaSelected);
            gridViewAlgo.Rows[_IDChange].Selected = true;
        }

        private void btnAlgorithmIndexMoveDown_Click(object sender, EventArgs e)
        {
            if (gridViewArea.RowCount <= 0) return;
            if (gridViewArea.SelectedRows.Count == 0) return;

            int _ID = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
            if (gridViewArea.RowCount < _ID) return;

            List<int> _BenchMarkList = new List<int>();
            for (int iLoopCount = 0; iLoopCount < gridViewArea.RowCount; ++iLoopCount)
            {
                int _Benchmark = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[iLoopCount].AlgoBenchMark;

                if (false == _BenchMarkList.Contains(_Benchmark) && _Benchmark != 0)
                    _BenchMarkList.Add(_Benchmark);
            }

            int _IDChange = _ID + 1;
            if (_IDChange < InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam.Count)
            {
                InspectionAlgorithmParameter _Temp = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID];
                InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID] = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_IDChange];
                InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_IDChange] = _Temp;

                UpdateInspectionAlgoList(InspAreaSelected);
                gridViewAlgo.Rows[_IDChange].Selected = true;
            }
        }

        private void btnShowAlgorithmMoveButton_MousePressing(object sender, EventArgs e)
        {
            btnAlgorithmIndexMoveUp.Visible = !btnAlgorithmIndexMoveUp.Visible;
            btnAlgorithmIndexMoveDown.Visible = !btnAlgorithmIndexMoveDown.Visible;

            btnInspectionAlgoAdd.Visible = !btnInspectionAlgoAdd.Visible;
            btnInspectionAlgoDel.Visible = !btnInspectionAlgoDel.Visible;
        }

        private void btnMapDataApplyInspectionArea_Click(object sender, EventArgs e)
        {
            //if (MapDataParam.Info.UnitTotalCount > 20) { MessageBox.Show(String.Format("Area가 {0}개 입니다.", MapDataParam.Info.UnitTotalCount)); return; }
            MessageBox.Show(String.Format("Area가 {0}개 입니다.", MapDataParam.Info.UnitTotalCount));

            int _StartNumber = InspParam.InspAreaParam.Count + 1;
            for (int iLoopCount = 0; iLoopCount < MapDataParam.Info.UnitTotalCount; ++iLoopCount)
            {
                InspectionAreaParameter _InspAreaParam = new InspectionAreaParameter();
                _InspAreaParam.AreaRegionCenterX = MapDataParam.Info.UnitListCenterX[iLoopCount];
                _InspAreaParam.AreaRegionCenterY = MapDataParam.Info.UnitListCenterY[iLoopCount];
                _InspAreaParam.AreaRegionWidth   = MapDataParam.Info.UnitListWidth[iLoopCount];
                _InspAreaParam.AreaRegionHeight  = MapDataParam.Info.UnitListHeight[iLoopCount];
                _InspAreaParam.BaseIndexNumber   = _StartNumber - 1;

                _InspAreaParam.IsUseMapData = true;
                _InspAreaParam.MapDataUnitTotalCount = (int)MapDataParam.Info.UnitTotalCount;
                _InspAreaParam.MapDataStartNumber   = _StartNumber;
                _InspAreaParam.MapDataEndNumber     = _StartNumber + _InspAreaParam.MapDataUnitTotalCount - 1;

                //Map ID를 사용하는 경우 BlobReference Algo를 넣어준다.
                #region BlobReference Algo ADD
                if (true == MapDataParam.MapID.IsUsableMapID)
                {
                    InspectionAlgorithmParameter _InspAlgoParam = new InspectionAlgorithmParameter();
                    _InspAlgoParam.AlgoType = (int)eAlgoType.C_BLOB_REFER;
                    _InspAlgoParam.AlgoBenchMark = 0;
                    _InspAlgoParam.AlgoEnable = true;
                    _InspAlgoParam.AlgoRegionCenterX = MapDataParam.MapID.MapIDInfoList[iLoopCount].CenterPt.X;
                    _InspAlgoParam.AlgoRegionCenterY = MapDataParam.MapID.MapIDInfoList[iLoopCount].CenterPt.Y;
                    _InspAlgoParam.AlgoRegionWidth = MapDataParam.MapID.MapIDInfoList[iLoopCount].Width + 15;
                    _InspAlgoParam.AlgoRegionHeight = MapDataParam.MapID.MapIDInfoList[iLoopCount].Height + 15;

                    _InspAlgoParam.Algorithm = new CogBlobReferenceAlgo();
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ThresholdMin = MapDataParam.MapID.SearchThreshold;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BlobAreaMin = MapDataParam.MapID.BlobAreaSizeMin;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).BlobAreaMax = MapDataParam.MapID.BlobAreaSizeMax;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Width = (MapDataParam.MapID.SearchSizeMin + MapDataParam.MapID.SearchSizeMax) / 2;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).WidthPos = MapDataParam.MapID.SearchSizeMax - ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Width;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).WidthNeg = ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Width - MapDataParam.MapID.SearchSizeMin;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).Height = 10000;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).HeightPos = 10000;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).HeightNeg = 10000;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ResolutionX = 1;
                    ((CogBlobReferenceAlgo)_InspAlgoParam.Algorithm).ResolutionY = 1;

                    _InspAreaParam.InspAlgoParam.Add(_InspAlgoParam);
                }
                #endregion BlobReference Algo ADD

                InspParam.InspAreaParam.Add(_InspAreaParam);
            }

            GridViewAreaAndAlgoClear();

            int _AreaListIndex = InspParam.InspAreaParam[InspParam.InspAreaParam.Count - 1].BaseIndexNumber;
            UpdateInspectionAreaList(_AreaListIndex);
            //UpdateInspectionAreaList(InspParam.InspAreaParam.Count - 1);

            UpdateTeachingStatus(eTeachStep.AREA_CLEAR);
            gridViewAlgo.ClearSelection();
            gridViewAlgo.Rows.Clear();
        }

        private void btnMapDataAlgorithmSet_Click(object sender, EventArgs e)
        {
            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                //Map Data를 사용 할 시
                if (true == InspParam.InspAreaParam[iLoopCount].IsUseMapData)
                {
                    int _Base = InspParam.InspAreaParam[iLoopCount].BaseIndexNumber;
                    if (_Base == iLoopCount) continue;

                    double _AreaCenterX = InspParam.InspAreaParam[_Base].AreaRegionCenterX;
                    double _AreaCenterY = InspParam.InspAreaParam[_Base].AreaRegionCenterY;
                    InspParam.InspAreaParam[iLoopCount].AreaRegionCenterX += ChangeAreaCenterOffsetX;
                    InspParam.InspAreaParam[iLoopCount].AreaRegionCenterY += ChangeAreaCenterOffsetY;
                    InspParam.InspAreaParam[iLoopCount].AreaRegionWidth = InspParam.InspAreaParam[_Base].AreaRegionWidth;
                    InspParam.InspAreaParam[iLoopCount].AreaRegionHeight = InspParam.InspAreaParam[_Base].AreaRegionHeight;

                    for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[_Base].InspAlgoParam.Count; ++jLoopCount)
                    {
                        double _AlgoCenterX = InspParam.InspAreaParam[_Base].InspAlgoParam[jLoopCount].AlgoRegionCenterX;
                        double _AlgoCenterY = InspParam.InspAreaParam[_Base].InspAlgoParam[jLoopCount].AlgoRegionCenterY;
                        double _OffsetX = _AlgoCenterX - _AreaCenterX;
                        double _OffsetY = _AlgoCenterY - _AreaCenterY;

                        InspectionAlgorithmParameter _InspAlgoParamSrc  = InspParam.InspAreaParam[_Base].InspAlgoParam[jLoopCount];
                        InspectionAlgorithmParameter _InspAlgoParamDest = new InspectionAlgorithmParameter();
                        InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Clear();

                        CParameterManager.RecipeCopy(_InspAlgoParamSrc, ref _InspAlgoParamDest, _OffsetX, _OffsetY);
                        _InspAlgoParamDest.AlgoRegionCenterX = InspParam.InspAreaParam[iLoopCount].AreaRegionCenterX + _OffsetX;
                        _InspAlgoParamDest.AlgoRegionCenterY = InspParam.InspAreaParam[iLoopCount].AreaRegionCenterY + _OffsetY;
                        InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Add(_InspAlgoParamDest);
                    }
                }
            }

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                if (false == InspParam.InspAreaParam[iLoopCount].IsUseMapData) continue;

                //임시 : Map data copy Draw
                CogRectangle _Area = new CogRectangle();
                _Area.SetCenterWidthHeight(InspParam.InspAreaParam[iLoopCount].AreaRegionCenterX, InspParam.InspAreaParam[iLoopCount].AreaRegionCenterY, InspParam.InspAreaParam[iLoopCount].AreaRegionWidth, InspParam.InspAreaParam[iLoopCount].AreaRegionHeight);
                kpTeachDisplay.DrawStaticShape(_Area, string.Format("Area_{0}", iLoopCount), CogColorConstants.Green);

                for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                {
                    //임시 : Map data copy Draw
                    CogRectangle _Algo = new CogRectangle();
                    _Algo.SetCenterWidthHeight(InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionCenterX, InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionCenterY,
                                                InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionWidth, InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoRegionHeight);
                    kpTeachDisplay.DrawStaticShape(_Algo, string.Format("Algo_{0}_{1}", iLoopCount, jLoopCount), CogColorConstants.Green);
                }
            }

            ChangeAreaCenterOffsetX = 0;
            ChangeAreaCenterOffsetY = 0;

            //InspAreaSelected = -1;
            //gridViewArea.ClearSelection();
        }

        private void btnShowAllArea_Click(object sender, EventArgs e)
        {
            ShowAllArea();
        }

        private void ShowAllArea()
        {
            kpTeachDisplay.ClearDisplay();
            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                CogRectangle _InspAreaRect = new CogRectangle();
                _InspAreaRect.SetCenterWidthHeight(InspParam.InspAreaParam[iLoopCount].AreaRegionCenterX, InspParam.InspAreaParam[iLoopCount].AreaRegionCenterY,
                                                   InspParam.InspAreaParam[iLoopCount].AreaRegionWidth, InspParam.InspAreaParam[iLoopCount].AreaRegionHeight);
                kpTeachDisplay.DrawStaticShape(_InspAreaRect, "Area " + (iLoopCount + 1), kpTeachDisplay.ColorDefine[iLoopCount], 2, CogGraphicLineStyleConstants.Dash);
            }
        }
        #endregion Button Event

        #region Inspection Area & Algorithm Gridview Update
        private void GridViewAreaAndAlgoClear()
        {
            gridViewArea.Rows.Clear();
            gridViewAlgo.Rows.Clear();
        }

        private void UpdateInspectionAreaList(int _Selected = -1, bool _IsNew = false)
        {
            gridViewArea.Rows.Clear();

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                if (InspParam.InspAreaParam[iLoopCount].IsUseMapData && MapDataParam.Info.UnitTotalCount > 20)
                {
                    int _Index = (iLoopCount + 1);
                    string _IndexString = _Index.ToString();
                    string _Name = String.Format("Area {0} ~ {1}", InspParam.InspAreaParam[iLoopCount].MapDataStartNumber, InspParam.InspAreaParam[iLoopCount].MapDataEndNumber);
                    if (_Index > InspParam.InspAreaParam[iLoopCount].MapDataStartNumber && _Index <= InspParam.InspAreaParam[iLoopCount].MapDataEndNumber) continue;

                    bool _Enable = InspParam.InspAreaParam[iLoopCount].Enable;
                    AddInspectionArea(_IndexString, _Name, _Enable);
                }

                else
                {
                    string _IndexString = (iLoopCount + 1).ToString();
                    //string _Name = "Area" + _IndexString;
                    string _Name = "검사영역" + _IndexString;
                    bool _Enable = InspParam.InspAreaParam[iLoopCount].Enable;

                    AddInspectionArea(_IndexString, _Name, _Enable);

                    int _Index = InspParam.InspAreaParam[iLoopCount].BaseIndexNumber;
                    gridViewArea.Rows[_Index].Selected = false;
                }
            }

            if (_Selected != -1)
            {
                int _Index = InspParam.InspAreaParam[_Selected].BaseIndexNumber;

                UpdateInspectionAlgoList(_Index, true);
                InspAreaSelected = _Selected;
            }
        }

        private void AddInspectionArea(string _Index, string _Name, bool _Enable)
        {
            DataGridViewCell[] _GridCell = new DataGridViewCell[5];
            _GridCell[0] = (DataGridViewCell)this.gridAreaID.CellTemplate.Clone();
            _GridCell[1] = (DataGridViewCell)this.gridAreaName.CellTemplate.Clone();
            _GridCell[2] = (DataGridViewCell)this.gridAreaBenchMark.CellTemplate.Clone();
            _GridCell[3] = (DataGridViewCell)this.gridAreaEnable.CellTemplate.Clone();
            _GridCell[4] = (DataGridViewCell)this.gridAreaNgNum.CellTemplate.Clone();

            //BenchMark ComboBox Setting
            DataGridViewComboBoxCell _DataGridComboCell = new DataGridViewComboBoxCell();
            //_DataGridComboCell = (DataGridViewComboBoxCell)this.gridAreaBenchMark.CellTemplate.Clone();
            _DataGridComboCell = this.gridAreaBenchMark.CellTemplate.Clone() as DataGridViewComboBoxCell;
            _DataGridComboCell.Items.Add("None");
            
            for (int iLoopCount = 0; iLoopCount < Convert.ToInt32(_Index) - 1; ++iLoopCount)
            {
                string _ItemName = String.Format("Area{0}", iLoopCount + 1);
                _DataGridComboCell.Items.Add(_ItemName);
            }
            _GridCell[2] = _DataGridComboCell;

            _GridCell[0].Value = _Index;
            _GridCell[1].Value = _Name;
            _GridCell[2].Value = "Area" + InspParam.InspAreaParam[Convert.ToInt32(_Index) - 1].AreaBenchMark;
            if (InspParam.InspAreaParam[Convert.ToInt32(_Index) - 1].AreaBenchMark == 0) _GridCell[2].Value = "None";
            _GridCell[3].Value = _Enable;
            _GridCell[4].Value = InspParam.InspAreaParam[Convert.ToInt32(_Index) - 1].NgAreaNumber.ToString();
            if (InspParam.InspAreaParam[Convert.ToInt32(_Index) - 1].NgAreaNumber == 0) _GridCell[4].Value = "1";

            DataGridViewRow _GridRow = new DataGridViewRow();
            _GridRow.Cells.AddRange(_GridCell);
            gridViewArea.Rows.Add(_GridRow);
        }

        private void UpdateInspectionAlgoList(int _ID, bool _IsNew = false)
        {
            InspAreaSelected = _ID;
            gridViewAlgo.Rows.Clear();

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam[_ID].InspAlgoParam.Count; ++iLoopCount)
            {
                string _Index = (iLoopCount + 1).ToString();
                string _Name = "Algo" + _Index;
                bool _Enable = InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoEnable;

                if (ProjectItem == eProjectItem.NONE)
                {
                    if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_BLOB) _Name = "Defect detection";        //"Blob - Defect"
                    else if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_MULTI_PATTERN) _Name = "기준 멀티패턴 검사";
                    else if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_LINE_FIND) _Name = "제품 라인 검사";
                    else if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_ID) _Name = "코드 검사"; //"ID - Search"
                }

                else if (ProjectItem == eProjectItem.ALIGN)
                {
                    if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_PATTERN) _Name = "기준 패턴 검사";
                    else if (InspParam.InspAreaParam[_ID].InspAlgoParam[iLoopCount].AlgoType == (int)eAlgoType.C_LINE_FIND) _Name = "제품 라인 검사";
                }

                AddInspectionAlgo(_Index, _Name, _Enable);
            }
            gridViewAlgo.ClearSelection();
        }

        private void AddInspectionAlgo(string _Index, string _Name, bool _Enable)
        {
            DataGridViewCell[] _GridCell = new DataGridViewCell[4];
            _GridCell[0] = (DataGridViewCell)this.gridAlgoID.CellTemplate.Clone();
            _GridCell[1] = (DataGridViewCell)this.gridAlgoName.CellTemplate.Clone();
            _GridCell[2] = (DataGridViewCell)this.gridAlgoBenchMark.CellTemplate.Clone();
            _GridCell[3] = (DataGridViewCell)this.gridAlgoEnable.CellTemplate.Clone();

            //BenchMark ComboBox Setting
            DataGridViewComboBoxCell _DataGridComboCell = new DataGridViewComboBoxCell();
            //_DataGridComboCell = (DataGridViewComboBoxCell)this.gridAlgoBenchMark.CellTemplate.Clone();
            _DataGridComboCell = this.gridAlgoBenchMark.CellTemplate.Clone() as DataGridViewComboBoxCell;
            _DataGridComboCell.Items.Add("None");

            for (int iLoopCount = 0; iLoopCount < Convert.ToInt32(_Index) - 1; ++iLoopCount)
            {
                string _ItemName = String.Format("Area{0}", iLoopCount + 1);
                _DataGridComboCell.Items.Add(_ItemName);
            }
            _GridCell[2] = _DataGridComboCell;

            //GridView Setting
            _GridCell[0].Value = _Index;
            _GridCell[1].Value = _Name;
            _GridCell[2].Value = "Area" + InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[Convert.ToInt32(_Index) - 1].AlgoBenchMark;
            if (InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[Convert.ToInt32(_Index) - 1].AlgoBenchMark == 0) _GridCell[2].Value = "None";
            _GridCell[3].Value = _Enable;

            DataGridViewRow _GridRow = new DataGridViewRow();
            _GridRow.Cells.AddRange(_GridCell);
            gridViewAlgo.Rows.Add(_GridRow);
        }

        private void UpdateInspectionAlgoTeachingWindow(int _ID, bool _IsNew = false)
        {
            InspAlgoSelected = _ID;

            eAlgoType _AlgoType = (eAlgoType)InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].AlgoType;
            object _Algorithm = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].Algorithm;
            double _BenchMarkOffsetX = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].BenchMarkOffsetX;
            double _BenchMarkOffsetY = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].BenchMarkOffsetY;

            switch (_AlgoType)
            {
                case eAlgoType.C_PATTERN:       panelTeaching.Controls.Add(ucCogPatternWnd);      ucCogPatternWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY);  break;
                case eAlgoType.C_BLOB_REFER:    panelTeaching.Controls.Add(ucCogBlobReferWnd);    ucCogBlobReferWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY);  break;
                case eAlgoType.C_BLOB:          panelTeaching.Controls.Add(ucCogBlobWnd);         ucCogBlobWnd.SetAlgoRecipe();       break;
                case eAlgoType.C_ID:            panelTeaching.Controls.Add(ucCogIDInspWnd);       ucCogIDInspWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY); break;
                case eAlgoType.C_LINE_FIND:     panelTeaching.Controls.Add(ucCogLineFindWnd);     ucCogLineFindWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY); break;
                case eAlgoType.C_MULTI_PATTERN: panelTeaching.Controls.Add(ucCogMultiPatternWnd); ucCogMultiPatternWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY); break;
                case eAlgoType.C_AUTO_PATTERN:  panelTeaching.Controls.Add(ucCogAutoPatternWnd);  ucCogAutoPatternWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY); break;
                case eAlgoType.C_ELLIPSE:       panelTeaching.Controls.Add(ucCogEllipseFindWnd);  ucCogEllipseFindWnd.SetAlgoRecipe(_Algorithm, _BenchMarkOffsetX, _BenchMarkOffsetY, ResolutionX, ResolutionY); break;
            }
            if (panelTeaching.Controls.Count == 2) panelTeaching.Controls.RemoveAt(0);
            CurrentAlgoType = _AlgoType;
        }

        private void UpdateAlgoResultListAddAlgorithm(eAlgoType _AlgoType)
        {
            //ADD Algorithm
            int _ResultIndex = 0;
            for (int iLoopCount = 0; iLoopCount <= InspAreaSelected; ++iLoopCount)
            {
                if (InspParam.InspAreaParam[iLoopCount].Enable == true)
                {
                    for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                    {
                        if (InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable == true)
                            _ResultIndex++;
                    }
                }
            }

            //ADD ResultList
            if (AlgoResParamList.Count >= (_ResultIndex - 1))
            {
                AlgoResultParameter _AlgoResTemp = new AlgoResultParameter();
                _AlgoResTemp.ResultAlgoType = _AlgoType;
                AlgoResParamList.Insert(_ResultIndex - 1, _AlgoResTemp);
            }
        }

        private void AlgorithmAreaDisplayRefresh()
        {
            int _ID = Convert.ToInt32(gridViewAlgo.SelectedRows[0].Cells[(int)eAlgoList.ID].Value) - 1;
            //GetAlgoResultDataOffset(InspAreaSelected, _ID);
            UpdateInspectionAlgorithmAreaDraw(_ID);

            CogRectangle _AlgoRegion = new CogRectangle();
            CogRectangle _Boundary = new CogRectangle();
            _Boundary.SetCenterWidthHeight(InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterX, InspParam.InspAreaParam[InspAreaSelected].AreaRegionCenterY,
                                            InspParam.InspAreaParam[InspAreaSelected].AreaRegionWidth, InspParam.InspAreaParam[InspAreaSelected].AreaRegionHeight);
            if (false == GetCorrectionRectangle(kpTeachDisplay, _Boundary, ref _AlgoRegion)) { MessageBox.Show("The rectangle is outside the inspection area."); return; }

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawStaticShape(_Boundary, "InspRegion", CogColorConstants.Green, 2);
            kpTeachDisplay.DrawText("InspRegion", _Boundary.X, _Boundary.Y - 35, CogColorConstants.Green, 10);
            kpTeachDisplay.DrawStaticShape(_AlgoRegion, "AlgoRegion", CogColorConstants.Orange, 2);
            kpTeachDisplay.DrawText("AlgoRegion", _AlgoRegion.X, _AlgoRegion.Y - 35, CogColorConstants.Orange, 10);
        }
        #endregion Inspection Area & Algorithm Gridview Update

        #region Teaching Parameter Set & UI Setting
        public void SetTeachingImage(CogImage8Grey _InspectionImage, int _Width, int _Height)
        {
            InspectionImage = _InspectionImage;
            InspectionImageWidth = _Width;
            InspectionImageHeight = _Height;

            kpTeachDisplay.SetDisplayImage(InspectionImage);
        }

        private bool GetCorrectionRectangle(KPDisplay.KPCogDisplayControl _DisplayCtrl, CogRectangle _Boundary, ref CogRectangle _ResultRegion)
        {
            bool _Result = true;

            double _StartX, _StartY, _EndX, _EndY, _Width, _Height;
            CogRectangle _CurrentRegion = _DisplayCtrl.GetInterActiveRectangle();
            _CurrentRegion.GetXYWidthHeight(out _StartX, out _StartY, out _Width, out _Height);
            _EndX = _StartX + _Width;
            _EndY = _StartY + _Height;

            //영역이 Boundary를 완전히 벗어난 경우는 Fail
            if ((_EndX < _Boundary.X) || (_StartX > _Boundary.X + _Boundary.Width) || (_EndY < _Boundary.Y) || (_StartY > _Boundary.Y + _Boundary.Height)) return false;

            //Boundary 밖으로 벗어난 경우 Boundary 끝으로 변경
            if (_StartX < _Boundary.X) { _Width = (_Width + (_StartX - _Boundary.X)); _StartX = _Boundary.X; }
            if (_StartY < _Boundary.Y) { _Height = (_Height + (_StartY - _Boundary.Y)); _StartY = _Boundary.Y; }
            if (_EndX > (_Boundary.X + _Boundary.Width)) { _Width = _Width - (_EndX - (_Boundary.X + _Boundary.Width)); }
            if (_EndY > (_Boundary.Y + _Boundary.Height)) { _Height = _Height - (_EndY - (_Boundary.Y + _Boundary.Height)); }

            _ResultRegion.SetXYWidthHeight(_StartX, _StartY, _Width, _Height);

            return _Result;
        }

        private void GetAreaResultDataOffset(int _Area)
        {
            AreaOffsetX = AreaOffsetY;

            int _BenchmarkIndex = InspParam.InspAreaParam[_Area].AreaBenchMark - 1;

            //BenchMark가 있는 경우
            if (_BenchmarkIndex >= 0 && AreaResParamList.Count > _BenchmarkIndex)
            {
                AreaOffsetX = AreaResParamList[_BenchmarkIndex].OffsetX;
                AreaOffsetY = AreaResParamList[_BenchmarkIndex].OffsetY;
            }
        }

        private void GetAlgoResultDataOffset(int _Area, int _Algo)
        {
            AlgoOffsetX = AlgoOffsetY = 0;

            int _BenchmarkIndex = InspParam.InspAreaParam[_Area].InspAlgoParam[_Algo].AlgoBenchMark;

            int _ResultIndex = 0;

            for (int iLoopCount = 0; iLoopCount < _Area; ++iLoopCount)
            {
                if (InspParam.InspAreaParam[iLoopCount].Enable == true)
                {
                    for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                    {
                        if (InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoEnable == true)
                            _ResultIndex++;
                    }
                }
            }

            //BenchMark가 있는 경우
            if (_BenchmarkIndex > 0)
            {
                _ResultIndex = _ResultIndex + (_BenchmarkIndex - 1);
                if (AlgoResParamList.Count > _ResultIndex && _ResultIndex >= 0)
                {
                    AlgoOffsetX = AlgoResParamList[_ResultIndex].OffsetX;
                    AlgoOffsetY = AlgoResParamList[_ResultIndex].OffsetY;
                }
            }
        }

        public void SetResultData(AreaResultParameterList _AreaResParamList, AlgoResultParameterList _AlgoResParamList)
        {
            AreaOffsetX = AreaOffsetY = 0;
            AlgoOffsetX = AlgoOffsetY = 0;

            AreaResParamList = new AreaResultParameterList();
            AreaResParamList.Clear();
            AreaResParamList = _AreaResParamList;

            AlgoResParamList = new AlgoResultParameterList();
            AlgoResParamList.Clear();
            AlgoResParamList = _AlgoResParamList;
        }

        private void UpdateTeachingStatus(eTeachStep _TeachStep)
        {
            string _AreaSelectMsg = String.Format("Area{0} Select", InspAreaSelected + 1);
            string _AreaSetMsg = String.Format(" >> Area Set");
            string _AlgoSelectMsg = String.Format(" >> Algo{0} Select", InspAlgoSelected + 1);
            string _AlgoSetMsg = String.Format(" >> Algo Set");

            switch (_TeachStep)
            {
                case eTeachStep.AREA_CLEAR:     labelStatus.Text = ""; break;
                case eTeachStep.AREA_SELECT:    labelStatus.Text = _AreaSelectMsg; break;
                case eTeachStep.AREA_SET:       labelStatus.Text = _AreaSelectMsg + _AreaSetMsg; break;
                case eTeachStep.ALGO_CLEAR:     labelStatus.Text = _AreaSelectMsg + _AreaSetMsg; break;
                case eTeachStep.ALGO_SELECT:    labelStatus.Text = _AreaSelectMsg + _AreaSetMsg + _AlgoSelectMsg; break;
                case eTeachStep.ALGO_SET:       labelStatus.Text = _AreaSelectMsg + _AreaSetMsg + _AlgoSelectMsg + _AlgoSetMsg; break;
            }

            CurrentTeachStep = _TeachStep;
        }

        private void UpdateInspectionArea(int _ID)
        {
            double _CenterX = InspParam.InspAreaParam[_ID].AreaRegionCenterX + AreaOffsetX;
            double _CenterY = InspParam.InspAreaParam[_ID].AreaRegionCenterY + AreaOffsetY;
            double _Width = InspParam.InspAreaParam[_ID].AreaRegionWidth;
            double _Height = InspParam.InspAreaParam[_ID].AreaRegionHeight;

            CogRectangle _InspRegion = new CogRectangle();
            _InspRegion.SetCenterWidthHeight(_CenterX, _CenterY, _Width, _Height);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawInterActiveShape(_InspRegion, "InspRegion", CogColorConstants.Green);
        }

        private void UpdateInspectionAlgorithmAreaDraw(int _ID)
        {
            double _CenterX = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].AlgoRegionCenterX + AlgoOffsetX - AreaOffsetX;
            double _CenterY = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].AlgoRegionCenterY + AlgoOffsetY - AreaOffsetY;
            double _Width = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].AlgoRegionWidth;
            double _Height = InspParam.InspAreaParam[InspAreaSelected].InspAlgoParam[_ID].AlgoRegionHeight;

            CogRectangle _AlgoRegion = new CogRectangle();
            _AlgoRegion.SetCenterWidthHeight(_CenterX, _CenterY, _Width, _Height);

            kpTeachDisplay.ClearDisplay();
            kpTeachDisplay.DrawStaticShape(AreaRegionRectangle, "InspRegion", CogColorConstants.Green, 2);
            kpTeachDisplay.DrawText("InspRegion", AreaRegionRectangle.X, AreaRegionRectangle.Y - 35, CogColorConstants.Green, 10);
            kpTeachDisplay.DrawInterActiveShape(_AlgoRegion, "AlgoRegion", CogColorConstants.Orange);
        }
        #endregion Teaching Parameter Set & UI Setting
    }
}
