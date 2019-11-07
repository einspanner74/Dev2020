namespace MitsubishiCommunicationManager
{
    partial class MitsubishiCommunicationWindow
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.gridViewVTP = new CustomControl.GridViewManager();
            this.gridViewPTV = new CustomControl.GridViewManager();
            this.btnInspectionRequest = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnVTPSend = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxWord = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxBit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelExit = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(1011, 32);
            this.labelTitle.TabIndex = 12;
            this.labelTitle.Text = " Sequence Window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gridViewVTP);
            this.panelMain.Controls.Add(this.gridViewPTV);
            this.panelMain.Controls.Add(this.btnInspectionRequest);
            this.panelMain.Controls.Add(this.btnClear);
            this.panelMain.Controls.Add(this.btnVTPSend);
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.textBoxWord);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.textBoxBit);
            this.panelMain.Controls.Add(this.label3);
            this.panelMain.Controls.Add(this.textBoxAddr);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMain.Location = new System.Drawing.Point(0, 35);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1011, 832);
            this.panelMain.TabIndex = 13;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // gridViewVTP
            // 
            this.gridViewVTP.Location = new System.Drawing.Point(512, 25);
            this.gridViewVTP.Name = "gridViewVTP";
            this.gridViewVTP.Size = new System.Drawing.Size(495, 760);
            this.gridViewVTP.TabIndex = 13;
            // 
            // gridViewPTV
            // 
            this.gridViewPTV.Location = new System.Drawing.Point(7, 25);
            this.gridViewPTV.Name = "gridViewPTV";
            this.gridViewPTV.Size = new System.Drawing.Size(495, 760);
            this.gridViewPTV.TabIndex = 12;
            // 
            // btnInspectionRequest
            // 
            this.btnInspectionRequest.Location = new System.Drawing.Point(7, 791);
            this.btnInspectionRequest.Name = "btnInspectionRequest";
            this.btnInspectionRequest.Size = new System.Drawing.Size(88, 38);
            this.btnInspectionRequest.TabIndex = 11;
            this.btnInspectionRequest.Text = "Inspection Request";
            this.btnInspectionRequest.UseVisualStyleBackColor = true;
            this.btnInspectionRequest.Click += new System.EventHandler(this.btnInspectionRequest_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(943, 794);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 25);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnVTPSend
            // 
            this.btnVTPSend.Location = new System.Drawing.Point(875, 794);
            this.btnVTPSend.Name = "btnVTPSend";
            this.btnVTPSend.Size = new System.Drawing.Size(65, 25);
            this.btnVTPSend.TabIndex = 9;
            this.btnVTPSend.Text = "Send";
            this.btnVTPSend.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(739, 800);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "Word :";
            // 
            // textBoxWord
            // 
            this.textBoxWord.Location = new System.Drawing.Point(790, 795);
            this.textBoxWord.Name = "textBoxWord";
            this.textBoxWord.Size = new System.Drawing.Size(78, 21);
            this.textBoxWord.TabIndex = 7;
            this.textBoxWord.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(666, 800);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Bit :";
            // 
            // textBoxBit
            // 
            this.textBoxBit.Location = new System.Drawing.Point(699, 796);
            this.textBoxBit.Name = "textBoxBit";
            this.textBoxBit.Size = new System.Drawing.Size(27, 21);
            this.textBoxBit.TabIndex = 5;
            this.textBoxBit.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(510, 800);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address :";
            // 
            // textBoxAddr
            // 
            this.textBoxAddr.Location = new System.Drawing.Point(574, 796);
            this.textBoxAddr.Name = "textBoxAddr";
            this.textBoxAddr.Size = new System.Drawing.Size(78, 21);
            this.textBoxAddr.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(506, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vision -> PLC";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "PLC -> Vision";
            // 
            // labelExit
            // 
            this.labelExit.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExit.ForeColor = System.Drawing.Color.White;
            this.labelExit.Location = new System.Drawing.Point(982, 3);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(25, 24);
            this.labelExit.TabIndex = 15;
            this.labelExit.Text = "X";
            this.labelExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelExit.Click += new System.EventHandler(this.labelExit_Click);
            // 
            // MitsubishiCommunicationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(1011, 867);
            this.Controls.Add(this.labelExit);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MitsubishiCommunicationWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MitsubishiCommunicationWindow_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnInspectionRequest;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnVTPSend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxWord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxBit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CustomControl.GridViewManager gridViewVTP;
        private CustomControl.GridViewManager gridViewPTV;
        private System.Windows.Forms.Label labelExit;
    }
}

