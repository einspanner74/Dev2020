using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

using DALSA.SaperaLT.SapClassBasic;
using DALSA.SaperaLT.Examples.NET.Utils;
using System.IO;
using LogMessageManager;

namespace CameraManager
{
    public class CGenieManager
    {
        #region Sapera Parameter Variable
        private SapAcquisition SaperaAcq = null;
        private SapAcqDevice SaperaAcqDevice = null;
        private SapTransfer SaperaTransfer = null;
        private SapBuffer SaperaBuffer = null;
        private SapLocation SaperaLocation = null;
        private MyAcquisitionParams AcqParams = null;
        #endregion Sapera Parameter Variable

        //LDH, 2017.07.14, Cam Create Dictionary Add
        static bool[] CreateCamFlag;// = new Dictionary<bool, int>(); 

        #region Image Variable
        private byte[] GrabImageArray;      //Image array
        private double FrameRate;           //Frame rate
        private bool IsGrabComplete;        //Grab Complete 확인

        private Thread ThreadContinuousGrab;
        private bool IsThreadContinuousGrabExit;
        private bool IsThreadContinuousGrabTrigger;
        private int CameraNumber;

        public delegate void GenieGrabHandler(byte[] _GrabImageArray);
        public event GenieGrabHandler GenieGrabEvent;

        private ManualResetEvent PauseEvent = new ManualResetEvent(false);

        /// <summary>
        /// 이미지 버퍼 사이즈 : Width 
        /// </summary>
        public int Width
        {
            get { return SaperaBuffer.Width; }
        }

        /// <summary>
        /// 이미지 버퍼 사이즈 : Height
        /// </summary>
        public int Height
        {
            get { return SaperaBuffer.Height; }
        }
        #endregion Image Variable

        /// <summary>
        /// CGenieCamera()
        /// </summary>
        public CGenieManager()
        {
            if (null == CreateCamFlag)
            {
                CreateCamFlag = new bool[6];
                SetCreateDic();
            }
        }

        /// <summary>
        /// 카메라 초기화 
        /// </summary>
        /// <param name="_CameraName">카메라 Server Name</param>
        /// <param name="_ResourceIndex">카메라 Resource Index</param>
        /// <param name="_ConfigFileName">카메라 CCF 파일 경로</param>
        /// <returns>성공 여부 (성공 : true / 실패 : false)</returns>
        public bool Initialize(string _CameraInfo, int _ResourceIndex, string _CameraName = null)
        {
            bool _Result = true;
            int _TryCount = 6;

            IsGrabComplete = false;

            for (int iLoopCount = 0; iLoopCount < _TryCount; ++iLoopCount)
            {
                if (CreateCamFlag[iLoopCount] == true) continue;

                string _ServerName = "Nano-M2420_" + (iLoopCount + 1);

                AcqParams = new MyAcquisitionParams();
                AcqParams.ConfigFileName = _CameraInfo;
                AcqParams.ServerName = _ServerName;
                AcqParams.ResourceIndex = _ResourceIndex;

                SaperaLocation = new SapLocation(AcqParams.ServerName, AcqParams.ResourceIndex);
                SaperaAcqDevice = new SapAcqDevice(SaperaLocation, AcqParams.ConfigFileName);
                SaperaBuffer = new SapBufferWithTrash(1, SaperaAcqDevice, SapBuffer.MemoryType.ScatterGather);
                SaperaTransfer = new SapAcqDeviceToBuf(SaperaAcqDevice, SaperaBuffer);

                //End Of Frame Event Setting
                SaperaTransfer.Pairs[0].EventType = SapXferPair.XferEventType.EndOfFrame;
                SaperaTransfer.XferNotify += new SapXferNotifyHandler(GrabEvent);
                SaperaTransfer.XferNotifyContext = SaperaBuffer;

                //Create Object
                if (!SaperaAcqDevice.Create()) { DestroyObjects(SaperaAcq, SaperaAcqDevice, SaperaBuffer, SaperaTransfer); return false; }

                string UserID;
                SaperaAcqDevice.GetFeatureValue("DeviceUserID", out UserID);

                //LJH 2018.03.27 수정
                string _SerialNumber;
                SaperaAcqDevice.GetFeatureValue("DeviceSerialNumber", out _SerialNumber);

                //LJH 2018.03.27 수정
                if (UserID != _CameraName)
                //if(_SerialNumber == _CameraName)
                {
                    DestroyObjects(SaperaAcq, SaperaAcqDevice, SaperaBuffer, SaperaTransfer);
                }
                else
                {
                    CreateCamFlag[iLoopCount] = true;
                    break;
                }
            }

            if (!SaperaBuffer.Create()) { DestroyObjects(SaperaAcq, SaperaAcqDevice, SaperaBuffer, SaperaTransfer); return false; }
            if (!SaperaTransfer.Create()) { DestroyObjects(SaperaAcq, SaperaAcqDevice, SaperaBuffer, SaperaTransfer); return false; }

            ThreadContinuousGrab = new Thread(ThreadContinuousGrabFunc);
            ThreadContinuousGrab.IsBackground = true;
            IsThreadContinuousGrabExit = false;
            IsThreadContinuousGrabTrigger = false;
            ThreadContinuousGrab.Start();
            PauseEvent.Reset();

            return _Result;
        }

        /// <summary>
        /// 카메라 설정 해제
        /// </summary>
        public void DeInitialize()
        {
            try
            {
                if (SaperaTransfer.Grabbing == true) SaperaTransfer.Freeze();
                DestroyObjects(SaperaAcq, SaperaAcqDevice, SaperaBuffer, SaperaTransfer);
                SaperaLocation.Dispose();

                if (ThreadContinuousGrab != null) { IsThreadContinuousGrabExit = true; Thread.Sleep(100); ThreadContinuousGrab.Abort(); ThreadContinuousGrab = null; }
            }
            catch
            {
            	
            }
        }

        //LDH, 2017.07.14, Dictionary Add
        private void SetCreateDic()
        {
            for (int iLoopCount = 0; iLoopCount < 5; iLoopCount++) CreateCamFlag[iLoopCount] = false;
        }

        /// <summary>
        /// 카메라 Parameter Destroy
        /// </summary>
        /// <param name="_Acq">SapAcquisition Parameter</param>
        /// <param name="_Device">SapAcqDevice Parameter</param>
        /// <param name="_Buffer">SapBuffer Parameter</param>
        /// <param name="_Transfer">SapTransfer Parameter</param>
        private void DestroyObjects(SapAcquisition _Acq, SapAcqDevice _Device, SapBuffer _Buffer, SapTransfer _Transfer)
        {
            if (_Transfer != null) { _Transfer.Destroy(); _Transfer.Dispose(); _Transfer = null; }
            if (_Device != null) { _Device.Destroy(); _Device.Dispose(); _Device = null; }
            if (_Acq != null) { _Acq.Destroy(); _Acq.Dispose(); _Acq = null; }
            if (_Buffer != null) { _Buffer.Destroy(); _Buffer.Dispose(); _Buffer = null; }
        }

        /// <summary>
        /// 카메라 Grab Event 발생 시 구동 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void GrabEvent(object sender, SapXferNotifyEventArgs args)
        {
            IsGrabComplete = false;

            if (false == SaperaTransfer.Connected) return;
            SapBuffer _GrabBuffer = args.Context as SapBuffer;

            IntPtr _BufferAddress = new IntPtr();
            _GrabBuffer.GetAddress(out _BufferAddress);

            GrabImageArray = new byte[_GrabBuffer.Width * _GrabBuffer.Height];
            System.Runtime.InteropServices.Marshal.Copy(_BufferAddress, GrabImageArray, 0, (int)(_GrabBuffer.Width * _GrabBuffer.Height));
            IsGrabComplete = true;

            SapTransfer _SapTransfer = sender as SapTransfer;
            if (_SapTransfer.UpdateFrameRateStatistics())
            {
                SapXferFrameRateInfo _Stats = _SapTransfer.FrameRateStatistics;
                FrameRate = 0.0f;

                if (_Stats.IsLiveFrameRateAvailable) FrameRate = _Stats.LiveFrameRate;
            }
        }

        /// <summary>
        /// Grab Image를 얻어온다
        /// </summary>
        /// <returns>byte[] 이미지 리턴</returns>
        public byte[] GetGrabImage()
        {
            if (false == SaperaTransfer.Connected || null == GrabImageArray) { GrabImageArray = new byte[SaperaBuffer.Width * SaperaBuffer.Height]; }

            int LoopCount = 0;
            //while (!IsGrabComplete || LoopCount > 3000)
            while(true)
            {
                if (IsGrabComplete == true || LoopCount > 3000) break;

                System.Threading.Thread.Sleep(1);
                LoopCount++;
            }

            return GrabImageArray;
        }

        /// <summary>
        /// 1 Shot Grab
        /// </summary>
        public void OneShot()
        {
            if (false == SaperaTransfer.Connected) return;
            IsGrabComplete = false;
            SaperaTransfer.Snap();

            var _GenieGrabEvent = GenieGrabEvent;
            _GenieGrabEvent?.Invoke(GetGrabImage());
        }


        /// <summary>
        /// Continues Grab
        /// </summary>
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


        public void Live()
        {
            if (false == SaperaTransfer.Connected) return;
            IsGrabComplete = false;
            SaperaTransfer.Grab();
        }

        /// <summary>
        /// Continues Grab Stop
        /// </summary>
        public void Stop()
        {
            try
            {
                if (false == SaperaTransfer.Connected) return;
                //if (null == SaperaAcq) return;
                SaperaTransfer.Freeze();
            }
            catch
            {
            	
            }
        }

        private void ThreadContinuousGrabFunc()
        {
            try
            {
                while (false == IsThreadContinuousGrabExit)
                {
                    Thread.Sleep(25);
                    if (IsThreadContinuousGrabTrigger) OneShot();
                }
            }

            catch
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CBaslerManager ThreadContinuousGrabFunc Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }
    }
}
