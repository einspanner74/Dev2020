namespace InspectionSystemManager
{
    partial class ucCogAutoPattern
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
            this.btnFind = new System.Windows.Forms.Button();
            this.btnPatternModify = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownThreshold = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel5 = new CustomControl.GradientLabel();
            this.numericUpDownFindScore = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel9 = new CustomControl.GradientLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kpPatternDisplay = new KPDisplay.KPCogDisplayControl();
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnFind.ForeColor = System.Drawing.Color.Black;
            this.btnFind.Location = new System.Drawing.Point(430, 21);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 26);
            this.btnFind.TabIndex = 81;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnPatternModify
            // 
            this.btnPatternModify.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPatternModify.ForeColor = System.Drawing.Color.Black;
            this.btnPatternModify.Location = new System.Drawing.Point(372, 21);
            this.btnPatternModify.Name = "btnPatternModify";
            this.btnPatternModify.Size = new System.Drawing.Size(56, 26);
            this.btnPatternModify.TabIndex = 80;
            this.btnPatternModify.Text = "Modify";
            this.btnPatternModify.UseVisualStyleBackColor = true;
            this.btnPatternModify.Click += new System.EventHandler(this.btnPatternModify_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnFind);
            this.groupBox2.Controls.Add(this.numericUpDownThreshold);
            this.groupBox2.Controls.Add(this.btnPatternModify);
            this.groupBox2.Controls.Add(this.gradientLabel5);
            this.groupBox2.Controls.Add(this.numericUpDownFindScore);
            this.groupBox2.Controls.Add(this.gradientLabel9);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(3, 325);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 59);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Condition Setting ";
            // 
            // numericUpDownThreshold
            // 
            this.numericUpDownThreshold.Location = new System.Drawing.Point(291, 23);
            this.numericUpDownThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownThreshold.Name = "numericUpDownThreshold";
            this.numericUpDownThreshold.Size = new System.Drawing.Size(67, 21);
            this.numericUpDownThreshold.TabIndex = 65;
            this.numericUpDownThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownThreshold.Value = new decimal(new int[] {
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
            this.gradientLabel5.Location = new System.Drawing.Point(203, 21);
            this.gradientLabel5.Name = "gradientLabel5";
            this.gradientLabel5.Size = new System.Drawing.Size(85, 26);
            this.gradientLabel5.TabIndex = 64;
            this.gradientLabel5.Text = "Threashold";
            this.gradientLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDownFindScore
            // 
            this.numericUpDownFindScore.DecimalPlaces = 2;
            this.numericUpDownFindScore.Location = new System.Drawing.Point(95, 23);
            this.numericUpDownFindScore.Name = "numericUpDownFindScore";
            this.numericUpDownFindScore.Size = new System.Drawing.Size(84, 21);
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
            this.gradientLabel9.Location = new System.Drawing.Point(7, 21);
            this.gradientLabel9.Name = "gradientLabel9";
            this.gradientLabel9.Size = new System.Drawing.Size(85, 26);
            this.gradientLabel9.TabIndex = 60;
            this.gradientLabel9.Text = "Find Score";
            this.gradientLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(179, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 14);
            this.label4.TabIndex = 75;
            this.label4.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kpPatternDisplay);
            this.groupBox1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 286);
            this.groupBox1.TabIndex = 76;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Pattern ";
            // 
            // kpPatternDisplay
            // 
            this.kpPatternDisplay.BackColor = System.Drawing.Color.White;
            this.kpPatternDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kpPatternDisplay.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.kpPatternDisplay.Location = new System.Drawing.Point(7, 14);
            this.kpPatternDisplay.Name = "kpPatternDisplay";
            this.kpPatternDisplay.Size = new System.Drawing.Size(479, 266);
            this.kpPatternDisplay.TabIndex = 13;
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
            this.gradientLabel1.Location = new System.Drawing.Point(2, 0);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(497, 30);
            this.gradientLabel1.TabIndex = 75;
            this.gradientLabel1.Text = " Auto Pattern Teaching Window";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucCogAutoPattern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gradientLabel1);
            this.Name = "ucCogAutoPattern";
            this.Size = new System.Drawing.Size(500, 388);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFindScore)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnPatternModify;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownThreshold;
        private CustomControl.GradientLabel gradientLabel5;
        private System.Windows.Forms.NumericUpDown numericUpDownFindScore;
        private CustomControl.GradientLabel gradientLabel9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private KPDisplay.KPCogDisplayControl kpPatternDisplay;
        private CustomControl.GradientLabel gradientLabel1;
    }
}
