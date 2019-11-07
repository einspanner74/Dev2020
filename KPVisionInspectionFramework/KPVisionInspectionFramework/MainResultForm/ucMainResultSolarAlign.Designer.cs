namespace KPVisionInspectionFramework
{
    partial class ucMainResultSolarAlign
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnAlignSetting = new System.Windows.Forms.Button();
            this.gradientLabel7 = new CustomControl.GradientLabel();
            this.gradientLabelAlignResult = new CustomControl.GradientLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAlignW = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxAlignU = new System.Windows.Forms.TextBox();
            this.textBoxAlignV = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAlignT = new System.Windows.Forms.TextBox();
            this.gradientLabel5 = new CustomControl.GradientLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAlignX = new System.Windows.Forms.TextBox();
            this.textBoxAlignY = new System.Windows.Forms.TextBox();
            this.gradientLabel2 = new CustomControl.GradientLabel();
            this.gradientLabelFindResult = new CustomControl.GradientLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCam2X = new System.Windows.Forms.TextBox();
            this.textBoxCam2Y = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gradientLabel3 = new CustomControl.GradientLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCam1X = new System.Windows.Forms.TextBox();
            this.textBoxCam1Y = new System.Windows.Forms.TextBox();
            this.gradientLabelTitle = new CustomControl.GradientLabel();
            this.panelMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnAlignSetting);
            this.panelMain.Controls.Add(this.gradientLabel7);
            this.panelMain.Controls.Add(this.gradientLabelAlignResult);
            this.panelMain.Controls.Add(this.groupBox3);
            this.panelMain.Controls.Add(this.gradientLabel2);
            this.panelMain.Controls.Add(this.gradientLabelFindResult);
            this.panelMain.Controls.Add(this.groupBox2);
            this.panelMain.Controls.Add(this.groupBox1);
            this.panelMain.Controls.Add(this.gradientLabelTitle);
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(377, 405);
            this.panelMain.TabIndex = 0;
            // 
            // btnAlignSetting
            // 
            this.btnAlignSetting.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlignSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlignSetting.Location = new System.Drawing.Point(271, 255);
            this.btnAlignSetting.Name = "btnAlignSetting";
            this.btnAlignSetting.Size = new System.Drawing.Size(102, 32);
            this.btnAlignSetting.TabIndex = 71;
            this.btnAlignSetting.Text = "Align Setting";
            this.btnAlignSetting.UseVisualStyleBackColor = true;
            this.btnAlignSetting.Click += new System.EventHandler(this.btnAlignSetting_Click);
            // 
            // gradientLabel7
            // 
            this.gradientLabel7.BackColor = System.Drawing.Color.White;
            this.gradientLabel7.ColorBottom = System.Drawing.Color.DarkGray;
            this.gradientLabel7.ColorTop = System.Drawing.Color.DimGray;
            this.gradientLabel7.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel7.ForeColor = System.Drawing.Color.White;
            this.gradientLabel7.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel7.Location = new System.Drawing.Point(273, 148);
            this.gradientLabel7.Name = "gradientLabel7";
            this.gradientLabel7.Size = new System.Drawing.Size(99, 29);
            this.gradientLabel7.TabIndex = 70;
            this.gradientLabel7.Text = "Align Result";
            this.gradientLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabelAlignResult
            // 
            this.gradientLabelAlignResult.BackColor = System.Drawing.Color.DarkGreen;
            this.gradientLabelAlignResult.ColorBottom = System.Drawing.Color.White;
            this.gradientLabelAlignResult.ColorTop = System.Drawing.Color.White;
            this.gradientLabelAlignResult.Font = new System.Drawing.Font("나눔바른고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientLabelAlignResult.ForeColor = System.Drawing.Color.Lime;
            this.gradientLabelAlignResult.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabelAlignResult.Location = new System.Drawing.Point(273, 182);
            this.gradientLabelAlignResult.Name = "gradientLabelAlignResult";
            this.gradientLabelAlignResult.Size = new System.Drawing.Size(99, 70);
            this.gradientLabelAlignResult.TabIndex = 69;
            this.gradientLabelAlignResult.Tag = "0";
            this.gradientLabelAlignResult.Text = "-";
            this.gradientLabelAlignResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxAlignW);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.textBoxAlignU);
            this.groupBox3.Controls.Add(this.textBoxAlignV);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBoxAlignT);
            this.groupBox3.Controls.Add(this.gradientLabel5);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.textBoxAlignX);
            this.groupBox3.Controls.Add(this.textBoxAlignY);
            this.groupBox3.Location = new System.Drawing.Point(7, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 139);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(131, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 19);
            this.label8.TabIndex = 73;
            this.label8.Text = "W :";
            // 
            // textBoxAlignW
            // 
            this.textBoxAlignW.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignW.Location = new System.Drawing.Point(166, 103);
            this.textBoxAlignW.Name = "textBoxAlignW";
            this.textBoxAlignW.ReadOnly = true;
            this.textBoxAlignW.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignW.TabIndex = 72;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(136, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 19);
            this.label9.TabIndex = 71;
            this.label9.Text = "V :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(136, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 19);
            this.label10.TabIndex = 69;
            this.label10.Text = "U :";
            // 
            // textBoxAlignU
            // 
            this.textBoxAlignU.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignU.Location = new System.Drawing.Point(166, 34);
            this.textBoxAlignU.Name = "textBoxAlignU";
            this.textBoxAlignU.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignU.TabIndex = 68;
            // 
            // textBoxAlignV
            // 
            this.textBoxAlignV.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignV.Location = new System.Drawing.Point(166, 68);
            this.textBoxAlignV.Name = "textBoxAlignV";
            this.textBoxAlignV.ReadOnly = true;
            this.textBoxAlignV.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignV.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(4, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 19);
            this.label7.TabIndex = 67;
            this.label7.Text = "T :";
            // 
            // textBoxAlignT
            // 
            this.textBoxAlignT.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignT.Location = new System.Drawing.Point(34, 103);
            this.textBoxAlignT.Name = "textBoxAlignT";
            this.textBoxAlignT.ReadOnly = true;
            this.textBoxAlignT.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignT.TabIndex = 66;
            // 
            // gradientLabel5
            // 
            this.gradientLabel5.BackColor = System.Drawing.Color.White;
            this.gradientLabel5.ColorBottom = System.Drawing.Color.DarkGray;
            this.gradientLabel5.ColorTop = System.Drawing.Color.DimGray;
            this.gradientLabel5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel5.ForeColor = System.Drawing.Color.White;
            this.gradientLabel5.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel5.Location = new System.Drawing.Point(0, 0);
            this.gradientLabel5.Name = "gradientLabel5";
            this.gradientLabel5.Size = new System.Drawing.Size(261, 29);
            this.gradientLabel5.TabIndex = 63;
            this.gradientLabel5.Text = "Align Position";
            this.gradientLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(4, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 19);
            this.label5.TabIndex = 65;
            this.label5.Text = "Y :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 19);
            this.label6.TabIndex = 62;
            this.label6.Text = "X :";
            // 
            // textBoxAlignX
            // 
            this.textBoxAlignX.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignX.Location = new System.Drawing.Point(34, 34);
            this.textBoxAlignX.Name = "textBoxAlignX";
            this.textBoxAlignX.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignX.TabIndex = 61;
            // 
            // textBoxAlignY
            // 
            this.textBoxAlignY.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxAlignY.Location = new System.Drawing.Point(34, 68);
            this.textBoxAlignY.Name = "textBoxAlignY";
            this.textBoxAlignY.ReadOnly = true;
            this.textBoxAlignY.Size = new System.Drawing.Size(87, 29);
            this.textBoxAlignY.TabIndex = 64;
            // 
            // gradientLabel2
            // 
            this.gradientLabel2.BackColor = System.Drawing.Color.White;
            this.gradientLabel2.ColorBottom = System.Drawing.Color.DarkGray;
            this.gradientLabel2.ColorTop = System.Drawing.Color.DimGray;
            this.gradientLabel2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel2.ForeColor = System.Drawing.Color.White;
            this.gradientLabel2.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel2.Location = new System.Drawing.Point(273, 38);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(99, 29);
            this.gradientLabel2.TabIndex = 66;
            this.gradientLabel2.Text = "Find Result";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabelFindResult
            // 
            this.gradientLabelFindResult.BackColor = System.Drawing.Color.DarkGreen;
            this.gradientLabelFindResult.ColorBottom = System.Drawing.Color.White;
            this.gradientLabelFindResult.ColorTop = System.Drawing.Color.White;
            this.gradientLabelFindResult.Font = new System.Drawing.Font("나눔바른고딕", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gradientLabelFindResult.ForeColor = System.Drawing.Color.Lime;
            this.gradientLabelFindResult.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabelFindResult.Location = new System.Drawing.Point(273, 72);
            this.gradientLabelFindResult.Name = "gradientLabelFindResult";
            this.gradientLabelFindResult.Size = new System.Drawing.Size(99, 70);
            this.gradientLabelFindResult.TabIndex = 68;
            this.gradientLabelFindResult.Tag = "0";
            this.gradientLabelFindResult.Text = "-";
            this.gradientLabelFindResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gradientLabel1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBoxCam2X);
            this.groupBox2.Controls.Add(this.textBoxCam2Y);
            this.groupBox2.Location = new System.Drawing.Point(140, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(128, 104);
            this.groupBox2.TabIndex = 67;
            this.groupBox2.TabStop = false;
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.White;
            this.gradientLabel1.ColorBottom = System.Drawing.Color.DarkGray;
            this.gradientLabel1.ColorTop = System.Drawing.Color.DimGray;
            this.gradientLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel1.Location = new System.Drawing.Point(0, 0);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(128, 29);
            this.gradientLabel1.TabIndex = 63;
            this.gradientLabel1.Text = "Cam #2 Mark";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 19);
            this.label3.TabIndex = 65;
            this.label3.Text = "Y :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 19);
            this.label4.TabIndex = 62;
            this.label4.Text = "X :";
            // 
            // textBoxCam2X
            // 
            this.textBoxCam2X.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCam2X.Location = new System.Drawing.Point(34, 34);
            this.textBoxCam2X.Name = "textBoxCam2X";
            this.textBoxCam2X.Size = new System.Drawing.Size(87, 29);
            this.textBoxCam2X.TabIndex = 61;
            // 
            // textBoxCam2Y
            // 
            this.textBoxCam2Y.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCam2Y.Location = new System.Drawing.Point(34, 68);
            this.textBoxCam2Y.Name = "textBoxCam2Y";
            this.textBoxCam2Y.ReadOnly = true;
            this.textBoxCam2Y.Size = new System.Drawing.Size(87, 29);
            this.textBoxCam2Y.TabIndex = 64;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gradientLabel3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxCam1X);
            this.groupBox1.Controls.Add(this.textBoxCam1Y);
            this.groupBox1.Location = new System.Drawing.Point(7, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(128, 104);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            // 
            // gradientLabel3
            // 
            this.gradientLabel3.BackColor = System.Drawing.Color.White;
            this.gradientLabel3.ColorBottom = System.Drawing.Color.DarkGray;
            this.gradientLabel3.ColorTop = System.Drawing.Color.DimGray;
            this.gradientLabel3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel3.ForeColor = System.Drawing.Color.White;
            this.gradientLabel3.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.gradientLabel3.Location = new System.Drawing.Point(0, 0);
            this.gradientLabel3.Name = "gradientLabel3";
            this.gradientLabel3.Size = new System.Drawing.Size(128, 29);
            this.gradientLabel3.TabIndex = 63;
            this.gradientLabel3.Text = "Cam #1 Mark";
            this.gradientLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 19);
            this.label2.TabIndex = 65;
            this.label2.Text = "Y :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 19);
            this.label1.TabIndex = 62;
            this.label1.Text = "X :";
            // 
            // textBoxCam1X
            // 
            this.textBoxCam1X.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCam1X.Location = new System.Drawing.Point(34, 34);
            this.textBoxCam1X.Name = "textBoxCam1X";
            this.textBoxCam1X.Size = new System.Drawing.Size(87, 29);
            this.textBoxCam1X.TabIndex = 61;
            // 
            // textBoxCam1Y
            // 
            this.textBoxCam1Y.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxCam1Y.Location = new System.Drawing.Point(34, 68);
            this.textBoxCam1Y.Name = "textBoxCam1Y";
            this.textBoxCam1Y.ReadOnly = true;
            this.textBoxCam1Y.Size = new System.Drawing.Size(87, 29);
            this.textBoxCam1Y.TabIndex = 64;
            // 
            // gradientLabelTitle
            // 
            this.gradientLabelTitle.BackColor = System.Drawing.Color.White;
            this.gradientLabelTitle.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(121)))), ((int)(((byte)(155)))));
            this.gradientLabelTitle.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(32)))), ((int)(((byte)(63)))));
            this.gradientLabelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabelTitle.ForeColor = System.Drawing.Color.White;
            this.gradientLabelTitle.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabelTitle.Location = new System.Drawing.Point(2, 1);
            this.gradientLabelTitle.Name = "gradientLabelTitle";
            this.gradientLabelTitle.Size = new System.Drawing.Size(376, 30);
            this.gradientLabelTitle.TabIndex = 30;
            this.gradientLabelTitle.Text = " Left Align Result";
            this.gradientLabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucMainResultSolarAlign
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "ucMainResultSolarAlign";
            this.Size = new System.Drawing.Size(378, 409);
            this.panelMain.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private CustomControl.GradientLabel gradientLabelTitle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCam2X;
        private System.Windows.Forms.TextBox textBoxCam2Y;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControl.GradientLabel gradientLabel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCam1X;
        private System.Windows.Forms.TextBox textBoxCam1Y;
        private CustomControl.GradientLabel gradientLabel7;
        private CustomControl.GradientLabel gradientLabelAlignResult;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxAlignW;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxAlignU;
        private System.Windows.Forms.TextBox textBoxAlignV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAlignT;
        private CustomControl.GradientLabel gradientLabel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAlignX;
        private System.Windows.Forms.TextBox textBoxAlignY;
        private CustomControl.GradientLabel gradientLabel2;
        private CustomControl.GradientLabel gradientLabelFindResult;
        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.Button btnAlignSetting;
    }
}
