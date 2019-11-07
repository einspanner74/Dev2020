using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Globalization;

using Cognex.VisionPro;
using Cognex.VisionPro.PMAlign;

using LogMessageManager;
using ParameterManager;
using MapDataManager;

namespace InspectionSystemManager
{
    public class CInspectionSystemManager
    {
        private InspectionParameter InspParam = new InspectionParameter();
        private MapDataParameter    MapDataParam = new MapDataParameter();

        private InspectionWindow InspWnd;
        private string           InspWndName;

        private MapDataWindow    MapDataWnd;

        private Thread ThreadInspection;
        private bool IsThreadInspectionExit = false;
        private bool IsThreadInspectionTrigger = false;

        private  int  ID = 0;
        private  bool IsSimulationMode = false;
        private string CameraType;
        private eProjectType ProjectType = 0;
        private eProjectItem ProjectItem = 0;
        private double CameraRotate;

        private Point WndLocation = new Point(0, 0);

        public delegate void InspSysManagerHandler(eISMCMD _Command, object _Value = null, int _ID = 0);
        public event InspSysManagerHandler InspSysManagerEvent;

        #region Initialize & DeInitialize
        public CInspectionSystemManager(int _ID, string _SystemName, bool _IsSimulationMode)
        {
            InitializeLanguage();

            ID = _ID;
            IsSimulationMode = _IsSimulationMode;

            MapDataWnd = new MapDataWindow();
            InspWnd = new InspectionWindow();
            InspWndName = String.Format(" {0} {1}", _SystemName, InspectionSystemManager.LanguageResource.TitleName);

            ThreadInspection = new Thread(ThreadInspectionFunction);
            IsThreadInspectionExit = false;
            IsThreadInspectionTrigger = false;
            ThreadInspection.Start();
        }

        public void Initialize(Object _OwnerForm, int _ProjectType, InspectionSystemManagerParameter _InspSysManagerParam, InspectionParameter _InspParam, string _RecipeName, string _FolderPath)
        {
            ProjectType = (eProjectType)_ProjectType;
            ProjectItem = (eProjectItem)_InspSysManagerParam.ProjectItem;

            _InspParam.ResolutionX = _InspSysManagerParam.ResolutionX;
            _InspParam.ResolutionY = _InspSysManagerParam.ResolutionY;

            SetISMParameter(_InspSysManagerParam);
            SetInspectionParameter(_InspParam);

            InspWnd.Initialize(_OwnerForm, ID, InspParam, ProjectType, ProjectItem, InspWndName, _RecipeName, IsSimulationMode, _FolderPath);
            InspWnd.InitializeResolution(_InspSysManagerParam.ResolutionX, _InspSysManagerParam.ResolutionY);
            InspWnd.InitializeCam(_InspSysManagerParam.CameraType, _InspSysManagerParam.CameraConfigInfo, Convert.ToInt32(_InspSysManagerParam.ImageSizeWidth), Convert.ToInt32(_InspSysManagerParam.ImageSizeHeight), _InspSysManagerParam.CameraRotate, _InspSysManagerParam.CameraName);
            InspWnd.InspectionWindowEvent += new InspectionWindow.InspectionWindowHandler(InspectionWindowEventFunction);
        }

        public void DeInitialize()
        {
            MapDataWnd.MapDataParameterSaveEvent -= new MapDataWindow.MapDataParameterSaveHandler(MapDataParameterSaveEventFunction);
            MapDataWnd.DeInitialize();

            InspWnd.InspectionWindowEvent -= new InspectionWindow.InspectionWindowHandler(InspectionWindowEventFunction);
            InspWnd.Deinitialize();

            if (ThreadInspection != null) { IsThreadInspectionExit = true; Thread.Sleep(200); ThreadInspection.Abort(); ThreadInspection = null; }
        }

        public void InitializeLanguage()
        {
            if (CParameterManager.Language == eLanguage.KR) LanguageResource.Culture = new CultureInfo("ko-KR"); // ko-KR
            else                                            LanguageResource.Culture = new CultureInfo("en-US"); // ko-KR

            #region Control Name Setting

            #endregion  
        }

        public void SetMapDataParameter(MapDataParameter _MapDataParam)
        {
            if (null == _MapDataParam) return;

            CParameterManager.RecipeCopy(_MapDataParam, ref MapDataParam);
            MapDataWnd.Initialize(MapDataParam);
            MapDataWnd.MapDataParameterSaveEvent += new MapDataWindow.MapDataParameterSaveHandler(MapDataParameterSaveEventFunction);

            //InspectionWindow에 전달
            InspWnd.SetMapDataParameter(MapDataParam);
        }

        public void SetSystemMode(eSysMode _SystemMode)
        {
            ImageContinuesGrabStop();
            InspWnd.SetSystemMode(_SystemMode);
        }

        public void GetDisplayWindowInfo(out double _DisplayZoom, out double _DisplayPanX, out double _DisplayPanY)
        {
            InspWnd.GetWindowDisplayInfo(out _DisplayZoom, out _DisplayPanX, out _DisplayPanY);
        }

        public void GetWindowLocation(out Point _InspWndLocation)
        {
            _InspWndLocation = InspWnd.Location;
        }

        public void GetWindowSize(out Size _InspWndSize)
        {
            _InspWndSize = InspWnd.Size;
        }

        public void ShowWindows()
        {
            InspWnd.Show();
            ////InspWnd.SetLocation(WndLocation.X, WndLocation.Y);
        }
        #endregion Initialize & DeInitialize

        #region Parameter Management
        /// <summary>
        ///  Set System Parameter (Main -> Inspection System Manager)
        /// </summary>
        /// <param name="_InspSysManagerParam">System Parameter</param>
        private void SetISMParameter(InspectionSystemManagerParameter _InspSysManagerParam)
        {
            CameraType = _InspSysManagerParam.CameraType;
            InspWnd.SetLocation(_InspSysManagerParam.InspWndParam.LocationX, _InspSysManagerParam.InspWndParam.LocationY);
            InspWnd.SetWindowSize(_InspSysManagerParam.InspWndParam.Width, _InspSysManagerParam.InspWndParam.Height);
            InspWnd.SetWindowDisplayInfo(_InspSysManagerParam.InspWndParam.DisplayZoomValue, _InspSysManagerParam.InspWndParam.DisplayPanXValue, _InspSysManagerParam.InspWndParam.DisplayPanYValue);

            WndLocation = new Point(_InspSysManagerParam.InspWndParam.LocationX, _InspSysManagerParam.InspWndParam.LocationY);
        }

        /// <summary>
        /// Set Inspection Parameter : (InspectionWindow(Teaching) -> Inspection System Manager)
        /// </summary>
        /// <param name="_InspParam">Inspection Parameter</param>
        /// <param name="_IsNew">Is New Parameter</param>
        public void SetInspectionParameter(InspectionParameter _InspParam, bool _IsNew = true)
        {
            if (InspParam != null) FreeInspectionParameters(ref InspParam);
            CParameterManager.RecipeCopy(_InspParam, ref InspParam);

            InspWnd.SetInspectionParameter(InspParam, _IsNew);

            //Reference File(VPP) Load
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

        public void GetInspectionParameterRef(ref InspectionParameter _InspParamDest)
        {
            if (_InspParamDest != null) FreeInspectionParameters(ref _InspParamDest);
            CParameterManager.RecipeCopy(InspParam, ref _InspParamDest);

            for (int iLoopCount = 0; iLoopCount < InspParam.InspAreaParam.Count; ++iLoopCount)
            {
                for (int jLoopCount = 0; jLoopCount < InspParam.InspAreaParam[iLoopCount].InspAlgoParam.Count; ++jLoopCount)
                {
                    eAlgoType _AlgoType = (eAlgoType)InspParam.InspAreaParam[iLoopCount].InspAlgoParam[jLoopCount].AlgoType;

                    if (eAlgoType.C_PATTERN == _AlgoType)
                    {

                    }
                }
            }
        }
        #endregion Parameter Management

        #region Vision Management
        private void ImageGrab()
        {
            ImageGrabSnap();
        }

        public void ImageContinuesGrabStop()
        {
            InspWnd.ContinuesGrabStop();
        }

        private void ImageGrabSnap()
        {
            //Camera 모듈 Check

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} ImageGrabSnap Complete", ID + 1), CLogManager.LOG_LEVEL.LOW);
            CParameterManager.SystemMode = eSysMode.AUTO_MODE;
            InspWnd.GrabAndInspection();

            //InspWnd.IsThreadInspectionProcessTrigger = true;
            CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM{0} IsThreadInspectionProcessTrigger = true", ID + 1), CLogManager.LOG_LEVEL.LOW);
        }

        private void ImageGrabLive()
        {

        }

        private void ImageGrabLiveStop()
        {

        }

        public CogImage8Grey GetOriginImage()
        {
            return InspWnd.GetOriginImage();
        }

        public void ShowMapDataWindow()
        {
            MapDataWnd.SetMapDataImage(InspWnd.GetOriginImage());
            if (System.Windows.Forms.DialogResult.OK == MapDataWnd.ShowDialog())
            {
                CParameterManager.RecipeCopy(MapDataWnd.GetCurrentMapDataParameter(), ref MapDataParam);
                InspWnd.SetMapDataParameter(MapDataParam);
            }
        }
        #endregion Vision Management

        #region Event : Inspection Window Event & Function
        private void InspectionWindowEventFunction(eIWCMD _Command, object _Value = null, int _ID = 0)
        {
            switch (_Command)
            {
                case eIWCMD.TEACHING:       Teaching(_Value);                break;
                case eIWCMD.TEACH_OK:       TeachingComplete(_Value);        break;
                case eIWCMD.TEACH_SAVE:     TeachingSave(_Value);            break;
                case eIWCMD.LIGHT_CONTROL:  LightControl(_Value, _ID);       break;
                case eIWCMD.SEND_DATA:      SendResultData(_Value);          break;
                case eIWCMD.SET_RESULT:     SetResultData(_Value);           break;
                case eIWCMD.INSP_COMPLETE:  InspectionComplete(_Value, _ID); break;
                case eIWCMD.NOTICE_WINDOW:  NoticeWindow(_Value);            break;
            }
        }

        private void Teaching(object _Value)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.TEACHING_STATUS, Convert.ToBoolean(_Value));
        }

        private void TeachingComplete(object _Value)
        {
            //InspectionParameter _InspParam = (InspectionParameter)_Value;
            var _InspParam = _Value as InspectionParameter;
            SetInspectionParameter(_InspParam, false);
        }

        private void TeachingSave(object _Value)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.TEACHING_SAVE, Convert.ToInt32(_Value));
        }

        private void LightControl(object _Value, int _ID)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.LIGHT_CONTROL, Convert.ToBoolean(_Value), _ID);
        }

        private void SendResultData(object _Value)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.SEND_DATA, _Value);
        }
            
        private void SetResultData(object _Value)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.SET_RESULT, _Value);
        }

        private void InspectionComplete(object _Value, int _ID)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.INSP_COMPLETE, _Value, _ID);
        }

        private void NoticeWindow(object _Value)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.NOTICE_WINDOW, _Value);
        }
        #endregion Event : Inspection Window Event

        #region Event : MapDataWindow Event Function
        private void MapDataParameterSaveEventFunction(MapDataParameter _MapDataParam, int _ID = 0)
        {
            var _InspSysManagerEvent = InspSysManagerEvent;
            _InspSysManagerEvent?.Invoke(eISMCMD.MAPDATA_SAVE, _MapDataParam, _ID);
        }
        #endregion Event : MapDataWindow Event Function

        public void TriggerOn()
        {
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("ISM {0} Trigger ON!", ID + 1));

            InspWnd.InspMode = eInspMode.TRI_INSP;
            InspWnd.IsInspectionComplete = false;
            IsThreadInspectionTrigger = true;
        }

        public void DataSend()
        {
            InspWnd.InspectionDataSend();
        }

        private void ThreadInspectionFunction()
        {
            try
            {
                while (false == IsThreadInspectionExit)
                {
                    if (true == IsThreadInspectionTrigger)
                    {
                        IsThreadInspectionTrigger = false;
                        InspWnd.IsInspectionComplete = false;
                        CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.INFO, String.Format("Vision : ISM{0} IsInspectionComplete false", ID + 1), CLogManager.LOG_LEVEL.LOW);

                        if (!IsSimulationMode)  ImageGrabSnap();
                        else                    InspWnd.IsThreadInspectionProcessTrigger = true;

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(10);
                }
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadInspectionFunction Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }
    }
}
