using Main.BUS;
using Main.DAO;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
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
        bool themddh, suaddh, locdh = false;
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
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = btnLoc.Enabled = btnTaiLai.Enabled =false;
        }
        private void LoadStaViewDDH()
        {
            btnLuuDH.Enabled = btnHuyDH.Enabled = false;
            btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = btnLoc.Enabled = btnTaiLai.Enabled = true;
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
            btnThemCT.Enabled = btnXoaCT.Enabled = false;
        }
        private void LoadStaViewCTDDH()
        {
            btnLuuCT.Enabled = btnHuyCT.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled = true;
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
                cbTrangThai.SelectedItem = selectedRow.Cells["TrangThai"].Value?.ToString() ?? "";
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
            if (dgvDonDatHang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get the selected order's details
                int maDDH = Convert.ToInt32(dgvDonDatHang.SelectedRows[0].Cells["MaDDH"].Value);
                string trangThai = dgvDonDatHang.SelectedRows[0].Cells["TrangThai"].Value?.ToString() ?? "";

                // Check if the order can be deleted
                if (string.IsNullOrEmpty(trangThai) || trangThai == "Đang lên đơn")
                {
                    // Confirm deletion
                    DialogResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa đơn đặt hàng mã {maDDH} không?",
                        "Xác nhận xóa",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.OK)
                    {
                        // Delete order details first
                        int deletedDetails = don.DeleteChiTietDonDatHang(maDDH);

                        // Delete the order
                        int deletedOrder = don.DeleteDonDatHang(maDDH);

                        if (deletedOrder > 0)
                        {
                            MessageBox.Show("Xóa đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Reload data
                            LoadDataDDH();

                            // Clear detail grid
                            dgvChiTiet.DataSource = null;

                            // Reset input fields
                            cbMaDatHang.SelectedIndex = -1;
                            cbNhaCungCap.SelectedIndex = -1;
                            dtpNgayDatHang.Value = DateTime.Now;
                            cbTrangThai.SelectedIndex = -1;
                            txtTongTien.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa đơn đặt hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Chỉ được xóa đơn hàng có trạng thái 'Đang lên đơn' hoặc trạng thái trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaDH_Click(object sender, EventArgs e)
        {
            suaddh = true;
            LoadStaEditDDH();
            cbTrangThai.Enabled = true;
            string trangThai = dgvDonDatHang.SelectedRows[0].Cells["TrangThai"].Value?.ToString() ?? "";

            if (string.IsNullOrEmpty(trangThai) || trangThai == "Đang lên đơn")
            {
                btnThemCT.Enabled = btnXoaCT.Enabled = btnLuuCT.Enabled = btnHuyCT.Enabled = true;
                cbSanPham.Enabled = txtDonGia.Enabled = numbericSoLuong.Enabled = true;
                suact = true;
            }
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

                DataRow[] existingRows = dtChiTiet.Select($"MaSP = {maSP}");
                if (existingRows.Length > 0)
                {
                    DataRow existingRow = existingRows[0];
                    int soLuongCu = Convert.ToInt32(existingRow["SoLuong"]);
                    int soLuongMoi = soLuongCu + soLuongThem;
                    existingRow["SoLuong"] = soLuongMoi;
                    existingRow["ThanhTien"] = soLuongMoi * donGia;
                }
                else
                {
                    DataRow newRow = dtChiTiet.NewRow();
                    //newRow["MaDDH"] = maDDH;
                    newRow["MaSP"] = maSP;
                    newRow["SoLuong"] = soLuongThem;
                    newRow["DonGia"] = donGia;
                    newRow["ThanhTien"] = soLuongThem * donGia;
                    dtChiTiet.Rows.Add(newRow);
                }

                decimal tongTien = dtChiTiet.AsEnumerable()
                    .Sum(row => Convert.ToDecimal(row["ThanhTien"]));
                txtTongTien.Text = tongTien.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK);
            }

        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null)
            {
                try
                {
                    int selectedIndex = dgvChiTiet.CurrentRow.Index;

                    int maSP = Convert.ToInt32(dgvChiTiet.Rows[selectedIndex].Cells["MaSP"].Value);

                    if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
                    {
                        if (suact)
                        {
                            DialogResult result = MessageBox.Show(
                                "Bạn có chắc chắn muốn xóa chi tiết sản phẩm này?",
                                "Xác nhận",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Question
                            );

                            if (result == DialogResult.OK)
                            {
          
                                DataRow[] rowsToDelete = dtChiTiet.Select($"MaSP = {maSP}");
                                if (rowsToDelete.Length > 0)
                                {
                                    dtChiTiet.Rows.Remove(rowsToDelete[0]);
                                }

                             
                                decimal tongTien = dtChiTiet.Rows.Count > 0
                                    ? dtChiTiet.AsEnumerable()
                                        .Sum(row => Convert.ToDecimal(row["ThanhTien"]))
                                    : 0;
                                txtTongTien.Text = tongTien.ToString();

                                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                            }
                        }

                        else
                        {
                            MessageBox.Show("Không thể xóa!", "Thông báo", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
      
                        DataRow[] rowsToDelete = dtChiTiet.Select($"MaSP = {maSP}");
                        if (rowsToDelete.Length > 0)
                        {
                            dtChiTiet.Rows.Remove(rowsToDelete[0]);
                        }

               
                        decimal tongTien = dtChiTiet.Rows.Count > 0
                            ? dtChiTiet.AsEnumerable()
                                .Sum(row => Convert.ToDecimal(row["ThanhTien"]))
                            : 0;
                        txtTongTien.Text = tongTien.ToString();

                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK);
                    }
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

        private void btnLuuCT_Click(object sender, EventArgs e)
        {
            if (themct)
            {
                if (dtChiTiet == null || dtChiTiet.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm sản phẩm vào đơn đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    int maDDH = Convert.ToInt32(cbMaDatHang.SelectedValue);

                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSP"]);
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        decimal donGia = Convert.ToDecimal(row["DonGia"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

                       
                        int result = don.AddChiTietDonDatHang(maDDH, maSP, soLuong, donGia, thanhTien);

                        if (result <= 0)
                        {
                            MessageBox.Show($"Không thể thêm chi tiết cho sản phẩm mã {maSP}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    MessageBox.Show("Đã lưu chi tiết đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

       
                    LoadDataDDH();
                    LoadDataCTDDH(maDDH);

     
                    themct = false;
                    LoadComponentDisCTDDH();
                    LoadStaViewDDH();


    
                    cbSanPham.SelectedIndex = 0;
                    numbericSoLuong.Value = 1;
                    txtDonGia.Clear();
                    txtThanhTien.Clear();

      
                    dgvDonDatHang.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu chi tiết đơn đặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (suact)
            {
                try
                {
                    int maDDH = Convert.ToInt32(cbMaDatHang.SelectedValue);

             
                    foreach (DataRow row in dtChiTiet.Rows)
                    {
                        int maSP = Convert.ToInt32(row["MaSP"]);
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        decimal donGia = Convert.ToDecimal(row["DonGia"]);
                        decimal thanhTien = Convert.ToDecimal(row["ThanhTien"]);

     
                        bool chiTietTonTai = don.CheckChiTietDonDatHang(maDDH, maSP);

                        if (chiTietTonTai)
                        {
 
                            int result = don.UpdateChiTietDonDatHang(maDDH, maSP, soLuong, donGia);

                            if (result <= 0)
                            {
                                MessageBox.Show($"Không thể sửa chi tiết cho sản phẩm mã {maSP}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
            
                            int result = don.AddChiTietDonDatHang(maDDH, maSP, soLuong, donGia, thanhTien);

                            if (result <= 0)
                            {
                                MessageBox.Show($"Không thể thêm chi tiết mới cho sản phẩm mã {maSP}!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    MessageBox.Show("Đã cập nhật chi tiết đơn đặt hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

     
                    LoadDataDDH();
                    LoadDataCTDDH(maDDH);

 
                    suact = false;
                    LoadComponentDisCTDDH();

                    LoadStaViewDDH();

                    cbSanPham.SelectedIndex = 0;
                    numbericSoLuong.Value = 1;
                    txtDonGia.Clear();
                    txtThanhTien.Clear();

      
                    dgvDonDatHang.Enabled = true;

                    btnThemCT.Enabled = btnXoaCT.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật chi tiết đơn đặt hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            int ma = Convert.ToInt32(cbMaDatHang.Text.ToString());
            LoadDataCTDDH(ma);
        }

        private void btnHuyDH_Click(object sender, EventArgs e)
        {
            loadMaDatHang();
            loadNhaCungCap();
            loadSanPham();
            LoadDataDDH();
            LoadComponentDisDDH();
            LoadComponentDisCTDDH();
            LoadStaViewDDH();
            themddh = suaddh = false;
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            loadMaDatHang();
            loadNhaCungCap();
            loadSanPham();
            LoadDataDDH();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            locdh = true;
            LoadStaEditDDH();
            dtpNgayDatHang.Enabled = true;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text.Trim();
                var searchResult = don.SearchDDH(keyword);

                if (searchResult != null && searchResult.Count > 0)
                {
                    dgvDonDatHang.DataSource = searchResult;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn đặt hàng nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDonDatHang.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        btnXoaCT.Enabled = true;

                        if (dgvDonDatHang.Rows.Count > 0)
                        {
                            int lastIndex = dgvDonDatHang.Rows.Count - 1;
                            dgvDonDatHang.Rows[lastIndex].Selected = true;
                            dgvDonDatHang.CurrentCell = dgvDonDatHang.Rows[lastIndex].Cells[0];
                            dgvDonDatHang.FirstDisplayedScrollingRowIndex = lastIndex;
                        }

                        btnLuuDH.Enabled = btnHuyDH.Enabled = btnTaiLai.Enabled = btnTim.Enabled = btnXoaCT.Enabled = false;
                        btnLuuCT.Enabled = btnHuyCT.Enabled = true;
                        txtDonGia.Text = txtTongTien.Text = "";
                        numbericSoLuong.Value = 1;

                        cbSanPham.Enabled = txtDonGia.Enabled = numbericSoLuong.Enabled = true;

                        dgvDonDatHang.Enabled = false;
                    }
                }
            }
            else if (suaddh)
            {
                try
                {
                    if (dgvDonDatHang.CurrentRow != null)
                    {

                        int maDDH = Convert.ToInt32(dgvDonDatHang.CurrentRow.Cells["MaDDH"].Value);

                   
                        string newStatus = cbTrangThai.Text; 

                        if (!string.IsNullOrEmpty(newStatus))
                        {
                            var dao = new DDHNhaCungCapDAO();
                            int result = dao.UpdateDonDatHangStatus(maDDH, newStatus);

                            if (result > 0)
                            {
                                MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataDDH();
                                LoadStaViewDDH();
                            }
                            else
                            {
                                MessageBox.Show("Không thể cập nhật trạng thái đơn đặt hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Trạng thái không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn đơn đặt hàng để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi cập nhật trạng thái: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(locdh)
            {
                try
                {
                    int month = int.Parse(dtpNgayDatHang.Value.Month.ToString());
                    int year = int.Parse(dtpNgayDatHang.Value.Year.ToString());

                    var filteredData = don.GetDDHByMonth(month, year);

                    if (filteredData != null && filteredData.Count > 0)
                    {
                        dgvDonDatHang.DataSource = filteredData;
                    }
                    else
                    {
                        MessageBox.Show("Không có đơn đặt hàng nào trong tháng và năm đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvDonDatHang.DataSource = null;
                    }
                    LoadComponentDisDDH();
                    locdh = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
