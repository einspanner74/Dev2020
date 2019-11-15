using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Globalization;

using InspectionSystemManager;
using ParameterManager;
using LogMessageManager;
using LoadingManager;
using DIOControlManager;
using LightManager;
using SerialManager;
using HistoryManager;
using CustomMsgBoxManager;

namespace KPVisionInspectionFramework
{
    public partial class MainForm : RibbonForm
    {
        private CParameterManager             ParamManager;
        private CInspectionSystemManager[]    InspSysManager;
        private CLogManager                   LogWnd;
        private MainResultBase                ResultBaseWnd;
		private CLightManager                 LightControlManager;
        private RecipeWindow                  RecipeWnd;
        private MainProcessBase               MainProcess;
        private CHistoryManager               HistoryManager;
        private FolderPathWindow              FolderPathWnd;
        private MainLogoWindow                MainLogoWnd;
        private NoticeWindow                  NoticeWnd;

        private string ProjectName;
        private bool IsIOBoardUsable;
        private bool IsEthernetUsable;
        private bool IsMitsuCommUsable;
        private int ISMModuleCount = 1;

        private Timer TimerShowWindow = new Timer(); //Dialog Show용 Timer
        private int TimerCount = 0;

        #region Window Message Variable
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MOVE = 0xF010;
        private const int SC_SIZE = 0xF000;
        #endregion Window Message Variable

        #region Initialize & DeInitialize
        public MainForm()
        {
            CMsgBoxManager.Initialize();
            ProgramLogin();

            CLoadingManager.Show("Program Run", "Program Run Waiting...");
            InitializeComponent();
            InitializeLanguage();
            Initialize();
            CLoadingManager.Hide();
            System.Threading.Thread.Sleep(200);

            TimerShowWindow.Start();
        }

        private void ProgramLogin()
        {
            #region VisionInspectionData Common File
            DirectoryInfo _DirInfo = new DirectoryInfo(@"D:\VisionInspectionData\Common\");
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _ProjectInfoFileName = @"D:\VisionInspectionData\Common\ProjectInformation.xml";
            if (false == File.Exists(_ProjectInfoFileName))
            {
                File.Create(_ProjectInfoFileName).Close();
                ProjectName = "KPINTVisionInspection";
            }

            else
            {
                XmlNodeList _XmlNodeList = GetNodeList(_ProjectInfoFileName);
                if (null == _XmlNodeList) return;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return;
                    switch (_Node.Name)
                    {
                        case "ProjectName":     ProjectName = _Node.InnerText; break;
                        case "IOBoardUsable":   IsIOBoardUsable = Convert.ToBoolean(_Node.InnerText); break;
                        case "EthernetUsable":  IsEthernetUsable = Convert.ToBoolean(_Node.InnerText); break;
                        case "MitsuCommUsable": IsMitsuCommUsable = Convert.ToBoolean(_Node.InnerText); break;
                    }
                }
            }

            if (ProjectName == "") ProjectName = "KPINTVisionInspection";
            #endregion VisionInspectionData Common File

            #region Parameter Initialize
            ParamManager = new CParameterManager();
            ParamManager.Initialize(ProjectName);
            #endregion Parameter Initialize
        }

        private void InitializeLanguage()
        {
            CParameterManager.Language = (eLanguage)ParamManager.SystemParam.Language;
            if (CParameterManager.Language == eLanguage.KR) LanguageResource.Culture = new CultureInfo("ko-KR"); // ko-KR
            else                                            LanguageResource.Culture = new CultureInfo("en-US"); // ko-KR

            #region Control Name Setting
            this.ribbonTabInspectionMain.Text = KPVisionInspectionFramework.LanguageResource.InspectionMain;
            this.ribbonPanelOperating.Text = KPVisionInspectionFramework.LanguageResource.InspectionOperating;
            this.ribbonPanelSetting.Text = KPVisionInspectionFramework.LanguageResource.Setting;
            this.ribbonPanelData.Text = KPVisionInspectionFramework.LanguageResource.Data;
            this.ribbonPanelStatus.Text = KPVisionInspectionFramework.LanguageResource.Status;
            this.ribbonPanelSystem.Text = KPVisionInspectionFramework.LanguageResource.System;
            this.rbStart.Text = KPVisionInspectionFramework.LanguageResource.Auto;
            this.rbStop.Text = KPVisionInspectionFramework.LanguageResource.Stop;
            this.rbCalibration.Text = KPVisionInspectionFramework.LanguageResource.Calibration;
            this.rbAlign.Text = KPVisionInspectionFramework.LanguageResource.Align;
            this.rbEthernet.Text = KPVisionInspectionFramework.LanguageResource.Ethernet;
            this.rbSerial.Text = KPVisionInspectionFramework.LanguageResource.Serial;
            this.rbLight.Text = KPVisionInspectionFramework.LanguageResource.Light;
            this.rbDIO.Text = KPVisionInspectionFramework.LanguageResource.DIO;
            this.rbConfig.Text = KPVisionInspectionFramework.LanguageResource.Config;
            this.rbMapData.Text = KPVisionInspectionFramework.LanguageResource.MapData;
            this.rbRecipe.Text = KPVisionInspectionFramework.LanguageResource.Recipe;
            this.rbLog.Text = KPVisionInspectionFramework.LanguageResource.Log;
            this.rbHistory.Text = KPVisionInspectionFramework.LanguageResource.History;
            this.rbFolder.Text = KPVisionInspectionFramework.LanguageResource.Folder;
            this.rbSave.Text = KPVisionInspectionFramework.LanguageResource.Save;
            this.rbExit.Text = KPVisionInspectionFramework.LanguageResource.Exit;
            #endregion
        }

        private void Initialize()
        {
            LoadDefaultRibbonTheme();

            #region Ribbon Menu Setting
            //LDH, 2019.01.10, 다중 RecipeName 사용으로 표시 생략
            //UpdateRibbonRecipeName(ParamManager.SystemParam.LastRecipeName);
            if ((int)eProjectType.NONE == ParamManager.SystemParam.ProjectType)
            {
                rbAlign.Visible = false;
                rbSerial.Visible = false;
                rbConfig.Visible = false;
                rbFolder.Visible = false;
                this.Size = new Size(1280, 1024);
            }

            else if((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)
            {
                rbAlign.Visible = false;
                rbEthernet.Visible = false;
                rbSerial.Visible = false;
                rbDIO.Visible = false;
                rbConfig.Visible = false;
                rbMapData.Visible = false;
                rbHistory.Visible = false;
                rbFolder.Visible = false;
                this.Size = new Size(1920, 1080);
            }

            else if ((int)eProjectType.NAVIEN == ParamManager.SystemParam.ProjectType)
            {
                rbStop.Visible = false;
                rbAlign.Visible = false;
                rbEthernet.Visible = false;
                rbSerial.Visible = false;
                rbConfig.Visible = false;
                rbMapData.Visible = false;
                rbHistory.Visible = false;
                rbFolder.Visible = false;
                this.Size = new Size(1920, 1080);

                NoticeWnd = new NoticeWindow();
            }

            UpdateRibbonRecipeName(ParamManager.SystemParam.LastRecipeName[0]);

            #endregion Ribbon Menu Setting

            #region Log Window Initialize
            LogWnd = new CLogManager(ProjectName);
            CLogManager.LogSystemSetting(@"D:\VisionInspectionData\" + ProjectName + @"\Log\SystemLog");
            CLogManager.LogInspectionSetting(@"D:\VisionInspectionData\" + ProjectName + @"\Log\InspectionLog");
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("MainProcess : {0} program run!!", ProjectName));
            #endregion Log Window Initialize

            #region SubWindow 생성 및 Event 등록
            #region Recipe Window Initialize
            //Recipe Initialize
            RecipeWnd = new RecipeWindow((eProjectType)ParamManager.SystemParam.ProjectType, ProjectName, ParamManager.SystemParam.LastRecipeName, ParamManager.SystemParam.IsTotalRecipe);
            RecipeWnd.RecipeChangeEvent += new RecipeWindow.RecipeChangeHandler(RecipeChange);
            RecipeWnd.RecipeCopyEvent += new RecipeWindow.RecipeCopyHandler(RecipeCopy);
            #endregion Recipe Window Initialize

            #region Result Window Initialize
            //Result Initialize
            ResultBaseWnd = new MainResultBase(ParamManager.SystemParam.LastRecipeName);
            if ((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.NAVIEN)
            {
                ResultBaseWnd.SendDIOResultEvent += new MainResultBase.SendDIOResultHandler(SendDIOResult);
                ResultBaseWnd.RecipeChangeEvent += new MainResultBase.RecipeChangeHandler(RecipeChange);
            }
            else if ((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.SCALIGN)
            {
                ResultBaseWnd.SendPLCResultEvent += new MainResultBase.SendPLCResultHandler(SendPLCResult);
                ResultBaseWnd.SendResultWndEvent += new MainResultBase.ResultWndHandler(SendResultWndCommand);
            }
            ResultBaseWnd.Initialize(this, ParamManager.SystemParam.ProjectType);
            ResultBaseWnd.SetWindowLocation(ParamManager.SystemParam.ResultWindowLocationX, ParamManager.SystemParam.ResultWindowLocationY);
            ResultBaseWnd.SetWindowSize(ParamManager.SystemParam.ResultWindowWidth, ParamManager.SystemParam.ResultWindowHeight);
            if ((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.NAVIEN)
            {
                for(int iLoopCount = 0; iLoopCount < ParamManager.InspSysManagerParam.Count(); iLoopCount++)
                    ResultBaseWnd.ClearResultData(ParamManager.InspParam[iLoopCount].ResultUseFlag);
            }
            else ResultBaseWnd.ClearResultData();
                        
            #endregion Result Window Initialize

            #region Light Window Initialize
            //Light Initialize
            LightControlManager = new CLightManager();
            LightControlManager.Initialize(ParamManager.SystemParam.LastRecipeName, ParamManager.SystemParam.IsTotalRecipe, ParamManager.SystemParam.IsSimulationMode);
            #endregion Light Window Initialize

            #region History Window Initialize
            HistoryManager = new CHistoryManager(ProjectName, (eProjectType)ParamManager.SystemParam.ProjectType);
            #endregion History Window Initialize

            #region FolderPath Window Initialize
            FolderPathWnd = new FolderPathWindow(ParamManager.SystemParam.ProjectType);
            FolderPathWnd.SetDataPathEvent += new FolderPathWindow.SetDataPathHandler(SetDataFolderPath);
            #endregion FolderPath Window Initialize

            System.Threading.Thread.Sleep(100);
            #endregion SubWindow 생성 및 Event 등록

            #region Project 별 MainProcess Setting
            if ((int)eProjectType.NONE == ParamManager.SystemParam.ProjectType) 			MainProcess = new MainProcessDefault();
            else if ((int)eProjectType.NAVIEN == ParamManager.SystemParam.ProjectType)      MainProcess = new MainProcessNavien();
            else if ((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)     MainProcess = new MainProcessSolarAlign();
            else                                                                            MainProcess = new MainProcessBase();

            MainProcess.MainProcessCommandEvent += new MainProcessBase.MainProcessCommandHandler(MainProcessCommandEventFunction);
            if ((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)
            {
                MainProcess.GetReadAlignValueEvent += new MainProcessSolarAlign.GetReadAlignValueHanbdler(GetReadAlignValueEventFunction);
            }
            MainProcess.Initialize(@"D:\VisionInspectionData\Common\", IsIOBoardUsable, IsEthernetUsable, IsMitsuCommUsable);
            #endregion MainProcess Setting

            #region InspSysManager Initialize
            ISMModuleCount = ParamManager.SystemParam.InspSystemManagerCount;
            InspSysManager = new CInspectionSystemManager[ISMModuleCount];
            for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount)
            {
                InspSysManager[iLoopCount] = new CInspectionSystemManager(iLoopCount, KPVisionInspectionFramework.LanguageResource.Vision + (iLoopCount + 1), ParamManager.SystemParam.IsSimulationMode);
                InspSysManager[iLoopCount].InspSysManagerEvent += new CInspectionSystemManager.InspSysManagerHandler(InspectionSystemManagerEventFunction);
                InspSysManager[iLoopCount].Initialize(this, ParamManager.SystemParam.ProjectType, ParamManager.InspSysManagerParam[iLoopCount], ParamManager.InspParam[iLoopCount], ParamManager.SystemParam.LastRecipeName[iLoopCount], ParamManager.SystemParam.DataFolderPath[0]);
            }

            TimerShowWindow.Tick += new EventHandler(TimerShowWindowTick);
            TimerShowWindow.Interval = 500;
            #endregion InspSysManager Initialize
        }

        private void DeInitialize()
        {
            GetISMWindowInformation();

            ParamManager.SystemParam.ResultWindowLocationX = ResultBaseWnd.Location.X;
            ParamManager.SystemParam.ResultWindowLocationY = ResultBaseWnd.Location.Y;
            ParamManager.SystemParam.ResultWindowWidth = ResultBaseWnd.Width;
            ParamManager.SystemParam.ResultWindowHeight = ResultBaseWnd.Height;
            ResultBaseWnd.DeInitialize();

            RecipeWnd.RecipeChangeEvent -= new RecipeWindow.RecipeChangeHandler(RecipeChange);
            RecipeWnd.RecipeCopyEvent -= new RecipeWindow.RecipeCopyHandler(RecipeCopy);

            if ((int)eProjectType.NAVIEN == ParamManager.SystemParam.ProjectType)
            {
                for (int iLoopCount = 0; iLoopCount < ParamManager.SystemParam.LastRecipeName.Length; ++iLoopCount)
                    ParamManager.SystemParam.LastRecipeName[iLoopCount] = "[Default]";

                ResultBaseWnd.SendDIOResultEvent -= new MainResultBase.SendDIOResultHandler(SendDIOResult);
                ResultBaseWnd.RecipeChangeEvent -= new MainResultBase.RecipeChangeHandler(RecipeChange);
            }
            else if ((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)
            {
                ResultBaseWnd.SendPLCResultEvent -= new MainResultBase.SendPLCResultHandler(SendPLCResult);
                ResultBaseWnd.SendResultWndEvent -= new MainResultBase.ResultWndHandler(SendResultWndCommand);
            }

            ParamManager.DeInitialize();

            LightControlManager.DeInitialize();

            FolderPathWnd.SetDataPathEvent -= new FolderPathWindow.SetDataPathHandler(SetDataFolderPath);

            MainProcess.MainProcessCommandEvent -= new MainProcessBase.MainProcessCommandHandler(MainProcessCommandEventFunction);
            if ((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)
            {
                MainProcess.GetReadAlignValueEvent -= new MainProcessBase.GetReadAlignValueHanbdler(GetReadAlignValueEventFunction);
            }
            MainProcess.DeInitialize();

            if((int)eProjectType.NONE == ParamManager.SystemParam.ProjectType)              MainProcess.DeInitialize();

            for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount)
                InspSysManager[iLoopCount].InspSysManagerEvent -= new CInspectionSystemManager.InspSysManagerHandler(InspectionSystemManagerEventFunction);

            for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount)
                InspSysManager[iLoopCount].DeInitialize();
        }

        private void LoadDefaultRibbonTheme()
        {
            string content = System.IO.File.ReadAllText("MainTheme2.ini");
            Theme.ColorTable.ReadThemeIniFile(content);
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Text = string.Format("(주)KP INT - {0} Program", ProjectName);

            string _CompileMode = "";
#if DEBUG
            _CompileMode = " - [DEBUG]";
#endif
            string _Version = System.Windows.Forms.Application.ProductVersion;
            this.Text = this.Text + " - Ver " + _Version + _CompileMode;

            ribbonMain.Refresh();
            this.Refresh();
        }

        private void GetISMWindowInformation()
        {
            Point _InspLocation;
            Size _InspSize;
            double _Zoom, _PanX, _PanY;
            for (int iLoopCount = 0; iLoopCount < ParamManager.SystemParam.InspSystemManagerCount; iLoopCount++)
            {
                InspSysManager[iLoopCount].GetDisplayWindowInfo(out _Zoom, out _PanX, out _PanY);
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.DisplayZoomValue = _Zoom;
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.DisplayPanXValue = _PanX;
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.DisplayPanYValue = _PanY;

                InspSysManager[iLoopCount].GetWindowLocation(out _InspLocation);
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.LocationX = _InspLocation.X;
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.LocationY = _InspLocation.Y;

                InspSysManager[iLoopCount].GetWindowSize(out _InspSize);
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.Width = _InspSize.Width;
                ParamManager.InspSysManagerParam[iLoopCount].InspWndParam.Height = _InspSize.Height;
            }
        }

        private void TimerShowWindowTick(object sender, EventArgs e)
        {
            TimerCount++;
            if (TimerCount >= 2)
            {
                TimerCount = 0;
                TimerShowWindow.Stop();
                CLoadingManager.Show("Recent Recipe", "Recipe Loading...");
                for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount)
                {
                    InspSysManager[iLoopCount].ShowWindows();
                }
                ResultBaseWnd.Show();
                CLoadingManager.Hide();
            }

            //FormTopMostInvoke(this, true);
            if (false == ParamManager.SystemParam.IsSimulationMode)
                this.WindowState = FormWindowState.Maximized;

            if (MainLogoWnd != null) MainLogoWnd.Show();

        }

        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int _Command = message.WParam.ToInt32() & 0xfff0;
                    if (_Command == SC_MOVE || _Command == SC_SIZE) return;
                    break;
            }

            base.WndProc(ref message);
        }

        private XmlNodeList GetNodeList(string _XmlFilePath)
        {
            XmlNodeList _XmlNodeList = null;

            try
            {
                XmlDocument _XmlDocument = new XmlDocument();
                _XmlDocument.Load(_XmlFilePath);
                XmlElement _XmlRoot = _XmlDocument.DocumentElement;
                _XmlNodeList = _XmlRoot.ChildNodes;
            }

            catch
            {
                _XmlNodeList = null;
            }

            return _XmlNodeList;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show(new Form { TopMost = true }, "Do you want exit program ? ", "Exit Program", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (DialogResult.Yes != dlgResult) { e.Cancel = true; return; }
            DeInitialize();
            Environment.Exit(0);
        }
        #endregion Initialize & DeInitialize

        #region Ribbon Button Event
        private void rbStart_Click(object sender, EventArgs e)
        {
            //MainProcessStartOn();
            MainProcess.SetModeStatus(VTPAddr.MODE_STATUS, VTPDefine.AUTO_MODE);
            CParameterManager.SystemMode = eSysMode.AUTO_MODE;
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess : AutoMode", CLogManager.LOG_LEVEL.LOW);

            MainProcessReset();

            rbStart.Enabled = false;
            rbStop.Enabled = true;
            rbCalibration.Enabled = false;
        }

        private void rbStop_Click(object sender, EventArgs e)
        {
            SetManualMode();

            rbStart.Enabled = true;
            rbCalibration.Enabled = true;
        }

        private void SetManualMode()
        {
            MainProcess.SetModeStatus(VTPAddr.MODE_STATUS, VTPDefine.MANUAL_MODE);
            CParameterManager.SystemMode = eSysMode.MANUAL_MODE;
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess : ManualMode", CLogManager.LOG_LEVEL.LOW);

            //for (int iLoopCount = 0; iLoopCount < ParamManager.SystemParam.InspSystemManagerCount; ++iLoopCount)
            //    InspSysManager[iLoopCount].SetSystemMode(eSysMode.MANUAL_MODE);
            //
            rbStart.Enabled = true;
            rbStop.Enabled = true;
            //rbLight.Enabled = true;
            //
            //MainProcess.AutoMode(false);
            //ResultBaseWnd.SetAutoMode(false);
            //CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess AutoMode STOP", CLogManager.LOG_LEVEL.MID);
        }

        private void rbCalibration_Click(object sender, EventArgs e)
        {
            MainProcess.SetModeStatus(VTPAddr.MODE_STATUS, VTPDefine.AUTO_MODE);
            CParameterManager.SystemMode = eSysMode.CAL_MODE;
            ResultBaseWnd.SetCalMode();

            rbStart.Enabled = false;

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess : CalibrationMode", CLogManager.LOG_LEVEL.LOW);
        }

        private void rbCommunication_Click(object sender, EventArgs e)
        {
            if (false == MainProcess.GetMitsubishiWindowShown()) MainProcess.ShowMitsubishiWindow();
            else                                                 MainProcess.SetMitsubishiWindowTopMost(true);
        }

        private void rbEthernet_Click(object sender, EventArgs e)
        {
            if (false == MainProcess.GetEhernetWindowShown())   MainProcess.ShowEthernetWindow();
            else                                                MainProcess.SetEthernetWindowTopMost(true);

            //MainProcess.SetEthernetWindowTopMost(false);
        }

        private void rbSerial_Click(object sender, EventArgs e)
        {
            if (false == MainProcess.GetSerialWindowShown())    MainProcess.ShowSerialWindow();
            else                                                MainProcess.SetSerialWindowTopMost(true);

            //MainProcess.SetSerialWindowTopMost(false);
        }

        private void rbLight_Click(object sender, EventArgs e)
        {
            LightControlManager.ShowLightWindow();
        }

        private void rbDIO_Click(object sender, EventArgs e)
        {
            if (false == MainProcess.GetDIOWindowShown())   MainProcess.ShowDIOWindow();
            else                                            MainProcess.SetDIOWindowTopMost(true);

            //MainProcess.SetDIOWindowTopMost(false);
        }

        private void rbConfig_Click(object sender, EventArgs e)
        {

        }

        private void rbMapData_Click(object sender, EventArgs e)
        {
            ribbonPanelOperating.Enabled = false;
            ribbonPanelSetting.Enabled = false;
            ribbonPanelData.Enabled = false;
            ribbonPanelStatus.Enabled = false;
            ribbonPanelSystem.Enabled = false;
            rbMapData.Enabled = false;

            InspSysManager[0].ShowMapDataWindow();

            ribbonPanelOperating.Enabled = true;
            ribbonPanelSetting.Enabled = true;
            ribbonPanelData.Enabled = true;
            ribbonPanelStatus.Enabled = true;
            ribbonPanelSystem.Enabled = true;
            rbMapData.Enabled = true;
        }

        private void rbRecipe_Click(object sender, EventArgs e)
        {
            CParameterManager.SystemModeBackup = CParameterManager.SystemMode;
            RecipeWnd.ShowDialogWindow();
            CParameterManager.SystemMode = CParameterManager.SystemModeBackup;
        }

        private void rbLog_Click(object sender, EventArgs e)
        {
            LogWnd.ShowLogWindow(!LogWnd.IsShowLogWindow);
        }

        private void rbHistory_Click(object sender, EventArgs e)
        {
            HistoryManager.ShowHistoryWindow();
        }

        private void rbFolder_Click(object sender, EventArgs e)
        {
            for(int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
            {
                FolderPathWnd.SetCurrentDataPath(iLoopCount, ParamManager.SystemParam.DataFolderPath[iLoopCount]);
            }
            FolderPathWnd.ShowDialog();
        }

        private void rbSave_Click(object sender, EventArgs e)
        {
            //LDH, 2019.09.18, 전체저장
            for(int iLoopCount = 0; iLoopCount < InspSysManager.Count(); iLoopCount++)
            {
                TeachingParameterSave(iLoopCount);
            }
        }

        private void rbExit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount) InspSysManager[iLoopCount].ImageContinuesGrabStop();

                DialogResult dlgResult = MessageBox.Show(new Form { TopMost = true }, "Do you want exit program ? ", "Exit Program", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
                if (DialogResult.Yes != dlgResult) return;
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess : KPINT Vision inspection program exit!!");

                DeInitialize();
                Environment.Exit(0);
            }

            catch (Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "MainForm Exit : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                Environment.Exit(0);
            }
        }

        #region Form TopMost Invoke
        /// <summary>
        /// Label Update
        /// </summary>
        public void FormTopMostInvoke(object _object, bool _Flag)
        {
            Form _Form = (MainForm)_object;
            if (_Form.InvokeRequired)
            {
                _Form.Invoke(new MethodInvoker(delegate ()
                {
                    _Form.TopMost = _Flag;
                }
                ));
            }

            else
            {
                _Form.TopMost = _Flag;
            }
        }
        #endregion Label Invoke
        #endregion Ribbon Button Event

        #region Event : Inspection System Manager Event & Function
        private void InspectionSystemManagerEventFunction(eISMCMD _Command, object _Value = null, int _ID = 0)
        {
            switch (_Command)
            {
                case eISMCMD.TEACHING_STATUS:   TeachingStatusCheck(Convert.ToBoolean(_Value)); break;
                case eISMCMD.TEACHING_SAVE:     TeachingParameterSave(Convert.ToInt32(_Value)); break;
                case eISMCMD.MAPDATA_SAVE:      MapDataParameterSave(_Value, _ID);              break;
                case eISMCMD.SEND_DATA:         SendResultData(_Value);                         break;
                case eISMCMD.SET_RESULT:        SetResultData(_Value);                          break;
                case eISMCMD.INSP_COMPLETE:     InspectionComplete(_Value, _ID);                break;
                case eISMCMD.LIGHT_CONTROL:     LightControl(_Value, _ID);                      break;
                case eISMCMD.NOTICE_WINDOW:     SetNoticeWindow(_Value);                           break;
            }
        }

        private void TeachingStatusCheck(bool _IsTeaching)
        {
            //Teacing status check
            if (_IsTeaching)
            {
                ribbonPanelOperating.Enabled = false;
                ribbonPanelSetting.Enabled = false;
                ribbonPanelData.Enabled = false;
                ribbonPanelStatus.Enabled = false;
                ribbonPanelSystem.Enabled = false;
                rbMapData.Enabled = false;
                CParameterManager.SystemMode = eSysMode.TEACH_MODE;
            }

            else
            {
                ribbonPanelOperating.Enabled = true;
                ribbonPanelSetting.Enabled = true;
                ribbonPanelData.Enabled = true;
                ribbonPanelStatus.Enabled = true;
                ribbonPanelSystem.Enabled = true;
                rbMapData.Enabled = true;
                CParameterManager.SystemMode = eSysMode.MANUAL_MODE;
            }
        }

        private void TeachingParameterSave(int _ID)
        {
            string _UseResultFlag = "";

            if((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.NAVIEN)
            {
                ResultBaseWnd.GetUseResult(_ID, out _UseResultFlag);
                ParamManager.InspParam[_ID].ResultUseFlag = _UseResultFlag;
            }

            InspSysManager[_ID].GetInspectionParameterRef(ref ParamManager.InspParam[_ID]);
            ParamManager.WriteInspectionParameter(_ID);
        }

        private void MapDataParameterSave(object _Value, int _ID)
        {
            var _MapDataParam = _Value as MapDataParameter;
            CParameterManager.RecipeCopy(_MapDataParam, ref ParamManager.InspMapDataParam[_ID]);

            ParamManager.WriteInspectionMapDataparameter(_ID);
        }
        #endregion Event : Inspection System Manager Event & Function

        #region Event : I/O Event & Function
        private void InputChangeEventFunction(short _BitNum, bool _Signal)
        {
            //if ((short)DIO_DEF.IN_TRG1 == _BitNum) EventInspectionTriggerOn(_Signal);
            //if ((short)DIO_DEF.IN_TRG1 == _BitNum) MainProcess.TriggerOn(InspSysManager, 0);
            switch (_BitNum)
            {
                //case DIO_DEF.IN_TRG1:   MainProcess.TriggerOn(InspSysManager, 0);  break;
                //case DIO_DEF.IN_RESET:  MainProcess.Reset(_BitNum); break;
            }
        }
        #endregion Event : I/O Event & Function

        #region Sub Window Events
        private bool RecipeChange(int _ID, string _RecipeName)
        {
            bool _Result = true;

            if((int)eProjectType.NAVIEN == ParamManager.SystemParam.ProjectType)
            {
                NoticeWnd.HideWindow();

                string ReturnRecipeName = "";
                //LDH, 2019.09.17, Recipe가 생성되어 있는지 체크
                if (RecipeWnd.CheckRecipeExist(_RecipeName, out ReturnRecipeName))
                {
                    //LDH, 2019.09.03, 사용하던 Recipe명과 같은지 확인
                    if (ParamManager.SystemParam.LastRecipeName[_ID] == ReturnRecipeName) return true;

                    _RecipeName = ReturnRecipeName;
                }
                else
                {
                    MessageBox.Show("불러올 Recipe가 없습니다.\nRecipe를 새로 생성하세요.");
                    return false;
                }
            }

            if (true == ParamManager.RecipeReload(_ID, _RecipeName))
            {
                //LDH, 2018.07.26, Light File 따로 관리
                //LJH, 2019.01.28 For문 안으로 위치 변경.
                LightControlManager.RecipeChange(_ID, _RecipeName);

                for (int iLoopCount = 0; iLoopCount < ISMModuleCount; ++iLoopCount)
                {
                    InspSysManager[iLoopCount].SetInspectionParameter(ParamManager.InspParam[iLoopCount], false);
                    InspSysManager[iLoopCount].GetInspectionParameterRef(ref ParamManager.InspParam[iLoopCount]);

                    if ((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.NAVIEN)
                    {
                        ResultBaseWnd.ClearResultData(ParamManager.InspParam[iLoopCount].ResultUseFlag);
                    }
                }

                UpdateRibbonRecipeName(_RecipeName);
            }
            else
            {
                MessageBox.Show(new Form { TopMost = true }, "Recipe 변경에 실패했습니다.\nRecipe를 확인하세요.");
                _Result = false;
            }

            ResultBaseWnd.SetLastRecipeName((eProjectType)ParamManager.SystemParam.ProjectType, ParamManager.SystemParam.LastRecipeName);

            return _Result;
        }

        private bool RecipeCopy(string _RecipeName, string _SrcRecipeName)
        {
            bool _Result = true;

            try
            {
                LightControlManager.RecipeCopy(_RecipeName, _SrcRecipeName);
            }
            
            catch
            {
                MessageBox.Show(new Form { TopMost = true }, "Recipe 변경에 실패했습니다.\nRecipe를 확인하세요.");
                _Result = false;
            }

            return _Result;
        }

        private void UpdateRibbonRecipeName(string _RecipeName)
        {
            if((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.NAVIEN)
            {
                string[] _ProductInfo = new string[2];
                if (_RecipeName.Length > 14)
                {
                    _ProductInfo[0] = _RecipeName.Substring(6, 9);
                    _ProductInfo[1] = _RecipeName.Substring(14, _RecipeName.Length - 14);

                    rbLabelCurrentRecipe.Text = "Recipe : " + _RecipeName.Substring(0, 5) + " ";
                }
                else
                {
                    _ProductInfo[0] = "";
                    _ProductInfo[1] = "";

                    rbLabelCurrentRecipe.Text = "Recipe : " + _RecipeName + " ";
                }

                if (ResultBaseWnd != null) ResultBaseWnd.SetLOTNum(_ProductInfo);                
            }
            else rbLabelCurrentRecipe.Text = "Recipe : " + _RecipeName + " ";
        }
        #endregion Sub Window Events

        #region Main Process
        private void MainProcessCommandEventFunction(eMainProcCmd _MainProcCmd, object _Value)
        {
            switch (_MainProcCmd)
            {
                case eMainProcCmd.TRG:          MainProcessTriggerOn(_Value);    break;
                case eMainProcCmd.CAL:          MainProcessTriggerOn(_Value);    break;
                case eMainProcCmd.REQUEST:      MainProcessDataRequest(_Value);  break;
                case eMainProcCmd.RCP_CHANGE:   MainProcessRcpChange(_Value);    break;
                case eMainProcCmd.RECV_DATA:    MainProcessEthernetRecv(_Value); break;
                case eMainProcCmd.START:        MainProcessStartOn();            break;
            }
        }

        private void MainProcessTriggerOn(object _Value)
        {
#if _RELEASE
            if (CParameterManager.SystemMode != eSysMode.AUTO_MODE && CParameterManager.SystemMode != eSysMode.CAL_MODE) return;
#endif

            int _ID = Convert.ToInt32(_Value);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : Trigger{0} On Event", _ID + 1));
            InspSysManager[_ID].TriggerOn();

            if (eProjectType.NAVIEN == (eProjectType)ParamManager.SystemParam.ProjectType) ResultBaseWnd.SetCycleTime();
        }

        private bool MainProcessRcpChange(object _Value)
        {
            bool _Result = true;

            string _RecipeName = _Value as string;
            
            //LDH, 2019.01.11, ID로 Recipe 관리를 하기 위해 통신으로 변경시 ID = -1 로 고정함 
            _Result = RecipeChange(-1, _RecipeName);
            if (!_Result) return false;

            return _Result;
        }

        private void MainProcessDataRequest(object _Value)
        {
            if (CParameterManager.SystemMode != eSysMode.AUTO_MODE) return;

            int _ID = Convert.ToInt32(_Value);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : Data Request{0} Event", _ID + 1));
            InspSysManager[_ID].DataSend();
        }

        //LDH, 2019.04.02, Ethernet Receive Data 전달 Event
        private void MainProcessEthernetRecv(object _Value)
        {
            ResultBaseWnd.SetEthernetRecvData(_Value);

            //Data 받은거 확인 command 넘길때
            if (eProjectType.NONE == (eProjectType)ParamManager.SystemParam.ProjectType)
            {
                //EthernetRecvInfo _ReceiveData = _Value as EthernetRecvInfo;

                //switch (_ReceiveData.RecvData[0])
                //{
                //    case "00": MainProcess.SendSerialData(eMainProcCmd.ACK_COMPLETE, _ReceiveData.PortNumber.ToString()); break;
                //}
            }
            else
            {
                string[] _ReceiveData = _Value as string[];
            }
        }

        private void MainProcessStartOn()
        {
            bool _InspFlag = true;

            if (eProjectType.SCALIGN == (eProjectType)ParamManager.SystemParam.ProjectType)
            {
                if (_InspFlag)
                {
                    rbStart.Enabled = false;
                    rbLight.Enabled = false;

                    MainProcess.SetModeStatus(VTPAddr.MODE_STATUS, VTPDefine.AUTO_MODE);
                }
            }

            else if (eProjectType.NAVIEN == (eProjectType)ParamManager.SystemParam.ProjectType)
            {
                if (!NoticeWnd.GetWindowStatus())
                {
                    if (!ResultBaseWnd.GetBarcodeStatus()) { MessageBox.Show("Barcode를 입력하세요."); _InspFlag = false; }
                }
            }

            else
            {
                if (_InspFlag)
                {
                    rbStart.Enabled = false;
                    rbLight.Enabled = false;
                }
            }

            if (_InspFlag)
            {
                for (int iLoopCount = 0; iLoopCount < ParamManager.SystemParam.InspSystemManagerCount; ++iLoopCount)
                    InspSysManager[iLoopCount].SetSystemMode(eSysMode.AUTO_MODE);

                CParameterManager.SystemMode = eSysMode.AUTO_MODE;

                MainProcess.AutoMode(true);
                ResultBaseWnd.SetAutoMode(true);
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "MainProcess AutoMode ON", CLogManager.LOG_LEVEL.MID);
            }
        }

        private void MainProcessReset()
        {
            if ((int)eProjectType.SCALIGN == ParamManager.SystemParam.ProjectType)
            {
                for (int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
                {
                    MainProcess.Reset(iLoopCount + 1);
                }
            }
        }

        private void SendResultData(object _Result)
        {
            SendResultParameter _SendResParam = _Result as SendResultParameter;
            MainProcess.SendResultData(_SendResParam);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : SendResultData"));
        }

        private void SetResultData(object _Result)
        {
            SendResultParameter _SendResParam = _Result as SendResultParameter;
            ResultBaseWnd.SetResultData(_SendResParam);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : SetResultData"));
        }

        private void LightControl(object _LightOnOff, int _ID)
        {
            if ((bool)_LightOnOff == true)  LightControlManager.LightControl(_ID, true);
            else                            LightControlManager.LightControl(_ID, false);
        }

        private void InspectionComplete(object _Value, int _ID)
        {
            //if ((eProjectType)ParamManager.SystemParam.ProjectType == eProjectType.TRIM_FORM)
            //{
            //    bool _Flag = Convert.ToBoolean(_Value);
            //    MainProcess.InspectionComplete(_ID, _Flag);
            //}
        }

        private void SetNoticeWindow(object _Value)
        {
            NoticeWnd.ShowWindow();
        }

        private void SetDataFolderPath(string[] _DataPath)
        {
            for(int iLoopCount = 0; iLoopCount < _DataPath.Count(); iLoopCount++)
            {
                ParamManager.SystemParam.DataFolderPath[iLoopCount] = _DataPath[iLoopCount];
            }            
            ParamManager.WriteSystemParameter();

            ResultBaseWnd.SetDataFolderPath(_DataPath);
        }

        private void SendDIOResult(bool _LastResult)
        {
            if (!ParamManager.SystemParam.IsSimulationMode)
            {
                MainProcess.StatusMode(NavienCmd.OUT_GOOD, false);
                MainProcess.StatusMode(NavienCmd.OUT_READY, false);
                MainProcess.StatusMode(NavienCmd.OUT_ERROR, false);

                if (_LastResult) MainProcess.StatusMode(NavienCmd.OUT_GOOD, true);
                else             MainProcess.StatusMode(NavienCmd.OUT_ERROR, true);
            }
        }

        private void SendPLCResult(object _Command, string _PLCResult)
        {
            if (VTPAddr.V1_INSP_RESULT == (int)_Command || VTPAddr.V2_INSP_RESULT == (int)_Command)
            {
                MainProcess.SetResult((int)_Command, Convert.ToInt16(_PLCResult));
            }

            else
            {
                int _ResultData = Convert.ToInt32(Convert.ToDouble(_PLCResult) * 10);
                string _ResultBitData = Convert.ToString(_ResultData, 2);

                for (int iLoopCount = _ResultBitData.Length; iLoopCount < 32; ++iLoopCount) _ResultBitData = "0" + _ResultBitData;
                string _ResultHiBit = _ResultBitData.Substring(0, _ResultBitData.Length - 16);
                string _ResultLowBit = _ResultBitData.Substring(_ResultBitData.Length - 16, 16);
                short _ResultHiDec = Convert.ToInt16(_ResultHiBit, 2);
                short _ResultLowDec = Convert.ToInt16(_ResultLowBit, 2);

                MainProcess.SetDWordResult((int)_Command, _ResultLowDec, _ResultHiDec);
            }
        }

        private void SendResultWndCommand(object _Command)
        {
            if((eSysMode)_Command == eSysMode.MANUAL_MODE) SetManualMode();
        }

        private void GetReadAlignValueEventFunction(int _StageNumber, AlignAxis _AxisValue)
        {
            ResultBaseWnd.SetAlignCurrentPosition(_StageNumber, _AxisValue);
        }
        #endregion Main Process
    }
}
