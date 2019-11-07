using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using LogMessageManager;

namespace EthernetManager
{
    public class CEthernetManager
    {
        private const char STX = (char)0x02;
        private const char ETX = (char)0x03;
        //private const string STX = "2";
        //private const string ETX = "3";

        private Socket SockClient;
        private SocketAsyncEventArgs SockArgs;
        private IPEndPoint IPEndPoint;

        private Thread ThreadClientReceive;
        public bool IsInitialize = false;
        private Object Lock_Recv = new object();
        private int BufferSize = 1024;

        private int RetryCount = 0;
        public bool AckFlag = false;
        public bool NakFlag = false;
        private const int RETRY_COUNT = 5;

        private Thread ThreadSendMessageQueue;
        private bool ThreadSendMessageQueueExit = false;
        private Queue<byte[]> SendDataQueue;

        private int ConnectPort = 5050;
        private string ConnectIP = "127.0.0.1";
        private bool IsInterrupt = false;

        private string LastSendMessage;
        private string LastCommand;

        public delegate void RecvMessageHandler(string _RecvMessage);
        public event RecvMessageHandler RecvMessageEvent;

        public delegate void SendMessageHandler(string _SendMessage);
        public event SendMessageHandler SendMessageEvent;

        #region Initialize & DeInitialize
        public CEthernetManager()
        {

        }

        public bool Initialize(string _IPAddress, int _PortNumber)
        {
            bool _Result = true;

            ConnectIP = _IPAddress;
            ConnectPort = _PortNumber;

            SockClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            SockClient.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 5000);
            IPEndPoint = new IPEndPoint(IPAddress.Parse(ConnectIP), ConnectPort);

            SockArgs = new SocketAsyncEventArgs();
            SockArgs.RemoteEndPoint = IPEndPoint;

            SendDataQueue = new Queue<byte[]>();

            ThreadSendMessageQueue = new Thread(ThreadSendMessageQueueFunc);
            ThreadSendMessageQueueExit = false;
            ThreadSendMessageQueue.Start();

            Connection();
            ClientStart();

            return _Result;
        }

        public void DeInitialize()
        {
            if (SockClient.Connected) SockClient.Disconnect(false); SockClient.Dispose();
            if (ThreadClientReceive != null) { ThreadClientReceive.Abort(); ThreadClientReceive = null; }
            if (ThreadSendMessageQueue != null) { ThreadSendMessageQueueExit = true; Thread.Sleep(100); ThreadSendMessageQueue.Abort(); ThreadSendMessageQueue = null; }
        }

        #endregion Initialize & DeInitialize

        #region Connection & DisConnection
        public void Connection()
        {
            try
            {
                SockArgs.Dispose();
                SockArgs = new SocketAsyncEventArgs();
                SockArgs.RemoteEndPoint = IPEndPoint;

                SockClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SockClient.ConnectAsync(SockArgs);

                if (ThreadClientReceive != null && ThreadClientReceive.IsAlive == false)
                {
                    Thread.Sleep(100);
                    ClientStart();
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "ThreadClientReceive Start");
                }

                IsInitialize = true;
            }

            catch
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Connect Exception", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void DisConnection()
        {
            try
            {
                if (SockArgs.ConnectSocket != null && SockArgs.ConnectSocket.Connected == true)
                {
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "Socket DisConnection");
                    SockClient.DisconnectAsync(SockArgs);
                    SockClient.Close();
                    Thread.Sleep(100);
                }
            }

            catch
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Socket DisConnect Exception", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void ReConnection(bool _IsThreadRebirth = false)
        {
            if (_IsThreadRebirth == true)
            {
                if (ThreadClientReceive != null)
                {
                    ThreadClientReceive.Abort();
                    ThreadClientReceive = null;
                }
            }

            DisConnection();
            Connection();
        }
        #endregion Connection & DisConnection

        #region 클라이언트 송신
        //public void Send(string _SendMessage, string _Command)
        public void Send(string _SendMessage)
        {
            LastSendMessage = _SendMessage;
            //LastCommand = _Command;

            //Protocol
            //형식 : STX TYPE DATA EXT
            //예 : STX, 1, 0.01, 0.05, 0.02, 0.01, ETX
            string _SendProtocol = String.Format("{0},{1},{2}", STX, _SendMessage, ETX);
            byte[] _SendData = Encoding.ASCII.GetBytes(_SendProtocol);
            SendDataQueue.Enqueue(_SendData);

            //Send
            SendMessageEvent(_SendProtocol);
        }

        public void SendLastCommand()
        {

        }

        public void SendACK()
        {

        }

        public void SendNACK()
        {

        }
        #endregion 클라이언트 송신

        #region 클라이언트 수신처리
        private void ClientStart()
        {
            ThreadClientReceive = new Thread(new ThreadStart(ThreadClientReceiveFunc));
            ThreadClientReceive.IsBackground = true;
            ThreadClientReceive.Start();
        }

        private void ThreadClientReceiveFunc()
        {
            //Receive Data
            try
            {
                int _RecvLen = 0;
                byte[] _RecvData = new byte[BufferSize];

                while (true)
                {
                    Thread.Sleep(5);
                    if (false == SockClient.Connected) continue;

                    Array.Clear(_RecvData, 0, _RecvData.Length);
                    _RecvLen = SockClient.Receive(_RecvData, 0, _RecvData.Length, SocketFlags.None);
                    if (_RecvLen <= 0) return;

                    string _ReturnString = null;
                    byte[] _Utf8Byte = Encoding.Convert(Encoding.Default, Encoding.UTF8, _RecvData);
                    //Unicode로 받을 시
                    //_ReturnString = Encoding.Unicode.GetString(_Utf8Byte);
                    //string _ReturnStringFit = _ReturnString.Remove(_RecvLen / 2);

                    //ASCII로 받을 시
                    //ASCII Value to String Convert!!
                    _ReturnString = Encoding.ASCII.GetString(_Utf8Byte);
                    string _ReturnStringFit = _ReturnString.Remove(_RecvLen); ;

                    RecvMessageEvent(_ReturnStringFit);
                }
            }

            catch (SocketException ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadClientReceiveFunc ERR!");
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, ex.Message);
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SocketErrorCode : " + ex.ErrorCode.ToString());
                if (ex.SocketErrorCode == SocketError.Interrupted)
                {
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Blocking : " + SocketError.Interrupted.ToString(), CLogManager.LOG_LEVEL.LOW);
                    IsInterrupt = true;
                }
            }

            finally
            {
                if (true == IsInterrupt)
                {
                    Thread.Sleep(50);
                    IsInterrupt = false;
                    ReConnection(IsInterrupt);
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Blocking Reconnection");
                }
            }
        }
        #endregion "클라이언트 수신처리"

        /// <summary>
        /// Send 할 Message를 Queue에 넣고 순차적으로 처리 한다.
        /// </summary>
        private void ThreadSendMessageQueueFunc()
        {
            while (false == ThreadSendMessageQueueExit)
            {
                //Queue 확인 후 Message Send 처리
                if (SendDataQueue.Count > 0)
                {
                    byte[] _SendData = SendDataQueue.Dequeue();
                    SockArgs.SetBuffer(_SendData, 0, _SendData.Length);
                    SockClient.SendAsync(SockArgs);
                    RetryCount = 0;

                    string _SendMessage = Encoding.Unicode.GetString(_SendData);
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "CMD : " + _SendMessage);

                    //LJH 2017.10.30
                    //사용 여부 판단 필요
                    //WaitingRoofFunction(_SendData);
                }
                Thread.Sleep(10);
            }
        }

        private void WaitingRoofFunction(byte[] _SendData)
        {
            while (RetryCount < RETRY_COUNT - 1)
            {
                lock (Lock_Recv)
                {
                    string _Command = Encoding.Unicode.GetString(_SendData);
                    if (_Command.IndexOf("ACK") != -1) break;

                    if (false == WaitReceiveMessage(300, 10))
                    {
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.WARN, @"ASK/NAK Return Message TimeOut(" + _Command + ") : " + RetryCount.ToString());
                    }

                    else
                    {

                    }
                }

                RetryCount++;
            }
        }

        private bool WaitReceiveMessage(int _TimeOut, int _SleepPeriod)
        {
            bool bRet = false;

            do
            {
                while (_TimeOut > 0 && (AckFlag == false && NakFlag == false))
                {
                    Thread.Sleep(_SleepPeriod);
                    _TimeOut -= _SleepPeriod;
                    //Application.DoEvents();
                }
                if (_TimeOut <= 0 || (AckFlag == false && NakFlag == false)) break;
                bRet = true;
            } while (false);

            return bRet;
        }

        #region TCP 포트 이용 가능 여부 조사하기 - IsAvailableTCPPort()
        /// <summary>
        /// TCP 포트 이용 가능 여부 조사하기
        /// </summary>
        /// <param name="port">포트</param>
        /// <returns>TCP 포트 이용 가능 여부</returns>
        public bool IsAvailableTCPPort(bool _IsTryConnect)
        {
            bool _IsAvailable = false;

            IPGlobalProperties _IpGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] _TcpConnectionInformationArray = _IpGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpConnectionInformation in _TcpConnectionInformationArray)
            {
                //CLogManager.AddLogMessage(CLogManager.LOG_TYPE.ERR, tcpConnectionInformation.LocalEndPoint.ToString());
                if (tcpConnectionInformation.RemoteEndPoint.Port == ConnectPort)
                {
                    if (tcpConnectionInformation.State == TcpState.Established)
                    {
                        _IsAvailable = true;
                        break;
                    }
                }
            }

            if (false == _IsAvailable && true == _IsTryConnect) ReConnection();

            return _IsAvailable;
        }
        #endregion
    }
}
