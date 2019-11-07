using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

using ParameterManager;
using LogMessageManager;

namespace KPVisionInspectionFramework
{
    public partial class MainResultBase : Form
    {
        private ucMainResultNone            MainResultNoneWnd;
        private ucMainResultNavien          MainResultNavienWnd;
        private ucMainResultSolarAlign[]    MainResultSolarAlignWnd;

        private eProjectType ProjectType;
        private string[] LastRecipeName;

        private bool ResizingFlag = false;
        private bool IsResizing = false;
        private Point LastPosition = new Point(0, 0);

        public delegate void SendDIOResultHandler(bool _LastResult);
        public event SendDIOResultHandler SendDIOResultEvent;

        public delegate void SendPLCResultHandler(string[] _PLCResult);
        public event SendPLCResultHandler SendPLCResultEvent;

        public delegate bool RecipeChangeHandler(int _ID, string _RecipeName);
        public event RecipeChangeHandler RecipeChangeEvent;

        #region Initialize & DeInitialize
        public MainResultBase(string[] _LastRecipeName)
        {
            LastRecipeName = new string[_LastRecipeName.Count()];
            for (int iLoopCount = 0; iLoopCount < _LastRecipeName.Count(); iLoopCount++)
            {
                LastRecipeName[iLoopCount] = _LastRecipeName[iLoopCount];
            }
            InitializeComponent();
            InitializeLanguage();
        }

        public void Initialize(Object _OwnerForm, int _ProjectType)
        {
            this.Owner = (Form)_OwnerForm;
            ProjectType = (eProjectType)_ProjectType;

            if (ProjectType == eProjectType.NONE)
            {
                MainResultNoneWnd = new ucMainResultNone(LastRecipeName);
                MainResultNoneWnd.ScreenshotEvent += new ucMainResultNone.ScreenshotHandler(ScreenShot);
                panelMain.Controls.Add(MainResultNoneWnd);
            }

            else if(ProjectType == eProjectType.NAVIEN)
            {
                MainResultNavienWnd = new ucMainResultNavien(LastRecipeName);
                MainResultNavienWnd.ScreenshotEvent += new ucMainResultNavien.ScreenshotHandler(ScreenShotSetSize);
                MainResultNavienWnd.DIOResultEvent += new ucMainResultNavien.DIOResultHandler(SendDIOResultEvent);
                MainResultNavienWnd.RecipeChangeEvent += new ucMainResultNavien.RecipeChangeHandler(RecipeChangeEvent);
                panelMain.Controls.Add(MainResultNavienWnd);
            }

            else if(ProjectType == eProjectType.SCALIGN)
            {
                MainResultSolarAlignWnd = new ucMainResultSolarAlign[2];

                for(int iLoopCount = 0; iLoopCount < 2; iLoopCount++)
                {
                    MainResultSolarAlignWnd[iLoopCount] = new ucMainResultSolarAlign(LastRecipeName, iLoopCount);
                    MainResultSolarAlignWnd[iLoopCount].PLCResultEvent += new ucMainResultSolarAlign.PLCResultHandler(SendPLCResultEvent);
                    panelMain.Controls.Add(MainResultSolarAlignWnd[iLoopCount]);
                    if(iLoopCount == 0) MainResultSolarAlignWnd[iLoopCount].Location = new Point(2, 2);
                    else                MainResultSolarAlignWnd[iLoopCount].Location = new Point(2, 457);
                }
            }

            SetWindowLocation(1482, 148);
        }

        private void InitializeLanguage()
        {
            labelTitle.Text = LanguageResource.ResultWindow;
        }

        public void DeInitialize()
        {
            if (ProjectType == eProjectType.NONE)
            {
                MainResultNoneWnd.ScreenshotEvent -= new ucMainResultNone.ScreenshotHandler(ScreenShot);
            }
            else if (ProjectType == eProjectType.NAVIEN)
            {
                MainResultNavienWnd.DIOResultEvent -= new ucMainResultNavien.DIOResultHandler(SendDIOResultEvent);
            }

            panelMain.Controls.Clear();
        }

        public void SetWindowLocation(int _StartX, int _StartY)
        {
            this.Location = new Point(_StartX, _StartY);
        }

        public void SetWindowSize(int _Width, int _Height)
        {
            this.Size = new Size(_Width, _Height);
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void MainResultBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.F4) e.Handled = true;
        }

        private void MainResultBase_MouseDown(object sender, MouseEventArgs e)
        {
            this.IsResizing = true;
            this.LastPosition = e.Location;
        }

        private void MainResultBase_MouseUp(object sender, MouseEventArgs e)
        {
            this.IsResizing = false;
        }

        private void MainResultBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            if (!IsResizing) // handle cursor type
            {
                bool resize_x = e.X > (this.Width - 8);
                bool resize_y = e.Y > (this.Height - 8);

                if (resize_x && resize_y) this.Cursor = Cursors.SizeNWSE;
                else if (resize_x) this.Cursor = Cursors.SizeWE;
                else if (resize_y) this.Cursor = Cursors.SizeNS;
                else this.Cursor = Cursors.Default;
            }
            else // handle resize
            {
                if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

                int w = this.Size.Width;
                int h = this.Size.Height;

                if (this.Cursor.Equals(Cursors.SizeNWSE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h + (e.Location.Y - this.LastPosition.Y));
                else if (this.Cursor.Equals(Cursors.SizeWE)) this.Size = new Size(w + (e.Location.X - this.LastPosition.X), h);
                else if (this.Cursor.Equals(Cursors.SizeNS)) this.Size = new Size(w, h + (e.Location.Y - this.LastPosition.Y));

                this.LastPosition = e.Location;
            }
        }

        private void MainResultBase_Resize(object sender, EventArgs e)
        {
            Size _TitleSize = new Size(this.Size.Width, labelTitle.Size.Height);
            Point _Location = panelMain.Location;

            labelTitle.Size = new Size(_TitleSize.Width - 6, _TitleSize.Height);
            labelTitle.Location = new Point(3, 2);

            panelMain.Size = new Size(_TitleSize.Width - 6, this.Size.Height - _TitleSize.Height - 6);
            panelMain.Location = new Point(3, _Location.Y);

            panelMain.Invalidate();
            labelTitle.Invalidate();
        }

        private void labelTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.labelTitle.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void labelTitle_DoubleClick(object sender, EventArgs e)
        {
            ResizingFlag = !ResizingFlag;
            if (true == ResizingFlag) labelTitle.ForeColor = Color.Chocolate;
            else labelTitle.ForeColor = Color.WhiteSmoke;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            var s = sender as Label;
            if (e.Button != System.Windows.Forms.MouseButtons.Right) return;

            s.Parent.Left = this.Left + (e.X - ((Point)s.Tag).X);
            s.Parent.Top = this.Top + (e.Y - ((Point)s.Tag).Y);

            this.Cursor = Cursors.Default;
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            var s = sender as Label;
            s.Tag = new Point(e.X, e.Y);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panelMain.ClientRectangle, Color.White, ButtonBorderStyle.Solid);
        }

        private void panelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (false == ResizingFlag) { this.Cursor = Cursors.Default; return; }

            this.Cursor = Cursors.Default;
        }
        #endregion Control Default Event

        public void ClearResultData(string _LOTNum = "", string _InDataPath = "", string _OutDataPath = "")
        {
            if (ProjectType == eProjectType.NONE)           MainResultNoneWnd.ClearResult();
            else if (ProjectType == eProjectType.NAVIEN)    MainResultNavienWnd.ClearResult(_LOTNum);
        }

        //LDH, 2019.04.02, Ethernet Receive Data 전달
        public void SetEthernetRecvData(object _Value)
        {
            //if (ProjectType == eProjectType.BC_QCC) MainResultCardManagerWnd.SetEthernetRecvData(_Value);
        }

        //LDH, 2019.05.13, ProjectItem -> ProjectType 으로 변경
        public void SetResultData(SendResultParameter _ResultParam)
        {
            if (ProjectType == eProjectType.NONE) MainResultNoneWnd.SetNoneResultData(_ResultParam);
            else if (ProjectType == eProjectType.NAVIEN) MainResultNavienWnd.SetResult(_ResultParam);
            else if (ProjectType == eProjectType.SCALIGN) MainResultSolarAlignWnd[_ResultParam.ID % 2].SetResultData(_ResultParam);
        }

        /// <summary>
        /// 프로젝트별 AutoMode 관리
        /// </summary>
        /// <param name="_AutoModeFlag"></param>
        /// <returns></returns>
        public bool SetAutoMode(bool _AutoModeFlag)
        {
            bool _Result = false;

            if (ProjectType == eProjectType.NONE)           MainResultNoneWnd.SetAutoMode(_AutoModeFlag);
            else if (ProjectType == eProjectType.NAVIEN)    MainResultNavienWnd.SetAutoMode(_AutoModeFlag);

            return _Result;
        }

        public void SetLastRecipeName(eProjectType _ProjectType, string[] _LastRecipeName)
        {
            if (_ProjectType == eProjectType.NONE)              MainResultNoneWnd.SetLastRecipeName(_LastRecipeName);
            else if (ProjectType == eProjectType.NAVIEN)        MainResultNavienWnd.SetLastRecipeName(_LastRecipeName);
        }
        
        private void ScreenShot(string ImageSaveFile)
        {
            try
            {
                Size Wondbounds = new Size(1280, 1024 - 150);
                Bitmap printScreen = new Bitmap(1280, 1024 - 150);
                Graphics graphics = Graphics.FromImage(printScreen as Image);
                graphics.CopyFromScreen(new Point(8, 150), Point.Empty, Wondbounds);
                printScreen.Save(ImageSaveFile, ImageFormat.Jpeg);
                printScreen.Dispose();
            }
            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Screenshot Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }

        private void ScreenShotSetSize(string ImageSaveFile, Size ScreenshotSize)
        {
            try
            {
                Size Wondbounds = new Size(ScreenshotSize.Width, ScreenshotSize.Height - 150);
                Bitmap printScreen = new Bitmap(ScreenshotSize.Width, ScreenshotSize.Height - 150);
                Graphics graphics = Graphics.FromImage(printScreen as Image);
                graphics.CopyFromScreen(new Point(8, 150), Point.Empty, Wondbounds);
                printScreen.Save(ImageSaveFile, ImageFormat.Jpeg);
                printScreen.Dispose();
            }
            catch (System.Exception ex)
            {
                CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "Screenshot Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
            }
        }

        //LDH, 2019.03.20, 저장 Data Folder 지정 함수
        public void SetDataFolderPath(string[] _FolderPath)
        {
            //if (ProjectType == eProjectType.TRIM_FORM) MainResultTrimFormWnd.SetDataFolderPath(_FolderPath);
        }

        //LDH, 2019.06.18, LOTNum 지정함수
        public void SetLOTNum(string[] _LOTNum)
        {
            if (ProjectType == eProjectType.NAVIEN) MainResultNavienWnd.SetLOTNum(_LOTNum);
        }
		
		//LDH, 2019.06.10, Result 사용 Flag 받아오기
        public void GetUseResult(int _ID, out string _UseResultFlag)
        {
            _UseResultFlag = "1";
            if (ProjectType == eProjectType.NAVIEN) MainResultNavienWnd.GetUseResultFlag(out _UseResultFlag);
        }

        //LDH, 2019.09.17, Insepction Time 체크용 시간 설정
        public void SetCycleTime()
        {
            DateTime dateTime = DateTime.Now;

            if (ProjectType == eProjectType.NAVIEN) MainResultNavienWnd.InspStartTime = dateTime;
        }

        public bool GetBarcodeStatus()
        {
            bool _Result = false;

            if (ProjectType == eProjectType.NAVIEN) _Result = MainResultNavienWnd.GetBarcodeStatus();

            return _Result;
        }
    }
}
