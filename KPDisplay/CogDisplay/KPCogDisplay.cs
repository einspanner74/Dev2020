using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;

using LogMessageManager;

namespace KPDisplay
{
    public partial class KPCogDisplay : CogDisplay
    {
        public KPCogDisplay()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        #region Initialize
        protected override void InitializeContextMenuStrip()
        {
            this.ContextMenuStrip = new ContextMenuStrip();
            this.ContextMenuStrip.Items.Add("Point(&P)", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemPointerClicked));
            this.ContextMenuStrip.Items.Add("Move(&M)", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemPanClicked));
            this.ContextMenuStrip.Items.Add("Zoom In", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemZoomInClicked));
            this.ContextMenuStrip.Items.Add("Zoom Out", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemZoomOutClicked));
            //this.ContextMenuStrip.Items.Add((ToolStripItem)new ToolStripSeparator());
            this.ContextMenuStrip.Items.Add("Image Fit(&F)", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemFitImageClicked));
            //this.ContextMenuStrip.Items.Add("이미지불러오기(&L)", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemLoadImageClicked));
            //this.ContextMenuStrip.Items.Add("이미지저장(&S)", (System.Drawing.Image)null, new EventHandler(this.OnContextMenuItemSaveImageClicked));
            this.ContextMenuStrip.Items.Add((ToolStripItem)new ToolStripSeparator());
        }

        protected void OnContextMenuItemLoadImageClicked(object sender, EventArgs e)
        {
            using (OpenFileDialog _openFileDialog = new OpenFileDialog())
            {
                _openFileDialog.Filter = "BmpFile (*.bmp)|*.bmp";
                try
                {
                    if(_openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string _fileName = _openFileDialog.FileName;
                        CogImageFileTool _cogImageFileTool = new CogImageFileTool();
                        _cogImageFileTool.Operator.Open(_fileName, CogImageFileModeConstants.Read);
                        _cogImageFileTool.Run();
                        this.Image = _cogImageFileTool.OutputImage;
                        this.AutoFit = true;
                    }
                }
                catch 
                {
                    CLogManager.AddInspectionLog(CLogManager.LOG_TYPE.ERR, "OnContextMenuItemLoadImageClicked Exception!!", CLogManager.LOG_LEVEL.LOW);
                    MessageBox.Show("Loading image failed.");
                }
            }
        }
        #endregion Initialize

        #region Clear CogDisplay()
        /// <summary>
        /// Display 화면을 지운다.
        /// </summary>
        public void ClearDisplay(bool _ImageClear = false)
        {
            if(_ImageClear) this.Image = null;
            this.StaticGraphics.Clear();
            this.InteractiveGraphics.Clear();
        }

        /// <summary>
        /// Display 화면을 지운다.
        /// </summary>
        public void ClearDisplay(bool _StaticClear, bool _InteractiveClear)
        {
            if (true == _StaticClear)       this.StaticGraphics.Clear();
            if (true == _InteractiveClear)  this.InteractiveGraphics.Clear();
        }

        /// <summary>
        /// 지정한 GroupName의 Display를 지운다.
        /// </summary>
        /// <param name="_groupName">Display Group 이름</param>
        public void ClearDisplay(string _groupName)
        {
            CogStringCollection _collectionString = this.StaticGraphics.ZOrderGroups;
            int _index = _collectionString.IndexOf(_groupName);
            if (_index >= 0) this.StaticGraphics.Remove(_groupName);

            _collectionString = this.InteractiveGraphics.ZOrderGroups;
            _index = _collectionString.IndexOf(_groupName);
            if (_index >= 0) this.InteractiveGraphics.Remove(_groupName);
        }

        public void ClearDisplay(string[] _ExceptGroupName)
        {
            bool _Flag = false;
            CogStringCollection _CollectionStringStatic = this.StaticGraphics.ZOrderGroups;
            for (int iLoopCount = 0; iLoopCount < _CollectionStringStatic.Count; ++iLoopCount)
            {
                _Flag = false;
                for (int jLoopCount = 0; jLoopCount < _ExceptGroupName.Length; ++jLoopCount)
                {
                    if (_CollectionStringStatic[iLoopCount] == _ExceptGroupName[jLoopCount])
                        _Flag = true;
                }

                if (false == _Flag)
                {
                    this.StaticGraphics.Remove(_CollectionStringStatic[iLoopCount]);
                }
            }

            CogStringCollection _CollectionStringInter = this.InteractiveGraphics.ZOrderGroups;
            for (int iLoopCount = 0; iLoopCount < _CollectionStringInter.Count; ++iLoopCount)
            {
                _Flag = false;
                for (int jLoopCount = 0; jLoopCount < _ExceptGroupName.Length; ++jLoopCount)
                {
                    if (_CollectionStringInter[iLoopCount] == _ExceptGroupName[jLoopCount])
                        _Flag = true;
                }

                if (false == _Flag)
                {
                    this.InteractiveGraphics.Remove(_CollectionStringInter[iLoopCount]);
                }
            }
        }
        #endregion Clear CogDisplay()

        public void SaveImageIncludeGraphics()
        {
            this.OnContextMenuItemFitImageClicked(null, null);
        }
    }
}
