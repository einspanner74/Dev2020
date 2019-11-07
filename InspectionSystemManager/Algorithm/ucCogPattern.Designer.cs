namespace InspectionSystemManager
{
    partial class ucCogPattern
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowArea = new System.Windows.Forms.Button();
            this.labelPatternCount = new System.Windows.Forms.Label();
            this.kpPatternDisplay = new KPDisplay.KPCogDisplayControl();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownAllowedShiftY = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel2 = new CustomControl.GradientLabel();
            this.numericUpDownAllowedShiftX = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel3 = new CustomControl.GradientLabel();
            this.checkBoxShift = new System.Windows.Forms.CheckBox();
            this.numericUpDownAngleLimit = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel5 = new CustomControl.GradientLabel();
            this.numericUpDownFindCount = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel8 = new CustomControl.GradientLabel();
            this.numericUpDownFindScore = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel9 = new CustomControl.GradientLabel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnPatternModify = new System.Windows.Forms.Button();
            this.btnPatternDel = new System.Windows.Forms.Button();
            this.btnPatternAdd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllowedShiftY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllowedShiftX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).BeginInit();
            this.SuspendLayout();
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.White;
            this.gradientLabel1.ColorBottom = System.Drawing.Color.White;
            this.gradientLabel1.ColorTop = System.Drawing.Color.SteelBlue;
            this.gradientLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel1.Location = new System.Drawing.Point(2, 0);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(580, 28);
            this.gradientLabel1.TabIndex = 12;
            this.gradientLabel1.Text = " Pattern Reference Teaching Window";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPrev
            // 
            this.btnPrev.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrev.ForeColor = System.Drawing.Color.Black;
            this.btnPrev.Location = new System.Drawing.Point(8, 290);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(30, 30);
            this.btnPrev.TabIndex = 14;
            this.btnPrev.Text = "◀";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("나눔바른고딕", 10F, System.Drawing.FontStyle.Bold);
            this.btnNext.ForeColor = System.Drawing.Color.Black;
            this.btnNext.Location = new System.Drawing.Point(166, 290);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(30, 30);
            this.btnNext.TabIndex = 15;
            this.btnNext.Text = "▶";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowArea);
            this.groupBox1.Controls.Add(this.labelPatternCount);
            this.groupBox1.Controls.Add(this.kpPatternDisplay);
            this.groupBox1.Controls.Add(this.btnPrev);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 325);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Pattern ";
            // 
            // btnShowArea
            // 
            this.btnShowArea.Font = new System.Drawing.Font("나눔바른고딕", 8F, System.Drawing.FontStyle.Bold);
            this.btnShowArea.ForeColor = System.Drawing.Color.Black;
            this.btnShowArea.Location = new System.Drawing.Point(203, 288);
            this.btnShowArea.Name = "btnShowArea";
            this.btnShowArea.Size = new System.Drawing.Size(77, 34);
            this.btnShowArea.TabIndex = 75;
            this.btnShowArea.Text = "New Pattern Area";
            this.btnShowArea.UseVisualStyleBackColor = true;
            this.btnShowArea.Click += new System.EventHandler(this.btnShowArea_Click);
            // 
            // labelPatternCount
            // 
            this.labelPatternCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPatternCount.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelPatternCount.ForeColor = System.Drawing.Color.White;
            this.labelPatternCount.Location = new System.Drawing.Point(40, 290);
            this.labelPatternCount.Name = "labelPatternCount";
            this.labelPatternCount.Size = new System.Drawing.Size(124, 30);
            this.labelPatternCount.TabIndex = 16;
            this.labelPatternCount.Text = "0/0";
            this.labelPatternCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kpPatternDisplay
            // 
            this.kpPatternDisplay.BackColor = System.Drawing.Color.White;
            this.kpPatternDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpPatternDisplay.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpPatternDisplay.Location = new System.Drawing.Point(8, 14);
            this.kpPatternDisplay.Name = "kpPatternDisplay";
            this.kpPatternDisplay.Size = new System.Drawing.Size(271, 272);
            this.kpPatternDisplay.TabIndex = 13;
            this.kpPatternDisplay.UseStatusBar = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownAllowedShiftY);
            this.groupBox2.Controls.Add(this.gradientLabel2);
            this.groupBox2.Controls.Add(this.numericUpDownAllowedShiftX);
            this.groupBox2.Controls.Add(this.gradientLabel3);
            this.groupBox2.Controls.Add(this.checkBoxShift);
            this.groupBox2.Controls.Add(this.numericUpDownAngleLimit);
            this.groupBox2.Controls.Add(this.gradientLabel5);
            this.groupBox2.Controls.Add(this.numericUpDownFindCount);
            this.groupBox2.Controls.Add(this.gradientLabel8);
            this.groupBox2.Controls.Add(this.numericUpDownFindScore);
            this.groupBox2.Controls.Add(this.gradientLabel9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(296, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 285);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Condition Setting ";
            // 
            // numericUpDownAllowedShiftY
            // 
            this.numericUpDownAllowedShiftY.DecimalPlaces = 2;
            this.numericUpDownAllowedShiftY.Location = new System.Drawing.Point(139, 174);
            this.numericUpDownAllowedShiftY.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownAllowedShiftY.Name = "numericUpDownAllowedShiftY";
            this.numericUpDownAllowedShiftY.Size = new System.Drawing.Size(101, 21);
            this.numericUpDownAllowedShiftY.TabIndex = 70;
            this.numericUpDownAllowedShiftY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientLabel2
            // 
            this.gradientLabel2.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel2.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel2.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel2.ForeColor = System.Drawing.Color.White;
            this.gradientLabel2.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel2.Location = new System.Drawing.Point(8, 173);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(124, 24);
            this.gradientLabel2.TabIndex = 69;
            this.gradientLabel2.Text = "Allowed Shift Y";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownAllowedShiftX
            // 
            this.numericUpDownAllowedShiftX.DecimalPlaces = 2;
            this.numericUpDownAllowedShiftX.Location = new System.Drawing.Point(139, 147);
            this.numericUpDownAllowedShiftX.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownAllowedShiftX.Name = "numericUpDownAllowedShiftX";
            this.numericUpDownAllowedShiftX.Size = new System.Drawing.Size(101, 21);
            this.numericUpDownAllowedShiftX.TabIndex = 68;
            this.numericUpDownAllowedShiftX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientLabel3
            // 
            this.gradientLabel3.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel3.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel3.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel3.ForeColor = System.Drawing.Color.White;
            this.gradientLabel3.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel3.Location = new System.Drawing.Point(8, 145);
            this.gradientLabel3.Name = "gradientLabel3";
            this.gradientLabel3.Size = new System.Drawing.Size(124, 24);
            this.gradientLabel3.TabIndex = 67;
            this.gradientLabel3.Text = "Allowed Shift X";
            this.gradientLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxShift
            // 
            this.checkBoxShift.AutoSize = true;
            this.checkBoxShift.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBoxShift.ForeColor = System.Drawing.Color.White;
            this.checkBoxShift.Location = new System.Drawing.Point(8, 126);
            this.checkBoxShift.Name = "checkBoxShift";
            this.checkBoxShift.Size = new System.Drawing.Size(51, 18);
            this.checkBoxShift.TabIndex = 66;
            this.checkBoxShift.Text = "Shift";
            this.checkBoxShift.UseVisualStyleBackColor = true;
            // 
            // numericUpDownAngleLimit
            // 
            this.numericUpDownAngleLimit.DecimalPlaces = 2;
            this.numericUpDownAngleLimit.Location = new System.Drawing.Point(139, 77);
            this.numericUpDownAngleLimit.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownAngleLimit.Name = "numericUpDownAngleLimit";
            this.numericUpDownAngleLimit.Size = new System.Drawing.Size(101, 21);
            this.numericUpDownAngleLimit.TabIndex = 65;
            this.numericUpDownAngleLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownAngleLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // gradientLabel5
            // 
            this.gradientLabel5.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel5.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel5.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel5.ForeColor = System.Drawing.Color.White;
            this.gradientLabel5.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel5.Location = new System.Drawing.Point(8, 75);
            this.gradientLabel5.Name = "gradientLabel5";
            this.gradientLabel5.Size = new System.Drawing.Size(124, 24);
            this.gradientLabel5.TabIndex = 64;
            this.gradientLabel5.Text = "Angle Limit";
            this.gradientLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownFindCount
            // 
            this.numericUpDownFindCount.Location = new System.Drawing.Point(139, 49);
            this.numericUpDownFindCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFindCount.Name = "numericUpDownFindCount";
            this.numericUpDownFindCount.Size = new System.Drawing.Size(101, 21);
            this.numericUpDownFindCount.TabIndex = 63;
            this.numericUpDownFindCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFindCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gradientLabel8
            // 
            this.gradientLabel8.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel8.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel8.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel8.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel8.ForeColor = System.Drawing.Color.White;
            this.gradientLabel8.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel8.Location = new System.Drawing.Point(8, 47);
            this.gradientLabel8.Name = "gradientLabel8";
            this.gradientLabel8.Size = new System.Drawing.Size(124, 24);
            this.gradientLabel8.TabIndex = 62;
            this.gradientLabel8.Text = "Find Count";
            this.gradientLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownFindScore
            // 
            this.numericUpDownFindScore.DecimalPlaces = 2;
            this.numericUpDownFindScore.Location = new System.Drawing.Point(139, 21);
            this.numericUpDownFindScore.Name = "numericUpDownFindScore";
            this.numericUpDownFindScore.Size = new System.Drawing.Size(101, 21);
            this.numericUpDownFindScore.TabIndex = 61;
            this.numericUpDownFindScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFindScore.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // gradientLabel9
            // 
            this.gradientLabel9.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel9.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel9.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel9.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel9.ForeColor = System.Drawing.Color.White;
            this.gradientLabel9.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel9.Location = new System.Drawing.Point(8, 19);
            this.gradientLabel9.Name = "gradientLabel9";
            this.gradientLabel9.Size = new System.Drawing.Size(124, 24);
            this.gradientLabel9.TabIndex = 60;
            this.gradientLabel9.Text = "Find Score";
            this.gradientLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(244, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 14);
            this.label11.TabIndex = 71;
            this.label11.Text = "mm";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(244, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 14);
            this.label10.TabIndex = 72;
            this.label10.Text = "mm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(244, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 14);
            this.label9.TabIndex = 73;
            this.label9.Text = "˚";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(243, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 74;
            this.label5.Text = "ea";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(244, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 14);
            this.label4.TabIndex = 75;
            this.label4.Text = "%";
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFind.Location = new System.Drawing.Point(512, 317);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(65, 34);
            this.btnFind.TabIndex = 74;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnPatternModify
            // 
            this.btnPatternModify.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternModify.Location = new System.Drawing.Point(441, 317);
            this.btnPatternModify.Name = "btnPatternModify";
            this.btnPatternModify.Size = new System.Drawing.Size(65, 34);
            this.btnPatternModify.TabIndex = 73;
            this.btnPatternModify.Text = "Modify";
            this.btnPatternModify.UseVisualStyleBackColor = true;
            this.btnPatternModify.Click += new System.EventHandler(this.btnPatternModify_Click);
            // 
            // btnPatternDel
            // 
            this.btnPatternDel.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternDel.Location = new System.Drawing.Point(370, 317);
            this.btnPatternDel.Name = "btnPatternDel";
            this.btnPatternDel.Size = new System.Drawing.Size(65, 34);
            this.btnPatternDel.TabIndex = 72;
            this.btnPatternDel.Text = "Del";
            this.btnPatternDel.UseVisualStyleBackColor = true;
            this.btnPatternDel.Click += new System.EventHandler(this.btnPatternDel_Click);
            // 
            // btnPatternAdd
            // 
            this.btnPatternAdd.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternAdd.Location = new System.Drawing.Point(299, 317);
            this.btnPatternAdd.Name = "btnPatternAdd";
            this.btnPatternAdd.Size = new System.Drawing.Size(65, 34);
            this.btnPatternAdd.TabIndex = 71;
            this.btnPatternAdd.Text = "Add";
            this.btnPatternAdd.UseVisualStyleBackColor = true;
            this.btnPatternAdd.Click += new System.EventHandler(this.btnPatternAdd_Click);
            // 
            // ucCogPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnPatternModify);
            this.Controls.Add(this.btnPatternDel);
            this.Controls.Add(this.btnPatternAdd);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gradientLabel1);
            this.Name = "ucCogPattern";
            this.Size = new System.Drawing.Size(583, 358);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllowedShiftY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllowedShiftX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.GroupBox groupBox1;
        private KPDisplay.KPCogDisplayControl kpPatternDisplay;
        private System.Windows.Forms.Label labelPatternCount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownAllowedShiftY;
        private CustomControl.GradientLabel gradientLabel2;
        private System.Windows.Forms.NumericUpDown numericUpDownAllowedShiftX;
        private CustomControl.GradientLabel gradientLabel3;
        private System.Windows.Forms.CheckBox checkBoxShift;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleLimit;
        private CustomControl.GradientLabel gradientLabel5;
        private System.Windows.Forms.NumericUpDown numericUpDownFindCount;
        private CustomControl.GradientLabel gradientLabel8;
        private System.Windows.Forms.NumericUpDown numericUpDownFindScore;
        private CustomControl.GradientLabel gradientLabel9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnPatternModify;
        private System.Windows.Forms.Button btnPatternDel;
        private System.Windows.Forms.Button btnPatternAdd;
        private System.Windows.Forms.Button btnShowArea;
    }
}
