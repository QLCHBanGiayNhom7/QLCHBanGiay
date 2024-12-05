using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Main.BUS;
using Main.DTO;
using Sunny.UI;
using System.IO;
using System.Diagnostics;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Main.DAO;
using iText.Kernel.Font;
namespace Main.GUI
{
    public partial class frmTaoPhieuDoiTra : Form
    {
        private string maHD;
        private ChiTietHoaDonBUS chiTietHoaDonBUS = new ChiTietHoaDonBUS();
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private HoaDonBUS hoaDonBUS = new HoaDonBUS();
        private KhoBUS khoBUS = new KhoBUS();
        private DoiTraHangBUS doiTraHangBUS = new DoiTraHangBUS();
        public int soluongsp;
        public KhachHangDTO kh = new KhachHangDTO();
        public frmTaoPhieuDoiTra(string maHD)
        {
            InitializeComponent();
            this.maHD = maHD;
            dgSanPhamDoi.CellValueChanged += new DataGridViewCellEventHandler(dgSanPhamDoi_CellValueChanged);
            dgvSanPhamTra.CellValueChanged += new DataGridViewCellEventHandler(dgvSanPhamTra_CellValueChanged);
            KhachHangBUS khachHangBUS = new KhachHangBUS();
            KhachHangDTO khs = khachHangBUS.GetKhachHangByMaHD(maHD);
            kh = khs;
        }

        private void frmTaoPhieuDoiTra_Load(object sender, EventArgs e)
        {
            if (dgvSanPhamTra.Columns.Count == 0) // Kiểm tra xem DataGridView đã có cột chưa
            {
                dgvSanPhamTra.Columns.Add("MaSP", "Mã Sản Phẩm");
                dgvSanPhamTra.Columns.Add("TenSP", "Tên Sản Phẩm");
                dgvSanPhamTra.Columns.Add("GiaBan", "Giá Bán");
                dgvSanPhamTra.Columns.Add("SoLuong", "Số lượng");
                dgvSanPhamTra.Columns.Add("ThanhTien", "Thành Tiền");
            }
            if (dgSanPhamDoi.Columns.Count == 0) // Kiểm tra xem DataGridView đã có cột chưa
            {
                dgSanPhamDoi.Columns.Add("MaSP", "Mã Sản Phẩm");
                dgSanPhamDoi.Columns.Add("TenSP", "Tên Sản Phẩm");
                dgSanPhamDoi.Columns.Add("GiaBan", "Giá Bán");
                dgSanPhamDoi.Columns.Add("SoLuong", "Số lượng");
                dgSanPhamDoi.Columns.Add("ThanhTien", "Thành Tiền");
            }
            LoadSanPham();
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSanPham.MultiSelect = true;
            dgvSanPhamDoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSanPhamDoi.MultiSelect = true;
            dgSanPhamDoi.CellValueChanged += new DataGridViewCellEventHandler(dgSanPhamDoi_CellValueChanged);
            dgvSanPhamTra.CellValueChanged += new DataGridViewCellEventHandler(dgvSanPhamTra_CellValueChanged);
            dgSanPhamDoi.RowsAdded += new DataGridViewRowsAddedEventHandler(dgSanPhamDoi_RowsAdded);
            dgvSanPhamTra.RowsAdded += new DataGridViewRowsAddedEventHandler(dgvSanPhamTra_RowsAdded);
        }
        private void LoadSanPham()
        {
            var sanPhamList = chiTietHoaDonBUS.GetSanPhamByMaHD(maHD);
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
                //txtSoLuongTra.Text = soLuong.ToString();
                //txtMaSPTra.Text = maSP; // (nếu bạn cần mã sản phẩm)
                //txtGiaBan.Text = giaban.ToString();
            }
        }

        private void btnTraHang_Click(object sender, EventArgs e)
        {
            try
            {
                decimal tongTienTra = 0; // Tổng tiền của các sản phẩm trả
                decimal tongTienDoi = 0; // Tổng tiền của các sản phẩm đổi

                // Lưu thông tin trả hàng vào database
                foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                {
                    if (row.Cells["MaSP"].Value == null || row.Cells["SoLuong"].Value == null)
                        continue; // Bỏ qua nếu dòng không hợp lệ

                    string maSP = row.Cells["MaSP"].Value.ToString();
                    int soLuongTra = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal giaBanTra = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                    string lyDo = "Sản phẩm trả";

                    // Tính tổng tiền sản phẩm trả
                    tongTienTra += soLuongTra * giaBanTra;

                    // Cập nhật kho: Thêm lại số lượng sản phẩm trả
                    bool isKhoUpdated = khoBUS.CapNhatKho(maSP, soLuongTra, true); // true: thêm lại vào kho
                    if (!isKhoUpdated)
                    {
                        MessageBox.Show($"Cập nhật kho thất bại cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Tạo đối tượng DoiTraHangDTO cho sản phẩm trả
                    DoiTraHangDTO doiTraHang = new DoiTraHangDTO(
                        maHDDT: maHD,       // Mã hóa đơn
                        maHD: maHD,         // Mã hóa đơn
                        maSP: maSP,         // Mã sản phẩm
                        soLuong: soLuongTra, // Số lượng sản phẩm trả
                        lyDo: lyDo,         // Lý do
                        isTra: true          // Đánh dấu là trả hàng
                    );

                    // Lưu thông tin sản phẩm trả vào bảng DoiTraHang
                    bool isDoiTraInserted = doiTraHangBUS.ThemDoiTraHang(doiTraHang);
                    if (!isDoiTraInserted)
                    {
                        MessageBox.Show($"Lưu thông tin trả hàng thất bại cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Xử lý sản phẩm đổi (nếu có)
                foreach (DataGridViewRow row in dgSanPhamDoi.Rows)
                {
                    if (row.Cells["MaSP"].Value == null || row.Cells["SoLuong"].Value == null)
                        continue; // Bỏ qua nếu dòng không hợp lệ

                    string maSP = row.Cells["MaSP"].Value.ToString();
                    int soLuongDoi = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal giaBanDoi = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                    // Tính tổng tiền sản phẩm đổi
                    tongTienDoi += soLuongDoi * giaBanDoi;

                    // Cập nhật kho: Giảm số lượng sản phẩm đổi
                    bool isKhoUpdated = khoBUS.CapNhatKho(maSP, soLuongDoi, false); // false: giảm số lượng trong kho
                    if (!isKhoUpdated)
                    {
                        MessageBox.Show($"Cập nhật kho thất bại cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Tạo đối tượng DoiTraHangDTO cho sản phẩm đổi
                    DoiTraHangDTO doiTraHang = new DoiTraHangDTO(
                        maHDDT: maHD,       // Mã hóa đơn
                        maHD: maHD,         // Mã hóa đơn
                        maSP: maSP,         // Mã sản phẩm
                        soLuong: soLuongDoi, // Số lượng sản phẩm đổi
                        lyDo: "Sản phẩm đổi", // Lý do
                        isTra: false         // Đánh dấu là đổi hàng
                    );

                    // Lưu thông tin sản phẩm đổi vào bảng DoiTraHang
                    bool isDoiTraInserted = doiTraHangBUS.ThemDoiTraHang(doiTraHang);
                    if (!isDoiTraInserted)
                    {
                        MessageBox.Show($"Lưu thông tin đổi hàng thất bại cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Tính tiền chênh lệch
                decimal tienChenhLech = tongTienDoi - tongTienTra;

                // Xuất phiếu đổi trả
                string phieuDoiTra = $"Phiếu đổi trả hàng\n" +
                                     $"Mã hóa đơn: {maHD}\n" +
                                     $"Tổng tiền trả: {tongTienTra:C}\n" +
                                     $"Tổng tiền đổi: {tongTienDoi:C}\n" +
                                     $"Chênh lệch: {(tienChenhLech > 0 ? "Khách cần trả thêm" : "Cần trả lại khách")}: {Math.Abs(tienChenhLech):C}\n";

                MessageBox.Show(phieuDoiTra, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Cập nhật giao diện
                GeneratePdf("C:/Users/ASUS/Documents/HoaDonDoi.pdf", maHD, kh.TenKH, dgvSanPhamTra, dgSanPhamDoi, kh.SoDienThoai, tongTienTra, tongTienDoi);
                dgvSanPhamTra.Rows.Clear();
                dgSanPhamDoi.Rows.Clear();
                LoadSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dgSanPhamDoi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvSanPhamTra_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng có sửa cột "SoLuong" không
            if (e.ColumnIndex == dgvSanPhamTra.Columns["SoLuong"].Index)
            {
                DataGridViewRow row = dgvSanPhamTra.Rows[e.RowIndex];

                // Kiểm tra giá trị nhập có hợp lệ không
                if (row.Cells["SoLuong"].Value == null || !int.TryParse(row.Cells["SoLuong"].Value.ToString(), out int soLuongNhap))
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    row.Cells["SoLuong"].Value = 1; // Đặt lại số lượng mặc định nếu lỗi
                    return;
                }

                string maSP = row.Cells["MaSP"].Value.ToString();
                decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value); // Giá bán của sản phẩm
                int soLuongMax = 0;

                // Lấy số lượng tối đa từ cột "SoLuong" trong dgvSanPham
                foreach (DataGridViewRow r in dgvSanPham.Rows)
                {
                    if (r.Cells["MaSP"].Value.ToString() == maSP)
                    {
                        soLuongMax = Convert.ToInt32(r.Cells["SoLuong"].Value);
                        break;
                    }
                }

                // Kiểm tra số lượng nhập vào có hợp lệ không (không vượt quá số lượng gốc trong hóa đơn)
                if (soLuongNhap > soLuongMax)
                {
                    MessageBox.Show("Số lượng trả không thể lớn hơn số lượng trong hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Quay lại số lượng tối đa
                    row.Cells["SoLuong"].Value = soLuongMax;
                    soLuongNhap = soLuongMax;
                }
                else if (soLuongNhap <= 0)
                {
                    MessageBox.Show("Số lượng trả phải lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // Đặt lại số lượng mặc định
                    row.Cells["SoLuong"].Value = 1;
                    soLuongNhap = 1;
                }

                // Cập nhật cột "ThanhTien"
                row.Cells["ThanhTien"].Value = giaBan * soLuongNhap;

                // Nếu cần, cập nhật cơ sở dữ liệu tại đây
            }
        }

        private void btnChuyenSanPhamTra_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu dgvSanPham và dgvSanPhamTra đã được khởi tạo
            if (dgvSanPham == null || dgvSanPhamTra == null)
            {
                MessageBox.Show("Một trong các bảng sản phẩm chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu không có dòng nào được chọn trong dgvSanPham
            if (dgvSanPham.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm từ bảng sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvSanPham.SelectedRows)
            {
                // Kiểm tra null cho các ô trong dòng
                if (row.Cells["MaSP"].Value == null || row.Cells["TenSP"].Value == null || row.Cells["GiaBan"].Value == null)
                {
                    MessageBox.Show("Một số thông tin sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue; // Tiếp tục với sản phẩm khác nếu có lỗi
                }

                string maSP = row.Cells["MaSP"].Value.ToString();
                bool productExistsInTra = false;

                // Kiểm tra sản phẩm đã có trong bảng Trả hay chưa
                foreach (DataGridViewRow r in dgvSanPhamTra.Rows)
                {
                    if (r.Cells["MaSP"].Value != null && r.Cells["MaSP"].Value.ToString() == maSP)
                    {
                        productExistsInTra = true;
                        break;
                    }
                }

                // Nếu chưa có thì thêm sản phẩm vào bảng Trả
                if (!productExistsInTra)
                {
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value); // Lấy giá bán
                    int soLuong = 1; // Số lượng mặc định là 1
                    decimal thanhTien = giaBan * soLuong; // Tính thành tiền

                    dgvSanPhamTra.Rows.Add(
                        row.Cells["MaSP"].Value.ToString(),
                        row.Cells["TenSP"].Value.ToString(),
                        giaBan.ToString("N0"), // Hiển thị giá bán có định dạng
                        soLuong,
                        thanhTien.ToString("N0") // Hiển thị thành tiền có định dạng
                    );
                }
            }
        }

        private void btnChuyenSanPhamDoi_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu dgSanPhamDoi đã được khởi tạo
            if (dgSanPhamDoi == null)
            {
                MessageBox.Show("Bảng sản phẩm đổi chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra nếu không có dòng nào được chọn trong dgSanPhamDoi
            if (dgvSanPhamDoi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm từ bảng sản phẩm đổi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataGridViewRow row in dgvSanPhamDoi.SelectedRows)
            {
                // Kiểm tra null cho các ô trong dòng
                if (row.Cells["MaSP"].Value == null || row.Cells["TenSP"].Value == null
                    || row.Cells["GiaBan"].Value == null)
                {
                    MessageBox.Show("Một số thông tin sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue; // Tiếp tục với sản phẩm khác nếu có lỗi
                }

                string maSP = row.Cells["MaSP"].Value.ToString();
                bool productExistsInDoi = false;

                // Kiểm tra sản phẩm đã có trong bảng Đổi hay chưa
                foreach (DataGridViewRow r in dgSanPhamDoi.Rows)
                {
                    if (r.Cells["MaSP"].Value != null && r.Cells["MaSP"].Value.ToString() == maSP)
                    {
                        productExistsInDoi = true;
                        break;
                    }
                }

                // Nếu chưa có thì thêm sản phẩm vào bảng Đổi
                if (!productExistsInDoi)
                {
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value); // Lấy giá bán
                    int soLuong = 1; // Số lượng mặc định là 1
                    decimal thanhTien = giaBan * soLuong; // Tính thành tiền

                    dgSanPhamDoi.Rows.Add(
                        row.Cells["MaSP"].Value.ToString(),
                        row.Cells["TenSP"].Value.ToString(),
                        giaBan.ToString("N0"), // Hiển thị giá bán có định dạng
                        soLuong,
                        thanhTien.ToString("N0") // Hiển thị thành tiền có định dạng
                    );
                }
                else
                {
                    MessageBox.Show($"Sản phẩm '{maSP}' đã có trong bảng đổi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvSanPhamTra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Chỉ cho phép chỉnh sửa cột "SoLuong"
            if (e.ColumnIndex != dgvSanPhamTra.Columns["SoLuong"].Index)
            {
                MessageBox.Show("Chỉ được phép chỉnh sửa số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true; // Hủy thao tác chỉnh sửa
            }
        }

        private void dgSanPhamDoi_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != dgSanPhamDoi.Columns["SoLuong"].Index)
            {
                MessageBox.Show("Chỉ được phép chỉnh sửa số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true; // Hủy thao tác chỉnh sửa
            }
        }

        private decimal TinhTongTien(DataGridView dgv)
        {
            decimal tongTien = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["SoLuong"].Value != null && row.Cells["GiaBan"].Value != null)
                {
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                    tongTien += soLuong * giaBan;
                }
            }

            return tongTien;
        }

        // Sự kiện khi giá trị ô trong DataGridView thay đổi
        private void dgSanPhamDoi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu ô giá trị bị thay đổi không phải là cột "MaSP" (bởi vì "MaSP" không ảnh hưởng đến tính tiền)
            if (e.ColumnIndex != -1 && (e.RowIndex >= 0))
            {
                // Tính lại tổng tiền mỗi khi có thay đổi trong dgvSanPhamDoi
                decimal tongTienDoi = TinhTongTien(dgSanPhamDoi);
                decimal tongTienTra = TinhTongTien(dgvSanPhamTra);

                // Cập nhật tổng tiền vào TextBox và disable nó
                txtTongTienSPDoi.Text = (tongTienDoi + tongTienTra).ToString("C");
            }
        }

        // Tương tự, nếu bạn muốn theo dõi bảng dgvSanPhamTra
        private void dgvSanPhamTra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && (e.RowIndex >= 0))
            {
                // Tính lại tổng tiền mỗi khi có thay đổi trong dgvSanPhamTra
                decimal tongTienDoi = TinhTongTien(dgvSanPhamDoi);
                decimal tongTienTra = TinhTongTien(dgvSanPhamTra);

                // Cập nhật tổng tiền vào TextBox và disable nó
                txtTongTienSPTra.Text = (tongTienDoi + tongTienTra).ToString("C"); 
            }
        }
        private void dgSanPhamDoi_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Tính tổng tiền mỗi khi thêm dòng mới
            decimal totalAmount = 0;

            // Duyệt qua tất cả các dòng trong dgvSanPhamDoi để tính tổng
            foreach (DataGridViewRow row in dgSanPhamDoi.Rows)
            {
                if (row.Cells["GiaBan"].Value != null && row.Cells["SoLuong"].Value != null)
                {
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    totalAmount += giaBan * soLuong;
                }
            }

            // Cập nhật giá trị tổng tiền vào TextBox
            txtTongTienSPDoi.Text = totalAmount.ToString("C"); // Định dạng hiển thị theo tiền tệ
        }

        private void dgvSanPhamTra_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Tính tổng tiền mỗi khi thêm dòng mới
            decimal totalAmount = 0;

            // Duyệt qua tất cả các dòng trong dgvSanPhamTra để tính tổng
            foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
            {
                if (row.Cells["GiaBan"].Value != null && row.Cells["SoLuong"].Value != null)
                {
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                    int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    totalAmount += giaBan * soLuong;
                }
            }

            // Cập nhật giá trị tổng tiền vào TextBox
            txtTongTienSPTra.Text = totalAmount.ToString("C"); // Định dạng hiển thị theo tiền tệ
        }


        public void GeneratePdf(string filePath, string maHoaDon, string tenKhachHang, DataGridView dgvSanPhamTra, DataGridView dgvSanPhamDoi, string sdt, decimal tongTienTra, decimal tongTienDoi)
        {
            try
            {
                // Tạo tài liệu PDF
                PdfSharp.Pdf.PdfDocument pdfDocument = new PdfSharp.Pdf.PdfDocument();
                pdfDocument.Info.Title = "Hóa Đơn Đổi Trả";

                // Tạo trang mới trong PDF
                PdfSharp.Pdf.PdfPage pdfPage = pdfDocument.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(pdfPage);

                // Định nghĩa các font chữ
                XFont fontTitle = new XFont("Arial", 16, XFontStyleEx.Bold);
                XFont fontHeader = new XFont("Arial", 12, XFontStyleEx.Bold);
                XFont fontContent = new XFont("Arial", 10, XFontStyleEx.Bold);
                XFont fontSmall = new XFont("Arial", 9);

                // Bố cục lề
                int margin = 20;
                int pageWidth = (int)pdfPage.Width - 2 * margin;
                int y = margin;

                // Tiêu đề
                gfx.DrawString("HÓA ĐƠN ĐỔI TRẢ", fontTitle, XBrushes.Black, new XRect(margin, y, pageWidth, 20), XStringFormats.TopCenter);
                y += 30;
                gfx.DrawLine(XPens.Black, margin, y, pageWidth + margin, y);
                y += 20;
                // Thông tin hóa đơn
                gfx.DrawString($"Mã hóa đơn: {maHoaDon}", fontContent, XBrushes.Black, margin, y);
                y += 15;
                gfx.DrawString($"Tên khách hàng: {tenKhachHang}", fontContent, XBrushes.Black, margin, y);
                y += 15;
                gfx.DrawString($"SĐT: {sdt}", fontContent, XBrushes.Black, margin, y);
                y += 20;

                // Đường phân cách
                gfx.DrawLine(XPens.Black, margin, y, pageWidth + margin, y);
                y += 20;

                // Bảng sản phẩm trả
                y = DrawProductTable(gfx, "Sản phẩm trả", dgvSanPhamTra, margin, y, fontHeader, fontContent, ref tongTienTra);

                // Khoảng cách giữa hai bảng
                y += 20;

                // Bảng sản phẩm đổi
                y = DrawProductTable(gfx, "Sản phẩm đổi", dgvSanPhamDoi, margin, y, fontHeader, fontContent, ref tongTienDoi);

                // Tổng tiền
                decimal tienChenhLech = tongTienDoi - tongTienTra;
                y += 30;
                gfx.DrawString($"Tổng tiền trả: {tongTienTra:C}", fontHeader, XBrushes.Black, margin, y);
                y += 15;
                gfx.DrawString($"Tổng tiền đổi: {tongTienDoi:C}", fontHeader, XBrushes.Black, margin, y);
                y += 15;
                gfx.DrawString($"Chênh lệch: {(tienChenhLech > 0 ? "Khách cần trả thêm" : "Cần trả lại khách")} {Math.Abs(tienChenhLech):C}", fontHeader, XBrushes.Black, margin, y);

                // Cảm ơn khách hàng
                y += 20;
                gfx.DrawLine(XPens.Black, margin, y, pageWidth + margin, y);
                y += 10;
                gfx.DrawString("Cảm ơn quý khách!", fontContent, XBrushes.Black, new XRect(margin, y, pageWidth, 20), XStringFormats.TopCenter);

                // Lưu file PDF
                pdfDocument.Save(filePath);
                MessageBox.Show($"Hóa đơn đã được xuất ra: {filePath}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xuất hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int DrawProductTable(XGraphics gfx, string tableTitle, DataGridView dgv, int x, int y, XFont fontHeader, XFont fontContent, ref decimal totalAmount)
        {
            int cellHeight = 25;
            int[] columnWidths = { 80, 200, 80, 100 }; // Cột: Mã SP, Tên SP, Số lượng, Giá

            // Tiêu đề bảng
            gfx.DrawString(tableTitle, fontHeader, XBrushes.Black, x, y);
            y += 20;

            // Vẽ tiêu đề cột
            int currentX = x;
            string[] headers = { "Mã SP", "Tên SP", "Số lượng", "Giá" };
            foreach (var header in headers)
            {
                gfx.DrawRectangle(XPens.Black, currentX, y, columnWidths[Array.IndexOf(headers, header)], cellHeight);
                gfx.DrawString(header, fontHeader, XBrushes.Black, new XRect(currentX + 5, y + 5, columnWidths[Array.IndexOf(headers, header)] - 10, cellHeight), XStringFormats.TopLeft);
                currentX += columnWidths[Array.IndexOf(headers, header)];
            }
            y += cellHeight;

            // Vẽ dữ liệu
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["MaSP"].Value == null || row.Cells["SoLuong"].Value == null)
                    continue;

                string maSP = row.Cells["MaSP"].Value.ToString();
                string tenSP = row.Cells["TenSP"].Value.ToString();
                int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                decimal gia = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                currentX = x;
                gfx.DrawRectangle(XPens.Black, currentX, y, columnWidths[0], cellHeight);
                gfx.DrawString(maSP, fontContent, XBrushes.Black, new XRect(currentX + 5, y + 5, columnWidths[0] - 10, cellHeight), XStringFormats.TopLeft);

                currentX += columnWidths[0];
                gfx.DrawRectangle(XPens.Black, currentX, y, columnWidths[1], cellHeight);
                gfx.DrawString(tenSP, fontContent, XBrushes.Black, new XRect(currentX + 5, y + 5, columnWidths[1] - 10, cellHeight), XStringFormats.TopLeft);

                currentX += columnWidths[1];
                gfx.DrawRectangle(XPens.Black, currentX, y, columnWidths[2], cellHeight);
                gfx.DrawString(soLuong.ToString(), fontContent, XBrushes.Black, new XRect(currentX + 5, y + 5, columnWidths[2] - 10, cellHeight), XStringFormats.TopLeft);

                currentX += columnWidths[2];
                gfx.DrawRectangle(XPens.Black, currentX, y, columnWidths[3], cellHeight);
                gfx.DrawString(gia.ToString("C"), fontContent, XBrushes.Black, new XRect(currentX + 5, y + 5, columnWidths[3] - 10, cellHeight), XStringFormats.TopLeft);

                totalAmount += soLuong * gia;
                y += cellHeight;
            }

            return y;
        }

    }
}
