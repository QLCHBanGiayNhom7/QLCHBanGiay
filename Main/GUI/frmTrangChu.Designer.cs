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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrangChu));
            this.lblTenDangNhap = new Sunny.UI.UILabel();
            this.uiImageButton1 = new Sunny.UI.UIImageButton();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.White;
            this.Header.Controls.Add(this.lblTenDangNhap);
            this.Header.Controls.Add(this.uiImageButton1);
            this.Header.FillColor = System.Drawing.Color.MistyRose;
            this.Header.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Header.FillColorGradient = true;
            this.Header.FillColorGradientDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.Header.FillDisableColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(426, 55);
            this.Header.Size = new System.Drawing.Size(1546, 51);
            // 
            // Aside
            // 
            this.Aside.BackColor = System.Drawing.Color.LightPink;
            this.Aside.FillColor = System.Drawing.Color.LightPink;
            this.Aside.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aside.ForeColor = System.Drawing.Color.Black;
            this.Aside.HoverColor = System.Drawing.Color.LavenderBlush;
            this.Aside.Location = new System.Drawing.Point(0, 55);
            this.Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Aside.SelectedForeColor = System.Drawing.Color.White;
            this.Aside.SelectedHighColor = System.Drawing.Color.White;
            this.Aside.Size = new System.Drawing.Size(426, 1313);
            // 
            // lblTenDangNhap
            // 
            this.lblTenDangNhap.BackColor = System.Drawing.Color.MistyRose;
            this.lblTenDangNhap.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTenDangNhap.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenDangNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.lblTenDangNhap.Location = new System.Drawing.Point(1329, 0);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(150, 51);
            this.lblTenDangNhap.TabIndex = 1;
            this.lblTenDangNhap.Text = "uiLabel1";
            this.lblTenDangNhap.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiImageButton1
            // 
            this.uiImageButton1.BackColor = System.Drawing.Color.MistyRose;
            this.uiImageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiImageButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.uiImageButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiImageButton1.Image = global::Main.Properties.Resources.logo;
            this.uiImageButton1.Location = new System.Drawing.Point(1479, 0);
            this.uiImageButton1.Name = "uiImageButton1";
            this.uiImageButton1.Size = new System.Drawing.Size(67, 51);
            this.uiImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.uiImageButton1.TabIndex = 0;
            this.uiImageButton1.TabStop = false;
            this.uiImageButton1.Text = null;
            // 
            // frmTrangChu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1972, 1368);
            this.ControlBoxForeColor = System.Drawing.Color.Black;
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrangChu";
            this.Padding = new System.Windows.Forms.Padding(0, 55, 0, 0);
            this.RectColor = System.Drawing.Color.White;
            this.Text = "TRANG CHỦ";
            this.TitleColor = System.Drawing.Color.LightPink;
            this.TitleFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleForeColor = System.Drawing.Color.Black;
            this.TitleHeight = 55;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiImageButton1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIImageButton uiImageButton1;
        private Sunny.UI.UILabel lblTenDangNhap;
    }
}