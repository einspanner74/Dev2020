namespace InspectionSystemManager
{
    partial class InspectionWindow
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.kpCogDisplayMain = new KPDisplay.KPCogDisplayControl();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelZoomPlus = new System.Windows.Forms.Label();
            this.labelZoomMinus = new System.Windows.Forms.Label();
            this.panelMenuHide = new System.Windows.Forms.Panel();
            this.btnQuickInspection = new CustomControl.ImageButton();
            this.btnImageResultDisplay = new CustomControl.ImageButton();
            this.btnCrossBar = new CustomControl.ImageButton();
            this.btnImageAutoSave = new CustomControl.ImageButton();
            this.btnAutoDelete = new CustomControl.ImageButton();
            this.btnConfigSave = new CustomControl.ImageButton();
            this.btnImageSave = new CustomControl.ImageButton();
            this.btnImageLoad = new CustomControl.ImageButton();
            this.btnRecipeSave = new CustomControl.ImageButton();
            this.btnRecipe = new CustomControl.ImageButton();
            this.btnOneShot = new CustomControl.ImageButton();
            this.btnInspection = new CustomControl.ImageButton();
            this.btnLive = new CustomControl.ImageButton();
            this.panelMain.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.kpCogDisplayMain);
            this.panelMain.Controls.Add(this.panelMenu);
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(624, 590);
            this.panelMain.TabIndex = 2;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
            // 
            // kpCogDisplayMain
            // 
            this.kpCogDisplayMain.BackColor = System.Drawing.SystemColors.Control;
            this.kpCogDisplayMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpCogDisplayMain.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpCogDisplayMain.Location = new System.Drawing.Point(3, 47);
            this.kpCogDisplayMain.Name = "kpCogDisplayMain";
            this.kpCogDisplayMain.Size = new System.Drawing.Size(621, 540);
            this.kpCogDisplayMain.TabIndex = 1;
            this.kpCogDisplayMain.UseStatusBar = true;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.btnImageResultDisplay);
            this.panelMenu.Controls.Add(this.btnCrossBar);
            this.panelMenu.Controls.Add(this.btnImageAutoSave);
            this.panelMenu.Controls.Add(this.btnAutoDelete);
            this.panelMenu.Controls.Add(this.btnConfigSave);
            this.panelMenu.Controls.Add(this.btnImageSave);
            this.panelMenu.Controls.Add(this.btnImageLoad);
            this.panelMenu.Controls.Add(this.btnRecipeSave);
            this.panelMenu.Controls.Add(this.btnRecipe);
            this.panelMenu.Controls.Add(this.btnOneShot);
            this.panelMenu.Controls.Add(this.btnInspection);
            this.panelMenu.Controls.Add(this.btnLive);
            this.panelMenu.Location = new System.Drawing.Point(4, 4);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(616, 38);
            this.panelMenu.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(624, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = " Vision# Inspection window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDoubleClick);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.labelStatus.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.White;
            this.labelStatus.Location = new System.Drawing.Point(188, 9);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(43, 15);
            this.labelStatus.TabIndex = 4;
            this.labelStatus.Text = "(Live)";
            // 
            // labelZoomPlus
            // 
            this.labelZoomPlus.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelZoomPlus.ForeColor = System.Drawing.Color.White;
            this.labelZoomPlus.Location = new System.Drawing.Point(233, 8);
            this.labelZoomPlus.Name = "labelZoomPlus";
            this.labelZoomPlus.Size = new System.Drawing.Size(15, 15);
            this.labelZoomPlus.TabIndex = 7;
            this.labelZoomPlus.Text = "+";
            this.labelZoomPlus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelZoomPlus.Click += new System.EventHandler(this.labelZoomPlus_Click);
            // 
            // labelZoomMinus
            // 
            this.labelZoomMinus.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelZoomMinus.ForeColor = System.Drawing.Color.White;
            this.labelZoomMinus.Location = new System.Drawing.Point(252, 8);
            this.labelZoomMinus.Name = "labelZoomMinus";
            this.labelZoomMinus.Size = new System.Drawing.Size(15, 15);
            this.labelZoomMinus.TabIndex = 8;
            this.labelZoomMinus.Text = "-";
            this.labelZoomMinus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelZoomMinus.Click += new System.EventHandler(this.labelZoomMinus_Click);
            // 
            // panelMenuHide
            // 
            this.panelMenuHide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.panelMenuHide.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Arrow_Up;
            this.panelMenuHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMenuHide.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenuHide.Location = new System.Drawing.Point(164, 8);
            this.panelMenuHide.Name = "panelMenuHide";
            this.panelMenuHide.Size = new System.Drawing.Size(12, 14);
            this.panelMenuHide.TabIndex = 3;
            this.panelMenuHide.Click += new System.EventHandler(this.panelMenuHide_Click);
            // 
            // btnQuickInspection
            // 
            this.btnQuickInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.btnQuickInspection.BackgroundImage = global::InspectionSystemManager.Properties.Resources.QuickInsp;
            this.btnQuickInspection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnQuickInspection.ButtonImage = global::InspectionSystemManager.Properties.Resources.QuickInsp;
            this.btnQuickInspection.ButtonImageDiable = null;
            this.btnQuickInspection.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.QuickInspDown;
            this.btnQuickInspection.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.QuickInspOver;
            this.btnQuickInspection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnQuickInspection.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnQuickInspection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnQuickInspection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnQuickInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickInspection.ForeColor = System.Drawing.Color.White;
            this.btnQuickInspection.Location = new System.Drawing.Point(274, 5);
            this.btnQuickInspection.Name = "btnQuickInspection";
            this.btnQuickInspection.Size = new System.Drawing.Size(24, 24);
            this.btnQuickInspection.TabIndex = 18;
            this.btnQuickInspection.UseVisualStyleBackColor = false;
            this.btnQuickInspection.Visible = false;
            this.btnQuickInspection.Click += new System.EventHandler(this.btnQuickInspection_Click);
            // 
            // btnImageResultDisplay
            // 
            this.btnImageResultDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageResultDisplay.BackgroundImage = global::InspectionSystemManager.Properties.Resources.ResultStop;
            this.btnImageResultDisplay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImageResultDisplay.ButtonImage = global::InspectionSystemManager.Properties.Resources.ResultStop;
            this.btnImageResultDisplay.ButtonImageDiable = null;
            this.btnImageResultDisplay.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.ResultStopDown;
            this.btnImageResultDisplay.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.ResultStopOver;
            this.btnImageResultDisplay.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageResultDisplay.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageResultDisplay.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageResultDisplay.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageResultDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImageResultDisplay.ForeColor = System.Drawing.Color.White;
            this.btnImageResultDisplay.Location = new System.Drawing.Point(420, 1);
            this.btnImageResultDisplay.Name = "btnImageResultDisplay";
            this.btnImageResultDisplay.Size = new System.Drawing.Size(34, 34);
            this.btnImageResultDisplay.TabIndex = 17;
            this.btnImageResultDisplay.UseVisualStyleBackColor = false;
            this.btnImageResultDisplay.Click += new System.EventHandler(this.btnImageResultDisplay_Click);
            // 
            // btnCrossBar
            // 
            this.btnCrossBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnCrossBar.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Cross;
            this.btnCrossBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCrossBar.ButtonImage = global::InspectionSystemManager.Properties.Resources.Cross;
            this.btnCrossBar.ButtonImageDiable = null;
            this.btnCrossBar.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.CrossDown;
            this.btnCrossBar.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.CrossOver;
            this.btnCrossBar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnCrossBar.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnCrossBar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnCrossBar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnCrossBar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrossBar.ForeColor = System.Drawing.Color.White;
            this.btnCrossBar.Location = new System.Drawing.Point(373, 0);
            this.btnCrossBar.Name = "btnCrossBar";
            this.btnCrossBar.Size = new System.Drawing.Size(34, 34);
            this.btnCrossBar.TabIndex = 16;
            this.btnCrossBar.UseVisualStyleBackColor = false;
            this.btnCrossBar.Click += new System.EventHandler(this.btnCrossBar_Click);
            // 
            // btnImageAutoSave
            // 
            this.btnImageAutoSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageAutoSave.BackgroundImage = global::InspectionSystemManager.Properties.Resources.AutoStop;
            this.btnImageAutoSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImageAutoSave.ButtonImage = global::InspectionSystemManager.Properties.Resources.AutoStop;
            this.btnImageAutoSave.ButtonImageDiable = null;
            this.btnImageAutoSave.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.AutoStopDown;
            this.btnImageAutoSave.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.AutoStopOver;
            this.btnImageAutoSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageAutoSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageAutoSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageAutoSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageAutoSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImageAutoSave.ForeColor = System.Drawing.Color.White;
            this.btnImageAutoSave.Location = new System.Drawing.Point(261, 0);
            this.btnImageAutoSave.Name = "btnImageAutoSave";
            this.btnImageAutoSave.Size = new System.Drawing.Size(34, 34);
            this.btnImageAutoSave.TabIndex = 15;
            this.btnImageAutoSave.UseVisualStyleBackColor = false;
            this.btnImageAutoSave.Click += new System.EventHandler(this.btnImageAutoSave_Click);
            // 
            // btnAutoDelete
            // 
            this.btnAutoDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnAutoDelete.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Delete;
            this.btnAutoDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutoDelete.ButtonImage = global::InspectionSystemManager.Properties.Resources.Delete;
            this.btnAutoDelete.ButtonImageDiable = null;
            this.btnAutoDelete.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.DeleteDown;
            this.btnAutoDelete.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.DeleteOver;
            this.btnAutoDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnAutoDelete.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnAutoDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnAutoDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnAutoDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoDelete.ForeColor = System.Drawing.Color.White;
            this.btnAutoDelete.Location = new System.Drawing.Point(339, 0);
            this.btnAutoDelete.Name = "btnAutoDelete";
            this.btnAutoDelete.Size = new System.Drawing.Size(34, 34);
            this.btnAutoDelete.TabIndex = 14;
            this.btnAutoDelete.UseVisualStyleBackColor = false;
            this.btnAutoDelete.Click += new System.EventHandler(this.btnAutoDelete_Click);
            // 
            // btnConfigSave
            // 
            this.btnConfigSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnConfigSave.BackgroundImage = global::InspectionSystemManager.Properties.Resources.ConfigSave;
            this.btnConfigSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfigSave.ButtonImage = global::InspectionSystemManager.Properties.Resources.ConfigSave;
            this.btnConfigSave.ButtonImageDiable = null;
            this.btnConfigSave.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.ConfigSaveDown;
            this.btnConfigSave.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.ConfigSaveOver;
            this.btnConfigSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnConfigSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnConfigSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnConfigSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnConfigSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigSave.ForeColor = System.Drawing.Color.White;
            this.btnConfigSave.Location = new System.Drawing.Point(307, 0);
            this.btnConfigSave.Name = "btnConfigSave";
            this.btnConfigSave.Size = new System.Drawing.Size(34, 34);
            this.btnConfigSave.TabIndex = 14;
            this.btnConfigSave.UseVisualStyleBackColor = false;
            this.btnConfigSave.Click += new System.EventHandler(this.btnConfigSave_Click);
            // 
            // btnImageSave
            // 
            this.btnImageSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageSave.BackgroundImage = global::InspectionSystemManager.Properties.Resources.SaveImage;
            this.btnImageSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImageSave.ButtonImage = global::InspectionSystemManager.Properties.Resources.SaveImage;
            this.btnImageSave.ButtonImageDiable = null;
            this.btnImageSave.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.SaveImageDown;
            this.btnImageSave.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.SaveImageOver;
            this.btnImageSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImageSave.ForeColor = System.Drawing.Color.White;
            this.btnImageSave.Location = new System.Drawing.Point(227, 0);
            this.btnImageSave.Name = "btnImageSave";
            this.btnImageSave.Size = new System.Drawing.Size(34, 34);
            this.btnImageSave.TabIndex = 13;
            this.btnImageSave.UseVisualStyleBackColor = false;
            this.btnImageSave.Click += new System.EventHandler(this.btnImageSave_Click);
            // 
            // btnImageLoad
            // 
            this.btnImageLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageLoad.BackgroundImage = global::InspectionSystemManager.Properties.Resources.LoadImage;
            this.btnImageLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImageLoad.ButtonImage = global::InspectionSystemManager.Properties.Resources.LoadImage;
            this.btnImageLoad.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.LoadImageDisable;
            this.btnImageLoad.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.LoadImageDown;
            this.btnImageLoad.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.LoadImageOver;
            this.btnImageLoad.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageLoad.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageLoad.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageLoad.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnImageLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImageLoad.ForeColor = System.Drawing.Color.White;
            this.btnImageLoad.Location = new System.Drawing.Point(192, 0);
            this.btnImageLoad.Name = "btnImageLoad";
            this.btnImageLoad.Size = new System.Drawing.Size(34, 34);
            this.btnImageLoad.TabIndex = 12;
            this.btnImageLoad.UseVisualStyleBackColor = false;
            this.btnImageLoad.Click += new System.EventHandler(this.btnImageLoad_Click);
            // 
            // btnRecipeSave
            // 
            this.btnRecipeSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipeSave.BackgroundImage = global::InspectionSystemManager.Properties.Resources.TrainFileSave;
            this.btnRecipeSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRecipeSave.ButtonImage = global::InspectionSystemManager.Properties.Resources.TrainFileSave;
            this.btnRecipeSave.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.TrainFileSaveDisable;
            this.btnRecipeSave.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.TrainFileSaveDown;
            this.btnRecipeSave.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.TrainFileSaveOver;
            this.btnRecipeSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipeSave.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipeSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipeSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipeSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipeSave.ForeColor = System.Drawing.Color.White;
            this.btnRecipeSave.Location = new System.Drawing.Point(107, 0);
            this.btnRecipeSave.Name = "btnRecipeSave";
            this.btnRecipeSave.Size = new System.Drawing.Size(34, 34);
            this.btnRecipeSave.TabIndex = 11;
            this.btnRecipeSave.UseVisualStyleBackColor = false;
            this.btnRecipeSave.Click += new System.EventHandler(this.btnRecipeSave_Click);
            // 
            // btnRecipe
            // 
            this.btnRecipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipe.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Train;
            this.btnRecipe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRecipe.ButtonImage = global::InspectionSystemManager.Properties.Resources.Train;
            this.btnRecipe.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.TrainDisable;
            this.btnRecipe.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.TrainDown;
            this.btnRecipe.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.TrainOver;
            this.btnRecipe.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipe.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipe.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipe.ForeColor = System.Drawing.Color.White;
            this.btnRecipe.Location = new System.Drawing.Point(72, 0);
            this.btnRecipe.Name = "btnRecipe";
            this.btnRecipe.Size = new System.Drawing.Size(34, 34);
            this.btnRecipe.TabIndex = 10;
            this.btnRecipe.UseVisualStyleBackColor = false;
            this.btnRecipe.Click += new System.EventHandler(this.btnRecipe_Click);
            // 
            // btnOneShot
            // 
            this.btnOneShot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnOneShot.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Camera;
            this.btnOneShot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOneShot.ButtonImage = global::InspectionSystemManager.Properties.Resources.Camera;
            this.btnOneShot.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.CameraDisable;
            this.btnOneShot.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.CameraDown;
            this.btnOneShot.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.CameraOver;
            this.btnOneShot.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnOneShot.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnOneShot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnOneShot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnOneShot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOneShot.ForeColor = System.Drawing.Color.White;
            this.btnOneShot.Location = new System.Drawing.Point(37, 0);
            this.btnOneShot.Name = "btnOneShot";
            this.btnOneShot.Size = new System.Drawing.Size(34, 34);
            this.btnOneShot.TabIndex = 9;
            this.btnOneShot.UseVisualStyleBackColor = false;
            this.btnOneShot.Click += new System.EventHandler(this.btnOneShot_Click);
            // 
            // btnInspection
            // 
            this.btnInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnInspection.BackgroundImage = global::InspectionSystemManager.Properties.Resources.InspStart;
            this.btnInspection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInspection.ButtonImage = global::InspectionSystemManager.Properties.Resources.InspStart;
            this.btnInspection.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.InspStartDisable;
            this.btnInspection.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.InspStartDown;
            this.btnInspection.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.InspStartOver;
            this.btnInspection.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnInspection.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnInspection.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnInspection.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInspection.ForeColor = System.Drawing.Color.White;
            this.btnInspection.Location = new System.Drawing.Point(2, 0);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(34, 34);
            this.btnInspection.TabIndex = 8;
            this.btnInspection.UseVisualStyleBackColor = false;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // btnLive
            // 
            this.btnLive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnLive.BackgroundImage = global::InspectionSystemManager.Properties.Resources.Live;
            this.btnLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLive.ButtonImage = global::InspectionSystemManager.Properties.Resources.Live;
            this.btnLive.ButtonImageDiable = global::InspectionSystemManager.Properties.Resources.LiveDisable;
            this.btnLive.ButtonImageDown = global::InspectionSystemManager.Properties.Resources.LiveDown;
            this.btnLive.ButtonImageOver = global::InspectionSystemManager.Properties.Resources.LiveOver;
            this.btnLive.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnLive.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnLive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnLive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLive.ForeColor = System.Drawing.Color.White;
            this.btnLive.Location = new System.Drawing.Point(159, 0);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(34, 34);
            this.btnLive.TabIndex = 7;
            this.btnLive.UseVisualStyleBackColor = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // InspectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(624, 623);
            this.Controls.Add(this.btnQuickInspection);
            this.Controls.Add(this.labelZoomMinus);
            this.Controls.Add(this.labelZoomPlus);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.panelMenuHide);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InspectionWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InspectionWindow_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InspectionWindow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InspectionWindow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InspectionWindow_MouseUp);
            this.Resize += new System.EventHandler(this.InspectionWindow_Resize);
            this.panelMain.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMenu;
        private KPDisplay.KPCogDisplayControl kpCogDisplayMain;
        private CustomControl.ImageButton btnInspection;
        private CustomControl.ImageButton btnLive;
        private CustomControl.ImageButton btnRecipe;
        private CustomControl.ImageButton btnOneShot;
        private CustomControl.ImageButton btnRecipeSave;
        private System.Windows.Forms.Label labelTitle;
        private CustomControl.ImageButton btnImageSave;
        private CustomControl.ImageButton btnImageLoad;
        private CustomControl.ImageButton btnConfigSave;
        private CustomControl.ImageButton btnImageAutoSave;
        private CustomControl.ImageButton btnCrossBar;
		private CustomControl.ImageButton btnAutoDelete;
        private System.Windows.Forms.Panel panelMenuHide;
        private System.Windows.Forms.Label labelStatus;
        private CustomControl.ImageButton btnImageResultDisplay;
        private System.Windows.Forms.Label labelZoomPlus;
        private System.Windows.Forms.Label labelZoomMinus;
        private CustomControl.ImageButton btnQuickInspection;
    }
}