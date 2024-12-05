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
using Main.DTO;
using Sunny.UI;

namespace Main.GUI
{
    public partial class frmPhieuDoiTraHang : UIPage
    {
        public frmPhieuDoiTraHang()
        {
            InitializeComponent();
        }

        private void LoadHoaDonToDataGridView()
        {
            dgvHoaDon.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvHoaDon.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvHoaDon.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
            dgvHoaDon.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;


            HoaDonBUS hoaDonBUS = new HoaDonBUS();
            List<HoaDonDTO> hoaDonList = hoaDonBUS.GetAllHoaDon();

            dgvHoaDon.DataSource = hoaDonList; // DataGridView tự động hiển thị các thuộc tính trong DTO
            dgvHoaDon.Columns["MaHD"].HeaderText = "Mã Hóa Đơn";
            dgvHoaDon.Columns["NgayLapHD"].HeaderText = "Ngày Lập HĐ";
            dgvHoaDon.Columns["TongTien"].HeaderText = "Tổng Tiền";
            dgvHoaDon.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
            dgvHoaDon.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvHoaDon.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";
        }

        private void frmPhieuDoiTraHang_Load(object sender, EventArgs e)
        {
            LoadHoaDonToDataGridView();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string maHD = txtSearchMaHD.Text.Trim();
            HoaDonBUS hoaDonBUS = new HoaDonBUS();

            if (!string.IsNullOrEmpty(maHD))
            {
                HoaDonDTO hoaDon = hoaDonBUS.SearchHoaDonByMaHD(maHD);

                if (hoaDon != null)
                {
                    List<HoaDonDTO> result = new List<HoaDonDTO> { hoaDon };
                    dgvHoaDon.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn với mã này!", "Thông báo");
                    LoadHoaDonToDataGridView(); // Tải lại danh sách hóa đơn
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!", "Thông báo");
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvHoaDon.Rows[e.RowIndex];
                string maKH = selectedRow.Cells["MaKH"].Value.ToString();
            }
        }
        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value.ToString();

                // Mở form Đổi Trả Hàng và truyền mã hóa đơn
                frmTaoPhieuDoiTra frmDoiTra = new frmTaoPhieuDoiTra(maHD);
                frmDoiTra.ShowDialog(); // Mở form Đổi Trả Hàng
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để đổi trả.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
