using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using System.Reflection;

using ParameterManager;
using CustomControl;
using LogMessageManager;

namespace MitsubishiCommunicationManager
{
    public partial class MitsubishiCommunicationWindow : Form
    {
        private eProjectType ProjectType = eProjectType.NONE;
        private string CommonFolderPath = "";
        public bool IsShowMitsuCommWindow = false;
        public bool IsSimulationMode = false;

        private string[] PLCHeader = new string[] { "Item", "Address", "Size", "Bit", "Word" };
        private int[] PLCColumnWidth = new int[] { 250, 60, 40, 60, 60 };
        private string[] VisionHeader = new string[] { "Item", "Address", "Size", "Bit", "Word" };
        private int[] VisionColumnWidth = new int[] { 250, 60, 40, 60, 60 };

        private string PLCStartAddr = "1000";

        string PLCDataFilePath = "D:\\VisionInspectionData\\SCAlignInspection" + "\\PTV_Aligner.csv";
        string VisionDataFilePath = "D:\\VisionInspectionData\\SCAlignInspection" + "\\VTP_Aligner.csv";

        private string PTVListFileName = "PTV_Aligner.csv";
        private string VTPListFileName = "VTP_Aligner.csv";

        #region Initialize
        public MitsubishiCommunicationWindow(bool _IsSimulation = false)//(int _ProjectType = 1, string _CommonFolderPath = "")
        {
            InitializeComponent();

            ResultCommData.BitData = new short[64];
            ResultCommData.WordData = new int[64];
            ResultCommData.MainBitData = new short[2];

            PLCCommData.BitData = new short[64];
            PLCCommData.WordData = new short[64];
            PLCCommPreData.BitData = new short[64];
            PLCCommPreData.WordData = new short[64];
        }

        public void Initialize(int _ProjectType = 0, string _CommonFolderPath = "")
        {
            CommonFolderPath = _CommonFolderPath;
            ProjectType = (eProjectType)_ProjectType;
            if (ProjectType == eProjectType.SCALIGN) { PTVListFileName = "PTV_Aligner.csv"; VTPListFileName = "VTP_Aligner.csv"; }

            ResultMemMapFile = MemoryMappedFile.CreateNew("ResultMemMappedFile", 8192, MemoryMappedFileAccess.ReadWrite);

            //Receive Thread -> X
            ThreadReceiveData = new Thread(ThreadReceiveDataFunc);
            ThreadReceiveData.IsBackground = true;

            //Send Thread -> X
            ThreadReadResultData = new Thread(ThreadReadResultDataFunc);
            ThreadReadResultData.IsBackground = true;
            IsThreadReadResultDataExit = false;

            ThreadAliveData = new Thread(ThreadAliveDataFunc);
            ThreadAliveData.IsBackground = true;

            PLCDataViewInitialize();
        }

        public void AliveStart()
        {
            ThreadAliveData.Start();
            IsThreadAliveDataExit = false;
        }

        private void PLCDataViewInitialize()
        {
            gridViewPTV.Initialize(PLCHeader.Count(), PLCHeader, false, true);
            gridViewPTV.SetColumnsWidth(PLCColumnWidth);

            gridViewVTP.Initialize(VisionHeader.Count(), VisionHeader, true, true);
            gridViewVTP.SetColumnsWidth(VisionColumnWidth);
            gridViewVTP.VTPEvent += new GridViewManager.VTPEventHandler(SetVTPTextMessage);

            PLCDataFilePath = "D:\\VisionInspectionData\\SCAlignInspection\\" + PTVListFileName;
            VisionDataFilePath = "D:\\VisionInspectionData\\SCAlignInspection\\" + VTPListFileName;

            if (!File.Exists(PLCDataFilePath))
            {
                MessageBox.Show("PTV data file is not found."); return;
            }

            SetPLCData(gridViewPTV, PLCHeader.Count(), PLCDataFilePath);
            SetPLCData(gridViewVTP, VisionHeader.Count(), VisionDataFilePath);
        }

        public void ShowMitsuCommWindow()
        {
            IsShowMitsuCommWindow = true;
            this.Show();
        }

        public void DeInitialize()
        {
            if (ThreadReceiveData != null)        { IsThreadReceiveDataExit = true;     Thread.Sleep(100);  ThreadReceiveData = null; }
            if (ThreadReadResultData != null)     { IsThreadReadResultDataExit = true;  Thread.Sleep(100);  ThreadReadResultData = null; }
            if (ThreadAliveData != null)          { IsThreadAliveDataExit = true;       Thread.Sleep(100);  ThreadAliveData = null; }
            if (true == IsShowMitsuCommWindow)    { IsShowMitsuCommWindow = false;   this.Hide(); }

            //ResultCommData.MainBitData[0] = 1;
            ResultDataMemoryMapping();
        }
        #endregion

        #region Control Default Event
        private void MitsubishiCommunicationWindow_KeyDown(object sender, KeyEventArgs e)
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
        private void btnInspectionRequest_Click(object sender, EventArgs e)
        {
            //ResultCommData.WordData[PTVAddr.V1_INSP_REQ] = 1;
            //ResultCommData.WordData[PTVAddr.V1_RETRY_CNT] = 0;
            //ResultDataMemoryMapping();

            PLCCommData.WordData[PTVAddr.V1_INSP_REQ] = 1;
            PLCCommData.WordData[PTVAddr.V1_RETRY_CNT] = 0;
            ResultDataMemoryMapping();
        }

        private void labelExit_Click(object sender, EventArgs e)
        {
            IsShowMitsuCommWindow = false;
            this.Hide();
        }
        #endregion

        #region GridView Invoke
        /// <summary>
        /// LDH, GridView Vision Data Update
        /// </summary>
        private void GridViewUpdateVisionData()
        {
            if (PLCCommData.BitData != null)
                //for (int iLoopCount = 0; iLoopCount < PLCCommData.BitData.Length; ++iLoopCount)
                for (int iLoopCount = 0; iLoopCount < gridViewVTP.GetGridViewRowCount(); ++iLoopCount)
                    GridViewInvoke(gridViewVTP, iLoopCount, 3, ResultCommData.BitData[iLoopCount].ToString());
            if (PLCCommData.WordData != null)
                //for (int iLoopCount = 0; iLoopCount < PLCCommData.WordData.Length; ++iLoopCount)
                for (int iLoopCount = 0; iLoopCount < gridViewVTP.GetGridViewRowCount(); ++iLoopCount)
                    GridViewInvoke(gridViewVTP, iLoopCount, 4, ResultCommData.WordData[iLoopCount].ToString());
        }

        /// <summary>
        /// LDH, GridView PTV Data Update
        /// </summary>
        private void GridViewUpdateReadData()
        {
            try
            {
                //for (int iLoopCount = 0; iLoopCount < PLCCommData.BitData.Length; ++iLoopCount)
                for (int iLoopCount = 0; iLoopCount < gridViewPTV.GetGridViewRowCount(); ++iLoopCount)
                    GridViewInvoke(gridViewPTV, iLoopCount, 3, PLCCommData.BitData[iLoopCount].ToString());

                //for (int iLoopCount = 0; iLoopCount < PLCCommData.WordData.Length; ++iLoopCount)
                for (int iLoopCount = 0; iLoopCount < gridViewPTV.GetGridViewRowCount(); ++iLoopCount)
                    GridViewInvoke(gridViewPTV, iLoopCount, 4, PLCCommData.WordData[iLoopCount].ToString());
            }
            catch
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "GridViewUpdateReadData Func Exception");
            }
        }

        /// <summary>
        /// LDH, GridView Clear Invoke
        /// </summary>
        /// <param name="_GridView"></param>
        private void GridViewClearInvoke(GridViewManager _GridView)
        {
            if (_GridView.InvokeRequired)
            {
                _GridView.Invoke(new MethodInvoker(delegate ()
                {
                    _GridView.ClearGridView();
                }));
            }

            else
            {
                _GridView.ClearGridView();
            }
        }

        /// <summary>
        /// LDH, GridView Add Invoke
        /// </summary>
        /// <param name="_GridView"></param>
        /// <param name="RowIndex"></param>
        /// <param name="ColumnIndex"></param>
        /// <param name="_Msg"></param>
        private void GridViewInvoke(GridViewManager _GridView, int RowIndex, int ColumnIndex, string _Msg)
        {
            if (_GridView.InvokeRequired)
            {
                _GridView.Invoke(new MethodInvoker(delegate ()
                {
                    _GridView.SetGridViewCellData(_Msg, RowIndex, ColumnIndex);
                }));
            }

            else
            {
                _GridView.SetGridViewCellData(_Msg, RowIndex, ColumnIndex);
            }
        }

        public void SetPLCData(GridViewManager _GridView, int HeaderCount, string FilePath)
        {
            string[] item = new string[HeaderCount];
            int count = 0;

            StreamReader file = new StreamReader(FilePath, Encoding.Default);
            Encoding en = file.CurrentEncoding;

            using (CsvFileReader reader = new CsvFileReader(FilePath, en))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    count = 0;
                    item = new string[HeaderCount];
                    foreach (string s in row)
                    {
                        item[count++] = s;
                    }
                    _GridView.SetGridViewData(item, true);
                }
            }
        }
        #endregion GridView Invoke

        #region PLC Data Set / Get Function
            //public void SetModeStatus(int _Addr, short _Value)
            //{
            //    ResultCommData.WordData[_Addr] = _Value;
            //}

        public void UpdateVisionData()
        {
            GridViewUpdateReadData();
        }

        public bool PTVWordDataChangeCheck(int _Addr)
        {
            bool _Result = false;

            if (PLCCommData.WordData[_Addr] != PLCCommPreData.WordData[_Addr])
            {
                PLCCommPreData.WordData[_Addr] = PLCCommData.WordData[_Addr];
                _Result = true;
            }

            return _Result;
        }

        public short GetPTVWordData(int _Addr)
        {
            return PLCCommData.WordData[_Addr];
        }

        public short[] GetPTVDWordData(int _Addr)
        {
            short[] _Values = new short[2];
            _Values[0] = PLCCommData.WordData[_Addr];
            _Values[1] = PLCCommData.WordData[_Addr + 1];

            return _Values;
        }

        public int GetVTPWordData(int _Addr)
        {
            return ResultCommData.WordData[_Addr];
        }

        public void SetVTPWordData(int _Addr, short _Value, bool _IsMapping = true)
        {
            ResultCommData.WordData[_Addr] = _Value;
            if (true == _IsMapping) ResultDataMemoryMapping();
        }

        public void SetVTPDWordData(int _Addr, short _LowValue, short _HiValue, bool _IsMapping = true)
        {
            ResultCommData.WordData[_Addr] = _LowValue;
            ResultCommData.WordData[_Addr + 1] = _HiValue;
            if (true == _IsMapping) ResultDataMemoryMapping();
        }

        private void SetVTPTextMessage(string _Adress)
        {
            textBoxAddr.Text = _Adress;
        }
        #endregion
    }
}

#region Memory Map Data
[Serializable]
public class PLCCommunicationData
{
    public short[] BitData;
    public short[] WordData;
    //public short[] DWordData;
}
[Serializable]
public class ResultCommunicationData
{
    //public short[] ResultData;
    public int[] WordData;
    public short[] BitData;
    public short[] MainBitData;
}

sealed class AllowAllAssemblyVersionsDeserializationBinder : System.Runtime.Serialization.SerializationBinder
{
    public override Type BindToType(string _AssemblyName, string _TypeName)
    {
        Type _TypeToDeserialize = null;
        String _CurrentAssembly = Assembly.GetExecutingAssembly().FullName;

        // In this case we are always using the current assembly
        _AssemblyName = _CurrentAssembly;

        // Get the type using the typeName and assemblyName
        _TypeToDeserialize = Type.GetType(String.Format("{0}, {1}", _TypeName, _AssemblyName));

        return _TypeToDeserialize;
    }
}
#endregion
