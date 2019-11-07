using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

using CustomMsgBoxManager;

namespace EthernetServerManager
{
    public class CEtherentServerManager
    {
        public const char STX = (char)0x02;
        public const char ETX = (char)0x03;
        //public const string STX = "STX";
        //public const string ETX = "ETX";

        private Socket SockServer;
        private IPEndPoint IPEndPoint;
        private SocketAsyncEventArgs SockArgs;
        private List<Socket> SockClientList;
        private byte[] ReceiveData;

        private int ConnectPort = 5050;
        //private string ConnectIP = "10.100.110.214";
        private string ConnectIP = "192.168.0.2";
        private bool IsServerAlready = false;

        private string LastSendMessage;
        private Timer ConnectCheckTimer;

        private object LockClientCheck = new object();

        public delegate void RecvMessageHandler(string _RecvMessage);
        public event RecvMessageHandler RecvMessageEvent;

        #region Initialize & DeInitialize
        public CEtherentServerManager()
        {

        }

        public bool Initialize(string _IPAddress, int _PortNumber)
        {
            bool _Result = true;

            ConnectIP = _IPAddress;
            ConnectPort = _PortNumber;

            SockClientList = new List<Socket>();

            try
            {
                SockServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint = new IPEndPoint(IPAddress.Parse(ConnectIP), ConnectPort);
                SockServer.Bind(IPEndPoint);
                SockServer.Listen(20);

                SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
                _SockArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
                SockServer.AcceptAsync(_SockArgs);

                IsServerAlready = true;
            }

            catch
            {
                IsServerAlready = false;
            }

            ConnectCheckTimer = new Timer();
            ConnectCheckTimer.Tick += ConnectCheckTimer_Tick;
            ConnectCheckTimer.Interval = 350;
            ConnectCheckTimer.Start();

            return _Result;
        }

        public void DeInitialize()
        {
            ConnectCheckTimer.Stop();
        }
        #endregion Initialize & DeInitialize

        #region Client Event
        private void Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket _SockClient = e.AcceptSocket;
            if (false == _SockClient.Connected) return;
            SockClientList.Add(_SockClient);

            if (SockClientList != null)
            {
                SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
                ReceiveData = new byte[1024];
                _SockArgs.SetBuffer(ReceiveData, 0, 1024);
                _SockArgs.UserToken = SockClientList;
                _SockArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
                _SockClient.ReceiveAsync(_SockArgs);
            }
            e.AcceptSocket = null;
            SockServer.AcceptAsync(e);
        }

        private void Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket _SockClient = (Socket)sender;

            if (_SockClient.Connected && e.BytesTransferred > 0)
            {
                byte[] _DataBuffer = e.Buffer;
                string _DataString = Encoding.UTF8.GetString(_DataBuffer);

                string _TempText = _DataString.Trim();

                var _RecvMessageEvent = RecvMessageEvent;
                _RecvMessageEvent?.Invoke(_TempText);

                _DataBuffer = new byte[_DataBuffer.Length];
                e.SetBuffer(_DataBuffer, 0, 1024);
                _SockClient.ReceiveAsync(e);
            }

            else
            {
                _SockClient.Disconnect(false);
                _SockClient.Dispose();
                SockClientList.Remove(_SockClient);
            }
        }
        #endregion Client Event

        #region Connection & DisConnection
        public void Connection()
        {
            //if (true == IsServerAlready) { MessageBox.Show("Already connected"); return; }
            if (true == IsServerAlready) { CMsgBoxManager.Show("Already connected", "", false, 2000); return; }

            SockClientList.Clear();

            SockServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint = new IPEndPoint(IPAddress.Parse(ConnectIP), ConnectPort);
            SockServer.Bind(IPEndPoint);
            SockServer.Listen(20);

            SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
            _SockArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            SockServer.AcceptAsync(_SockArgs);

            IsServerAlready = true;
        }

        public void Connection(string _IPAddress, int _PortNumber)
        {
            //if (true == IsServerAlready) { MessageBox.Show("Already connected"); return; }
            if (true == IsServerAlready) { CMsgBoxManager.Show("Already connected", "", false, 2000); return; }

            ConnectIP = _IPAddress;
            ConnectPort = _PortNumber;

            SockClientList.Clear();

            SockServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint = new IPEndPoint(IPAddress.Parse(ConnectIP), ConnectPort);
            SockServer.Bind(IPEndPoint);
            SockServer.Listen(20);

            SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
            _SockArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Completed);
            SockServer.AcceptAsync(_SockArgs);

            IsServerAlready = true;
        }

        public void DisConnection()
        {
            foreach (Socket _SockClient in SockClientList)
            {
                if (_SockClient.Connected) _SockClient.Disconnect(false);
                _SockClient.Dispose();
            }
            SockServer.Dispose();
            SockClientList.Clear();

            IsServerAlready = false;
        }

        public bool GetServerAlready()
        {
            return IsServerAlready;
        }
        #endregion Connection & DisConnection

        public void Send(string _Data)
        {
            LastSendMessage = _Data;

            string _SendProtocol = String.Format("{0},{1},{2}", STX, LastSendMessage, ETX);
            byte[] _SendData = Encoding.ASCII.GetBytes(_SendProtocol);
            //byte[] _SendData = Encoding.Unicode.GetBytes(_SendProtocol);

            SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
            _SockArgs.SetBuffer(_SendData, 0, _SendData.Length);

            foreach (Socket _SockClient in SockClientList)
            {
                //_SockClient.Sㅁend(_SendData);
                _SockClient.SendAsync(_SockArgs);
            }
        }

        //LDH, 2019.04.26, ','로 구분하지 않는 Data는 앞뒤로 STX, ETX만 붙여서 보냄
        public void SendNotFormat(string _Data)
        {
            LastSendMessage = _Data;

            string _SendProtocol = String.Format("{0}{1}{2}", STX, LastSendMessage, ETX);
            byte[] _SendData = Encoding.ASCII.GetBytes(_SendProtocol);
            //byte[] _SendData = Encoding.Unicode.GetBytes(_SendProtocol);

            SocketAsyncEventArgs _SockArgs = new SocketAsyncEventArgs();
            _SockArgs.SetBuffer(_SendData, 0, _SendData.Length);

            foreach (Socket _SockClient in SockClientList)
            {
                //_SockClient.Send(_SendData);
                _SockClient.SendAsync(_SockArgs);
            }
        }

        private void ConnectCheckTimer_Tick(object sender, EventArgs e)
        {
            int _ConnectCount = 0;
            _ConnectCount = SockClientList.Count;
        }

        public string[] GetConnectedClientList()
        {
            string[] _ClientList = new string[SockClientList.Count];

            try
            {
                //lock (LockClientCheck)
                {
                    for (int iLoopCount = 0; iLoopCount < SockClientList.Count; ++iLoopCount)
                    {
                        IPAddress _Address = ((IPEndPoint)SockClientList[iLoopCount].RemoteEndPoint).Address;
                        _ClientList[iLoopCount] = _Address.ToString();
                    }
                }
            }

            catch
            {

            }

            return _ClientList;
        }
    }
}
