using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;   

using LogMessageManager;

namespace EthernetManager
{
    public partial class EthernetWindow : Form
    {
        public bool IsShowWindow = false;

        private string CommonFolderPath = "";

        private char STX = (char)0x02;
        private char ETX = (char)0x03;

        private Queue<string> CommandQueue = new Queue<string>();

        private CEthernetManager ClientSock;
        private string IPAddress = "192.168.0.1";
        private short PortNumber = 5050;

        private bool IsConnected = false;
        private bool IsTryConnect = true;
        private int RetryCount = 0;

        private Timer ConnectCheckTimer;

        public delegate void ReceiveStringHandler(string[] _ReceiveMsasage);
        public event ReceiveStringHandler ReceiveStringEvent;

        #region Initialize & DeInitialize
        public EthernetWindow()
        {
            InitializeComponent();
        }

        #region Read & Write Ethernet Information
        private XmlNodeList GetNodeList(string _XmlFilePath)
        {
            XmlNodeList _XmlNodeList = null;

            try
            {
                XmlDocument _XmlDocument = new XmlDocument();
                _XmlDocument.Load(_XmlFilePath);
                XmlElement _XmlRoot = _XmlDocument.DocumentElement;
                _XmlNodeList = _XmlRoot.ChildNodes;
            }

            catch
            {
                _XmlNodeList = null;
            }

            return _XmlNodeList;
        }

        private void ReadEthernetInfoFile()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(@CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _EthernetInfoFileName = string.Format(@"{0}EthernetInformation.xml", CommonFolderPath);
            if (false == File.Exists(_EthernetInfoFileName))
            {
                File.Create(_EthernetInfoFileName).Close();
                WriteEthernetInfoFile();
                System.Threading.Thread.Sleep(100);
            }

            else
            {
                XmlNodeList _XmlNodeList = GetNodeList(_EthernetInfoFileName);
                if (null == _XmlNodeList) return;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return;
                    switch(_Node.Name)
                    {
                        case "IPAddress":   IPAddress = _Node.InnerText; break;
                        case "PortNumber":  PortNumber = Convert.ToInt16(_Node.InnerText);  break;
                    }
                }
            }
        }

        private void WriteEthernetInfoFile()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(@CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _EthernetInfoFileName = string.Format(@"{0}EthernetInformation.xml", CommonFolderPath);
            XmlTextWriter _XmlWriter = new XmlTextWriter(_EthernetInfoFileName, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("EthernetInformation");
            {
                _XmlWriter.WriteElementString("IPAddress", IPAddress);
                _XmlWriter.WriteElementString("PortNumber", PortNumber.ToString());
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write Ethernet Information

        public void Initialize(string _CommonFolderPath)
        {
            CommonFolderPath = _CommonFolderPath;

            ReadEthernetInfoFile();

            textBoxIPAddress.Text = IPAddress;
            textBoxPortNumber.Text = PortNumber.ToString();

            ConnectCheckTimer = new Timer();
            ConnectCheckTimer.Tick += ConnectCheckTimer_Tick;
            ConnectCheckTimer.Interval = 350;
            ConnectCheckTimer.Start();

            ClientSock = new CEthernetManager();
            ClientSock.RecvMessageEvent += new CEthernetManager.RecvMessageHandler(UpdateReceiveString);
            ClientSock.SendMessageEvent += new CEthernetManager.SendMessageHandler(UpdateSendString);

            ClientSock.Initialize(IPAddress, PortNumber);
        }

        public void DeInitialize()
        {
            ConnectCheckTimer.Stop();

            ClientSock.RecvMessageEvent -= new CEthernetManager.RecvMessageHandler(UpdateReceiveString);
            ClientSock.SendMessageEvent -= new CEthernetManager.SendMessageHandler(UpdateSendString);
            ClientSock.DeInitialize();
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void EthernetWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion Control Default Event

        #region Button Event
        public void ShowEthernetWindow()
        {
            textBoxIPAddress.Text = IPAddress;
            textBoxPortNumber.Text = PortNumber.ToString();

            IsShowWindow = true;
            this.Show();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress = textBoxIPAddress.Text;
            PortNumber = Convert.ToInt16(textBoxPortNumber.Text);

            IsTryConnect = true;
            ClientSock.DeInitialize();
            ClientSock.Initialize(IPAddress, PortNumber);
            ClientSock.Connection();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            IsTryConnect = false;
            ClientSock.DisConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (false == IsConnected) return;

            ClientSock.Send(textBoxManualData.Text);
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            int _Type = 1;
            string _Message = String.Format("{0},{1},{2},{3},{4},{5},{6}", STX, _Type, 0.05, 0.02, 0.03, 0.01, ETX);

            //ASCII Value to String
            //byte[] _SendAscii = Encoding.ASCII.GetBytes(_SendProtocol);
            //string asciiString = Encoding.ASCII.GetString(_SendAscii);
            //string asciiString2 = asciiString;

            UpdateReceiveString(_Message);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsShowWindow = false;
            this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            WriteEthernetInfoFile();

            IsShowWindow = false;
            this.Hide();
        }
        #endregion Button Event

        #region "Control Invoke"
        /// <summary>
        /// 컨트롤 Text 입력 Invoke
        /// </summary>
        /// <param name="_Control">Control </param>
        /// <param name="_msg">text</param>
        private void ControlInvoke(Control _Control, string _Msg)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate()
                {
                    _Control.Text = _Msg;
                }
                ));
            }
            else
            {
                _Control.Text = _Msg;
            }
        }

        /// <summary>
        /// 컨트롤 BackColor 변경 Invoke
        /// </summary>
        /// <param name="_Control">Control </param>
        /// <param name="_Color">변경 색</param>
        private void ControlInvoke(Control _Control, Color _Color)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate()
                {
                    _Control.BackColor = _Color;
                }
                ));
            }
            else
            {
                _Control.BackColor = _Color;
            }
        }
        #endregion

        #region Connecting Check Timer
        private void ConnectCheckTimer_Tick(object sender, EventArgs e)
        {
            IsConnected = ClientSock.IsAvailableTCPPort(IsTryConnect);
            if (true == IsConnected) ControlInvoke(picConnection, Color.Green);
            else                     ControlInvoke(picConnection, Color.Red);

            RetryCount++;
            if (true == IsConnected && RetryCount >= 50) RetryCount = 0;
        }
        #endregion Connecting Check Timer

        #region Ethernet Window -> Socket Class(EthernetManager) Send Command
        public void SendResultData(string _ResultDataString)
        {
            if (false == IsConnected) return;

            ClientSock.Send(_ResultDataString);
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Send Data : " + _ResultDataString);
        }
        #endregion Ethernet Window -> Socket Class(EthernetManager) Send Command

        #region Socket Class(EthernetManager) => Ethernet Window Event
        public void UpdateSendString(string _SendMessage)
        {
            ControlInvoke(textBoxSendString, _SendMessage);
        }

        public void UpdateReceiveString(string _RecvMessage)
        {
            //ㄱ,1,0.05,0.02,0.03,0.01,ㄴ
            string _RecvString = _RecvMessage.Trim();
            _RecvString = _RecvString.Replace(" ", "");
            string[] _RecvCmd = _RecvString.Split(',');

            for (int iLoopCount = 0; iLoopCount < _RecvCmd.Length; ++iLoopCount) CommandQueue.Enqueue(_RecvCmd[iLoopCount]);

            if (true == ProtocolCommandProcess())
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Receive Data : " + _RecvString);
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "ProtocolCommandProcess : Good");
            }

            else
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Receive Data : " + _RecvString);
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "ProtocolCommandProcess : Bad");
            }

            ControlInvoke(textBoxRecvString, _RecvString);
        }

        private bool ProtocolCommandProcess()
        {
            if (CommandQueue.Contains(STX.ToString()) == false) return false;
            if (CommandQueue.Contains(ETX.ToString()) == false) return false;
            if (CommandQueue.Count < 7) return false;

            string _Data = "";
            while (_Data != STX.ToString()) _Data = CommandQueue.Dequeue();

            string[] _Datas = new string[6];
            for (int iLoopCount = 0; iLoopCount < _Datas.Length; ++iLoopCount)
                _Datas[iLoopCount] = CommandQueue.Dequeue();

            if (_Datas[_Datas.Length - 1] != ETX.ToString()) return false;

            //LaserProtocol.Type = Convert.ToInt32(_Datas[0]);
            //LaserProtocol.Data1 = Convert.ToDouble(_Datas[1]);
            //LaserProtocol.Data2 = Convert.ToDouble(_Datas[2]);
            //LaserProtocol.Data3 = Convert.ToDouble(_Datas[3]);
            //LaserProtocol.Data4 = Convert.ToDouble(_Datas[4]);
            //LaserProtocol.Status = true;

            var _ReceiveStringEvent = ReceiveStringEvent;
            _ReceiveStringEvent?.Invoke(_Datas);

            return true;
        }
        #endregion Socket Class(EthernetManager) => Ethernet Window Event
    }
}
