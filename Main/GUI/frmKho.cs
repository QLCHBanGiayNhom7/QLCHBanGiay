using Main.BUS;
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

namespace Main.GUI
{
    public partial class frmKho : UIPage
    {
        private KhoBUS khoBUS;
        private string selectedMaSP;
        public frmKho()
        {
            InitializeComponent();
            khoBUS = new KhoBUS();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var khoList = khoBUS.GetAllKho();
                dgv_kho.DataSource = khoList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu kho: " + ex.Message);
            }
            dgv_kho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_kho.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_kho.Columns[0].Frozen = true;
            dgv_kho.ReadOnly = true;
            if (dgv_kho.Columns.Contains("SANPHAM"))
            {
                dgv_kho.Columns.Remove("SANPHAM");
            }
            txtMaSP.Enabled = false;
            txt_TenSP.Enabled = false;
            txt_SLTon.Enabled = false;
            txt_GiaNhap.Enabled = false;
            txt_GiaBan.Enabled = false;
            dtpNgayNhap.Value = DateTime.Now;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string keyword = txt_TimKiem.Text.Trim();
            SearchData(keyword);
            if (dgv_kho.Columns.Contains("SanPham"))
            {
                dgv_kho.Columns.Remove("SanPham");
            }
        }
        private void SearchData(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var filteredData = khoBUS.GetAllKho()
                       .Where(kh => kh.MaSP.ToString().Contains(keyword) ||
                                     kh.TenSP.Contains(keyword))
                       .ToList();

                dgv_kho.DataSource = filteredData;
            }
            else
            {

                LoadData();
            }
        }

        private void dgv_kho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_kho.Rows[e.RowIndex];
                selectedMaSP = row.Cells["MaSP"].Value.ToString();
                txtMaSP.Text = row.Cells["MaSP"].Value.ToString();
                txt_TenSP.Text = row.Cells["TenSP"].Value.ToString();
                txt_SLTon.Text = row.Cells["SoLuongTon"].Value.ToString();
                txt_GiaNhap.Text = row.Cells["GiaNhap"].Value.ToString();
                txt_GiaBan.Text = row.Cells["GiaBan"].Value.ToString();
                dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
            }
        }
    }
}
