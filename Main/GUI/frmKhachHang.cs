using Sunny.UI;
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
using Main.BUS;
using Main.DTO;

namespace Main.GUI
{
    public partial class frmKhachHang : UIPage
    {
        KhachHangBUS khBUS; 
        public frmKhachHang()
        {
            InitializeComponent();
            khBUS = new KhachHangBUS(); // Khởi tạo BUS
            LoadData();
            //txtKH.Enabled = false;
        }

        private void LoadData()
        {
            try
            {
                // Lấy danh sách khách hàng từ BUS
                List<KhachHangDTO> dsKhachHang = khBUS.GetKhachHang();
                // Kiểm tra nếu có dữ liệu thì hiển thị lên DataGridView
                if (dsKhachHang != null && dsKhachHang.Count > 0)
                {
                    dgv_kh.DataSource = dsKhachHang; // Gán dữ liệu cho DataGridView
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dgv_kh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_kh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_kh.Columns[0].Frozen = true;
            dgv_kh.ReadOnly = true;
        }





        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_kh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_kh.Rows[e.RowIndex];

                txtKH.Text = row.Cells["MaKH"].Value.ToString();
                txtTenKH.Text = row.Cells["TenKH"].Value.ToString();
                txt_Sdt.Text = row.Cells["SoDienThoai"].Value.ToString();
                if (row.Cells["NgaySinh"].Value != DBNull.Value)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                }
                else
                {
                    dtpNgaySinh.Value = DateTime.Now; // Hoặc bạn có thể đặt giá trị mặc định
                }

                // Cập nhật giới tính cho RadioButton
                string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                if (gioiTinh == "Nam")
                {
                    rdo_Nam.Checked = true;
                    rdo_Nu.Checked = false;
                }
                else if (gioiTinh == "Nữ")
                {
                    rdo_Nu.Checked = true;
                    rdo_Nam.Checked = false;
                }
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            var khachHangDTO = new KhachHangDTO
            {
                TenKH = txtTenKH.Text,
                SoDienThoai = txt_Sdt.Text,
                NgaySinh = dtpNgaySinh.Value,
                GioiTinh = rdo_Nam.Checked ? "Nam" : "Nữ"
            };

            if (khBUS.AddKhachHang(khachHangDTO))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Cập nhật lại danh sách
            }
            else
            {
                MessageBox.Show("Thêm khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            var khachHangDTO = new KhachHangDTO
            {
                //MaKH = int.Parse(txtKH.Text), // Giả định bạn đã nhập mã khách hàng
                //TenKH = txtTenKH.Text,
                //SoDienThoai = txt_Sdt.Text,
                //NgaySinh = dtpNgaySinh.Value,
                //GioiTinh = rdo_Nam.Checked ? "Nam" : "Nữ"
            };

            //if (khBUS.UpdateKhachHang(khachHangDTO))
            //{
            //    MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    LoadData(); // Cập nhật lại danh sách
            //}
            //else
            //{
            //    MessageBox.Show("Cập nhật khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
           

         
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtKH.Text, out int maKH))
            {
                if (khBUS.DeleteKhachHang(maKH))
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Cập nhật lại danh sách
                }
                else
                {
                    MessageBox.Show("Xóa khách hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mã khách hàng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string keyword = txt_TimKiem.Text.Trim();
            SearchData(keyword);
        }
        private void SearchData(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var filteredData = khBUS.GetKhachHang()
                       .Where(kh => kh.MaKH.ToString().Contains(keyword) ||
                                     kh.TenKH.Contains(keyword) ||
                                     kh.SoDienThoai.Contains(keyword))
                       .ToList();

                dgv_kh.DataSource = filteredData;
            }
            else
            {
                LoadData();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtKH.Text = string.Empty;
            txtTenKH.Text = string.Empty;
            txt_Sdt.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now; // Hoặc một giá trị mặc định khác
            rdo_Nam.Checked = false;
            rdo_Nu.Checked = false;
        }
    }
}
