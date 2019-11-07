namespace InspectionSystemManager
{
    partial class ucCogMultiPattern
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFindTop = new System.Windows.Forms.Button();
            this.btnPatternModifyTop = new System.Windows.Forms.Button();
            this.btnPatternAddTop = new System.Windows.Forms.Button();
            this.btnShowAreaTop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFindBottom = new System.Windows.Forms.Button();
            this.btnPatternModifyBottom = new System.Windows.Forms.Button();
            this.btnPatternAddBottom = new System.Windows.Forms.Button();
            this.btnShowAreaBottom = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDownAngleLimit = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFindCount = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownFindScore = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFindAll = new System.Windows.Forms.Button();
            this.gradientLabel5 = new CustomControl.GradientLabel();
            this.gradientLabel8 = new CustomControl.GradientLabel();
            this.gradientLabel9 = new CustomControl.GradientLabel();
            this.kpPatternDisplay1 = new KPDisplay.KPCogDisplayControl();
            this.kpPatternDisplay = new KPDisplay.KPCogDisplayControl();
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.txtBoxAngle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gradientLabel2 = new CustomControl.GradientLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFindTop);
            this.groupBox1.Controls.Add(this.btnPatternModifyTop);
            this.groupBox1.Controls.Add(this.btnPatternAddTop);
            this.groupBox1.Controls.Add(this.btnShowAreaTop);
            this.groupBox1.Controls.Add(this.kpPatternDisplay);
            this.groupBox1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(5, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 252);
            this.groupBox1.TabIndex = 55;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Top ";
            // 
            // btnFindTop
            // 
            this.btnFindTop.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFindTop.ForeColor = System.Drawing.Color.Black;
            this.btnFindTop.Location = new System.Drawing.Point(181, 208);
            this.btnFindTop.Name = "btnFindTop";
            this.btnFindTop.Size = new System.Drawing.Size(57, 39);
            this.btnFindTop.TabIndex = 78;
            this.btnFindTop.Tag = "0";
            this.btnFindTop.Text = "Find";
            this.btnFindTop.UseVisualStyleBackColor = true;
            this.btnFindTop.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnPatternModifyTop
            // 
            this.btnPatternModifyTop.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternModifyTop.ForeColor = System.Drawing.Color.Black;
            this.btnPatternModifyTop.Location = new System.Drawing.Point(122, 208);
            this.btnPatternModifyTop.Name = "btnPatternModifyTop";
            this.btnPatternModifyTop.Size = new System.Drawing.Size(57, 39);
            this.btnPatternModifyTop.TabIndex = 77;
            this.btnPatternModifyTop.Tag = "0";
            this.btnPatternModifyTop.Text = "Modify";
            this.btnPatternModifyTop.UseVisualStyleBackColor = true;
            this.btnPatternModifyTop.Click += new System.EventHandler(this.btnPatternModify_Click);
            // 
            // btnPatternAddTop
            // 
            this.btnPatternAddTop.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternAddTop.ForeColor = System.Drawing.Color.Black;
            this.btnPatternAddTop.Location = new System.Drawing.Point(63, 208);
            this.btnPatternAddTop.Name = "btnPatternAddTop";
            this.btnPatternAddTop.Size = new System.Drawing.Size(57, 39);
            this.btnPatternAddTop.TabIndex = 76;
            this.btnPatternAddTop.Tag = "0";
            this.btnPatternAddTop.Text = "Add";
            this.btnPatternAddTop.UseVisualStyleBackColor = true;
            this.btnPatternAddTop.Click += new System.EventHandler(this.btnPatternAdd_Click);
            // 
            // btnShowAreaTop
            // 
            this.btnShowAreaTop.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowAreaTop.ForeColor = System.Drawing.Color.Black;
            this.btnShowAreaTop.Location = new System.Drawing.Point(4, 208);
            this.btnShowAreaTop.Name = "btnShowAreaTop";
            this.btnShowAreaTop.Size = new System.Drawing.Size(57, 39);
            this.btnShowAreaTop.TabIndex = 75;
            this.btnShowAreaTop.Tag = "0";
            this.btnShowAreaTop.Text = "New Area";
            this.btnShowAreaTop.UseVisualStyleBackColor = true;
            this.btnShowAreaTop.Click += new System.EventHandler(this.btnShowArea_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFindBottom);
            this.groupBox2.Controls.Add(this.btnPatternModifyBottom);
            this.groupBox2.Controls.Add(this.btnPatternAddBottom);
            this.groupBox2.Controls.Add(this.btnShowAreaBottom);
            this.groupBox2.Controls.Add(this.kpPatternDisplay1);
            this.groupBox2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(253, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 252);
            this.groupBox2.TabIndex = 76;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Bottom ";
            // 
            // btnFindBottom
            // 
            this.btnFindBottom.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFindBottom.ForeColor = System.Drawing.Color.Black;
            this.btnFindBottom.Location = new System.Drawing.Point(181, 208);
            this.btnFindBottom.Name = "btnFindBottom";
            this.btnFindBottom.Size = new System.Drawing.Size(57, 39);
            this.btnFindBottom.TabIndex = 82;
            this.btnFindBottom.Tag = "1";
            this.btnFindBottom.Text = "Find";
            this.btnFindBottom.UseVisualStyleBackColor = true;
            this.btnFindBottom.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnPatternModifyBottom
            // 
            this.btnPatternModifyBottom.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternModifyBottom.ForeColor = System.Drawing.Color.Black;
            this.btnPatternModifyBottom.Location = new System.Drawing.Point(122, 208);
            this.btnPatternModifyBottom.Name = "btnPatternModifyBottom";
            this.btnPatternModifyBottom.Size = new System.Drawing.Size(57, 39);
            this.btnPatternModifyBottom.TabIndex = 81;
            this.btnPatternModifyBottom.Tag = "1";
            this.btnPatternModifyBottom.Text = "Modify";
            this.btnPatternModifyBottom.UseVisualStyleBackColor = true;
            this.btnPatternModifyBottom.Click += new System.EventHandler(this.btnPatternModify_Click);
            // 
            // btnPatternAddBottom
            // 
            this.btnPatternAddBottom.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternAddBottom.ForeColor = System.Drawing.Color.Black;
            this.btnPatternAddBottom.Location = new System.Drawing.Point(63, 208);
            this.btnPatternAddBottom.Name = "btnPatternAddBottom";
            this.btnPatternAddBottom.Size = new System.Drawing.Size(57, 39);
            this.btnPatternAddBottom.TabIndex = 80;
            this.btnPatternAddBottom.Tag = "1";
            this.btnPatternAddBottom.Text = "Add";
            this.btnPatternAddBottom.UseVisualStyleBackColor = true;
            this.btnPatternAddBottom.Click += new System.EventHandler(this.btnPatternAdd_Click);
            // 
            // btnShowAreaBottom
            // 
            this.btnShowAreaBottom.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowAreaBottom.ForeColor = System.Drawing.Color.Black;
            this.btnShowAreaBottom.Location = new System.Drawing.Point(4, 208);
            this.btnShowAreaBottom.Name = "btnShowAreaBottom";
            this.btnShowAreaBottom.Size = new System.Drawing.Size(57, 39);
            this.btnShowAreaBottom.TabIndex = 79;
            this.btnShowAreaBottom.Tag = "1";
            this.btnShowAreaBottom.Text = "New Area";
            this.btnShowAreaBottom.UseVisualStyleBackColor = true;
            this.btnShowAreaBottom.Click += new System.EventHandler(this.btnShowArea_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gradientLabel2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtBoxAngle);
            this.groupBox3.Controls.Add(this.btnFindAll);
            this.groupBox3.Controls.Add(this.numericUpDownAngleLimit);
            this.groupBox3.Controls.Add(this.gradientLabel5);
            this.groupBox3.Controls.Add(this.numericUpDownFindCount);
            this.groupBox3.Controls.Add(this.gradientLabel8);
            this.groupBox3.Controls.Add(this.numericUpDownFindScore);
            this.groupBox3.Controls.Add(this.gradientLabel9);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(5, 296);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(490, 87);
            this.groupBox3.TabIndex = 77;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Condition Setting ";
            // 
            // numericUpDownAngleLimit
            // 
            this.numericUpDownAngleLimit.DecimalPlaces = 2;
            this.numericUpDownAngleLimit.Location = new System.Drawing.Point(360, 20);
            this.numericUpDownAngleLimit.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownAngleLimit.Name = "numericUpDownAngleLimit";
            this.numericUpDownAngleLimit.Size = new System.Drawing.Size(87, 21);
            this.numericUpDownAngleLimit.TabIndex = 65;
            this.numericUpDownAngleLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownAngleLimit.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownFindCount
            // 
            this.numericUpDownFindCount.Location = new System.Drawing.Point(119, 52);
            this.numericUpDownFindCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFindCount.Name = "numericUpDownFindCount";
            this.numericUpDownFindCount.Size = new System.Drawing.Size(87, 21);
            this.numericUpDownFindCount.TabIndex = 63;
            this.numericUpDownFindCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFindCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownFindScore
            // 
            this.numericUpDownFindScore.DecimalPlaces = 2;
            this.numericUpDownFindScore.Location = new System.Drawing.Point(119, 20);
            this.numericUpDownFindScore.Name = "numericUpDownFindScore";
            this.numericUpDownFindScore.Size = new System.Drawing.Size(87, 21);
            this.numericUpDownFindScore.TabIndex = 61;
            this.numericUpDownFindScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFindScore.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(450, 24);
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
            this.label5.Location = new System.Drawing.Point(208, 59);
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
            this.label4.Location = new System.Drawing.Point(209, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 14);
            this.label4.TabIndex = 75;
            this.label4.Text = "%";
            // 
            // btnFindAll
            // 
            this.btnFindAll.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFindAll.ForeColor = System.Drawing.Color.Black;
            this.btnFindAll.Location = new System.Drawing.Point(359, 50);
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(39, 28);
            this.btnFindAll.TabIndex = 83;
            this.btnFindAll.Tag = "-1";
            this.btnFindAll.Text = "Cal";
            this.btnFindAll.UseVisualStyleBackColor = true;
            this.btnFindAll.Click += new System.EventHandler(this.btnFind_Click);
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
            this.gradientLabel5.Location = new System.Drawing.Point(248, 17);
            this.gradientLabel5.Name = "gradientLabel5";
            this.gradientLabel5.Size = new System.Drawing.Size(107, 28);
            this.gradientLabel5.TabIndex = 64;
            this.gradientLabel5.Text = "Angle Limit";
            this.gradientLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.gradientLabel8.Location = new System.Drawing.Point(7, 50);
            this.gradientLabel8.Name = "gradientLabel8";
            this.gradientLabel8.Size = new System.Drawing.Size(107, 28);
            this.gradientLabel8.TabIndex = 62;
            this.gradientLabel8.Text = "Find Count";
            this.gradientLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.gradientLabel9.Location = new System.Drawing.Point(7, 17);
            this.gradientLabel9.Name = "gradientLabel9";
            this.gradientLabel9.Size = new System.Drawing.Size(107, 28);
            this.gradientLabel9.TabIndex = 60;
            this.gradientLabel9.Text = "Find Score";
            this.gradientLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // kpPatternDisplay1
            // 
            this.kpPatternDisplay1.BackColor = System.Drawing.Color.White;
            this.kpPatternDisplay1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpPatternDisplay1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpPatternDisplay1.Location = new System.Drawing.Point(4, 14);
            this.kpPatternDisplay1.Name = "kpPatternDisplay1";
            this.kpPatternDisplay1.Size = new System.Drawing.Size(234, 190);
            this.kpPatternDisplay1.TabIndex = 13;
            this.kpPatternDisplay1.Tag = "1";
            this.kpPatternDisplay1.UseStatusBar = false;
            // 
            // kpPatternDisplay
            // 
            this.kpPatternDisplay.BackColor = System.Drawing.Color.White;
            this.kpPatternDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpPatternDisplay.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpPatternDisplay.Location = new System.Drawing.Point(4, 14);
            this.kpPatternDisplay.Name = "kpPatternDisplay";
            this.kpPatternDisplay.Size = new System.Drawing.Size(233, 190);
            this.kpPatternDisplay.TabIndex = 13;
            this.kpPatternDisplay.Tag = "0";
            this.kpPatternDisplay.UseStatusBar = false;
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.White;
            this.gradientLabel1.ColorBottom = System.Drawing.Color.White;
            this.gradientLabel1.ColorTop = System.Drawing.Color.SteelBlue;
            this.gradientLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel1.Location = new System.Drawing.Point(2, 2);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(497, 33);
            this.gradientLabel1.TabIndex = 54;
            this.gradientLabel1.Text = " Multi Pattern Teaching Window";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxAngle
            // 
            this.txtBoxAngle.Location = new System.Drawing.Point(404, 53);
            this.txtBoxAngle.Name = "txtBoxAngle";
            this.txtBoxAngle.Size = new System.Drawing.Size(43, 21);
            this.txtBoxAngle.TabIndex = 84;
            this.txtBoxAngle.Text = "20.25";
            this.txtBoxAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(450, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 14);
            this.label1.TabIndex = 85;
            this.label1.Text = "˚";
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
            this.gradientLabel2.Location = new System.Drawing.Point(248, 50);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(107, 28);
            this.gradientLabel2.TabIndex = 86;
            this.gradientLabel2.Text = "Angle Result";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucCogMultiPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gradientLabel1);
            this.Name = "ucCogMultiPattern";
            this.Size = new System.Drawing.Size(500, 388);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnShowAreaTop;
        private KPDisplay.KPCogDisplayControl kpPatternDisplay;
        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private KPDisplay.KPCogDisplayControl kpPatternDisplay1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleLimit;
        private CustomControl.GradientLabel gradientLabel5;
        private System.Windows.Forms.NumericUpDown numericUpDownFindCount;
        private CustomControl.GradientLabel gradientLabel8;
        private System.Windows.Forms.NumericUpDown numericUpDownFindScore;
        private CustomControl.GradientLabel gradientLabel9;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFindTop;
        private System.Windows.Forms.Button btnPatternModifyTop;
        private System.Windows.Forms.Button btnPatternAddTop;
        private System.Windows.Forms.Button btnFindBottom;
        private System.Windows.Forms.Button btnPatternModifyBottom;
        private System.Windows.Forms.Button btnPatternAddBottom;
        private System.Windows.Forms.Button btnShowAreaBottom;
        private System.Windows.Forms.Button btnFindAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxAngle;
        private CustomControl.GradientLabel gradientLabel2;
    }
}
