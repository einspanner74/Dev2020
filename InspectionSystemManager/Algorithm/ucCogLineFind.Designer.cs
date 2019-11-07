namespace InspectionSystemManager
{
    partial class ucCogLineFind
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
            this.numUpDownIgnoreNumber = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel9 = new CustomControl.GradientLabel();
            this.gradientLabel6 = new CustomControl.GradientLabel();
            this.rbSearchDirectionIn = new System.Windows.Forms.RadioButton();
            this.rbSearchDirectionOut = new System.Windows.Forms.RadioButton();
            this.numUpDownProjectionLength = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel2 = new CustomControl.GradientLabel();
            this.numUpDownSearchLength = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel1 = new CustomControl.GradientLabel();
            this.numUpDownCaliperNumber = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel7 = new CustomControl.GradientLabel();
            this.graLabelSearchDirection = new CustomControl.GradientLabel();
            this.numUpDownFilterHalfSizePixels = new System.Windows.Forms.NumericUpDown();
            this.numUpDownContrastThreshold = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numUpDownEndY = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel8 = new CustomControl.GradientLabel();
            this.numUpDownEndX = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel5 = new CustomControl.GradientLabel();
            this.numUpDownStartY = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel4 = new CustomControl.GradientLabel();
            this.numUpDownStartX = new System.Windows.Forms.NumericUpDown();
            this.gradientLabel3 = new CustomControl.GradientLabel();
            this.btnDrawCaliper = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.ckUseAlignment = new System.Windows.Forms.CheckBox();
            this.gradientLabel11 = new CustomControl.GradientLabel();
            this.gradientLabel10 = new CustomControl.GradientLabel();
            this.labelTitle = new CustomControl.GradientLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownIgnoreNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownProjectionLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSearchLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownCaliperNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFilterHalfSizePixels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownContrastThreshold)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEndY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEndX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownStartY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownStartX)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numUpDownIgnoreNumber);
            this.groupBox1.Controls.Add(this.gradientLabel9);
            this.groupBox1.Controls.Add(this.gradientLabel6);
            this.groupBox1.Controls.Add(this.rbSearchDirectionIn);
            this.groupBox1.Controls.Add(this.rbSearchDirectionOut);
            this.groupBox1.Controls.Add(this.numUpDownProjectionLength);
            this.groupBox1.Controls.Add(this.gradientLabel2);
            this.groupBox1.Controls.Add(this.numUpDownSearchLength);
            this.groupBox1.Controls.Add(this.gradientLabel1);
            this.groupBox1.Controls.Add(this.numUpDownCaliperNumber);
            this.groupBox1.Controls.Add(this.gradientLabel7);
            this.groupBox1.Controls.Add(this.graLabelSearchDirection);
            this.groupBox1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 167);
            this.groupBox1.TabIndex = 53;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Caliper Setting ";
            // 
            // numUpDownIgnoreNumber
            // 
            this.numUpDownIgnoreNumber.Location = new System.Drawing.Point(163, 136);
            this.numUpDownIgnoreNumber.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numUpDownIgnoreNumber.Name = "numUpDownIgnoreNumber";
            this.numUpDownIgnoreNumber.Size = new System.Drawing.Size(149, 21);
            this.numUpDownIgnoreNumber.TabIndex = 71;
            this.numUpDownIgnoreNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientLabel9
            // 
            this.gradientLabel9.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel9.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel9.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel9.ForeColor = System.Drawing.Color.White;
            this.gradientLabel9.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel9.Location = new System.Drawing.Point(12, 133);
            this.gradientLabel9.Name = "gradientLabel9";
            this.gradientLabel9.Size = new System.Drawing.Size(145, 24);
            this.gradientLabel9.TabIndex = 70;
            this.gradientLabel9.Text = "Number of ignore";
            this.gradientLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabel6
            // 
            this.gradientLabel6.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel6.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel6.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel6.ForeColor = System.Drawing.Color.White;
            this.gradientLabel6.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel6.Location = new System.Drawing.Point(12, 103);
            this.gradientLabel6.Name = "gradientLabel6";
            this.gradientLabel6.Size = new System.Drawing.Size(145, 24);
            this.gradientLabel6.TabIndex = 68;
            this.gradientLabel6.Text = "Search direction(B→W)";
            this.gradientLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbSearchDirectionIn
            // 
            this.rbSearchDirectionIn.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSearchDirectionIn.ForeColor = System.Drawing.Color.Black;
            this.rbSearchDirectionIn.Location = new System.Drawing.Point(163, 103);
            this.rbSearchDirectionIn.Name = "rbSearchDirectionIn";
            this.rbSearchDirectionIn.Size = new System.Drawing.Size(75, 26);
            this.rbSearchDirectionIn.TabIndex = 67;
            this.rbSearchDirectionIn.Tag = "90";
            this.rbSearchDirectionIn.Text = "Inward";
            this.rbSearchDirectionIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSearchDirectionIn.UseVisualStyleBackColor = true;
            this.rbSearchDirectionIn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbSearchDirection_MouseUp);
            // 
            // rbSearchDirectionOut
            // 
            this.rbSearchDirectionOut.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbSearchDirectionOut.Checked = true;
            this.rbSearchDirectionOut.ForeColor = System.Drawing.Color.Black;
            this.rbSearchDirectionOut.Location = new System.Drawing.Point(238, 103);
            this.rbSearchDirectionOut.Name = "rbSearchDirectionOut";
            this.rbSearchDirectionOut.Size = new System.Drawing.Size(75, 26);
            this.rbSearchDirectionOut.TabIndex = 66;
            this.rbSearchDirectionOut.TabStop = true;
            this.rbSearchDirectionOut.Tag = "-90";
            this.rbSearchDirectionOut.Text = "Outward";
            this.rbSearchDirectionOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbSearchDirectionOut.UseVisualStyleBackColor = true;
            this.rbSearchDirectionOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rbSearchDirection_MouseUp);
            // 
            // numUpDownProjectionLength
            // 
            this.numUpDownProjectionLength.DecimalPlaces = 2;
            this.numUpDownProjectionLength.Location = new System.Drawing.Point(163, 78);
            this.numUpDownProjectionLength.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numUpDownProjectionLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUpDownProjectionLength.Name = "numUpDownProjectionLength";
            this.numUpDownProjectionLength.Size = new System.Drawing.Size(149, 21);
            this.numUpDownProjectionLength.TabIndex = 65;
            this.numUpDownProjectionLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownProjectionLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDownProjectionLength.ValueChanged += new System.EventHandler(this.numUpDownProjectionLength_ValueChanged);
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
            this.gradientLabel2.Location = new System.Drawing.Point(12, 75);
            this.gradientLabel2.Name = "gradientLabel2";
            this.gradientLabel2.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel2.TabIndex = 64;
            this.gradientLabel2.Text = "Projection length";
            this.gradientLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpDownSearchLength
            // 
            this.numUpDownSearchLength.DecimalPlaces = 2;
            this.numUpDownSearchLength.Location = new System.Drawing.Point(163, 50);
            this.numUpDownSearchLength.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numUpDownSearchLength.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numUpDownSearchLength.Name = "numUpDownSearchLength";
            this.numUpDownSearchLength.Size = new System.Drawing.Size(149, 21);
            this.numUpDownSearchLength.TabIndex = 63;
            this.numUpDownSearchLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownSearchLength.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numUpDownSearchLength.ValueChanged += new System.EventHandler(this.numUpDownSearchLength_ValueChanged);
            // 
            // gradientLabel1
            // 
            this.gradientLabel1.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel1.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel1.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel1.ForeColor = System.Drawing.Color.White;
            this.gradientLabel1.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel1.Location = new System.Drawing.Point(12, 47);
            this.gradientLabel1.Name = "gradientLabel1";
            this.gradientLabel1.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel1.TabIndex = 62;
            this.gradientLabel1.Text = "Search length";
            this.gradientLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpDownCaliperNumber
            // 
            this.numUpDownCaliperNumber.Location = new System.Drawing.Point(163, 22);
            this.numUpDownCaliperNumber.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numUpDownCaliperNumber.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numUpDownCaliperNumber.Name = "numUpDownCaliperNumber";
            this.numUpDownCaliperNumber.Size = new System.Drawing.Size(149, 21);
            this.numUpDownCaliperNumber.TabIndex = 61;
            this.numUpDownCaliperNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownCaliperNumber.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numUpDownCaliperNumber.ValueChanged += new System.EventHandler(this.numUpDownCaliperNumber_ValueChanged);
            // 
            // gradientLabel7
            // 
            this.gradientLabel7.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel7.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel7.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel7.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel7.ForeColor = System.Drawing.Color.White;
            this.gradientLabel7.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel7.Location = new System.Drawing.Point(12, 19);
            this.gradientLabel7.Name = "gradientLabel7";
            this.gradientLabel7.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel7.TabIndex = 60;
            this.gradientLabel7.Text = "Number of calipers";
            this.gradientLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // graLabelSearchDirection
            // 
            this.graLabelSearchDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.graLabelSearchDirection.ColorBottom = System.Drawing.Color.Empty;
            this.graLabelSearchDirection.ColorTop = System.Drawing.Color.Empty;
            this.graLabelSearchDirection.ForeColor = System.Drawing.Color.White;
            this.graLabelSearchDirection.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.graLabelSearchDirection.Location = new System.Drawing.Point(303, 104);
            this.graLabelSearchDirection.Name = "graLabelSearchDirection";
            this.graLabelSearchDirection.Size = new System.Drawing.Size(45, 24);
            this.graLabelSearchDirection.TabIndex = 69;
            this.graLabelSearchDirection.Text = "-90";
            this.graLabelSearchDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.graLabelSearchDirection.Visible = false;
            // 
            // numUpDownFilterHalfSizePixels
            // 
            this.numUpDownFilterHalfSizePixels.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.numUpDownFilterHalfSizePixels.Location = new System.Drawing.Point(500, 70);
            this.numUpDownFilterHalfSizePixels.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numUpDownFilterHalfSizePixels.Name = "numUpDownFilterHalfSizePixels";
            this.numUpDownFilterHalfSizePixels.Size = new System.Drawing.Size(79, 21);
            this.numUpDownFilterHalfSizePixels.TabIndex = 75;
            this.numUpDownFilterHalfSizePixels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownFilterHalfSizePixels.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numUpDownContrastThreshold
            // 
            this.numUpDownContrastThreshold.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.numUpDownContrastThreshold.Location = new System.Drawing.Point(500, 40);
            this.numUpDownContrastThreshold.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numUpDownContrastThreshold.Name = "numUpDownContrastThreshold";
            this.numUpDownContrastThreshold.Size = new System.Drawing.Size(79, 21);
            this.numUpDownContrastThreshold.TabIndex = 73;
            this.numUpDownContrastThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownContrastThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numUpDownEndY);
            this.groupBox2.Controls.Add(this.gradientLabel8);
            this.groupBox2.Controls.Add(this.numUpDownEndX);
            this.groupBox2.Controls.Add(this.gradientLabel5);
            this.groupBox2.Controls.Add(this.numUpDownStartY);
            this.groupBox2.Controls.Add(this.gradientLabel4);
            this.groupBox2.Controls.Add(this.numUpDownStartX);
            this.groupBox2.Controls.Add(this.gradientLabel3);
            this.groupBox2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(3, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 141);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Expected line segment ";
            // 
            // numUpDownEndY
            // 
            this.numUpDownEndY.DecimalPlaces = 2;
            this.numUpDownEndY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numUpDownEndY.Location = new System.Drawing.Point(167, 107);
            this.numUpDownEndY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUpDownEndY.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numUpDownEndY.Name = "numUpDownEndY";
            this.numUpDownEndY.Size = new System.Drawing.Size(149, 21);
            this.numUpDownEndY.TabIndex = 69;
            this.numUpDownEndY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownEndY.ValueChanged += new System.EventHandler(this.numUpDownEndY_ValueChanged);
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
            this.gradientLabel8.Location = new System.Drawing.Point(15, 104);
            this.gradientLabel8.Name = "gradientLabel8";
            this.gradientLabel8.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel8.TabIndex = 68;
            this.gradientLabel8.Text = "End Y";
            this.gradientLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpDownEndX
            // 
            this.numUpDownEndX.DecimalPlaces = 2;
            this.numUpDownEndX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numUpDownEndX.Location = new System.Drawing.Point(167, 78);
            this.numUpDownEndX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUpDownEndX.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numUpDownEndX.Name = "numUpDownEndX";
            this.numUpDownEndX.Size = new System.Drawing.Size(149, 21);
            this.numUpDownEndX.TabIndex = 67;
            this.numUpDownEndX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownEndX.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numUpDownEndX.ValueChanged += new System.EventHandler(this.numUpDownEndX_ValueChanged);
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
            this.gradientLabel5.Location = new System.Drawing.Point(15, 76);
            this.gradientLabel5.Name = "gradientLabel5";
            this.gradientLabel5.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel5.TabIndex = 66;
            this.gradientLabel5.Text = "End X";
            this.gradientLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpDownStartY
            // 
            this.numUpDownStartY.DecimalPlaces = 2;
            this.numUpDownStartY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numUpDownStartY.Location = new System.Drawing.Point(167, 50);
            this.numUpDownStartY.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUpDownStartY.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numUpDownStartY.Name = "numUpDownStartY";
            this.numUpDownStartY.Size = new System.Drawing.Size(149, 21);
            this.numUpDownStartY.TabIndex = 65;
            this.numUpDownStartY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownStartY.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDownStartY.ValueChanged += new System.EventHandler(this.numUpDownStartY_ValueChanged);
            // 
            // gradientLabel4
            // 
            this.gradientLabel4.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gradientLabel4.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel4.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.gradientLabel4.ForeColor = System.Drawing.Color.White;
            this.gradientLabel4.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel4.Location = new System.Drawing.Point(15, 48);
            this.gradientLabel4.Name = "gradientLabel4";
            this.gradientLabel4.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel4.TabIndex = 64;
            this.gradientLabel4.Text = " Start Y";
            this.gradientLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numUpDownStartX
            // 
            this.numUpDownStartX.DecimalPlaces = 2;
            this.numUpDownStartX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numUpDownStartX.Location = new System.Drawing.Point(167, 22);
            this.numUpDownStartX.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUpDownStartX.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.numUpDownStartX.Name = "numUpDownStartX";
            this.numUpDownStartX.Size = new System.Drawing.Size(149, 21);
            this.numUpDownStartX.TabIndex = 63;
            this.numUpDownStartX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDownStartX.Value = new decimal(new int[] {
            15000,
            0,
            0,
            131072});
            this.numUpDownStartX.ValueChanged += new System.EventHandler(this.numUpDownStartX_ValueChanged);
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
            this.gradientLabel3.Location = new System.Drawing.Point(15, 19);
            this.gradientLabel3.Name = "gradientLabel3";
            this.gradientLabel3.Size = new System.Drawing.Size(144, 24);
            this.gradientLabel3.TabIndex = 62;
            this.gradientLabel3.Text = " Start X";
            this.gradientLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDrawCaliper
            // 
            this.btnDrawCaliper.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDrawCaliper.ForeColor = System.Drawing.Color.Black;
            this.btnDrawCaliper.Location = new System.Drawing.Point(342, 314);
            this.btnDrawCaliper.Name = "btnDrawCaliper";
            this.btnDrawCaliper.Size = new System.Drawing.Size(117, 34);
            this.btnDrawCaliper.TabIndex = 72;
            this.btnDrawCaliper.Text = "Draw Caliper";
            this.btnDrawCaliper.UseVisualStyleBackColor = true;
            this.btnDrawCaliper.Click += new System.EventHandler(this.btnDrawCaliper_Click);
            // 
            // btnSetting
            // 
            this.btnSetting.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetting.ForeColor = System.Drawing.Color.Black;
            this.btnSetting.Location = new System.Drawing.Point(461, 314);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(117, 34);
            this.btnSetting.TabIndex = 71;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // ckUseAlignment
            // 
            this.ckUseAlignment.AutoSize = true;
            this.ckUseAlignment.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.ckUseAlignment.ForeColor = System.Drawing.Color.White;
            this.ckUseAlignment.Location = new System.Drawing.Point(346, 288);
            this.ckUseAlignment.Name = "ckUseAlignment";
            this.ckUseAlignment.Size = new System.Drawing.Size(107, 18);
            this.ckUseAlignment.TabIndex = 73;
            this.ckUseAlignment.Text = "Use Alignment";
            this.ckUseAlignment.UseVisualStyleBackColor = true;
            // 
            // gradientLabel11
            // 
            this.gradientLabel11.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel11.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel11.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel11.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.gradientLabel11.ForeColor = System.Drawing.Color.White;
            this.gradientLabel11.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel11.Location = new System.Drawing.Point(349, 67);
            this.gradientLabel11.Name = "gradientLabel11";
            this.gradientLabel11.Size = new System.Drawing.Size(145, 24);
            this.gradientLabel11.TabIndex = 74;
            this.gradientLabel11.Text = "Filter half size pixels";
            this.gradientLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradientLabel10
            // 
            this.gradientLabel10.BackColor = System.Drawing.Color.SteelBlue;
            this.gradientLabel10.ColorBottom = System.Drawing.Color.Empty;
            this.gradientLabel10.ColorTop = System.Drawing.Color.Empty;
            this.gradientLabel10.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.gradientLabel10.ForeColor = System.Drawing.Color.White;
            this.gradientLabel10.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.gradientLabel10.Location = new System.Drawing.Point(349, 38);
            this.gradientLabel10.Name = "gradientLabel10";
            this.gradientLabel10.Size = new System.Drawing.Size(145, 24);
            this.gradientLabel10.TabIndex = 72;
            this.gradientLabel10.Text = "Contrast Threshold";
            this.gradientLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.White;
            this.labelTitle.ColorBottom = System.Drawing.Color.White;
            this.labelTitle.ColorTop = System.Drawing.Color.SteelBlue;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.labelTitle.Location = new System.Drawing.Point(2, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(580, 28);
            this.labelTitle.TabIndex = 13;
            this.labelTitle.Text = " Line Find Teaching Window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ucCogLineFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.Controls.Add(this.numUpDownFilterHalfSizePixels);
            this.Controls.Add(this.ckUseAlignment);
            this.Controls.Add(this.gradientLabel11);
            this.Controls.Add(this.btnDrawCaliper);
            this.Controls.Add(this.numUpDownContrastThreshold);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.gradientLabel10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelTitle);
            this.Name = "ucCogLineFind";
            this.Size = new System.Drawing.Size(583, 358);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownIgnoreNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownProjectionLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSearchLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownCaliperNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownFilterHalfSizePixels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownContrastThreshold)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEndY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEndX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownStartY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownStartX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControl.GradientLabel labelTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControl.GradientLabel gradientLabel6;
        private System.Windows.Forms.NumericUpDown numUpDownProjectionLength;
        private CustomControl.GradientLabel gradientLabel2;
        private System.Windows.Forms.NumericUpDown numUpDownSearchLength;
        private CustomControl.GradientLabel gradientLabel1;
        private System.Windows.Forms.NumericUpDown numUpDownCaliperNumber;
        private CustomControl.GradientLabel gradientLabel7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numUpDownEndY;
        private CustomControl.GradientLabel gradientLabel8;
        private System.Windows.Forms.NumericUpDown numUpDownEndX;
        private CustomControl.GradientLabel gradientLabel5;
        private System.Windows.Forms.NumericUpDown numUpDownStartY;
        private CustomControl.GradientLabel gradientLabel4;
        private System.Windows.Forms.NumericUpDown numUpDownStartX;
        private CustomControl.GradientLabel gradientLabel3;
        private System.Windows.Forms.NumericUpDown numUpDownIgnoreNumber;
        private CustomControl.GradientLabel gradientLabel9;
        private System.Windows.Forms.RadioButton rbSearchDirectionIn;
        private System.Windows.Forms.RadioButton rbSearchDirectionOut;
        private CustomControl.GradientLabel graLabelSearchDirection;
        private System.Windows.Forms.Button btnDrawCaliper;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.NumericUpDown numUpDownFilterHalfSizePixels;
        private CustomControl.GradientLabel gradientLabel11;
        private System.Windows.Forms.NumericUpDown numUpDownContrastThreshold;
        private CustomControl.GradientLabel gradientLabel10;
        private System.Windows.Forms.CheckBox ckUseAlignment;
    }
}
