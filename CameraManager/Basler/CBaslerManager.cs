using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PylonC.NET;
using LogMessageManager;

namespace CameraManager
{
    public class CBaslerManager
    {
        public static uint AvailableDeviceCount;
        public static uint DeviceCount;

        private PYLON_DEVICE_HANDLE DeviceHandle;
        private PylonBuffer<Byte> GrabBuffer;
        public bool IsAvailable;

        private Thread ThreadContinuousGrab;
        private bool IsThreadContinuousGrabExit;
        private bool IsThreadContinuousGrabTrigger;
        private int CameraNumber;

        //public delegate void BaslerGrabHandler(IntPtr _GrabImageAdress);
        //public event BaslerGrabHandler BaslerGrabEvent;

        public delegate void BaslerGrabHandler(byte[] _GrabImageArray);
        public event BaslerGrabHandler BaslerGrabEvent;

        private ManualResetEvent PauseEvent = new ManualResetEvent(false);

        private object GrabLock = new object();
        public CBaslerManager()
        {
            Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "3000");
            Pylon.Initialize();

            AvailableDeviceCount = Pylon.EnumerateDevices();
        }

        public bool Initialize(int _ID, string _DeviceID)
        {
            bool _Result = false;
            if (0 == AvailableDeviceCount)   return false;
            if (_ID >= AvailableDeviceCount) return false;

            CameraNumber = _ID;
            for (int iLoopCount = 0; iLoopCount < AvailableDeviceCount; ++iLoopCount)
            {
                try
                {
                    DeviceHandle = new PYLON_DEVICE_HANDLE();
                    DeviceHandle = Pylon.CreateDeviceByIndex((uint)iLoopCount);

                    Pylon.DeviceOpen(DeviceHandle, Pylon.cPylonAccessModeControl | Pylon.cPylonAccessModeStream);
                    IsAvailable = Pylon.DeviceFeatureIsAvailable(DeviceHandle, "EnumEntry_PixelFormat_Mono8");
                    if (false == IsAvailable) { DestroyDeviceHandle(); return false; }

                    string _DeviceIDTemp = Pylon.DeviceFeatureToString(DeviceHandle, "DeviceID");
                    if (_DeviceID != _DeviceIDTemp) { DestroyDeviceHandle(); continue; }
                    Pylon.DeviceFeatureFromString(DeviceHandle, "PixelFormat", "Mono8");

                    IsAvailable = Pylon.DeviceFeatureIsAvailable(DeviceHandle, "EnumEntry_TriggerSelector_AcquisitionStart");
                    if (false == IsAvailable) { DestroyDeviceHandle(); return false; }
                    Pylon.DeviceFeatureFromString(DeviceHandle, "TriggerSelector", "AcquisitionStart");
                    Pylon.DeviceFeatureFromString(DeviceHandle, "TriggerMode", "Off");

                    _Result = true;
                    break;
                }

                catch(Exception ex)
                {
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CBaslerManager Initialize Exception!!", CLogManager.LOG_LEVEL.LOW);
                    _Result = false;
                }
            }

            if (false == _Result) return false;

            ThreadContinuousGrab = new Thread(ThreadContinuousGrabFunc);
            ThreadContinuousGrab.IsBackground = true;
            IsThreadContinuousGrabExit = false;
            IsThreadContinuousGrabTrigger = false;
            ThreadContinuousGrab.Start();
            PauseEvent.Reset();

            return true;
        }

        public void DeInitialize()
        {
            DestroyDeviceHandle();
            ThreadDeInitialize();
        }

        public void ThreadDeInitialize()
        {
            if (ThreadContinuousGrab != null) { IsThreadContinuousGrabExit = true; Thread.Sleep(100); ThreadContinuousGrab.Abort(); ThreadContinuousGrab = null; }
        }

        private void DestroyDeviceHandle()
        {
            Pylon.DeviceClose(DeviceHandle);
            Pylon.DestroyDevice(DeviceHandle);
        }

        public void OneShot()
        {
            lock (GrabLock)
            {
                PylonGrabResult_t _GrabResult;
                bool _Result = Pylon.DeviceGrabSingleFrame(DeviceHandle, 0, ref GrabBuffer, out _GrabResult, 500);

                var _BaslerGrabEvent = BaslerGrabEvent;
                _BaslerGrabEvent?.Invoke(GrabBuffer.Array);
            }
        }
        public void Continuous(bool _Live)
        {
            if (_Live)
            {
                PauseEvent.Set(); //Thread 재시작
                IsThreadContinuousGrabTrigger = _Live;
            }

            else
            {
                IsThreadContinuousGrabTrigger = _Live;
                PauseEvent.Reset(); //Thread 일시정지

                Thread.Sleep(100);
            }
        }

        private void ThreadContinuousGrabFunc()
        {
            try
            {
                while (false == IsThreadContinuousGrabExit)
                {
                    if (IsThreadContinuousGrabTrigger)  OneShot();
                    Thread.Sleep(25);
                }
            }

            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CBaslerManager ThreadContinuousGrabFunc Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }
    }
}
