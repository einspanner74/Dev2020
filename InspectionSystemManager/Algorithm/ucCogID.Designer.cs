namespace InspectionSystemManager
{
    partial class ucCogID
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numUpDownNumtoFind = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSymbology = new System.Windows.Forms.ComboBox();
            this.textBoxProcessingMode = new System.Windows.Forms.TextBox();
            this.gradientLabel12 = new CustomControl.GradientLabel();
            this.gradientLabel13 = new CustomControl.GradientLabel();
            this.gradientLabel14 = new CustomControl.GradientLabel();
            this.btnSetting = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNumtoFind)).BeginInit();
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
            this.gradientLabel1.Size = new System.Drawing.Size(497, 30);
            this.gradientLabel1.TabIndex = 13;
            this.gradientLabel1.Text = " ID Teaching Window";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numUpDownNumtoFind);
            this.groupBox3.Controls.Add(this.comboBoxSymbology);
            this.groupBox3.Controls.Add(this.textBoxProcessingMode);
            this.groupBox3.Controls.Add(this.gradientLabel12);
            this.groupBox3.Controls.Add(this.gradientLabel13);
            this.groupBox3.Controls.Add(this.gradientLabel14);
            this.groupBox3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(5, 35);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 127);
            this.groupBox3.TabIndex = 73;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " ID Setting";
            // 
            // numUpDownNumtoFind
            // 
            this.numUpDownNumtoFind.Location = new System.Drawing.Point(143, 87);
            this.numUpDownNumtoFind.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numUpDownNumtoFind.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownNumtoFind.Name = "numUpDownNumtoFind";
            this.numUpDownNumtoFind.Size = new System.Drawing.Size(128, 21);
            this.numUpDownNumtoFind.TabIndex = 75;
            this.numUpDownNumtoFind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownNumtoFind.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxSymbology
            // 
            this.comboBoxSymbology.FormattingEnabled = true;
            this.comboBoxSymbology.Location = new System.Drawing.Point(143, 54);
            this.comboBoxSymbology.Name = "comboBoxSymbology";
            this.comboBoxSymbology.Size = new System.Drawing.Size(128, 22);
            this.comboBoxSymbology.TabIndex = 74;
            // 
            // textBoxProcessingMode
            // 
            this.textBoxProcessingMode.Enabled = false;
            this.textBoxProcessingMode.Location = new System.Drawing.Point(143, 24);
            this.textBoxProcessingMode.Name = "textBoxProcessingMode";
            this.textBoxProcessingMode.ReadOnly = true;
            this.textBoxProcessingMode.Size = new System.Drawing.Size(128, 21);
            this.textBoxProcessingMode.TabIndex = 68;
            this.textBoxProcessingMode.Text = "IDMax";
            this.textBoxProcessingMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientLabel12
            // 
            this.gradientLabel12.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel12.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel12.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel12.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel12.ForeColor = System.Drawing.Color.White;
            this.gradientLabel12.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel12.Location = new System.Drawing.Point(13, 83);
            this.gradientLabel12.Name = "gradientLabel12";
            this.gradientLabel12.Size = new System.Drawing.Size(124, 26);
            this.gradientLabel12.TabIndex = 66;
            this.gradientLabel12.Text = "Num to Find";
            this.gradientLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabel13
            // 
            this.gradientLabel13.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel13.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel13.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel13.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel13.ForeColor = System.Drawing.Color.White;
            this.gradientLabel13.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel13.Location = new System.Drawing.Point(13, 52);
            this.gradientLabel13.Name = "gradientLabel13";
            this.gradientLabel13.Size = new System.Drawing.Size(124, 26);
            this.gradientLabel13.TabIndex = 64;
            this.gradientLabel13.Text = " Symbology";
            this.gradientLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabel14
            // 
            this.gradientLabel14.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel14.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel14.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel14.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel14.ForeColor = System.Drawing.Color.White;
            this.gradientLabel14.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel14.Location = new System.Drawing.Point(13, 21);
            this.gradientLabel14.Name = "gradientLabel14";
            this.gradientLabel14.Size = new System.Drawing.Size(124, 26);
            this.gradientLabel14.TabIndex = 62;
            this.gradientLabel14.Text = " Processing Mode";
            this.gradientLabel14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetting.ForeColor = System.Drawing.Color.Black;
            this.btnSetting.Location = new System.Drawing.Point(395, 348);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(100, 37);
            this.btnSetting.TabIndex = 74;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // ucCogID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gradientLabel1);
            this.Name = "ucCogID";
            this.Size = new System.Drawing.Size(500, 388);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownNumtoFind)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxSymbology;
        private System.Windows.Forms.TextBox textBoxProcessingMode;
        private CustomControl.GradientLabel gradientLabel12;
        private CustomControl.GradientLabel gradientLabel13;
        private CustomControl.GradientLabel gradientLabel14;
        private System.Windows.Forms.NumericUpDown numUpDownNumtoFind;
        private System.Windows.Forms.Button btnSetting;
    }
}
