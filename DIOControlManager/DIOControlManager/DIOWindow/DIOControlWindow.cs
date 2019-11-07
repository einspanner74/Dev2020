using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Xml;

using LogMessageManager;
using ParameterManager;
using CustomMsgBoxManager;

namespace DIOControlManager
{
    public partial class DIOControlWindow : Form
    {
        private DIONamingWindow DioNamingWnd;
        private CDIO DigitalIO = new CDIO();
        public DIOBaseCommand DioBaseCmd;

        private eProjectType ProjectType = eProjectType.NONE;

        private string CommonFolderPath = "";

        private List<SignalToggleData> SignalToggleList;
        private short SleepTime = 10;

        private readonly int ALIVE_SIGNAL_TIME = 0;
        private readonly int ALIVE_CHECK_TIME = 0;

        private short IOCnt = 16;
        private List<string> InputNameList;
        private List<string> OutputNameList;

        private string IODeviceName = "DIO000";

        private byte[] InputMultiSignal;
        private byte[] InputMultiSignalPre;
        private bool[] OutputSignalFlag = null;

        public bool IsShowWindow = false;
        public bool IsInitialize = false;

        private CPressingButton.PressButton CurrentButton;
        private CPressingButton.PressButton[] btnInputSignal;
        private CPressingButton.PressButton[] btnOutputSignal;

        private Thread ThreadInputIOCheck;
        private bool IsThreadInputIOCheckExit;
        private Thread ThreadOutputIOCheck;
        private bool IsThreadOutputIOCheckExit;

        private Thread ThreadVisionAliveSignal;
        private bool IsThreadVisionAliveSignalExit;
        private int VisionAliveSignalCount = 0;
        private bool VisionAliveSignalFlag = false;

        private Thread ThreadInputAliveCheck;
        private bool IsThreadInputAliveCheckExit;
        private int InputAliveCheckCount = 0;
        private bool InputAliveCheckFlag = false;

        private Thread ThreadSignalToggle;
        private bool IsThreadSignalToggleExit;

        public delegate void InputChangedHandler(short _BitNum, bool _Signal);
        public event InputChangedHandler InputChangedEvent;

        #region Initialize & DeInitialize
        public DIOControlWindow(int _ProjectType = 0, string _CommonFolderPath = "")
        {
            CommonFolderPath = _CommonFolderPath;

            InitializeComponent();
            InitializeControl();

            ProjectType = (eProjectType)_ProjectType;
            DioNamingWnd.ChangeNameEvent += new DIONamingWindow.ChangeNameHandler(ChangeNameEventFunction);

            ALIVE_SIGNAL_TIME = 50;
            ALIVE_CHECK_TIME = 500;

            if (ProjectType == eProjectType.NONE)               DioBaseCmd = new DefaultCmd(IOCnt);
            else if (ProjectType == eProjectType.NAVIEN)        DioBaseCmd = new NavienCmd(IOCnt);
        }

        public void InitializeControl()
        {
            DioNamingWnd = new DIONamingWindow();
            CurrentButton = new CPressingButton.PressButton();

            ReadIOInfoFile();

            if (IOCnt <= 16)
            {
                this.Size = new Size(360, this.Height);
                panelMain.Size = new Size(360, panelMain.Height);
                labelTitle.Size = new Size(360, labelTitle.Height);
                labelInputTitle.Size = new Size(354, labelInputTitle.Height);
                labelOutputTitle.Size = new Size(354, labelOutputTitle.Height);
                labelExit.Location = new Point(330, 3);
            }

            else if (IOCnt <= 32)
            {
                this.Size = new Size(730, this.Height);
                panelMain.Size = new Size(730, panelMain.Height);
                labelTitle.Size = new Size(730, labelTitle.Height);
                labelInputTitle.Size = new Size(724, labelInputTitle.Height);
                labelOutputTitle.Size = new Size(724, labelOutputTitle.Height);
                labelExit.Location = new Point(700, 3);
            }

            btnInputSignal = new CPressingButton.PressButton[32] { btnInput0, btnInput1, btnInput2,  btnInput3,  btnInput4,  btnInput5,  btnInput6,  btnInput7,
                                                                   btnInput8, btnInput9, btnInput10, btnInput11, btnInput12, btnInput13, btnInput14, btnInput15,
                                                                   btnInput16, btnInput17, btnInput18,  btnInput19,  btnInput20,  btnInput21,  btnInput22,  btnInput23,
                                                                   btnInput24, btnInput25, btnInput26,  btnInput27,  btnInput28,  btnInput29,  btnInput30,  btnInput31 };

            btnOutputSignal = new CPressingButton.PressButton[32] { btnOutput0, btnOutput1, btnOutput2,  btnOutput3,  btnOutput4,  btnOutput5,  btnOutput6,  btnOutput7,
                                                                    btnOutput8, btnOutput9, btnOutput10, btnOutput11, btnOutput12, btnOutput13, btnOutput14, btnOutput15,
                                                                    btnOutput16, btnOutput17, btnOutput18, btnOutput19, btnOutput20, btnOutput21, btnOutput22, btnOutput23,
                                                                    btnOutput24, btnOutput25, btnOutput26, btnOutput27, btnOutput28, btnOutput29, btnOutput30, btnOutput31 };

            OutputSignalFlag = new bool[32] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            InputMultiSignal = new byte[IOCnt];
            InputMultiSignalPre = new byte[IOCnt];

            for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
            {
                if (InputNameList.Count <= iLoopCount)
                {
                    InputNameList.Add(String.Format("DI{0}", iLoopCount));
                    OutputNameList.Add(String.Format("DO{0}", iLoopCount));
                }

                btnInputSignal[iLoopCount].Text = InputNameList[iLoopCount];
                btnInputSignal[iLoopCount].Tag = iLoopCount;

                btnOutputSignal[iLoopCount].Text = OutputNameList[iLoopCount];
                btnOutputSignal[iLoopCount].Tag = iLoopCount;
            }
        }

        #region Read & Write IO Information
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

        private void ReadIOInfoFile()
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(@CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _IOInfoFileName = string.Format(@"{0}IOInformation.xml", CommonFolderPath);
            if (false == File.Exists(_IOInfoFileName))
            {
                File.Create(_IOInfoFileName).Close();
                WriteIOInfoFile();
                System.Threading.Thread.Sleep(100);
            }

            else
            {
                XmlNodeList _XmlNodeList = GetNodeList(_IOInfoFileName);
                if (null == _XmlNodeList) return;
                foreach (XmlNode _Node in _XmlNodeList)
                {
                    if (null == _Node) return;
                    switch (_Node.Name)
                    {
                        case "IOCount":     IOCnt = Convert.ToInt16(_Node.InnerText); break;
                        case "InputInfo":   ReadInputInformation(_Node);    break;
                        case "OutputInfo":  ReadOutputInformation(_Node);   break;
                        case "DeviceName":  IODeviceName = _Node.InnerText; break;
                    }
                }
            }
        }

        private void ReadInputInformation(XmlNode _Nodes)
        {
            int _Count = 0;
            InputNameList = new List<string>();
            InputNameList.Clear();
            foreach (XmlNode _Node in _Nodes)
            {
                string _Name = string.Format($"IN_{_Count}");
                if (_Node.Name == _Name) InputNameList.Add(_Node.InnerText);
                else                     InputNameList.Add("DI" + _Count);
                _Count++;
            }
        }

        private void ReadOutputInformation(XmlNode _Nodes)
        {
            int _Count = 0;
            OutputNameList = new List<string>();
            OutputNameList.Clear();
            foreach (XmlNode _Node in _Nodes)
            {
                string _Name = string.Format($"OUT_{_Count}");
                if (_Node.Name == _Name) OutputNameList.Add(_Node.InnerText);
                else                     OutputNameList.Add("DO" + _Count);
                _Count++;
            }
        }

        private void WriteIOInfoFile()
        {
            if (InputNameList == null || InputNameList?.Count != IOCnt)
            {
                InputNameList = new List<string>();
                for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                    InputNameList.Add("DI" + iLoopCount);
            }

            else
            {
                InputNameList.Clear();
                for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                    InputNameList.Add(btnInputSignal[iLoopCount].Text);
            }

            if (OutputNameList == null || OutputNameList?.Count != IOCnt)
            {
                OutputNameList = new List<string>();
                for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                    OutputNameList.Add("DO" + iLoopCount);
            }

            else
            {
                OutputNameList.Clear();
                for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                    OutputNameList.Add(btnOutputSignal[iLoopCount].Text);
            }

            DirectoryInfo _DirInfo = new DirectoryInfo(@CommonFolderPath);
            if (false == _DirInfo.Exists) { _DirInfo.Create(); System.Threading.Thread.Sleep(100); }

            string _IOInfoFileName = string.Format(@"{0}IOInformation.xml", CommonFolderPath);
            XmlTextWriter _XmlWriter = new XmlTextWriter(_IOInfoFileName, Encoding.Unicode);
            _XmlWriter.Formatting = Formatting.Indented;
            _XmlWriter.WriteStartDocument();
            _XmlWriter.WriteStartElement("IOInformation");
            {
                _XmlWriter.WriteElementString("IOCount", IOCnt.ToString());

                _XmlWriter.WriteStartElement("InputInfo");
                {
                    for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                        _XmlWriter.WriteElementString("IN_" + iLoopCount, InputNameList[iLoopCount]);
                }
                _XmlWriter.WriteEndElement();

                _XmlWriter.WriteStartElement("OutputInfo");
                {
                    for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount)
                        _XmlWriter.WriteElementString("OUT_" + iLoopCount, OutputNameList[iLoopCount]);
                }
                _XmlWriter.WriteEndElement();

                _XmlWriter.WriteElementString("DeviceName", IODeviceName);
            }
            _XmlWriter.WriteEndElement();
            _XmlWriter.WriteEndDocument();
            _XmlWriter.Close();
        }
        #endregion Read & Write IO Information

        public bool Initialize()
        {
            bool _Result = true;
            IsInitialize = true;

            IsInitialize = InitializeIOBoard();

            SignalToggleList = new List<SignalToggleData>();

            if (!IsInitialize) return false;

            #region Initialize Thread
            ThreadInputIOCheck = new Thread(ThreadInputIOCheckFunc);
            IsThreadInputIOCheckExit = false;
            ThreadInputIOCheck.IsBackground = true;
            ThreadInputIOCheck.Start();

            ThreadOutputIOCheck = new Thread(ThreadOutputIOCheckFunc);
            IsThreadOutputIOCheckExit = false;
            ThreadOutputIOCheck.IsBackground = true;
            ThreadOutputIOCheck.Start();

            ThreadVisionAliveSignal = new Thread(ThreadVisionAliveSignalFunc);
            IsThreadVisionAliveSignalExit = false;
            ThreadOutputIOCheck.IsBackground = true;
            if (ProjectType != eProjectType.NAVIEN) 
                ThreadVisionAliveSignal.Start();

            ThreadInputAliveCheck = new Thread(ThreadInputAliveCheckFunc);
            IsThreadInputAliveCheckExit = false;
            ThreadOutputIOCheck.IsBackground = true;
            ThreadInputAliveCheck.Start();

            ThreadSignalToggle = new Thread(ThreadSignalToggleFunc);
            IsThreadSignalToggleExit = false;
            ThreadSignalToggle.IsBackground = true;
            ThreadSignalToggle.Start();
            #endregion Initialize Thread

            return _Result;
        }

        public void DeInitialize()
        {
            if (true == IsInitialize)
            {
                IsInitialize = false;

                if (ThreadInputIOCheck != null)      { IsThreadInputIOCheckExit = true; Thread.Sleep(100); ThreadInputIOCheck.Abort(); ThreadInputIOCheck = null; }
                if (ThreadOutputIOCheck != null)     { IsThreadOutputIOCheckExit = true; Thread.Sleep(100); ThreadOutputIOCheck.Abort(); ThreadOutputIOCheck = null; }
                if (ThreadVisionAliveSignal != null) { IsThreadVisionAliveSignalExit = true; Thread.Sleep(100); ThreadVisionAliveSignal.Abort(); ThreadVisionAliveSignal = null; }
                if (ThreadInputAliveCheck != null)   { IsThreadInputAliveCheckExit = true; Thread.Sleep(100); ThreadInputAliveCheck.Abort(); ThreadInputAliveCheck = null; }
                if (ThreadSignalToggle != null)      { IsThreadSignalToggleExit = true; Thread.Sleep(100); ThreadSignalToggle.Abort(); ThreadSignalToggle = null; }

                DioNamingWnd.ChangeNameEvent -= new DIONamingWindow.ChangeNameHandler(ChangeNameEventFunction);
                WriteIOInfoFile();
                DigitalIO.DeInitialize();
            }
        }

        private bool InitializeIOBoard()
        {
            DigitalIO = new CDIO(IOCnt);
            if ((int)CDioConst.DIO_ERR_SUCCESS != DigitalIO.Initialize(IODeviceName))
            {
                CMsgBoxManager.Show("IO Board Initialize Error", "", false, 2000);
                //MessageBox.Show("IO Board Initialize Error");
                return false;
            }
            return true;
        }
        #endregion Initialize & DeInitialize

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

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion Control Default Event

        #region Control Event
        private void btnTrigger_Click(object sender, EventArgs e)
        {
            //int _BitCommand = AirBlowCmd.BitCheck(5);
            int _BitCommand = DioBaseCmd.InputBitCheck(DefaultCmd.IN_TRIGGER);
            if (_BitCommand == DIO_DEF.NONE) return;

            var _InputChangedEvent = InputChangedEvent;
            _InputChangedEvent?.Invoke((short)_BitCommand, true); // 6.0부터
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            int _Bitcommand = DioBaseCmd.InputBitCheck(DefaultCmd.IN_REQUEST);
            if (_Bitcommand == DIO_DEF.NONE) return;

            var _InputChangedEvent = InputChangedEvent;
            _InputChangedEvent?.Invoke((short)_Bitcommand, true);
        }

        public void ChangeNameEventFunction(string _ChangeName)
        {
            CurrentButton.Text = _ChangeName;
        }

        public void ShowDIOWindow()
        {
            IsShowWindow = true;
            this.Show();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            short _Tag = Convert.ToInt16(((Button)sender).Tag);
            SetOutputSignal(_Tag, !OutputSignalFlag[_Tag]);
        }

        private void btnOutput_MousePressing(object sender, EventArgs e)
        {
            short _Tag = Convert.ToInt16(((Button)sender).Tag);
            DioNamingWnd.SetCurrentName(btnOutputSignal[_Tag].Text);
            CurrentButton = btnOutputSignal[_Tag];

            Point _Position = Cursor.Position;
            DioNamingWnd.ShowWindow(_Position);
        }

        private void btnInput_MousePressing(object sender, EventArgs e)
        {
            short _Tag = Convert.ToInt16(((Button)sender).Tag);
            DioNamingWnd.SetCurrentName(btnInputSignal[_Tag].Text);
            CurrentButton = btnInputSignal[_Tag];

            Point _Position = Cursor.Position;
            DioNamingWnd.ShowWindow(_Position);
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            IsShowWindow = false;
            this.Hide();
        }
        #endregion Control Event

        private void SetSignalToggleVariable(short _BitNumber, bool _Signal, short _ToggleTime)
        {
            if (_ToggleTime == 0) return;

            SignalToggleData _SignalToggleData = new SignalToggleData();
            _SignalToggleData.Signal = _BitNumber;
            _SignalToggleData.ToggleTime = _ToggleTime;
            _SignalToggleData.CurrentSignal = _Signal;
            SignalToggleList.Add(_SignalToggleData);
        }

        public void SetOutputSignal(short _BitNumber, bool _Signal, short _ToggleTime = 0)
        {
            if (false == IsInitialize) return;

            OutputSignalFlag[_BitNumber] = _Signal;
            byte _Data = Convert.ToByte(OutputSignalFlag[_BitNumber]);
            DigitalIO.OutputBitData(_BitNumber, _Data);
            SetSignalToggleVariable(_BitNumber, _Signal, _ToggleTime);
        }

        private void InputSignalCheck()
        {
            if (false == IsInitialize) return;

            short[] _BitNum = new short[IOCnt];
            for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount) _BitNum[iLoopCount] = (short)iLoopCount;

            if(DigitalIO.InputMultiBitData(_BitNum, IOCnt, InputMultiSignal)!= (int)CDioConst.DIO_ERR_SUCCESS)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Input Check Error");
                return;
            }

            for(int iLoopCount = 0; iLoopCount < IOCnt; iLoopCount++)
            {
                if (InputMultiSignal[iLoopCount] == (short)DIOEnum.ON)
                    ControlInvoke(btnInputSignal[iLoopCount], Color.DarkGreen);
                else if (InputMultiSignal[iLoopCount] == (short)DIOEnum.OFF)
                    ControlInvoke(btnInputSignal[iLoopCount], Color.Maroon);
            }
        }

        private void InputSignalChangeCheck()
        {
            for(short iLoopCount = 0; iLoopCount < IOCnt; iLoopCount++)
            {
                if (InputMultiSignal[iLoopCount] != InputMultiSignalPre[iLoopCount])
                {
                    InputMultiSignalPre[iLoopCount] = InputMultiSignal[iLoopCount];

                    if (InputMultiSignal[iLoopCount] == SIGNAL.ON)
                    {
                        //int _BitCommand = AirBlowCmd.BitCheck(iLoopCount);
                        int _BitCommand = DioBaseCmd.InputBitCheck(iLoopCount);
                        if (_BitCommand == DIO_DEF.IN_LIVE) continue;

                        var _InputChangedEvent = InputChangedEvent;
                        _InputChangedEvent?.Invoke(Convert.ToInt16(_BitCommand), Convert.ToBoolean(InputMultiSignal[iLoopCount]));
                    }
                }
            }
        }

        private void OutputSignalCheck()
        {
            if (false == IsInitialize) return;

            short[] _BitNum = new short[IOCnt];
            byte[] _Data = new byte[IOCnt];
            for (int iLoopCount = 0; iLoopCount < IOCnt; ++iLoopCount) _BitNum[iLoopCount] = (short)iLoopCount;

            if (DigitalIO.OutputEchoBackMultiBitData(_BitNum, IOCnt, _Data) != (int)CDioConst.DIO_ERR_SUCCESS)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Output Check Error");
                return;
            }

            for (int iLoopCount = 0; iLoopCount < IOCnt; iLoopCount++)
            {
                if (_Data[iLoopCount] == (short)DIOEnum.ON)         { ControlInvoke(btnOutputSignal[iLoopCount], Color.DarkGreen);  OutputSignalFlag[iLoopCount] = true; }
                else if (_Data[iLoopCount] == (short)DIOEnum.OFF)   { ControlInvoke(btnOutputSignal[iLoopCount], Color.Maroon);     OutputSignalFlag[iLoopCount] = false; }
            }
        }

        private void VisionAliveSignal()
        {
            VisionAliveSignalCount++;
            if (VisionAliveSignalCount >= ALIVE_SIGNAL_TIME)
            {
                VisionAliveSignalFlag = !VisionAliveSignalFlag;
                //SetOutputSignal(DIO_DEF.OUT_LIVE, VisionAliveSignalFlag);
                VisionAliveSignalCount = 0;
            }
        }

        private bool InputAliveCheck()
        {
            bool _Result = true;

            if(InputMultiSignal[DIO_DEF.IN_LIVE] == (short)DIOEnum.OFF)
            {
                InputAliveCheckCount++;
                if(InputAliveCheckCount > ALIVE_CHECK_TIME)
                {
                    var _InputChangedEvent = InputChangedEvent;
                    InputChangedEvent?.Invoke(DIO_DEF.IN_LIVE, false);

                    //CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "DIO Alive Check Count : " + InputAliveCheckCount);

                    InputAliveCheckFlag = false;
                    InputAliveCheckCount = 0;
                    _Result = false;

                    //CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "DIO Alive Check OFF");
                }
            }
            else
            {
                if(InputAliveCheckFlag == false)
                {
                    var _InputChangedEvent = InputChangedEvent;
                    _InputChangedEvent?.Invoke(DIO_DEF.IN_LIVE, true);
                    //InputChangedEvent(DIOMAP.IN_LIVE, true);

                    InputAliveCheckFlag = true;
                    _Result = true;
                    CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "DIO Alive Check ON");
                }
                InputAliveCheckCount = 0;
            }

            return _Result;
        }
		
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

        /// <summary>
        /// 컨트롤 BackColor 변경 Invoke
        /// </summary>
        /// <param name="_Control">Control </param>
        /// <param name="_Color">변경 색</param>
        private void ControlInvoke(Control _Control, Color _Color, string _Msg = null)
        {
            if (_Control.InvokeRequired)
            {
                _Control.Invoke(new MethodInvoker(delegate ()
                {
                    _Control.BackColor = _Color;
                    if (_Msg != null) _Control.Text = _Msg;
                }
                ));
            }
            else
            {
                _Control.BackColor = _Color;
                if (_Msg != null) _Control.Text = _Msg;
            }
        }
        #endregion

        private void ThreadInputIOCheckFunc()
        {
            try
            {
                while (false == IsThreadInputIOCheckExit)
                {
                    InputSignalCheck();
                    InputSignalChangeCheck();
                    Thread.Sleep(25);
                }
            }
            catch(Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadInputIOCheckFunc Err : " + ex.Message, CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void ThreadOutputIOCheckFunc()
        {
            try
            {
                while (false == IsThreadOutputIOCheckExit)
                {
                    OutputSignalCheck();
                    Thread.Sleep(25);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void ThreadVisionAliveSignalFunc()
        {
            Stopwatch _Stopwatch = new Stopwatch();
            try
            {
                while (false == IsThreadVisionAliveSignalExit)
                {
                    VisionAliveSignal();
                    Thread.Sleep(10);
                }
            }
            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadVisionAliveSignalFunc Err : " + ex.Message, CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void ThreadInputAliveCheckFunc()
        {
            try
            {
                while(false == IsThreadInputAliveCheckExit)
                {
                    InputAliveCheck();
                    Thread.Sleep(10);
                }
            }

            catch(System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadInputAliveCheckFunc Err : " + ex.Message, CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void ThreadSignalToggleFunc()
        {
            try
            {
                SleepTime = 10;
                while (false == IsThreadSignalToggleExit)
                {
                    for (int iLoopCount = SignalToggleList.Count - 1; iLoopCount >= 0; --iLoopCount)
                    {
                        SignalToggleList[iLoopCount].ToggleTime -= SleepTime;

                        if (SignalToggleList[iLoopCount].ToggleTime <= 0)
                        {
                            byte _Data = Convert.ToByte(!SignalToggleList[iLoopCount].CurrentSignal);
                            DigitalIO.OutputBitData(SignalToggleList[iLoopCount].Signal, _Data);
                            SignalToggleList.RemoveAt(iLoopCount);
                        }
                    }
                    Thread.Sleep(SleepTime);
                }
            }

            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "ThreadSignalToggleFunc Err : " + ex.Message, CLogManager.LOG_LEVEL.LOW);
            }
        }
    }
}
