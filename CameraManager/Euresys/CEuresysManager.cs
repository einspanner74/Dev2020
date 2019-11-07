using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Euresys.MultiCam;

using LogMessageManager;

namespace CameraManager
{
    public class CEuresysManager
    {
        public delegate void EuresysGrabHandler(byte[] GrabImageAdress);
        public event EuresysGrabHandler EuresysGrabEvent;

        // The MultiCam object that controls the acquisition
        UInt32 channel;

        // The MultiCam object that contains the acquired buffer
        private UInt32 currentSurface;

        MC.CALLBACK multiCamCallback;

        private int ImageSizeWidth;
        private int ImageSizeHeight;

        public CEuresysManager(string _CamInfo = "VCC-G20S20_P15RA")
        {
            try
            {
                // Open MultiCam driver
                MC.OpenDriver();

                // Enable error logging
                MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

                // Create a channel and associate it with the first connector on the first board
                MC.Create("CHANNEL", out channel);
                MC.SetParam(channel, "DriverIndex", 0);
                // For all Domino boards except Domino Symphony
                MC.SetParam(channel, "Connector", "X");
                // For Domino Symphony
                //MC.SetParam(channel, "Connector", "A");

                // Choose the CAM file
                //MC.SetParam(channel, "CamFile", "XC-HR50_P60RA");
                //MC.SetParam(channel, "CamFile", "VCC-G20S20_P15RA");
                //MC.SetParam(channel, "CamFile", "CV-A1_P16RA");

                if (_CamInfo == "") _CamInfo = "VCC-G20S20_P15RA";
                MC.SetParam(channel, "CamFile", _CamInfo);
                // Choose the camera expose duration
                MC.SetParam(channel, "Expose_us", 1000);
                // Choose the pixel color format
                MC.SetParam(channel, "ColorFormat", "Y8");

                // Choose the way the first acquisition is triggered
                //MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                MC.SetParam(channel, "TrigMode", "HARD");
                // Choose the triggering mode for subsequent acquisitions
                //MC.SetParam(channel, "NextTrigMode", "REPEAT");
                MC.SetParam(channel, "NextTrigMode", "SAME");
                // Choose the number of images to acquire

                //MC.SetParam(channel, "TrigLine", "NOM");
                //MC.SetParam(channel, "TrigEdge", "GOHIGH");
                //MC.SetParam(channel, "TrigFilter", "ON");
                //MC.SetParam(channel, "TrigCtl", "LVDS");

                MC.SetParam(channel, "TrigCtl", "TTL");
                MC.SetParam(channel, "TrigLine", "NOM");
                MC.SetParam(channel, "TrigEdge", "GOLOW");
                MC.SetParam(channel, "TrigFilter", "ON");

                MC.SetParam(channel, "StrobeCtl", "OPTO");
                //MC.SetParam(channel, "StrobeCtl", "TTL");
                MC.SetParam(channel, "StrobeMode", "AUTO");
                MC.SetParam(channel, "StrobeLevel", "PLSLOW");

                //MC.SetParam(channel, "SeqLength_Fr", MC.INDETERMINATE);
                MC.SetParam(channel, "SeqLength_Fr", -1);

                // Register the callback function
                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(channel, multiCamCallback, channel);

                // Enable the signals corresponding to the callback functions
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

                // Prepare the channel in order to minimize the acquisition sequence startup latency
                MC.SetParam(channel, "ChannelState", "READY");
                MC.SetParam(channel, "ChannelState", "ACTIVE");
            }
			
            catch (Euresys.MultiCamException ex)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "cEuresysManager() Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void DeInitialize()
        {
            if (channel != 0)
            {
                MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                MC.SetParam(channel, "ChannelState", "IDLE");
                MC.Delete(channel);
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

            // + DominoSnapshot Sample Program

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

                var _EuresysGrabEvent = EuresysGrabEvent;
                _EuresysGrabEvent?.Invoke(GrabImage);
                //if (_EuresysGrabEvent != null)
                //    EuresysGrabEvent(GrabImage);

                // Retrieve the frame rate
                Double frameRate_Hz;
                MC.GetParam(channel, "PerSecond_Fr", out frameRate_Hz);

                // Retrieve the channel state
                String channelState;
                MC.GetParam(channel, "ChannelState", out channelState);

            }
            catch (Euresys.MultiCamException exc)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CEuresysManager ProcessingCallback Exception!! : MultiCanException" , CLogManager.LOG_LEVEL.LOW);
            }
            catch (System.Exception exc)
            {
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "CEuresysManager Initialize Exception!! : SystemException", CLogManager.LOG_LEVEL.LOW);
            }
            // - DominoSnapshot Sample Program
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

            // - DominoSnapshot Sample Program
        }

        public void SetActive(bool LiveFlag)
        {
            MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "OFF");
            MC.SetParam(channel, "ChannelState", "IDLE");

            if (LiveFlag)
            {
                //MC.SetParam(channel, "ChannelState", "IDLE");
                MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                //MC.SetParam(channel, "NextTrigMode", "REPEAT");
                MC.SetParam(channel, "NextTrigMode", "SAME");

                String channelState;
                MC.GetParam(channel, "ChannelState", out channelState);
                if (channelState != "ACTIVE")
                    MC.SetParam(channel, "ChannelState", "ACTIVE");
            }
            else
            {
                MC.SetParam(channel, "TrigMode", "HARD");
                //MC.SetParam(channel, "NextTrigMode", "COMBINED");
                MC.SetParam(channel, "NextTrigMode", "SAME");
                //MC.SetParam(channel, "ForceTrig", "TRIG");
                String channelState2;
                MC.GetParam(channel, "ChannelState", out channelState2);
                MC.SetParam(channel, "ChannelState", "IDLE");
                MC.SetParam(channel, "ChannelState", "ACTIVE");
            }

            MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
            Thread.Sleep(50);
        }

        public void SetImageSize(int Width, int Height)
        {
            ImageSizeWidth = Width;
            ImageSizeHeight = Height;
        }
    }
}
