using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

using LogMessageManager;

namespace LightManager
{
    class LightController
    {
        private const string STX = "#";
        private const string ETX = "&";
        private const string SAV = "S";
        private const string ADJ = "A";
        private const int ON = 1;
        private const int OFF = 0;

        private SerialPort SerialLight;

        private int LightChannel = 0;
        private int[] LightValues;

        public LightController()
        {
            SerialLight = new SerialPort();
            SerialLight.BaudRate = 9600;
            SerialLight.DataBits = (int)8;
            SerialLight.Parity = Parity.None;
            SerialLight.StopBits = StopBits.One;
            SerialLight.ReadTimeout = (int)500;
            SerialLight.WriteTimeout = (int)500;
        }

        public bool Initialize(string _PortName)
        {
            bool _Result = true;

            SerialLight.PortName = _PortName;

            try
            {
                SerialLight.Open();
            }
            catch
            {
                _Result = false;
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "LightController Initialize Exception!!", CLogManager.LOG_LEVEL.LOW);
            }

            return _Result;
        }

        public void DeInitialize()
        {
            if (SerialLight != null && SerialLight.IsOpen)
            {
                SerialLight.Close();
                SerialLight.Dispose();
                SerialLight = null;
            }
        }

        public void SetCommand(LightCommand _Command)
        {
            switch (_Command)
            {
                case LightCommand.LightOn: SetPacket("TI" + LightChannel); break;
                case LightCommand.SaveValue: WriteEEPROM(); break;
            }
        }

        private void SetPacket(string _OutputCommand)
        {
            char[] OutputCommand = _OutputCommand.ToCharArray();

            byte[] PacketTemp = new byte[128];
            byte BLRC = 0;
            int idx = 0;

            PacketTemp[idx++] = 0x02;
            for (int iLoopCount = 0; iLoopCount < OutputCommand.Count(); iLoopCount++)
            {
                PacketTemp[idx++] = Convert.ToByte(OutputCommand[iLoopCount]);
            }
            PacketTemp[idx++] = 0x03;

            for (int iLoopCount = 0; iLoopCount < idx; iLoopCount++)
            {
                BLRC ^= PacketTemp[iLoopCount];
            }
            PacketTemp[idx++] = BLRC;

            SerialLight.Write(PacketTemp, 0, idx);
        }

        public void SetLightChannel(int LightNum)
        {
            LightChannel = LightNum + 1;
        }

        public void SetLightValue(int _LightValue)
        {
            string LightValueTemp = _LightValue.ToString().PadLeft(5, '0');
            SetPacket("TO" + LightChannel + LightValueTemp);
            System.Threading.Thread.Sleep(100);
        }

        public void SetLightValue(int[] _LightValues)
        {
            LightValues = new int[_LightValues.Count()];

            for (int iLoopCount = 0; iLoopCount < _LightValues.Count(); iLoopCount++)
            {
                LightValues[iLoopCount] = _LightValues[iLoopCount];
            }
        }

        private void WriteEEPROM()
        {
            int CurrentConfigurationMode = 1;
            string LightValueTemp = "W" + CurrentConfigurationMode;
            for (int iLoopCount = 0; iLoopCount < LightValues.Count(); iLoopCount++)
            {
                LightValueTemp = LightValueTemp + LightValues[iLoopCount].ToString().PadLeft(11, '0');
            }

            // 사용하는 조명이 4개 이하일 경우 CH4까지 나머지 0으로 채우기 
            for (int iLoopCount = 0; iLoopCount < 4 - LightValues.Count(); iLoopCount++)
            {
                int LightZero = 0;
                LightValueTemp = LightValueTemp + LightZero.ToString().PadLeft(11, '0');
            }
            SetPacket(LightValueTemp);
            System.Threading.Thread.Sleep(100);
        }
    }
}
