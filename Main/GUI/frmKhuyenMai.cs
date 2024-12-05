using Main.BUS;
using Main.DAO;
using OfficeOpenXml;
using Sunny.UI;
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

namespace Main.GUI
{
    public partial class frmKhuyenMai : UIPage
    {
        KhuyenMaiBUS khuyenMaiBUS;
        bool them, sua = false;
        public frmKhuyenMai()
        {
            InitializeComponent();
            khuyenMaiBUS = new KhuyenMaiBUS();
            LoadData();
            LoadComponentDis();
        }

        private void dgvKhuyenMai_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvKhuyenMai.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvKhuyenMai.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvKhuyenMai.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private string formatGiaGiam(decimal? gt, string dk)
        {
            return dk == "Giảm Trực Tiếp"
                ? gt?.ToString("#,##0 VNĐ")
                : gt?.ToString("#0.##") + "%";
        }

        private void LoadData()
        {
            dgvKhuyenMai.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvKhuyenMai.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
            dgvKhuyenMai.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;


            dgvKhuyenMai.DataSource = null;
            var khuyenMaiList = khuyenMaiBUS.GetKhuyenMai();
            var formattedList = khuyenMaiList.Select(km => new
            {
                MaKM = km.MaKM,
                TenChuongTrinh = km.TenChuongTrinh,
                GiaTriGiamGia = formatGiaGiam(km.GiaTriGiamGia, km.DieuKienPhu),
                NgayBatDau = km.NgayBatDau,
                NgayKetThuc = km.NgayKetThuc,
                HoaDonTu = km.HoaDonTu,
                DieuKienPhu = km.DieuKienPhu,
                TrangThai = km.TrangThai
            }).ToList();
            dgvKhuyenMai.DataSource = formattedList;

            foreach (DataGridViewRow row in dgvKhuyenMai.Rows)
            {
                row.Selected = false;
            }

            dgvKhuyenMai.Columns["MaKM"].HeaderText = "Mã Khuyến Mãi";
            dgvKhuyenMai.Columns["TenChuongTrinh"].HeaderText = "Tên Chương Trình";
            dgvKhuyenMai.Columns["GiaTriGiamGia"].HeaderText = "Giá Trị Giảm Giá";
            dgvKhuyenMai.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
            dgvKhuyenMai.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
            dgvKhuyenMai.Columns["HoaDonTu"].HeaderText = "Hóa Đơn Từ";
            dgvKhuyenMai.Columns["DieuKienPhu"].HeaderText = "Điều Kiện Phụ";
            dgvKhuyenMai.Columns["TrangThai"].HeaderText = "Trạng Thái";

        }
        private void LoadStaEdit()
        {
            btnLuu.Enabled = btnHuy.Enabled = btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnXuatExcel.Enabled = btnThongKe.Enabled = false;
        }
        private void LoadStaView()
        {
            btnLuu.Enabled = btnHuy.Enabled = false;
            btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = btnXuatExcel.Enabled = btnThongKe.Enabled = true;
        }

        private void LoadComponentDis()
        {
            cbDieuKienApDung.SelectedIndex = 0;
            txtMaKM.Enabled = false;
            txtGiaTriKM.Enabled = dtpNgayBD.Enabled = dtpNgayKT.Enabled = txtHoaDonTu.Enabled =
                cbDieuKienApDung.Enabled = txtChuongTrinh.Enabled = false;
            btnLuu.Enabled = false;
        }

        private bool CheckValid()
        {
            if (string.IsNullOrWhiteSpace(txtChuongTrinh.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương trình khuyến mãi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGiaTriKM.Text))
            {
                MessageBox.Show("Vui lòng nhập giá trị khuyến mãi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dtpNgayBD.Value > dtpNgayKT.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtGiaTriKM.Enabled = dtpNgayBD.Enabled = dtpNgayKT.Enabled = txtHoaDonTu.Enabled =
                cbDieuKienApDung.Enabled = txtChuongTrinh.Enabled = true;
            them = true;
            sua = false;
            txtMaKM.Text = khuyenMaiBUS.layMaKMMoi();
            LoadStaEdit();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa các khuyến mãi đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    List<string> maKMList = new List<string>();

                    foreach (DataGridViewRow row in dgvKhuyenMai.SelectedRows)
                    {
                        string maKM = row.Cells["MaKM"].Value.ToString();
                        maKMList.Add(maKM);
                    }

                    bool deleteSuccess = khuyenMaiBUS.DeleteKhuyenMai(maKMList);

                    if (deleteSuccess)
                    {
                        MessageBox.Show("Xóa khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Xóa khuyến mãi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtGiaTriKM.Enabled = dtpNgayBD.Enabled = dtpNgayKT.Enabled = txtHoaDonTu.Enabled =
                cbDieuKienApDung.Enabled = txtChuongTrinh.Enabled = true;
            sua = true;
            them = false;
            LoadStaEdit();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (CheckValid())
            {
                string trangThai = "";
                if (dtpNgayBD.Value <= DateTime.Now && dtpNgayKT.Value >= DateTime.Now)
                {
                    trangThai = "Đang diễn ra";
                }
                else if (dtpNgayBD.Value >= DateTime.Now)
                {
                    trangThai = "Chưa diễn ra";
                }
                else
                {
                    trangThai = "Đã kết thúc";
                }

                decimal giaTriKM;
                if (cbDieuKienApDung.SelectedItem.ToString() == "Giảm Trực Tiếp")
                {
                    if (txtGiaTriKM.Text.Contains("%"))
                    {
                        giaTriKM = decimal.Parse(txtGiaTriKM.Text.Replace("%", "").Trim());
                        txtGiaTriKM.Text = giaTriKM.ToString("#,##0 VNĐ");
                    }
                    else
                    {
                        giaTriKM = decimal.Parse(txtGiaTriKM.Text.Replace("VNĐ", "").Replace(",", "").Trim());
                    }
                }
                else
                {
                    if (txtGiaTriKM.Text.Contains("VNĐ"))
                    {
                      
                        giaTriKM = decimal.Parse(txtGiaTriKM.Text.Replace("VNĐ", "").Replace(",", "").Trim());
                        txtGiaTriKM.Text = giaTriKM.ToString("#0.##") + "%";
                    }
                    else
                    {
                        giaTriKM = decimal.Parse(txtGiaTriKM.Text.Replace("%", "").Trim());
                    }
                }
                KhuyenMai km = new KhuyenMai
                {
                    MaKM = txtMaKM.Text,
                    TenChuongTrinh = txtChuongTrinh.Text,
                    GiaTriGiamGia = giaTriKM,
                    NgayBatDau = dtpNgayBD.Value,
                    NgayKetThuc = dtpNgayKT.Value,
                    HoaDonTu = int.Parse(txtHoaDonTu.Text),
                    TrangThai = trangThai
                };
                if (cbDieuKienApDung.SelectedItem.ToString() == "Giảm Trực Tiếp")
                {
                    km.DieuKienPhu = "Giảm Trực Tiếp";
                }
                else if (cbDieuKienApDung.SelectedItem.ToString() == "Giảm Phần Trăm")
                {
                    km.DieuKienPhu = "Giảm Phần Trăm";
                }
                if (them)
                {
                    if (dtpNgayBD.Value < DateTime.Now)
                    {
                        DialogResult kq = MessageBox.Show("Ngày bắt đầu của chương trình khuyến mãi này đã qua, bạn có muốn tạo khuyến mãi này không?", "Thông báo", MessageBoxButtons.OKCancel);
                        if (kq == DialogResult.OK)
                        {
                            bool result = khuyenMaiBUS.AddKhuyenMai(km);
                            if (result)
                            {
                                MessageBox.Show("Thêm khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadComponentDis();
                                LoadStaView();
                                them = false;
                                LoadData();
                            }
                            else
                            {
                                MessageBox.Show("Thêm khuyến mãi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        bool result = khuyenMaiBUS.AddKhuyenMai(km);
                        if (result)
                        {
                            MessageBox.Show("Thêm khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadComponentDis();
                            LoadStaView();
                            them = false;
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Thêm khuyến mãi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else if (sua)
                {
                    km.MaKM = txtMaKM.Text;
                    km.TrangThai = trangThai;
                    bool result = khuyenMaiBUS.UpdateKhuyenMai(km);
                    if (result)
                    {
                        MessageBox.Show("Cập nhật khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadComponentDis();
                        LoadStaView();
                        sua = false;
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật khuyến mãi thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Xuất Danh Sách Khuyến Mãi";
                saveFileDialog.FileName = $"DanhSachKhuyenMai_{DateTime.Now:ddMMyyyy}.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(saveFileDialog.FileName)))
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Khuyến Mãi");

                        string[] headers = {
                    "Mã KM",
                    "Tên Chương Trình",
                    "Giá Trị Giảm Giá",
                    "Ngày Bắt Đầu",
                    "Ngày Kết Thúc",
                    "Hóa Đơn Từ",
                    "Điều Kiện Phụ",
                    "Trạng Thái"
                };

                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = headers[i];
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                            worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }

                        var khuyenMaiList = khuyenMaiBUS.GetKhuyenMai();
                        for (int row = 0; row < khuyenMaiList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = khuyenMaiList[row].MaKM;
                            worksheet.Cells[row + 2, 2].Value = khuyenMaiList[row].TenChuongTrinh;
                            worksheet.Cells[row + 2, 3].Value = khuyenMaiList[row].GiaTriGiamGia;
                            worksheet.Cells[row + 2, 4].Value = khuyenMaiList[row].NgayBatDau;
                            worksheet.Cells[row + 2, 5].Value = khuyenMaiList[row].NgayKetThuc;
                            worksheet.Cells[row + 2, 6].Value = khuyenMaiList[row].HoaDonTu;
                            worksheet.Cells[row + 2, 7].Value = khuyenMaiList[row].DieuKienPhu;
                            worksheet.Cells[row + 2, 8].Value = khuyenMaiList[row].TrangThai;
                        }

                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        package.Save();

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadStaView();
            LoadData();
            dgvKhuyenMai.Rows[0].Selected = true;

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtTim.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(keyword))
            {
                var searchResult = khuyenMaiBUS.SearchKhuyenMai(keyword);
                dgvKhuyenMai.DataSource = searchResult;
            }
            else
            {
                LoadData();
            }
        }

        private void dgvKhuyenMai_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhuyenMai.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvKhuyenMai.SelectedRows[0];

                txtMaKM.Text = selectedRow.Cells["MaKM"].Value.ToString();
                txtChuongTrinh.Text = selectedRow.Cells["TenChuongTrinh"].Value.ToString();
                txtGiaTriKM.Text = selectedRow.Cells["GiaTriGiamGia"].Value.ToString();
                dtpNgayBD.Value = Convert.ToDateTime(selectedRow.Cells["NgayBatDau"].Value);
                dtpNgayKT.Value = Convert.ToDateTime(selectedRow.Cells["NgayKetThuc"].Value);
                txtHoaDonTu.Text = selectedRow.Cells["HoaDonTu"].Value.ToString();

                string dieuKienPhu = selectedRow.Cells["DieuKienPhu"].Value.ToString();
                cbDieuKienApDung.SelectedItem = dieuKienPhu == "Giảm Trực Tiếp" ? "Giảm Trực Tiếp" : "Giảm Phần Trăm";

                txtTrangThai.Text = selectedRow.Cells["TrangThai"].Value?.ToString() ?? "";
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            LoadStaView();
            LoadComponentDis();
            LoadData();
        }

        private void txtGiaTriKM_Leave(object sender, EventArgs e)
        {
            FormatDiscountValue();
        }

        private void FormatDiscountValue()
        {
            if (string.IsNullOrWhiteSpace(txtGiaTriKM.Text)) return;

            try
            {
                decimal value = decimal.Parse(txtGiaTriKM.Text.Replace(",", "").Replace("VNĐ", "").Replace("%", "").Trim());

                if (cbDieuKienApDung.SelectedItem.ToString() == "Giảm Trực Tiếp")
                {
                    txtGiaTriKM.Text = value.ToString("#,##0 VNĐ");
                }
                else
                {
                    txtGiaTriKM.Text = value.ToString("#0.##") + "%";
                }
            }
            catch
            {
                MessageBox.Show("Giá trị không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaTriKM.Focus();
            }
        }


        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadComponentDis();
        }
    }
}
