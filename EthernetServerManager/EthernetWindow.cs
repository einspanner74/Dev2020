
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;

using CustomMsgBoxManager;
using LogMessageManager;

namespace EthernetServerManager
{
    public partial class EthernetWindow : Form
    {
        public bool IsShowWindow = false;

        private string CommonFolderPath = "";

        private CEtherentServerManager ServerSock;
        private Queue<string> CmdQueue = new Queue<string>();

        private string IPAddress = "192.168.0.1";
        private int PortNumber = 5050;
        private string ClientIPAddress = "192.168.0.2";

        private bool IsConnected = false;
        //private Timer ConnectCheckTimer;

        public delegate bool ReceiveStringHandler(string[] _ReceiveMsasage, int _PortNumber);
        public event ReceiveStringHandler ReceiveStringEvent;

        //LDH, 2019.04.03, timer를 Thread로 변경
        private Thread ThreadConnectCheck;
        private bool IsThreadConnectCheckExit = false;

        private Thread ThreadReceiveDataCheck;
        private bool IsThreadReceiveDataCheckExit = false;

        #region Initialize & DeInitialize
        public EthernetWindow()
        {
            InitializeComponent();
        }

        public void Initialize(string _CommonFolderPath, short _PortNumber)
        {
            CommonFolderPath = _CommonFolderPath;

            ReadEthernetInfoFile();

            PortNumber = PortNumber + _PortNumber;

            textBoxIPAddress.Text = IPAddress;
            textBoxPortNumber.Text = PortNumber.ToString();

            ServerSock = new CEtherentServerManager();
            ServerSock.Initialize(IPAddress, PortNumber);
            ServerSock.RecvMessageEvent += new CEtherentServerManager.RecvMessageHandler(SetReceiveMessage);

            ThreadConnectCheck = new Thread(ThreadConnectCheckFunction);
            IsThreadConnectCheckExit = false;
            ThreadConnectCheck.Start();

            ThreadReceiveDataCheck = new Thread(ThreadReceiceDataCheckFunction);
            IsThreadReceiveDataCheckExit = false;
            ThreadReceiveDataCheck.Start();            
        }

        public void DeInitialize()
        {
            if (ThreadConnectCheck != null) { IsThreadConnectCheckExit = true; Thread.Sleep(200); ThreadConnectCheck.Abort(); ThreadConnectCheck = null; }
            if (ThreadReceiveDataCheck != null) { IsThreadReceiveDataCheckExit = true; Thread.Sleep(200); ThreadReceiveDataCheck.Abort(); ThreadReceiveDataCheck = null; }

            ServerSock.RecvMessageEvent -= new CEtherentServerManager.RecvMessageHandler(SetReceiveMessage);
            ServerSock.DeInitialize();
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
                    switch (_Node.Name)
                    {
                        case "IPAddress": IPAddress = _Node.InnerText; break;
                        case "PortNumber": PortNumber = Convert.ToInt16(_Node.InnerText); break;
                        case "ClientIPAddress": ClientIPAddress = _Node.InnerText; break;
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
                _XmlWriter.WriteElementString("ClientIPAddress", ClientIPAddress);
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write Ethernet Information
        #endregion Initialize & DeInitialize
        
        //LDH, 2019.04.03, Client Connect Check
        private void ThreadConnectCheckFunction()
        {
            try
            {
                while (false == IsThreadConnectCheckExit)
                {
                    ConnectCheckTimer_Tick();
                    Thread.Sleep(500);
                }
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadConnectCheckFunction Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }

        //LDH, 2019.04.03, Check
        private void ThreadReceiceDataCheckFunction()
        {
            try
            {
                while (false == IsThreadConnectCheckExit)
                {
                    ProtocolCommandProcess();
                    Thread.Sleep(10);
                }
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadReceiceDataCheckFunction Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }

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
        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress = textBoxIPAddress.Text;
            PortNumber = Convert.ToInt16(textBoxPortNumber.Text);

            ServerSock.Connection(IPAddress, PortNumber);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            ServerSock.DisConnection();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //if (false == IsConnected) { MessageBox.Show("Disconnected"); return; }
            if (false == IsConnected) { CMsgBoxManager.Show("Disconnected", "", false, 2000); return; }
            ServerSock.Send(textBoxManualData.Text);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsShowWindow = false;
            this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //WriteEthernetInfoFile();
            IsShowWindow = false;
            this.Hide();
        }
        #endregion Button Event

        #region "Control Invoke"
        private void ControlInvoke(Control _Control, string _Msg)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate ()
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
        private void ControlInvoke(Control _Control, Color _Color)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate ()
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
        #endregion "Control Invoke"

        private void ConnectCheckTimer_Tick()
        {
            string[] _ConnectedList = ServerSock.GetConnectedClientList();

            IsConnected = false;
            for (int iLoopCount = 0; iLoopCount < _ConnectedList.Length; ++iLoopCount)
            {
                if (_ConnectedList[iLoopCount] == ClientIPAddress)
                {
                    IsConnected = true;
                    break;
                }
            }

            if (true == IsConnected)    ControlInvoke(picConnection, Color.Green);
            else                        ControlInvoke(picConnection, Color.Red);

            if (true == ServerSock.GetServerAlready())  ControlInvoke(picServerStatus, Color.Green);
            else                                        ControlInvoke(picServerStatus, Color.Red);
        }

        private void SetReceiveMessage(string _RecvMessage)
        {
            string _RecvString = _RecvMessage.Trim();
            _RecvString = _RecvString.Replace(" ", "");
            _RecvString = _RecvString.Replace("\0", "");
            string[] _RecvCmd = ParsingProtocol(_RecvString);

            for (int iLoopCount = 0; iLoopCount < _RecvCmd.Length; ++iLoopCount)
                CmdQueue.Enqueue(_RecvCmd[iLoopCount]);

            string _RecvText = string.Join(",", _RecvCmd);
            ControlInvoke(textBoxRecvString, _RecvText);
        }

        private string[] ParsingProtocol(string _SendProtocol)
        {
            string _Protocol = _SendProtocol;
            char[] _Separators = { '>', ';', ',' };

            int _EtxIndex = _SendProtocol.LastIndexOf(CEtherentServerManager.ETX);
            if (_EtxIndex != -1) _Protocol = _SendProtocol.Insert(_EtxIndex, ";");

            string[] _RecvCmd = _Protocol.Split(_Separators);

            return _RecvCmd;
        }

        private bool ProtocolCommandProcess()
        {
            bool _Result = false;

            if (CmdQueue.Contains(CEtherentServerManager.STX.ToString()) == false) return false;
            if (CmdQueue.Contains(CEtherentServerManager.ETX.ToString()) == false) return false;

            string _Data = "";
            while (_Data != CEtherentServerManager.STX.ToString()) _Data = CmdQueue.Dequeue();
            
            List<string> _Datas = new List<string>();

            do
            {
                _Datas.Add(CmdQueue.Dequeue());
            }
            while (_Datas[_Datas.Count - 1] != CEtherentServerManager.ETX.ToString());

            //if (_Datas.Count != 2) { _Result = false; return _Result; }

            var _ReceiveStringEvent = ReceiveStringEvent;
            if (true == _ReceiveStringEvent?.Invoke(_Datas.ToArray(), PortNumber)) _Result = true;

            return _Result;
        }

        public void SendResultData(string _ResultDataString, bool _UseFormat)
        {
            if (false == IsConnected) { CMsgBoxManager.Show(string.Format("No connection - IP : {0}, Port : {1}", IPAddress, PortNumber), "", false, 2000); return; }

            if(_UseFormat) ServerSock.Send(_ResultDataString);
            else           ServerSock.SendNotFormat(_ResultDataString);

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Receive Data : " + _ResultDataString);
        }

        public void ShowEthernetWindow()
        {
            textBoxIPAddress.Text = IPAddress;
            textBoxPortNumber.Text = PortNumber.ToString();

            IsShowWindow = true;
            this.Show();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            string[] _RecvTest = textBoxManualData.Text.Split(',');
            ReceiveStringEvent(_RecvTest, PortNumber);
        }
    }
}
