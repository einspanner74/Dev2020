using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;

using ParameterManager;

namespace MitsubishiCommunicationManager
{
    public partial class MitsubishiCommunicationWindow
    {
        private PLCCommunicationData  PLCCommData = new PLCCommunicationData();
        private PLCCommunicationData  PLCCommPreData = new PLCCommunicationData();

        private Thread ThreadReceiveData;
        private bool IsThreadReceiveDataExit;
            
        private bool IsAliveSignal = false;
        private bool IsReConnection = false;
        private int LiveCheckCount = 0;
        private const int LIVE_CHECK_CNT = 1000;

        /// Read PLC Monitoring Data Mapping
        /// </summary>
        /// <returns>Connection Result</returns>
        public bool ReadCommunicationDatas()
        {
            bool _Result = true;
            try
            {
                using (MemoryMappedFile _MemMapFile = MemoryMappedFile.OpenExisting("PLCMemMappedFile", MemoryMappedFileRights.ReadWrite))
                {
                    using (MemoryMappedViewStream _Stream = _MemMapFile.CreateViewStream(0, 0, MemoryMappedFileAccess.ReadWrite))
                    {
                        BinaryFormatter _Serializer = new BinaryFormatter();
                        _Serializer.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        PLCCommData = _Serializer.Deserialize(_Stream) as PLCCommunicationData;
                        if (null == PLCCommPreData.BitData) PLCCommPreData.BitData = new short[PLCCommData.BitData.Length];
                        if (null == PLCCommPreData.WordData) PLCCommPreData.WordData = new short[PLCCommData.WordData.Length];
                    }
                }
            }

            catch
            {
                _Result = false;
            }

            return _Result;
        }

        /* Bit Check 
        /// <summary>
        /// PLC Data Toggle Checking
        /// </summary>
        /// <param name="_Command"></param>
        /// <returns></returns>
        private bool PLCDataChecking(int _Command)
        {
            bool _Result = false;
            if (PLCCommData.BitData[_Command] == SIGNAL.ON)
            {
                if (PLCCommPreData.BitData[_Command] != PLCCommData.BitData[_Command])
                {
                    PLCCommData.BitData[_Command] = SIGNAL.ON;
                    PLCCommPreData.BitData[_Command] = PLCCommData.BitData[_Command];
                    _Result = true;
                }
            }

            else if (PLCCommData.BitData[_Command] == SIGNAL.OFF)
            {
                PLCCommPreData.BitData[_Command] = SIGNAL.OFF;
                _Result = false;
            }

            return _Result;
        }

        /// <summary>
        /// Bit Change Check
        /// </summary>
        /// <param name="_PTVBit"></param>
        /// <returns></returns>
        private bool PLCBitChangeChecking(int _PTVBit)
        {
            if (PLCCommData.BitData[_PTVBit] != PLCCommPreData.BitData[_PTVBit])    return true;
            else                                                                    return false;
        }
        */

        /// <summary>
        /// PLC To Vision alive check
        /// </summary>
        /// <returns></returns>
        private bool PLCToVisionAliveCheck()
        {
            bool _Result = true;

            return _Result;
        }

        //Thread
        private void ThreadReceiveDataFunc()
        {
            try
            {
                while(false == IsThreadReceiveDataExit)
                {
                    Thread.Sleep(1);
                }
            }

            catch (Exception ex)
            {

            }
        }

        private void ThreadLiveCheck()
        {
            if (false == ThreadReceiveData.IsAlive)
            {
                ThreadReceiveData = new Thread(ThreadReceiveDataFunc);
                ThreadReceiveData.IsBackground = true;
                IsThreadReceiveDataExit = false;
                ThreadReceiveData.Start();
                //CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Vision : ThreadReceiveData Re-Start");
            }
        }
    }
}
