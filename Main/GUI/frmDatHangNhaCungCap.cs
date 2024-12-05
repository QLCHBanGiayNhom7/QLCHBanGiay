using Main.BUS;
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
        public frmDatHangNhaCungCap()
        {
            InitializeComponent();
            don = new DDHNhaCungCapBUS();
            //loadMaDatHang();
            loadNhaCungCap();
            loadSanPham();
            LoadDataDDH();
            LoadComponentDisDDH();
            LoadComponentDisCTDDH();
        }

        //private void loadMaDatHang()
        //{
        //    try
        //    {
        //        var maDDHList = don.GetMaDonDatHangList()
        //          .Select(ma => new { MaDDH = ma })
        //          .ToList();

        //        if (maDDHList.Count > 0)
        //        {
        //            cbMaDatHang.DataSource = maDDHList;
        //            cbMaDatHang.ValueMember = "MaDDH";
        //            cbMaDatHang.DisplayMember = "MaDDH";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không có mã đơn đặt hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi load dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
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
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = false;
        }
        private void LoadStaViewDDH()
        {
            btnLuuDH.Enabled = btnHuyDH.Enabled = false;
            btnThoat.Enabled = btnTaiLai.Enabled = true;
            btnThemDH.Enabled = btnXoaDH.Enabled = btnSuaDH.Enabled = btnIn.Enabled = true;
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
                dgvChiTiet.Columns["MaSP"].HeaderText = "Mã Sản Phẩm";
                dgvChiTiet.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn Giá";
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            }


        }
        private void LoadStaEditCTDDH()
        {
            btnLuuCT.Enabled = btnHuyCT.Enabled = true;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnSuaCT.Enabled = false;
        }
        private void LoadStaViewCTDDH()
        {
            btnLuuCT.Enabled = btnHuyCT.Enabled = false;
            btnThemCT.Enabled = btnXoaCT.Enabled = btnSuaCT.Enabled = true;
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
                cbTrangThai.SelectedValue = selectedRow.Cells["TrangThai"].Value?.ToString() ?? "";
                txtTongTien.Text = selectedRow.Cells["TongTien"].Value?.ToString() ?? "";


                int maDDH = Convert.ToInt32(cbMaDatHang.Text);
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
    }

}
