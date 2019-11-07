namespace LogMessageManager
{
    partial class LogWindowSE
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.listBoxConfigLog = new System.Windows.Forms.ListBox();
            this.labelLogClear = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSetLog = new System.Windows.Forms.Button();
            this.btnLogFolderOpen = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.DarkSlateGray;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(857, 30);
            this.labelTitle.TabIndex = 10;
            this.labelTitle.Text = "  Log Window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDoubleClick);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listBoxConfigLog);
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(857, 473);
            this.panelMain.TabIndex = 11;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
            // 
            // listBoxConfigLog
            // 
            this.listBoxConfigLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.listBoxConfigLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxConfigLog.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBoxConfigLog.ForeColor = System.Drawing.Color.DarkOrange;
            this.listBoxConfigLog.FormattingEnabled = true;
            this.listBoxConfigLog.HorizontalScrollbar = true;
            this.listBoxConfigLog.ItemHeight = 14;
            this.listBoxConfigLog.Location = new System.Drawing.Point(3, 2);
            this.listBoxConfigLog.Name = "listBoxConfigLog";
            this.listBoxConfigLog.Size = new System.Drawing.Size(844, 462);
            this.listBoxConfigLog.TabIndex = 3;
            // 
            // labelLogClear
            // 
            this.labelLogClear.AutoSize = true;
            this.labelLogClear.BackColor = System.Drawing.Color.DarkSlateGray;
            this.labelLogClear.Font = new System.Drawing.Font("나눔바른고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelLogClear.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.labelLogClear.Location = new System.Drawing.Point(744, 5);
            this.labelLogClear.Name = "labelLogClear";
            this.labelLogClear.Size = new System.Drawing.Size(22, 22);
            this.labelLogClear.TabIndex = 12;
            this.labelLogClear.Text = "C";
            this.labelLogClear.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labelLogClear_MouseDoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(825, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 28);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSetLog
            // 
            this.btnSetLog.BackgroundImage = global::LogMessageManager.Properties.Resources.Config;
            this.btnSetLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSetLog.Location = new System.Drawing.Point(769, 4);
            this.btnSetLog.Name = "btnSetLog";
            this.btnSetLog.Size = new System.Drawing.Size(28, 28);
            this.btnSetLog.TabIndex = 15;
            this.btnSetLog.UseVisualStyleBackColor = true;
            this.btnSetLog.Click += new System.EventHandler(this.btnSetLog_Click);
            // 
            // btnLogFolderOpen
            // 
            this.btnLogFolderOpen.BackgroundImage = global::LogMessageManager.Properties.Resources.Documents;
            this.btnLogFolderOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogFolderOpen.Location = new System.Drawing.Point(797, 4);
            this.btnLogFolderOpen.Name = "btnLogFolderOpen";
            this.btnLogFolderOpen.Size = new System.Drawing.Size(28, 28);
            this.btnLogFolderOpen.TabIndex = 13;
            this.btnLogFolderOpen.UseVisualStyleBackColor = true;
            this.btnLogFolderOpen.Click += new System.EventHandler(this.btnLogFolderOpen_Click);
            // 
            // LogWindowSE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.ClientSize = new System.Drawing.Size(857, 508);
            this.ControlBox = false;
            this.Controls.Add(this.btnSetLog);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogFolderOpen);
            this.Controls.Add(this.labelLogClear);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.249999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "LogWindowSE";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogWindowSE";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogWindowSE_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogWindowSE_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LogWindowSE_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LogWindowSE_MouseUp);
            this.Resize += new System.EventHandler(this.LogWindowSE_Resize);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ListBox listBoxConfigLog;
        private System.Windows.Forms.Label labelLogClear;
        private System.Windows.Forms.Button btnLogFolderOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSetLog;
    }
}