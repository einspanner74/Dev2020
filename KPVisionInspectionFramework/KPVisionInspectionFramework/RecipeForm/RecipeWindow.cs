using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using Microsoft.VisualBasic.FileIO;

using LogMessageManager;
using ParameterManager;
using CustomControl;

namespace KPVisionInspectionFramework
{
    public partial class RecipeWindow : Form
    {
        private string RecipeFolderPath = @"D:\VisionInspectionData\KPINTVisionInspection\RecipeParameter\";
        private string ProjectName = "KPINTVisionInspection";
        private eProjectType ProjectType;
        private bool IsRecipeNew = false;

        //LDH, 2019.01.09, 다중 Recipe 관리를 위해 추가
        bool IsTotalRecipe = false;
        int UseRecipecount = 0;
        int SelectedRecipeNum = 0;
        GradientLabel[] labelCurrentRecipeNum;
        TextBox[] txtboxCurrentRecipeName;
        private string[] CurrentRecipeName;

        public delegate bool RecipeChangeHandler(int _ID, string _RecipeName);
        public event RecipeChangeHandler RecipeChangeEvent;

        public delegate bool RecipeCopyHandler(string _RecipeName, string _SrcRecipeName);
        public event RecipeCopyHandler RecipeCopyEvent;

        #region Initialize & DeInitialize
        public RecipeWindow()
        {
            InitializeComponent();
        }

        public RecipeWindow(eProjectType _ProjectType, string _ProjectName, string[] _CurrentRecipe, bool _IsTotalRecipe)
        {
            InitializeComponent();

            labelCurrentRecipeNum = new GradientLabel[6] { labelRecipeNum0, labelRecipeNum1, labelRecipeNum2, labelRecipeNum3, labelRecipeNum4, labelRecipeNum5 };
            txtboxCurrentRecipeName = new TextBox[6] { textBoxCurrentRecipe0, textBoxCurrentRecipe1, textBoxCurrentRecipe2, textBoxCurrentRecipe3, textBoxCurrentRecipe4, textBoxCurrentRecipe5 };

            ProjectName = _ProjectName;
            ProjectType = _ProjectType;
            RecipeFolderPath = String.Format(@"D:\VisionInspectionData\{0}\RecipeParameter\", ProjectName);

            //LDH, 2019.01.17, Recipe 형태 구분
            IsTotalRecipe = _IsTotalRecipe;
            UseRecipecount = _CurrentRecipe.Count();

            if (IsTotalRecipe) SetRecipeOption(1);
            else               SetRecipeOption(UseRecipecount);

            CurrentRecipeName = new string[UseRecipecount];

            for (int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
            {
                CurrentRecipeName[iLoopCount] = _CurrentRecipe[iLoopCount];
            }

            SelectedRecipeChange();

            LoadRecipeList();
        }

        //LDH, 2019.01.14, RecipeName textbox 개수 설정
        private void SetRecipeOption(int _RecipeCount)
        {
            if (_RecipeCount == 0) return;

            else if (_RecipeCount > 0 && _RecipeCount <= 3)
            {
                panelCurrentRecipe.Size = new Size(637, 33);
                panelMain.Size = new Size(640, 762);
                this.Size = new Size(640, 798);

                if (_RecipeCount == 1)
                {
                    labelCurrentRecipeNum[0].Visible = false;
                    txtboxCurrentRecipeName[0].Size = new Size(634, 29);
                    txtboxCurrentRecipeName[0].Location = new Point(2, 2);
                }
            }

            for (int iLoopcount = 1; iLoopcount < _RecipeCount; iLoopcount++)
            {
                labelCurrentRecipeNum[iLoopcount].Visible = true;
                txtboxCurrentRecipeName[iLoopcount].Visible = true;
            }
        }

        //LDH, 2019.01.16, 선택된 Recipe 색상 설정
        private void SelectedRecipeChange()
        {
            for(int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
            {
                if (SelectedRecipeNum == iLoopCount)
                {
                    ControlInvoke.GradientLabelColor(labelCurrentRecipeNum[iLoopCount], Color.DarkOrange, Color.AntiqueWhite);
                }
                else
                {
                    ControlInvoke.GradientLabelColor(labelCurrentRecipeNum[iLoopCount], Color.FromArgb(64, 64, 64), Color.Gray);    
                }
            }
        }
        #endregion Initialize & DeInitialize

        #region Control Default Event
        private void RecipeWindow_KeyDown(object sender, KeyEventArgs e)
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

        #region Control Event
        private void btnRecipeChange_Click(object sender, EventArgs e)
        {
            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "RecipeWindow - RecipeChange_Click", CLogManager.LOG_LEVEL.LOW);
            //if (listBoxRecipe.SelectedItem == null) { MessageBox.Show(new Form { TopMost = true }, "Select the recipe you want to change."); return; }

            this.Hide();
            CParameterManager.SystemMode = eSysMode.RCP_MANUAL_CHANGE;

            //for (int iLoopCount = 0; iLoopCount < UseRecipecount - 1; iLoopCount++)
            //{
            //    if (txtboxCurrentRecipeName[iLoopCount].Text == txtboxCurrentRecipeName[iLoopCount + 1].Text)
            //    {
            //        MessageBox.Show("중복 선택된 Recipe가 있습니다. \n레시피를 변경하세요."); return;
            //    }
            //}

            RecipeChange();

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "RecipeWindow - RecipeChange Complete : " + listBoxRecipe.SelectedItem.ToString(), CLogManager.LOG_LEVEL.LOW);
            this.Show();
        }

        private void btnRecipeAdd_Click(object sender, EventArgs e)
        {
            IsRecipeNew = true;

            RecipeNewNameWindow _RcpNewNameWnd = new RecipeNewNameWindow(ProjectType);
            _RcpNewNameWnd.RecipeCopyEvent += new RecipeNewNameWindow.RecipeCopyHandler(RecipeCopyEventFunction);

            string[] _RecipeList = new string[listBoxRecipe.Items.Count];
            for (int iLoopCount = 0; iLoopCount < _RecipeList.Count(); ++iLoopCount)
                _RecipeList[iLoopCount] = listBoxRecipe.Items[iLoopCount].ToString();

            _RcpNewNameWnd.SetCurrentRecipe("[Default]", _RecipeList);

            if (_RcpNewNameWnd.ShowDialog() == DialogResult.OK)
            {
                LoadRecipeList();
                this.Hide();
                CParameterManager.SystemMode = eSysMode.RCP_MANUAL_CHANGE;
                //RecipeChange(_RcpNewNameWnd.NewRecipeName, "[Default]");
                this.Show();
            }

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "RecipeWindow - RecipeAdd Complete", CLogManager.LOG_LEVEL.LOW);

            _RcpNewNameWnd.RecipeCopyEvent -= new RecipeNewNameWindow.RecipeCopyHandler(RecipeCopyEventFunction);
            IsRecipeNew = false;
        }

        private void btnRecipeCopy_Click(object sender, EventArgs e)
        {
            RecipeNewNameWindow _RcpNewNameWnd = new RecipeNewNameWindow(ProjectType);
            _RcpNewNameWnd.RecipeCopyEvent += new RecipeNewNameWindow.RecipeCopyHandler(RecipeCopyEventFunction);

            string[] _RecipeList = new string[listBoxRecipe.Items.Count];
            for (int iLoopCount = 0; iLoopCount < _RecipeList.Count(); ++iLoopCount)
                _RecipeList[iLoopCount] = listBoxRecipe.Items[iLoopCount].ToString();

            if (listBoxRecipe.SelectedIndex == -1) { MessageBox.Show("No recipes selected."); return; }

            _RcpNewNameWnd.SetCurrentRecipe(listBoxRecipe.SelectedItem.ToString(), _RecipeList);
            _RcpNewNameWnd.ShowDialog();

            _RcpNewNameWnd.RecipeCopyEvent -= new RecipeNewNameWindow.RecipeCopyHandler(RecipeCopyEventFunction);
        }

        private void btnRecipeDelete_Click(object sender, EventArgs e)
        {
            if (listBoxRecipe.SelectedItem == null) { MessageBox.Show(new Form { TopMost = true }, "Select recipe to delete."); return; }

            for (int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
            {
                if (CurrentRecipeName[iLoopCount] == listBoxRecipe.SelectedItem.ToString()) { MessageBox.Show("Unable to delete the recipe being used."); return; }
            }

            string _RecipeFilePath = RecipeFolderPath + listBoxRecipe.SelectedItem.ToString();
            DirectoryInfo _RecipeFolderInfo = new DirectoryInfo(_RecipeFilePath);

            if (_RecipeFolderInfo.Exists)
            {
                DialogResult DeleteResult = MessageBox.Show(new Form { TopMost = true }, "Do you want to delete?", "Message", MessageBoxButtons.YesNo);
                if (DeleteResult == DialogResult.Yes)
                {
                    _RecipeFolderInfo.Delete(true);
                    listBoxRecipe.Items.Remove(listBoxRecipe.SelectedItem);
                }
                else return;
            }

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "RecipeWindow - RecipeDelete Complete : " + listBoxRecipe.SelectedItem, CLogManager.LOG_LEVEL.LOW);

            LoadRecipeList();
        }

        private void textBoxSearchRecipe_TextChanged(object sender, EventArgs e)
        {
            LoadRecipeList(textBoxSearchRecipe.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            textBoxSearchRecipe.Text = "";
            textBoxSearchRecipe.Focus();
            this.Close();
        }

        public void ShowDialogWindow()
        {
            LoadRecipeList();

            SelectedRecipeNum = 0;
            SelectedRecipeChange();

            listBoxRecipe.SelectedItem = listBoxRecipe.Items[0];

            this.ShowDialog();
        }
        #endregion Control Event

        private void LoadRecipeList(string _SearchString = "")
        {
            DirectoryInfo _DirInfo = new DirectoryInfo(RecipeFolderPath);
            if (true == _DirInfo.Exists)
            {
                listBoxRecipe.Items.Clear();
                DirectoryInfo[] _DirInfos = _DirInfo.GetDirectories();
                foreach (DirectoryInfo _DInfo in _DirInfos)
                {
                    if (_DInfo.Name == "[Default]") continue;

                    if (_SearchString != "")
                    {
                        if (_DInfo.Name.Contains(_SearchString))
                            listBoxRecipe.Items.Add(_DInfo.Name);
                    }

                    else
                        listBoxRecipe.Items.Add(_DInfo.Name);

                }
            }

            //LDH, 2019.01.14, TextBox Recipe 이름설정 변경
            for (int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
            {
                txtboxCurrentRecipeName[iLoopCount].Clear();
                txtboxCurrentRecipeName[iLoopCount].Text = CurrentRecipeName[iLoopCount];
            }
        }

        //LDH, 2019.09.08, Navien용 Recipe 생성 유무 확인
        public bool CheckRecipeExist(string _RecipeName, out string _RecipeNameTemp)
        {
            bool _RecipeExist = false;
            _RecipeNameTemp = "";

            foreach (string ListItem in listBoxRecipe.Items)
            {
                if (ListItem.Substring(0, 5) == _RecipeName)
                {
                    _RecipeExist = true;
                    _RecipeNameTemp = ListItem;
                    break;
                }
            }

            return _RecipeExist;
        }

        //LDH, 2019.01.11, Recipe Copy할 때만 매개변수 사용
        private void RecipeChange()
        {
            var _RecipeChangeEvent = RecipeChangeEvent;
            string _RecipeName = "";

            for (int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
            {
                _RecipeName = txtboxCurrentRecipeName[iLoopCount].Text;
                if (false == _RecipeChangeEvent?.Invoke(iLoopCount, _RecipeName)) { MessageBox.Show(new Form { TopMost = true }, "Failed to load recipe."); return; }

                CurrentRecipeName[iLoopCount] = _RecipeName;
                txtboxCurrentRecipeName[iLoopCount].Text = _RecipeName;
            }
        }

        private void RecipeCopyEventFunction(string _NewRecipeName)
        {
            string _SrcRecipeName;
            if (listBoxRecipe.SelectedItem == null) _SrcRecipeName = "[Default]";
            else                                    _SrcRecipeName = listBoxRecipe.SelectedItem.ToString();

            if (IsRecipeNew) _SrcRecipeName = "[Default]";
            string _NewRecipePath = RecipeFolderPath + _NewRecipeName;
            string _SrcRecipePath = RecipeFolderPath + _SrcRecipeName;

            FileSystem.CopyDirectory(_SrcRecipePath, _NewRecipePath, UIOption.OnlyErrorDialogs);
            DirectoryInfo _CurrentDirInfo = new DirectoryInfo(_NewRecipePath);
            if (false == _CurrentDirInfo.Exists) return;

            DirectoryInfo[] _ModuleFolder = _CurrentDirInfo.GetDirectories();
            foreach (DirectoryInfo _DirInfo in _ModuleFolder)
            {
                FileInfo[] _FileInfo = _DirInfo.GetFiles();
                foreach (FileInfo _FInfo in _FileInfo)
                {
                    if (_FInfo.Extension != ".Rcp") continue;

                    try
                    {
                        XmlDocument _XmlDoc = new XmlDocument();
                        _XmlDoc.Load(_FInfo.FullName);

                        XmlNode _FirstNode = _XmlDoc.DocumentElement;
                        //XmlElement _ModuleNode = (XmlElement)_FirstNode.ChildNodes[0];

                        XmlElement _AreaNode = _XmlDoc.DocumentElement;
                        foreach (XmlElement _Node0 in _AreaNode)
                        {
                            if (false == _Node0.Name.Contains("InspAlgoArea")) continue;
                            XmlElement _ModuleNode = _Node0;

                            foreach (XmlElement _Node in _ModuleNode)
                            {
                                if (false == _Node.Name.Contains("Algo")) continue;
                                XmlElement _Algo = _Node;

                                foreach (XmlElement _Node2 in _Algo)
                                {
                                    if (false == _Node2.Name.Contains("Reference")) continue;
                                    XmlElement _ReferenceData = _Node2;
                                    XmlNode _DeleteNode = _ReferenceData.SelectSingleNode("ReferencePath");

                                    if (_DeleteNode.InnerText != null && _DeleteNode.InnerText != "")
                                    {
                                        string _ReferencePath = _DeleteNode.InnerText;
                                        string[] _ReferencePaths = _ReferencePath.Split('\\');
                                        _ReferencePaths[4] = _NewRecipeName;
                                        _ReferencePath = String.Join("\\", _ReferencePaths);
                                        _ReferenceData.RemoveChild(_DeleteNode);
                                        _ReferenceData.AppendChild(CreateNode(_XmlDoc, "ReferencePath", _ReferencePath));
                                    }
                                }
                            }
                        }
                        _XmlDoc.Save(_FInfo.FullName);
                    }

                    catch(System.Exception ex)
                    {
                        CLogManager.AddSystemLog(CLogManager.LOG_TYPE.ERR, "RecipeCopyEventFunction Exception : " + ex.ToString(), CLogManager.LOG_LEVEL.LOW);
                    }
                }
            }

            CLogManager.AddSystemLog(CLogManager.LOG_TYPE.INFO, "RecipeWindow - RecipeCopy Complete", CLogManager.LOG_LEVEL.LOW);

            CParameterManager.SystemMode = eSysMode.RCP_MANUAL_CHANGE;

            var _RecipeCopyEvent = RecipeCopyEvent;
            _RecipeCopyEvent(_NewRecipeName, _SrcRecipeName);
            LoadRecipeList();
        }

        private XmlNode CreateNode(XmlDocument xmlDoc, string name, string innerXml)
        {
            XmlNode node = xmlDoc.CreateElement(string.Empty, name, string.Empty);
            node.InnerXml = innerXml;
            return node;
        }

        //LDH, 2019.01.16, Recipe 번호 선택
        private void labelRecipeNum_Click(object sender, EventArgs e)
        {
            SelectedRecipeNum = Convert.ToInt32(((GradientLabel)sender).Tag);
            SelectedRecipeChange();
        }

        //LDH, 2019.01.16, Recipe 변경시 List에서 Recipe 선택
        private void listBoxRecipe_DoubleClick(object sender, EventArgs e)
        {
            DialogResult _MsgBoxResult = DialogResult.Yes;

            if (!IsTotalRecipe) _MsgBoxResult = MessageBox.Show(string.Format("{0}번 Recipe를 변경하시겠습니까?", SelectedRecipeNum + 1), "Recipe 변경", MessageBoxButtons.YesNo);

            if (_MsgBoxResult == DialogResult.Yes)
            {
                bool _UseCheckFlag = false;

                for (int iLoopCount = 0; iLoopCount < UseRecipecount; iLoopCount++)
                {
                    if (txtboxCurrentRecipeName[iLoopCount].Text == listBoxRecipe.SelectedItem.ToString())
                    {
                        MessageBox.Show("중복되는 Recipe가 있습니다. Recipe를 변경하세요.");
                        _UseCheckFlag = true; break;
                    }
                }

                if (!_UseCheckFlag)
                {
                    if (IsTotalRecipe)
                    {
                        for (int jLoopCount = 0; jLoopCount < UseRecipecount; jLoopCount++)
                        {
                            txtboxCurrentRecipeName[jLoopCount].Text = listBoxRecipe.SelectedItem.ToString();
                        }
                    }
                    else txtboxCurrentRecipeName[SelectedRecipeNum].Text = listBoxRecipe.SelectedItem.ToString();
                }
            }
        }
    }
}
