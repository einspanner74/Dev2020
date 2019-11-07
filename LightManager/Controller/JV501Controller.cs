using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

using LogMessageManager;

namespace LightManager
{
    class JV501Controller
    {
        private const string STX = "#";
        private const string ETX = "&";
        private const string SAV = "S";
        private const string ADJ = "A";
        private const int ON = 1;
        private const int OFF = 0;

        private SerialPort SerialLight;

        private int LightChannel = 0;

        public JV501Controller()
        {
            SerialLight = new SerialPort();
            SerialLight.BaudRate = 19200;
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
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "JV501Controller Initialize Exception!!", CLogManager.LOG_LEVEL.LOW);
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
            string _SendCommand = "";

            switch (_Command)
            {
                case LightCommand.LightOn: _SendCommand = String.Format("{0}{1}{2}{3:D3}{4}", STX, ADJ, LightChannel, ON, ETX); break;
                case LightCommand.LightOff: _SendCommand = String.Format("{0}{1}{2}{3:D3}{4}", STX, ADJ, LightChannel, OFF, ETX); break;
                case LightCommand.LightAllOn: _SendCommand = String.Format("{0}{1}{2}{3:D3}{4}", STX, ADJ, "a", ON, ETX); break;
                case LightCommand.LightAllOff: _SendCommand = String.Format("{0}{1}{2}{3:D3}{4}", STX, ADJ, "a", OFF, ETX); break;
            }

            if (true == SerialLight.IsOpen) SerialLight.Write(_SendCommand);
        }

        public void SetLightChannel(int LightNum)
        {
            LightChannel = LightNum;
        }

        public void SetLightValue(int _LightValue)
        {
            string _Command = String.Format("{0}{1}{2}{3:D3}{4}", STX, ADJ, LightChannel, _LightValue, ETX);
            if (true == SerialLight.IsOpen) SerialLight.Write(_Command);
            System.Threading.Thread.Sleep(100);

            string _Commands = String.Format("{0}{1}{2}", STX, SAV, ETX);
            if (true == SerialLight.IsOpen) SerialLight.Write(_Commands);
        }
    }
}
