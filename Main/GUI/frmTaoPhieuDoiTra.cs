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

namespace Main.GUI
{
    public partial class frmTaoPhieuDoiTra : Form
    {
        private string maHD;
        private ChiTietHoaDonBUS chiTietHoaDonBUS = new ChiTietHoaDonBUS();
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private HoaDonBUS hoaDonBUS = new HoaDonBUS();
        private KhoBUS khoBUS = new KhoBUS();
        public int soluongsp;
        public frmTaoPhieuDoiTra(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
        }

        private void frmTaoPhieuDoiTra_Load(object sender, EventArgs e)
        {
            LoadSanPham();
        }
        private void LoadSanPham()
        {
            // Lấy danh sách sản phẩm của hóa đơn
            var sanPhamList = chiTietHoaDonBUS.GetSanPhamByMaHD(maHD);

            // Binding dữ liệu vào DataGridView
            dgvSanPham.DataSource = sanPhamList;
        }


        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                // Lấy thông tin của sản phẩm trong dòng được click
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                // Lấy mã sản phẩm và số lượng
                string maSP = row.Cells["MaSP"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal giaban = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                soluongsp = soLuong;
                // Hiển thị số lượng vào TextBox
                txtSoLuongTra.Text = soLuong.ToString();
                txtMaSPTra.Text = maSP; // (nếu bạn cần mã sản phẩm)
                txtGiaBan.Text = giaban.ToString();
            }
        }

        private void btnTraHang_Click(object sender, EventArgs e)
        {
            // Lấy thông tin sản phẩm trả
            string maSPTra = txtMaSPTra.Text;
            int soLuongTra = Convert.ToInt32(txtSoLuongTra.Text);

            // Lấy thông tin sản phẩm đổi (nếu có)
            string maSPDoi = txtMaSPDoi.Text;
            int soLuongDoi = Convert.ToInt32(txtSoLuongDoi.Text);
            decimal giaBan=Convert.ToDecimal(txtGiaBan.Text);

            // Kiểm tra tính hợp lệ của số lượng trả và số lượng đổi
            if (soLuongTra <= 0 || (soLuongDoi < 0))
            {
                MessageBox.Show("Số lượng trả hoặc số lượng đổi không hợp lệ.");
                return;
            }

            // Cập nhật chi tiết hóa đơn với số lượng trả
            bool isUpdatedCTHD = chiTietHoaDonBUS.CapNhatSoLuongTra(maHD, maSPTra, soLuongTra);
            if (!isUpdatedCTHD)
            {
                MessageBox.Show("Cập nhật chi tiết hóa đơn thất bại!");
                return;
            }

            // Cập nhật kho (giảm số lượng sản phẩm trả về kho)
            bool isUpdatedKho = khoBUS.CapNhatKho(maSPTra, soLuongTra, true); // true -> là trả lại kho
            if (!isUpdatedKho)
            {
                MessageBox.Show("Cập nhật kho thất bại!");
                return;
            }

            // Nếu có đổi thì thực hiện xử lý sản phẩm đổi
            if (soLuongDoi > 0)
            {
                bool isAddedCTHDDoi = chiTietHoaDonBUS.ThemChiTietHoaDon(maHD, maSPDoi, soLuongDoi, giaBan);
                // Nếu sản phẩm đổi khác với sản phẩm trả, cập nhật chi tiết hóa đơn với sản phẩm đổi
                if(maSPDoi==maSPTra)
                {
                    bool isUpdatedCTHDDoi = chiTietHoaDonBUS.CapNhatSoLuongDoi(maHD, maSPDoi, soLuongDoi);
                    if (!isUpdatedCTHDDoi)
                    {
                        MessageBox.Show("Cập nhật chi tiết hóa đơn (đổi) thất bại!");
                        return;
                    }
                }    
                // Cập nhật kho (tăng số lượng sản phẩm đổi vào kho)
                bool isUpdatedKhoDoi = khoBUS.CapNhatKho(maSPDoi, soLuongDoi, false); // false -> là sản phẩm đổi
                if (!isUpdatedKhoDoi)
                {
                    MessageBox.Show("Cập nhật kho (đổi) thất bại!");
                    return;
                }
            }

            // Kiểm tra số lượng sản phẩm trả nếu bằng 0 thì xóa sản phẩm khỏi chi tiết hóa đơn
            if (soLuongTra == soluongsp)
            {
                // Xóa sản phẩm khỏi chi tiết hóa đơn nếu số lượng trả = 0
                bool isRemovedCTHD = chiTietHoaDonBUS.XoaChiTietHoaDon(maHD, maSPTra);
                if (!isRemovedCTHD)
                {
                    MessageBox.Show("Xóa sản phẩm khỏi chi tiết hóa đơn thất bại!");
                    return;
                }
            }

            // Cập nhật lại tổng tiền hóa đơn sau khi trả và (nếu có) đổi sản phẩm
            bool isUpdatedHoaDon = hoaDonBUS.CapNhatTongTien(maHD);
            if (!isUpdatedHoaDon)
            {
                MessageBox.Show("Cập nhật hóa đơn thất bại!");
                return;
            }

            MessageBox.Show("Đổi và trả sản phẩm thành công!");

            // Cập nhật lại DataGridView sau khi thực hiện đổi và trả
            LoadSanPham();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string masp = txtSearchMaSP.Text.Trim();
            SanPhamBUS sanPhamBUS = new SanPhamBUS();

            if (!string.IsNullOrEmpty(maHD))
            {
                SanPhamDTO sanPham = sanPhamBUS.SearchSanPhamByMaSP(masp);

                if (sanPham != null)
                {
                    List<SanPhamDTO> result = new List<SanPhamDTO> { sanPham };
                    dgvSanPhamDoi.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn với mã này!", "Thông báo");
                    LoadSanPhamToDataGridView(); // Tải lại danh sách hóa đơn
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!", "Thông báo");
            }
        }

        private void LoadSanPhamToDataGridView()
        {
            HoaDonBUS hoaDonBUS = new HoaDonBUS();
            List<HoaDonDTO> hoaDonList = hoaDonBUS.GetAllHoaDon();

            dgvSanPhamDoi.DataSource = hoaDonList; // DataGridView tự động hiển thị các thuộc tính trong DTO
            dgvSanPhamDoi.Columns["MaSP"].HeaderText = "MaSP";
            dgvSanPhamDoi.Columns["TenSP"].HeaderText = "TenSP";
            dgvSanPhamDoi.Columns["Mau"].HeaderText = "Mau";
            dgvSanPhamDoi.Columns["Size"].HeaderText = "Size";
            dgvSanPhamDoi.Columns["ThuongHieu"].HeaderText = "ThuongHieu";
            dgvSanPhamDoi.Columns["GiaBan"].HeaderText = "GiaBan";
        }
    }
}
