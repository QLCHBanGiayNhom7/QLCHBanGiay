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
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using System.Collections.Generic;
using iText.IO.Font.Constants;
using System.IO;

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
            dgSanPhamDoi.CellValueChanged += new DataGridViewCellEventHandler(dgSanPhamDoi_CellValueChanged);
            dgvSanPhamTra.CellValueChanged += new DataGridViewCellEventHandler(dgvSanPhamTra_CellValueChanged);
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
                bool isReturn = true; // Mặc định là trả hàng
                bool hasDoiSanPham = dgvSanPhamDoi.Rows.Count > 0; // Kiểm tra bảng dgvSanPhamDoi có dữ liệu không

                if (hasDoiSanPham) // Nếu có dữ liệu trong dgvSanPhamDoi thì coi là đổi hàng
                {
                    isReturn = false;
                }
                 if (!isReturn) // Nếu không phải trả hàng (nghĩa là đổi hàng)
                {
                    string DoiSanPham = dgSanPhamDoi.Rows.Count.ToString();
                    int i = Convert.ToInt32(DoiSanPham);
                    foreach (DataGridViewRow r in dgSanPhamDoi.Rows)
                        {
                            if (i<=1)
                            {
                                break;
                            }
                            string maSPMoi = r.Cells["MaSP"].Value.ToString(); // Giả sử bạn có mã sản phẩm mới để đổi
                            int soLuongMoi = Convert.ToInt32(r.Cells["SoLuong"].Value);
                            decimal giaBanMoi = Convert.ToDecimal(r.Cells["GiaBan"].Value);

                            // Thêm sản phẩm mới vào chi tiết hóa đơn
                            bool isAddedCTHD = chiTietHoaDonBUS.ThemChiTietHoaDon(maHD, maSPMoi, soLuongMoi, giaBanMoi);
                            if (!isAddedCTHD)
                            {
                                MessageBox.Show($"Thêm sản phẩm mới {maSPMoi} vào chi tiết hóa đơn thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // Cập nhật lại kho sản phẩm mới
                            bool isUpdatedKhoMoi = khoBUS.CapNhatKho(maSPMoi, soLuongMoi, false); // false: thêm vào kho sản phẩm mới
                            if (!isUpdatedKhoMoi)
                            {
                                MessageBox.Show($"Cập nhật kho thất bại cho sản phẩm {maSPMoi}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        i--;

                    }
                   
                }
                // Lặp qua từng sản phẩm trong bảng "Trả hàng" hoặc "Đổi hàng"
                foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                {
                    if (row.Cells["MaSP"].Value == null || row.Cells["SoLuong"].Value == null)
                        continue; // Bỏ qua nếu dòng không hợp lệ

                    string maSP = row.Cells["MaSP"].Value.ToString();
                    int soLuongTra = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                    if (soLuongTra <= 0)
                    {
                        MessageBox.Show($"Số lượng trả của sản phẩm {maSP} phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Kiểm tra số lượng trả có vượt quá số lượng trong hóa đơn
                    int soLuongMax = chiTietHoaDonBUS.LaySoLuongTrongHoaDon(maHD, maSP); // Lấy số lượng gốc từ hóa đơn
                    if (soLuongTra > soLuongMax)
                    {
                        MessageBox.Show($"Số lượng trả không được vượt quá {soLuongMax} cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Nếu là trả hàng, cập nhật lại kho (thêm số lượng sản phẩm trả lại kho)
                    

                    // Cập nhật số lượng trả vào chi tiết hóa đơn
                    bool isUpdatedCTHDTra = chiTietHoaDonBUS.CapNhatSoLuongTra(maHD, maSP, soLuongTra);
                    if (!isUpdatedCTHDTra)
                    {
                        MessageBox.Show($"Cập nhật trả hàng thất bại cho sản phẩm {maSP}.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Nếu sản phẩm trả hết số lượng, xóa khỏi chi tiết hóa đơn
                    if (soLuongTra == soLuongMax)
                    {
                        bool isRemovedCTHD = chiTietHoaDonBUS.XoaChiTietHoaDon(maHD, maSP);
                        if (!isRemovedCTHD)
                        {
                            MessageBox.Show($"Xóa sản phẩm {maSP} khỏi chi tiết hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Cập nhật lại tổng tiền hóa đơn sau khi trả hàng hoặc đổi hàng
                bool isUpdatedHoaDon = hoaDonBUS.CapNhatTongTien(maHD);
                if (!isUpdatedHoaDon)
                {
                    MessageBox.Show("Cập nhật tổng tiền hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(isReturn ? "Trả hàng thành công!" : "Đổi hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật lại giao diện
                LoadSanPham();
                dgvSanPhamTra.Rows.Clear(); // Xóa sản phẩm đã trả hoặc đổi khỏi bảng
                dgSanPhamDoi.Rows.Clear(); // Xóa sản phẩm đã đổi khỏi bảng
                ExportInvoice();
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
                decimal tongTienDoi = TinhTongTien(dgvSanPhamDoi);
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

        private void ExportInvoice()
        {
            // Tạo PDF Writer và Document
            string folderPath = @"D:\app hoc";  // Đường dẫn đến thư mục muốn lưu tệp
                                                // Kiểm tra nếu thư mục không tồn tại, tạo mới
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, "HoaDon_DoiTra_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf");
            // Tạo PdfFont cho bold
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            using (var writer = new PdfWriter(filePath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);

                    // Tiêu đề hóa đơn
                    document.Add(new Paragraph("Hóa Đơn Đổi/Trả Hàng")
                        .SetFont(boldFont)  // Áp dụng font bold
                        .SetFontSize(20)
                        .SetTextAlignment(TextAlignment.CENTER));

                    // Thông tin khách hàng và ngày tháng
                    document.Add(new Paragraph($"Ngày: {DateTime.Now.ToString("dd/MM/yyyy")}")
                        .SetFont(regularFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT));

                    // Thông tin hóa đơn (Có thể thêm các thông tin khác như mã hóa đơn, thông tin khách hàng)
                    document.Add(new Paragraph($"Mã Hóa Đơn: {maHD}")
                        .SetFont(regularFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT));

                    // Tạo bảng cho sản phẩm trả
                    var tableTra = new Table(UnitValue.CreatePercentArray(new float[] { 2, 4, 3, 3 }))
                        .UseAllAvailableWidth();

                    tableTra.AddCell(new Cell().Add(new Paragraph("Mã Sản Phẩm").SetFont(boldFont)));
                    tableTra.AddCell(new Cell().Add(new Paragraph("Tên Sản Phẩm").SetFont(boldFont)));
                    tableTra.AddCell(new Cell().Add(new Paragraph("Số Lượng").SetFont(boldFont)));
                    tableTra.AddCell(new Cell().Add(new Paragraph("Giá").SetFont(boldFont)));

                    decimal totalReturn = 0;

                    foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                    {
                        if (row.Cells["MaSP"].Value != null && row.Cells["SoLuong"].Value != null)
                        {
                            string maSP = row.Cells["MaSP"].Value.ToString();
                            string tenSP = row.Cells["TenSP"].Value.ToString(); // Lấy tên sản phẩm (có thể điều chỉnh theo tên cột)
                            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                            tableTra.AddCell(new Cell().Add(new Paragraph(maSP).SetFont(regularFont)));
                            tableTra.AddCell(new Cell().Add(new Paragraph(tenSP).SetFont(regularFont)));
                            tableTra.AddCell(new Cell().Add(new Paragraph(soLuong.ToString()).SetFont(regularFont)));
                            tableTra.AddCell(new Cell().Add(new Paragraph(giaBan.ToString("C")).SetFont(regularFont)));

                            totalReturn += giaBan * soLuong;
                        }
                    }

                    // Thêm bảng sản phẩm trả vào tài liệu
                    document.Add(new Paragraph("Sản Phẩm Trả").SetFont(boldFont));
                    document.Add(tableTra);

                    // Tạo bảng cho sản phẩm đổi
                    var tableDoi = new Table(UnitValue.CreatePercentArray(new float[] { 2, 4, 3, 3 }))
                        .UseAllAvailableWidth();

                    tableDoi.AddCell(new Cell().Add(new Paragraph("Mã Sản Phẩm").SetFont(boldFont)));
                    tableDoi.AddCell(new Cell().Add(new Paragraph("Tên Sản Phẩm").SetFont(boldFont)));
                    tableDoi.AddCell(new Cell().Add(new Paragraph("Số Lượng").SetFont(boldFont)));
                    tableDoi.AddCell(new Cell().Add(new Paragraph("Giá").SetFont(boldFont)));

                    decimal totalExchange = 0;

                    foreach (DataGridViewRow row in dgvSanPhamDoi.Rows)
                    {
                        if (row.Cells["MaSP"].Value != null && row.Cells["SoLuong"].Value != null)
                        {
                            string maSP = row.Cells["MaSP"].Value.ToString();
                            string tenSP = row.Cells["TenSP"].Value.ToString(); // Lấy tên sản phẩm (có thể điều chỉnh theo tên cột)
                            int soLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                            decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);

                            tableDoi.AddCell(new Cell().Add(new Paragraph(maSP).SetFont(regularFont)));
                            tableDoi.AddCell(new Cell().Add(new Paragraph(tenSP).SetFont(regularFont)));
                            tableDoi.AddCell(new Cell().Add(new Paragraph(soLuong.ToString()).SetFont(regularFont)));
                            tableDoi.AddCell(new Cell().Add(new Paragraph(giaBan.ToString("C")).SetFont(regularFont)));

                            totalExchange += giaBan * soLuong;
                        }
                    }

                    // Thêm bảng sản phẩm đổi vào tài liệu
                    document.Add(new Paragraph("Sản Phẩm Đổi").SetFont(boldFont));
                    document.Add(tableDoi);

                    // Tính tổng số tiền cần thanh toán
                    decimal totalAmount = totalExchange - totalReturn;

                    // Thêm thông tin tổng tiền
                    document.Add(new Paragraph($"Tổng Tiền Trả: {totalReturn.ToString("C")}")
                        .SetFont(regularFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT));
                    document.Add(new Paragraph($"Tổng Tiền Đổi: {totalExchange.ToString("C")}")
                        .SetFont(regularFont)
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.LEFT));
                    document.Add(new Paragraph($"Tổng Tiền Khách Cần Thanh Toán: {totalAmount.ToString("C")}")
                        .SetFont(boldFont)
                        .SetFontSize(14)
                        .SetTextAlignment(TextAlignment.LEFT));

                    document.Close(); // Đóng document và ghi vào file PDF
                }
            }

            // Hiển thị thông báo đã xuất hóa đơn
            MessageBox.Show("Hóa đơn đổi trả đã được xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
