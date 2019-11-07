using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using LogMessageManager;
using ParameterManager;
using MitsubishiCommunicationManager;

namespace KPVisionInspectionFramework
{
    public class MainProcessSolarAlign : MainProcessBase
    {
        public MitsubishiCommunicationWindow MitsuCommWnd;

        private bool UseSerialCommFlag = false;
        private bool UseDIOCommFlag = false;
        private bool UseMitsuCommFlag = true;

        private Thread ThreadVisionAlive;
        private bool IsThreadVisionAliveExit;

        private Thread ThreadPLCAliveCheck;
        private bool IsThreadPLCAliveCheckExit;

        private Thread ThreadMitsuInterfaceCheck;
        private bool   IsThreadMitsuInterfaceCheckExit;

        private bool IsPLCAliveSignal    = false;
        private int  AliveValuePre       = 0;
        private int  LiveCheckCount      = 0;
        private const int LIVE_CHECK_CNT = 500;

        private int Vision1Status       = PTVDefine.V_STATUS_NOT;
        private bool Vision1Trigger     = false;
        private int Vision1RetryCount   = 0;

        private int Vision2Status = PTVDefine.V_STATUS_NOT;
        private bool Vision2Trigger = false;
        private int Vision2RetryCount = 0;

        #region Initialize & DeInitialize
        public MainProcessSolarAlign()
        {

        }

        public override void Initialize(string _CommonFolderPath, bool _IsIOBoardUsable, bool _IsEthernetUsable, bool _IsMitsuCommUsable)
        {
            UseMitsuCommFlag = _IsMitsuCommUsable;

            if (UseMitsuCommFlag)
            {
                MitsuCommWnd = new MitsubishiCommunicationWindow();
                MitsuCommWnd.Initialize((int)eProjectType.SCALIGN, _CommonFolderPath);

                ThreadVisionAlive = new Thread(ThreadVisionAliveFunc);
                ThreadVisionAlive.IsBackground = true;
                IsThreadVisionAliveExit = false;
                ThreadVisionAlive.Start();

                ThreadPLCAliveCheck = new Thread(ThreadPLCAliveCheckFunc);
                ThreadPLCAliveCheck.IsBackground = true;
                IsThreadPLCAliveCheckExit = false;
                ThreadPLCAliveCheck.Start();

                ThreadMitsuInterfaceCheck = new Thread(ThreadMitsuInterfaceCheckFunc);
                ThreadMitsuInterfaceCheck.IsBackground = true;
                IsThreadMitsuInterfaceCheckExit = false;
                ThreadMitsuInterfaceCheck.Start();
            }
        }

        public override void DeInitialize()
        {
            if (ThreadMitsuInterfaceCheck != null)  { IsThreadMitsuInterfaceCheckExit = true;   Thread.Sleep(100);  ThreadMitsuInterfaceCheck = null; }
            if (ThreadPLCAliveCheck != null)        { IsThreadPLCAliveCheckExit = true;         Thread.Sleep(100);  ThreadPLCAliveCheck = null; }
            if (ThreadVisionAlive != null)          { IsThreadVisionAliveExit = true;           Thread.Sleep(100);  ThreadVisionAlive = null; }
        }
        #endregion Initialize & DeInitialize

        #region Mitsubishi Communication Window Function
        public override void ShowMitsubishiWindow()
        {
            if (MitsuCommWnd != null) MitsuCommWnd.ShowMitsuCommWindow();
        }

        public override bool GetMitsubishiWindowShown()
        {
            return MitsuCommWnd.IsShowMitsuCommWindow;
        }

        public override void SetMitsubishiWindowTopMost(bool _IsTopMost)
        {
            MitsuCommWnd.TopMost = _IsTopMost;
        }
        #endregion

        #region Mitsubishi Communication Event Fuction
        public override void SetModeStatus(int _Addr, short _Value)
        {
            MitsuCommWnd.SetVTPWordData(_Addr, _Value);
        }

        public override void SetResult(int _Addr, short _Value)
        {
            MitsuCommWnd.SetVTPWordData(_Addr, _Value); 
        }

        private void ThreadVisionAliveFunc()
        {
            int _AliveCount = 32767;
            try
            {
                while (false == IsThreadVisionAliveExit)
                {
                    _AliveCount = MitsuCommWnd.GetVTPWordData(VTPAddr.ALIVE);
                    _AliveCount = _AliveCount + 1;
                    if (_AliveCount > 32767) _AliveCount = 0;

                    MitsuCommWnd.SetVTPWordData(VTPAddr.ALIVE, (short)_AliveCount);
                    Thread.Sleep(250);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void ThreadPLCAliveCheckFunc()
        {
            try
            {
                while (false == IsThreadPLCAliveCheckExit)
                {
                    PLCToVisionAliveCheck();
                    Thread.Sleep(10);
                }
            }

            catch
            {

            }
        }

        private void PLCToVisionAliveCheck()
        {
            short _AliveValue = MitsuCommWnd.GetPTVWordData(PTVAddr.ALIVE);

            if (_AliveValue != AliveValuePre)
            {
                LiveCheckCount = 0;
                IsPLCAliveSignal = true;
                AliveValuePre = _AliveValue;
            }

            else
            {
                LiveCheckCount++;
                if (LiveCheckCount > LIVE_CHECK_CNT)
                {
                    LiveCheckCount = 0;
                    IsPLCAliveSignal = false;
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.WARN, "PLC -> Vision : Connection Fail", CLogManager.LOG_LEVEL.LOW);
                }
            }
        }

        private void ThreadMitsuInterfaceCheckFunc()
        {
            try
            {
                while(false == IsThreadMitsuInterfaceCheckExit)
                {
                    Thread.Sleep(5);
                    //if (false == MitsuCommWnd.ReadCommunicationDatas()) continue;
                    if (false == ResetInspectionStatus()) continue;

                    if (true == Vision1InspectionRequestCheck()) InspectionRequest(1);
                    if (true == Vision2InspectionRequestCheck()) InspectionRequest(2);

                    MitsuCommWnd.UpdateVisionData();
                }
            }

            catch (Exception ex)
            {

            }
        }

        private bool ResetInspectionStatus()
        {
            bool _Result = true;

            Vision1Status = PTVDefine.V_STATUS_NOT;
            Vision2Status = PTVDefine.V_STATUS_NOT;

            return _Result;
        }

        private bool ReceiveResetRequest(int _StageNumber)
        {
            if (_StageNumber == 1)
            {

            }

            else if (_StageNumber == 2)
            {

            }

            return true;
        }

        private bool Vision1InspectionRequestCheck()
        {
            int _InspReq = MitsuCommWnd.GetPTVWordData(PTVAddr.V1_INSP_REQ);
            int _RetryCnt = MitsuCommWnd.GetPTVWordData(PTVAddr.V1_RETRY_CNT);

            //if (_InspReq != PTVDefine.V_STATUS_CAL && _InspReq != PTVDefine.V_STATUS_INSP) return false;

            //Insp Request / Cal Reques가 발생
            if (true == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V1_INSP_REQ))
            {
                if (_InspReq == PTVDefine.V_STATUS_INSP)        {   Vision1Status = PTVDefine.V_STATUS_INSP;    Vision1Trigger = true;  Vision1RetryCount = 0;  }
                else if (_InspReq == PTVDefine.V_STATUS_CAL)    {   Vision1Status = PTVDefine.V_STATUS_CAL;     Vision1Trigger = true;  Vision1RetryCount = 0;  }
                else if (_InspReq == PTVDefine.V_STATUS_NOT)    {   Vision1Status = PTVDefine.V_STATUS_CLEAR;   Vision1Trigger = false; ReceiveResetRequest(1); }
            }

            //Request가 유지된 상태
            else if (false == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V1_INSP_REQ) && (_InspReq == PTVDefine.V_STATUS_INSP || _InspReq == PTVDefine.V_STATUS_CAL))
            {
                //Retry Count 가 변함.
                if (_RetryCnt > Vision1RetryCount)              {   Vision1Status = PTVDefine.V_STATUS_INSP;     Vision1Trigger = true;     Vision1RetryCount = _RetryCnt; }
            }

            return Vision1Trigger;
        }

        private bool Vision2InspectionRequestCheck()
        {
            int _InspReq = MitsuCommWnd.GetPTVWordData(PTVAddr.V2_INSP_REQ);
            int _RetryCnt = MitsuCommWnd.GetPTVWordData(PTVAddr.V2_RETRY_CNT);

            //if (_InspReq == PTVDefine.V_STATUS_CAL) return false;

            //Insp Request / Cal Reques가 발생
            if (true == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V2_INSP_REQ))
            {
                if (_InspReq == PTVDefine.V_STATUS_INSP)        {   Vision2Status = PTVDefine.V_STATUS_INSP;    Vision2Trigger = true;          Vision2RetryCount = 0; }
                else if (_InspReq == PTVDefine.V_STATUS_CAL)    {   Vision2Status = PTVDefine.V_STATUS_CAL;     Vision2Trigger = true;          Vision2RetryCount = 0; }
                else                                            {   Vision2Status = PTVDefine.V_STATUS_CLEAR;   Vision2Trigger = false;         ReceiveResetRequest(2); }
            }

            //Request가 유지된 상태
            else if (false == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V2_INSP_REQ) && _InspReq == PTVDefine.V_STATUS_INSP)
            {
                //Retry Count 가 변함.
                if (_RetryCnt > Vision2RetryCount)              {   Vision2Status = PTVDefine.V_STATUS_INSP;     Vision2Trigger = true;         Vision2RetryCount = _RetryCnt; }
            }

            return Vision1Trigger;
        }

        private bool Vision1CalibrationRequestCheck()
        {
            int _InspReq = MitsuCommWnd.GetPTVWordData(PTVAddr.V1_INSP_REQ);
            int _RetryCnt = MitsuCommWnd.GetPTVWordData(PTVAddr.V1_RETRY_CNT);

            if (_InspReq == PTVDefine.V_STATUS_INSP) return false;

            //Cal Request가 발생
            if (true == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V1_INSP_REQ))
            {
                if (_InspReq == PTVDefine.V_STATUS_CAL)      { Vision1Status = PTVDefine.V_STATUS_CAL;   Vision1Trigger = true;     Vision1RetryCount = 0; }
                else if (_InspReq == PTVDefine.V_STATUS_NOT) { Vision1Status = PTVDefine.V_STATUS_CLEAR; Vision1Trigger = false;    ReceiveResetRequest(1); }
            }

            else if (false == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V1_INSP_REQ) && _InspReq == PTVDefine.V_STATUS_CAL)
            {
                if (_RetryCnt > Vision1RetryCount)          { Vision1Status = PTVDefine.V_STATUS_CAL;    Vision1Trigger = true;     Vision1RetryCount = _RetryCnt; }
            }

            return Vision1Trigger;
        }

        private bool Vision2CalibrationRequestCheck()
        {
            int _InspReq = MitsuCommWnd.GetPTVWordData(PTVAddr.V2_INSP_REQ);
            int _RetryCnt = MitsuCommWnd.GetPTVWordData(PTVAddr.V2_RETRY_CNT);

            if (_InspReq == PTVDefine.V_STATUS_INSP) return false;

            //Cal Request가 발생
            if (true == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V2_INSP_REQ))
            {
                if (_InspReq == PTVDefine.V_STATUS_CAL)      { Vision2Status = PTVDefine.V_STATUS_CAL;   Vision2Trigger = true;     Vision2RetryCount = 0; }
                else if (_InspReq == PTVDefine.V_STATUS_NOT) { Vision2Status = PTVDefine.V_STATUS_CLEAR; Vision2Trigger = false;    ReceiveResetRequest(2); }
            }

            else if (false == MitsuCommWnd.PTVWordDataChangeCheck(PTVAddr.V2_INSP_REQ) && _InspReq == PTVDefine.V_STATUS_CAL)
            {
                if (_RetryCnt > Vision1RetryCount)          { Vision2Status = PTVDefine.V_STATUS_CAL;    Vision1Trigger = true;     Vision2RetryCount = _RetryCnt; }
            }

            return Vision2Trigger;
        }
        #endregion

        #region Inspection Process
        /// <summary>
        /// Inspection Request & Retry Count Check
        /// </summary>
        /// <param name="_StageNumber"></param>
        /// <returns></returns>
        private bool InspectionRequest(int _StageNumber)
        {
            bool _Result = true;

            do
            {
                if (false == InspectionResultClear()) break;
                if (false == InspectionTriggerOn(_StageNumber)) break;
            } while (false);

            return _Result;
        }

        private bool InspectionResultClear()
        {
            return true;
        }

        private bool InspectionTriggerOn(int _StageNumber)
        {
            if (_StageNumber == 1)
            {
                if (Vision1Status == PTVDefine.V_STATUS_INSP)
                {
                    for (int iLoopCount = 0; iLoopCount < 2; ++iLoopCount)
                    {
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("MainProces : Mitsu Comm Inspection Request{0} On Event", iLoopCount + 1));
                        OnMainProcessCommand(eMainProcCmd.TRG, (2 * iLoopCount)); 
                    }
                }

                else if (Vision1Status == PTVDefine.V_STATUS_CAL)
                {
                    for (int iLoopCount = 0; iLoopCount < 2; ++iLoopCount)
                    {
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("MainProces : Mitsu Comm Calibration Request{0} On Event", iLoopCount + 1));
                        OnMainProcessCommand(eMainProcCmd.CAL, (2 * iLoopCount)); 
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
 