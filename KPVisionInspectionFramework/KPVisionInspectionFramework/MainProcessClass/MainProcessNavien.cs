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
    public class MainProcessNavien : MainProcessBase
    {
        public DIOControlWindow DIOWnd;
        public SerialWindow SerialWnd;

        private bool UseSerialCommFlag = false;
        private bool UseDIOCommFlag = true;

        #region Initialize & DeInitialize
        public MainProcessNavien()
        {

        }

        public override void Initialize(string _CommonFolderPath, bool _IsIOBoardUsable, bool _IsEthernetUsable, bool _IsMitsuCommUsable)
        {
            UseDIOCommFlag = _IsIOBoardUsable;

            if (UseDIOCommFlag)
            {
                DIOWnd = new DIOControlWindow((int)eProjectType.NAVIEN, _CommonFolderPath);
                DIOWnd.InputChangedEvent += new DIOControlWindow.InputChangedHandler(InputChangeEventFunction);
                DIOWnd.Initialize();

                int _ReadyCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_READY);
                if (_ReadyCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ReadyCmdBit, true);
            }

            if (UseSerialCommFlag)
            {
                SerialWnd = new SerialWindow();
                SerialWnd.SerialReceiveEvent += new SerialWindow.SerialReceiveHandler(SeraialReceiveEventFunction);
                SerialWnd.Initialize("COM1");
            }
        }

        public override void DeInitialize()
        {
            if (UseDIOCommFlag)
            {
                int _ReadyCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_READY);
                int _ErrorCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_ERROR);
                int _GoodCmdBit = DIOWnd.DioBaseCmd.OutputBitIndexCheck((int)DIO_DEF.OUT_GOOD);

                if (_ReadyCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ReadyCmdBit, false);
                if (_ErrorCmdBit >= 0) DIOWnd.SetOutputSignal((short)_ErrorCmdBit, false);
                if (_GoodCmdBit >= 0) DIOWnd.SetOutputSignal((short)_GoodCmdBit, false);

                DIOWnd.InputChangedEvent -= new DIOControlWindow.InputChangedHandler(InputChangeEventFunction);
                DIOWnd.DeInitialize();
            }

            if (UseSerialCommFlag)
            {
                SerialWnd.SerialReceiveEvent -= new SerialWindow.SerialReceiveHandler(SeraialReceiveEventFunction);
                SerialWnd.DeInitialize();
            }
        }
        #endregion

        #region DIO Window Function
        public override void ShowDIOWindow()
        {
            if (DIOWnd != null) DIOWnd.ShowDIOWindow();
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
        #endregion

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
        #endregion

        #region Communication Event Function
        private void InputChangeEventFunction(short _BitNum, bool _Signal)
        {
            switch (_BitNum)
            {
                case DIO_DEF.IN_TRG: OnMainProcessCommand(eMainProcCmd.START, true); break;
                case DIO_DEF.IN_RESET: Reset(0); break;
            }
        }

        private bool SeraialReceiveEventFunction(string _SerialData)
        {
            return true;
        }
        #endregion

        public override bool AutoMode(bool _Flag)
        {
            bool _Result = true;

            TriggerOn(0);

            return _Result;
        }

        public override bool TriggerOn(int _ID)
        {
            bool _Result = true;

            CParameterManager.SystemMode = eSysMode.AUTO_MODE;

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : I/O Trigger{0} On Event", 1));
            OnMainProcessCommand(eMainProcCmd.TRG, 0);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format("Main : I/O Trigger{0} On Event", 2));
            OnMainProcessCommand(eMainProcCmd.TRG, 1);
            return _Result;
        }

        public override bool Reset(int _ID)
        {
            bool _Result = true;

            return _Result;
        }

        public override bool StatusMode(int _ID, bool _Flag)
        {
            DIOWnd.SetOutputSignal((short)_ID, _Flag);

            if (NavienCmd.OUT_ERROR == _ID && _Flag) DIOWnd.SetOutputSignal(3, true, 250);
            return true;
        }
    }
}
