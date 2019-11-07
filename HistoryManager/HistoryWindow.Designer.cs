namespace HistoryManager
{
    partial class HistoryWindow
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.labelTrainImage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.monthCalendarFrom = new System.Windows.Forms.MonthCalendar();
            this.monthCalendarTo = new System.Windows.Forms.MonthCalendar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.HistoryToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ckbRecipe = new System.Windows.Forms.CheckBox();
            this.ckbResult = new System.Windows.Forms.CheckBox();
            this.comboBoxResult = new System.Windows.Forms.ComboBox();
            this.btnNGType = new System.Windows.Forms.Button();
            this.ckbNGType = new System.Windows.Forms.CheckBox();
            this.ckListBoxRecipe = new System.Windows.Forms.CheckedListBox();
            this.ckListBoxNGType = new System.Windows.Forms.CheckedListBox();
            this.btnDropDownRecipe = new System.Windows.Forms.Button();
            this.btnAllDelete = new System.Windows.Forms.Button();
            this.btnSelectDelete = new System.Windows.Forms.Button();
            this.btnAllview = new System.Windows.Forms.Button();
            this.btnSelectView = new System.Windows.Forms.Button();
            this.pictureBoxScreenshot = new System.Windows.Forms.PictureBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.kpCogDisplayHistory = new KPDisplay.KPCogDisplayControl();
            this.dataGridViewSubHistory = new CustomControl.GridViewManager();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshot)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistory.Location = new System.Drawing.Point(3, 567);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.ReadOnly = true;
            this.dataGridViewHistory.RowTemplate.Height = 23;
            this.dataGridViewHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistory.Size = new System.Drawing.Size(506, 229);
            this.dataGridViewHistory.TabIndex = 0;
            this.dataGridViewHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHistory_CellClick);
            this.dataGridViewHistory.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHistory_RowEnter);
            this.dataGridViewHistory.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridViewHistory_RowPostPaint);
            // 
            // labelTrainImage
            // 
            this.labelTrainImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.labelTrainImage.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTrainImage.ForeColor = System.Drawing.Color.White;
            this.labelTrainImage.Location = new System.Drawing.Point(3, 3);
            this.labelTrainImage.Name = "labelTrainImage";
            this.labelTrainImage.Size = new System.Drawing.Size(758, 30);
            this.labelTrainImage.TabIndex = 20;
            this.labelTrainImage.Text = "Result Image";
            this.labelTrainImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 534);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1254, 30);
            this.label1.TabIndex = 21;
            this.label1.Text = "History";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(767, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(491, 30);
            this.label2.TabIndex = 22;
            this.label2.Text = "Search Option";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // monthCalendarFrom
            // 
            this.monthCalendarFrom.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.monthCalendarFrom.Location = new System.Drawing.Point(2, 36);
            this.monthCalendarFrom.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.monthCalendarFrom.Name = "monthCalendarFrom";
            this.monthCalendarFrom.ShowTodayCircle = false;
            this.monthCalendarFrom.TabIndex = 23;
            this.monthCalendarFrom.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarFrom_DateChanged);
            // 
            // monthCalendarTo
            // 
            this.monthCalendarTo.Location = new System.Drawing.Point(269, 36);
            this.monthCalendarTo.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.monthCalendarTo.Name = "monthCalendarTo";
            this.monthCalendarTo.ShowTodayCircle = false;
            this.monthCalendarTo.TabIndex = 23;
            this.monthCalendarTo.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendarTo_DateChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(235, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 21);
            this.label3.TabIndex = 26;
            this.label3.Text = "~";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.SlateGray;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 31);
            this.label4.TabIndex = 25;
            this.label4.Text = "Search Start Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Enabled = false;
            this.dateTimePickerFrom.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateTimePickerFrom.Location = new System.Drawing.Point(4, 204);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(217, 26);
            this.dateTimePickerFrom.TabIndex = 27;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Enabled = false;
            this.dateTimePickerTo.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.dateTimePickerTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.dateTimePickerTo.Location = new System.Drawing.Point(270, 204);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(218, 26);
            this.dateTimePickerTo.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.SlateGray;
            this.label5.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(270, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(218, 31);
            this.label5.TabIndex = 25;
            this.label5.Text = "Search End Date";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ckbRecipe
            // 
            this.ckbRecipe.AutoSize = true;
            this.ckbRecipe.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ckbRecipe.Location = new System.Drawing.Point(10, 11);
            this.ckbRecipe.Name = "ckbRecipe";
            this.ckbRecipe.Size = new System.Drawing.Size(72, 21);
            this.ckbRecipe.TabIndex = 32;
            this.ckbRecipe.Text = "Recipe";
            this.ckbRecipe.UseVisualStyleBackColor = true;
            this.ckbRecipe.CheckedChanged += new System.EventHandler(this.ckbRecipe_CheckedChanged);
            // 
            // ckbResult
            // 
            this.ckbResult.AutoSize = true;
            this.ckbResult.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ckbResult.Location = new System.Drawing.Point(10, 53);
            this.ckbResult.Name = "ckbResult";
            this.ckbResult.Size = new System.Drawing.Size(69, 21);
            this.ckbResult.TabIndex = 34;
            this.ckbResult.Text = "Result";
            this.ckbResult.UseVisualStyleBackColor = true;
            this.ckbResult.CheckedChanged += new System.EventHandler(this.ckbResult_CheckedChanged);
            // 
            // comboBoxResult
            // 
            this.comboBoxResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResult.Enabled = false;
            this.comboBoxResult.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBoxResult.FormattingEnabled = true;
            this.comboBoxResult.Items.AddRange(new object[] {
            "All",
            "NG",
            "OK"});
            this.comboBoxResult.Location = new System.Drawing.Point(119, 50);
            this.comboBoxResult.Name = "comboBoxResult";
            this.comboBoxResult.Size = new System.Drawing.Size(358, 25);
            this.comboBoxResult.TabIndex = 33;
            this.comboBoxResult.SelectedIndexChanged += new System.EventHandler(this.comboBoxResult_SelectedIndexChanged);
            // 
            // btnNGType
            // 
            this.btnNGType.Enabled = false;
            this.btnNGType.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnNGType.Image = global::HistoryManager.Properties.Resources.Down_small;
            this.btnNGType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNGType.Location = new System.Drawing.Point(118, 91);
            this.btnNGType.Name = "btnNGType";
            this.btnNGType.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.btnNGType.Size = new System.Drawing.Size(360, 28);
            this.btnNGType.TabIndex = 41;
            this.btnNGType.Text = "All";
            this.btnNGType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNGType.UseVisualStyleBackColor = true;
            this.btnNGType.Visible = false;
            this.btnNGType.Click += new System.EventHandler(this.btnNGType_Click);
            // 
            // ckbNGType
            // 
            this.ckbNGType.AutoSize = true;
            this.ckbNGType.Enabled = false;
            this.ckbNGType.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ckbNGType.Location = new System.Drawing.Point(10, 95);
            this.ckbNGType.Name = "ckbNGType";
            this.ckbNGType.Size = new System.Drawing.Size(84, 21);
            this.ckbNGType.TabIndex = 36;
            this.ckbNGType.Text = "NG Type";
            this.ckbNGType.UseVisualStyleBackColor = true;
            this.ckbNGType.Visible = false;
            this.ckbNGType.CheckedChanged += new System.EventHandler(this.ckbNGType_CheckedChanged);
            // 
            // ckListBoxRecipe
            // 
            this.ckListBoxRecipe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ckListBoxRecipe.CheckOnClick = true;
            this.ckListBoxRecipe.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ckListBoxRecipe.FormattingEnabled = true;
            this.ckListBoxRecipe.Location = new System.Drawing.Point(1079, 469);
            this.ckListBoxRecipe.Name = "ckListBoxRecipe";
            this.ckListBoxRecipe.Size = new System.Drawing.Size(82, 23);
            this.ckListBoxRecipe.TabIndex = 38;
            this.ckListBoxRecipe.Visible = false;
            // 
            // ckListBoxNGType
            // 
            this.ckListBoxNGType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ckListBoxNGType.CheckOnClick = true;
            this.ckListBoxNGType.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ckListBoxNGType.FormattingEnabled = true;
            this.ckListBoxNGType.Location = new System.Drawing.Point(1167, 469);
            this.ckListBoxNGType.Name = "ckListBoxNGType";
            this.ckListBoxNGType.Size = new System.Drawing.Size(82, 23);
            this.ckListBoxNGType.TabIndex = 39;
            this.ckListBoxNGType.Visible = false;
            this.ckListBoxNGType.SelectedIndexChanged += new System.EventHandler(this.ckListBoxNGType_SelectedIndexChanged);
            // 
            // btnDropDownRecipe
            // 
            this.btnDropDownRecipe.Enabled = false;
            this.btnDropDownRecipe.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDropDownRecipe.Image = global::HistoryManager.Properties.Resources.Down_small;
            this.btnDropDownRecipe.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDropDownRecipe.Location = new System.Drawing.Point(118, 6);
            this.btnDropDownRecipe.Name = "btnDropDownRecipe";
            this.btnDropDownRecipe.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.btnDropDownRecipe.Size = new System.Drawing.Size(360, 28);
            this.btnDropDownRecipe.TabIndex = 40;
            this.btnDropDownRecipe.Text = "Recipe List";
            this.btnDropDownRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDropDownRecipe.UseVisualStyleBackColor = true;
            this.btnDropDownRecipe.Click += new System.EventHandler(this.btnDropDownRecipe_Click);
            // 
            // btnAllDelete
            // 
            this.btnAllDelete.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAllDelete.Image = global::HistoryManager.Properties.Resources.Recycle;
            this.btnAllDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllDelete.Location = new System.Drawing.Point(1138, 479);
            this.btnAllDelete.Name = "btnAllDelete";
            this.btnAllDelete.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnAllDelete.Size = new System.Drawing.Size(120, 50);
            this.btnAllDelete.TabIndex = 24;
            this.btnAllDelete.Text = "           Delete All";
            this.btnAllDelete.UseVisualStyleBackColor = true;
            this.btnAllDelete.Click += new System.EventHandler(this.btnAllDelete_Click);
            // 
            // btnSelectDelete
            // 
            this.btnSelectDelete.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSelectDelete.Image = global::HistoryManager.Properties.Resources.database_delete;
            this.btnSelectDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectDelete.Location = new System.Drawing.Point(1014, 479);
            this.btnSelectDelete.Name = "btnSelectDelete";
            this.btnSelectDelete.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnSelectDelete.Size = new System.Drawing.Size(120, 50);
            this.btnSelectDelete.TabIndex = 24;
            this.btnSelectDelete.Text = "           Delete";
            this.btnSelectDelete.UseVisualStyleBackColor = true;
            this.btnSelectDelete.Click += new System.EventHandler(this.btnSelectDelete_Click);
            // 
            // btnAllview
            // 
            this.btnAllview.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAllview.Image = global::HistoryManager.Properties.Resources.AllView_35_2;
            this.btnAllview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllview.Location = new System.Drawing.Point(890, 479);
            this.btnAllview.Name = "btnAllview";
            this.btnAllview.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnAllview.Size = new System.Drawing.Size(120, 50);
            this.btnAllview.TabIndex = 24;
            this.btnAllview.Text = "             View All";
            this.btnAllview.UseVisualStyleBackColor = true;
            this.btnAllview.Click += new System.EventHandler(this.btnAllview_Click);
            // 
            // btnSelectView
            // 
            this.btnSelectView.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSelectView.Image = global::HistoryManager.Properties.Resources.planner_35_2;
            this.btnSelectView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSelectView.Location = new System.Drawing.Point(766, 479);
            this.btnSelectView.Name = "btnSelectView";
            this.btnSelectView.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnSelectView.Size = new System.Drawing.Size(120, 50);
            this.btnSelectView.TabIndex = 24;
            this.btnSelectView.Text = "           View Period";
            this.btnSelectView.UseVisualStyleBackColor = true;
            this.btnSelectView.Click += new System.EventHandler(this.btnSelectView_Click);
            // 
            // pictureBoxScreenshot
            // 
            this.pictureBoxScreenshot.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxScreenshot.Location = new System.Drawing.Point(631, 418);
            this.pictureBoxScreenshot.Name = "pictureBoxScreenshot";
            this.pictureBoxScreenshot.Size = new System.Drawing.Size(134, 135);
            this.pictureBoxScreenshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxScreenshot.TabIndex = 19;
            this.pictureBoxScreenshot.TabStop = false;
            this.pictureBoxScreenshot.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(1132, 800);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(124, 35);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "Confirm";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.SlateGray;
            this.label6.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(771, 292);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(484, 31);
            this.label6.TabIndex = 25;
            this.label6.Text = "Search Item";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ckbRecipe);
            this.panel1.Controls.Add(this.btnDropDownRecipe);
            this.panel1.Controls.Add(this.ckbNGType);
            this.panel1.Controls.Add(this.comboBoxResult);
            this.panel1.Controls.Add(this.ckbResult);
            this.panel1.Controls.Add(this.btnNGType);
            this.panel1.Location = new System.Drawing.Point(771, 326);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 137);
            this.panel1.TabIndex = 43;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1266, 30);
            this.labelTitle.TabIndex = 44;
            this.labelTitle.Text = " History window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.dateTimePickerTo);
            this.panel2.Controls.Add(this.dateTimePickerFrom);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.monthCalendarTo);
            this.panel2.Controls.Add(this.monthCalendarFrom);
            this.panel2.Location = new System.Drawing.Point(767, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(491, 432);
            this.panel2.TabIndex = 45;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.kpCogDisplayHistory);
            this.panelMain.Controls.Add(this.dataGridViewSubHistory);
            this.panelMain.Controls.Add(this.ckListBoxRecipe);
            this.panelMain.Controls.Add(this.label6);
            this.panelMain.Controls.Add(this.btnOk);
            this.panelMain.Controls.Add(this.ckListBoxNGType);
            this.panelMain.Controls.Add(this.dataGridViewHistory);
            this.panelMain.Controls.Add(this.btnAllDelete);
            this.panelMain.Controls.Add(this.btnSelectDelete);
            this.panelMain.Controls.Add(this.btnAllview);
            this.panelMain.Controls.Add(this.btnSelectView);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.labelTrainImage);
            this.panelMain.Controls.Add(this.pictureBoxScreenshot);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.panel2);
            this.panelMain.Location = new System.Drawing.Point(0, 32);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1266, 838);
            this.panelMain.TabIndex = 46;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // kpCogDisplayHistory
            // 
            this.kpCogDisplayHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpCogDisplayHistory.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpCogDisplayHistory.Location = new System.Drawing.Point(3, 36);
            this.kpCogDisplayHistory.Name = "kpCogDisplayHistory";
            this.kpCogDisplayHistory.Size = new System.Drawing.Size(758, 493);
            this.kpCogDisplayHistory.TabIndex = 47;
            this.kpCogDisplayHistory.UseStatusBar = true;
            // 
            // dataGridViewSubHistory
            // 
            this.dataGridViewSubHistory.Location = new System.Drawing.Point(512, 568);
            this.dataGridViewSubHistory.Name = "dataGridViewSubHistory";
            this.dataGridViewSubHistory.Size = new System.Drawing.Size(744, 227);
            this.dataGridViewSubHistory.TabIndex = 46;
            // 
            // HistoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1266, 873);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(8, 148);
            this.Name = "HistoryWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "History Window";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenshot)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBoxScreenshot;
        private System.Windows.Forms.Label labelTrainImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MonthCalendar monthCalendarFrom;
        private System.Windows.Forms.MonthCalendar monthCalendarTo;
        private System.Windows.Forms.Button btnSelectView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAllview;
        private System.Windows.Forms.Button btnSelectDelete;
        private System.Windows.Forms.Button btnAllDelete;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip HistoryToolTip;
        private System.Windows.Forms.Button btnNGType;
        private System.Windows.Forms.CheckBox ckbRecipe;
        private System.Windows.Forms.CheckBox ckbResult;
        private System.Windows.Forms.ComboBox comboBoxResult;
        private System.Windows.Forms.CheckBox ckbNGType;
        private System.Windows.Forms.CheckedListBox ckListBoxRecipe;
        private System.Windows.Forms.CheckedListBox ckListBoxNGType;
        private System.Windows.Forms.Button btnDropDownRecipe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelMain;
        private CustomControl.GridViewManager dataGridViewSubHistory;
        private KPDisplay.KPCogDisplayControl kpCogDisplayHistory;
    }
}