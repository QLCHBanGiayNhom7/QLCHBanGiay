using Main.BUS;
using Main.DAO;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.GUI
{
    public partial class frmDatHangNhaCungCap : UIPage
    {
        DDHNhaCungCapBUS don;
        bool themddh, suaddh = false;
        bool themct, suact = false;
        public frmDatHangNhaCungCap()
        {
            InitializeComponent();
            don = new DDHNhaCungCapBUS();
            loadMaDatHang();
            loadNhaCungCap();
            loadSanPham();
            LoadDataDDH();
            LoadComponentDisDDH();
            LoadComponentDisCTDDH();
        }

        private void loadMaDatHang()
        {
            try
            {
                var maDDHList = don.GetMaDonDatHangList()
                  .Select(ma => new { MaDDH = ma })
                  .ToList();

                if (maDDHList.Count > 0)
                {
                    cbMaDatHang.DataSource = maDDHList;
                    cbMaDatHang.ValueMember = "MaDDH";
                    cbMaDatHang.DisplayMember = "MaDDH";
                }
                else
                {
                    MessageBox.Show("Không có mã đơn đặt hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void loadNhaCungCap()
        {
            cbNhaCungCap.DataSource = null;
            var ncc = don.GetNhaCungCap();
            cbNhaCungCap.DataSource = ncc;
            cbNhaCungCap.ValueMember = "MaNCC";
            cbNhaCungCap.DisplayMember = "TenNCC";
        }

        private void loadSanPham()
        {
            cbSanPham.DataSource = null;
            var sp = don.GetSanPham();
            cbSanPham.DataSource = sp;
            cbSanPham.ValueMember = "MaSP";
            cbSanPham.DisplayMember = "TenSP";
        }

        private void LoadDataDDH()
        {
            loadMaDatHang();
            dgvDonDatHang.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvDonDatHang.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDonDatHang.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvDonDatHang.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
            dgvDonDatHang.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;


            dgvDonDatHang.DataSource = null;
            var dondh = don.GetDonDH();
            dgvDonDatHang.DataSource = dondh;

            foreach (DataGridViewRow row in dgvDonDatHang.Rows)
            {
                row.Selected = false;
            }

            dgvDonDatHang.Columns["MaDDH"].HeaderText = "Mã Đơn Đặt Hàng";
            dgvDonDatHang.Columns["NgayLapDDH"].HeaderText = "Ngày Lập";
            dgvDonDatHang.Columns["MaNCC"].HeaderText = "Mã Nhà Cung Cấp";
            dgvDonDatHang.Columns["TongTien"].HeaderText = "Tổng Tiền";
            dgvDonDatHang.Columns["TrangThai"].HeaderText = "Trạng Thái";


        }
        private void LoadStaEditDDH()
        {
            btnLuuDH.Enabled = btnHuyDH.Enabled = btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = btnLoc.Enabled = false;
        }
        private void LoadStaViewDDH()
        {
            btnLuuDH.Enabled = btnHuyDH.Enabled = false;
            btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = btnLoc.Enabled = true;
        }

        private void LoadComponentDisDDH()
        {

            btnLuuDH.Enabled = btnHuyDH.Enabled = false;
            cbMaDatHang.Enabled = cbNhaCungCap.Enabled = cbTrangThai.Enabled =
                dtpNgayDatHang.Enabled = txtTongTien.Enabled = false;
        }

        private void LoadDataCTDDH(int maddh)
        {
            dgvChiTiet.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            dgvChiTiet.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dgvChiTiet.ColumnHeadersDefaultCellStyle.BackColor = Color.PaleVioletRed;
            dgvChiTiet.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.PaleVioletRed;


            dgvChiTiet.DataSource = null;
            var dondh = don.GetChiTietDonDH(maddh);
            if (dondh != null)
            {
                dgvChiTiet.DataSource = dondh;
            }
            else
            {
                dgvChiTiet.DataSource = null;
            }

            foreach (DataGridViewRow row in dgvChiTiet.Rows)
            {
                row.Selected = false;
            }

            if (dgvChiTiet.Columns.Count > 0)
            {
                dgvChiTiet.Columns["MaDDH"].HeaderText = "Mã Đơn Đặt Hàng";

                dgvChiTiet.Columns["MaDDH"].Visible = false;

                dgvChiTiet.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            }


        }
        private void LoadStaEditCTDDH()
        {
            btnLuuCT.Enabled = btnHuyCT.Enabled = true;
            btnThemCT.Enabled = btnXoaCT.Enabled  = false;
        }
        private void LoadStaViewCTDDH()
        {
            btnLuuCT.Enabled = btnHuyCT.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled  = true;
        }

        private void LoadComponentDisCTDDH()
        {

            btnLuuCT.Enabled = btnHuyCT.Enabled = false;
            cbSanPham.Enabled = txtDonGia.Enabled = numbericSoLuong.Enabled = numbericSoLuong.Enabled =
                txtThanhTien.Enabled = false;
        }

        private void dgvDonNhapHang_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvDonDatHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvDonDatHang.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvDonDatHang.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void dgvDonDatHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonDatHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvDonDatHang.SelectedRows[0];

                cbMaDatHang.SelectedValue = Convert.ToInt32(selectedRow.Cells["MaDDH"].Value);
                cbNhaCungCap.SelectedValue = Convert.ToInt32(selectedRow.Cells["MaNCC"].Value);
                dtpNgayDatHang.Value = Convert.ToDateTime(selectedRow.Cells["NgayLapDDH"].Value);
                cbTrangThai.Text = selectedRow.Cells["TrangThai"].Value?.ToString() ?? "";
                txtTongTien.Text = selectedRow.Cells["TongTien"].Value?.ToString() ?? "";


                int maDDH = Convert.ToInt32(selectedRow.Cells["MaDDH"].Value);
                LoadDataCTDDH(maDDH);
            }
        }



        private void dgvChiTiet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChiTiet.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvChiTiet.SelectedRows[0];

                cbSanPham.SelectedValue = Convert.ToInt32(selectedRow.Cells["MaSP"].Value);
                numbericSoLuong.Value = (int) selectedRow.Cells["SoLuong"].Value;
                txtDonGia.Text = selectedRow.Cells["DonGia"].Value.ToString();
                txtThanhTien.Text = selectedRow.Cells["ThanhTien"].Value.ToString();
            }
        }

        private void btnThemDH_Click(object sender, EventArgs e)
        {
            themddh = true;
            MessageBox.Show("Vui lòng chọn nhà cung cấp để đặt hàng!", "Thông báo", MessageBoxButtons.OK);
            cbNhaCungCap.Enabled = true;
            LoadStaEditDDH();
        }

        private void dgvChiTiet_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvChiTiet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvChiTiet.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvChiTiet.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnXoaDH_Click(object sender, EventArgs e)
        {

        }

        private void btnSuaDH_Click(object sender, EventArgs e)
        {

        }

        private DataTable dtChiTiet;
        private void loadGioHang()
        {
            dtChiTiet = new DataTable();
            //dtChiTiet.Columns.Add("MaDDH", typeof(int));
            dtChiTiet.Columns.Add("MaSP", typeof(int));
            dtChiTiet.Columns.Add("SoLuong", typeof(int));
            dtChiTiet.Columns.Add("DonGia", typeof(decimal));
            dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));

            dgvChiTiet.DataSource = dtChiTiet;

            //dgvChiTiet.Columns["MaDDH"].HeaderText = "Mã Đơn Đặt Hàng";
            dgvChiTiet.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (dtChiTiet == null)
            {
                loadGioHang();
            }

            if (themct)
            {
                btnThemCT.Enabled = btnXoaCT.Enabled = true;
                if (string.IsNullOrEmpty(txtDonGia.Text))
                {
                    MessageBox.Show("Vui lòng nhập đơn giá!", "Thông báo", MessageBoxButtons.OK);
                    return;
                }

                try
                {
                    int maDDH = Convert.ToInt32(cbMaDatHang.SelectedValue);
                    int maSP = Convert.ToInt32(cbSanPham.SelectedValue);
                    int soLuongThem = (int) numbericSoLuong.Value;
                    decimal donGia = Convert.ToDecimal(txtDonGia.Text);

                    if (soLuongThem <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }

                    // Kiểm tra sản phẩm đã tồn tại
                    DataRow[] existingRows = dtChiTiet.Select($"MaSP = {maSP}");
                    if (existingRows.Length > 0)
                    {
                        // Cập nhật số lượng nếu sản phẩm đã tồn tại
                        DataRow existingRow = existingRows[0];
                        int soLuongCu = Convert.ToInt32(existingRow["SoLuong"]);
                        int soLuongMoi = soLuongCu + soLuongThem;
                        existingRow["SoLuong"] = soLuongMoi;
                        existingRow["ThanhTien"] = soLuongMoi * donGia;
                    }
                    else
                    {
                        // Thêm sản phẩm mới
                        DataRow newRow = dtChiTiet.NewRow();
                        //newRow["MaDDH"] = maDDH;
                        newRow["MaSP"] = maSP;
                        newRow["SoLuong"] = soLuongThem;
                        newRow["DonGia"] = donGia;
                        newRow["ThanhTien"] = soLuongThem * donGia;
                        dtChiTiet.Rows.Add(newRow);
                    }

                    // Tính tổng tiền
                    decimal tongTien = dtChiTiet.AsEnumerable()
                        .Sum(row => Convert.ToDecimal(row["ThanhTien"]));
                    txtTongTien.Text = tongTien.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK);
                }
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null)
            {
                try
                {
                    // Lấy chỉ số dòng đang chọn
                    int selectedIndex = dgvChiTiet.CurrentRow.Index;

                    // Lấy mã sản phẩm từ dòng được chọn
                    int maSP = Convert.ToInt32(dgvChiTiet.Rows[selectedIndex].Cells["MaSP"].Value);

                    // Tìm dòng tương ứng trong DataTable và xóa
                    DataRow[] rowsToDelete = dtChiTiet.Select($"MaSP = {maSP}");
                    if (rowsToDelete.Length > 0)
                    {
                        dtChiTiet.Rows.Remove(rowsToDelete[0]);
                    }

                    // Tính lại tổng tiền
                    decimal tongTien = dtChiTiet.AsEnumerable()
                        .Sum(row => Convert.ToDecimal(row["ThanhTien"]));
                    txtTongTien.Text = tongTien.ToString();

                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Thông báo", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa!", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
                    }

        private void dgvDonDatHang_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (dgvDonDatHang.CurrentRow != null)
            //{
            //    DataGridViewRow selectedRow = dgvDonDatHang.CurrentRow;
            //    cbMaDatHang.SelectedValue = Convert.ToInt32(selectedRow.Cells["MaDDH"].Value);
            //    cbNhaCungCap.SelectedValue = Convert.ToInt32(selectedRow.Cells["MaNCC"].Value);
            //    dtpNgayDatHang.Value = Convert.ToDateTime(selectedRow.Cells["NgayLapDDH"].Value);
            //    cbTrangThai.SelectedValue = selectedRow.Cells["TrangThai"].Value?.ToString() ?? "";
            //    txtTongTien.Text = selectedRow.Cells["TongTien"].Value?.ToString() ?? "";
            //    int maDDH = Convert.ToInt32(selectedRow.Cells["MaDDH"].Value);
            //    LoadDataCTDDH(maDDH);
            //}
        }

        private void btnLuuDH_Click(object sender, EventArgs e)
        {
            if (themddh)
            {
                DialogResult kq = MessageBox.Show("Bạn có chắc chắn đặt hàng từ nhà cung cấp " + cbNhaCungCap.Text + " không?", "Thông báo", MessageBoxButtons.OKCancel);
                if (kq == DialogResult.OK)
                {
                    int mancc = Convert.ToInt32(cbNhaCungCap.SelectedValue.ToString());
                    int result = don.AddDonDatHang(mancc);
                    if (result > 0)
                    {
                        MessageBox.Show("Đã thêm đơn đặt hàng thành công. Vui lòng chọn sản phẩm cần đặt hàng!", "Thông báo", MessageBoxButtons.OK);
                        LoadDataDDH();
                        themddh = false;
                        themct = true;
                        LoadComponentDisDDH();
                        btnThemCT.Enabled = true;

                        if (dgvDonDatHang.Rows.Count > 0)
                        {
                            int lastIndex = dgvDonDatHang.Rows.Count - 1;
                            dgvDonDatHang.Rows[lastIndex].Selected = true;
                            dgvDonDatHang.CurrentCell = dgvDonDatHang.Rows[lastIndex].Cells[0];
                            dgvDonDatHang.FirstDisplayedScrollingRowIndex = lastIndex;
                        }

                        btnLuuDH.Enabled = btnHuyDH.Enabled = btnTaiLai.Enabled = btnTim.Enabled = btnXoaCT.Enabled =  false;
                        btnLuuCT.Enabled = btnHuyCT.Enabled = true;
                        txtDonGia.Text = txtTongTien.Text = "";
                        numbericSoLuong.Value = 1;

                        cbSanPham.Enabled = txtDonGia.Enabled = numbericSoLuong.Enabled = true;

                        dgvDonDatHang.Enabled = false;
                    }
                }
            }
        }
    }

}
