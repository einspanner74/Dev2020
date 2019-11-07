using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ParameterManager;

namespace KPVisionInspectionFramework
{
    public partial class FolderPathWindow : Form
    {
        public delegate void SetDataPathHandler(string[] _DataPath);
        public event SetDataPathHandler SetDataPathEvent;

        Label[] lbPathName;
        TextBox[] tbPath;
        Button[] btnPathSearch;

        int PathCount = 0;

        public FolderPathWindow(int _ProjectType)
        {
            InitializeComponent();

            lbPathName = new Label[2] { labelPath1, labelPath2 };
            tbPath = new TextBox[2] { textBoxPath1, textBoxPath2 };
            btnPathSearch = new Button[2] { btnPathSearch1, btnPathSearch2 };

            SetlabelPathName(_ProjectType);
        }

        private void SetlabelPathName(int _ProjectType)
        {
            if(eProjectType.NONE == (eProjectType)_ProjectType)
            {
                PathCount = 1;

                lbPathName[0].Text = "Image Path";

                panelPath2.Visible = false;
                btnConfirm.Location = new Point(587, 66);
                this.Size = new Size(723, 102);
            }
            else
            {
                PathCount = 2;
                
                lbPathName[0].Text = "Cam 1";
                lbPathName[1].Text = "Cam 2";
            }            
        }

        public void SetCurrentDataPath(int Num, string _CurrentDataPath)
        {
            tbPath[Num].Clear();
            tbPath[Num].Text = _CurrentDataPath;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string[] DataPath = new string[PathCount];

            for (int iLoopCount = 0; iLoopCount < PathCount; iLoopCount++)
            {
                if (tbPath[iLoopCount].Text == null || tbPath[iLoopCount].Text == "") { MessageBox.Show("폴더 경로가 없습니다."); return; }

                DataPath[iLoopCount] = tbPath[iLoopCount].Text;
            }

            var _RecipeCopyEvent = SetDataPathEvent;
            _RecipeCopyEvent?.Invoke(DataPath);

            this.DialogResult = DialogResult.OK;
            this.Hide();
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
        
        private void btnSearchDataPath_Click(object sender, EventArgs e)
        {
            int Num = Convert.ToInt32(((Button)sender).Tag);

            FolderBrowserDialog FolderDialog = new FolderBrowserDialog();
            if (DialogResult.OK == FolderDialog.ShowDialog())
            {
                tbPath[Num].Text = FolderDialog.SelectedPath;
            }
        }
    }
}
