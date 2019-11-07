using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InspectionSystemManager;
using ParameterManager;

namespace KPVisionInspectionFramework
{
    public class MainProcessBase
    {
        public delegate void MainProcessCommandHandler(eMainProcCmd _MainCmd, object _Value);
        public event MainProcessCommandHandler MainProcessCommandEvent;

        public virtual void Initialize(string _CommonFolderPath, bool _IsIOBoardUsable, bool _IsEthernetUsable, bool _IsMitsuCommUsable)
        {

        }

        public virtual void DeInitialize()
        {

        }

        #region DIO Window Function
        public virtual void ShowDIOWindow()
        {

        }

        public virtual bool GetDIOWindowShown()
        {
            return true;
        }

        public virtual void SetDIOWindowTopMost(bool _IsTopMost)
        {
            
        }

        public virtual void SetDIOOutputSignal(short _BitNumber, bool _Signal)
        {

        }
        #endregion DIO Window Function

        #region Ethernet Window Function
        public virtual void ShowEthernetWindow()
        {

        }
        
        public virtual bool GetEhernetWindowShown()
        {
            return true;
        }

        public virtual void SetEthernetWindowTopMost(bool _IsTopMost)
        {

        }
        #endregion Ethernet Window Function

        #region Mitsubishi Window Function
        public virtual void ShowMitsubishiWindow()
        {

        }

        public virtual bool GetMitsubishiWindowShown()
        {
            return true;
        }

        public virtual void SetMitsubishiWindowTopMost(bool _IsTopMost)
        {

        }

        public virtual void SetModeStatus(int _Addr, short _Value)
        {

        }

        public virtual void SetResult(int _Addr, short _Value)
        {

        }
        #endregion

        #region Serial Window Function
        public virtual void ShowSerialWindow()
        {

        }

        public virtual bool GetSerialWindowShown()
        {
            return true;
        }

        public virtual void SetSerialWindowTopMost(bool _IsTopMost)
        {

        }

        public virtual void SendSerialData(eMainProcCmd _SendCmd, string _SendData = "")
        {

        }
        #endregion Serial Window Function

        #region Main sequence process
        protected virtual void OnMainProcessCommand(eMainProcCmd _MainCmd, object _Value)
        {
            var _MainProcessCommandEvent = MainProcessCommandEvent;
            _MainProcessCommandEvent?.Invoke(_MainCmd, _Value);
        }

        public virtual bool TriggerOn(int _ID)
        {
            return true;
        }

        public virtual bool Reset(int _ID)
        {
            return true;
        }

        public virtual bool StatusMode(int _ID, bool _Flag)
        {
            return true;
        }

        public virtual bool DataRequest(int _ID)
        {
            return true;
        }

        public virtual bool SendResultData(SendResultParameter _ResultParam)
        {
            return true;
        }

        public virtual bool InspectionComplete(int _ID, bool _Flag)
        {
            return true;
        }

        public virtual bool AutoMode(bool _Flag)
        {
            return true;
        }
        #endregion Main sequence process
    }
}
