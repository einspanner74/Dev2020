namespace WindowKeyPad
{
    partial class WindowKeypadControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WindowKeypadControl));
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.labelKeyPadCharactor = new CustomControl.GradientLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCaps = new System.Windows.Forms.Button();
            this.button51 = new System.Windows.Forms.Button();
            this.btnESC = new System.Windows.Forms.Button();
            this.btnShift = new System.Windows.Forms.Button();
            this.btnShift2 = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnWon = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btn0 = new System.Windows.Forms.Button();
            this.btn9 = new System.Windows.Forms.Button();
            this.btn8 = new System.Windows.Forms.Button();
            this.btn7 = new System.Windows.Forms.Button();
            this.btn6 = new System.Windows.Forms.Button();
            this.btn5 = new System.Windows.Forms.Button();
            this.btn4 = new System.Windows.Forms.Button();
            this.btn3 = new System.Windows.Forms.Button();
            this.btn2 = new System.Windows.Forms.Button();
            this.btn1 = new System.Windows.Forms.Button();
            this.btnRightBlacket = new System.Windows.Forms.Button();
            this.btnLeftBlacket = new System.Windows.Forms.Button();
            this.btnP = new System.Windows.Forms.Button();
            this.btnO = new System.Windows.Forms.Button();
            this.btnI = new System.Windows.Forms.Button();
            this.btnU = new System.Windows.Forms.Button();
            this.btnY = new System.Windows.Forms.Button();
            this.btnT = new System.Windows.Forms.Button();
            this.btnR = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnW = new System.Windows.Forms.Button();
            this.btnQ = new System.Windows.Forms.Button();
            this.btnSlash = new System.Windows.Forms.Button();
            this.btnPoint = new System.Windows.Forms.Button();
            this.btnComma = new System.Windows.Forms.Button();
            this.btnM = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.btnB = new System.Windows.Forms.Button();
            this.btnV = new System.Windows.Forms.Button();
            this.btnC = new System.Windows.Forms.Button();
            this.btnX = new System.Windows.Forms.Button();
            this.btnZ = new System.Windows.Forms.Button();
            this.btnQuotation = new System.Windows.Forms.Button();
            this.btnSemi = new System.Windows.Forms.Button();
            this.btnL = new System.Windows.Forms.Button();
            this.btnK = new System.Windows.Forms.Button();
            this.btnJ = new System.Windows.Forms.Button();
            this.btnH = new System.Windows.Forms.Button();
            this.btnG = new System.Windows.Forms.Button();
            this.btnF = new System.Windows.Forms.Button();
            this.btnD = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.Button();
            this.btnA = new System.Windows.Forms.Button();
            this.btnPasswordChange = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.labelTitle, "labelTitle");
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.btnPasswordChange);
            this.panelMain.Controls.Add(this.labelKeyPadCharactor);
            this.panelMain.Controls.Add(this.btnCancel);
            this.panelMain.Controls.Add(this.btnOK);
            this.panelMain.Controls.Add(this.btnCaps);
            this.panelMain.Controls.Add(this.button51);
            this.panelMain.Controls.Add(this.btnESC);
            this.panelMain.Controls.Add(this.btnShift);
            this.panelMain.Controls.Add(this.btnShift2);
            this.panelMain.Controls.Add(this.btnEnter);
            this.panelMain.Controls.Add(this.btnWon);
            this.panelMain.Controls.Add(this.btnBack);
            this.panelMain.Controls.Add(this.btnEqual);
            this.panelMain.Controls.Add(this.btnMinus);
            this.panelMain.Controls.Add(this.btn0);
            this.panelMain.Controls.Add(this.btn9);
            this.panelMain.Controls.Add(this.btn8);
            this.panelMain.Controls.Add(this.btn7);
            this.panelMain.Controls.Add(this.btn6);
            this.panelMain.Controls.Add(this.btn5);
            this.panelMain.Controls.Add(this.btn4);
            this.panelMain.Controls.Add(this.btn3);
            this.panelMain.Controls.Add(this.btn2);
            this.panelMain.Controls.Add(this.btn1);
            this.panelMain.Controls.Add(this.btnRightBlacket);
            this.panelMain.Controls.Add(this.btnLeftBlacket);
            this.panelMain.Controls.Add(this.btnP);
            this.panelMain.Controls.Add(this.btnO);
            this.panelMain.Controls.Add(this.btnI);
            this.panelMain.Controls.Add(this.btnU);
            this.panelMain.Controls.Add(this.btnY);
            this.panelMain.Controls.Add(this.btnT);
            this.panelMain.Controls.Add(this.btnR);
            this.panelMain.Controls.Add(this.btnE);
            this.panelMain.Controls.Add(this.btnW);
            this.panelMain.Controls.Add(this.btnQ);
            this.panelMain.Controls.Add(this.btnSlash);
            this.panelMain.Controls.Add(this.btnPoint);
            this.panelMain.Controls.Add(this.btnComma);
            this.panelMain.Controls.Add(this.btnM);
            this.panelMain.Controls.Add(this.btnN);
            this.panelMain.Controls.Add(this.btnB);
            this.panelMain.Controls.Add(this.btnV);
            this.panelMain.Controls.Add(this.btnC);
            this.panelMain.Controls.Add(this.btnX);
            this.panelMain.Controls.Add(this.btnZ);
            this.panelMain.Controls.Add(this.btnQuotation);
            this.panelMain.Controls.Add(this.btnSemi);
            this.panelMain.Controls.Add(this.btnL);
            this.panelMain.Controls.Add(this.btnK);
            this.panelMain.Controls.Add(this.btnJ);
            this.panelMain.Controls.Add(this.btnH);
            this.panelMain.Controls.Add(this.btnG);
            this.panelMain.Controls.Add(this.btnF);
            this.panelMain.Controls.Add(this.btnD);
            this.panelMain.Controls.Add(this.btnS);
            this.panelMain.Controls.Add(this.btnA);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // labelKeyPadCharactor
            // 
            this.labelKeyPadCharactor.BackColor = System.Drawing.Color.White;
            this.labelKeyPadCharactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyPadCharactor.ColorBottom = System.Drawing.Color.Empty;
            this.labelKeyPadCharactor.ColorTop = System.Drawing.Color.Empty;
            resources.ApplyResources(this.labelKeyPadCharactor, "labelKeyPadCharactor");
            this.labelKeyPadCharactor.GradientDirection = CustomControl.GradientLabel.Direction.Vertical;
            this.labelKeyPadCharactor.Name = "labelKeyPadCharactor";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCaps
            // 
            resources.ApplyResources(this.btnCaps, "btnCaps");
            this.btnCaps.Name = "btnCaps";
            this.btnCaps.UseVisualStyleBackColor = true;
            this.btnCaps.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // button51
            // 
            resources.ApplyResources(this.button51, "button51");
            this.button51.Name = "button51";
            this.button51.UseVisualStyleBackColor = true;
            this.button51.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnESC
            // 
            resources.ApplyResources(this.btnESC, "btnESC");
            this.btnESC.Name = "btnESC";
            this.btnESC.UseVisualStyleBackColor = true;
            this.btnESC.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnShift
            // 
            resources.ApplyResources(this.btnShift, "btnShift");
            this.btnShift.Name = "btnShift";
            this.btnShift.UseVisualStyleBackColor = true;
            this.btnShift.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnShift2
            // 
            resources.ApplyResources(this.btnShift2, "btnShift2");
            this.btnShift2.Name = "btnShift2";
            this.btnShift2.UseVisualStyleBackColor = true;
            this.btnShift2.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnEnter
            // 
            resources.ApplyResources(this.btnEnter, "btnEnter");
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnWon
            // 
            resources.ApplyResources(this.btnWon, "btnWon");
            this.btnWon.Name = "btnWon";
            this.btnWon.UseVisualStyleBackColor = true;
            this.btnWon.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnEqual
            // 
            resources.ApplyResources(this.btnEqual, "btnEqual");
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnMinus
            // 
            resources.ApplyResources(this.btnMinus, "btnMinus");
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn0
            // 
            resources.ApplyResources(this.btn0, "btn0");
            this.btn0.Name = "btn0";
            this.btn0.UseVisualStyleBackColor = true;
            this.btn0.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn9
            // 
            resources.ApplyResources(this.btn9, "btn9");
            this.btn9.Name = "btn9";
            this.btn9.UseVisualStyleBackColor = true;
            this.btn9.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn8
            // 
            resources.ApplyResources(this.btn8, "btn8");
            this.btn8.Name = "btn8";
            this.btn8.UseVisualStyleBackColor = true;
            this.btn8.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn7
            // 
            resources.ApplyResources(this.btn7, "btn7");
            this.btn7.Name = "btn7";
            this.btn7.UseVisualStyleBackColor = true;
            this.btn7.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn6
            // 
            resources.ApplyResources(this.btn6, "btn6");
            this.btn6.Name = "btn6";
            this.btn6.UseVisualStyleBackColor = true;
            this.btn6.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn5
            // 
            resources.ApplyResources(this.btn5, "btn5");
            this.btn5.Name = "btn5";
            this.btn5.UseVisualStyleBackColor = true;
            this.btn5.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn4
            // 
            resources.ApplyResources(this.btn4, "btn4");
            this.btn4.Name = "btn4";
            this.btn4.UseVisualStyleBackColor = true;
            this.btn4.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn3
            // 
            resources.ApplyResources(this.btn3, "btn3");
            this.btn3.Name = "btn3";
            this.btn3.UseVisualStyleBackColor = true;
            this.btn3.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn2
            // 
            resources.ApplyResources(this.btn2, "btn2");
            this.btn2.Name = "btn2";
            this.btn2.UseVisualStyleBackColor = true;
            this.btn2.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btn1
            // 
            resources.ApplyResources(this.btn1, "btn1");
            this.btn1.Name = "btn1";
            this.btn1.UseVisualStyleBackColor = true;
            this.btn1.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnRightBlacket
            // 
            resources.ApplyResources(this.btnRightBlacket, "btnRightBlacket");
            this.btnRightBlacket.Name = "btnRightBlacket";
            this.btnRightBlacket.UseVisualStyleBackColor = true;
            this.btnRightBlacket.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnLeftBlacket
            // 
            resources.ApplyResources(this.btnLeftBlacket, "btnLeftBlacket");
            this.btnLeftBlacket.Name = "btnLeftBlacket";
            this.btnLeftBlacket.UseVisualStyleBackColor = true;
            this.btnLeftBlacket.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnP
            // 
            resources.ApplyResources(this.btnP, "btnP");
            this.btnP.Name = "btnP";
            this.btnP.UseVisualStyleBackColor = true;
            this.btnP.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnO
            // 
            resources.ApplyResources(this.btnO, "btnO");
            this.btnO.Name = "btnO";
            this.btnO.UseVisualStyleBackColor = true;
            this.btnO.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnI
            // 
            resources.ApplyResources(this.btnI, "btnI");
            this.btnI.Name = "btnI";
            this.btnI.UseVisualStyleBackColor = true;
            this.btnI.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnU
            // 
            resources.ApplyResources(this.btnU, "btnU");
            this.btnU.Name = "btnU";
            this.btnU.UseVisualStyleBackColor = true;
            this.btnU.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnY
            // 
            resources.ApplyResources(this.btnY, "btnY");
            this.btnY.Name = "btnY";
            this.btnY.UseVisualStyleBackColor = true;
            this.btnY.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnT
            // 
            resources.ApplyResources(this.btnT, "btnT");
            this.btnT.Name = "btnT";
            this.btnT.UseVisualStyleBackColor = true;
            this.btnT.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnR
            // 
            resources.ApplyResources(this.btnR, "btnR");
            this.btnR.Name = "btnR";
            this.btnR.UseVisualStyleBackColor = true;
            this.btnR.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnE
            // 
            resources.ApplyResources(this.btnE, "btnE");
            this.btnE.Name = "btnE";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnW
            // 
            resources.ApplyResources(this.btnW, "btnW");
            this.btnW.Name = "btnW";
            this.btnW.UseVisualStyleBackColor = true;
            this.btnW.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnQ
            // 
            resources.ApplyResources(this.btnQ, "btnQ");
            this.btnQ.Name = "btnQ";
            this.btnQ.UseVisualStyleBackColor = true;
            this.btnQ.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnSlash
            // 
            resources.ApplyResources(this.btnSlash, "btnSlash");
            this.btnSlash.Name = "btnSlash";
            this.btnSlash.UseVisualStyleBackColor = true;
            this.btnSlash.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnPoint
            // 
            resources.ApplyResources(this.btnPoint, "btnPoint");
            this.btnPoint.Name = "btnPoint";
            this.btnPoint.UseVisualStyleBackColor = true;
            this.btnPoint.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnComma
            // 
            resources.ApplyResources(this.btnComma, "btnComma");
            this.btnComma.Name = "btnComma";
            this.btnComma.UseVisualStyleBackColor = true;
            this.btnComma.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnM
            // 
            resources.ApplyResources(this.btnM, "btnM");
            this.btnM.Name = "btnM";
            this.btnM.UseVisualStyleBackColor = true;
            this.btnM.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnN
            // 
            resources.ApplyResources(this.btnN, "btnN");
            this.btnN.Name = "btnN";
            this.btnN.UseVisualStyleBackColor = true;
            this.btnN.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnB
            // 
            resources.ApplyResources(this.btnB, "btnB");
            this.btnB.Name = "btnB";
            this.btnB.UseVisualStyleBackColor = true;
            this.btnB.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnV
            // 
            resources.ApplyResources(this.btnV, "btnV");
            this.btnV.Name = "btnV";
            this.btnV.UseVisualStyleBackColor = true;
            this.btnV.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnC
            // 
            resources.ApplyResources(this.btnC, "btnC");
            this.btnC.Name = "btnC";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnX
            // 
            resources.ApplyResources(this.btnX, "btnX");
            this.btnX.Name = "btnX";
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnZ
            // 
            resources.ApplyResources(this.btnZ, "btnZ");
            this.btnZ.Name = "btnZ";
            this.btnZ.UseVisualStyleBackColor = true;
            this.btnZ.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnQuotation
            // 
            resources.ApplyResources(this.btnQuotation, "btnQuotation");
            this.btnQuotation.Name = "btnQuotation";
            this.btnQuotation.UseVisualStyleBackColor = true;
            this.btnQuotation.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnSemi
            // 
            resources.ApplyResources(this.btnSemi, "btnSemi");
            this.btnSemi.Name = "btnSemi";
            this.btnSemi.UseVisualStyleBackColor = true;
            this.btnSemi.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnL
            // 
            resources.ApplyResources(this.btnL, "btnL");
            this.btnL.Name = "btnL";
            this.btnL.UseVisualStyleBackColor = true;
            this.btnL.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnK
            // 
            resources.ApplyResources(this.btnK, "btnK");
            this.btnK.Name = "btnK";
            this.btnK.UseVisualStyleBackColor = true;
            this.btnK.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnJ
            // 
            resources.ApplyResources(this.btnJ, "btnJ");
            this.btnJ.Name = "btnJ";
            this.btnJ.UseVisualStyleBackColor = true;
            this.btnJ.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnH
            // 
            resources.ApplyResources(this.btnH, "btnH");
            this.btnH.Name = "btnH";
            this.btnH.UseVisualStyleBackColor = true;
            this.btnH.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnG
            // 
            resources.ApplyResources(this.btnG, "btnG");
            this.btnG.Name = "btnG";
            this.btnG.UseVisualStyleBackColor = true;
            this.btnG.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnF
            // 
            resources.ApplyResources(this.btnF, "btnF");
            this.btnF.Name = "btnF";
            this.btnF.UseVisualStyleBackColor = true;
            this.btnF.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnD
            // 
            resources.ApplyResources(this.btnD, "btnD");
            this.btnD.Name = "btnD";
            this.btnD.UseVisualStyleBackColor = true;
            this.btnD.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnS
            // 
            resources.ApplyResources(this.btnS, "btnS");
            this.btnS.Name = "btnS";
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnA
            // 
            resources.ApplyResources(this.btnA, "btnA");
            this.btnA.Name = "btnA";
            this.btnA.UseVisualStyleBackColor = true;
            this.btnA.Click += new System.EventHandler(this.ButtonsClickEvent);
            // 
            // btnPasswordChange
            // 
            this.btnPasswordChange.BackgroundImage = global::WindowKeyPad.Properties.Resources.PasswordChange;
            resources.ApplyResources(this.btnPasswordChange, "btnPasswordChange");
            this.btnPasswordChange.Name = "btnPasswordChange";
            this.btnPasswordChange.UseVisualStyleBackColor = true;
            this.btnPasswordChange.Click += new System.EventHandler(this.btnPasswordChange_Click);
            // 
            // WindowKeypadControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WindowKeypadControl";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WindowKeypadControl_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WindowKeyPad_KeyPress);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private CustomControl.GradientLabel labelKeyPadCharactor;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCaps;
        private System.Windows.Forms.Button button51;
        private System.Windows.Forms.Button btnESC;
        private System.Windows.Forms.Button btnShift;
        private System.Windows.Forms.Button btnShift2;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnWon;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btn0;
        private System.Windows.Forms.Button btn9;
        private System.Windows.Forms.Button btn8;
        private System.Windows.Forms.Button btn7;
        private System.Windows.Forms.Button btn6;
        private System.Windows.Forms.Button btn5;
        private System.Windows.Forms.Button btn4;
        private System.Windows.Forms.Button btn3;
        private System.Windows.Forms.Button btn2;
        private System.Windows.Forms.Button btn1;
        private System.Windows.Forms.Button btnRightBlacket;
        private System.Windows.Forms.Button btnLeftBlacket;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Button btnO;
        private System.Windows.Forms.Button btnI;
        private System.Windows.Forms.Button btnU;
        private System.Windows.Forms.Button btnY;
        private System.Windows.Forms.Button btnT;
        private System.Windows.Forms.Button btnR;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button btnW;
        private System.Windows.Forms.Button btnQ;
        private System.Windows.Forms.Button btnSlash;
        private System.Windows.Forms.Button btnPoint;
        private System.Windows.Forms.Button btnComma;
        private System.Windows.Forms.Button btnM;
        private System.Windows.Forms.Button btnN;
        private System.Windows.Forms.Button btnB;
        private System.Windows.Forms.Button btnV;
        private System.Windows.Forms.Button btnC;
        private System.Windows.Forms.Button btnX;
        private System.Windows.Forms.Button btnZ;
        private System.Windows.Forms.Button btnQuotation;
        private System.Windows.Forms.Button btnSemi;
        private System.Windows.Forms.Button btnL;
        private System.Windows.Forms.Button btnK;
        private System.Windows.Forms.Button btnJ;
        private System.Windows.Forms.Button btnH;
        private System.Windows.Forms.Button btnG;
        private System.Windows.Forms.Button btnF;
        private System.Windows.Forms.Button btnD;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnA;
        private System.Windows.Forms.Button btnPasswordChange;
    }
}
