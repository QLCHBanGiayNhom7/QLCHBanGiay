using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main.BUS;
using Main.DAO;
using Main.DTO;

namespace Main.GUI
{
    public partial class frmQLNhanVien : Form
    {
        private NhanVienBUS nhanVienBUS;
        public frmQLNhanVien()
        {
            InitializeComponent();
            nhanVienBUS = new NhanVienBUS();
            TextboxDisable();
            LoadNV();
            dtpNS.ValueChanged -= dtpNS_ValueChanged;
        }

        private void frmQLNhanVien_Load(object sender, EventArgs e)
        {
            LoadNV();

        }

        private void LoadNV()
        {
            dgvNV.ColumnHeadersVisible = true;
            try
            {
                var dsNhanVien = nhanVienBUS.GetNhanVien();

                if (dsNhanVien != null && dsNhanVien.Count > 0)
                {
                    dgvNV.DataSource = dsNhanVien;

                  
                    dgvNV.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                    dgvNV.Columns["TenNV"].HeaderText = "Tên Nhân Viên";
                    dgvNV.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
                    dgvNV.Columns["GioiTinh"].HeaderText = "Giới Tính";
                    dgvNV.Columns["NoiSinh"].HeaderText = "Nơi Sinh";
                    dgvNV.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                    dgvNV.Columns["CCCD"].HeaderText = "CCCD";
                    dgvNV.Columns["ChucVu"].HeaderText = "Chức Vụ";

                    // Ẩn cột không cần thiết
                    dgvNV.Columns["IdNguoiDung"].Visible = false;
                    dgvNV.Columns["NguoiDung"].Visible = false;
                    // Tùy chỉnh kiểu tiêu đề
                    dgvNV.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
                    dgvNV.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgvNV.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                    dgvNV.EnableHeadersVisualStyles = false;
                    dgvNV.ColumnHeadersHeight = 50;
                    // Tùy chỉnh hover
                    //dgvNV.DefaultCellStyle.SelectionBackColor = Color.Black;
                    //dgvNV.DefaultCellStyle.SelectionForeColor = Color.White;

                   
                    dgvNV.Columns["MaNV"].DisplayIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không có nhân viên nào để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void TextboxDisable()
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtNoiSinh.Enabled = false;
            txtSDT.Enabled = false;
            txtCV.Enabled = false;
            txtCCCD.Enabled = false;
            dtpNS.Enabled = false;
            rdoNam.Enabled = false;
            rdoNu.Enabled = false;
        }

        private void TextboxEnable()
        {
            txtTenNV.Enabled = true;
            txtNoiSinh.Enabled = true;
            txtSDT.Enabled = true;
            txtCV.Enabled = true;
            txtCCCD.Enabled = true;
            dtpNS.Enabled = true;
            rdoNam.Enabled = true;
            rdoNu.Enabled = true;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            TextboxDisable();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaNV.Text = string.Empty;
            txtNoiSinh.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtCV.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            dtpNS.Value = DateTime.Now;
            rdoNam.Checked = false;
            rdoNu.Checked = false;
            LoadNV();
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvNV.Rows[e.RowIndex];
                    txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                    txtTenNV.Text = row.Cells["TenNV"].Value.ToString();
                    dtpNS.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                    if (gioiTinh == "Nam")
                    {
                        rdoNam.Checked = true;
                    }
                    else if (gioiTinh == "Nữ")
                    {
                        rdoNu.Checked = true;
                    }

                    txtNoiSinh.Text = row.Cells["NoiSinh"].Value.ToString();
                    txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                    txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
                    txtCV.Text = row.Cells["ChucVu"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ ô: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMaNV.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã nhân viên.");
                    return;
                }

                List<int?> maNVList = new List<int?> { int.Parse(txtMaNV.Text) };
                string result = nhanVienBUS.DeleteNhanVien(maNVList);
                MessageBox.Show(result);
                LoadNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text;
                List<NhanVien> searchResults = nhanVienBUS.SearchNhanVien(keyword);
                dgvNV.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm nhân viên: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
           
            TextboxEnable();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaNV.Text = string.Empty;
            txtNoiSinh.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtCV.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtTenNV.Text = string.Empty;
            dtpNS.Value = DateTime.Now;
            rdoNam.Checked = false;
            rdoNu.Checked = false;  
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            TextboxEnable();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            dtpNS.ValueChanged += dtpNS_ValueChanged;
            if (string.IsNullOrEmpty(txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa!");
                return;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtCV.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                    return;
                }

                if (dtpNS.Value > DateTime.Now || dtpNS.Value == DateTimePicker.MinimumDateTime)
                {
                    MessageBox.Show("Ngày sinh không hợp lệ.");
                    return;
                }

                if (!rdoNam.Checked && !rdoNu.Checked)
                {
                    MessageBox.Show("Vui lòng chọn giới tính.");
                    return;
                }

                if (!string.IsNullOrEmpty(txtMaNV.Text))
                {
                    if (!int.TryParse(txtMaNV.Text, out int maNV))
                    {
                        MessageBox.Show("Mã nhân viên phải là số.");
                        return;
                    }

                    NhanVien nv = new NhanVien()
                    {
                        MaNV = maNV,
                        TenNV = txtTenNV.Text,
                        NgaySinh = dtpNS.Value,
                        GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
                        NoiSinh = txtNoiSinh.Text,
                        SoDienThoai = txtSDT.Text,
                        CCCD = txtCCCD.Text,
                        ChucVu = txtCV.Text
                    };

                    var result = nhanVienBUS.UpdateNhanVien(nv);

                    MessageBox.Show(result ? "Cập nhật nhân viên thành công!" : "Cập nhật nhân viên thất bại.");
                    LoadNV();
                }
                else
                {
                    NhanVien nv = new NhanVien()
                    {
                        TenNV = txtTenNV.Text,
                        NgaySinh = dtpNS.Value,
                        GioiTinh = rdoNam.Checked ? "Nam" : "Nữ",
                        NoiSinh = txtNoiSinh.Text,
                        SoDienThoai = txtSDT.Text,
                        CCCD = txtCCCD.Text,
                        ChucVu = txtCV.Text
                    };

                    var result = nhanVienBUS.AddNhanVien(nv);

                    MessageBox.Show(result ? "Thêm nhân viên thành công!" : "Thêm nhân viên thất bại.");
                    LoadNV();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void dgvNV_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvNV.Columns["IdNguoiDung"].Visible = false;

        }

        private void ShowMessageOnForm(string message)
        {
            // Tạo Label
            Label lblMessage = new Label();
            lblMessage.Text = message;

            // Cấu hình màu nền và màu chữ
            lblMessage.BackColor = Color.White; 
            lblMessage.ForeColor = Color.Black; 

            // Thay đổi kích thước chữ
            lblMessage.Font = new Font("Arial", 16, FontStyle.Bold); 

            // Đảm bảo label tự động thay đổi kích thước phù hợp với nội dung
            lblMessage.AutoSize = true;

            // Đặt border cho Label để dễ nhìn
            lblMessage.TextAlign = ContentAlignment.MiddleCenter; 

            // Đặt vị trí label ở giữa form
            lblMessage.Location = new Point((this.ClientSize.Width - lblMessage.Width) / 2, (this.ClientSize.Height - lblMessage.Height) / 2);

            // Thêm label vào Controls của form
            this.Controls.Add(lblMessage);

            // Đảm bảo label luôn ở trên các điều khiển khác
            lblMessage.BringToFront();

            // Đảm bảo form được cập nhật lại để hiển thị label
            this.Refresh();
            Timer timer = new Timer();
            timer.Interval = 3000; 
            timer.Tick += (sender, e) =>
            {
                this.Controls.Remove(lblMessage); 
                timer.Stop();
            };
            timer.Start();
        }




        private void dtpNS_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpNS.Value;
            int age = DateTime.Now.Year - selectedDate.Year;

            if (selectedDate.Date > DateTime.Now.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                ShowMessageOnForm("Nhân viên phải lớn hơn 18 tuổi!");
                dtpNS.Value = DateTime.Now.AddYears(-18).Date;
            }
        }

        private void dtpNS_Click(object sender, EventArgs e)
        {
            dtpNS.ValueChanged += dtpNS_ValueChanged;
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) 
            {
                e.Handled = true; 
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại không được quá 11 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Text = txtSDT.Text.Substring(0, 11);
                txtSDT.SelectionStart = txtSDT.Text.Length; 
            }
        }

        private void txtCCCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8) 
            {
                e.Handled = true;
            }
        }

        private void txtCCCD_TextChanged(object sender, EventArgs e)
        {
            if (txtCCCD.Text.Length > 12)
            {
                MessageBox.Show("CCCD không được quá 12 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Text = txtCCCD.Text.Substring(0, 12); 
                txtCCCD.SelectionStart = txtCCCD.Text.Length; 
            }
        }
    }
}
