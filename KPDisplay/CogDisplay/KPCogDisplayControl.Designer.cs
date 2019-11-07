namespace KPDisplay
{
    partial class KPCogDisplayControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KPCogDisplayControl));
            this.cogDisplayStatusBarV2 = new Cognex.VisionPro.CogDisplayStatusBarV2();
            this.kCogDisplay = new KPDisplay.KPCogDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.kCogDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // cogDisplayStatusBarV2
            // 
            this.cogDisplayStatusBarV2.CoordinateSpaceName = "*\\#";
            this.cogDisplayStatusBarV2.CoordinateSpaceName3D = "*\\#";
            this.cogDisplayStatusBarV2.Location = new System.Drawing.Point(-1, 503);
            this.cogDisplayStatusBarV2.Name = "cogDisplayStatusBarV2";
            this.cogDisplayStatusBarV2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cogDisplayStatusBarV2.Size = new System.Drawing.Size(499, 22);
            this.cogDisplayStatusBarV2.TabIndex = 1;
            this.cogDisplayStatusBarV2.Use3DCoordinateSpaceTree = false;
            // 
            // kCogDisplay
            // 
            this.kCogDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.kCogDisplay.ColorMapLowerRoiLimit = 0D;
            this.kCogDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.kCogDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.kCogDisplay.ColorMapUpperRoiLimit = 1D;
            this.kCogDisplay.DoubleTapZoomCycleLength = 2;
            this.kCogDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.kCogDisplay.Location = new System.Drawing.Point(0, 0);
            this.kCogDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.kCogDisplay.MouseWheelSensitivity = 1D;
            this.kCogDisplay.Name = "kCogDisplay";
            this.kCogDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("kCogDisplay.OcxState")));
            this.kCogDisplay.Size = new System.Drawing.Size(498, 500);
            this.kCogDisplay.TabIndex = 0;
            this.kCogDisplay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kCogDisplay_MouseDown);
            this.kCogDisplay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.kCogDisplay_MouseUp);
            // 
            // KPCogDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cogDisplayStatusBarV2);
            this.Controls.Add(this.kCogDisplay);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "KPCogDisplayControl";
            this.Size = new System.Drawing.Size(498, 527);
            this.Resize += new System.EventHandler(this.KPCogDisplayControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.kCogDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KPCogDisplay kCogDisplay;
        private Cognex.VisionPro.CogDisplayStatusBarV2 cogDisplayStatusBarV2;
    }
}
