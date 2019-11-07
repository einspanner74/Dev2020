using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;

using LogMessageManager;
using WindowKeyPad;

namespace InspectionSystemManager
{
    public partial class ImageDeleteWindow : Form
    {
        WindowKeypadControl objWndKeyPad = new WindowKeypadControl(false);

        DateTime DeleteDate;
        public string DeleteYear;
        public string DeleteMonth;
        public string DeleteDay;
        public string SavedDate;

        private string DeleteFolderPath = "";
        private string DeleteFolderName = "";

        private Thread ThreadImageAutoDelete;
        private bool IsThreadImageAutoDeleteExit = false;
        private bool IsThreadImageAutoDeleteTrigger = false;

        public ImageDeleteWindow(string _DeleteFolderName, bool _FolderPathSet = false)
        {
            InitializeComponent();

            if (_FolderPathSet)
            {
                DeleteFolderName = Path.GetFileNameWithoutExtension(_DeleteFolderName);
                DeleteFolderPath = Path.GetDirectoryName(_DeleteFolderName);

                string _PathTemp = DeleteFolderPath.Substring(DeleteFolderPath.Length - 1, 1);
                if (_PathTemp != @"\") DeleteFolderPath = DeleteFolderPath + "\\";
            }
            else
            {
                DeleteFolderPath = "D:\\VisionInspectionData\\";
                DeleteFolderName = _DeleteFolderName;
            }

            ThreadImageAutoDelete = new Thread(ThreadImageAutoDeleteFunc);
            IsThreadImageAutoDeleteExit = false;
            IsThreadImageAutoDeleteTrigger = false;
            ThreadImageAutoDelete.IsBackground = true;
            ThreadImageAutoDelete.Start();

            GetDeleteDateFromRegistry();
        }

        public void DeInitialize()
        {
            if (ThreadImageAutoDelete != null) { IsThreadImageAutoDeleteExit = true; Thread.Sleep(200); ThreadImageAutoDelete.Abort(); ThreadImageAutoDelete = null; }
        }

        public void SetDeleteDate(string _SavedDate)
        {
            //registry에서 받은 후 날짜 계산한다음 설정
            SavedDate = _SavedDate;
            btnDeleteDay.Text = SavedDate;
        }

        public int GetDeleteDate()
        {
            return Convert.ToInt32(SavedDate);
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

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DateTime TimeNow = DateTime.Now;
            DeleteDate = TimeNow;
            SetDeleteFolderName(DeleteDate);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            DialogResult DeleteDialog = MessageBox.Show("Do you want to delete?", "Image Delete", MessageBoxButtons.YesNo);

            switch (DeleteDialog)
            {
                case DialogResult.Yes:
                    {
                        int Daycount = dateTimePickerDelete.Value.DayOfYear - dateTimePickerDeleteFrom.Value.DayOfYear;

                        if (Daycount <= 0) Daycount = dateTimePickerDelete.Value.DayOfYear + 365 - dateTimePickerDeleteFrom.Value.DayOfYear;
                        DeleteDate = dateTimePickerDeleteFrom.Value;

                        SetDeleteFolderName(DeleteDate);
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnDeleteDay_Click(object sender, EventArgs e)
        {
            objWndKeyPad.SetKeyPadValue(btnDeleteDay.Text);
            objWndKeyPad.TopMost = true;
            objWndKeyPad.ShowDialog();

            if (objWndKeyPad.DialogResult == DialogResult.Cancel) return;
            else
            {
                DialogResult Result = MessageBox.Show("Do you want to change keep period?", "Confirm", MessageBoxButtons.YesNo);
                if (Result == DialogResult.No) return;
            }

            if (10 > Convert.ToInt32(objWndKeyPad.KeyPadCharactor)) { MessageBox.Show(new Form { TopMost = true }, "Minimum period is 10 days."); return; }
            else if (365 < Convert.ToInt32(objWndKeyPad.KeyPadCharactor)) { MessageBox.Show(new Form { TopMost = true }, "Maximun period is 365 days."); return; }

            btnDeleteDay.Text = objWndKeyPad.KeyPadCharactor;
            SetDeleteDayFrom(objWndKeyPad.KeyPadCharactor);
            SavedDate = btnDeleteDay.Text;
            SaveDeleteDateToRegistry(GetDeleteDate());
        }

        private void SetDeleteDayFrom(string DeleteDay)
        {
            double SetDay = Convert.ToDouble(DeleteDay);
            dateTimePickerDeleteFrom.Value = dateTimePickerDelete.Value.AddDays(-SetDay);
        }

        public void SetDeleteFolderName(DateTime _DeleteDate)
        {
            DeleteYear = _DeleteDate.Year.ToString();

            if (_DeleteDate.Month < 10) DeleteMonth = string.Format("0{0}", _DeleteDate.Month);
            else DeleteMonth = _DeleteDate.Month.ToString();

            if (_DeleteDate.Day < 10) DeleteDay = string.Format("0{0}", _DeleteDate.Day);
            else DeleteDay = _DeleteDate.Day.ToString();

            DeleteImage();
        }

        private void DeleteImage()
        {
            string DeletePath = "";
            string DeleteFolder;

            //if(DeleteFolderPath == "") DeletePath = @"D:\VisionInspectionData\" + DeleteFolderName;
            DeletePath = string.Format(@"{0}{1}", DeleteFolderPath, DeleteFolderName);

            //Year 폴더 삭제
            DirectoryInfo DeleteYearFolderInfo = new DirectoryInfo(DeletePath);
            if (DeleteYearFolderInfo.Exists)
            {
                DirectoryInfo[] YearFolderInfo = DeleteYearFolderInfo.GetDirectories();
                foreach (DirectoryInfo YearFolder in YearFolderInfo)
                {
                    if (Convert.ToInt32(YearFolder.Name) < Convert.ToInt32(DeleteYear))
                    {
                        DeleteFolder = String.Format("{0}\\{1}", DeletePath, YearFolder.Name);
                        Directory.Delete(DeleteFolder, true);
                    }
                }
            }

            //Month 폴더 삭제
            DeletePath = String.Format("{0}\\{1}", DeletePath, DeleteYear);
            DirectoryInfo DeleteMonthFolderInfo = new DirectoryInfo(DeletePath);
            if (DeleteMonthFolderInfo.Exists)
            {
                DirectoryInfo[] MonthFolderInfo = DeleteMonthFolderInfo.GetDirectories();
                foreach (DirectoryInfo MonthFolder in MonthFolderInfo)
                {
                    if (Convert.ToInt32(MonthFolder.Name) < Convert.ToInt32(DeleteMonth))
                    {
                        DeleteFolder = String.Format("{0}\\{1}", DeletePath, MonthFolder.Name);
                        Directory.Delete(DeleteFolder, true);
                    }
                }
            }

            //Day 폴더 삭제
            DeletePath = String.Format("{0}\\{1}", DeletePath, DeleteMonth);
            DirectoryInfo DeleteDayFolderInfo = new DirectoryInfo(DeletePath);
            if (DeleteDayFolderInfo.Exists)
            {
                DirectoryInfo[] DayFolderInfo = DeleteDayFolderInfo.GetDirectories();
                foreach (DirectoryInfo DayFolder in DayFolderInfo)
                {
                    if (Convert.ToInt32(DayFolder.Name) < Convert.ToInt32(DeleteDay))
                    {
                        DeleteFolder = String.Format("{0}\\{1}", DeletePath, DayFolder.Name);
                        Directory.Delete(DeleteFolder, true);
                    }
                }
            }

            DialogResult CloseResult = DialogResult.Cancel;
            if (IsThreadImageAutoDeleteTrigger == true) CloseResult = MessageBox.Show(new Form { TopMost = true }, "Deleted.");
            else IsThreadImageAutoDeleteTrigger = true;
            if (CloseResult == DialogResult.OK) this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 자동 삭제 기간 저장
        /// </summary>
        /// <param name="_DeleteDate"></param>
        private void SaveDeleteDateToRegistry(int _DeleteDate)
        {
            string _RegKeyDeleteDateName = String.Format(@"KPVisionInspection\DeleteDate");
            RegistryKey _RegKeyDeleteDate = Registry.CurrentUser.CreateSubKey(_RegKeyDeleteDateName);
            _RegKeyDeleteDate.SetValue("Value", _DeleteDate, RegistryValueKind.DWord);

            GetDeleteDateFromRegistry();
        }

        /// <summary>
        /// 자동 삭제 기간 불러오기
        /// </summary>
        public void GetDeleteDateFromRegistry()
        {
            string _RegKeyDeleteDateName = String.Format(@"KPVisionInspection\DeleteDate");
            RegistryKey _RegDeleteDate = Registry.CurrentUser.CreateSubKey(_RegKeyDeleteDateName);

            if (null == _RegDeleteDate.GetValue("Value")) { _RegDeleteDate.SetValue("Value", "50", RegistryValueKind.DWord); }

            SetDeleteDate((_RegDeleteDate.GetValue("Value")).ToString());
        }

        private void ThreadImageAutoDeleteFunc()
        {
            DateTime TimeNow;

            bool IsDeleted = false;

            try
            {
                while (false == IsThreadImageAutoDeleteExit)
                {
                    TimeNow = DateTime.Now;

                    if (true == IsThreadImageAutoDeleteTrigger)
                    {
                        if (TimeNow.Hour == 0 && IsDeleted == false)
                        {
                            IsThreadImageAutoDeleteTrigger = false;
                            TimeNow = TimeNow.AddDays(-GetDeleteDate());
                            SetDeleteFolderName(TimeNow);
                            IsDeleted = true;
                        }
                        else if (TimeNow.Hour != 0) IsDeleted = false;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(new Form { TopMost = true }, ex.Message + " ->" + ex.StackTrace);
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, String.Format(ex.Message + " ->" + ex.StackTrace), CLogManager.LOG_LEVEL.LOW);
            }
        }
    }
}
