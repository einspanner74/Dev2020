namespace DIOControlManager
{
    partial class DIOControlWindow
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
            this.btnTrigger = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnInput4 = new CPressingButton.PressButton();
            this.btnInput3 = new CPressingButton.PressButton();
            this.btnInput2 = new CPressingButton.PressButton();
            this.btnInput1 = new CPressingButton.PressButton();
            this.btnInput0 = new CPressingButton.PressButton();
            this.btnInput5 = new CPressingButton.PressButton();
            this.btnInput7 = new CPressingButton.PressButton();
            this.btnInput6 = new CPressingButton.PressButton();
            this.btnOutput7 = new CPressingButton.PressButton();
            this.btnOutput6 = new CPressingButton.PressButton();
            this.btnOutput5 = new CPressingButton.PressButton();
            this.btnOutput4 = new CPressingButton.PressButton();
            this.btnOutput3 = new CPressingButton.PressButton();
            this.btnOutput2 = new CPressingButton.PressButton();
            this.btnOutput1 = new CPressingButton.PressButton();
            this.btnOutput0 = new CPressingButton.PressButton();
            this.labelExit = new System.Windows.Forms.Label();
            this.btnOutput15 = new CPressingButton.PressButton();
            this.btnOutput14 = new CPressingButton.PressButton();
            this.btnOutput13 = new CPressingButton.PressButton();
            this.btnOutput12 = new CPressingButton.PressButton();
            this.btnOutput11 = new CPressingButton.PressButton();
            this.btnOutput10 = new CPressingButton.PressButton();
            this.btnOutput9 = new CPressingButton.PressButton();
            this.btnOutput8 = new CPressingButton.PressButton();
            this.btnInput15 = new CPressingButton.PressButton();
            this.btnInput14 = new CPressingButton.PressButton();
            this.btnInput13 = new CPressingButton.PressButton();
            this.btnInput12 = new CPressingButton.PressButton();
            this.btnInput11 = new CPressingButton.PressButton();
            this.btnInput10 = new CPressingButton.PressButton();
            this.btnInput9 = new CPressingButton.PressButton();
            this.btnInput8 = new CPressingButton.PressButton();
            this.labelInputTitle = new CustomControl.GradientLabel();
            this.labelOutputTitle = new CustomControl.GradientLabel();
            this.btnInput31 = new CPressingButton.PressButton();
            this.btnInput30 = new CPressingButton.PressButton();
            this.btnInput29 = new CPressingButton.PressButton();
            this.btnInput28 = new CPressingButton.PressButton();
            this.btnInput27 = new CPressingButton.PressButton();
            this.btnInput26 = new CPressingButton.PressButton();
            this.btnInput25 = new CPressingButton.PressButton();
            this.btnInput24 = new CPressingButton.PressButton();
            this.btnInput23 = new CPressingButton.PressButton();
            this.btnInput22 = new CPressingButton.PressButton();
            this.btnInput21 = new CPressingButton.PressButton();
            this.btnInput20 = new CPressingButton.PressButton();
            this.btnInput19 = new CPressingButton.PressButton();
            this.btnInput18 = new CPressingButton.PressButton();
            this.btnInput17 = new CPressingButton.PressButton();
            this.btnInput16 = new CPressingButton.PressButton();
            this.btnOutput31 = new CPressingButton.PressButton();
            this.btnOutput30 = new CPressingButton.PressButton();
            this.btnOutput29 = new CPressingButton.PressButton();
            this.btnOutput28 = new CPressingButton.PressButton();
            this.btnOutput27 = new CPressingButton.PressButton();
            this.btnOutput26 = new CPressingButton.PressButton();
            this.btnOutput25 = new CPressingButton.PressButton();
            this.btnOutput24 = new CPressingButton.PressButton();
            this.btnOutput23 = new CPressingButton.PressButton();
            this.btnOutput22 = new CPressingButton.PressButton();
            this.btnOutput21 = new CPressingButton.PressButton();
            this.btnOutput20 = new CPressingButton.PressButton();
            this.btnOutput19 = new CPressingButton.PressButton();
            this.btnOutput18 = new CPressingButton.PressButton();
            this.btnOutput17 = new CPressingButton.PressButton();
            this.btnOutput16 = new CPressingButton.PressButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.btnRequest = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(6, 324);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(87, 25);
            this.btnTrigger.TabIndex = 0;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.Visible = false;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(728, 30);
            this.labelTitle.TabIndex = 11;
            this.labelTitle.Text = " Digital I/O Window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // btnInput4
            // 
            this.btnInput4.BackColor = System.Drawing.Color.Maroon;
            this.btnInput4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput4.ForeColor = System.Drawing.Color.White;
            this.btnInput4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput4.Location = new System.Drawing.Point(4, 181);
            this.btnInput4.Name = "btnInput4";
            this.btnInput4.Size = new System.Drawing.Size(173, 32);
            this.btnInput4.TabIndex = 266;
            this.btnInput4.Tag = "4";
            this.btnInput4.Text = "DI4";
            this.btnInput4.UseVisualStyleBackColor = false;
            this.btnInput4.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput3
            // 
            this.btnInput3.BackColor = System.Drawing.Color.Maroon;
            this.btnInput3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput3.ForeColor = System.Drawing.Color.White;
            this.btnInput3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput3.Location = new System.Drawing.Point(4, 145);
            this.btnInput3.Name = "btnInput3";
            this.btnInput3.Size = new System.Drawing.Size(173, 32);
            this.btnInput3.TabIndex = 265;
            this.btnInput3.Tag = "3";
            this.btnInput3.Text = "DI3";
            this.btnInput3.UseVisualStyleBackColor = false;
            this.btnInput3.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput2
            // 
            this.btnInput2.BackColor = System.Drawing.Color.Maroon;
            this.btnInput2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput2.ForeColor = System.Drawing.Color.White;
            this.btnInput2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput2.Location = new System.Drawing.Point(4, 109);
            this.btnInput2.Name = "btnInput2";
            this.btnInput2.Size = new System.Drawing.Size(173, 32);
            this.btnInput2.TabIndex = 264;
            this.btnInput2.Tag = "2";
            this.btnInput2.Text = "DI2";
            this.btnInput2.UseVisualStyleBackColor = false;
            this.btnInput2.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput1
            // 
            this.btnInput1.BackColor = System.Drawing.Color.Maroon;
            this.btnInput1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput1.ForeColor = System.Drawing.Color.White;
            this.btnInput1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput1.Location = new System.Drawing.Point(4, 73);
            this.btnInput1.Name = "btnInput1";
            this.btnInput1.Size = new System.Drawing.Size(173, 32);
            this.btnInput1.TabIndex = 263;
            this.btnInput1.Tag = "1";
            this.btnInput1.Text = "DI1";
            this.btnInput1.UseVisualStyleBackColor = false;
            this.btnInput1.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput0
            // 
            this.btnInput0.BackColor = System.Drawing.Color.Maroon;
            this.btnInput0.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput0.ForeColor = System.Drawing.Color.White;
            this.btnInput0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput0.Location = new System.Drawing.Point(4, 37);
            this.btnInput0.Name = "btnInput0";
            this.btnInput0.Size = new System.Drawing.Size(173, 32);
            this.btnInput0.TabIndex = 262;
            this.btnInput0.Tag = "0";
            this.btnInput0.Text = "Live";
            this.btnInput0.UseVisualStyleBackColor = false;
            this.btnInput0.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput5
            // 
            this.btnInput5.BackColor = System.Drawing.Color.Maroon;
            this.btnInput5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput5.ForeColor = System.Drawing.Color.White;
            this.btnInput5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput5.Location = new System.Drawing.Point(4, 217);
            this.btnInput5.Name = "btnInput5";
            this.btnInput5.Size = new System.Drawing.Size(173, 32);
            this.btnInput5.TabIndex = 272;
            this.btnInput5.Tag = "5";
            this.btnInput5.Text = "DI5";
            this.btnInput5.UseVisualStyleBackColor = false;
            this.btnInput5.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput7
            // 
            this.btnInput7.BackColor = System.Drawing.Color.Maroon;
            this.btnInput7.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput7.ForeColor = System.Drawing.Color.White;
            this.btnInput7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput7.Location = new System.Drawing.Point(4, 289);
            this.btnInput7.Name = "btnInput7";
            this.btnInput7.Size = new System.Drawing.Size(173, 32);
            this.btnInput7.TabIndex = 274;
            this.btnInput7.Tag = "7";
            this.btnInput7.Text = "DI7";
            this.btnInput7.UseVisualStyleBackColor = false;
            this.btnInput7.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput6
            // 
            this.btnInput6.BackColor = System.Drawing.Color.Maroon;
            this.btnInput6.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput6.ForeColor = System.Drawing.Color.White;
            this.btnInput6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput6.Location = new System.Drawing.Point(4, 253);
            this.btnInput6.Name = "btnInput6";
            this.btnInput6.Size = new System.Drawing.Size(173, 32);
            this.btnInput6.TabIndex = 273;
            this.btnInput6.Tag = "6";
            this.btnInput6.Text = "DI6";
            this.btnInput6.UseVisualStyleBackColor = false;
            this.btnInput6.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnOutput7
            // 
            this.btnOutput7.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput7.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput7.ForeColor = System.Drawing.Color.White;
            this.btnOutput7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput7.Location = new System.Drawing.Point(4, 638);
            this.btnOutput7.Name = "btnOutput7";
            this.btnOutput7.Size = new System.Drawing.Size(173, 32);
            this.btnOutput7.TabIndex = 282;
            this.btnOutput7.Tag = "7";
            this.btnOutput7.Text = "DO7";
            this.btnOutput7.UseVisualStyleBackColor = false;
            this.btnOutput7.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput7.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput6
            // 
            this.btnOutput6.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput6.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput6.ForeColor = System.Drawing.Color.White;
            this.btnOutput6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput6.Location = new System.Drawing.Point(4, 602);
            this.btnOutput6.Name = "btnOutput6";
            this.btnOutput6.Size = new System.Drawing.Size(173, 32);
            this.btnOutput6.TabIndex = 281;
            this.btnOutput6.Tag = "6";
            this.btnOutput6.Text = "DO6";
            this.btnOutput6.UseVisualStyleBackColor = false;
            this.btnOutput6.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput6.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput5
            // 
            this.btnOutput5.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput5.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput5.ForeColor = System.Drawing.Color.White;
            this.btnOutput5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput5.Location = new System.Drawing.Point(4, 566);
            this.btnOutput5.Name = "btnOutput5";
            this.btnOutput5.Size = new System.Drawing.Size(173, 32);
            this.btnOutput5.TabIndex = 280;
            this.btnOutput5.Tag = "5";
            this.btnOutput5.Text = "DO5";
            this.btnOutput5.UseVisualStyleBackColor = false;
            this.btnOutput5.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput5.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput4
            // 
            this.btnOutput4.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput4.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput4.ForeColor = System.Drawing.Color.White;
            this.btnOutput4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput4.Location = new System.Drawing.Point(4, 530);
            this.btnOutput4.Name = "btnOutput4";
            this.btnOutput4.Size = new System.Drawing.Size(173, 32);
            this.btnOutput4.TabIndex = 279;
            this.btnOutput4.Tag = "4";
            this.btnOutput4.Text = "DO4";
            this.btnOutput4.UseVisualStyleBackColor = false;
            this.btnOutput4.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput4.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput3
            // 
            this.btnOutput3.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput3.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput3.ForeColor = System.Drawing.Color.White;
            this.btnOutput3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput3.Location = new System.Drawing.Point(4, 494);
            this.btnOutput3.Name = "btnOutput3";
            this.btnOutput3.Size = new System.Drawing.Size(173, 32);
            this.btnOutput3.TabIndex = 278;
            this.btnOutput3.Tag = "3";
            this.btnOutput3.Text = "Alarm";
            this.btnOutput3.UseVisualStyleBackColor = false;
            this.btnOutput3.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput3.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput2
            // 
            this.btnOutput2.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput2.ForeColor = System.Drawing.Color.White;
            this.btnOutput2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput2.Location = new System.Drawing.Point(4, 458);
            this.btnOutput2.Name = "btnOutput2";
            this.btnOutput2.Size = new System.Drawing.Size(173, 32);
            this.btnOutput2.TabIndex = 277;
            this.btnOutput2.Tag = "2";
            this.btnOutput2.Text = "Manual Mode";
            this.btnOutput2.UseVisualStyleBackColor = false;
            this.btnOutput2.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput2.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput1
            // 
            this.btnOutput1.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput1.ForeColor = System.Drawing.Color.White;
            this.btnOutput1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput1.Location = new System.Drawing.Point(4, 422);
            this.btnOutput1.Name = "btnOutput1";
            this.btnOutput1.Size = new System.Drawing.Size(173, 32);
            this.btnOutput1.TabIndex = 276;
            this.btnOutput1.Tag = "1";
            this.btnOutput1.Text = "Auto Mode";
            this.btnOutput1.UseVisualStyleBackColor = false;
            this.btnOutput1.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput1.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput0
            // 
            this.btnOutput0.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput0.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput0.ForeColor = System.Drawing.Color.White;
            this.btnOutput0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput0.Location = new System.Drawing.Point(4, 386);
            this.btnOutput0.Name = "btnOutput0";
            this.btnOutput0.Size = new System.Drawing.Size(173, 32);
            this.btnOutput0.TabIndex = 275;
            this.btnOutput0.Tag = "0";
            this.btnOutput0.Text = "Live";
            this.btnOutput0.UseVisualStyleBackColor = false;
            this.btnOutput0.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput0.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // labelExit
            // 
            this.labelExit.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelExit.ForeColor = System.Drawing.Color.White;
            this.labelExit.Location = new System.Drawing.Point(700, 3);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(25, 24);
            this.labelExit.TabIndex = 283;
            this.labelExit.Text = "X";
            this.labelExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            // 
            // btnOutput15
            // 
            this.btnOutput15.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput15.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput15.ForeColor = System.Drawing.Color.White;
            this.btnOutput15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput15.Location = new System.Drawing.Point(183, 638);
            this.btnOutput15.Name = "btnOutput15";
            this.btnOutput15.Size = new System.Drawing.Size(173, 32);
            this.btnOutput15.TabIndex = 291;
            this.btnOutput15.Tag = "15";
            this.btnOutput15.Text = "DO15";
            this.btnOutput15.UseVisualStyleBackColor = false;
            this.btnOutput15.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput15.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput14
            // 
            this.btnOutput14.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput14.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput14.ForeColor = System.Drawing.Color.White;
            this.btnOutput14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput14.Location = new System.Drawing.Point(183, 602);
            this.btnOutput14.Name = "btnOutput14";
            this.btnOutput14.Size = new System.Drawing.Size(173, 32);
            this.btnOutput14.TabIndex = 290;
            this.btnOutput14.Tag = "14";
            this.btnOutput14.Text = "DO14";
            this.btnOutput14.UseVisualStyleBackColor = false;
            this.btnOutput14.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput14.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput13
            // 
            this.btnOutput13.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput13.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput13.ForeColor = System.Drawing.Color.White;
            this.btnOutput13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput13.Location = new System.Drawing.Point(183, 566);
            this.btnOutput13.Name = "btnOutput13";
            this.btnOutput13.Size = new System.Drawing.Size(173, 32);
            this.btnOutput13.TabIndex = 289;
            this.btnOutput13.Tag = "13";
            this.btnOutput13.Text = "DO13";
            this.btnOutput13.UseVisualStyleBackColor = false;
            this.btnOutput13.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput13.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput12
            // 
            this.btnOutput12.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput12.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput12.ForeColor = System.Drawing.Color.White;
            this.btnOutput12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput12.Location = new System.Drawing.Point(183, 530);
            this.btnOutput12.Name = "btnOutput12";
            this.btnOutput12.Size = new System.Drawing.Size(173, 32);
            this.btnOutput12.TabIndex = 288;
            this.btnOutput12.Tag = "12";
            this.btnOutput12.Text = "DO12";
            this.btnOutput12.UseVisualStyleBackColor = false;
            this.btnOutput12.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput12.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput11
            // 
            this.btnOutput11.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput11.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput11.ForeColor = System.Drawing.Color.White;
            this.btnOutput11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput11.Location = new System.Drawing.Point(183, 494);
            this.btnOutput11.Name = "btnOutput11";
            this.btnOutput11.Size = new System.Drawing.Size(173, 32);
            this.btnOutput11.TabIndex = 287;
            this.btnOutput11.Tag = "11";
            this.btnOutput11.Text = "DO11";
            this.btnOutput11.UseVisualStyleBackColor = false;
            this.btnOutput11.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput11.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput10
            // 
            this.btnOutput10.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput10.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput10.ForeColor = System.Drawing.Color.White;
            this.btnOutput10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput10.Location = new System.Drawing.Point(183, 458);
            this.btnOutput10.Name = "btnOutput10";
            this.btnOutput10.Size = new System.Drawing.Size(173, 32);
            this.btnOutput10.TabIndex = 286;
            this.btnOutput10.Tag = "10";
            this.btnOutput10.Text = "DO10";
            this.btnOutput10.UseVisualStyleBackColor = false;
            this.btnOutput10.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput10.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput9
            // 
            this.btnOutput9.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput9.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput9.ForeColor = System.Drawing.Color.White;
            this.btnOutput9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput9.Location = new System.Drawing.Point(183, 422);
            this.btnOutput9.Name = "btnOutput9";
            this.btnOutput9.Size = new System.Drawing.Size(173, 32);
            this.btnOutput9.TabIndex = 285;
            this.btnOutput9.Tag = "9";
            this.btnOutput9.Text = "Vision1 Complete";
            this.btnOutput9.UseVisualStyleBackColor = false;
            this.btnOutput9.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput9.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput8
            // 
            this.btnOutput8.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput8.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput8.ForeColor = System.Drawing.Color.White;
            this.btnOutput8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput8.Location = new System.Drawing.Point(183, 386);
            this.btnOutput8.Name = "btnOutput8";
            this.btnOutput8.Size = new System.Drawing.Size(173, 32);
            this.btnOutput8.TabIndex = 284;
            this.btnOutput8.Tag = "8";
            this.btnOutput8.Text = "Vision1 Ready";
            this.btnOutput8.UseVisualStyleBackColor = false;
            this.btnOutput8.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput8.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnInput15
            // 
            this.btnInput15.BackColor = System.Drawing.Color.Maroon;
            this.btnInput15.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput15.ForeColor = System.Drawing.Color.White;
            this.btnInput15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput15.Location = new System.Drawing.Point(183, 289);
            this.btnInput15.Name = "btnInput15";
            this.btnInput15.Size = new System.Drawing.Size(173, 32);
            this.btnInput15.TabIndex = 299;
            this.btnInput15.Tag = "15";
            this.btnInput15.Text = "DI15";
            this.btnInput15.UseVisualStyleBackColor = false;
            this.btnInput15.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput14
            // 
            this.btnInput14.BackColor = System.Drawing.Color.Maroon;
            this.btnInput14.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput14.ForeColor = System.Drawing.Color.White;
            this.btnInput14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput14.Location = new System.Drawing.Point(183, 253);
            this.btnInput14.Name = "btnInput14";
            this.btnInput14.Size = new System.Drawing.Size(173, 32);
            this.btnInput14.TabIndex = 298;
            this.btnInput14.Tag = "14";
            this.btnInput14.Text = "DI14";
            this.btnInput14.UseVisualStyleBackColor = false;
            this.btnInput14.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput13
            // 
            this.btnInput13.BackColor = System.Drawing.Color.Maroon;
            this.btnInput13.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput13.ForeColor = System.Drawing.Color.White;
            this.btnInput13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput13.Location = new System.Drawing.Point(183, 217);
            this.btnInput13.Name = "btnInput13";
            this.btnInput13.Size = new System.Drawing.Size(173, 32);
            this.btnInput13.TabIndex = 297;
            this.btnInput13.Tag = "13";
            this.btnInput13.Text = "DI13";
            this.btnInput13.UseVisualStyleBackColor = false;
            this.btnInput13.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput12
            // 
            this.btnInput12.BackColor = System.Drawing.Color.Maroon;
            this.btnInput12.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput12.ForeColor = System.Drawing.Color.White;
            this.btnInput12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput12.Location = new System.Drawing.Point(183, 181);
            this.btnInput12.Name = "btnInput12";
            this.btnInput12.Size = new System.Drawing.Size(173, 32);
            this.btnInput12.TabIndex = 296;
            this.btnInput12.Tag = "12";
            this.btnInput12.Text = "DI12";
            this.btnInput12.UseVisualStyleBackColor = false;
            this.btnInput12.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput11
            // 
            this.btnInput11.BackColor = System.Drawing.Color.Maroon;
            this.btnInput11.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput11.ForeColor = System.Drawing.Color.White;
            this.btnInput11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput11.Location = new System.Drawing.Point(183, 145);
            this.btnInput11.Name = "btnInput11";
            this.btnInput11.Size = new System.Drawing.Size(173, 32);
            this.btnInput11.TabIndex = 295;
            this.btnInput11.Tag = "11";
            this.btnInput11.Text = "DI11";
            this.btnInput11.UseVisualStyleBackColor = false;
            this.btnInput11.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput10
            // 
            this.btnInput10.BackColor = System.Drawing.Color.Maroon;
            this.btnInput10.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput10.ForeColor = System.Drawing.Color.White;
            this.btnInput10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput10.Location = new System.Drawing.Point(183, 109);
            this.btnInput10.Name = "btnInput10";
            this.btnInput10.Size = new System.Drawing.Size(173, 32);
            this.btnInput10.TabIndex = 294;
            this.btnInput10.Tag = "10";
            this.btnInput10.Text = "DI10";
            this.btnInput10.UseVisualStyleBackColor = false;
            this.btnInput10.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput9
            // 
            this.btnInput9.BackColor = System.Drawing.Color.Maroon;
            this.btnInput9.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput9.ForeColor = System.Drawing.Color.White;
            this.btnInput9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput9.Location = new System.Drawing.Point(183, 73);
            this.btnInput9.Name = "btnInput9";
            this.btnInput9.Size = new System.Drawing.Size(173, 32);
            this.btnInput9.TabIndex = 293;
            this.btnInput9.Tag = "9";
            this.btnInput9.Text = "Vision1 Data Request";
            this.btnInput9.UseVisualStyleBackColor = false;
            this.btnInput9.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput8
            // 
            this.btnInput8.BackColor = System.Drawing.Color.Maroon;
            this.btnInput8.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput8.ForeColor = System.Drawing.Color.White;
            this.btnInput8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput8.Location = new System.Drawing.Point(183, 37);
            this.btnInput8.Name = "btnInput8";
            this.btnInput8.Size = new System.Drawing.Size(173, 32);
            this.btnInput8.TabIndex = 292;
            this.btnInput8.Tag = "8";
            this.btnInput8.Text = "Vision1 Reset";
            this.btnInput8.UseVisualStyleBackColor = false;
            this.btnInput8.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // labelInputTitle
            // 
            this.labelInputTitle.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelInputTitle.ColorBottom = System.Drawing.Color.LightSteelBlue;
            this.labelInputTitle.ColorTop = System.Drawing.Color.LightSlateGray;
            this.labelInputTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelInputTitle.ForeColor = System.Drawing.Color.White;
            this.labelInputTitle.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelInputTitle.Location = new System.Drawing.Point(3, 3);
            this.labelInputTitle.Name = "labelInputTitle";
            this.labelInputTitle.Size = new System.Drawing.Size(724, 30);
            this.labelInputTitle.TabIndex = 300;
            this.labelInputTitle.Text = " Input I/O Signal (PLC -> VISION)";
            this.labelInputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOutputTitle
            // 
            this.labelOutputTitle.BackColor = System.Drawing.Color.LightSlateGray;
            this.labelOutputTitle.ColorBottom = System.Drawing.Color.LightSteelBlue;
            this.labelOutputTitle.ColorTop = System.Drawing.Color.LightSlateGray;
            this.labelOutputTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelOutputTitle.ForeColor = System.Drawing.Color.White;
            this.labelOutputTitle.GradientDirection = CustomControl.GradientLabel.Direction.Horizon;
            this.labelOutputTitle.Location = new System.Drawing.Point(3, 352);
            this.labelOutputTitle.Name = "labelOutputTitle";
            this.labelOutputTitle.Size = new System.Drawing.Size(724, 30);
            this.labelOutputTitle.TabIndex = 301;
            this.labelOutputTitle.Text = " Output I/O Signal (VISION -> PLC)";
            this.labelOutputTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnInput31
            // 
            this.btnInput31.BackColor = System.Drawing.Color.Maroon;
            this.btnInput31.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput31.ForeColor = System.Drawing.Color.White;
            this.btnInput31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput31.Location = new System.Drawing.Point(553, 289);
            this.btnInput31.Name = "btnInput31";
            this.btnInput31.Size = new System.Drawing.Size(173, 32);
            this.btnInput31.TabIndex = 317;
            this.btnInput31.Tag = "4";
            this.btnInput31.Text = "DI31";
            this.btnInput31.UseVisualStyleBackColor = false;
            this.btnInput31.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput30
            // 
            this.btnInput30.BackColor = System.Drawing.Color.Maroon;
            this.btnInput30.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput30.ForeColor = System.Drawing.Color.White;
            this.btnInput30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput30.Location = new System.Drawing.Point(553, 253);
            this.btnInput30.Name = "btnInput30";
            this.btnInput30.Size = new System.Drawing.Size(173, 32);
            this.btnInput30.TabIndex = 316;
            this.btnInput30.Tag = "4";
            this.btnInput30.Text = "DI30";
            this.btnInput30.UseVisualStyleBackColor = false;
            this.btnInput30.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput29
            // 
            this.btnInput29.BackColor = System.Drawing.Color.Maroon;
            this.btnInput29.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput29.ForeColor = System.Drawing.Color.White;
            this.btnInput29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput29.Location = new System.Drawing.Point(553, 217);
            this.btnInput29.Name = "btnInput29";
            this.btnInput29.Size = new System.Drawing.Size(173, 32);
            this.btnInput29.TabIndex = 315;
            this.btnInput29.Tag = "6";
            this.btnInput29.Text = "DI29";
            this.btnInput29.UseVisualStyleBackColor = false;
            this.btnInput29.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput28
            // 
            this.btnInput28.BackColor = System.Drawing.Color.Maroon;
            this.btnInput28.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput28.ForeColor = System.Drawing.Color.White;
            this.btnInput28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput28.Location = new System.Drawing.Point(553, 181);
            this.btnInput28.Name = "btnInput28";
            this.btnInput28.Size = new System.Drawing.Size(173, 32);
            this.btnInput28.TabIndex = 314;
            this.btnInput28.Tag = "4";
            this.btnInput28.Text = "DI28";
            this.btnInput28.UseVisualStyleBackColor = false;
            this.btnInput28.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput27
            // 
            this.btnInput27.BackColor = System.Drawing.Color.Maroon;
            this.btnInput27.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput27.ForeColor = System.Drawing.Color.White;
            this.btnInput27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput27.Location = new System.Drawing.Point(553, 145);
            this.btnInput27.Name = "btnInput27";
            this.btnInput27.Size = new System.Drawing.Size(173, 32);
            this.btnInput27.TabIndex = 313;
            this.btnInput27.Tag = "3";
            this.btnInput27.Text = "DI27";
            this.btnInput27.UseVisualStyleBackColor = false;
            this.btnInput27.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput26
            // 
            this.btnInput26.BackColor = System.Drawing.Color.Maroon;
            this.btnInput26.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput26.ForeColor = System.Drawing.Color.White;
            this.btnInput26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput26.Location = new System.Drawing.Point(553, 109);
            this.btnInput26.Name = "btnInput26";
            this.btnInput26.Size = new System.Drawing.Size(173, 32);
            this.btnInput26.TabIndex = 312;
            this.btnInput26.Tag = "2";
            this.btnInput26.Text = "DI26";
            this.btnInput26.UseVisualStyleBackColor = false;
            this.btnInput26.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput25
            // 
            this.btnInput25.BackColor = System.Drawing.Color.Maroon;
            this.btnInput25.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput25.ForeColor = System.Drawing.Color.White;
            this.btnInput25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput25.Location = new System.Drawing.Point(553, 73);
            this.btnInput25.Name = "btnInput25";
            this.btnInput25.Size = new System.Drawing.Size(173, 32);
            this.btnInput25.TabIndex = 311;
            this.btnInput25.Tag = "1";
            this.btnInput25.Text = "Vision3 Data Request";
            this.btnInput25.UseVisualStyleBackColor = false;
            this.btnInput25.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput24
            // 
            this.btnInput24.BackColor = System.Drawing.Color.Maroon;
            this.btnInput24.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput24.ForeColor = System.Drawing.Color.White;
            this.btnInput24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput24.Location = new System.Drawing.Point(553, 37);
            this.btnInput24.Name = "btnInput24";
            this.btnInput24.Size = new System.Drawing.Size(173, 32);
            this.btnInput24.TabIndex = 310;
            this.btnInput24.Tag = "0";
            this.btnInput24.Text = "Vision3 Reset";
            this.btnInput24.UseVisualStyleBackColor = false;
            this.btnInput24.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput23
            // 
            this.btnInput23.BackColor = System.Drawing.Color.Maroon;
            this.btnInput23.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput23.ForeColor = System.Drawing.Color.White;
            this.btnInput23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput23.Location = new System.Drawing.Point(374, 289);
            this.btnInput23.Name = "btnInput23";
            this.btnInput23.Size = new System.Drawing.Size(173, 32);
            this.btnInput23.TabIndex = 309;
            this.btnInput23.Tag = "4";
            this.btnInput23.Text = "DI23";
            this.btnInput23.UseVisualStyleBackColor = false;
            this.btnInput23.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput22
            // 
            this.btnInput22.BackColor = System.Drawing.Color.Maroon;
            this.btnInput22.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput22.ForeColor = System.Drawing.Color.White;
            this.btnInput22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput22.Location = new System.Drawing.Point(374, 253);
            this.btnInput22.Name = "btnInput22";
            this.btnInput22.Size = new System.Drawing.Size(173, 32);
            this.btnInput22.TabIndex = 308;
            this.btnInput22.Tag = "4";
            this.btnInput22.Text = "DI22";
            this.btnInput22.UseVisualStyleBackColor = false;
            this.btnInput22.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput21
            // 
            this.btnInput21.BackColor = System.Drawing.Color.Maroon;
            this.btnInput21.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput21.ForeColor = System.Drawing.Color.White;
            this.btnInput21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput21.Location = new System.Drawing.Point(374, 217);
            this.btnInput21.Name = "btnInput21";
            this.btnInput21.Size = new System.Drawing.Size(173, 32);
            this.btnInput21.TabIndex = 307;
            this.btnInput21.Tag = "6";
            this.btnInput21.Text = "DI21";
            this.btnInput21.UseVisualStyleBackColor = false;
            this.btnInput21.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput20
            // 
            this.btnInput20.BackColor = System.Drawing.Color.Maroon;
            this.btnInput20.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput20.ForeColor = System.Drawing.Color.White;
            this.btnInput20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput20.Location = new System.Drawing.Point(374, 181);
            this.btnInput20.Name = "btnInput20";
            this.btnInput20.Size = new System.Drawing.Size(173, 32);
            this.btnInput20.TabIndex = 306;
            this.btnInput20.Tag = "4";
            this.btnInput20.Text = "DI20";
            this.btnInput20.UseVisualStyleBackColor = false;
            this.btnInput20.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput19
            // 
            this.btnInput19.BackColor = System.Drawing.Color.Maroon;
            this.btnInput19.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput19.ForeColor = System.Drawing.Color.White;
            this.btnInput19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput19.Location = new System.Drawing.Point(374, 145);
            this.btnInput19.Name = "btnInput19";
            this.btnInput19.Size = new System.Drawing.Size(173, 32);
            this.btnInput19.TabIndex = 305;
            this.btnInput19.Tag = "3";
            this.btnInput19.Text = "DI19";
            this.btnInput19.UseVisualStyleBackColor = false;
            this.btnInput19.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput18
            // 
            this.btnInput18.BackColor = System.Drawing.Color.Maroon;
            this.btnInput18.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput18.ForeColor = System.Drawing.Color.White;
            this.btnInput18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput18.Location = new System.Drawing.Point(374, 109);
            this.btnInput18.Name = "btnInput18";
            this.btnInput18.Size = new System.Drawing.Size(173, 32);
            this.btnInput18.TabIndex = 304;
            this.btnInput18.Tag = "2";
            this.btnInput18.Text = "DI18";
            this.btnInput18.UseVisualStyleBackColor = false;
            this.btnInput18.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput17
            // 
            this.btnInput17.BackColor = System.Drawing.Color.Maroon;
            this.btnInput17.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput17.ForeColor = System.Drawing.Color.White;
            this.btnInput17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput17.Location = new System.Drawing.Point(374, 73);
            this.btnInput17.Name = "btnInput17";
            this.btnInput17.Size = new System.Drawing.Size(173, 32);
            this.btnInput17.TabIndex = 303;
            this.btnInput17.Tag = "1";
            this.btnInput17.Text = "Vision2 Data Request";
            this.btnInput17.UseVisualStyleBackColor = false;
            this.btnInput17.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnInput16
            // 
            this.btnInput16.BackColor = System.Drawing.Color.Maroon;
            this.btnInput16.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInput16.ForeColor = System.Drawing.Color.White;
            this.btnInput16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnInput16.Location = new System.Drawing.Point(374, 37);
            this.btnInput16.Name = "btnInput16";
            this.btnInput16.Size = new System.Drawing.Size(173, 32);
            this.btnInput16.TabIndex = 302;
            this.btnInput16.Tag = "0";
            this.btnInput16.Text = "Vision2 Reset";
            this.btnInput16.UseVisualStyleBackColor = false;
            this.btnInput16.MousePressing += new System.EventHandler(this.btnInput_MousePressing);
            // 
            // btnOutput31
            // 
            this.btnOutput31.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput31.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput31.ForeColor = System.Drawing.Color.White;
            this.btnOutput31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput31.Location = new System.Drawing.Point(553, 638);
            this.btnOutput31.Name = "btnOutput31";
            this.btnOutput31.Size = new System.Drawing.Size(173, 32);
            this.btnOutput31.TabIndex = 333;
            this.btnOutput31.Tag = "15";
            this.btnOutput31.Text = "DO31";
            this.btnOutput31.UseVisualStyleBackColor = false;
            this.btnOutput31.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput31.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput30
            // 
            this.btnOutput30.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput30.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput30.ForeColor = System.Drawing.Color.White;
            this.btnOutput30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput30.Location = new System.Drawing.Point(553, 602);
            this.btnOutput30.Name = "btnOutput30";
            this.btnOutput30.Size = new System.Drawing.Size(173, 32);
            this.btnOutput30.TabIndex = 332;
            this.btnOutput30.Tag = "14";
            this.btnOutput30.Text = "DO30";
            this.btnOutput30.UseVisualStyleBackColor = false;
            this.btnOutput30.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput30.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput29
            // 
            this.btnOutput29.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput29.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput29.ForeColor = System.Drawing.Color.White;
            this.btnOutput29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput29.Location = new System.Drawing.Point(553, 566);
            this.btnOutput29.Name = "btnOutput29";
            this.btnOutput29.Size = new System.Drawing.Size(173, 32);
            this.btnOutput29.TabIndex = 331;
            this.btnOutput29.Tag = "13";
            this.btnOutput29.Text = "DO29";
            this.btnOutput29.UseVisualStyleBackColor = false;
            this.btnOutput29.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput29.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput28
            // 
            this.btnOutput28.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput28.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput28.ForeColor = System.Drawing.Color.White;
            this.btnOutput28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput28.Location = new System.Drawing.Point(553, 530);
            this.btnOutput28.Name = "btnOutput28";
            this.btnOutput28.Size = new System.Drawing.Size(173, 32);
            this.btnOutput28.TabIndex = 330;
            this.btnOutput28.Tag = "12";
            this.btnOutput28.Text = "DO28";
            this.btnOutput28.UseVisualStyleBackColor = false;
            this.btnOutput28.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput28.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput27
            // 
            this.btnOutput27.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput27.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput27.ForeColor = System.Drawing.Color.White;
            this.btnOutput27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput27.Location = new System.Drawing.Point(553, 494);
            this.btnOutput27.Name = "btnOutput27";
            this.btnOutput27.Size = new System.Drawing.Size(173, 32);
            this.btnOutput27.TabIndex = 329;
            this.btnOutput27.Tag = "11";
            this.btnOutput27.Text = "DO27";
            this.btnOutput27.UseVisualStyleBackColor = false;
            this.btnOutput27.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput27.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput26
            // 
            this.btnOutput26.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput26.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput26.ForeColor = System.Drawing.Color.White;
            this.btnOutput26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput26.Location = new System.Drawing.Point(553, 458);
            this.btnOutput26.Name = "btnOutput26";
            this.btnOutput26.Size = new System.Drawing.Size(173, 32);
            this.btnOutput26.TabIndex = 328;
            this.btnOutput26.Tag = "10";
            this.btnOutput26.Text = "DO26";
            this.btnOutput26.UseVisualStyleBackColor = false;
            this.btnOutput26.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput26.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput25
            // 
            this.btnOutput25.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput25.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput25.ForeColor = System.Drawing.Color.White;
            this.btnOutput25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput25.Location = new System.Drawing.Point(553, 422);
            this.btnOutput25.Name = "btnOutput25";
            this.btnOutput25.Size = new System.Drawing.Size(173, 32);
            this.btnOutput25.TabIndex = 327;
            this.btnOutput25.Tag = "9";
            this.btnOutput25.Text = "Vision3 Complete";
            this.btnOutput25.UseVisualStyleBackColor = false;
            this.btnOutput25.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput25.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput24
            // 
            this.btnOutput24.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput24.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput24.ForeColor = System.Drawing.Color.White;
            this.btnOutput24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput24.Location = new System.Drawing.Point(553, 386);
            this.btnOutput24.Name = "btnOutput24";
            this.btnOutput24.Size = new System.Drawing.Size(173, 32);
            this.btnOutput24.TabIndex = 326;
            this.btnOutput24.Tag = "8";
            this.btnOutput24.Text = "Vision3 Ready";
            this.btnOutput24.UseVisualStyleBackColor = false;
            this.btnOutput24.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput24.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput23
            // 
            this.btnOutput23.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput23.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput23.ForeColor = System.Drawing.Color.White;
            this.btnOutput23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput23.Location = new System.Drawing.Point(374, 638);
            this.btnOutput23.Name = "btnOutput23";
            this.btnOutput23.Size = new System.Drawing.Size(173, 32);
            this.btnOutput23.TabIndex = 325;
            this.btnOutput23.Tag = "7";
            this.btnOutput23.Text = "DO23";
            this.btnOutput23.UseVisualStyleBackColor = false;
            this.btnOutput23.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput23.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput22
            // 
            this.btnOutput22.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput22.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput22.ForeColor = System.Drawing.Color.White;
            this.btnOutput22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput22.Location = new System.Drawing.Point(374, 602);
            this.btnOutput22.Name = "btnOutput22";
            this.btnOutput22.Size = new System.Drawing.Size(173, 32);
            this.btnOutput22.TabIndex = 324;
            this.btnOutput22.Tag = "6";
            this.btnOutput22.Text = "DO22";
            this.btnOutput22.UseVisualStyleBackColor = false;
            this.btnOutput22.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput22.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput21
            // 
            this.btnOutput21.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput21.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput21.ForeColor = System.Drawing.Color.White;
            this.btnOutput21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput21.Location = new System.Drawing.Point(374, 566);
            this.btnOutput21.Name = "btnOutput21";
            this.btnOutput21.Size = new System.Drawing.Size(173, 32);
            this.btnOutput21.TabIndex = 323;
            this.btnOutput21.Tag = "5";
            this.btnOutput21.Text = "DO21";
            this.btnOutput21.UseVisualStyleBackColor = false;
            this.btnOutput21.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput21.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput20
            // 
            this.btnOutput20.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput20.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput20.ForeColor = System.Drawing.Color.White;
            this.btnOutput20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput20.Location = new System.Drawing.Point(374, 530);
            this.btnOutput20.Name = "btnOutput20";
            this.btnOutput20.Size = new System.Drawing.Size(173, 32);
            this.btnOutput20.TabIndex = 322;
            this.btnOutput20.Tag = "4";
            this.btnOutput20.Text = "DO20";
            this.btnOutput20.UseVisualStyleBackColor = false;
            this.btnOutput20.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput20.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput19
            // 
            this.btnOutput19.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput19.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput19.ForeColor = System.Drawing.Color.White;
            this.btnOutput19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput19.Location = new System.Drawing.Point(374, 494);
            this.btnOutput19.Name = "btnOutput19";
            this.btnOutput19.Size = new System.Drawing.Size(173, 32);
            this.btnOutput19.TabIndex = 321;
            this.btnOutput19.Tag = "3";
            this.btnOutput19.Text = "DO19";
            this.btnOutput19.UseVisualStyleBackColor = false;
            this.btnOutput19.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput19.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput18
            // 
            this.btnOutput18.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput18.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput18.ForeColor = System.Drawing.Color.White;
            this.btnOutput18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput18.Location = new System.Drawing.Point(374, 458);
            this.btnOutput18.Name = "btnOutput18";
            this.btnOutput18.Size = new System.Drawing.Size(173, 32);
            this.btnOutput18.TabIndex = 320;
            this.btnOutput18.Tag = "2";
            this.btnOutput18.Text = "DO18";
            this.btnOutput18.UseVisualStyleBackColor = false;
            this.btnOutput18.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput18.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput17
            // 
            this.btnOutput17.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput17.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput17.ForeColor = System.Drawing.Color.White;
            this.btnOutput17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput17.Location = new System.Drawing.Point(374, 422);
            this.btnOutput17.Name = "btnOutput17";
            this.btnOutput17.Size = new System.Drawing.Size(173, 32);
            this.btnOutput17.TabIndex = 319;
            this.btnOutput17.Tag = "1";
            this.btnOutput17.Text = "Vision2 Complete";
            this.btnOutput17.UseVisualStyleBackColor = false;
            this.btnOutput17.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput17.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnOutput16
            // 
            this.btnOutput16.BackColor = System.Drawing.Color.Maroon;
            this.btnOutput16.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOutput16.ForeColor = System.Drawing.Color.White;
            this.btnOutput16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutput16.Location = new System.Drawing.Point(374, 386);
            this.btnOutput16.Name = "btnOutput16";
            this.btnOutput16.Size = new System.Drawing.Size(173, 32);
            this.btnOutput16.TabIndex = 318;
            this.btnOutput16.Tag = "0";
            this.btnOutput16.Text = "Vision2 Ready";
            this.btnOutput16.UseVisualStyleBackColor = false;
            this.btnOutput16.MousePressing += new System.EventHandler(this.btnOutput_MousePressing);
            this.btnOutput16.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnRequest);
            this.panelMain.Controls.Add(this.labelInputTitle);
            this.panelMain.Controls.Add(this.btnOutput31);
            this.panelMain.Controls.Add(this.btnTrigger);
            this.panelMain.Controls.Add(this.btnOutput30);
            this.panelMain.Controls.Add(this.btnInput0);
            this.panelMain.Controls.Add(this.btnOutput29);
            this.panelMain.Controls.Add(this.btnInput1);
            this.panelMain.Controls.Add(this.btnOutput28);
            this.panelMain.Controls.Add(this.btnInput2);
            this.panelMain.Controls.Add(this.btnOutput27);
            this.panelMain.Controls.Add(this.btnInput3);
            this.panelMain.Controls.Add(this.btnOutput26);
            this.panelMain.Controls.Add(this.btnInput4);
            this.panelMain.Controls.Add(this.btnOutput25);
            this.panelMain.Controls.Add(this.btnInput5);
            this.panelMain.Controls.Add(this.btnOutput24);
            this.panelMain.Controls.Add(this.btnInput6);
            this.panelMain.Controls.Add(this.btnOutput23);
            this.panelMain.Controls.Add(this.btnInput7);
            this.panelMain.Controls.Add(this.btnOutput22);
            this.panelMain.Controls.Add(this.btnOutput0);
            this.panelMain.Controls.Add(this.btnOutput21);
            this.panelMain.Controls.Add(this.btnOutput1);
            this.panelMain.Controls.Add(this.btnOutput20);
            this.panelMain.Controls.Add(this.btnOutput2);
            this.panelMain.Controls.Add(this.btnOutput19);
            this.panelMain.Controls.Add(this.btnOutput3);
            this.panelMain.Controls.Add(this.btnOutput18);
            this.panelMain.Controls.Add(this.btnOutput4);
            this.panelMain.Controls.Add(this.btnOutput17);
            this.panelMain.Controls.Add(this.btnOutput5);
            this.panelMain.Controls.Add(this.btnOutput16);
            this.panelMain.Controls.Add(this.btnOutput6);
            this.panelMain.Controls.Add(this.btnInput31);
            this.panelMain.Controls.Add(this.btnOutput7);
            this.panelMain.Controls.Add(this.btnInput30);
            this.panelMain.Controls.Add(this.btnOutput8);
            this.panelMain.Controls.Add(this.btnInput29);
            this.panelMain.Controls.Add(this.btnOutput9);
            this.panelMain.Controls.Add(this.btnInput28);
            this.panelMain.Controls.Add(this.btnOutput10);
            this.panelMain.Controls.Add(this.btnInput27);
            this.panelMain.Controls.Add(this.btnOutput11);
            this.panelMain.Controls.Add(this.btnInput26);
            this.panelMain.Controls.Add(this.btnOutput12);
            this.panelMain.Controls.Add(this.btnInput25);
            this.panelMain.Controls.Add(this.btnOutput13);
            this.panelMain.Controls.Add(this.btnInput24);
            this.panelMain.Controls.Add(this.btnOutput14);
            this.panelMain.Controls.Add(this.btnInput23);
            this.panelMain.Controls.Add(this.btnOutput15);
            this.panelMain.Controls.Add(this.btnInput22);
            this.panelMain.Controls.Add(this.btnInput8);
            this.panelMain.Controls.Add(this.btnInput21);
            this.panelMain.Controls.Add(this.btnInput9);
            this.panelMain.Controls.Add(this.btnInput20);
            this.panelMain.Controls.Add(this.btnInput10);
            this.panelMain.Controls.Add(this.btnInput19);
            this.panelMain.Controls.Add(this.btnInput11);
            this.panelMain.Controls.Add(this.btnInput18);
            this.panelMain.Controls.Add(this.btnInput12);
            this.panelMain.Controls.Add(this.btnInput17);
            this.panelMain.Controls.Add(this.btnInput13);
            this.panelMain.Controls.Add(this.btnInput16);
            this.panelMain.Controls.Add(this.btnInput14);
            this.panelMain.Controls.Add(this.labelOutputTitle);
            this.panelMain.Controls.Add(this.btnInput15);
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(728, 672);
            this.panelMain.TabIndex = 334;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // btnRequest
            // 
            this.btnRequest.Location = new System.Drawing.Point(99, 324);
            this.btnRequest.Name = "btnRequest";
            this.btnRequest.Size = new System.Drawing.Size(87, 25);
            this.btnRequest.TabIndex = 334;
            this.btnRequest.Text = "Request";
            this.btnRequest.UseVisualStyleBackColor = true;
            this.btnRequest.Visible = false;
            this.btnRequest.Click += new System.EventHandler(this.btnRequest_Click);
            // 
            // DIOControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(728, 707);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelExit);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "DIOControlWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DIOControlWindow";
            this.TopMost = true;
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnTrigger;
        private CustomControl.GradientLabel labelInputTitle;
        private CustomControl.GradientLabel labelOutputTitle;
        private System.Windows.Forms.Label labelExit;
        private System.Windows.Forms.Label labelTitle;
        private CPressingButton.PressButton btnInput4;
        private CPressingButton.PressButton btnInput3;
        private CPressingButton.PressButton btnInput2;
        private CPressingButton.PressButton btnInput1;
        private CPressingButton.PressButton btnInput0;
        private CPressingButton.PressButton btnInput5;
        private CPressingButton.PressButton btnInput7;
        private CPressingButton.PressButton btnInput6;
        private CPressingButton.PressButton btnOutput7;
        private CPressingButton.PressButton btnOutput6;
        private CPressingButton.PressButton btnOutput5;
        private CPressingButton.PressButton btnOutput4;
        private CPressingButton.PressButton btnOutput3;
        private CPressingButton.PressButton btnOutput2;
        private CPressingButton.PressButton btnOutput1;
        private CPressingButton.PressButton btnOutput0;
        private CPressingButton.PressButton btnOutput15;
        private CPressingButton.PressButton btnOutput14;
        private CPressingButton.PressButton btnOutput13;
        private CPressingButton.PressButton btnOutput12;
        private CPressingButton.PressButton btnOutput11;
        private CPressingButton.PressButton btnOutput10;
        private CPressingButton.PressButton btnOutput9;
        private CPressingButton.PressButton btnOutput8;
        private CPressingButton.PressButton btnInput15;
        private CPressingButton.PressButton btnInput14;
        private CPressingButton.PressButton btnInput13;
        private CPressingButton.PressButton btnInput12;
        private CPressingButton.PressButton btnInput11;
        private CPressingButton.PressButton btnInput10;
        private CPressingButton.PressButton btnInput9;
        private CPressingButton.PressButton btnInput8;
        private CPressingButton.PressButton btnInput31;
        private CPressingButton.PressButton btnInput30;
        private CPressingButton.PressButton btnInput29;
        private CPressingButton.PressButton btnInput28;
        private CPressingButton.PressButton btnInput27;
        private CPressingButton.PressButton btnInput26;
        private CPressingButton.PressButton btnInput25;
        private CPressingButton.PressButton btnInput24;
        private CPressingButton.PressButton btnInput23;
        private CPressingButton.PressButton btnInput22;
        private CPressingButton.PressButton btnInput21;
        private CPressingButton.PressButton btnInput20;
        private CPressingButton.PressButton btnInput19;
        private CPressingButton.PressButton btnInput18;
        private CPressingButton.PressButton btnInput17;
        private CPressingButton.PressButton btnInput16;
        private CPressingButton.PressButton btnOutput31;
        private CPressingButton.PressButton btnOutput30;
        private CPressingButton.PressButton btnOutput29;
        private CPressingButton.PressButton btnOutput28;
        private CPressingButton.PressButton btnOutput27;
        private CPressingButton.PressButton btnOutput26;
        private CPressingButton.PressButton btnOutput25;
        private CPressingButton.PressButton btnOutput24;
        private CPressingButton.PressButton btnOutput23;
        private CPressingButton.PressButton btnOutput22;
        private CPressingButton.PressButton btnOutput21;
        private CPressingButton.PressButton btnOutput20;
        private CPressingButton.PressButton btnOutput19;
        private CPressingButton.PressButton btnOutput18;
        private CPressingButton.PressButton btnOutput17;
        private CPressingButton.PressButton btnOutput16;
        private System.Windows.Forms.Button btnRequest;
    }
}

