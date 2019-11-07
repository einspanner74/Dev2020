namespace InspectionSystemManager
{
    partial class ManualInspectionWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualInspectionWindow));
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listViewImageFile = new System.Windows.Forms.ListView();
            this.btnInspection = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.textBoxBasePath = new System.Windows.Forms.TextBox();
            this.btnSetPath = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.listBoxImageFile = new System.Windows.Forms.ListBox();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.BackColor = System.Drawing.Color.Maroon;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTitle.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(434, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = " Quick Inspection Window";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Paint += new System.Windows.Forms.PaintEventHandler(this.labelTitle_Paint);
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelTitle_MouseMove);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.listViewImageFile);
            this.panelMain.Controls.Add(this.btnInspection);
            this.panelMain.Controls.Add(this.btnLoadImage);
            this.panelMain.Controls.Add(this.textBoxBasePath);
            this.panelMain.Controls.Add(this.btnSetPath);
            this.panelMain.Controls.Add(this.btnOK);
            this.panelMain.Controls.Add(this.listBoxImageFile);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelMain.Location = new System.Drawing.Point(0, 33);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(434, 619);
            this.panelMain.TabIndex = 2;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 59;
            this.label2.Text = "Image List";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔바른고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 59;
            this.label1.Text = "Image load base path";
            // 
            // listViewImageFile
            // 
            this.listViewImageFile.Location = new System.Drawing.Point(3, 72);
            this.listViewImageFile.Name = "listViewImageFile";
            this.listViewImageFile.Size = new System.Drawing.Size(386, 547);
            this.listViewImageFile.TabIndex = 58;
            this.listViewImageFile.UseCompatibleStateImageBehavior = false;
            this.listViewImageFile.View = System.Windows.Forms.View.SmallIcon;
            this.listViewImageFile.SelectedIndexChanged += new System.EventHandler(this.listViewImageFile_SelectedIndexChanged);
            this.listViewImageFile.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewImageFile_MouseDoubleClick);
            // 
            // btnInspection
            // 
            this.btnInspection.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnInspection.BackgroundImage")));
            this.btnInspection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInspection.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnInspection.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInspection.Location = new System.Drawing.Point(395, 114);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(36, 36);
            this.btnInspection.TabIndex = 57;
            this.btnInspection.Text = " ";
            this.btnInspection.UseVisualStyleBackColor = true;
            this.btnInspection.Click += new System.EventHandler(this.btnInspection_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLoadImage.BackgroundImage")));
            this.btnLoadImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLoadImage.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnLoadImage.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadImage.Location = new System.Drawing.Point(395, 72);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(36, 36);
            this.btnLoadImage.TabIndex = 56;
            this.btnLoadImage.Text = " ";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // textBoxBasePath
            // 
            this.textBoxBasePath.Location = new System.Drawing.Point(3, 23);
            this.textBoxBasePath.Name = "textBoxBasePath";
            this.textBoxBasePath.Size = new System.Drawing.Size(386, 21);
            this.textBoxBasePath.TabIndex = 52;
            // 
            // btnSetPath
            // 
            this.btnSetPath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSetPath.BackgroundImage")));
            this.btnSetPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSetPath.Font = new System.Drawing.Font("나눔바른고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSetPath.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSetPath.Location = new System.Drawing.Point(395, 8);
            this.btnSetPath.Name = "btnSetPath";
            this.btnSetPath.Size = new System.Drawing.Size(36, 36);
            this.btnSetPath.TabIndex = 51;
            this.btnSetPath.UseVisualStyleBackColor = true;
            this.btnSetPath.Click += new System.EventHandler(this.btnSetPath_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOK.BackgroundImage")));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOK.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(395, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(36, 36);
            this.btnOK.TabIndex = 50;
            this.btnOK.Text = " ";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // listBoxImageFile
            // 
            this.listBoxImageFile.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.listBoxImageFile.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBoxImageFile.FormattingEnabled = true;
            this.listBoxImageFile.ItemHeight = 14;
            this.listBoxImageFile.Location = new System.Drawing.Point(3, 556);
            this.listBoxImageFile.Name = "listBoxImageFile";
            this.listBoxImageFile.Size = new System.Drawing.Size(428, 60);
            this.listBoxImageFile.TabIndex = 54;
            this.listBoxImageFile.Visible = false;
            this.listBoxImageFile.SelectedIndexChanged += new System.EventHandler(this.listBoxImageFile_SelectedIndexChanged);
            // 
            // ManualInspectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(434, 652);
            this.ControlBox = false;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.labelTitle);
            this.Font = new System.Drawing.Font("나눔바른고딕", 8.999999F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualInspectionWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ManualInspectionWindow";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ManualInspectionWindow_KeyDown);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSetPath;
        private System.Windows.Forms.Button btnInspection;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.ListBox listBoxImageFile;
        private System.Windows.Forms.TextBox textBoxBasePath;
        private System.Windows.Forms.ListView listViewImageFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}