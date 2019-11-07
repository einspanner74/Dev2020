using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;

using LogMessageManager;
using ParameterManager;
using CameraManager;
using WindowKeyPad;

namespace InspectionSystemManager
{
    public partial class InspectionWindow : Form
    {
        #region Registry Varibale
        private string Password = "1234";
        private RegistryKey RegPassword;
        private string regPasswordPath = string.Format(@"KPVision\UserInfo\CommonPassword");
        #endregion

        #region Inspection Variable
        private InspectionPattern           InspPatternProc;
        private InspectionBlobReference     InspBlobReferProc;
        private InspectionID                InspIDProc;
        private InspectionLineFind          InspLineFindProc;
        private InspectionMultiPattern      InspMultiPatternProc;
        private InspectionAutoPattern       InspAutoPatternProc;
        private InspectionEllipse           InspEllipseProc;

        private CCameraManager              CameraManager;

        private TeachingWindow          TeachWnd;
        private ImageDeleteWindow       ImageDeleteWnd;
        private ManualInspectionWindow  ManualInspWnd;
        private WindowKeypadControl     KeypadWnd = new WindowKeypadControl(false);

        private InspectionParameter     InspParam = new InspectionParameter();
        private MapDataParameter        MapDataParam = new MapDataParameter();
        public AreaResultParameterList  AreaResultParamList = new AreaResultParameterList();
        public AlgoResultParameterList  AlgoResultParamList = new AlgoResultParameterList();
        private SendResultParameter     SendResParam = new SendResultParameter();
		
        private double AnyReferenceX = 0;
        private double AnyReferenceY = 0;

        private int ID;
        private eProjectType ProjectType;
        private eProjectItem ProjectItem;
        public eInspMode InspMode = eInspMode.TRI_INSP;
        private string FormName;
        private string RecipeName;
        private bool ResizingFlag = false;
        private bool IsResizing = false;
        private Point LastPosition = new Point(0, 0);
        public bool IsSimulationMode = false;

        private CogImageFileTool OriginImageFileTool = new CogImageFileTool();
        private CogImage8Grey OriginImage = new CogImage8Grey();
        private CogImage8Grey OriginConstImage = new CogImage8Grey();

        private double ResolutionX = 0.005;
        private double ResolutionY = 0.005;

        //검사 시 Area 별 Offset 값을 적용할 변수
        private double AreaBenchMarkOffsetX;
        private double AreaBenchMarkOffsetY;
        private double AreaBenchMarkOffsetT;
        private double BenchMarkOffsetX = 0;
        private double BenchMarkOffsetY = 0;
        private double BenchMarkOffsetT = 0;
        private int AreaAlgoCount;

        private string CameraType;
        private double CameraRotate;
        private int ImageSizeWidth = 0;
        private int ImageSizeHeight = 0;
        private bool IsCamLiveFlag = false;
        private bool IsCrossLine = false;
        private bool IsMenuHide = false;
        private bool IsResultDisplay = true;

        private double DisplayZoomValue = 1;
        private double DisplayPanXValue = 0;
        private double DisplayPanYValue = 0;

        private Thread ThreadInspectionProcess;
        private bool IsThreadInspectionProcessExit = false;
        public bool IsThreadInspectionProcessTrigger = false;
        public bool IsInspectionComplete = false;
        private bool InspectionPassFlag = false;

        private Thread ThreadLiveCheck;
        private bool IsThreadLiveCheckExit = false;

        private Thread ThreadImageSave;
        private bool IsThreadImageSaveExit = false;
        public bool IsThreadImageSaveTrigger = false;
        private eSaveMode ImageAutoSaveMode = eSaveMode.ONLY_NG;

        public delegate void InspectionWindowHandler(eIWCMD _Command, object _Value = null, int _ID = 0);
        public event InspectionWindowHandler InspectionWindowEvent;

        private ToolTip InspMenuToolTip = new ToolTip();
        #endregion Inspection Variable

        #region Initialize & DeInitialize
        public InspectionWindow()
        {
            InitializeComponent();
            #region ToolTip Setting
            InspMenuToolTip.ShowAlways = true;
            InspMenuToolTip.AutoPopDelay = 0;
            InspMenuToolTip.InitialDelay = 0;
            InspMenuToolTip.ReshowDelay = 500;
            InspMenuToolTip.SetToolTip(btnInspection, InspectionSystemManager.LanguageResource.ToolTip_Inspection);
            InspMenuToolTip.SetToolTip(btnOneShot, InspectionSystemManager.LanguageResource.ToolTip_OneShotInspection);
            InspMenuToolTip.SetToolTip(btnRecipe, InspectionSystemManager.LanguageResource.ToolTip_Teaching);
            InspMenuToolTip.SetToolTip(btnRecipeSave, InspectionSystemManager.LanguageResource.ToolTip_TeachingSave);
            InspMenuToolTip.SetToolTip(btnLive, InspectionSystemManager.LanguageResource.ToolTip_CameraLive);
            InspMenuToolTip.SetToolTip(btnImageLoad, InspectionSystemManager.LanguageResource.ToolTip_ImageLoad);
            InspMenuToolTip.SetToolTip(btnImageSave, InspectionSystemManager.LanguageResource.ToolTip_ImageSave);
            InspMenuToolTip.SetToolTip(btnImageAutoSave, InspectionSystemManager.LanguageResource.ToolTip_ImageAutoSave);
            InspMenuToolTip.SetToolTip(btnConfigSave, InspectionSystemManager.LanguageResource.ToolTip_ImageConfigurationSave);
            InspMenuToolTip.SetToolTip(btnAutoDelete, InspectionSystemManager.LanguageResource.ToolTip_SetImageAutoDelete);
            InspMenuToolTip.SetToolTip(btnCrossBar, InspectionSystemManager.LanguageResource.ToolTip_CrossBar);
            InspMenuToolTip.SetToolTip(btnImageResultDisplay, InspectionSystemManager.LanguageResource.ToolTip_ResultDisplay);
            #endregion

            LoadRegistry();

#if _DEBUG
            btnQuickInspection.Visible = true;
#endif
        }

        public void Initialize(Object _OwnerForm, int _ID, InspectionParameter _InspParam, eProjectType _ProjectType, eProjectItem _ProjectItem, string _FormName, string _RecipeName, bool _IsSimulationMode, string _FolderPath)
        {
            ID = _ID;
            ProjectItem = _ProjectItem;
            ProjectType = _ProjectType;
            FormName = _FormName;
            RecipeName = _RecipeName;
            IsSimulationMode = _IsSimulationMode;

            this.labelStatus.Text = "";
            this.labelTitle.Text = _FormName;
            this.Owner = (Form)_OwnerForm;

            InspPatternProc = new InspectionPattern();
            InspPatternProc.Initialize();

            InspMultiPatternProc = new InspectionMultiPattern();
            InspMultiPatternProc.Initialize();

            InspAutoPatternProc = new InspectionAutoPattern();
            InspAutoPatternProc.Initialize();

            InspBlobReferProc = new InspectionBlobReference();
            InspBlobReferProc.Initialize();
            
            InspEllipseProc = new InspectionEllipse();
            
            InspIDProc = new InspectionID();
            InspIDProc.Initialize();

            InspLineFindProc = new InspectionLineFind();
            InspLineFindProc.Initialize();
            
            CameraManager = new CCameraManager();

            AreaResultParamList = new AreaResultParameterList();
            AlgoResultParamList = new AlgoResultParameterList();

            ThreadInspectionProcess = new Thread(ThreadInspectionProcessFunction);
            IsThreadInspectionProcessExit = false;
            IsThreadInspectionProcessTrigger = false;
            ThreadInspectionProcess.Start();

            ThreadLiveCheck = new Thread(ThreadLiveCheckFunction);
            ThreadLiveCheck.IsBackground = true;
            IsThreadLiveCheckExit = false;
            ThreadLiveCheck.Start();

            ThreadImageSave = new Thread(ThreadImageSaveFunction);
            ThreadImageSave.IsBackground = true;
            IsThreadImageSaveExit = false;
            IsThreadImageSaveTrigger = false;
            ThreadImageSave.Start();

            TeachWnd = new TeachingWindow();

            ManualInspWnd = new ManualInspectionWindow();
            ManualInspWnd.ImageLoadEvent += new ManualInspectionWindow.ImageLoadHandler(ManualInspectionImageLoad);
            ManualInspWnd.ImageInspectionEvent += new ManualInspectionWindow.ImageInspectionHandler(ManualInspection);

            if(eProjectType.NAVIEN == _ProjectType)
            {
                btnAutoDelete.Visible = false;

                //if(_FolderPath == "") ImageDeleteWnd = new ImageDeleteWindow(this.labelTitle.Text);
                //else                  ImageDeleteWnd = new ImageDeleteWindow(_FolderPath, true);
                //btnImageResultDisplay.Visible = true;
                //IsResultDisplay = false;
            }
        }

        public void InitializeResolution(double _ResolutionX, double _ResolutionY)
        {
            ResolutionX = _ResolutionX;
            ResolutionY = _ResolutionY;

            InspParam.ResolutionX = _ResolutionX;
            InspParam.ResolutionY = _ResolutionY;
        }

        /// <summary>
        ///  Camera 초기화
        /// </summary>
        /// <param name="_CamType"></param>
        /// <param name="_CamInfo"></param>
        /// <param name="_Width"></param>
        /// <param name="_Height"></param>
        public void InitializeCam(string _CamType, string _CamInfo, int _Width, int _Height, double _Rotate, string _CameraName)
        {
            if (IsSimulationMode) return;

            ImageSizeWidth = _Width;
            ImageSizeHeight = _Height;
            CameraRotate = _Rotate;

            if (_CamType == eCameraType.Euresys.ToString() || _CamType == eCameraType.EuresysIOTA.ToString())
            {
                CameraManager.ImageGrabEvent += new CCameraManager.ImageGrabHandler(SetDisplayGrabImage);
                CameraManager.Initialize(ID, _CamType, _CamInfo);
            }

            else if (_CamType == eCameraType.BaslerGE.ToString())
            {
                CameraManager.ImageGrabEvent += new CCameraManager.ImageGrabHandler(SetDisplayGrabImage);
                CameraManager.Initialize(ID, _CamType, _CamInfo);
            }

            else if(_CamType == eCameraType.Dalsa.ToString())
            {
                CameraManager.ImageGrabEvent += new CCameraManager.ImageGrabHandler(SetDisplayGrabImage);
                CameraManager.Initialize(ID, _CamType, _CamInfo, _CameraName);
            }

            else
            {
                //CameraManager.ImageGrabIntPtrEvent += new CCameraManager.ImageGrabIntPtrHandler(SetDisplayGrabIntPtrImage);
                CameraManager.ImageGrabEvent += new CCameraManager.ImageGrabHandler(SetDisplayGrabImage);
                CameraManager.Initialize(ID, _CamType, _CamInfo);
            }
        }

        public void Deinitialize()
        {
            if (InspPatternProc != null)    InspPatternProc.DeInitialize();
            if (InspBlobReferProc != null)  InspBlobReferProc.DeInitialize();
            if (InspLineFindProc != null)   InspLineFindProc.DeInitialize();
            if (ImageDeleteWnd!= null)      ImageDeleteWnd.DeInitialize();
			CameraManager.DeInitialilze();
            CameraManager.ImageGrabEvent -= new CCameraManager.ImageGrabHandler(SetDisplayGrabImage);

            ManualInspWnd.ImageLoadEvent -= new ManualInspectionWindow.ImageLoadHandler(ManualInspectionImageLoad);
            ManualInspWnd.ImageInspectionEvent -= new ManualInspectionWindow.ImageInspectionHandler(ManualInspection);

            if (ThreadInspectionProcess != null) { IsThreadInspectionProcessExit = true; Thread.Sleep(200); ThreadInspectionProcess.Abort(); ThreadInspectionProcess = null; }
            if (ThreadLiveCheck != null) { IsThreadLiveCheckExit = true; Thread.Sleep(200); ThreadLiveCheck.Abort(); ThreadLiveCheck = null; }
            if (ThreadImageSave != null) { IsThreadImageSaveExit = true; Thread.Sleep(200); ThreadImageSave.Abort(); ThreadImageSave = null; }
        }

        private void LoadRegistry()
        {
            RegPassword = Registry.CurrentUser.CreateSubKey(regPasswordPath);
            Password = RegPassword.GetValue("Value", "1234").ToString();
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Load Registry");
        }

        private void SaveRegistry()
        {
            RegPassword.SetValue("Value", Password, RegistryValueKind.String);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Save Registry");
        }

        public void SetLocation(int _StartX, int _StartY)
        {
            this.Location = new Point(_StartX, _StartY);
        }

        public void SetWindowSize(int _Width, int _Height)
        {
            this.Size = new Size(_Width, _Height);
        }

        public void SetWindowDisplayInfo(double _Zoom, double _PanX, double _PanY)
        {
            DisplayZoomValue = _Zoom;
            DisplayPanXValue = _PanX;
            DisplayPanYValue = _PanY;
        }

        public void GetWindowDisplayInfo(out double _Zoom, out double _PanX, out double _PanY)
        {
            _Zoom = DisplayZoomValue;
            _PanX = DisplayPanXValue;
            _PanY = DisplayPanYValue;
        }

        public void SetInspectionParameter(InspectionParameter _InspParam, bool _IsNew = true)
        {
            if (InspParam != null) FreeInspectionParameters(ref InspParam);
            CParameterManager.RecipeCopy(_InspParam, ref InspParam);

            //Reference File(VPP) Load
        }

        public void SetSystemMode(eSysMode _SystemMode)
        {
            if (ProjectType != eProjectType.NAVIEN)
            {
                if (_SystemMode == eSysMode.AUTO_MODE)
                {
                    btnInspection.Enabled = false;
                    btnOneShot.Enabled = false;
                    btnRecipe.Enabled = false;
                    btnRecipeSave.Enabled = false;
                    //btnRecipeSave.Enabled = false;
                    btnLive.Enabled = false;
                    btnImageLoad.Enabled = false;
                    //btnImageSave.Enabled = false;
                    //btnConfigSave.Enabled = false;
                }

                else
                {
                    btnInspection.Enabled = true;
                    btnOneShot.Enabled = true;
                    btnRecipe.Enabled = true;
                    btnRecipeSave.Enabled = true;
                    //btnRecipeSave.Enabled = true;
                    btnLive.Enabled = true;
                    btnImageLoad.Enabled = true;
                    //btnImageSave.Enabled = true;
                    //btnConfigSave.Enabled = true;
                }
            }
        }

        public void SetMapDataParameter(MapDataParameter _MapDataParam)
        {
            CParameterManager.RecipeCopy(_MapDataParam, ref MapDataParam);
        }

        public void FreeInspectionParameters(ref InspectionParameter _InspParam)
        {
            for (int iLoopCount = 0; iLoopCount < _InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                for (int jLoopCount = 0; jLoopCount < _InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                    FreeInspectionParameter(ref _InspParam, iLoopCount, jLoopCount);
            }
        }

        public void FreeInspectionParameter(ref InspectionParameter _InspParam, int _AreaIndex, int _AlgoIndex)
        {
            if (eAlgoType.C_PATTERN == (eAlgoType)_InspParam.InspAreaParam[_AreaIndex].InspAlgoParam[_AlgoIndex].AlgoType)
            {

            }
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void InspectionWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            if (!IsResizing) // handle cursor type
            {
                bool resize_x = e.X > (this.Width - 8);
                bool resize_y = e.Y > (this.Height - 8);

                if (resize_x && resize_y) this.Cursor = Cursors.SizeNWSE;
                else if (resize_x) this.Cursor = Cursors.SizeWE;
                else if (resize_y) this.Cursor = Cursors.SizeNS;
                else this.Cursor = Cursors.Default;
            }
            else // handle resize
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

                int w = this.Size.Width;
                int h = this.Size.Height;

                if (this.Cursor.Equals(Cursors.SizeNWSE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h + (e.Location.Y - this.LastPosition.Y));
                else if (this.Cursor.Equals(Cursors.SizeWE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h);
                else if (this.Cursor.Equals(Cursors.SizeNS)) this.Size = new Size(w, h + (e.Location.Y - this.LastPosition.Y));

                this.LastPosition = e.Location;
            }
        }

        private void InspectionWindow_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsResizing = true;
            this.LastPosition = e.Location;
        }

        private void InspectionWindow_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsResizing = false;
        }

        private void InspectionWindow_Resize(object sender, EventArgs e)
        {
            Size _TitleSize = new Size(this.Size.Width, labelTitle.Size.Height);
            Point _Location = panelMain.Location;

            labelTitle.Size = new Size(_TitleSize.Width - 6, _TitleSize.Height);
            labelTitle.Location = new Point(3, 2);

            panelMain.Size = new Size(_TitleSize.Width - 6, this.Size.Height - _TitleSize.Height - 6);
            panelMain.Location = new Point(3, _Location.Y);

            panelMenu.Size = new Size(panelMain.Width - 6, panelMenu.Height);
            panelMenu.Location = new Point(2, 2);

            Size _DisplayMain = kpCogDisplayMain.Size;
            kpCogDisplayMain.Size = new Size(panelMain.Width - 6, panelMain.Height - panelMenu.Height - 6);
            kpCogDisplayMain.Location = new Point(2, panelMenu.Height + 3);

            panelMain.Invalidate();
            labelTitle.Invalidate();
            panelMenu.Invalidate();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            this.Cursor = Cursors.Default;
        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMenu.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void labelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ResizingFlag = !ResizingFlag;
            if (true == ResizingFlag) labelTitle.ForeColor = Color.Chocolate;
            else labelTitle.ForeColor = Color.WhiteSmoke;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void InspectionWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }
        #endregion Control Default Event

        #region Control Event
        private void btnInspection_Click(object sender, EventArgs e)
        {
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM{0} Single Inspection Run", ID + 1), CLogManager.LOG_LEVEL.LOW);
            CParameterManager.SystemModeBackup = CParameterManager.SystemMode;
            CParameterManager.SystemMode = eSysMode.ONESHOT_MODE;
            ContinuesGrabStop();
            Inspection();
            CParameterManager.SystemMode = CParameterManager.SystemModeBackup;
        }

        private void btnOneShot_Click(object sender, EventArgs e)
        {
            if (true == IsSimulationMode) return;

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM{0} Single One-Shot Inspection Run", ID + 1), CLogManager.LOG_LEVEL.LOW);

            CParameterManager.SystemMode = eSysMode.ONESHOT_MODE;
            ContinuesGrabStop();
            GrabAndInspection();
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
#if _RELEASE
            //Password 입력 후 입장~
            if (ProjectType == eProjectType.NAVIEN)
            {
                KeypadWnd.Initialize(false, true);
                KeypadWnd.SetPassword(Password);
                KeypadWnd.ShowDialog();

                if (KeypadWnd.GetPassword() != Password) { Password = KeypadWnd.GetPassword(); SaveRegistry(); }
                if (KeypadWnd.DialogResult == DialogResult.Cancel) { return; }
                if (KeypadWnd.KeyPadCharactor != Password && KeypadWnd.KeyPadCharactor != "510704") { MessageBox.Show("비밀번호가 틀렸습니다."); return; }
            }

            //LDH, 2019.08.29, 관리자모드 상태 표시
            if (ProjectType == eProjectType.NAVIEN) InspectionWindowEvent(eIWCMD.NOTICE_WINDOW);
#endif

            ContinuesGrabStop();
            InspectionWindowEvent(eIWCMD.TEACHING, true);
            Teaching();
            InspectionWindowEvent(eIWCMD.TEACHING, false);
        }

        private void btnRecipeSave_Click(object sender, EventArgs e)
        {
            InspectionWindowEvent(eIWCMD.TEACH_SAVE, ID);
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            if (IsSimulationMode) return;

            IsCrossLine = false;
            kpCogDisplayMain.ClearDisplay();
            IsCamLiveFlag = !IsCamLiveFlag;
            CameraManager.CamLive(IsCamLiveFlag);

            if (IsCamLiveFlag)  { CParameterManager.SystemMode = eSysMode.LIVE_MODE;    labelStatus.Text = "(Live)"; }
            else                { CParameterManager.SystemMode = eSysMode.MANUAL_MODE;  labelStatus.Text = ""; }
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "CamLive : " + IsCamLiveFlag.ToString(), CLogManager.LOG_LEVEL.MID);
        }

        private void btnImageLoad_Click(object sender, EventArgs e)
        {
            ContinuesGrabStop();
            LoadCogImage();
            kpCogDisplayMain.SetDisplayZoom(DisplayZoomValue);
            kpCogDisplayMain.SetDisplayPanX(DisplayPanXValue);
            kpCogDisplayMain.SetDisplayPanY(DisplayPanYValue);
        }
		
		private void btnAutoDelete_Click(object sender, EventArgs e)
        {
            ImageDeleteWnd.ShowDialog();
        }

        private void btnImageSave_Click(object sender, EventArgs e)
        {
            SaveCogImage("M");
        }

        private void btnImageAutoSave_Click(object sender, EventArgs e)
        {
            if (eSaveMode.ALL == ImageAutoSaveMode)
            {
                ImageAutoSaveMode = eSaveMode.ONLY_NG;
                btnImageAutoSave.ButtonImage = InspectionSystemManager.Properties.Resources.AutoStop;
                btnImageAutoSave.ButtonImageOver = InspectionSystemManager.Properties.Resources.AutoStopOver;
                btnImageAutoSave.ButtonImageDown = InspectionSystemManager.Properties.Resources.AutoStopDown;
            }
            else
            {
                ImageAutoSaveMode = eSaveMode.ALL;
                btnImageAutoSave.ButtonImage = InspectionSystemManager.Properties.Resources.AutoSaveImage;
                btnImageAutoSave.ButtonImageOver = InspectionSystemManager.Properties.Resources.AutoSaveImageOver;
                btnImageAutoSave.ButtonImageDown = InspectionSystemManager.Properties.Resources.AutoSaveImageDown;
            }
        }

        private void btnImageResultDisplay_Click(object sender, EventArgs e)
        {
            IsResultDisplay = !IsResultDisplay;
            if (IsResultDisplay)
            {
                btnImageResultDisplay.ButtonImage = InspectionSystemManager.Properties.Resources.Result;
                btnImageResultDisplay.ButtonImageOver = InspectionSystemManager.Properties.Resources.ResultOver;
                btnImageResultDisplay.ButtonImageDown = InspectionSystemManager.Properties.Resources.ResultDown;
            }

            else
            {
                btnImageResultDisplay.ButtonImage = InspectionSystemManager.Properties.Resources.ResultStop;
                btnImageResultDisplay.ButtonImageOver = InspectionSystemManager.Properties.Resources.ResultStopOver;
                btnImageResultDisplay.ButtonImageDown = InspectionSystemManager.Properties.Resources.ResultStopDown;
            }
        }

        private void btnConfigSave_Click(object sender, EventArgs e)
        {
            DisplayZoomValue = kpCogDisplayMain.GetDisplayZoom();
            DisplayPanXValue = kpCogDisplayMain.GetDisplayPanX();
            DisplayPanYValue = kpCogDisplayMain.GetDisplayPanY();
        }

        private void btnCrossBar_Click(object sender, EventArgs e)
        {
            IsCrossLine = !IsCrossLine;
            kpCogDisplayMain.ClearDisplay();

            //if (IsCrossLine) kpCogDisplayMain.DrawCross(ImageSizeWidth / 2, ImageSizeHeight / 2, ImageSizeWidth, ImageSizeHeight, "Cross", CogColorConstants.Green);
            if (IsCrossLine) kpCogDisplayMain.DrwaInterActiveCross(ImageSizeWidth / 2, ImageSizeHeight / 2, 1, 8000, "Cross", CogColorConstants.Green);
        }

        private void panelMenuHide_Click(object sender, EventArgs e)
        {
            IsMenuHide = !IsMenuHide;
            double _PanX = kpCogDisplayMain.GetDisplayPanX();
            double _PanY = kpCogDisplayMain.GetDisplayPanY();
            double _Zoom = kpCogDisplayMain.GetDisplayZoom();

            if (IsMenuHide)
            {
                kpCogDisplayMain.Location = new Point(kpCogDisplayMain.Location.X, kpCogDisplayMain.Location.Y - 47);
                kpCogDisplayMain.Size = new Size(kpCogDisplayMain.Width, kpCogDisplayMain.Height + 47);
                panelMenuHide.BackgroundImage = InspectionSystemManager.Properties.Resources.Arrow_Down;

                kpCogDisplayMain.SetDisplayPanX(_PanX);
                kpCogDisplayMain.SetDisplayPanY(_PanY);
                kpCogDisplayMain.SetDisplayZoom(_Zoom);
            }

            else
            {
                kpCogDisplayMain.Location = new Point(kpCogDisplayMain.Location.X, kpCogDisplayMain.Location.Y + 47);
                kpCogDisplayMain.Size = new Size(kpCogDisplayMain.Width, kpCogDisplayMain.Height - 47);
                panelMenuHide.BackgroundImage = InspectionSystemManager.Properties.Resources.Arrow_Up;

                kpCogDisplayMain.SetDisplayPanX(_PanX);
                kpCogDisplayMain.SetDisplayPanY(_PanY);
                kpCogDisplayMain.SetDisplayZoom(_Zoom);
            }
        }

        private void btnQuickInspection_Click(object sender, EventArgs e)
        {
            ManualInspWnd.ShowManualInspectionWindow();
        }

        private void labelZoomPlus_Click(object sender, EventArgs e)
        {
            double _Zoom = kpCogDisplayMain.GetDisplayZoom() * 100;
            _Zoom = Math.Truncate(_Zoom) + 1.5;
            kpCogDisplayMain.SetDisplayZoom(_Zoom / 100);
        }

        private void labelZoomMinus_Click(object sender, EventArgs e)
        {
            double _Zoom = kpCogDisplayMain.GetDisplayZoom() * 100;
            _Zoom = Math.Ceiling(_Zoom) - 1.5;
            kpCogDisplayMain.SetDisplayZoom(_Zoom / 100);
        }
        #endregion Control Event

        #region Set Image Display Control
        public void SetDisplayImage(CogImage8Grey _DisplayImage)
        {
            if (_DisplayImage != null)
            {
                ImageSizeWidth = _DisplayImage.Width;
                ImageSizeHeight = _DisplayImage.Height;
            }

            IsCrossLine = false;
            kpCogDisplayMain.ClearDisplay();
            kpCogDisplayMain.SetDisplayImage(_DisplayImage);
            
            GC.Collect();
        }

        //LDH, 2018.07.04, byte Image display 함수 
        private void SetDisplayGrabImage(byte[] Image)
        {
            kpCogDisplayMain.SetDisplayImage(Image, ImageSizeWidth, ImageSizeHeight, CameraRotate);
            if (CParameterManager.SystemMode != eSysMode.LIVE_MODE)
            {
                kpCogDisplayMain.SetDisplayZoom(DisplayZoomValue);
                kpCogDisplayMain.SetDisplayPanX(DisplayPanXValue);
                kpCogDisplayMain.SetDisplayPanY(DisplayPanYValue);
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - H/W Trigger ON Grab", ID + 1), CLogManager.LOG_LEVEL.LOW);
            }

            OriginImage = (CogImage8Grey)kpCogDisplayMain.GetDisplayImage();
            OriginConstImage = new CogImage8Grey((CogImage8Grey)kpCogDisplayMain.GetDisplayImage());
            //OriginConstImage = (CogImage8Grey)kpCogDisplayMain.GetDisplayImage();
            GC.Collect();

            //Auto / Manual Mode 구분
            //if (ProjectType == eProjectType.BLOWER)
            //{
            //    if (CParameterManager.SystemMode == eSysMode.AUTO_MODE)
            //    {
            //        IsInspectionComplete = false;
            //        IsThreadInspectionProcessTrigger = true;
            //    }
            //}
        }

        private void SetDisplayGrabIntPtrImage(IntPtr _Image)
        {
            var _CogRoot = new CogImage8Root();
            _CogRoot.Initialize(ImageSizeWidth, ImageSizeHeight, _Image, ImageSizeWidth, null);

            var _CogImage = new CogImage8Grey();
            _CogImage.SetRoot(_CogRoot);

            kpCogDisplayMain.SetDisplayImage(_CogImage);
            GC.Collect();
        }

        public CogImage8Grey GetOriginImage()
        {
            return OriginImage;
        }
        #endregion Set Image Display Control

        #region Image Load & Save
        private string LoadCogImage()
        {
            string _ImageFileName = "";
            string _ImageFilePath = "";

            OpenFileDialog _OpenDialog = new OpenFileDialog();
            _OpenDialog.InitialDirectory = @"D:\VisionInspectionData";
            _OpenDialog.Filter = "BmpFile (*.bmp)|*.bmp";

            try
            {
                if (_OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    _ImageFileName = _OpenDialog.SafeFileName;
                    _ImageFilePath = _OpenDialog.FileName;
                    OriginImageFileTool.Operator.Open(_ImageFilePath, CogImageFileModeConstants.Read);
                    OriginImageFileTool.Run();
                    OriginImage = (CogImage8Grey)OriginImageFileTool.OutputImage;
                    //OriginConstImage = (CogImage8Grey)OriginImageFileTool.OutputImage;
                    OriginConstImage = new CogImage8Grey((CogImage8Grey)OriginImageFileTool.OutputImage);

                    SetDisplayImage(OriginImage);
                } 
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionWindow - LoadCogImage Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                MessageBox.Show(new Form { TopMost = true }, "Could not open image file.");
            }

            return _ImageFileName;
        }

        private bool SaveCogImage(string _SaveType, string _SaveDirectory = null)
        {
            bool _Result = false;

            if (_SaveDirectory == null) _SaveDirectory = @"D:\VisionInspectionData\" + FormName;

            if (_SaveType == "A" && ImageAutoSaveMode == eSaveMode.ONLY_NG)
            {
                if(SendResParam.IsGood == false) kpCogDisplayMain.SaveDisplayImage(_SaveDirectory);
            }
            else kpCogDisplayMain.SaveDisplayImage(_SaveDirectory, OriginConstImage);

            return _Result;
        }

        private void ManualInspectionImageLoad(string _ImageFileFullPath)
        {
            try
            {
                OriginImageFileTool.Operator.Open(_ImageFileFullPath, CogImageFileModeConstants.Read);
                OriginImageFileTool.Run();
                OriginImage = (CogImage8Grey)OriginImageFileTool.OutputImage;
                OriginConstImage = new CogImage8Grey((CogImage8Grey)OriginImageFileTool.OutputImage);

                SetDisplayImage(OriginImage);

                kpCogDisplayMain.SetDisplayZoom(DisplayZoomValue);
                kpCogDisplayMain.SetDisplayPanX(DisplayPanXValue);
                kpCogDisplayMain.SetDisplayPanY(DisplayPanYValue);
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "InspectionWindow - Manual Inspection Image Load Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                MessageBox.Show(new Form { TopMost = true }, "Could not open image file.");
            }
        }
        #endregion Image Load & Save

        #region Call Teaching  Window & Parameter Set
        private void Teaching()
        {
            TeachWnd = new TeachingWindow();
            TeachWnd.Initialize(ID, ProjectType, ProjectItem);
            TeachWnd.SetParameters(InspParam, MapDataParam);
            TeachWnd.SetResultData(AreaResultParamList, AlgoResultParamList);
            TeachWnd.SetTeachingImage(OriginImage, OriginImage.Width, OriginImage.Height);
            TeachWnd.ShowDialog();
            
            if (DialogResult.OK == TeachWnd.DialogResult)
            {
                //Teaching 한 Recipe Update
                SetInspectionParameter(TeachWnd.GetInspectionParameter());
                InspectionWindowEvent(eIWCMD.TEACH_OK, TeachWnd.GetInspectionParameter());
            }
            TeachWnd.DeInitialize();
            TeachWnd.Dispose();
            GC.Collect();
        }
        #endregion Call Teaching  Window & Parameter Set

        #region Inspection Process
        private void ManualInspection()
        {
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM{0} Manual Inspection Run", ID + 1), CLogManager.LOG_LEVEL.LOW);
            CParameterManager.SystemMode = eSysMode.ONESHOT_MODE;
            ContinuesGrabStop();
            Inspection();
        }

        public void GrabAndInspection()
        {
            InspectionWindowEvent(eIWCMD.LIGHT_CONTROL, true, ID);
            Thread.Sleep(50);

            CameraManager.CameraGrab();
            InspectionWindowEvent(eIWCMD.LIGHT_CONTROL, false, ID);
            IsThreadInspectionProcessTrigger = true;
            
        }

        public void ContinuesGrabStop()
        {
            CameraManager.CamLive(false);
            labelStatus.Text = "";
            Thread.Sleep(100);
        }

        private bool Inspection()
        {
            bool _Result = false;

            if (OriginImage.Height == 0) { _Result = false; return _Result; }

            IsInspectionComplete = false;

            do
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - Inspection Start", ID + 1), CLogManager.LOG_LEVEL.LOW);
                if (false == InspectionResultClear()) break;
                if (false == InspectionProcess()) break;
                if (false == InpsectionResultAnalysis()) break;
                if (false == InspectionDataSet()) break; //send랑 순서바꾼거 확인해보기
                if (false == InspectionDataSend()) break;
                if (false == InspectionResultDsiplay()) break;
                if (false == InspectionComplete(true)) break;
				IsThreadImageSaveTrigger = true;

                //if (false == InspectionComplete(false)) break;    //Auto Toggle로 변경
                _Result = true;
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - Inspection End", ID + 1), CLogManager.LOG_LEVEL.LOW);
            } while (false);

            GC.Collect();

            //CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - Inspection Result : {1}", ID, ));
            return _Result;
        }

        private bool InspectionResultClear()
        {
            bool _Result = true;
            AreaAlgoCount = 0;
            AreaResultParamList.Clear();
            AlgoResultParamList.Clear();
            AnyReferenceX = 0;
            AnyReferenceY = 0;
            IsCrossLine = false;
            DisplayClear(true, true);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - Inspection Resut Clear", ID + 1), CLogManager.LOG_LEVEL.LOW);
            return _Result;
        }

        private bool InspectionProcess()
        {
            bool _Result = true;

            System.Diagnostics.Stopwatch _ProcessWatch = new System.Diagnostics.Stopwatch();
            _ProcessWatch.Reset(); _ProcessWatch.Start();
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - InspectionProcess Start", ID + 1), CLogManager.LOG_LEVEL.LOW);

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                if (iLoopCount > 0 && InspParam.InspAreaParam[iLoopCount - 1].Enable == true)
                {
                    for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount - 1].InspAlgoParam.Count; ++jLoopCount)
                    {
                        if (InspParam.InspAreaParam[iLoopCount - 1].InspAlgoParam[jLoopCount].AlgoEnable == true)
                            AreaAlgoCount++;
                    }   
                }

                //Area 단위로 검사. Return은 Area 단위별 결과
                AreaStepInspection(InspParam.InspAreaParam[iLoopCount]);
            }

            IsInspectionComplete = true;
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} - InspectionProcess End", ID + 1), CLogManager.LOG_LEVEL.LOW);

            _ProcessWatch.Stop();
            string _ProcessTime = String.Format("ISM {0} - InspectionProcess Time : {1} ms", ID + 1, _ProcessWatch.Elapsed.TotalSeconds.ToString());
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, _ProcessTime, CLogManager.LOG_LEVEL.LOW);

            return _Result;
        }

        private void AreaStepInspection(InspectionAreaParameter _InspAreaParam)
        {
            if (false == _InspAreaParam.Enable) return; //검사 Enable 확인
            bool _InspectionResult = true;

            
            for (int iLoopCount = 0; iLoopCount < _InspAreaParam.InspAlgoParam.Count; ++iLoopCount)
            {
                int _BenchMark = _InspAreaParam.AreaBenchMark - 1;
                AreaBenchMarkOffsetX = AreaBenchMarkOffsetY = AreaBenchMarkOffsetT = 0;

                //한 Area 에서 Algorithm NG가 발생했을 시 Algorithm Pass를 하면 비어있는 Result를 채워 준다
                if (false == _InspectionResult && true == InspectionPassFlag)
                {
                    AlgoResultParameter _AlgoResultParam = new AlgoResultParameter();
                    AlgoResultParamList.Add(_AlgoResultParam);
                    continue;
                }

                //Area 단위로 Benchmark가 있는 경우 Offset 값 계산. (각 Area의 첫번째 알고리즘의 Offset 값이 적용 됨)
                if (_InspAreaParam.AreaBenchMark > 0 && AreaResultParamList.Count > _BenchMark)
                {
                    AreaBenchMarkOffsetX = AreaResultParamList[_BenchMark].OffsetX;
                    AreaBenchMarkOffsetY = AreaResultParamList[_BenchMark].OffsetY;
                    AreaBenchMarkOffsetT = AreaResultParamList[_BenchMark].OffsetT;
                }

                //Algorithm 단위로 검사. Return은 Algorithm 단위의 결과
                //double _StartX = _InspAreaParam.AreaRegionCenterX - (_InspAreaParam.AreaRegionWidth / 2) + AreaBenchMarkOffsetX;
                //double _StartY = _InspAreaParam.AreaRegionCenterY - (_InspAreaParam.AreaRegionHeight / 2) + AreaBenchMarkOffsetY;
                //double _Width = _InspAreaParam.AreaRegionWidth;
                //double _Height = _InspAreaParam.AreaRegionHeight;
                //double _Theta = AreaBenchMarkOffsetT;

                //_InspectionResult = AlgorithmStepInspection(_InspAreaParam.InspAlgoParam[iLoopCount], _StartX, _StartY, _Width, _Height, _Theta, _InspAreaParam.NgAreaNumber);
                _InspectionResult = AlgorithmStepInspection(_InspAreaParam.InspAlgoParam[iLoopCount], _InspAreaParam.NgAreaNumber);

                //각 Area의 첫번째 알고리즘의 Offset 값이 Area 검사 Offset에 적용 됨
                if (iLoopCount == 0)
                {
                    int _Index = AlgoResultParamList.Count - 1;

                    AreaResultParameter _AreaResParam = new AreaResultParameter();
                    _AreaResParam.OffsetX = AlgoResultParamList[_Index].OffsetX;
                    _AreaResParam.OffsetY = AlgoResultParamList[_Index].OffsetY;
                    _AreaResParam.OffsetT = AlgoResultParamList[_Index].OffsetT;
                    AreaResultParamList.Add(_AreaResParam);
                }
            }
         }

        #region Algorithm 별 Inspection Step
        //private bool AlgorithmStepInspection(InspectionAlgorithmParameter _InspAlgoParam, double _AreaStartX, double _AreaStartY, double _AreaWidth, double _AreaHeight, double _Theta, int _NgAreaNumber)
        private bool AlgorithmStepInspection(InspectionAlgorithmParameter _InspAlgoParam, int _NgAreaNumber)
        {
            bool _Result = true;

            if (false == _InspAlgoParam.AlgoEnable) return true;
            BenchMarkOffsetX = 0;
            BenchMarkOffsetY = 0;
            BenchMarkOffsetT = 0;

            //double _BenchMarkOffsetX = 0, _BenchMarkOffsetY = 0;

            #region Buffer Area Calculate
            int _BenchMark = AreaAlgoCount + (_InspAlgoParam.AlgoBenchMark - 1);
            if (_InspAlgoParam.AlgoBenchMark > 0 && AlgoResultParamList.Count > _BenchMark)
            {
                if (AlgoResultParamList[_BenchMark].ResultParam != null)
                {
                    BenchMarkOffsetX = AlgoResultParamList[_BenchMark].OffsetX - AreaBenchMarkOffsetX;
                    BenchMarkOffsetY = AlgoResultParamList[_BenchMark].OffsetY - AreaBenchMarkOffsetY;
                    //BenchMarkOffsetT = AlgoResultParamList[_BenchMark].OffsetT;
                }
            }

            double _CenterX = _InspAlgoParam.AlgoRegionCenterX +  BenchMarkOffsetX;
            double _CenterY = _InspAlgoParam .AlgoRegionCenterY + BenchMarkOffsetY;
            double _Width = _InspAlgoParam.AlgoRegionWidth;
            double _Height = _InspAlgoParam.AlgoRegionHeight;
            double _Theta = AreaBenchMarkOffsetT;

            CogRectangle _InspRegion = new CogRectangle();
            _InspRegion.SetCenterWidthHeight(_CenterX, _CenterY, _Width, _Height);

            CogRectangleAffine _InspRegionAffine = new CogRectangleAffine();
            _InspRegionAffine.SetCenterLengthsRotationSkew(_CenterX, _CenterY, _Width, _Height, _Theta, 0);
            #endregion Buffer Area Calculate

            eAlgoType _AlgoType = (eAlgoType)_InspAlgoParam.AlgoType;
            if (eAlgoType.C_PATTERN == _AlgoType)           _Result = CogPatternAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);
            else if (eAlgoType.C_BLOB == _AlgoType)         _Result = CogBlobAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);
            else if (eAlgoType.C_BLOB_REFER == _AlgoType)   _Result = CogBlobReferenceAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegionAffine, _NgAreaNumber);
            else if (eAlgoType.C_ELLIPSE == _AlgoType)      _Result = CogEllipseFindAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);
            else if (eAlgoType.C_ID == _AlgoType)           _Result = CogBarCodeIDAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);
            else if (eAlgoType.C_LINE_FIND == _AlgoType)    _Result = CogLineFindAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);
            else if (eAlgoType.C_MULTI_PATTERN == _AlgoType)_Result = CogMultiPatternAlgorithmStep(_InspAlgoParam.Algorithm, _InspRegion, _NgAreaNumber);

            return _Result;
        }

        private bool CogPatternAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            var _CogPatternAlgo = _Algorithm as CogPatternAlgo;
            CogPatternResult _CogPatternResult = new CogPatternResult();

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "PatternMatching Algorithm Start", CLogManager.LOG_LEVEL.MID);
            bool _Result = InspPatternProc.Run(OriginImage, _InspRegion, _CogPatternAlgo, ref _CogPatternResult);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "PatternMatching Algorithm End", CLogManager.LOG_LEVEL.MID);

            if (_CogPatternResult.OriginPointX?.Length > 0) AnyReferenceX = _CogPatternResult.OriginPointX[0];
            if (_CogPatternResult.OriginPointY?.Length > 0) AnyReferenceY = _CogPatternResult.OriginPointY[0];

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_PATTERN, _CogPatternResult);
            _AlgoResultParam.OffsetX = 0;
            _AlgoResultParam.OffsetY = 0;
            if (_CogPatternAlgo.ReferenceInfoList.Count > 0 && _CogPatternResult.CenterX.Length > 0 && _CogPatternResult.CenterY.Length > 0)
            {
                double _OriginX = _CogPatternAlgo.ReferenceInfoList[0].CenterX - _CogPatternAlgo.ReferenceInfoList[0].OriginPointOffsetX;
                double _OriginY = _CogPatternAlgo.ReferenceInfoList[0].CenterY - _CogPatternAlgo.ReferenceInfoList[0].OriginPointOffsetY;

                _AlgoResultParam.OffsetX = _OriginX - _CogPatternResult.CenterX[0];
                _AlgoResultParam.OffsetY = _OriginY - _CogPatternResult.CenterY[0];
            }
            AlgoResultParamList.Add(_AlgoResultParam);

            return true;
        }

        private bool CogMultiPatternAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            var _CogMultiPatternAlgo = _Algorithm as CogMultiPatternAlgo;
            CogMultiPatternResult _CogMultiPatternResult = new CogMultiPatternResult();

            bool _Result = InspMultiPatternProc.Run(OriginImage, _InspRegion, _CogMultiPatternAlgo, ref _CogMultiPatternResult);

            if (_CogMultiPatternResult.OriginPointX?.Length > 0) AnyReferenceX = _CogMultiPatternResult.OriginPointX[0];
            if (_CogMultiPatternResult.OriginPointY?.Length > 0) AnyReferenceY = _CogMultiPatternResult.OriginPointY[0];

            double _OriginX = _CogMultiPatternAlgo.ReferenceInfoList[0].CenterX - _CogMultiPatternAlgo.ReferenceInfoList[0].OriginPointOffsetX;
            double _OriginY = _CogMultiPatternAlgo.ReferenceInfoList[0].CenterY - _CogMultiPatternAlgo.ReferenceInfoList[0].OriginPointOffsetY;

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_MULTI_PATTERN, _CogMultiPatternResult);
            _AlgoResultParam.OffsetX = 0;
            _AlgoResultParam.OffsetY = 0;
            _AlgoResultParam.OffsetT = 0.0;
            if (_CogMultiPatternAlgo.ReferenceInfoList.Count > 0 && _CogMultiPatternResult.CenterX.Length > 0 && _CogMultiPatternResult.CenterY.Length > 0)
            {
                _AlgoResultParam.OffsetX = _OriginX - _CogMultiPatternResult.CenterX[0];
                _AlgoResultParam.OffsetY = _OriginY - _CogMultiPatternResult.CenterY[0];
                _AlgoResultParam.OffsetT = _CogMultiPatternResult.TwoPointAngle;
            }
            AlgoResultParamList.Add(_AlgoResultParam);

            return true;
        }

        private bool CogAutoPatternAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            var _CogAutoPatternAlgo = _Algorithm as CogAutoPatternAlgo;
            CogAutoPatternResult _CogAutoPatternResult = new CogAutoPatternResult();

            bool _Result = InspAutoPatternProc.Run(OriginImage, _InspRegion, _CogAutoPatternAlgo, ref _CogAutoPatternResult);

            if (_CogAutoPatternResult.OriginPointX > 0) AnyReferenceX = _CogAutoPatternResult.OriginPointX;
            if (_CogAutoPatternResult.OriginPointY > 0) AnyReferenceY = _CogAutoPatternResult.OriginPointY;

            double _OriginX = _CogAutoPatternAlgo.ReferenceInfoList[0].CenterX - _CogAutoPatternAlgo.ReferenceInfoList[0].OriginPointOffsetX;
            double _OriginY = _CogAutoPatternAlgo.ReferenceInfoList[0].CenterY - _CogAutoPatternAlgo.ReferenceInfoList[0].OriginPointOffsetY;

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_AUTO_PATTERN, _CogAutoPatternResult);
            _AlgoResultParam.OffsetX = 0;
            _AlgoResultParam.OffsetY = 0;
            if (_CogAutoPatternAlgo.ReferenceInfoList.Count > 0 && _CogAutoPatternResult.Score > 0)
            {
                _AlgoResultParam.OffsetX = _OriginX - _CogAutoPatternResult.CenterX;
                _AlgoResultParam.OffsetY = _OriginY - _CogAutoPatternResult.CenterY;
            }
            AlgoResultParamList.Add(_AlgoResultParam);

            return true;
        }

        //private bool CogBlobReferenceAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        private bool CogBlobReferenceAlgorithmStep(Object _Algorithm, CogRectangleAffine _InspRegion, int _NgAreaNumber)
        {
            //CogBlobReferenceAlgo    _CogBlobReferAlgo = (CogBlobReferenceAlgo)_Algorithm;
            var _CogBlobReferAlgo = _Algorithm as CogBlobReferenceAlgo;
            CogBlobReferenceResult  _CogBlobReferResult = new CogBlobReferenceResult();

            _CogBlobReferAlgo.ResolutionX = ResolutionX;
            _CogBlobReferAlgo.ResolutionY = ResolutionY;

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "BlobReference Algorithm Start", CLogManager.LOG_LEVEL.MID);
            bool _Result = InspBlobReferProc.Run(OriginImage, _InspRegion, _CogBlobReferAlgo, ref _CogBlobReferResult, _NgAreaNumber);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "BlobReference Algorithm Start End", CLogManager.LOG_LEVEL.MID);

            if (_CogBlobReferResult.OriginX?.Length > 0) AnyReferenceX = _CogBlobReferResult.OriginX[0];
            if (_CogBlobReferResult.OriginY?.Length > 0) AnyReferenceY = _CogBlobReferResult.OriginY[0];

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_BLOB_REFER, _CogBlobReferResult);
            _AlgoResultParam.OffsetX = _CogBlobReferAlgo.OriginX - _CogBlobReferResult.OriginX[0];
            _AlgoResultParam.OffsetY = _CogBlobReferAlgo.OriginY - _CogBlobReferResult.OriginY[0];
            _AlgoResultParam.NgAreaNumber = _NgAreaNumber;
            AlgoResultParamList.Add(_AlgoResultParam);

            return _CogBlobReferResult.IsGood;
        }

        private bool CogBlobAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            return true;
        }

        private bool CogEllipseFindAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            CogEllipseAlgo _CogEllipseAlgo = _Algorithm as CogEllipseAlgo;
            CogEllipseResult _CogEllipseResult = new CogEllipseResult();

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Ellipse Find Algorithm Start", CLogManager.LOG_LEVEL.MID);
            bool _Result = InspEllipseProc.Run(OriginImage, _InspRegion, _CogEllipseAlgo, ref _CogEllipseResult, BenchMarkOffsetX, BenchMarkOffsetY);

            _CogEllipseResult.CenterXReal = (_CogEllipseResult.CenterX - (OriginImage.Width / 2)) * ResolutionX;
            _CogEllipseResult.CenterYReal = (_CogEllipseResult.CenterY - (OriginImage.Height / 2)) * ResolutionY;
            _CogEllipseResult.OriginXReal = (_CogEllipseResult.OriginX - (OriginImage.Width / 2)) * ResolutionX;
            _CogEllipseResult.OriginYReal = (_CogEllipseResult.OriginY - (OriginImage.Height / 2)) * ResolutionY;
            _CogEllipseResult.RadiusXReal = _CogEllipseResult.RadiusX * ResolutionX;
            _CogEllipseResult.RadiusYReal = _CogEllipseResult.RadiusY * ResolutionX;

            if (_CogEllipseAlgo.CaliperNumber - 2 >= _CogEllipseResult.PointFoundCount)
                _CogEllipseResult.IsGood = false;

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - Real Position X : {0}, Y : {1}", _CogEllipseResult.CenterXReal.ToString("F2"), _CogEllipseResult.CenterYReal.ToString("F2")), CLogManager.LOG_LEVEL.MID);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format(" - Real Radius X : {0}, Y : {1}", _CogEllipseResult.RadiusXReal.ToString("F2"), _CogEllipseResult.RadiusYReal.ToString("F2")), CLogManager.LOG_LEVEL.MID);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "Ellipse Algorithm End", CLogManager.LOG_LEVEL.MID);

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_ELLIPSE, _CogEllipseResult);
            _AlgoResultParam.OffsetX = _CogEllipseAlgo.OriginX - _CogEllipseResult.CenterX;
            _AlgoResultParam.OffsetY = _CogEllipseAlgo.OriginY - _CogEllipseResult.CenterY;
            _AlgoResultParam.NgAreaNumber = _NgAreaNumber;
            AlgoResultParamList.Add(_AlgoResultParam);

            return _CogEllipseResult.IsGood;
        }

        private bool CogBarCodeIDAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            CogBarCodeIDAlgo    _CogBarCodeIDAlgo = _Algorithm as CogBarCodeIDAlgo;
            CogBarCodeIDResult  _CogBarCodeIDResult = new CogBarCodeIDResult();

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "CodeRead Algorithm Start", CLogManager.LOG_LEVEL.MID);
            bool _Result = InspIDProc.Run(OriginImage, _InspRegion, _CogBarCodeIDAlgo, ref _CogBarCodeIDResult);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "CodeRead Algorithm End", CLogManager.LOG_LEVEL.MID);

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_ID, _CogBarCodeIDResult);
            AlgoResultParamList.Add(_AlgoResultParam);

            return _CogBarCodeIDResult.IsGood;
        }

        private bool CogLineFindAlgorithmStep(Object _Algorithm, CogRectangle _InspRegion, int _NgAreaNumber)
        {
            CogLineFindAlgo     _CogLineFindAlgo = _Algorithm as CogLineFindAlgo;
            CogLineFindResult   _CogLineFindResult = new CogLineFindResult();

            CogImage8Grey _DestImage = new CogImage8Grey();
            bool _Result = InspLineFindProc.Run(OriginImage, ref _DestImage, _InspRegion, _CogLineFindAlgo, ref _CogLineFindResult);

            if (_CogLineFindResult.IsGood == true && _CogLineFindAlgo.UseAlignment)
            {
                OriginImage = _DestImage;
                kpCogDisplayMain.SetDisplayImage(_DestImage);
            }

            AlgoResultParameter _AlgoResultParam = new AlgoResultParameter(eAlgoType.C_LINE_FIND, _CogLineFindResult);
            AlgoResultParamList.Add(_AlgoResultParam);              

            return _CogLineFindResult.IsGood;
        }
        #endregion Algorithm 별 Inspection Step

        #region Algorithm 별 Display
        private bool InspectionResultDsiplay()
        {
            bool _IsLastGood = true, _IsGood = false;

            if (AlgoResultParamList.Count == 0) _IsLastGood = false;
            for (int iLoopCount = 0; iLoopCount < AlgoResultParamList.Count; ++iLoopCount)
            {
                eAlgoType _AlgoType = AlgoResultParamList[iLoopCount].ResultAlgoType;
                if (eAlgoType.C_PATTERN == _AlgoType)           _IsGood = DisplayResultPatternMatching(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
                else if (eAlgoType.C_BLOB_REFER == _AlgoType)   _IsGood = DisplayResultBlobReference(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
                else if (eAlgoType.C_BLOB == _AlgoType)         _IsGood = DisplayResultBlob(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
                else if (eAlgoType.C_ELLIPSE == _AlgoType)      _IsGood = DisplayResultEllipseFind(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
                else if (eAlgoType.C_ID == _AlgoType)           _IsGood = DisplayResultBarCodeIDFind(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
                else if (eAlgoType.C_LINE_FIND == _AlgoType)    _IsGood = DisplayResultLineFind(AlgoResultParamList[iLoopCount].ResultParam, iLoopCount);
            }

            //LJH 2018.09.28 SendResultParam에서 결과값 가져오기
            _IsLastGood = SendResParam.IsGood;

            DisplayLastResultMessage(_IsLastGood);

            return true;
        }

        public void DisplayClear(bool _StaticClear, bool _InteractiveClear)
        {
            //DisplayClear(_StaticClear, _InteractiveClear);
            kpCogDisplayMain.ClearDisplay(_StaticClear, _InteractiveClear);
        }

        private bool DisplayResultPatternMatching(Object _ResultParam, int _Index)
        {
            bool _IsGood = true;
            var _PatternResult = _ResultParam as CogPatternResult;

            CogRectangle _PatternRect = new CogRectangle();
            CogRectangleAffine _PatternAffine = new CogRectangleAffine();
            CogPointMarker _Point = new CogPointMarker();
            for (int iLoopCount = 0; iLoopCount < _PatternResult.FindCount; ++iLoopCount)
            {
                if (false == IsResultDisplay) continue;
                _PatternRect.SetCenterWidthHeight(_PatternResult.CenterX[iLoopCount], _PatternResult.CenterY[iLoopCount], _PatternResult.Width[iLoopCount], _PatternResult.Height[iLoopCount]);
                _PatternAffine.SetCenterLengthsRotationSkew(_PatternResult.CenterX[iLoopCount], _PatternResult.CenterY[iLoopCount], _PatternResult.Width[iLoopCount], _PatternResult.Height[iLoopCount], _PatternResult.Angle[iLoopCount], 0);
                _Point.SetCenterRotationSize(_PatternResult.OriginPointX[iLoopCount], _PatternResult.OriginPointY[iLoopCount], 0, 2);
                //ResultDisplay(_PatternAffine, _Point, string.Format("Pattern_{0}_{1}", _Index, iLoopCount), _PatternResult.IsGoods[iLoopCount]);
                ResultDisplay(_PatternAffine, _Point, string.Format("Pattern_{0}_{1}", _Index, iLoopCount), _PatternResult.IsGoods[iLoopCount]);

                string _MatchingName = string.Format($"Rate{_Index} = {_PatternResult.Score[iLoopCount]:F2}, X = {_PatternResult.OriginPointX[iLoopCount]:F2}, Y = {_PatternResult.OriginPointY[iLoopCount]:F2}");
                ResultDisplayMessage(_PatternResult.OriginPointX[iLoopCount], _PatternResult.OriginPointY[iLoopCount] + _PatternResult.Height[iLoopCount] / 2 + 30, _MatchingName, _PatternResult.IsGoods[iLoopCount], CogGraphicLabelAlignmentConstants.BaselineCenter);
            }

            if (_PatternResult.FindCount <= 0)
            {
                _PatternAffine.SetCenterLengthsRotationSkew(_PatternResult.CenterX[0], _PatternResult.CenterY[0], _PatternResult.Width[0], _PatternResult.Height[0], _PatternResult.Angle[0], 0);
                _Point.SetCenterRotationSize(_PatternResult.OriginPointX[0], _PatternResult.OriginPointY[0], 0, 2);
                ResultDisplay(_PatternAffine, _Point, string.Format("Pattern_{0}_0", _Index), _PatternResult.IsGood);
                _IsGood = false;
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultPatternMatching Complete", CLogManager.LOG_LEVEL.MID);

            return _IsGood;
        }

        private bool DisplayResultBlobReference(Object _ResultParam, int _Index)
        {
            bool _IsGood = false;
            var _BlobReferResult = _ResultParam as CogBlobReferenceResult;

            CogRectangle _Region = new CogRectangle();
            CogPointMarker _Point = new CogPointMarker();
            if (_BlobReferResult.BlobCount > 0)
            {
                for (int iLoopCount = 0; iLoopCount < _BlobReferResult.BlobCount; ++iLoopCount)
                {
                    _IsGood = _BlobReferResult.IsGood;

                    string _DrawName = String.Format("BlobReference_{0}_{1}", _Index, iLoopCount);
                    _Region.SetCenterWidthHeight(_BlobReferResult.BlobCenterX[iLoopCount], _BlobReferResult.BlobCenterY[iLoopCount], _BlobReferResult.Width[iLoopCount], _BlobReferResult.Height[iLoopCount]);
                    _Point.SetCenterRotationSize(_BlobReferResult.OriginX[iLoopCount], _BlobReferResult.OriginY[iLoopCount], 0, 10);
                    ResultDisplay(_Region, _Point, _DrawName, _IsGood);

                    double _WidthSize = _BlobReferResult.Width[iLoopCount] * ResolutionX;
                    double _HeightSize = _BlobReferResult.Height[iLoopCount] * ResolutionY;
                    string _RstName = String.Format("BlobResult_{0}_{1}", _Index, iLoopCount);
                    string _Message = String.Format("W : {0:F3}, H : {1:F3}", _WidthSize, _HeightSize);
                    ResultDisplayMessage(_BlobReferResult.BlobMinX[iLoopCount], _BlobReferResult.BlobMaxY[iLoopCount] + 4, _Message, _IsGood);
                }
            }

            else
            {
                _IsGood = _BlobReferResult.IsGood;

                string _DrawName = String.Format("BlobReference_{0}_{1}", _Index, 0);
                _Region.SetCenterWidthHeight(_BlobReferResult.BlobCenterX[0], _BlobReferResult.BlobCenterY[0], _BlobReferResult.Width[0], _BlobReferResult.Height[0]);
                _Point.SetCenterRotationSize(_BlobReferResult.OriginX[0], _BlobReferResult.OriginY[0], 0, 10);
                ResultDisplay(_Region, _Point, _DrawName, _IsGood);
                
                string _Message = String.Format("BlobReference_{0} NG", _Index);
                ResultDisplayMessage(_BlobReferResult.BlobMinX[0], _BlobReferResult.BlobMinY[0] + 4, _Message, _IsGood);
            }

            if (ProjectType == eProjectType.NAVIEN) DisplayAlgoNumber(_BlobReferResult.BlobCenterX[0], _BlobReferResult.BlobCenterY[0] - (_BlobReferResult.Height[0] / 2) - 200, _Index + 1);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultBlobReference Complete", CLogManager.LOG_LEVEL.MID);

            return _IsGood;
        }

        private bool DisplayResultBlob(Object _ResultParam, int _Index)
        {
            bool _IsGood = true;

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultBlob Complete", CLogManager.LOG_LEVEL.MID);

            return _IsGood;
        }

        private bool DisplayResultEllipseFind(Object _ResultParam, int _Index)
        {
            CogEllipseResult _CogEllipseResult = _ResultParam as CogEllipseResult;

            CogEllipse _Ellipse = new CogEllipse();
            CogPointMarker _CirclePoint = new CogPointMarker();

            if (_CogEllipseResult != null && _CogEllipseResult.IsGood)
            {
                if (_CogEllipseResult.RadiusX > 0)
                {
                    _Ellipse.SetCenterXYRadiusXYRotation(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY, _CogEllipseResult.RadiusX, _CogEllipseResult.RadiusY, _CogEllipseResult.Rotation);
                    //_CirclePoint.SetCenterRotationSize(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY, _CogEllipseResult.Rotation, 1);
                    ResultDisplay(_Ellipse, _CirclePoint, "Ellipse", _CogEllipseResult.IsGood);

                    //LDH, 반지름 결과값 숨기기
                    //string _CenterPointName = string.Format("XR = {0:F2}mm, YR = {1:F2}mm", _CogEllipseResult.RadiusXReal, _CogEllipseResult.RadiusYReal);
                    //ResultDisplayMessage(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY + 150, _CenterPointName, _CogEllipseResult.IsGood, CogGraphicLabelAlignmentConstants.BaselineCenter);

                    //LDH, Caliper Point 숨기기
                    //if (_CogEllipseResult.PointStatusInfo != null)
                    //{
                    //    for (int iLoopCount = 0; iLoopCount < _CogEllipseResult.PointStatusInfo.Length; ++iLoopCount)
                    //    {
                    //        if (_CogEllipseResult.PointPosXInfo[iLoopCount] == 0 && _CogEllipseResult.PointPosYInfo[iLoopCount] == 0) continue;

                    //        CogPointMarker _Point = new CogPointMarker();
                    //        _Point.SetCenterRotationSize(_CogEllipseResult.PointPosXInfo[iLoopCount], _CogEllipseResult.PointPosYInfo[iLoopCount], 0, 1);

                    //        string _PointName = string.Format("Point{0}", iLoopCount);
                    //        if (true == _CogEllipseResult.PointStatusInfo[iLoopCount]) ResultDisplay(_Point, _PointName, CogColorConstants.Green);
                    //        else ResultDisplay(_Point, _PointName, CogColorConstants.Red);
                    //    }
                    //}
                }
            }

            else
            {
                CogRectangle _EllipseRect = new CogRectangle();

                _EllipseRect.SetCenterWidthHeight(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY, _CogEllipseResult.RadiusX * 2, _CogEllipseResult.RadiusY * 2);
                _CirclePoint.SetCenterRotationSize(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY, 0, 1);
                ResultDisplay(_EllipseRect, _CirclePoint, "Ellipse", _CogEllipseResult.IsGood, false);
            }

            if (ProjectType == eProjectType.NAVIEN) DisplayAlgoNumber(_CogEllipseResult.CenterX, _CogEllipseResult.CenterY - _CogEllipseResult.RadiusY - 200, _Index + 1);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultEllipse Complete", CLogManager.LOG_LEVEL.MID);

            return _CogEllipseResult.IsGood;
        }

        private bool DisplayResultBarCodeIDFind(Object _ResultParam, int _Index)
        {
            CogBarCodeIDResult _CogBarCodeIDResult = _ResultParam as CogBarCodeIDResult;

            CogPolygon _Polygon = new CogPolygon();
            
            if(_CogBarCodeIDResult != null)
            {
                for (int iLoopCount = 0; iLoopCount < _CogBarCodeIDResult.IDCount; iLoopCount++)
                {
                    _Polygon.SetVertices(_CogBarCodeIDResult.IDPolygon[iLoopCount].GetVertices());
                    ResultDisplay(_Polygon, "BarCodeID" + iLoopCount, _CogBarCodeIDResult.IsGood);

                    string _ResultIDName = string.Format("ID = {0}", _CogBarCodeIDResult.IDResult);
                    ResultDisplayMessage(_CogBarCodeIDResult.IDCenterX[iLoopCount], _CogBarCodeIDResult.IDCenterY[iLoopCount] + 30, _ResultIDName, true, CogGraphicLabelAlignmentConstants.BaselineCenter);
                    ResultDisplayMessage(50, 125, _ResultIDName + ".", true, CogGraphicLabelAlignmentConstants.BaselineLeft);
                }

            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultBarcodeIDFind Complete", CLogManager.LOG_LEVEL.MID);

            return _CogBarCodeIDResult.IsGood;
        }

        private bool DisplayResultLineFind(Object _ResultParam, int _Index)
        {
            CogLineFindResult _CogLineFindResult = _ResultParam as CogLineFindResult;

            CogLineSegment _CogLine = new CogLineSegment();
            if (_CogLineFindResult.Length == 0 || _CogLineFindResult.Rotation == 0) return false;
            _CogLine.SetStartLengthRotation(_CogLineFindResult.StartX, _CogLineFindResult.StartY, _CogLineFindResult.Length, _CogLineFindResult.Rotation);

            if(_CogLineFindResult.IsGood) ResultDisplay(_CogLine, "LineFind", CogColorConstants.Orange);

            if (_CogLineFindResult.IntersectionX != 0 && _CogLineFindResult.IntersectionY != 0)
            {
                CogPointMarker _CogPoint = new CogPointMarker();
                _CogPoint.SetCenterRotationSize(_CogLineFindResult.IntersectionX, _CogLineFindResult.IntersectionY, 0, 10);
                ResultDisplay(_CogPoint, "IntersectionPoint", CogColorConstants.Green, 500);
            }

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - DisplayResultLineFind Complete", CLogManager.LOG_LEVEL.MID);

            return _CogLineFindResult.IsGood;
        }
        #endregion Algorithm 별 Display

        private bool InspectionComplete(bool _Flag)
        {
            bool _Result = true;

            if (false == _Flag) Thread.Sleep(500);
            var _InspectionWindowEvent = InspectionWindowEvent;
            _InspectionWindowEvent?.Invoke(eIWCMD.INSP_COMPLETE, _Flag, ID);

            return _Result;
        }

        #region Display Result function
        public void ResultDisplay(CogRectangle _Region, CogPointMarker _Point, string _Name, bool _IsGood, bool _ClearFlag = true)
        {
            if (_ClearFlag)
            {
                if (true == _IsGood) kpCogDisplayMain.DrawStaticShape(_Region, _Name + "_Rect", CogColorConstants.Green);
                else kpCogDisplayMain.DrawStaticShape(_Region, _Name + "_Rect", CogColorConstants.Red);

                if (true == _IsGood) kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Green);
                else kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Red);
            }
            else
            {
                if (true == _IsGood) kpCogDisplayMain.DrawStaticShapeNotClear(_Region, _Name + "_Rect", CogColorConstants.Green);
                else kpCogDisplayMain.DrawStaticShapeNotClear(_Region, _Name + "_Rect", CogColorConstants.Red);

                if (true == _IsGood) kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Green, 2, false);
                else kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Red, 2, false);
            }
        }      

        public void ResultDisplay(CogCircle _Circle, CogPointMarker _Point, string _Name, bool _IsGood)
        {
            if (true == _IsGood)    kpCogDisplayMain.DrawStaticShape(_Circle, "Circle", CogColorConstants.Green, 3);
            else                    kpCogDisplayMain.DrawStaticShape(_Circle, "Circle", CogColorConstants.Red, 3);

            if (true == _IsGood)    kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Green);
            else                    kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Red);
        }

        public void ResultDisplay(CogEllipse _Ellipse, CogPointMarker _Point, string _Name, bool _IsGood)
        {
            if (true == _IsGood) kpCogDisplayMain.DrawStaticShape(_Ellipse, "Ellipse", CogColorConstants.Green, 3, false);
            else kpCogDisplayMain.DrawStaticShape(_Ellipse, "Ellipse", CogColorConstants.Red, 3);

            if (true == _IsGood) kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Green);
            else kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Red);
        }

        public void ResultDisplay(CogRectangleAffine _Region, CogPointMarker _Point, string _Name, bool _IsGood)
        {
            if (true == _IsGood)    kpCogDisplayMain.DrawStaticShape(_Region, _Name + "_Rect", CogColorConstants.Green);
            else                    kpCogDisplayMain.DrawStaticShape(_Region, _Name + "_Rect", CogColorConstants.Red);

            if (true == _IsGood)    kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Green);
            else                    kpCogDisplayMain.DrawStaticShape(_Point, _Name + "_PointOrigin", CogColorConstants.Red);
        }

        public void ResultDisplay(CogPolygon _Polygon, string _Name, bool _IsGood)
        {
            kpCogDisplayMain.DrawStaticShape(_Polygon, _Name + "_Polygon", CogColorConstants.Green);
        }

        public void ResultDisplay(CogLineSegment _Line, string _Name, CogColorConstants _Color)
        {
            kpCogDisplayMain.DrawStaticLine(_Line, _Name, _Color);
        }

        public void ResultDisplay(CogPointMarker _Point, string _Name, CogColorConstants _Color)
        {
            kpCogDisplayMain.DrawStaticShape(_Point, _Name, _Color);
        }

        public void ResultDisplay(CogPointMarker _Point, string _Name, CogColorConstants _Color, int _LineSize)
        {
            kpCogDisplayMain.DrawStaticShape(_Point, _Name, _Color, _LineSize);
        }

        public void ResultDisplayMessage(double _StartX, double _StartY, string _Message, bool _IsGood = true, CogGraphicLabelAlignmentConstants _Align = CogGraphicLabelAlignmentConstants.TopLeft)
        {
            if (true == _IsGood)    kpCogDisplayMain.DrawText(_Message, _StartX, _StartY, CogColorConstants.Green, 8, _Align);
            else                    kpCogDisplayMain.DrawText(_Message, _StartX, _StartY, CogColorConstants.Red, 8, _Align);
        }

        private void DisplayLastResultMessage(bool _IsGood)
        {
            if (_IsGood)    kpCogDisplayMain.DrawText("Result : Good", 50, 50, CogColorConstants.Green);
            else            kpCogDisplayMain.DrawText("Result : NG", 50, 50, CogColorConstants.Red);
        }

        private void DisplayAlgoNumber(double _StartX, double _StartY, int _AlgoNum)
        {
            kpCogDisplayMain.DrawText(_AlgoNum.ToString(), _StartX, _StartY, CogColorConstants.DarkRed, CogColorConstants.White, 12, CogGraphicLabelAlignmentConstants.TopCenter);
        }
        #endregion Display Result function

        #region Result Data Send
        private bool InpsectionResultAnalysis()
        {
            bool _Result = true;

            SendResParam = new SendResultParameter();

            //LDH, 2019.10.25, Analysis 통합
			//if (ProjectItem == eProjectItem.NONE)	 			    SendResParam = GetDefaultInspectionResultAnalysis();
            SendResParam = GetResultAnalysis();

            return _Result;
        }

        private bool InspectionDataSet()
        {
            bool _Result = true;

            var _InspectionWindowEvent = InspectionWindowEvent;
            _InspectionWindowEvent?.Invoke(eIWCMD.SET_RESULT, SendResParam);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} Last Result : {1}", SendResParam.ID + 1, SendResParam.IsGood.ToString()), CLogManager.LOG_LEVEL.LOW);
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - InspectionDataSet Complete", CLogManager.LOG_LEVEL.MID);

            return _Result;
        }

        public bool InspectionDataSend()
        {
            bool _Result = true;

            var _InspectionWindowEvent = InspectionWindowEvent;
            _InspectionWindowEvent?.Invoke(eIWCMD.SEND_DATA, SendResParam);

            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, "InspectionWindow - InspectionDataSend Complete", CLogManager.LOG_LEVEL.MID);

            return _Result;
        }
        #endregion Result Data Send
        #endregion Inspection Process

        #region Thread Funtion
        private void ThreadInspectionProcessFunction()
        {
            try
            {
                while (false == IsThreadInspectionProcessExit)
                {
                    if (true == IsThreadInspectionProcessTrigger)
                    {
                        IsInspectionComplete = false;
                        IsThreadInspectionProcessTrigger = false;
                        Inspection();
                    }
                    Thread.Sleep(10);
                }
            }

            catch(Exception ex)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, String.Format("ISM {0} - ThreadInspectionProcessFunction Exception", ID + 1), CLogManager.LOG_LEVEL.LOW);
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, String.Format("{0}", ex));
            }
        }

        private void ThreadLiveCheckFunction()
        {
            try
            {
                while (false == IsThreadLiveCheckExit)
                {
                    Thread.Sleep(50);
                    CheckInspectionProcessThreadAlive();
                }
            }

            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, String.Format("ISM {0} - ThreadLiveCheckFunction Exception", ID + 1), CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void ThreadImageSaveFunction()
        {
            try
            {
                while (false == IsThreadImageSaveExit)
                {
                    if (true == IsThreadImageSaveTrigger)
                    {
                        IsThreadImageSaveTrigger = false;

                        SaveCogImage("A");
                    }
                    Thread.Sleep(10);
                }
            }

            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, String.Format("ISM {0} - ThreadImageSaveFunction Exception", ID + 1), CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void CheckInspectionProcessThreadAlive()
        {
            try
            {
                if (ThreadLiveCheck == null) return;
                if (ThreadLiveCheck.IsAlive == false)
                {
                    ThreadInspectionProcess = null;
                    ThreadInspectionProcess = new Thread(ThreadInspectionProcessFunction);
                    IsThreadInspectionProcessTrigger = false;
                    ThreadInspectionProcess.Start();
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.WARN, String.Format("ISM {0} - ThreadInspectionProcess Re-Start!!", ID + 1), CLogManager.LOG_LEVEL.LOW);
                }
            }

            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, String.Format("ISM {0} - CheckInspectionProcessThreadAlive Exception", ID + 1), CLogManager.LOG_LEVEL.LOW);
            }
        }
        #endregion Thread Funtion
    }
}
