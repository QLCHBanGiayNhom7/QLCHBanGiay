namespace Main.GUI
{
    partial class frmTrangChu
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
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.White;
            this.Header.Controls.Add(this.uiImageButton1);
            this.Header.FillColor = System.Drawing.Color.White;
            this.Header.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Header.FillDisableColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(426, 35);
            this.Header.Size = new System.Drawing.Size(960, 103);
            // 
            // Aside
            // 
            this.Aside.Size = new System.Drawing.Size(426, 542);
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiImageButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiImageButton1.Image = global::Main.Properties.Resources.logi;
            this.uiImageButton1.Location = new System.Drawing.Point(873, 0);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(87, 103);
            this.uiImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.uiImageButton1.TabIndex = 0;
            this.uiImageButton1.TabStop = false;
            // 
            // frmTrangChu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1386, 577);
            this.Name = "frmTrangChu";
            this.Text = "TRANG CHỦ";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIImageButton uiImageButton1;
    }
}