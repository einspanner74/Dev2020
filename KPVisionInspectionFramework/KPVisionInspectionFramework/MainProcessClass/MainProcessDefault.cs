using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InspectionSystemManager;
using LogMessageManager;
using ParameterManager;
using DIOControlManager;
using SerialManager;

namespace KPVisionInspectionFramework
{
    class MainProcessDefault : MainProcessBase
    {
        public const string CR = "cr";

        public DIOControlWindow DIOWnd;
        public SerialWindow SerialWnd;

        //LDH, Use Flag
        private bool UseSerialCommFlag = false;
        private bool UseDIOCommFlag = true;

        #region Initialize & DeInitialize
        public MainProcessDefault()
        {

        }

        public override void Initialize(string _CommonFolderPath, bool _IsIOBoardUsable, bool _IsEthernetUsable, bool _IsMitsuCommUsable)
        {
            if (UseDIOCommFlag)
            {
                DIOWnd = new DIOControlWindow((int)eProjectType.NONE, _CommonFolderPath);
                DIOWnd.InputChangedEvent += new DIOControlWindow.InputChangedHandler(InputChangeEventFunction);
                DIOWnd.Initialize();
            }

            if (UseSerialCommFlag)
            {
                SerialWnd = new SerialWindow();
                SerialWnd.SerialReceiveEvent += new SerialWindow.SerialReceiveHandler(SeraialReceiveEventFunction);
                SerialWnd.Initialize("COM1");
            }

            int _CompleteCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_COMPLETE);
            int _ReadyCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_READY);
            int _ResultCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_RESULT);
            int _LiveCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_LIVE);

            if (_CompleteCmdBit >= 0) DIOWnd.SetOutputSignal((short)_CompleteCmdBit, false);
            if (_ReadyCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ReadyCmdBit, false);
            if (_ResultCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ResultCmdBit, false);
            if (_LiveCmdBit >= 0) DIOWnd.SetOutputSignal((short)_LiveCmdBit, true);
        }

        public override void DeInitialize()
        {
            int _CompleteCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_COMPLETE);
            int _ReadyCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_READY);
            int _ResultCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_RESULT);
            int _LiveCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_LIVE);

            if (_CompleteCmdBit >= 0) DIOWnd.SetOutputSignal((short)_CompleteCmdBit, false);
            if (_ReadyCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ReadyCmdBit, false);
            if (_ResultCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ResultCmdBit, false);
            if (_LiveCmdBit >= 0) DIOWnd.SetOutputSignal((short)_LiveCmdBit, false);

            if (UseDIOCommFlag)
            {
                DIOWnd.InputChangedEvent -= new DIOControlWindow.InputChangedHandler(InputChangeEventFunction);
                DIOWnd.DeInitialize();
            }

            if (UseSerialCommFlag)
            {
                SerialWnd.SerialReceiveEvent -= new SerialWindow.SerialReceiveHandler(SeraialReceiveEventFunction);
                SerialWnd.DeInitialize();
            }
        }
        #endregion Initialize & DeInitialize

        #region DIO Window Function
        public override void ShowDIOWindow()
        {
            if(DIOWnd != null) DIOWnd.ShowDIOWindow();
        }

        public override bool GetDIOWindowShown()
        {
            return DIOWnd.IsShowWindow;
        }

        public override void SetDIOWindowTopMost(bool _IsTopMost)
        {
            DIOWnd.TopMost = _IsTopMost;
        }

        public override void SetDIOOutputSignal(short _BitNumber, bool _Signal)
        {
            DIOWnd.SetOutputSignal(_BitNumber, _Signal);
        }
        #endregion DIO Window Function

        #region Serial Window Function
        public override void ShowSerialWindow()
        {
            SerialWnd.ShowSerialWindow();
        }

        public override bool GetSerialWindowShown()
        {
            return SerialWnd.IsShowWindow;
        }

        public override void SetSerialWindowTopMost(bool _IsTopMost)
        {
            SerialWnd.TopMost = _IsTopMost;
        }
        #endregion Serial Window Function

        public override bool AutoMode(bool _Flag)
        {
            bool _Result = true;

            int _AutoCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_AUTO);
            DIOWnd.SetOutputSignal((short)_AutoCmdBit, _Flag);

            int _CompleteBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_COMPLETE);
            DIOWnd.SetOutputSignal((short)_CompleteBit, false);

            return _Result;
        }

        //LDH, 내부 Test 용. 실제로는 Camera Trigger로 동작
        public override bool TriggerOn(int _ID)
        {
            bool _Result = false;

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : Trigger{0} On Event", _ID + 1));
            OnMainProcessCommand(eMainProcCmd.TRG, _ID);

            return _Result;
        }

        public override bool Reset(int _ID)
        {
            bool _Result = false;

            int _CompleteCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_COMPLETE);
            int _ReadyCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_READY);
            int _ResultCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_RESULT);

            if (_CompleteCmdBit >= 0) DIOWnd.SetOutputSignal((short)_CompleteCmdBit, false);
            if (_ReadyCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ReadyCmdBit, false);
            if (_ResultCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ResultCmdBit, false);

            return _Result;
        }

        public override bool SendResultData(SendResultParameter _ResultParam)
        {
            bool _Result = true;

            bool _ResultFlag = _ResultParam.IsGood;
            int SendResultBit = 0;

            if (!_ResultFlag)
            {
                switch (_ResultParam.NgType)
                {
                    //case eNgType.REF_NG: SendResultBit = (short)DIOWnd.DioBaseCmd.OutputBitIndexCheck(DIO_DEF.OUT_RESULT_1); break;
                    //case eNgType.ID: SendResultBit = (short)DIOWnd.DioBaseCmd.OutputBitIndexCheck(DIO_DEF.OUT_RESULT_1); break;
                    //case eNgType.DEFECT: SendResultBit = (short)DIOWnd.DioBaseCmd.OutputBitIndexCheck(DIO_DEF.OUT_RESULT_1); break;
                }
                DIOWnd.SetOutputSignal((short)SendResultBit, true);
            }
            //InspectionComplete(0, true);

            return _Result;
        }

        #region Communication Event Function
        private void InputChangeEventFunction(short _BitNum, bool _Signal)
        {
            switch (_BitNum)
            {
                case DIO_DEF.IN_TRG: TriggerOn(0); break;
                case DIO_DEF.IN_RESET: Reset(0); break;
            }
        }

        private bool SeraialReceiveEventFunction(string _SerialData)
        {
            return true;
        }

        public override void SendSerialData(eMainProcCmd _SendCmd, string _SendData = "")
        {
            string SendBit = "";
            SerialWnd.SendSequenceData(SendBit + "," + '\r');
        }

        public override bool InspectionComplete(int _ID, bool _Flag)
        {
            bool _Result = true;
            int _CompleteCmdBit = 0;

            //_CompleteCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_COMPLETE);
            //DIOWnd.SetOutputSignal((short)_CompleteCmdBit, _Flag);

            return _Result;
        }
        #endregion Communication Event Function
    }
}
