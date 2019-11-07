using ParameterManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HistoryManager
{
    public partial class HistoryWindow : Form
    {
        private DataSet HistorySet = new DataSet();
        private string ProjectName;

        string SelectDateFrom;
        string SelectDateTo;
        static string RecipeName;
        bool SelectRecipeFlag = false;
        int Recipecount = 0;
        eProjectType ProjectType;
        int ScreenshotIndex = 0;

        Panel RecipePanel = new Panel();
        CheckedListBox RecipeCheckBox = new CheckedListBox();

        public HistoryWindow()
        {
            InitializeComponent();
        }

        public void Initialize(string _ProjectName, eProjectType _ProjectType)
        {
            ProjectName = _ProjectName;
            ProjectType = _ProjectType;

            SubHistoryGridView();
            
            DateTime TimeNow = DateTime.Now;
            SelectDateFrom = String.Format("{0:D4}-{1:D2}-{2:D2}", TimeNow.Year, TimeNow.Month, TimeNow.Day);
            SelectDateTo = String.Format("{0:D4}-{1:D2}-{2:D2}", TimeNow.Year, TimeNow.Month, TimeNow.Day);

            monthCalendarFrom.SetDate(TimeNow);
            monthCalendarTo.SetDate(TimeNow);

            HistoryToolTip.SetToolTip(btnSelectView, "View selected period");
            HistoryToolTip.SetToolTip(btnAllview, "View all");
            HistoryToolTip.SetToolTip(btnSelectDelete, "Delete selected Period");
            HistoryToolTip.SetToolTip(btnAllDelete, "Delete All");

            SqliteManager.SetHistoryFolderPath(ProjectName);
        }

        private void SubHistoryGridView()
        {
            if(ProjectType == eProjectType.NONE)
            {
                string[] _Header = new string[12] { "Num", "Length", "Bent", "S-Burr", "S-Nick", "T-Burr", "SideX", "SideY", "ChipOut", "Gate", "Count", "Exist"};
                int[] _ColumnsWidth = new int[12] { 34, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 60 };
                dataGridViewSubHistory.Initialize(12, _Header);
                dataGridViewSubHistory.SetColumnsWidth(_ColumnsWidth);

                ScreenshotIndex = 4;
            }
            else
            {
                dataGridViewHistory.Width = 1254;
                dataGridViewSubHistory.Visible = false;

                ScreenshotIndex = 4;
            }
        }

        public void GridViewInitialize()
        {
            dataGridViewHistory.Columns[0].Width = 160;
            dataGridViewHistory.Columns[1].Width = 168;
            dataGridViewHistory.Columns[2].Width = 75;
            dataGridViewHistory.Columns[3].Width = 75;
            dataGridViewHistory.Columns[4].Width = 80;
            dataGridViewHistory.Columns[5].Width = 230;
            dataGridViewHistory.Columns[6].Width = 230;
            dataGridViewHistory.Columns[7].Width = 255;
        }

        public bool CheckDBFile()
        {
            if (SqliteManager.CheckDirectory() == null) { MessageBox.Show(new Form { TopMost = true }, "DB File not found."); return false; }
            else return true;
        }

        public void SetDataGridViewHistory(bool SelectDate, string SelectSQL, string SelectedDateFrom = null, string SelectedDateTo = null)
        {
            ClearDataGridViewHistory();

            SetSearchCondition();

            switch (SelectSQL)
            {
                case "Open": dataGridViewHistory.DataSource = SqliteManager.SqlOpen(SelectDate, SelectedDateFrom, SelectedDateTo).Tables[0]; break;
                case "Delete": dataGridViewHistory.DataSource = SqliteManager.SqlDelete(SelectDate, SelectedDateFrom, SelectedDateTo).Tables[0]; break;
            }
            
            GridViewInitialize();
            dataGridViewHistory.Refresh();          
            //GridViewInitialize();
        }

        public void ClearDataGridViewHistory()
        {
            if (dataGridViewHistory.DataSource != null)
            {
                dataGridViewHistory.DataSource = null;
                dataGridViewHistory.Rows.Clear();
                dataGridViewHistory.Refresh();
            }

            //pictureBoxScreenshot.Image = null;
            //pictureBoxScreenshot.Refresh();

            kpCogDisplayHistory.ClearDisplay(true);
            dataGridViewSubHistory.ClearGridView();
        }

        public void ClearSearchOption()
        {            
            ckListBoxRecipe.Items.Clear();
            ckListBoxNGType.Items.Clear();
            comboBoxResult.Items.Clear();
            SetckListBoxRecipe();
            SetckListBoxNGType();
            SetcomboBoxResult();
        }

        //LDH, 2016.12.13 Inspection ScreenShot 삭제
        private void DeleteScreenShot(bool _DeleteAll)
        {
            string ScreenShotpath;
            ScreenShotpath = SqliteManager.SqlGetScreenshotPath();

            if (ScreenShotpath == null) { MessageBox.Show(new Form { TopMost = true }, "History File is not found."); return; }

            char Character = '\\';
            string[] SplitFolderName = ScreenShotpath.Split(Character);
            int FolderLength = SplitFolderName.Length;

            string RemoveFolderPath = "";
            string DeleteFolder;

            int YearIndex = 0;
            for(int iLoopCount = 0; iLoopCount < 5; iLoopCount++)
            {
                //폴더 경로내에 최근 5년 까지의 폴더이름이 있는지 체크
                YearIndex = Array.IndexOf(SplitFolderName, (DateTime.Now.Year - iLoopCount).ToString());
                if (YearIndex != -1) break;
            }

            for(int jLoopCount = YearIndex; jLoopCount < SplitFolderName.Length; jLoopCount++)
            {
                RemoveFolderPath = RemoveFolderPath + "\\" + SplitFolderName[jLoopCount];
            }
            ScreenShotpath = ScreenShotpath.Replace(RemoveFolderPath, "");

            //Year 폴더 삭제
            DirectoryInfo DeleteYearFolderInfo = new DirectoryInfo(ScreenShotpath);
            if (DeleteYearFolderInfo.Exists)
            {
                DirectoryInfo[] YearFolderInfo = DeleteYearFolderInfo.GetDirectories();
                foreach (DirectoryInfo YearFolder in YearFolderInfo)
                {
                    if (_DeleteAll)
                    {
                        if (Convert.ToInt32(YearFolder.Name) <= Convert.ToInt32(DateTime.Now.Year))
                        {
                            DeleteFolder = String.Format("{0}\\{1}", ScreenShotpath, YearFolder.Name);
                            Directory.Delete(DeleteFolder, true);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(YearFolder.Name) < Convert.ToInt32(SplitFolderName[YearIndex]))
                        {
                            DeleteFolder = String.Format("{0}\\{1}", ScreenShotpath, YearFolder.Name);
                            Directory.Delete(DeleteFolder, true);
                        }
                    }
                }
                DeleteYearFolderInfo.Refresh();
            }

            //Month 폴더 삭제
            ScreenShotpath = String.Format("{0}\\{1}", ScreenShotpath, SplitFolderName[YearIndex]);
            DirectoryInfo DeleteMonthFolderInfo = new DirectoryInfo(ScreenShotpath);
            if (DeleteMonthFolderInfo.Exists)
            {
                DirectoryInfo[] MonthFolderInfo = DeleteMonthFolderInfo.GetDirectories();
                foreach (DirectoryInfo MonthFolder in MonthFolderInfo)
                {
                    if (Convert.ToInt32(MonthFolder.Name) < Convert.ToInt32(SplitFolderName[YearIndex + 1]))
                    {
                        DeleteFolder = String.Format("{0}\\{1}", ScreenShotpath, MonthFolder.Name);
                        Directory.Delete(DeleteFolder, true);
                    }
                }
                DeleteMonthFolderInfo.Refresh();
            }

            //Day 폴더 삭제
            ScreenShotpath = String.Format("{0}\\{1}", ScreenShotpath, SplitFolderName[YearIndex + 1]);
            DirectoryInfo DeleteDayFolderInfo = new DirectoryInfo(ScreenShotpath);
            if (DeleteDayFolderInfo.Exists)
            {
                DirectoryInfo[] DayFolderInfo = DeleteDayFolderInfo.GetDirectories();
                foreach (DirectoryInfo DayFolder in DayFolderInfo)
                {
                    if (Convert.ToInt32(DayFolder.Name) < Convert.ToInt32(SplitFolderName[YearIndex + 2]))
                    {
                        DeleteFolder = String.Format("{0}\\{1}", ScreenShotpath, DayFolder.Name);
                        Directory.Delete(DeleteFolder, true);
                    }
                }
                DeleteDayFolderInfo.Refresh();
            }
        }

        private bool SelectDateCheck()
        {
            DateTime TimeNow = DateTime.Now;

            if (0 > TimeNow.DayOfYear - monthCalendarTo.SelectionStart.DayOfYear)
            {
                if (TimeNow.Year <= monthCalendarTo.SelectionStart.Year)
                {
                    MessageBox.Show(new Form { TopMost = true }, "[End date] must be selected prior to today's date.");
                    return false;
                }
            }

            if (0 > monthCalendarTo.SelectionStart.DayOfYear - monthCalendarFrom.SelectionStart.DayOfYear)
            {
                if (monthCalendarTo.SelectionStart.Year <= monthCalendarFrom.SelectionStart.Year)
                {
                    MessageBox.Show(new Form { TopMost = true }, "[Start date] must be selected prior to [End Date].");
                    return false;
                }
            }

            return true;
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

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }
        #endregion Control Default Event

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSelectView_Click(object sender, EventArgs e)
        {
            if (SelectDateCheck() == false) return;

            SetDataGridViewHistory(true, "Open", SelectDateFrom, SelectDateTo);            
        }

        private void btnAllview_Click(object sender, EventArgs e)
        {
            SetDataGridViewHistory(false, "Open"); 
        }

        private void btnSelectDelete_Click(object sender, EventArgs e)
        {
            if (SelectDateCheck() == false) return;

            DialogResult DeleteDialog = MessageBox.Show("Are you sure you want to delete records by " + SelectDateTo + "?", "History Delete", MessageBoxButtons.YesNo);

            if (DeleteDialog == DialogResult.Yes)
            {                
                SetDataGridViewHistory(true, "Delete", SelectDateFrom, SelectDateTo);
                DeleteScreenShot(false);
            }
        }

        private void btnAllDelete_Click(object sender, EventArgs e)
        {
            DialogResult DeleteDialog = MessageBox.Show("Are you sure you want to delete all?", "History Delete", MessageBoxButtons.YesNo);

            if (DeleteDialog == DialogResult.Yes)
            {
                DeleteScreenShot(true);
                SetDataGridViewHistory(false, "Delete");
            }
        }

        private void monthCalendarFrom_DateChanged(object sender, DateRangeEventArgs e)
        {
            SelectDateCheck();

            SelectDateFrom = monthCalendarFrom.SelectionStart.ToString("yyyy-MM-dd");
            dateTimePickerFrom.Value = monthCalendarFrom.SelectionStart.Date;
        }

        private void monthCalendarTo_DateChanged(object sender, DateRangeEventArgs e)
        {
            SelectDateCheck();

            SelectDateTo = monthCalendarTo.SelectionStart.ToShortDateString();
            dateTimePickerTo.Value = monthCalendarTo.SelectionStart.Date;
        }

        private void dataGridViewHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string ScreenshotImagePath = dataGridViewHistory.Rows[e.RowIndex].Cells[ScreenshotIndex].Value.ToString();
            SelectHistoryData(ScreenshotImagePath);
        }

        private void SelectHistoryData(string _ScreenshotImagePath)
        {
            if (_ScreenshotImagePath == "") { MessageBox.Show(new Form { TopMost = true }, "Image not found"); return; }
            else if (File.Exists(_ScreenshotImagePath) == false) { MessageBox.Show(new Form { TopMost = true }, "Image not found"); return; }

            //pictureBoxScreenshot.Image = Bitmap.FromFile(_ScreenshotImagePath);
            //File.ReadAllBytes(_ScreenshotImagePath);

            kpCogDisplayHistory.SetDisplayColorImage(_ScreenshotImagePath);
            SetSubHistory(_ScreenshotImagePath);

        }

        private void SetcomboBoxResult()
        {
            if (ProjectType == eProjectType.NONE)
            {
                ckbResult.Text = "CamType";
                ckbRecipe.Text = "ModelName";
                btnDropDownRecipe.Text = "Model List";
                //comboBoxResult.Items.Add("All");
                comboBoxResult.Items.Add("TOP");
                comboBoxResult.Items.Add("SIDE");
                comboBoxResult.SelectedIndex = 0;
            }
            else
            {
                comboBoxResult.Items.Add("All");
                comboBoxResult.Items.Add("OK");
                comboBoxResult.Items.Add("NG");
                comboBoxResult.SelectedIndex = 0;
            }
        }

        private void SetckListBoxRecipe()
        {
            if (eProjectType.NONE == ProjectType) LoadModelList();
            else                                       LoadRecipeList();

            int Height = 0;

            if (Recipecount < 6) { Height = 28 * Recipecount; }
            else { Height = 28 * 6; }
            //ckListBoxRecipe.Location = new System.Drawing.Point(883, 392);
            ckListBoxRecipe.Location = new System.Drawing.Point(890, 359);
            ckListBoxRecipe.Size = new System.Drawing.Size(360, Height);
        }

        private void SetckListBoxNGType()
        {
            ckListBoxNGType.Items.Add("OK");
            ckListBoxNGType.Items.Add("FIDU_NG");
            ckListBoxNGType.Items.Add("OCR_NG");

            int Height = 28 * 3;
            ckListBoxNGType.Location = new System.Drawing.Point(883, 478);
            ckListBoxNGType.Size = new System.Drawing.Size(369, Height);
        }
        
        private void LoadRecipeList()
        {
            string _RecipeFolderPath = String.Format(@"D:\VisionInspectionData\{0}\RecipeParameter", ProjectName);
            DirectoryInfo _DirInfo = new DirectoryInfo(_RecipeFolderPath);
            if (true == _DirInfo.Exists)
            {
                ckListBoxRecipe.Items.Clear();
                DirectoryInfo[] _DirInfos = _DirInfo.GetDirectories();
                foreach (DirectoryInfo _DInfo in _DirInfos)
                {
                    ckListBoxRecipe.Items.Add(_DInfo.Name);
                }
            }
            Recipecount = ckListBoxRecipe.Items.Count;
        }

        private void LoadModelList()
        {
            ckListBoxRecipe.Items.Clear();

            foreach(string _ModelName in SearchColumnName("ModelName"))
            {
                ckListBoxRecipe.Items.Add(_ModelName);
            }
            Recipecount = ckListBoxRecipe.Items.Count;
        }

        //LDH, 2019.06.28, DB에서 특정 Column에 있는 데이터 유형 찾기
        private List<string> SearchColumnName(string _ColumnName)
        {
            List<string> _GetDataList = new List<string>();
            SqliteManager.SqlDistinct(ref _GetDataList, _ColumnName);

            return _GetDataList;
        }

        private void SetSearchCondition()
        {
            if (ckbRecipe.Checked == true)
            {
                List<string> RecipeList = new List<string>();

                for (int iLoopCount = 0; iLoopCount < Recipecount; iLoopCount++)
                {
                    if (ckListBoxRecipe.GetItemChecked(iLoopCount))
                    {
                        RecipeList.Add(ckListBoxRecipe.Items[iLoopCount].ToString());
                    }
                }
                SqliteManager.SqlSetSearchCondition(ckbRecipe.Text, ckbRecipe.Checked, RecipeList);
            }
            else SqliteManager.SqlSetSearchConditionClear(ckbRecipe.Text, false);

            if (ckbResult.Checked == true) 
            {
                List<string> NGResultList = new List<string>();
                NGResultList.Add(comboBoxResult.Text);
                SqliteManager.SqlSetSearchCondition(ckbResult.Text, ckbResult.Checked, NGResultList);
            }
            else SqliteManager.SqlSetSearchConditionClear(ckbResult.Text, false);

            if (ckbNGType.Checked == true)
            {
                List<string> NGTypeList = new List<string>();

                for (int iLoopCount = 0; iLoopCount < ckListBoxNGType.Items.Count; iLoopCount++)
                {
                    if (ckListBoxNGType.GetItemChecked(iLoopCount))
                    {
                        NGTypeList.Add(ckListBoxNGType.Items[iLoopCount].ToString());
                    }
                }
                SqliteManager.SqlSetSearchCondition("NGType", ckbNGType.Checked, NGTypeList);
            }
            else SqliteManager.SqlSetSearchConditionClear("NGType", false);

            GC.Collect();
        }

        private void btnDropDownRecipe_Click(object sender, EventArgs e)
        {
            if (ckListBoxRecipe.Visible == false) { ckListBoxRecipe.Visible = true; }
            else { ckListBoxRecipe.Visible = false; }
        }

        private void ckbRecipe_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbRecipe.Checked == true) btnDropDownRecipe.Enabled = true;
            else { btnDropDownRecipe.Enabled = false; ckListBoxRecipe.Visible = false; }
        }

        private void ckbResult_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbResult.Checked == true) comboBoxResult.Enabled = true;
            else { comboBoxResult.Enabled = false; comboBoxResult.SelectedIndex = 0; }
        }

        private void ckbNGType_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNGType.Checked == true) { btnNGType.Enabled = true; btnNGType.Text = ""; }
            else { btnNGType.Enabled = false; ckListBoxNGType.Visible = false; btnNGType.Text = "All"; }
        }

        private void btnNGType_Click(object sender, EventArgs e)
        {
            if (ckListBoxNGType.Visible == false) { ckListBoxNGType.Visible = true; }
            else { ckListBoxNGType.Visible = false; }
        }

        private void ckListBoxNGType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNGType.Text = "";

            for (int iLoopCount = 0; iLoopCount < ckListBoxNGType.Items.Count; iLoopCount++)
            {
                if (ckListBoxNGType.GetItemChecked(iLoopCount))
                {
                    if (btnNGType.Text == "") btnNGType.Text = ckListBoxNGType.Items[iLoopCount].ToString();
                    else btnNGType.Text = btnNGType.Text + ", " + ckListBoxNGType.Items[iLoopCount].ToString();
                }
            }
        }

        private void comboBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxResult.SelectedItem.ToString() == "All") { ckbResult.Checked = false; ckbResult_CheckedChanged(sender, e); }
        }

        private void dataGridViewHistory_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string Number = (e.RowIndex + 1).ToString();

                SizeF stringSize = e.Graphics.MeasureString(Number, Font);

                PointF StringPoint = new PointF
                (
                    Convert.ToSingle(dataGridViewHistory.RowHeadersWidth - 3 - stringSize.Width),
                    Convert.ToSingle(e.RowBounds.Y) + dataGridViewHistory[0, e.RowIndex].ContentBounds.Height * 0.3f
                );

                e.Graphics.DrawString(Number, Font, Brushes.Black, StringPoint.X, StringPoint.Y);
            }
        }

        private void dataGridViewHistory_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewHistory.RowCount == 1 || dataGridViewHistory.SelectedRows.Count == 0) return;
            string ScreenshotImagePath = dataGridViewHistory.SelectedCells[ScreenshotIndex].Value.ToString();
            SelectHistoryData(ScreenshotImagePath);
        }

        //LDH, 2019.06.27, TrimForm용 Lead Data GridView set
        private void SetSubHistory(string _CSVFolderPath)
        {
            string CSVFolderPath = Path.GetDirectoryName(_CSVFolderPath);
            string CSVFileName = Path.GetFileNameWithoutExtension(_CSVFolderPath);
            CSVFileName = CSVFileName.Substring(0, CSVFileName.Length - 5);

            string[] ArrCSVFileName = CSVFileName.Split('_');
            string CSVFullPath = string.Format("{0}\\Result\\{1}.csv", CSVFolderPath, CSVFileName);

            string[] HeaderName;
            //if(ArrCSVFileName[3] =="TOP") { HeaderName = new string[3] { "Num", "Lead Length", "Lead Pitch" }; }
            //else                          { HeaderName = new string[3] { "Num", "Lead Position X", "Lead Position Y" }; }

            //dataGridViewSubHistory.SetHeader(3, HeaderName, true);

            //int[] _ColumnsWidth = new int[3] { 50, 139, 139 };
            //dataGridViewSubHistory.SetColumnsWidth(_ColumnsWidth);
            dataGridViewSubHistory.SetCSVData(null, CSVFullPath, 12);
        }
    }
}
