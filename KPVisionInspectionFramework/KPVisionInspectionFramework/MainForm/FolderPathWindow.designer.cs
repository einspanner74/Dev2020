namespace KPVisionInspectionFramework
{
    partial class FolderPathWindow
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
            this.btnConfirm = new System.Windows.Forms.Button();
            this.textBoxPath1 = new System.Windows.Forms.TextBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelPath1 = new System.Windows.Forms.Label();
            this.btnPathSearch1 = new System.Windows.Forms.Button();
            this.btnPathSearch2 = new System.Windows.Forms.Button();
            this.labelPath2 = new System.Windows.Forms.Label();
            this.textBoxPath2 = new System.Windows.Forms.TextBox();
            this.panelPath2 = new System.Windows.Forms.Panel();
            this.panelPath2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConfirm.Location = new System.Drawing.Point(587, 100);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(129, 33);
            this.btnConfirm.TabIndex = 23;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // textBoxPath1
            // 
            this.textBoxPath1.Enabled = false;
            this.textBoxPath1.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxPath1.Location = new System.Drawing.Point(119, 37);
            this.textBoxPath1.Name = "textBoxPath1";
            this.textBoxPath1.Size = new System.Drawing.Size(564, 26);
            this.textBoxPath1.TabIndex = 21;
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.SlateGray;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.White;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(723, 30);
            this.labelTitle.TabIndex = 20;
            this.labelTitle.Text = " New Name";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // labelPath1
            // 
            this.labelPath1.BackColor = System.Drawing.Color.Black;
            this.labelPath1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelPath1.ForeColor = System.Drawing.Color.White;
            this.labelPath1.Location = new System.Drawing.Point(6, 37);
            this.labelPath1.Name = "labelPath1";
            this.labelPath1.Size = new System.Drawing.Size(107, 26);
            this.labelPath1.TabIndex = 24;
            this.labelPath1.Text = "Folder Path";
            this.labelPath1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPathSearch1
            // 
            this.btnPathSearch1.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPathSearch1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPathSearch1.Location = new System.Drawing.Point(685, 37);
            this.btnPathSearch1.Name = "btnPathSearch1";
            this.btnPathSearch1.Size = new System.Drawing.Size(31, 24);
            this.btnPathSearch1.TabIndex = 25;
            this.btnPathSearch1.Tag = "0";
            this.btnPathSearch1.Text = "...";
            this.btnPathSearch1.UseVisualStyleBackColor = true;
            this.btnPathSearch1.Click += new System.EventHandler(this.btnSearchDataPath_Click);
            // 
            // btnPathSearch2
            // 
            this.btnPathSearch2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPathSearch2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPathSearch2.Location = new System.Drawing.Point(681, 4);
            this.btnPathSearch2.Name = "btnPathSearch2";
            this.btnPathSearch2.Size = new System.Drawing.Size(31, 24);
            this.btnPathSearch2.TabIndex = 28;
            this.btnPathSearch2.Tag = "1";
            this.btnPathSearch2.Text = "...";
            this.btnPathSearch2.UseVisualStyleBackColor = true;
            this.btnPathSearch2.Click += new System.EventHandler(this.btnSearchDataPath_Click);
            // 
            // labelPath2
            // 
            this.labelPath2.BackColor = System.Drawing.Color.Black;
            this.labelPath2.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelPath2.ForeColor = System.Drawing.Color.White;
            this.labelPath2.Location = new System.Drawing.Point(2, 4);
            this.labelPath2.Name = "labelPath2";
            this.labelPath2.Size = new System.Drawing.Size(107, 26);
            this.labelPath2.TabIndex = 27;
            this.labelPath2.Text = "Folder Path";
            this.labelPath2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxPath2
            // 
            this.textBoxPath2.Enabled = false;
            this.textBoxPath2.Font = new System.Drawing.Font("나눔바른고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBoxPath2.Location = new System.Drawing.Point(115, 4);
            this.textBoxPath2.Name = "textBoxPath2";
            this.textBoxPath2.Size = new System.Drawing.Size(564, 26);
            this.textBoxPath2.TabIndex = 26;
            // 
            // panelPath2
            // 
            this.panelPath2.Controls.Add(this.btnPathSearch2);
            this.panelPath2.Controls.Add(this.labelPath2);
            this.panelPath2.Controls.Add(this.textBoxPath2);
            this.panelPath2.Location = new System.Drawing.Point(4, 64);
            this.panelPath2.Name = "panelPath2";
            this.panelPath2.Size = new System.Drawing.Size(718, 36);
            this.panelPath2.TabIndex = 29;
            // 
            // FolderPathWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.ClientSize = new System.Drawing.Size(723, 138);
            this.Controls.Add(this.panelPath2);
            this.Controls.Add(this.btnPathSearch1);
            this.Controls.Add(this.labelPath1);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.textBoxPath1);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FolderPathWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RecipeNewName";
            this.TopMost = true;
            this.panelPath2.ResumeLayout(false);
            this.panelPath2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TextBox textBoxPath1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelPath1;
        private System.Windows.Forms.Button btnPathSearch1;
        private System.Windows.Forms.Button btnPathSearch2;
        private System.Windows.Forms.Label labelPath2;
        private System.Windows.Forms.TextBox textBoxPath2;
        private System.Windows.Forms.Panel panelPath2;
    }
}