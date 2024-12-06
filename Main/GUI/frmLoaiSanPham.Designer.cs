namespace Main.GUI
{
    partial class frmLoaiSanPham
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_lsp = new Sunny.UI.UIDataGridView();
            this.btn_Tim = new Sunny.UI.UIButton();
            this.txt_TenLoai = new Sunny.UI.UITextBox();
            this.txt_MaLoai = new Sunny.UI.UITextBox();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rdo_Nam = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rdo_Nu = new Guna.UI2.WinForms.Guna2RadioButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_Tim = new Sunny.UI.UIIPTextBox();
            this.btn_Luu = new Sunny.UI.UIButton();
            this.btn_Sua = new Sunny.UI.UIButton();
            this.btn_Them = new Sunny.UI.UIButton();
            this.btn_Xoa = new Sunny.UI.UIButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Reset = new Sunny.UI.UIButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lsp)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_lsp
            // 
            this.dgv_lsp.AllowUserToAddRows = false;
            this.dgv_lsp.AllowUserToDeleteRows = false;
            this.dgv_lsp.AllowUserToOrderColumns = true;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgv_lsp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.dgv_lsp.BackgroundColor = System.Drawing.Color.White;
            this.dgv_lsp.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_lsp.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.dgv_lsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_lsp.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgv_lsp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_lsp.EnableHeadersVisualStyles = false;
            this.dgv_lsp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_lsp.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.dgv_lsp.Location = new System.Drawing.Point(4, 255);
            this.dgv_lsp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgv_lsp.Name = "dgv_lsp";
            this.dgv_lsp.ReadOnly = true;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_lsp.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
            this.dgv_lsp.RowHeadersWidth = 51;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_lsp.RowsDefaultCellStyle = dataGridViewCellStyle25;
            this.dgv_lsp.SelectedIndex = -1;
            this.dgv_lsp.Size = new System.Drawing.Size(1151, 345);
            this.dgv_lsp.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.dgv_lsp.TabIndex = 3;
            this.dgv_lsp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_lsp_CellContentClick);
            // 
            // btn_Tim
            // 
            this.btn_Tim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Tim.FillColor = System.Drawing.Color.LightPink;
            this.btn_Tim.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tim.Location = new System.Drawing.Point(944, 5);
            this.btn_Tim.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Tim.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Tim.Name = "btn_Tim";
            this.btn_Tim.Radius = 14;
            this.btn_Tim.Size = new System.Drawing.Size(197, 32);
            this.btn_Tim.TabIndex = 1;
            this.btn_Tim.Text = "Tìm";
            this.btn_Tim.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_Tim.Click += new System.EventHandler(this.btn_Tim_Click);
            // 
            // txt_TenLoai
            // 
            this.txt_TenLoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_TenLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txt_TenLoai.Location = new System.Drawing.Point(867, 8);
            this.txt_TenLoai.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txt_TenLoai.MinimumSize = new System.Drawing.Size(1, 25);
            this.txt_TenLoai.Name = "txt_TenLoai";
            this.txt_TenLoai.Padding = new System.Windows.Forms.Padding(8);
            this.txt_TenLoai.Radius = 15;
            this.txt_TenLoai.ShowText = false;
            this.txt_TenLoai.Size = new System.Drawing.Size(241, 38);
            this.txt_TenLoai.TabIndex = 30;
            this.txt_TenLoai.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_TenLoai.Watermark = "";
            // 
            // txt_MaLoai
            // 
            this.txt_MaLoai.ButtonFillColor = System.Drawing.Color.LightPink;
            this.txt_MaLoai.ButtonStyleInherited = false;
            this.txt_MaLoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_MaLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txt_MaLoai.Location = new System.Drawing.Point(293, 8);
            this.txt_MaLoai.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txt_MaLoai.MinimumSize = new System.Drawing.Size(1, 25);
            this.txt_MaLoai.Name = "txt_MaLoai";
            this.txt_MaLoai.Padding = new System.Windows.Forms.Padding(8);
            this.txt_MaLoai.Radius = 15;
            this.txt_MaLoai.ScrollBarColor = System.Drawing.Color.LightPink;
            this.txt_MaLoai.ScrollBarStyleInherited = false;
            this.txt_MaLoai.ShowText = false;
            this.txt_MaLoai.Size = new System.Drawing.Size(240, 38);
            this.txt_MaLoai.TabIndex = 29;
            this.txt_MaLoai.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.txt_MaLoai.Watermark = "";
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel1.Location = new System.Drawing.Point(4, 0);
            this.uiLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(201, 35);
            this.uiLabel1.TabIndex = 24;
            this.uiLabel1.Text = "Mã Loại";
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel2.Location = new System.Drawing.Point(4, 68);
            this.uiLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(201, 35);
            this.uiLabel2.TabIndex = 25;
            this.uiLabel2.Text = "Giới Tính";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.rdo_Nam, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.rdo_Nu, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(290, 72);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(225, 60);
            this.tableLayoutPanel3.TabIndex = 31;
            // 
            // rdo_Nam
            // 
            this.rdo_Nam.AutoSize = true;
            this.rdo_Nam.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdo_Nam.CheckedState.BorderThickness = 0;
            this.rdo_Nam.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdo_Nam.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdo_Nam.CheckedState.InnerOffset = -4;
            this.rdo_Nam.Location = new System.Drawing.Point(3, 4);
            this.rdo_Nam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdo_Nam.Name = "rdo_Nam";
            this.rdo_Nam.Size = new System.Drawing.Size(67, 24);
            this.rdo_Nam.TabIndex = 0;
            this.rdo_Nam.Text = "Nam";
            this.rdo_Nam.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdo_Nam.UncheckedState.BorderThickness = 2;
            this.rdo_Nam.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdo_Nam.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // rdo_Nu
            // 
            this.rdo_Nu.AutoSize = true;
            this.rdo_Nu.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdo_Nu.CheckedState.BorderThickness = 0;
            this.rdo_Nu.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rdo_Nu.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rdo_Nu.CheckedState.InnerOffset = -4;
            this.rdo_Nu.Location = new System.Drawing.Point(115, 4);
            this.rdo_Nu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rdo_Nu.Name = "rdo_Nu";
            this.rdo_Nu.Size = new System.Drawing.Size(54, 24);
            this.rdo_Nu.TabIndex = 1;
            this.rdo_Nu.Text = "Nữ";
            this.rdo_Nu.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rdo_Nu.UncheckedState.BorderThickness = 2;
            this.rdo_Nu.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rdo_Nu.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.18209F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.81791F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutPanel5.Controls.Add(this.btn_Tim, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.txt_Tim, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(4, 203);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1151, 42);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // txt_Tim
            // 
            this.txt_Tim.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.txt_Tim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txt_Tim.Location = new System.Drawing.Point(195, 8);
            this.txt_Tim.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.txt_Tim.MinimumSize = new System.Drawing.Size(1, 1);
            this.txt_Tim.Name = "txt_Tim";
            this.txt_Tim.Padding = new System.Windows.Forms.Padding(1);
            this.txt_Tim.Radius = 15;
            this.txt_Tim.ShowText = false;
            this.txt_Tim.Size = new System.Drawing.Size(625, 26);
            this.txt_Tim.TabIndex = 28;
            this.txt_Tim.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Luu
            // 
            this.btn_Luu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Luu.FillColor = System.Drawing.Color.LightPink;
            this.btn_Luu.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Luu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Luu.Location = new System.Drawing.Point(694, 5);
            this.btn_Luu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Luu.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Luu.Name = "btn_Luu";
            this.btn_Luu.Radius = 15;
            this.btn_Luu.Size = new System.Drawing.Size(192, 32);
            this.btn_Luu.TabIndex = 3;
            this.btn_Luu.Text = "Lưu";
            this.btn_Luu.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            // 
            // btn_Sua
            // 
            this.btn_Sua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Sua.FillColor = System.Drawing.Color.LightPink;
            this.btn_Sua.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Sua.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Sua.Location = new System.Drawing.Point(464, 5);
            this.btn_Sua.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Sua.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Sua.Name = "btn_Sua";
            this.btn_Sua.Radius = 15;
            this.btn_Sua.Size = new System.Drawing.Size(192, 32);
            this.btn_Sua.TabIndex = 2;
            this.btn_Sua.Text = "Sửa";
            this.btn_Sua.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_Sua.Click += new System.EventHandler(this.btn_Sua_Click);
            // 
            // btn_Them
            // 
            this.btn_Them.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Them.FillColor = System.Drawing.Color.LightPink;
            this.btn_Them.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Them.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Them.Location = new System.Drawing.Point(4, 5);
            this.btn_Them.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Them.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Them.Name = "btn_Them";
            this.btn_Them.Radius = 15;
            this.btn_Them.Size = new System.Drawing.Size(192, 32);
            this.btn_Them.TabIndex = 0;
            this.btn_Them.Text = "Thêm";
            this.btn_Them.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_Them.Click += new System.EventHandler(this.btn_Them_Click);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Xoa.FillColor = System.Drawing.Color.LightPink;
            this.btn_Xoa.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Xoa.Location = new System.Drawing.Point(234, 5);
            this.btn_Xoa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Xoa.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Radius = 15;
            this.btn_Xoa.Size = new System.Drawing.Size(192, 32);
            this.btn_Xoa.TabIndex = 1;
            this.btn_Xoa.Text = "Xóa";
            this.btn_Xoa.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiLabel5.Location = new System.Drawing.Point(578, 0);
            this.uiLabel5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(201, 35);
            this.uiLabel5.TabIndex = 28;
            this.uiLabel5.Text = "Tên Loại";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.Controls.Add(this.btn_Reset, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Luu, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Sua, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Them, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Xoa, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 151);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1151, 42);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // btn_Reset
            // 
            this.btn_Reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Reset.FillColor = System.Drawing.Color.LightPink;
            this.btn_Reset.FillColor2 = System.Drawing.Color.LightPink;
            this.btn_Reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Reset.Location = new System.Drawing.Point(924, 5);
            this.btn_Reset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_Reset.MinimumSize = new System.Drawing.Size(1, 1);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Radius = 15;
            this.btn_Reset.Size = new System.Drawing.Size(194, 32);
            this.btn_Reset.TabIndex = 4;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.TipsFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.txt_TenLoai, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txt_MaLoai, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.uiLabel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.uiLabel2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.uiLabel5, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1151, 136);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgv_lsp, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.31078F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.666667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1159, 605);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // frmLoaiSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1159, 605);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmLoaiSanPham";
            this.Text = "frmLoaiSanPham";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_lsp)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIDataGridView dgv_lsp;
        private Sunny.UI.UIButton btn_Tim;
        private Sunny.UI.UITextBox txt_TenLoai;
        private Sunny.UI.UITextBox txt_MaLoai;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Guna.UI2.WinForms.Guna2RadioButton rdo_Nam;
        private Guna.UI2.WinForms.Guna2RadioButton rdo_Nu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Sunny.UI.UIIPTextBox txt_Tim;
        private Sunny.UI.UIButton btn_Luu;
        private Sunny.UI.UIButton btn_Sua;
        private Sunny.UI.UIButton btn_Them;
        private Sunny.UI.UIButton btn_Xoa;
        private Sunny.UI.UILabel uiLabel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Sunny.UI.UIButton btn_Reset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}