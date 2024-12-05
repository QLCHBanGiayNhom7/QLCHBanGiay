using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main.BUS;
using Main.DAO;
using Main.DTO;
using Sunny.UI;

namespace Main.GUI
{
    public partial class frmQLHoaDon : Form
    {
        HoaDon HoaDonBUS;
        bool them, sua = false;
        public frmQLHoaDon()
        {
            InitializeComponent();
            HoaDonBUS = new HoaDon();
            LoadData();
        }

        private void dgvQLHoaDon_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvQLHoaDon.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvQLHoaDon.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvQLHoaDon.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        HoaDonBUS hoaDonBUS = new HoaDonBUS();

        private void LoadHoaDon()
        {
            List<HoaDonDTO> hoaDonList = hoaDonBUS.GetAllHoaDon();

            if (hoaDonList != null && hoaDonList.Count > 0)
            {
                dgvQLHoaDon.DataSource = hoaDonList;
            }
            else
            {
                MessageBox.Show("Không có hóa đơn nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadData()
        {
            //Đặt lại định dạng cho DataGridView
            dgvQLHoaDon.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvQLHoaDon.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvQLHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvQLHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
            dgvQLHoaDon.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;

            // Lấy danh sách hóa đơn từ BUS
            List<HoaDonDTO> hoaDonList = hoaDonBUS.GetAllHoaDon();
            if (hoaDonList == null || hoaDonList.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu hoá đơn để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Gán dữ liệu vào DataGridView
            dgvQLHoaDon.DataSource = hoaDonList;


            //// Đặt lại lựa chọn
            foreach (DataGridViewRow row in dgvQLHoaDon.Rows)
            {
                row.Selected = false;
            }

            txt_SoHD.Text = "";
            txt_KhachHang.Text = "";
            txt_MaKM.Text = "";
            txt_NVPT.Text = "";
            txt_TongTien.Text = "";
            dtPicker_QLHD.Text = "";

            // Gán lại header cho các cột
            dgvQLHoaDon.Columns["MaHD"].HeaderText = "Mã Hoá Đơn";
            dgvQLHoaDon.Columns["NgayLapHD"].HeaderText = "Ngày lập hoá đơn";
            dgvQLHoaDon.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvQLHoaDon.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvQLHoaDon.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvQLHoaDon.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";
        }
        
        



        private bool CheckValid()
        {
            // Kiểm tra Mã khách hàng
            if (string.IsNullOrWhiteSpace(txt_KhachHang.Text) || !int.TryParse(txt_KhachHang.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập mã khách hàng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_KhachHang.Focus();
                return false;
            }

            // Kiểm tra Ngày lập hóa đơn
            if (dtPicker_QLHD.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày lập hóa đơn không được lớn hơn ngày hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtPicker_QLHD.Focus();
                return false;
            }

            // Kiểm tra Tổng tiền
            if (string.IsNullOrWhiteSpace(txt_TongTien.Text) || !decimal.TryParse(txt_TongTien.Text, out decimal tongTien) || tongTien <= 0)
            {
                MessageBox.Show("Vui lòng nhập tổng tiền hợp lệ (lớn hơn 0)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_TongTien.Focus();
                return false;
            }

            // Kiểm tra Mã nhân viên
            if (string.IsNullOrWhiteSpace(txt_NVPT.Text) || !int.TryParse(txt_NVPT.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_NVPT.Focus();
                return false;
            }

            // Kiểm tra Mã hóa đơn
            if (string.IsNullOrWhiteSpace(txt_SoHD.Text) || !int.TryParse(txt_SoHD.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_SoHD.Focus();
                return false;
            }

            // Kiểm tra Mã khuyến mãi
            if (string.IsNullOrWhiteSpace(txt_MaKM.Text) || !int.TryParse(txt_MaKM.Text, out _))
            {
                MessageBox.Show("Vui lòng nhập mã khuyến mãi hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_MaKM.Focus();
                return false;
            }

            // Nếu tất cả kiểm tra hợp lệ
            return true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                string maHD = txt_SoHD.Text.Trim();
                if (string.IsNullOrEmpty(maHD))
                {
                    MessageBox.Show("Số hóa đơn không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng hàm nếu có lỗi
                }

                string maNV = txt_NVPT.Text.Trim();
                if (string.IsNullOrEmpty(maNV))
                {
                    MessageBox.Show("Mã nhân viên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng hàm nếu có lỗi
                }

                string maKH = txt_KhachHang.Text.Trim();
                if (string.IsNullOrEmpty(maKH))
                {
                    MessageBox.Show("Mã khách hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng hàm nếu có lỗi
                }

                decimal tongTien;
                if (!decimal.TryParse(txt_TongTien.Text.Trim(), out tongTien))
                {
                    MessageBox.Show("Tổng tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Dừng hàm nếu có lỗi
                }

                DateTime ngayLapHD = dtPicker_QLHD.Value;

                string maKM = txt_MaKM.Text.Trim(); // Nếu có mã khuyến mãi thì kiểm tra
                if (string.IsNullOrEmpty(maKM))
                {
                    maKM = null; // Nếu không có mã khuyến mãi thì gán null hoặc có thể bỏ qua nếu trường không bắt buộc
                }

                
                HoaDonDTO hoaDon = new HoaDonDTO
                {
                    MaHD = txt_SoHD.Text,
                    NgayLapHD = dtPicker_QLHD.Value,  // DateTimePicker
                    TongTien = Convert.ToDecimal(txt_TongTien.Text),
                    MaNV = txt_NVPT.Text,
                    MaKH = txt_KhachHang.Text,
                    MaKM = txt_MaKM.Text
                };
                HoaDonBUS hoaDonBUS = new HoaDonBUS();

                if (hoaDonBUS.AddHoaDon(hoaDon))
                {
                    MessageBox.Show("Thêm hóa đơn thành công!");
                    LoadHoaDon(); // Gọi lại danh sách hóa đơn (nếu có)
                }
                else
                {
                    MessageBox.Show("Thêm hóa đơn thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            txt_KhachHang.Clear();
            txt_MaKM.Clear();
            txt_NVPT.Clear();
            txt_SoHD.Clear();
            txt_TongTien.Clear();
            txt_KhachHang.Focus();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            btn_Delete.Enabled = false;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            txt_SoHD.Enabled = dtPicker_QLHD.Enabled = txt_KhachHang.Enabled = txt_MaKM.Enabled =
                txt_NVPT.Enabled = txt_TongTien.Enabled = true;
            sua = true;
            them = false;
        }

        //private void btn_Save_Click(object sender, EventArgs e)
        //{
        //    if (CheckValid()) // Kiểm tra tính hợp lệ của dữ liệu
        //    {
        //        try
        //        {
        //            // Tạo đối tượng HoaDonDTO và truyền dữ liệu từ các TextBox
        //            HoaDonDTO hoaDonDTO = new HoaDonDTO
        //            {
        //                MaHD = txt_SoHD.Text, // Kiểu string
        //                NgayLapHD = dtPicker_QLHD.Value,
        //                TongTien = decimal.Parse(txt_TongTien.Text),
        //                MaNV = txt_NVPT.Text, // Kiểu string
        //                MaKH = txt_KhachHang.Text, // Kiểu string
        //                MaKM = string.IsNullOrEmpty(txt_MaKM.Text) ? null : txt_MaKM.Text // Kiểm tra null hoặc rỗng
        //            };

        //            if (them) // Nếu là thêm hóa đơn
        //            {
        //                bool result = hoaDonBUS.AddHoaDon(hoaDonDTO); // Gọi AddHoaDon từ BUS
        //                if (result)
        //                {
        //                    MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    them = false;
        //                    LoadData();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Thêm hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //            else if (sua) // Nếu là sửa hóa đơn
        //            {
        //                bool result = hoaDonBUS.UpdateHoaDon(hoaDonDTO); // Gọi UpdateHoaDon từ BUS
        //                if (result)
        //                {
        //                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    sua = false;
        //                    LoadData();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Cập nhật hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng kiểm tra dữ liệu nhập vào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}


        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            txtTim.Text = null;
            txt_KhachHang.Text = null;
            txt_SoHD.Text = null;
            txt_NVPT.Text = null;
            txt_TongTien.Text = null;
            txt_MaKM.Text = null;
            txt_SoHD.Focus();
            btn_Delete.Enabled = true;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string maHD = txtTim.Text;
            HoaDonDTO hoaDon = hoaDonBUS.SearchHoaDonByMaHD(maHD);

            if (hoaDon != null)
            {
                txt_SoHD.Text = hoaDon.MaHD;
                dtPicker_QLHD.Text = hoaDon.NgayLapHD.ToString("dd/MM/yyyy");
                txt_TongTien.Text = hoaDon.TongTien.ToString();
                txt_NVPT.Text = hoaDon.MaNV;
                txt_KhachHang.Text = hoaDon.MaKH;
                txt_MaKM.Text = hoaDon.MaKM;

                // Create a list with the single HoaDonDTO and bind to DataGridView
                List<HoaDonDTO> hoaDonList = new List<HoaDonDTO> { hoaDon };
                dgvQLHoaDon.DataSource = hoaDonList; // Bind the list to the DataGridView
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn.");
            }
        }

        private void dgvQLHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQLHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvQLHoaDon.SelectedRows[0];

                txt_SoHD.Text = selectedRow.Cells["MaHD"].Value.ToString();
                txt_KhachHang.Text = selectedRow.Cells["MaKH"].Value.ToString();
                txt_MaKM.Text = selectedRow.Cells["MaKM"].Value?.ToString() ?? "";
                dtPicker_QLHD.Value = Convert.ToDateTime(selectedRow.Cells["NgayLapHD"].Value);
                txt_NVPT.Text = selectedRow.Cells["MaNV"].Value.ToString();
                txt_TongTien.Text = selectedRow.Cells["TongTien"].Value.ToString();
            }
            else
            {
                txt_SoHD.Text = "";
                txt_TongTien.Text = "";
                txt_MaKM.Text = "";
                txt_NVPT.Text = "";
                dtPicker_QLHD.Text = "";
                txt_KhachHang.Text = "";
            }    

        }

        private void dgvQLHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmQLHoaDon_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        

    }
}
