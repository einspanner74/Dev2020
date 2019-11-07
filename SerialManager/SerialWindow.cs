using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Xml;
using System.IO;

using LogMessageManager;

enum eSerialProtocol { STX = '@', ETX = '\r' }
namespace SerialManager
{
    public partial class SerialWindow : Form
    {
        private SerialPort SerialComm;
        public bool IsShowWindow;

        private string CommonFolderPath = @"D:\VisionInspectionData\Common\";
        private string ComPort = "COM5";

        private delegate void SetTextCallback(string data);

        public delegate bool SerialReceiveHandler(string _SerialData);
        public event SerialReceiveHandler SerialReceiveEvent;

        public SerialWindow(string _CommonFolderPath = "")
        {
            InitializeComponent();

            if (_CommonFolderPath != "") CommonFolderPath = _CommonFolderPath;

            SerialComm = new SerialPort();
            SerialComm.BaudRate = 115200;
            SerialComm.DataBits = (int)7;
            SerialComm.Parity = Parity.Even;
            SerialComm.StopBits = StopBits.Two;
            SerialComm.ReadTimeout = (int)500;
            SerialComm.WriteTimeout = (int)500;
        }

        public bool Initialize(string _PortName)
        {
            bool _Result = true;

            SerialComm.PortName = "COM5";

            ReadSerialInfoFile();
            SerialComm.PortName = ComPort;

            try
            {
                SerialComm.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceived);
                SerialComm.Open();
            }
            catch(Exception ex)
            {                
                _Result = false;
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SerialWindow Initialize Exception!!", CLogManager.LOG_LEVEL.LOW);
            }

            return _Result;
        }

        public void DeInitialize()
        {
            SerialComm.DataReceived -= new SerialDataReceivedEventHandler(SerialDataReceived);
            SerialComm.Close();
        }

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
                CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "DIOControlWindow GetNodeList Exception!!", CLogManager.LOG_LEVEL.LOW);
            }

            return _XmlNodeList;
        }

        private void ReadSerialInfoFile()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _SerialInfoFileName = String.Format(@"{0}SerialInformation.xml", CommonFolderPath);
            if (false == File.Exists(_SerialInfoFileName))
            {
                File.Create(_SerialInfoFileName).Close();
                WriteSerialInfoFile();
                System.Threading.Thread.Sleep(100);
            }

            else
            {
                XmlNodeList _XmlNodeList = GetNodeList(_SerialInfoFileName);
                if (null == _XmlNodeList) return;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return;
                    switch (_Node.Name)
                    {
                        case "ComPort":ComPort = _Node.InnerText; break;
                    }
                }
            }
        }

        private void WriteSerialInfoFile()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(@CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _SerialInfoFileName = String.Format(@"{0}SerialInformation.xml", CommonFolderPath);
            XmlTextWriter _XmlWriter = new XmlTextWriter(_SerialInfoFileName, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("SerialInformation");
            {
                _XmlWriter.WriteElementString("ComPort", ComPort);
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }

        #region Control Default Event
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
        #endregion Control Default Event

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(SerialComm.IsOpen)
            {
                if(textBoxManualData.Text.Length == 0)
                {
                    MessageBox.Show("데이터를 입력 하십시오.");
                    return;
                }

                byte[] values = Encoding.ASCII.GetBytes(textBoxManualData.Text);

                try
                {
                    
                }
                catch
                {
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SerialWindow btnSend Exception!!", CLogManager.LOG_LEVEL.LOW);
                }

                SerialComm.Write(values, 0, values.Count());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IsShowWindow = false;
            this.Hide();
        }

        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SerialComm.IsOpen)
            {
                Thread.Sleep(50);

                string data = SerialComm.ReadExisting();

                if (data != string.Empty)
                {
                    string[] values = data.Split(',');

                    if (!values[0].Contains(Convert.ToChar(eSerialProtocol.STX))) return;
                    else if (!values[values.Count() - 1].Contains(Convert.ToChar(eSerialProtocol.ETX))) return;

                    SerialReceiveEvent(data);
                    SetText(data);
                }
            }
        }

        private void SetText(string _Data)
        {
            if(this.textBoxRecvString.InvokeRequired)
            {
                SetTextCallback DataSet = new SetTextCallback(SetText);
                this.Invoke(DataSet, new object[] { _Data });
            }
            else
            {
                this.textBoxRecvString.Clear();
                this.textBoxRecvString.Text += (_Data + " ");
            }
        }

        public void SendSequenceData(string _SendData)
        {
            byte[] SendData;

            try
            {
                SendData = Encoding.ASCII.GetBytes(_SendData);
                SerialComm.Write(SendData, 0, SendData.Count());
            }
            catch
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "SerialWindow SendWequenceData Exception!!", CLogManager.LOG_LEVEL.LOW);
            }
        }

        public void ShowSerialWindow()
        {
            IsShowWindow = true;
            this.Show();
        }
    }
}
