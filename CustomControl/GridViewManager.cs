using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CustomControl
{
    public partial class GridViewManager : UserControl
    {
        public delegate void VTPEventHandler(string Address);
        public event VTPEventHandler VTPEvent;

        public delegate void SendNGListHandler(string ErrorCode);
        public event SendNGListHandler SendNGListEvent;

        public bool SettingFlag = false;
        public bool NoneFomatFlag = false;
        public bool IndexFomatFlag = false;
        private double ChartYValue = 0.0;

        public GridViewManager()
        {
            InitializeComponent();
        }

        public void Initialize(int ColumnCount, string[] HeaderName, bool _SettingFlag = false, bool _NoneFomatFlag = false)
        {
            SettingFlag = _SettingFlag;
            IndexFomatFlag = _NoneFomatFlag;
            SetHeader(ColumnCount, HeaderName);
        }

        public void DeInitialize()
        {

        }

        public void SetHeader(int HeaderCount, string[] HeaderName, bool _DataAlignCenterFlag = false)
        {
            if (HeaderCount == 0 || HeaderName == null) return;

            GridViewControl.ColumnCount = HeaderCount;

            for (int iLoopCount = 0; iLoopCount < HeaderCount; iLoopCount++)
            {
                GridViewControl.Columns[iLoopCount].Name = HeaderName[iLoopCount];
                GridViewControl.Columns[iLoopCount].Width = 80;
            }

            if(_DataAlignCenterFlag) GridViewControl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void ClearGridView()
        {
            GridViewControl.Rows.Clear();
        }

        //LDH, Error List 용
        public void IndexInitialize(int ColumnCount, int IndexCount, int RowCount)
        {
            GridViewControl.ColumnHeadersVisible = false;
            GridViewControl.ReadOnly = true;
            GridViewControl.DefaultCellStyle.SelectionBackColor = Color.White;
            GridViewControl.DefaultCellStyle.SelectionForeColor = Color.Black;
            GridViewControl.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            IndexFomatFlag = true;

            string[] InitValue = new string[GridViewControl.Columns.Count];
            int CellValue = 0;

            for (int iLoopcount = 0; iLoopcount < RowCount; iLoopcount++)
            {
                for (int jLoopCount = 0; jLoopCount < GridViewControl.ColumnCount; jLoopCount++)
                {
                    if (jLoopCount % 2 == 0)
                    {
                        CellValue = (jLoopCount / 2) * RowCount + iLoopcount + 1;
                        if (CellValue <= IndexCount) InitValue[jLoopCount] = ((jLoopCount / 2) * RowCount + iLoopcount + 1).ToString();
                        else InitValue[jLoopCount] = " ";
                    }
                    else
                    {
                        CellValue = ((jLoopCount - 1) / 2) * RowCount + iLoopcount + 1;
                        if (CellValue <= IndexCount) InitValue[jLoopCount] = "0";
                        else InitValue[jLoopCount] = " ";
                    }
                }

                GridViewControl.Rows.Add(InitValue);
            }
        }

        //LDH, Error List 용
        public void SetErrorCount(string ErrorCode)
        {
            int CellValue = 0;

            for (int iLoopcount = 0; iLoopcount < GridViewControl.RowCount; iLoopcount++)
            {
                for (int jLoopCount = 0; jLoopCount < GridViewControl.ColumnCount; jLoopCount++)
                {
                    if (ErrorCode == GridViewControl.Rows[iLoopcount].Cells[jLoopCount].Value.ToString())
                    {
                        CellValue = Convert.ToInt32(GridViewControl.Rows[iLoopcount].Cells[jLoopCount + 1].Value);

                        ChartYValue = CellValue + 1;
                        GridViewControl.Rows[iLoopcount].Cells[jLoopCount + 1].Value = ChartYValue;                        
                    }
                }
            }
        }

        public double GetYValue()
        {
            return ChartYValue;
        }

        public void SetGridViewData(string[] Data, bool AddLastRow = false)
        {
            if (AddLastRow == true)
            {
                if (GridViewControl.InvokeRequired)
                {
                    GridViewControl.Invoke(new MethodInvoker(delegate() { GridViewControl.Rows.Add(Data); }));
                }
                else { GridViewControl.Rows.Add(Data); }
            }

            else
            {
                if (GridViewControl.InvokeRequired)
                {
                    GridViewControl.Invoke(new MethodInvoker(delegate() { GridViewControl.Rows.Insert(0, Data); }));
                }
                else { GridViewControl.Rows.Insert(0, Data); }

                GridViewControl.Rows[0].Selected = true;
            }

            if (SendNGListEvent != null) SendNGListEvent(Data[5]);
        }

        public void SetGridViewNGData(string[] Data, bool AddLastRow = false)
        {
            if (AddLastRow == true)
            {
                if (GridViewControl.InvokeRequired)
                {
                    GridViewControl.Invoke(new MethodInvoker(delegate() { GridViewControl.Rows.Add(Data); }));
                }
                else { GridViewControl.Rows.Add(Data); }
            }

            else
            {
                if (GridViewControl.InvokeRequired)
                {
                    GridViewControl.Invoke(new MethodInvoker(delegate() { GridViewControl.Rows.Insert(0, Data); }));
                }
                else { GridViewControl.Rows.Insert(0, Data); }

                GridViewControl.Rows[0].Selected = true;
            }
        }

        public void SetGridViewCellData(string Data, int RowIndex, int ColumnIndex)
        {
            if (GridViewControl.InvokeRequired)
            {
                GridViewControl.Invoke(new MethodInvoker(delegate() { GridViewControl.Rows[RowIndex].Cells[ColumnIndex].Value = Data; }));
            }
            else { GridViewControl.Rows[RowIndex].Cells[ColumnIndex].Value = Data; }

            //GridViewControl.Rows[0].Selected = true;
        }

        public void SetColumnsWidth(int[] ColumnWidth, bool ColumnHeaderUse = true)
        {
            GridViewControl.ColumnHeadersVisible = ColumnHeaderUse;

            for (int iLoopcount = 0; iLoopcount < ColumnWidth.Count(); iLoopcount++)
            {
                GridViewControl.Columns[iLoopcount].Width = ColumnWidth[iLoopcount];
            }
        }

        public void SetAllColumnsWidth(int ColumnWidth, int ColumnCount)
        {
            for (int iLoopcount = 0; iLoopcount < ColumnCount; iLoopcount++)
            {
                GridViewControl.Columns[iLoopcount].Width = ColumnWidth;
            }
        }

        public void SetCSVData(string FolderPath, string FileName, int HeaderCount)
        {
            ClearGridView();

            string FilePath;
            if(FolderPath == null) FilePath = FileName;
            else FilePath = FolderPath + "\\" + FileName;

            string[] item;
            int count = 0;
            bool HeaderFlag = true;

            try
            {
                StreamReader file = new StreamReader(FilePath, Encoding.GetEncoding("ks_c_5601-1987"));
                Encoding en = file.CurrentEncoding;

                using (CsvFileReader reader = new CsvFileReader(FilePath, en))
                {
                    CsvRow row = new CsvRow();
                    while (reader.ReadRow(row))
                    {
                        if (!HeaderFlag)
                        {
                            count = 0;
                            item = new string[row.Count()];
                            foreach (string Value in row)
                            {
                                if (Value == null) item[count++] = "";
                                else item[count++] = Value;
                            }
                            SetGridViewData(item, true);
                        }
                        HeaderFlag = false;
                    }
                }

                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Importing a file list.");
            }
        }

        private void GridViewControl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (SettingFlag == true)
            {
                if (GridViewControl.Rows[e.RowIndex].Cells[0].FormattedValue.ToString() == "") { MessageBox.Show("Address is not defined."); return; }

                VTPEvent(GridViewControl.Rows[e.RowIndex].Cells[1].FormattedValue.ToString());
                GridViewControl.Rows[e.RowIndex].Selected = true;
            }
        }

        private void GridViewControl_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (IndexFomatFlag)
            {
                if (e.ColumnIndex == 0 && e.Value != null && e.Value.ToString() != "")
                {
                    for(int iLoopCount = 0; iLoopCount < GridViewControl.ColumnCount; iLoopCount++) GridViewControl.Rows[e.RowIndex].Cells[iLoopCount].Style.BackColor = Color.Beige;
                }
            }
        }

        public string[] SearchTitle(string ErrorCode)
        {
            string[] ErrorInfo = new string[4];

            for (int iLoopcount = 0; iLoopcount < GridViewControl.RowCount; iLoopcount++)
            {
                if (ErrorCode == GridViewControl.Rows[iLoopcount].Cells[1].Value.ToString()) 
                {
                    ErrorInfo[0] = GridViewControl.Rows[iLoopcount].Cells[0].Value.ToString();
                    ErrorInfo[1] = GridViewControl.Rows[iLoopcount].Cells[1].Value.ToString();
                    ErrorInfo[2] = GridViewControl.Rows[iLoopcount].Cells[2].Value.ToString();

                    //LDH, 에러 내용 입력 안된 경우 예외처리
                    if (GridViewControl.Rows[iLoopcount].Cells[3].Value == null) ErrorInfo[3] = "";
                    else ErrorInfo[3] = GridViewControl.Rows[iLoopcount].Cells[3].Value.ToString();
                    
                    break;
                }
            }
            return ErrorInfo;
        }

        public void ChangeTitle(string[] ErrorInfo)
        {
            for (int iLoopcount = 0; iLoopcount < GridViewControl.RowCount; iLoopcount++)
            {
                if (ErrorInfo[1] == GridViewControl.Rows[iLoopcount].Cells[1].Value.ToString())
                {
                    GridViewControl.Rows[iLoopcount].Cells[0].Value = ErrorInfo[0];
                    GridViewControl.Rows[iLoopcount].Cells[1].Value = ErrorInfo[1];
                    GridViewControl.Rows[iLoopcount].Cells[2].Value = ErrorInfo[2];
                    GridViewControl.Rows[iLoopcount].Cells[3].Value = ErrorInfo[3];
                    break;
                }
            }
        }

        public int GetGridViewRowCount()
        {
            return GridViewControl.RowCount;
        }

        public DataGridView GetGridView()
        {
            return GridViewControl;
        }
    }
}
