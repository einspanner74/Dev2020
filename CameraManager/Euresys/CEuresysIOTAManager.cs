using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using Euresys.MultiCam;

using LogMessageManager;

namespace CameraManager
{
    class CEuresysIOTAManager
    {
        public delegate void EuresysGrabHandler(byte[] GrabImageAdress);
        public event EuresysGrabHandler EuresysGrabEvent;

        // The Mutex object that will protect image objects during processing
        private static Mutex imageMutex = new Mutex();

        // The MultiCam object that controls the acquisition
        UInt32 channel;

        // The MultiCam object that contains the acquired buffer
        private UInt32 currentSurface;

        MC.CALLBACK multiCamCallback;

        private int ImageSizeWidth;
        private int ImageSizeHeight;

        public CEuresysIOTAManager()
        {
            try
            {
                // Open MultiCam driver
                MC.OpenDriver();

                // Enable error logging
                MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");


                MC.Create("CHANNEL", out channel);

                // Domino board 의 채널번호
                MC.SetParam(channel, "DriverIndex", 0);

                // Domino Iota Board는 'X' channel만 존재.
                MC.SetParam(channel, "Connector", "X");

                // camfile 을 불러온다
                //MC.SetParam(channel, "CamFile", "STC-A33A_P60SA");
                MC.SetParam(channel, "CamFile", "CV-A1_P16RA");

                // Sequence를 무제한으로 설정해야 Frame을 지속적으로 받을 수 있음.
                MC.SetParam(channel, "SeqLength_Fr", MC.INDETERMINATE);
                MC.SetParam(channel, "TrigMode", "HARD");
                MC.SetParam(channel, "TrigEdge", "GOLOW");
                //MC.SetParam(channel, "StrobeLevel", "PLSLOW");
                MC.SetParam(channel, "StrobeMode", "AUTO");

                // CallBack 함수등록
                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(channel, multiCamCallback, channel);

                // 콜백 함수에 시크널 On 신호
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

            }
            catch (Euresys.MultiCamException exc)
            {
                // An exception has occurred in the try {...} block. 
                // Retrieve its description and display it in a message box.
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "cEuresysIOTAManager() Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void DeInitialize()
        {
            if (channel != 0)
            {
                // Close MultiCam driver
                MC.CloseDriver();
            }
        }

        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;
                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;
                default:
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            currentSurface = signalInfo.SignalInfo;

            // + GrablinkSnapshot Sample Program

            try
            {
                // Update the image with the acquired image buffer data 
                Int32 width, height, bufferPitch;
                IntPtr bufferAddress;
                MC.GetParam(currentChannel, "ImageSizeX", out width);
                MC.GetParam(currentChannel, "ImageSizeY", out height);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                MC.GetParam(currentSurface, "SurfaceAddr", out bufferAddress);

                byte[] GrabImage = new byte[width * height];
                Marshal.Copy(bufferAddress, GrabImage, 0, GrabImage.Length);

                try
                {
                    imageMutex.WaitOne();

                    var _EuresysGrabEvent = EuresysGrabEvent;
                    _EuresysGrabEvent?.Invoke(GrabImage);
                }
                finally
                {
                    imageMutex.ReleaseMutex();
                }

                // Retrieve the frame rate
                Double frameRate_Hz;
                MC.GetParam(channel, "PerSecond_Fr", out frameRate_Hz);

                // Retrieve the channel state
                String channelState;
                MC.GetParam(channel, "ChannelState", out channelState);

            }
            catch (Euresys.MultiCamException exc)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "cEuresysIOTAManager() ProcessingCallback Exception!! : MultiCanException", CLogManager.LOG_LEVEL.LOW);
            }
            catch (System.Exception exc)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "cEuresysIOTAManager() System Exception!! : SystemException", CLogManager.LOG_LEVEL.LOW);
            }
            // - GrablinkSnapshot Sample Program
        }

        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            // + DominoSnapshot Sample Program

            try
            {

            }
            catch (System.Exception exc)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CEuresysManager AcqFailureCallback Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void SetActive(bool LiveFlag)
        {
            String channelState;

            MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "OFF");
            MC.SetParam(channel, "ChannelState", "IDLE");

            if (LiveFlag)
            {
                //MC.SetParam(channel, "ChannelState", "IDLE");
                MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                //MC.SetParam(channel, "NextTrigMode", "REPEAT");
                MC.SetParam(channel, "NextTrigMode", "SAME");

                MC.GetParam(channel, "ChannelState", out channelState);
                if (channelState != "ACTIVE")
                    MC.SetParam(channel, "ChannelState", "ACTIVE");
            }
            else
            {
                if (channel != 0)
                {
                    MC.SetParam(channel, "TrigMode", "HARD");
                    //MC.SetParam(channel, "NextTrigMode", "COMBINED");
                    MC.SetParam(channel, "NextTrigMode", "SAME");
                    //MC.SetParam(channel, "ForceTrig", "TRIG");

                    MC.SetParam(channel, "ChannelState", "IDLE");
                    MC.SetParam(channel, "ChannelState", "ACTIVE");
                }
            }

            MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");

        }

        public void SetImageSize(int Width, int Height)
        {
            ImageSizeWidth = Width;
            ImageSizeHeight = Height;
        }
    }
}
