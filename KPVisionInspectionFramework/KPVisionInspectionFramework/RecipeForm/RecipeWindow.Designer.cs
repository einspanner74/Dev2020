namespace KPVisionInspectionFramework
{
    partial class RecipeWindow
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCurrentRecipe = new System.Windows.Forms.Panel();
            this.labelRecipeNum5 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe5 = new System.Windows.Forms.TextBox();
            this.labelRecipeNum4 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe4 = new System.Windows.Forms.TextBox();
            this.labelRecipeNum3 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe3 = new System.Windows.Forms.TextBox();
            this.labelRecipeNum2 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe2 = new System.Windows.Forms.TextBox();
            this.labelRecipeNum1 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe1 = new System.Windows.Forms.TextBox();
            this.labelRecipeNum0 = new CustomControl.GradientLabel();
            this.textBoxCurrentRecipe0 = new System.Windows.Forms.TextBox();
            this.panelManagement = new System.Windows.Forms.Panel();
            this.btnRecipeAdd = new System.Windows.Forms.Button();
            this.btnRecipeDelete = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnRecipeCopy = new System.Windows.Forms.Button();
            this.btnRecipeChange = new System.Windows.Forms.Button();
            this.gradientLabel2 = new CustomControl.GradientLabel();
            this.gradientLabel3 = new CustomControl.GradientLabel();
            this.textBoxSearchRecipe = new System.Windows.Forms.TextBox();
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.listBoxRecipe = new System.Windows.Forms.ListBox();
            this.panelMain.SuspendLayout();
            this.panelCurrentRecipe.SuspendLayout();
            this.panelManagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.SteelBlue;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(640, 30);
            this.labelTitle.TabIndex = 12;
            this.labelTitle.Text = " Recipe Management";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelCurrentRecipe);
            this.panelMain.Controls.Add(this.panelManagement);
            this.panelMain.Controls.Add(this.gradientLabel3);
            this.panelMain.Controls.Add(this.textBoxSearchRecipe);
            this.panelMain.Controls.Add(this.gradientLabel1);
            this.panelMain.Controls.Add(this.listBoxRecipe);
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(640, 797);
            this.panelMain.TabIndex = 13;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // panelCurrentRecipe
            // 
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum5);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe5);
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum4);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe4);
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum3);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe3);
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum2);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe2);
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum1);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe1);
            this.panelCurrentRecipe.Controls.Add(this.labelRecipeNum0);
            this.panelCurrentRecipe.Controls.Add(this.textBoxCurrentRecipe0);
            this.panelCurrentRecipe.Location = new System.Drawing.Point(1, 639);
            this.panelCurrentRecipe.Name = "panelCurrentRecipe";
            this.panelCurrentRecipe.Size = new System.Drawing.Size(637, 67);
            this.panelCurrentRecipe.TabIndex = 312;
            // 
            // labelRecipeNum5
            // 
            this.labelRecipeNum5.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum5.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum5.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum5.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum5.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum5.Location = new System.Drawing.Point(431, 36);
            this.labelRecipeNum5.Name = "labelRecipeNum5";
            this.labelRecipeNum5.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum5.TabIndex = 323;
            this.labelRecipeNum5.Tag = "5";
            this.labelRecipeNum5.Text = "6";
            this.labelRecipeNum5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum5.Visible = false;
            this.labelRecipeNum5.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe5
            // 
            this.textBoxCurrentRecipe5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe5.Location = new System.Drawing.Point(469, 36);
            this.textBoxCurrentRecipe5.Multiline = true;
            this.textBoxCurrentRecipe5.Name = "textBoxCurrentRecipe5";
            this.textBoxCurrentRecipe5.ReadOnly = true;
            this.textBoxCurrentRecipe5.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe5.TabIndex = 322;
            this.textBoxCurrentRecipe5.Tag = "5";
            this.textBoxCurrentRecipe5.Text = "Recipe Name";
            this.textBoxCurrentRecipe5.Visible = false;
            // 
            // labelRecipeNum4
            // 
            this.labelRecipeNum4.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum4.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum4.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum4.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum4.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum4.Location = new System.Drawing.Point(217, 36);
            this.labelRecipeNum4.Name = "labelRecipeNum4";
            this.labelRecipeNum4.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum4.TabIndex = 321;
            this.labelRecipeNum4.Tag = "4";
            this.labelRecipeNum4.Text = "5";
            this.labelRecipeNum4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum4.Visible = false;
            this.labelRecipeNum4.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe4
            // 
            this.textBoxCurrentRecipe4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe4.Location = new System.Drawing.Point(255, 36);
            this.textBoxCurrentRecipe4.Multiline = true;
            this.textBoxCurrentRecipe4.Name = "textBoxCurrentRecipe4";
            this.textBoxCurrentRecipe4.ReadOnly = true;
            this.textBoxCurrentRecipe4.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe4.TabIndex = 320;
            this.textBoxCurrentRecipe4.Tag = "4";
            this.textBoxCurrentRecipe4.Text = "Recipe Name";
            this.textBoxCurrentRecipe4.Visible = false;
            // 
            // labelRecipeNum3
            // 
            this.labelRecipeNum3.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum3.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum3.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum3.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum3.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum3.Location = new System.Drawing.Point(2, 36);
            this.labelRecipeNum3.Name = "labelRecipeNum3";
            this.labelRecipeNum3.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum3.TabIndex = 319;
            this.labelRecipeNum3.Tag = "3";
            this.labelRecipeNum3.Text = "4";
            this.labelRecipeNum3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum3.Visible = false;
            this.labelRecipeNum3.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe3
            // 
            this.textBoxCurrentRecipe3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe3.Location = new System.Drawing.Point(40, 36);
            this.textBoxCurrentRecipe3.Multiline = true;
            this.textBoxCurrentRecipe3.Name = "textBoxCurrentRecipe3";
            this.textBoxCurrentRecipe3.ReadOnly = true;
            this.textBoxCurrentRecipe3.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe3.TabIndex = 318;
            this.textBoxCurrentRecipe3.Tag = "3";
            this.textBoxCurrentRecipe3.Text = "Recipe Name";
            this.textBoxCurrentRecipe3.Visible = false;
            // 
            // labelRecipeNum2
            // 
            this.labelRecipeNum2.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum2.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum2.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum2.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum2.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum2.Location = new System.Drawing.Point(431, 2);
            this.labelRecipeNum2.Name = "labelRecipeNum2";
            this.labelRecipeNum2.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum2.TabIndex = 317;
            this.labelRecipeNum2.Tag = "2";
            this.labelRecipeNum2.Text = "3";
            this.labelRecipeNum2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum2.Visible = false;
            this.labelRecipeNum2.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe2
            // 
            this.textBoxCurrentRecipe2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe2.Location = new System.Drawing.Point(469, 2);
            this.textBoxCurrentRecipe2.Multiline = true;
            this.textBoxCurrentRecipe2.Name = "textBoxCurrentRecipe2";
            this.textBoxCurrentRecipe2.ReadOnly = true;
            this.textBoxCurrentRecipe2.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe2.TabIndex = 316;
            this.textBoxCurrentRecipe2.Tag = "2";
            this.textBoxCurrentRecipe2.Text = "Recipe Name";
            this.textBoxCurrentRecipe2.Visible = false;
            // 
            // labelRecipeNum1
            // 
            this.labelRecipeNum1.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum1.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum1.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum1.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum1.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum1.Location = new System.Drawing.Point(217, 2);
            this.labelRecipeNum1.Name = "labelRecipeNum1";
            this.labelRecipeNum1.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum1.TabIndex = 315;
            this.labelRecipeNum1.Tag = "1";
            this.labelRecipeNum1.Text = "2";
            this.labelRecipeNum1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum1.Visible = false;
            this.labelRecipeNum1.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe1
            // 
            this.textBoxCurrentRecipe1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe1.Location = new System.Drawing.Point(255, 2);
            this.textBoxCurrentRecipe1.Multiline = true;
            this.textBoxCurrentRecipe1.Name = "textBoxCurrentRecipe1";
            this.textBoxCurrentRecipe1.ReadOnly = true;
            this.textBoxCurrentRecipe1.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe1.TabIndex = 314;
            this.textBoxCurrentRecipe1.Tag = "1";
            this.textBoxCurrentRecipe1.Text = "Recipe Name";
            this.textBoxCurrentRecipe1.Visible = false;
            // 
            // labelRecipeNum0
            // 
            this.labelRecipeNum0.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelRecipeNum0.ColorBottom = System.Drawing.Color.Gray;
            this.labelRecipeNum0.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelRecipeNum0.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelRecipeNum0.ForeColor = System.Drawing.Color.White;
            this.labelRecipeNum0.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelRecipeNum0.Location = new System.Drawing.Point(2, 2);
            this.labelRecipeNum0.Name = "labelRecipeNum0";
            this.labelRecipeNum0.Size = new System.Drawing.Size(36, 28);
            this.labelRecipeNum0.TabIndex = 313;
            this.labelRecipeNum0.Tag = "0";
            this.labelRecipeNum0.Text = "1";
            this.labelRecipeNum0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRecipeNum0.Click += new System.EventHandler(this.labelRecipeNum_Click);
            // 
            // textBoxCurrentRecipe0
            // 
            this.textBoxCurrentRecipe0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxCurrentRecipe0.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCurrentRecipe0.Location = new System.Drawing.Point(40, 2);
            this.textBoxCurrentRecipe0.Multiline = true;
            this.textBoxCurrentRecipe0.Name = "textBoxCurrentRecipe0";
            this.textBoxCurrentRecipe0.ReadOnly = true;
            this.textBoxCurrentRecipe0.Size = new System.Drawing.Size(168, 29);
            this.textBoxCurrentRecipe0.TabIndex = 302;
            this.textBoxCurrentRecipe0.Tag = "0";
            this.textBoxCurrentRecipe0.Text = "Recipe Name";
            // 
            // panelManagement
            // 
            this.panelManagement.Controls.Add(this.btnRecipeAdd);
            this.panelManagement.Controls.Add(this.btnRecipeDelete);
            this.panelManagement.Controls.Add(this.btnOk);
            this.panelManagement.Controls.Add(this.btnRecipeCopy);
            this.panelManagement.Controls.Add(this.btnRecipeChange);
            this.panelManagement.Controls.Add(this.gradientLabel2);
            this.panelManagement.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelManagement.Location = new System.Drawing.Point(0, 706);
            this.panelManagement.Name = "panelManagement";
            this.panelManagement.Size = new System.Drawing.Size(640, 91);
            this.panelManagement.TabIndex = 311;
            // 
            // btnRecipeAdd
            // 
            this.btnRecipeAdd.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipeAdd.Location = new System.Drawing.Point(132, 42);
            this.btnRecipeAdd.Name = "btnRecipeAdd";
            this.btnRecipeAdd.Size = new System.Drawing.Size(120, 49);
            this.btnRecipeAdd.TabIndex = 305;
            this.btnRecipeAdd.Text = "New";
            this.btnRecipeAdd.UseVisualStyleBackColor = true;
            this.btnRecipeAdd.Click += new System.EventHandler(this.btnRecipeAdd_Click);
            // 
            // btnRecipeDelete
            // 
            this.btnRecipeDelete.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipeDelete.Location = new System.Drawing.Point(388, 42);
            this.btnRecipeDelete.Name = "btnRecipeDelete";
            this.btnRecipeDelete.Size = new System.Drawing.Size(120, 49);
            this.btnRecipeDelete.TabIndex = 306;
            this.btnRecipeDelete.Text = "Delete";
            this.btnRecipeDelete.UseVisualStyleBackColor = true;
            this.btnRecipeDelete.Click += new System.EventHandler(this.btnRecipeDelete_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(516, 42);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(120, 49);
            this.btnOk.TabIndex = 304;
            this.btnOk.Text = "Confirm";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnRecipeCopy
            // 
            this.btnRecipeCopy.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipeCopy.Location = new System.Drawing.Point(260, 42);
            this.btnRecipeCopy.Name = "btnRecipeCopy";
            this.btnRecipeCopy.Size = new System.Drawing.Size(120, 49);
            this.btnRecipeCopy.TabIndex = 307;
            this.btnRecipeCopy.Text = "Copy";
            this.btnRecipeCopy.UseVisualStyleBackColor = true;
            this.btnRecipeCopy.Click += new System.EventHandler(this.btnRecipeCopy_Click);
            // 
            // btnRecipeChange
            // 
            this.btnRecipeChange.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeChange.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecipeChange.Location = new System.Drawing.Point(4, 42);
            this.btnRecipeChange.Name = "btnRecipeChange";
            this.btnRecipeChange.Size = new System.Drawing.Size(120, 49);
            this.btnRecipeChange.TabIndex = 308;
            this.btnRecipeChange.Text = "Change";
            this.btnRecipeChange.UseVisualStyleBackColor = true;
            this.btnRecipeChange.Click += new System.EventHandler(this.btnRecipeChange_Click);
            // 
            // gradientLabel2
            // 
            this.gradientLabel2.BackColor = System.Drawing.Color.LightSlateGray;
            this.gradientLabel2.ColorBottom = System.Drawing.Color.Gray;
            this.gradientLabel2.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gradientLabel2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel2.ForeColor = System.Drawing.Color.White;
            this.gradientLabel2.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel2.Location = new System.Drawing.Point(3, 3);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(634, 35);
            this.gradientLabel2.TabIndex = 303;
            this.gradientLabel2.Text = "Recipe Management";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabel3
            // 
            this.gradientLabel3.BackColor = System.Drawing.Color.LightSlateGray;
            this.gradientLabel3.ColorBottom = System.Drawing.Color.Gray;
            this.gradientLabel3.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gradientLabel3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel3.ForeColor = System.Drawing.Color.White;
            this.gradientLabel3.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel3.Location = new System.Drawing.Point(3, 547);
            this.gradientLabel3.Name = "gradientLabel3";
            this.gradientLabel3.Size = new System.Drawing.Size(94, 28);
            this.gradientLabel3.TabIndex = 310;
            this.gradientLabel3.Text = "Search";
            this.gradientLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSearchRecipe
            // 
            this.textBoxSearchRecipe.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold);
            this.textBoxSearchRecipe.Location = new System.Drawing.Point(100, 549);
            this.textBoxSearchRecipe.Name = "textBoxSearchRecipe";
            this.textBoxSearchRecipe.Size = new System.Drawing.Size(538, 26);
            this.textBoxSearchRecipe.TabIndex = 1;
            this.textBoxSearchRecipe.TextChanged += new System.EventHandler(this.textBoxSearchRecipe_TextChanged);
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.LightSlateGray;
            this.gradientLabel1.ColorBottom = System.Drawing.Color.Gray;
            this.gradientLabel1.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gradientLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel1.Location = new System.Drawing.Point(3, 601);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(634, 35);
            this.gradientLabel1.TabIndex = 301;
            this.gradientLabel1.Text = "Current Recipe Name";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxRecipe
            // 
            this.listBoxRecipe.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Bold);
            this.listBoxRecipe.FormattingEnabled = true;
            this.listBoxRecipe.ItemHeight = 15;
            this.listBoxRecipe.Items.AddRange(new object[] {
            "Recipe"});
            this.listBoxRecipe.Location = new System.Drawing.Point(1, 1);
            this.listBoxRecipe.Name = "listBoxRecipe";
            this.listBoxRecipe.Size = new System.Drawing.Size(638, 544);
            this.listBoxRecipe.TabIndex = 4;
            this.listBoxRecipe.DoubleClick += new System.EventHandler(this.listBoxRecipe_DoubleClick);
            // 
            // RecipeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.ClientSize = new System.Drawing.Size(640, 834);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "RecipeWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RecipeWindow";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RecipeWindow_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelCurrentRecipe.ResumeLayout(false);
            this.panelCurrentRecipe.PerformLayout();
            this.panelManagement.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnRecipeAdd;
        private System.Windows.Forms.Button btnRecipeDelete;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnRecipeCopy;
        private System.Windows.Forms.Button btnRecipeChange;
        private CustomControl.GradientLabel gradientLabel2;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe0;
        private CustomControl.GradientLabel gradientLabel3;
        private System.Windows.Forms.TextBox textBoxSearchRecipe;
        private System.Windows.Forms.Panel panelCurrentRecipe;
        private CustomControl.GradientLabel labelRecipeNum5;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe5;
        private CustomControl.GradientLabel labelRecipeNum4;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe4;
        private CustomControl.GradientLabel labelRecipeNum3;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe3;
        private CustomControl.GradientLabel labelRecipeNum2;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe2;
        private CustomControl.GradientLabel labelRecipeNum1;
        private System.Windows.Forms.TextBox textBoxCurrentRecipe1;
        private CustomControl.GradientLabel labelRecipeNum0;
        private System.Windows.Forms.Panel panelManagement;
        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.ListBox listBoxRecipe;
    }
}