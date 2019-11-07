namespace KPVisionInspectionFramework
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ribbonMain = new System.Windows.Forms.Ribbon();
            this.ribbonTabInspectionMain = new System.Windows.Forms.RibbonTab();
            this.ribbonPanelOperating = new System.Windows.Forms.RibbonPanel();
            this.rbStart = new System.Windows.Forms.RibbonButton();
            this.rbStop = new System.Windows.Forms.RibbonButton();
            this.rbCalibration = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelSetting = new System.Windows.Forms.RibbonPanel();
            this.rbAlign = new System.Windows.Forms.RibbonButton();
            this.rbCommunication = new System.Windows.Forms.RibbonButton();
            this.rbEthernet = new System.Windows.Forms.RibbonButton();
            this.rbSerial = new System.Windows.Forms.RibbonButton();
            this.rbLight = new System.Windows.Forms.RibbonButton();
            this.rbDIO = new System.Windows.Forms.RibbonButton();
            this.rbConfig = new System.Windows.Forms.RibbonButton();
            this.rbMapData = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelData = new System.Windows.Forms.RibbonPanel();
            this.rbRecipe = new System.Windows.Forms.RibbonButton();
            this.rbLog = new System.Windows.Forms.RibbonButton();
            this.rbHistory = new System.Windows.Forms.RibbonButton();
            this.rbFolder = new System.Windows.Forms.RibbonButton();
            this.rbSave = new System.Windows.Forms.RibbonButton();
            this.ribbonPanelStatus = new System.Windows.Forms.RibbonPanel();
            this.rbLabelCurrentRecipe = new System.Windows.Forms.RibbonLabel();
            this.ribbonPanelSystem = new System.Windows.Forms.RibbonPanel();
            this.rbExit = new System.Windows.Forms.RibbonButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ribbonMain
            // 
            this.ribbonMain.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ribbonMain.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.ribbonMain.ForeColor = System.Drawing.SystemColors.Window;
            this.ribbonMain.Location = new System.Drawing.Point(0, 0);
            this.ribbonMain.Minimized = false;
            this.ribbonMain.Name = "ribbonMain";
            // 
            // 
            // 
            this.ribbonMain.OrbDropDown.BorderRoundness = 8;
            this.ribbonMain.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbonMain.OrbDropDown.Name = "";
            this.ribbonMain.OrbDropDown.Size = new System.Drawing.Size(527, 72);
            this.ribbonMain.OrbDropDown.TabIndex = 0;
            this.ribbonMain.OrbImage = null;
            this.ribbonMain.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbonMain.RibbonTabFont = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.ribbonMain.Size = new System.Drawing.Size(1351, 140);
            this.ribbonMain.TabIndex = 0;
            this.ribbonMain.Tabs.Add(this.ribbonTabInspectionMain);
            this.ribbonMain.TabsMargin = new System.Windows.Forms.Padding(12, 26, 20, 0);
            this.ribbonMain.ThemeColor = System.Windows.Forms.RibbonTheme.Black;
            // 
            // ribbonTabInspectionMain
            // 
            this.ribbonTabInspectionMain.Panels.Add(this.ribbonPanelOperating);
            this.ribbonTabInspectionMain.Panels.Add(this.ribbonPanelSetting);
            this.ribbonTabInspectionMain.Panels.Add(this.ribbonPanelData);
            this.ribbonTabInspectionMain.Panels.Add(this.ribbonPanelStatus);
            this.ribbonTabInspectionMain.Panels.Add(this.ribbonPanelSystem);
            this.ribbonTabInspectionMain.Text = "Inspection Main";
            // 
            // ribbonPanelOperating
            // 
            this.ribbonPanelOperating.ButtonMoreVisible = false;
            this.ribbonPanelOperating.Items.Add(this.rbStart);
            this.ribbonPanelOperating.Items.Add(this.rbStop);
            this.ribbonPanelOperating.Items.Add(this.rbCalibration);
            this.ribbonPanelOperating.Text = "Inspection Operating";
            // 
            // rbStart
            // 
            this.rbStart.Image = global::KPVisionInspectionFramework.Properties.Resources.Start;
            this.rbStart.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbStart.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbStart.SmallImage")));
            this.rbStart.Text = " Auto";
            this.rbStart.Click += new System.EventHandler(this.rbStart_Click);
            // 
            // rbStop
            // 
            this.rbStop.Image = global::KPVisionInspectionFramework.Properties.Resources.Stop;
            this.rbStop.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbStop.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbStop.SmallImage")));
            this.rbStop.Text = " Stop";
            this.rbStop.Click += new System.EventHandler(this.rbStop_Click);
            // 
            // rbCalibration
            // 
            this.rbCalibration.Image = global::KPVisionInspectionFramework.Properties.Resources.Calibration;
            this.rbCalibration.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbCalibration.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbCalibration.SmallImage")));
            this.rbCalibration.Text = " Cal";
            this.rbCalibration.Click += new System.EventHandler(this.rbCalibration_Click);
            // 
            // ribbonPanelSetting
            // 
            this.ribbonPanelSetting.Items.Add(this.rbAlign);
            this.ribbonPanelSetting.Items.Add(this.rbCommunication);
            this.ribbonPanelSetting.Items.Add(this.rbEthernet);
            this.ribbonPanelSetting.Items.Add(this.rbSerial);
            this.ribbonPanelSetting.Items.Add(this.rbLight);
            this.ribbonPanelSetting.Items.Add(this.rbDIO);
            this.ribbonPanelSetting.Items.Add(this.rbConfig);
            this.ribbonPanelSetting.Items.Add(this.rbMapData);
            this.ribbonPanelSetting.Text = "Setting ";
            // 
            // rbAlign
            // 
            this.rbAlign.Image = ((System.Drawing.Image)(resources.GetObject("rbAlign.Image")));
            this.rbAlign.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbAlign.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbAlign.SmallImage")));
            this.rbAlign.Text = "Align ";
            // 
            // rbCommunication
            // 
            this.rbCommunication.Image = global::KPVisionInspectionFramework.Properties.Resources.PLC;
            this.rbCommunication.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbCommunication.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbCommunication.SmallImage")));
            this.rbCommunication.Text = " PLC";
            this.rbCommunication.Click += new System.EventHandler(this.rbCommunication_Click);
            // 
            // rbEthernet
            // 
            this.rbEthernet.Image = global::KPVisionInspectionFramework.Properties.Resources.Ethernet;
            this.rbEthernet.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbEthernet.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbEthernet.SmallImage")));
            this.rbEthernet.Text = " Ethernet";
            this.rbEthernet.Click += new System.EventHandler(this.rbEthernet_Click);
            // 
            // rbSerial
            // 
            this.rbSerial.Image = global::KPVisionInspectionFramework.Properties.Resources.Serial;
            this.rbSerial.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbSerial.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSerial.SmallImage")));
            this.rbSerial.Text = "Serial ";
            this.rbSerial.Click += new System.EventHandler(this.rbSerial_Click);
            // 
            // rbLight
            // 
            this.rbLight.Image = global::KPVisionInspectionFramework.Properties.Resources.Light;
            this.rbLight.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbLight.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbLight.SmallImage")));
            this.rbLight.Text = " Light";
            this.rbLight.Click += new System.EventHandler(this.rbLight_Click);
            // 
            // rbDIO
            // 
            this.rbDIO.Image = global::KPVisionInspectionFramework.Properties.Resources.DIO;
            this.rbDIO.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbDIO.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbDIO.SmallImage")));
            this.rbDIO.Text = " DIO";
            this.rbDIO.Click += new System.EventHandler(this.rbDIO_Click);
            // 
            // rbConfig
            // 
            this.rbConfig.Image = global::KPVisionInspectionFramework.Properties.Resources.Config;
            this.rbConfig.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbConfig.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbConfig.SmallImage")));
            this.rbConfig.Text = " Config ";
            this.rbConfig.Click += new System.EventHandler(this.rbConfig_Click);
            // 
            // rbMapData
            // 
            this.rbMapData.Image = global::KPVisionInspectionFramework.Properties.Resources.MapData;
            this.rbMapData.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbMapData.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbMapData.SmallImage")));
            this.rbMapData.Text = " MapData ";
            this.rbMapData.Click += new System.EventHandler(this.rbMapData_Click);
            // 
            // ribbonPanelData
            // 
            this.ribbonPanelData.Items.Add(this.rbRecipe);
            this.ribbonPanelData.Items.Add(this.rbLog);
            this.ribbonPanelData.Items.Add(this.rbHistory);
            this.ribbonPanelData.Items.Add(this.rbFolder);
            this.ribbonPanelData.Items.Add(this.rbSave);
            this.ribbonPanelData.Text = "Data";
            // 
            // rbRecipe
            // 
            this.rbRecipe.Image = global::KPVisionInspectionFramework.Properties.Resources.Recipe;
            this.rbRecipe.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbRecipe.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbRecipe.SmallImage")));
            this.rbRecipe.Text = "Recipe ";
            this.rbRecipe.Click += new System.EventHandler(this.rbRecipe_Click);
            // 
            // rbLog
            // 
            this.rbLog.Image = global::KPVisionInspectionFramework.Properties.Resources.Log;
            this.rbLog.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbLog.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbLog.SmallImage")));
            this.rbLog.Text = "Log ";
            this.rbLog.Click += new System.EventHandler(this.rbLog_Click);
            // 
            // rbHistory
            // 
            this.rbHistory.Image = global::KPVisionInspectionFramework.Properties.Resources.History;
            this.rbHistory.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbHistory.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbHistory.SmallImage")));
            this.rbHistory.Text = "History ";
            this.rbHistory.Click += new System.EventHandler(this.rbHistory_Click);
            // 
            // rbFolder
            // 
            this.rbFolder.Image = global::KPVisionInspectionFramework.Properties.Resources.Folder;
            this.rbFolder.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbFolder.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbFolder.SmallImage")));
            this.rbFolder.Text = "Folder ";
            this.rbFolder.Click += new System.EventHandler(this.rbFolder_Click);
            // 
            // rbSave
            // 
            this.rbSave.Image = ((System.Drawing.Image)(resources.GetObject("rbSave.Image")));
            this.rbSave.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbSave.SmallImage")));
            this.rbSave.Text = "Save ";
            this.rbSave.Click += new System.EventHandler(this.rbSave_Click);
            // 
            // ribbonPanelStatus
            // 
            this.ribbonPanelStatus.Items.Add(this.rbLabelCurrentRecipe);
            this.ribbonPanelStatus.Text = "Status";
            // 
            // rbLabelCurrentRecipe
            // 
            this.rbLabelCurrentRecipe.Text = "Recipe : Default";
            // 
            // ribbonPanelSystem
            // 
            this.ribbonPanelSystem.Items.Add(this.rbExit);
            this.ribbonPanelSystem.Text = "System";
            // 
            // rbExit
            // 
            this.rbExit.Image = global::KPVisionInspectionFramework.Properties.Resources.Exit;
            this.rbExit.MinimumSize = new System.Drawing.Size(70, 60);
            this.rbExit.SmallImage = ((System.Drawing.Image)(resources.GetObject("rbExit.SmallImage")));
            this.rbExit.Text = " Exit";
            this.rbExit.Click += new System.EventHandler(this.rbExit_Click);
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 140);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1351, 846);
            this.panelMain.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1351, 986);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.ribbonMain);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " KV INT Vision Inspection Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbonMain;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.RibbonTab ribbonTabInspectionMain;
        private System.Windows.Forms.RibbonPanel ribbonPanelOperating;
        private System.Windows.Forms.RibbonButton rbStart;
        private System.Windows.Forms.RibbonButton rbStop;
        private System.Windows.Forms.RibbonPanel ribbonPanelSetting;
        private System.Windows.Forms.RibbonPanel ribbonPanelData;
        private System.Windows.Forms.RibbonPanel ribbonPanelStatus;
        private System.Windows.Forms.RibbonPanel ribbonPanelSystem;
        private System.Windows.Forms.RibbonButton rbExit;
        private System.Windows.Forms.RibbonButton rbEthernet;
        private System.Windows.Forms.RibbonButton rbLight;
        private System.Windows.Forms.RibbonButton rbDIO;
        private System.Windows.Forms.RibbonButton rbRecipe;
        private System.Windows.Forms.RibbonButton rbLog;
        private System.Windows.Forms.RibbonButton rbHistory;
        private System.Windows.Forms.RibbonButton rbFolder;
        private System.Windows.Forms.RibbonLabel rbLabelCurrentRecipe;
        private System.Windows.Forms.RibbonButton rbConfig;
        private System.Windows.Forms.RibbonButton rbAlign;
        private System.Windows.Forms.RibbonButton rbSerial;
        private System.Windows.Forms.RibbonButton rbMapData;
        private System.Windows.Forms.RibbonButton rbSave;
        private System.Windows.Forms.RibbonButton rbCommunication;
        private System.Windows.Forms.RibbonButton rbCalibration;
    }
}

