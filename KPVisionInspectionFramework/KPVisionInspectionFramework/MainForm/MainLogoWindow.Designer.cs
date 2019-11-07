namespace KPVisionInspectionFramework
{
    partial class MainLogoWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picLogoImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picLogoImage
            // 
            this.picLogoImage.BackgroundImage = global::KPVisionInspectionFramework.Properties.Resources.Com_Logo_Mobis;
            this.picLogoImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLogoImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogoImage.Location = new System.Drawing.Point(0, 0);
            this.picLogoImage.Name = "picLogoImage";
            this.picLogoImage.Size = new System.Drawing.Size(280, 77);
            this.picLogoImage.TabIndex = 0;
            this.picLogoImage.TabStop = false;
            // 
            // MainLogoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(280, 77);
            this.Controls.Add(this.picLogoImage);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(2000, 100);
            this.Name = "MainLogoWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MainLogoWindow";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)(this.picLogoImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogoImage;
    }
}