namespace InspectionSystemManager
{
    partial class TeachingWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tabControlTeach = new System.Windows.Forms.TabControl();
            this.tabPageInspectionArea = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMapDataAlgorithmSet = new System.Windows.Forms.Button();
            this.btnMapDataApplyInspectionArea = new System.Windows.Forms.Button();
            this.btnInspectionAreaCopy = new System.Windows.Forms.Button();
            this.labelInspectionArea = new System.Windows.Forms.Label();
            this.btnInspectionAreaSet = new System.Windows.Forms.Button();
            this.btnInspectionAreaDel = new System.Windows.Forms.Button();
            this.btnInspectionAreaAdd = new System.Windows.Forms.Button();
            this.gridViewArea = new System.Windows.Forms.DataGridView();
            this.gridAreaID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAreaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAreaBenchMark = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gridAreaEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.gridAreaNgNum = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.tabPageAlgorithmArea = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnShowAlgorithmMoveButton = new CPressingButton.PressButton();
            this.btnAlgorithmIndexMoveDown = new System.Windows.Forms.Button();
            this.btnAlgorithmIndexMoveUp = new System.Windows.Forms.Button();
            this.btnInspectionAlgoCopy = new System.Windows.Forms.Button();
            this.btnInspectionAlgoSet = new System.Windows.Forms.Button();
            this.gridViewAlgo = new System.Windows.Forms.DataGridView();
            this.gridAlgoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAlgoName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gridAlgoBenchMark = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.gridAlgoEnable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnInspectionAlgoDel = new System.Windows.Forms.Button();
            this.labelAlgorithmArea = new System.Windows.Forms.Label();
            this.btnInspectionAlgoAdd = new System.Windows.Forms.Button();
            this.btnShowAllArea = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.kpTeachDisplay = new KPDisplay.KPCogDisplayControl();
            this.panelTeaching = new CustomControl.PanelDoubleBuffer();
            this.gradientLabelTeaching = new CustomControl.GradientLabel();
            this.gradientLabelSettingWindowTitle = new CustomControl.GradientLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControlTeach.SuspendLayout();
            this.tabPageInspectionArea.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewArea)).BeginInit();
            this.tabPageAlgorithmArea.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlgo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelTeaching.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.Gold;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1262, 30);
            this.labelTitle.TabIndex = 8;
            this.labelTitle.Text = " Recipe Teaching window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.Location = new System.Drawing.Point(1152, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConfirm.Location = new System.Drawing.Point(1044, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(102, 35);
            this.btnConfirm.TabIndex = 13;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tabControlTeach
            // 
            this.tabControlTeach.Controls.Add(this.tabPageInspectionArea);
            this.tabControlTeach.Controls.Add(this.tabPageAlgorithmArea);
            this.tabControlTeach.Location = new System.Drawing.Point(676, 34);
            this.tabControlTeach.Name = "tabControlTeach";
            this.tabControlTeach.SelectedIndex = 0;
            this.tabControlTeach.Size = new System.Drawing.Size(583, 331);
            this.tabControlTeach.TabIndex = 16;
            // 
            // tabPageInspectionArea
            // 
            this.tabPageInspectionArea.Controls.Add(this.panel2);
            this.tabPageInspectionArea.Location = new System.Drawing.Point(4, 23);
            this.tabPageInspectionArea.Name = "tabPageInspectionArea";
            this.tabPageInspectionArea.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInspectionArea.Size = new System.Drawing.Size(575, 304);
            this.tabPageInspectionArea.TabIndex = 0;
            this.tabPageInspectionArea.Text = "Inspection Area";
            this.tabPageInspectionArea.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnMapDataAlgorithmSet);
            this.panel2.Controls.Add(this.btnMapDataApplyInspectionArea);
            this.panel2.Controls.Add(this.btnInspectionAreaCopy);
            this.panel2.Controls.Add(this.labelInspectionArea);
            this.panel2.Controls.Add(this.btnInspectionAreaSet);
            this.panel2.Controls.Add(this.btnInspectionAreaDel);
            this.panel2.Controls.Add(this.btnInspectionAreaAdd);
            this.panel2.Controls.Add(this.gridViewArea);
            this.panel2.Location = new System.Drawing.Point(0, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(575, 297);
            this.panel2.TabIndex = 11;
            // 
            // btnMapDataAlgorithmSet
            // 
            this.btnMapDataAlgorithmSet.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMapDataAlgorithmSet.Location = new System.Drawing.Point(497, 255);
            this.btnMapDataAlgorithmSet.Name = "btnMapDataAlgorithmSet";
            this.btnMapDataAlgorithmSet.Size = new System.Drawing.Size(74, 38);
            this.btnMapDataAlgorithmSet.TabIndex = 10;
            this.btnMapDataAlgorithmSet.Text = "Set\r\nMap Data";
            this.btnMapDataAlgorithmSet.UseVisualStyleBackColor = true;
            this.btnMapDataAlgorithmSet.Click += new System.EventHandler(this.btnMapDataAlgorithmSet_Click);
            // 
            // btnMapDataApplyInspectionArea
            // 
            this.btnMapDataApplyInspectionArea.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMapDataApplyInspectionArea.Location = new System.Drawing.Point(497, 213);
            this.btnMapDataApplyInspectionArea.Name = "btnMapDataApplyInspectionArea";
            this.btnMapDataApplyInspectionArea.Size = new System.Drawing.Size(74, 38);
            this.btnMapDataApplyInspectionArea.TabIndex = 9;
            this.btnMapDataApplyInspectionArea.Text = "Apply\r\nMap Data";
            this.btnMapDataApplyInspectionArea.UseVisualStyleBackColor = true;
            this.btnMapDataApplyInspectionArea.Click += new System.EventHandler(this.btnMapDataApplyInspectionArea_Click);
            // 
            // btnInspectionAreaCopy
            // 
            this.btnInspectionAreaCopy.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAreaCopy.Location = new System.Drawing.Point(497, 162);
            this.btnInspectionAreaCopy.Name = "btnInspectionAreaCopy";
            this.btnInspectionAreaCopy.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAreaCopy.TabIndex = 8;
            this.btnInspectionAreaCopy.Text = "Inspection Area Copy";
            this.btnInspectionAreaCopy.UseVisualStyleBackColor = true;
            this.btnInspectionAreaCopy.Click += new System.EventHandler(this.btnInspectionAreaCopy_Click);
            // 
            // labelInspectionArea
            // 
            this.labelInspectionArea.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelInspectionArea.ForeColor = System.Drawing.Color.DarkRed;
            this.labelInspectionArea.Location = new System.Drawing.Point(3, 1);
            this.labelInspectionArea.Name = "labelInspectionArea";
            this.labelInspectionArea.Size = new System.Drawing.Size(108, 27);
            this.labelInspectionArea.TabIndex = 6;
            this.labelInspectionArea.Text = "Inspection Area";
            this.labelInspectionArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnInspectionAreaSet
            // 
            this.btnInspectionAreaSet.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAreaSet.Location = new System.Drawing.Point(497, 30);
            this.btnInspectionAreaSet.Name = "btnInspectionAreaSet";
            this.btnInspectionAreaSet.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAreaSet.TabIndex = 7;
            this.btnInspectionAreaSet.Text = "Inspection Area Set";
            this.btnInspectionAreaSet.UseVisualStyleBackColor = true;
            this.btnInspectionAreaSet.Click += new System.EventHandler(this.btnInspectionAreaSet_Click);
            // 
            // btnInspectionAreaDel
            // 
            this.btnInspectionAreaDel.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAreaDel.Location = new System.Drawing.Point(497, 118);
            this.btnInspectionAreaDel.Name = "btnInspectionAreaDel";
            this.btnInspectionAreaDel.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAreaDel.TabIndex = 2;
            this.btnInspectionAreaDel.Text = "Inspection Area -";
            this.btnInspectionAreaDel.UseVisualStyleBackColor = true;
            this.btnInspectionAreaDel.Click += new System.EventHandler(this.btnInspectionAreaDel_Click);
            // 
            // btnInspectionAreaAdd
            // 
            this.btnInspectionAreaAdd.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAreaAdd.Location = new System.Drawing.Point(497, 74);
            this.btnInspectionAreaAdd.Name = "btnInspectionAreaAdd";
            this.btnInspectionAreaAdd.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAreaAdd.TabIndex = 2;
            this.btnInspectionAreaAdd.Text = "Inspection Area +";
            this.btnInspectionAreaAdd.UseVisualStyleBackColor = true;
            this.btnInspectionAreaAdd.Click += new System.EventHandler(this.btnInspectionAreaAdd_Click);
            // 
            // gridViewArea
            // 
            this.gridViewArea.AllowUserToAddRows = false;
            this.gridViewArea.AllowUserToDeleteRows = false;
            this.gridViewArea.AllowUserToResizeColumns = false;
            this.gridViewArea.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.gridViewArea.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridViewArea.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewArea.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridViewArea.ColumnHeadersHeight = 22;
            this.gridViewArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridViewArea.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridAreaID,
            this.gridAreaName,
            this.gridAreaBenchMark,
            this.gridAreaEnable,
            this.gridAreaNgNum});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridViewArea.DefaultCellStyle = dataGridViewCellStyle4;
            this.gridViewArea.EnableHeadersVisualStyles = false;
            this.gridViewArea.Location = new System.Drawing.Point(2, 31);
            this.gridViewArea.MultiSelect = false;
            this.gridViewArea.Name = "gridViewArea";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewArea.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridViewArea.RowHeadersVisible = false;
            this.gridViewArea.RowTemplate.Height = 23;
            this.gridViewArea.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewArea.Size = new System.Drawing.Size(490, 262);
            this.gridViewArea.TabIndex = 3;
            this.gridViewArea.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewArea_CellContentClick);
            this.gridViewArea.CurrentCellChanged += new System.EventHandler(this.gridViewArea_CurrentCellChanged);
            // 
            // gridAreaID
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.gridAreaID.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridAreaID.HeaderText = "ID";
            this.gridAreaID.Name = "gridAreaID";
            this.gridAreaID.ReadOnly = true;
            this.gridAreaID.Width = 40;
            // 
            // gridAreaName
            // 
            this.gridAreaName.HeaderText = "Name";
            this.gridAreaName.Name = "gridAreaName";
            this.gridAreaName.ReadOnly = true;
            this.gridAreaName.Width = 200;
            // 
            // gridAreaBenchMark
            // 
            this.gridAreaBenchMark.HeaderText = "BenchMark";
            this.gridAreaBenchMark.Name = "gridAreaBenchMark";
            this.gridAreaBenchMark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAreaBenchMark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.gridAreaBenchMark.Width = 80;
            // 
            // gridAreaEnable
            // 
            this.gridAreaEnable.HeaderText = "Enable";
            this.gridAreaEnable.Name = "gridAreaEnable";
            this.gridAreaEnable.ReadOnly = true;
            this.gridAreaEnable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAreaEnable.Width = 80;
            // 
            // gridAreaNgNum
            // 
            this.gridAreaNgNum.HeaderText = "Ng Area";
            this.gridAreaNgNum.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.gridAreaNgNum.Name = "gridAreaNgNum";
            this.gridAreaNgNum.Width = 85;
            // 
            // tabPageAlgorithmArea
            // 
            this.tabPageAlgorithmArea.Controls.Add(this.panel3);
            this.tabPageAlgorithmArea.Location = new System.Drawing.Point(4, 23);
            this.tabPageAlgorithmArea.Name = "tabPageAlgorithmArea";
            this.tabPageAlgorithmArea.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAlgorithmArea.Size = new System.Drawing.Size(575, 304);
            this.tabPageAlgorithmArea.TabIndex = 1;
            this.tabPageAlgorithmArea.Text = "Algorithm Area";
            this.tabPageAlgorithmArea.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnShowAlgorithmMoveButton);
            this.panel3.Controls.Add(this.btnAlgorithmIndexMoveDown);
            this.panel3.Controls.Add(this.btnAlgorithmIndexMoveUp);
            this.panel3.Controls.Add(this.btnInspectionAlgoCopy);
            this.panel3.Controls.Add(this.btnInspectionAlgoSet);
            this.panel3.Controls.Add(this.gridViewAlgo);
            this.panel3.Controls.Add(this.btnInspectionAlgoDel);
            this.panel3.Controls.Add(this.labelAlgorithmArea);
            this.panel3.Controls.Add(this.btnInspectionAlgoAdd);
            this.panel3.Location = new System.Drawing.Point(0, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(575, 299);
            this.panel3.TabIndex = 2;
            // 
            // btnShowAlgorithmMoveButton
            // 
            this.btnShowAlgorithmMoveButton.BackColor = System.Drawing.Color.Maroon;
            this.btnShowAlgorithmMoveButton.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnShowAlgorithmMoveButton.ForeColor = System.Drawing.Color.White;
            this.btnShowAlgorithmMoveButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShowAlgorithmMoveButton.Location = new System.Drawing.Point(552, 273);
            this.btnShowAlgorithmMoveButton.Name = "btnShowAlgorithmMoveButton";
            this.btnShowAlgorithmMoveButton.Size = new System.Drawing.Size(20, 20);
            this.btnShowAlgorithmMoveButton.TabIndex = 263;
            this.btnShowAlgorithmMoveButton.Tag = "0";
            this.btnShowAlgorithmMoveButton.Text = "!";
            this.btnShowAlgorithmMoveButton.UseVisualStyleBackColor = false;
            this.btnShowAlgorithmMoveButton.MousePressing += new System.EventHandler(this.btnShowAlgorithmMoveButton_MousePressing);
            // 
            // btnAlgorithmIndexMoveDown
            // 
            this.btnAlgorithmIndexMoveDown.Location = new System.Drawing.Point(498, 263);
            this.btnAlgorithmIndexMoveDown.Name = "btnAlgorithmIndexMoveDown";
            this.btnAlgorithmIndexMoveDown.Size = new System.Drawing.Size(30, 30);
            this.btnAlgorithmIndexMoveDown.TabIndex = 11;
            this.btnAlgorithmIndexMoveDown.Text = "▼";
            this.btnAlgorithmIndexMoveDown.UseVisualStyleBackColor = true;
            this.btnAlgorithmIndexMoveDown.Visible = false;
            this.btnAlgorithmIndexMoveDown.Click += new System.EventHandler(this.btnAlgorithmIndexMoveDown_Click);
            // 
            // btnAlgorithmIndexMoveUp
            // 
            this.btnAlgorithmIndexMoveUp.Location = new System.Drawing.Point(498, 230);
            this.btnAlgorithmIndexMoveUp.Name = "btnAlgorithmIndexMoveUp";
            this.btnAlgorithmIndexMoveUp.Size = new System.Drawing.Size(30, 30);
            this.btnAlgorithmIndexMoveUp.TabIndex = 10;
            this.btnAlgorithmIndexMoveUp.Text = "▲";
            this.btnAlgorithmIndexMoveUp.UseVisualStyleBackColor = true;
            this.btnAlgorithmIndexMoveUp.Visible = false;
            this.btnAlgorithmIndexMoveUp.Click += new System.EventHandler(this.btnAlgorithmIndexMoveUp_Click);
            // 
            // btnInspectionAlgoCopy
            // 
            this.btnInspectionAlgoCopy.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAlgoCopy.Location = new System.Drawing.Point(497, 162);
            this.btnInspectionAlgoCopy.Name = "btnInspectionAlgoCopy";
            this.btnInspectionAlgoCopy.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAlgoCopy.TabIndex = 9;
            this.btnInspectionAlgoCopy.Text = "Algorithm Copy";
            this.btnInspectionAlgoCopy.UseVisualStyleBackColor = true;
            this.btnInspectionAlgoCopy.Visible = false;
            this.btnInspectionAlgoCopy.Click += new System.EventHandler(this.btnInspectionAlgoCopy_Click);
            // 
            // btnInspectionAlgoSet
            // 
            this.btnInspectionAlgoSet.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAlgoSet.Location = new System.Drawing.Point(497, 30);
            this.btnInspectionAlgoSet.Name = "btnInspectionAlgoSet";
            this.btnInspectionAlgoSet.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAlgoSet.TabIndex = 8;
            this.btnInspectionAlgoSet.Text = "Algorithm Set";
            this.btnInspectionAlgoSet.UseVisualStyleBackColor = true;
            this.btnInspectionAlgoSet.Click += new System.EventHandler(this.btnInspectionAlgoSet_Click);
            // 
            // gridViewAlgo
            // 
            this.gridViewAlgo.AllowUserToAddRows = false;
            this.gridViewAlgo.AllowUserToDeleteRows = false;
            this.gridViewAlgo.AllowUserToResizeColumns = false;
            this.gridViewAlgo.AllowUserToResizeRows = false;
            this.gridViewAlgo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridViewAlgo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridViewAlgo.ColumnHeadersHeight = 22;
            this.gridViewAlgo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridViewAlgo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gridAlgoID,
            this.gridAlgoName,
            this.gridAlgoBenchMark,
            this.gridAlgoEnable});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridViewAlgo.DefaultCellStyle = dataGridViewCellStyle7;
            this.gridViewAlgo.EnableHeadersVisualStyles = false;
            this.gridViewAlgo.Location = new System.Drawing.Point(2, 31);
            this.gridViewAlgo.MultiSelect = false;
            this.gridViewAlgo.Name = "gridViewAlgo";
            this.gridViewAlgo.RowHeadersVisible = false;
            this.gridViewAlgo.RowTemplate.Height = 23;
            this.gridViewAlgo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewAlgo.Size = new System.Drawing.Size(490, 262);
            this.gridViewAlgo.TabIndex = 3;
            this.gridViewAlgo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewAlgo_CellClick);
            this.gridViewAlgo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewAlgo_CellContentClick);
            this.gridViewAlgo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridViewAlgo_CellMouseDoubleClick);
            this.gridViewAlgo.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridViewAlgo_CellMouseDown);
            // 
            // gridAlgoID
            // 
            this.gridAlgoID.HeaderText = "ID";
            this.gridAlgoID.Name = "gridAlgoID";
            this.gridAlgoID.Width = 40;
            // 
            // gridAlgoName
            // 
            this.gridAlgoName.HeaderText = "Name";
            this.gridAlgoName.Name = "gridAlgoName";
            this.gridAlgoName.ReadOnly = true;
            this.gridAlgoName.Width = 200;
            // 
            // gridAlgoBenchMark
            // 
            this.gridAlgoBenchMark.HeaderText = "BenchMark";
            this.gridAlgoBenchMark.Name = "gridAlgoBenchMark";
            this.gridAlgoBenchMark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.gridAlgoBenchMark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.gridAlgoBenchMark.Width = 80;
            // 
            // gridAlgoEnable
            // 
            this.gridAlgoEnable.HeaderText = "Enable";
            this.gridAlgoEnable.Name = "gridAlgoEnable";
            this.gridAlgoEnable.ReadOnly = true;
            this.gridAlgoEnable.Width = 80;
            // 
            // btnInspectionAlgoDel
            // 
            this.btnInspectionAlgoDel.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAlgoDel.Location = new System.Drawing.Point(497, 118);
            this.btnInspectionAlgoDel.Name = "btnInspectionAlgoDel";
            this.btnInspectionAlgoDel.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAlgoDel.TabIndex = 2;
            this.btnInspectionAlgoDel.Text = "Algorithm -";
            this.btnInspectionAlgoDel.UseVisualStyleBackColor = true;
            this.btnInspectionAlgoDel.Click += new System.EventHandler(this.btnInspectionAlgoDel_Click);
            // 
            // labelAlgorithmArea
            // 
            this.labelAlgorithmArea.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelAlgorithmArea.ForeColor = System.Drawing.Color.DarkRed;
            this.labelAlgorithmArea.Location = new System.Drawing.Point(3, 1);
            this.labelAlgorithmArea.Name = "labelAlgorithmArea";
            this.labelAlgorithmArea.Size = new System.Drawing.Size(108, 27);
            this.labelAlgorithmArea.TabIndex = 6;
            this.labelAlgorithmArea.Text = "Algorithm Area";
            this.labelAlgorithmArea.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnInspectionAlgoAdd
            // 
            this.btnInspectionAlgoAdd.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnInspectionAlgoAdd.Location = new System.Drawing.Point(497, 74);
            this.btnInspectionAlgoAdd.Name = "btnInspectionAlgoAdd";
            this.btnInspectionAlgoAdd.Size = new System.Drawing.Size(74, 38);
            this.btnInspectionAlgoAdd.TabIndex = 2;
            this.btnInspectionAlgoAdd.Text = "Algorithm +";
            this.btnInspectionAlgoAdd.UseVisualStyleBackColor = true;
            this.btnInspectionAlgoAdd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnInspectionAlgoAdd_MouseUp);
            // 
            // btnShowAllArea
            // 
            this.btnShowAllArea.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowAllArea.Location = new System.Drawing.Point(828, 3);
            this.btnShowAllArea.Name = "btnShowAllArea";
            this.btnShowAllArea.Size = new System.Drawing.Size(102, 35);
            this.btnShowAllArea.TabIndex = 10;
            this.btnShowAllArea.Text = "Show All Area";
            this.btnShowAllArea.UseVisualStyleBackColor = true;
            this.btnShowAllArea.Click += new System.EventHandler(this.btnShowAllArea_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnShowAllArea);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.labelStatus);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Location = new System.Drawing.Point(2, 790);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1257, 40);
            this.panel1.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.Location = new System.Drawing.Point(936, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 35);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.White;
            this.labelStatus.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelStatus.Location = new System.Drawing.Point(7, 4);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(663, 30);
            this.labelStatus.TabIndex = 15;
            this.labelStatus.Text = "Status";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // kpTeachDisplay
            // 
            this.kpTeachDisplay.BackColor = System.Drawing.Color.White;
            this.kpTeachDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpTeachDisplay.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpTeachDisplay.Location = new System.Drawing.Point(2, 2);
            this.kpTeachDisplay.Name = "kpTeachDisplay";
            this.kpTeachDisplay.Size = new System.Drawing.Size(672, 785);
            this.kpTeachDisplay.TabIndex = 9;
            this.kpTeachDisplay.UseStatusBar = true;
            // 
            // panelTeaching
            // 
            this.panelTeaching.BackColor = System.Drawing.Color.LightGray;
            this.panelTeaching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTeaching.Controls.Add(this.gradientLabelTeaching);
            this.panelTeaching.Location = new System.Drawing.Point(677, 367);
            this.panelTeaching.Name = "panelTeaching";
            this.panelTeaching.Size = new System.Drawing.Size(583, 420);
            this.panelTeaching.TabIndex = 15;
            // 
            // gradientLabelTeaching
            // 
            this.gradientLabelTeaching.BackColor = System.Drawing.Color.White;
            this.gradientLabelTeaching.ColorBottom = System.Drawing.Color.White;
            this.gradientLabelTeaching.ColorTop = System.Drawing.Color.SteelBlue;
            this.gradientLabelTeaching.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabelTeaching.ForeColor = System.Drawing.Color.White;
            this.gradientLabelTeaching.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabelTeaching.Location = new System.Drawing.Point(1, 0);
            this.gradientLabelTeaching.Name = "gradientLabelTeaching";
            this.gradientLabelTeaching.Size = new System.Drawing.Size(580, 30);
            this.gradientLabelTeaching.TabIndex = 19;
            this.gradientLabelTeaching.Text = " Teaching Window";
            this.gradientLabelTeaching.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gradientLabelSettingWindowTitle
            // 
            this.gradientLabelSettingWindowTitle.BackColor = System.Drawing.Color.White;
            this.gradientLabelSettingWindowTitle.ColorBottom = System.Drawing.Color.White;
            this.gradientLabelSettingWindowTitle.ColorTop = System.Drawing.Color.SteelBlue;
            this.gradientLabelSettingWindowTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabelSettingWindowTitle.ForeColor = System.Drawing.Color.White;
            this.gradientLabelSettingWindowTitle.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabelSettingWindowTitle.Location = new System.Drawing.Point(676, 3);
            this.gradientLabelSettingWindowTitle.Name = "gradientLabelSettingWindowTitle";
            this.gradientLabelSettingWindowTitle.Size = new System.Drawing.Size(583, 30);
            this.gradientLabelSettingWindowTitle.TabIndex = 18;
            this.gradientLabelSettingWindowTitle.Text = " Inspection and Algorithm Area Setting Window";
            this.gradientLabelSettingWindowTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.kpTeachDisplay);
            this.panelMain.Controls.Add(this.gradientLabelSettingWindowTitle);
            this.panelMain.Controls.Add(this.panelTeaching);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.tabControlTeach);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMain.Location = new System.Drawing.Point(0, 32);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1262, 833);
            this.panelMain.TabIndex = 19;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // TeachingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1262, 865);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(10, 149);
            this.Name = "TeachingWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "TeachingWindow";
            this.tabControlTeach.ResumeLayout(false);
            this.tabPageInspectionArea.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewArea)).EndInit();
            this.tabPageAlgorithmArea.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAlgo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panelTeaching.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private KPDisplay.KPCogDisplayControl kpTeachDisplay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private CustomControl.PanelDoubleBuffer panelTeaching;
        private System.Windows.Forms.TabControl tabControlTeach;
        private System.Windows.Forms.TabPage tabPageInspectionArea;
        private System.Windows.Forms.TabPage tabPageAlgorithmArea;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInspectionAreaCopy;
        private System.Windows.Forms.Label labelInspectionArea;
        private System.Windows.Forms.Button btnInspectionAreaSet;
        private System.Windows.Forms.Button btnInspectionAreaDel;
        private System.Windows.Forms.Button btnInspectionAreaAdd;
        private System.Windows.Forms.DataGridView gridViewArea;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnAlgorithmIndexMoveDown;
        private System.Windows.Forms.Button btnAlgorithmIndexMoveUp;
        private System.Windows.Forms.Button btnInspectionAlgoCopy;
        private System.Windows.Forms.Button btnInspectionAlgoSet;
        private System.Windows.Forms.DataGridView gridViewAlgo;
        private System.Windows.Forms.Button btnInspectionAlgoDel;
        private System.Windows.Forms.Label labelAlgorithmArea;
        private System.Windows.Forms.Button btnInspectionAlgoAdd;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button btnSave;
        private CustomControl.GradientLabel gradientLabelSettingWindowTitle;
        private CustomControl.GradientLabel gradientLabelTeaching;
        private CPressingButton.PressButton btnShowAlgorithmMoveButton;
        private System.Windows.Forms.Button btnMapDataApplyInspectionArea;
        private System.Windows.Forms.Button btnShowAllArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridAreaID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridAreaName;
        private System.Windows.Forms.DataGridViewComboBoxColumn gridAreaBenchMark;
        private System.Windows.Forms.DataGridViewCheckBoxColumn gridAreaEnable;
        private System.Windows.Forms.DataGridViewComboBoxColumn gridAreaNgNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridAlgoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn gridAlgoName;
        private System.Windows.Forms.DataGridViewComboBoxColumn gridAlgoBenchMark;
        private System.Windows.Forms.DataGridViewCheckBoxColumn gridAlgoEnable;
        private System.Windows.Forms.Button btnMapDataAlgorithmSet;
        private System.Windows.Forms.Panel panelMain;
    }
}