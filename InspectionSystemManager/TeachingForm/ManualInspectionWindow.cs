using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace InspectionSystemManager
{
    public partial class ManualInspectionWindow : Form
    {
        public bool IsShowWindow = false;

        private string FileName = "";
        private string FileBasePath = "";
        private string FileFullPath = "";

        public delegate void ImageLoadHandler(string _ImageFileFullPath);
        public event ImageLoadHandler ImageLoadEvent;

        public delegate void ImageInspectionHandler();
        public event ImageInspectionHandler ImageInspectionEvent;

        #region Initialize & DeInitialize
        public ManualInspectionWindow()
        {
            InitializeComponent();
        }

        public void Initialize(string _FormName)
        {
            labelTitle.Text = labelTitle.Text + " - " + _FormName;
        }

        public void DeInitialize()
        {

        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void ManualInspectionWindow_KeyDown(object sender, KeyEventArgs e)
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
        public void ShowManualInspectionWindow()
        {
            IsShowWindow = true;
            this.Show();
        }

        private void btnSetPath_Click(object sender, EventArgs e)
        {
            //FileBasePath = OpenFolderBrowser();
            //textBoxBasePath.Text = FileBasePath;

            //DirectoryInfo _DirInfo = new DirectoryInfo(FileBasePath);

            //foreach (FileInfo _File in _DirInfo.GetFiles())
            //    listBoxImageFile.Items.Add(_File.Name);

            LoadImageFileToListView();
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            LoadImageToDisplayWindow();
        }

        private void btnInspection_Click(object sender, EventArgs e)
        {
            ImageInspectionEvent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IsShowWindow = false;
            this.Hide();
        }

        private void listBoxImageFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _Index = listBoxImageFile.SelectedIndex;

            if (_Index == -1) return;

            FileName = (string)listBoxImageFile.Items[_Index];
        }

        private void listViewImageFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewImageFile.SelectedIndices.Count == 0) return;
            var _Indices = listViewImageFile.SelectedIndices;
            FileName = listViewImageFile.SelectedItems[0].Text;
        }

        private void listViewImageFile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (listViewImageFile.SelectedIndices.Count == 0) return;
                var _Indices = listViewImageFile.SelectedIndices;
                FileName = listViewImageFile.SelectedItems[0].Text;

                LoadImageToDisplayWindow();
            }

            else if (e.Button == MouseButtons.Right)
            {
                if (listViewImageFile.SelectedIndices.Count == 0) return;
                var _Indices = listViewImageFile.SelectedIndices;
                FileName = listViewImageFile.SelectedItems[0].Text;

                LoadImageToDisplayWindow();

                System.Threading.Thread.Sleep(50);
                ImageInspectionEvent();
            }
        }
        #endregion Button Event

        private string OpenFolderBrowser()
        {
            string _folderName = "";

            using (FolderBrowserDialog _FolderBrowserDialog = new FolderBrowserDialog())
            {
                try
                {
                    _FolderBrowserDialog.SelectedPath = @"D:\VisionInspectionData";
                    if (_FolderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        _folderName = _FolderBrowserDialog.SelectedPath;
                    }
                }
                catch
                {

                }
            }

            return _folderName;
        }

        private void LoadImageFileToListView()
        {
            string _SelectPath = OpenFolderBrowser();
            if (_SelectPath == "") return;
            FileBasePath = _SelectPath;

            textBoxBasePath.Text = FileBasePath;

            ImageList _ImageList = new ImageList();
            _ImageList.ImageSize = new Size(32, 32);

            DirectoryInfo _DirInfo = new DirectoryInfo(FileBasePath);
            foreach (FileInfo _File in _DirInfo.GetFiles())
            {
                try
                {
                    Image i = Image.FromFile(FileBasePath + "\\" + _File.Name);
                    Image img = i.GetThumbnailImage(32, 32, null, new IntPtr());
                    _ImageList.Images.Add(img);
                    i.Dispose();
                }

                catch
                {

                }
            }

            listViewImageFile.Items.Clear();
            listViewImageFile.SmallImageList = new ImageList();
            listViewImageFile.SmallImageList = _ImageList;

            for (int j = 0; j < _ImageList.Images.Count; j++)
            {
                ListViewItem lstItem = new ListViewItem();
                lstItem.ImageIndex = j;
                lstItem.Text = _DirInfo.GetFiles()[j].Name;
                listViewImageFile.Items.Add(lstItem);
            }
        }

        private void LoadImageToDisplayWindow()
        {
            if (FileName == "") return;
            if (FileBasePath == "") return;

            try
            {
                FileFullPath = String.Format(@"{0}\{1}", FileBasePath, FileName);
                ImageLoadEvent(FileFullPath);
            }

            catch
            {
                MessageBox.Show("Image file load fail.");
            }
        }
    }
}
