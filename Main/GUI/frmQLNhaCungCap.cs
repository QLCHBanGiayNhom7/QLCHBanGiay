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
using Sunny.UI;

namespace Main.GUI
{
    public partial class frmQLNhaCungCap : UIPage
    {
        private NhaCungCapBUS nhaCungCapBUS;
        public frmQLNhaCungCap()
        {
            InitializeComponent();
            nhaCungCapBUS = new NhaCungCapBUS();
            TextboxDisable();
            LoadNCC();
        }

        private void frmQLNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadNCC();
        }

        private void LoadNCC()
        {
            try
            {
                dgvNCC.DataSource = nhaCungCapBUS.GetNhaCC();

                // Thiết lập tiêu đề cột
                dgvNCC.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
                dgvNCC.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                dgvNCC.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgvNCC.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";

                // Tùy chỉnh giao diện tiêu đề
                //dgvNCC.ColumnHeadersHeight = 50; 
                //dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
                //dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                //dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                //dgvNCC.EnableHeadersVisualStyles = false;
                dgvNCC.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
                dgvNCC.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
                dgvNCC.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;

                // Tùy chỉnh hover
                //dgvNCC.DefaultCellStyle.SelectionBackColor = Color.Black;
                //dgvNCC.DefaultCellStyle.SelectionForeColor = Color.White;

                // Đảm bảo cột Mã Nhà Cung Cấp xuất hiện đầu tiên
                dgvNCC.Columns["MaNCC"].DisplayIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách nhà cung cấp: " + ex.Message);
            }


        }

        private void TextboxDisable()
        {
            txtMaNCC.Enabled = false;
            txtTenNCC.Enabled = false;
            txtDC.Enabled = false;
            txtSDT.Enabled = false;
        }

        private void TextboxEnable()
        {
            txtTenNCC.Enabled = true;
            txtDC.Enabled = true;
            txtSDT.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            TextboxEnable();
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaNCC.Text = dgvNCC.Rows[e.RowIndex].Cells["MaNCC"].Value.ToString();
                txtTenNCC.Text = dgvNCC.Rows[e.RowIndex].Cells["TenNCC"].Value.ToString();
                txtDC.Text = dgvNCC.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                txtSDT.Text = dgvNCC.Rows[e.RowIndex].Cells["SoDienThoai"].Value.ToString();
            }
        }

        private void BtnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTK.Text.Trim();
            dgvNCC.DataSource = nhaCungCapBUS.SearchNhaCC(keyword);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maNCCList = dgvNCC.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(r => (int?)int.Parse(r.Cells["MaNCC"].Value.ToString())) 
                    .ToList();
            string result = nhaCungCapBUS.DeleteNhaCC(maNCCList);
            if (result == "Xóa nhà cung cấp thành công!")
            {
                MessageBox.Show(result);
                LoadNCC();  
            }
            else
            {
                MessageBox.Show(result);  
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            TextboxEnable();
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn một nhà cung cấp để sửa!");
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            TextboxDisable();
            txtMaNCC.Text = string.Empty;
            txtTenNCC.Text = string.Empty;
            txtDC.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtTK.Text = string.Empty;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            LoadNCC();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var tenNCC = txtTenNCC.Text.Trim();
            var diaChi = txtDC.Text.Trim();
            var soDienThoai = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (string.IsNullOrEmpty(txtMaNCC.Text)) // Thêm mới
            {
                var ncc = new NhaCungCap
                {
                    TenNCC = tenNCC,
                    DiaChi = diaChi,
                    SoDienThoai = soDienThoai
                };

                if (nhaCungCapBUS.AddNhaCC(ncc))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!");
                    LoadNCC();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            else // Cập nhật
            {
                var ncc = new NhaCungCap
                {
                    MaNCC = int.Parse(txtMaNCC.Text),
                    TenNCC = tenNCC,
                    DiaChi = diaChi,
                    SoDienThoai = soDienThoai
                };

                if (nhaCungCapBUS.UpdateNhaCC(ncc))
                {
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!");
                    LoadNCC();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            Reset();
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;  
            }
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {
            if (txtSDT.Text.Length > 11)
            {
                txtSDT.Text = txtSDT.Text.Substring(0, 11);  
                txtSDT.SelectionStart = txtSDT.Text.Length;  
            }
        }

        private void dgvNCC_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvNCC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvNCC.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvNCC.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
