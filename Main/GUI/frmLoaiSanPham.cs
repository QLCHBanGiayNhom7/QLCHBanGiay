using Main.BUS;
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
using Main.DAO;
using Main.DTO;
using Sunny.UI;

namespace Main.GUI
{
    public partial class frmLoaiSanPham : UIPage
    {
        private LoaiSanPhamBUS loaispBUS;
        public frmLoaiSanPham()
        {
            InitializeComponent();
            loaispBUS = new LoaiSanPhamBUS();
            TextboxDisable();
            LoadNV();
            dgv_lsp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void LoadNV()
        {
            dgv_lsp.ColumnHeadersVisible = true;
            try
            {
                var dsLSP = loaispBUS.GetLoaiSanPham();

                if (dsLSP != null && dsLSP.Count > 0)
                {
                    dgv_lsp.DataSource = dsLSP;


                    dgv_lsp.Columns["MaLoai"].HeaderText = "Mã Loại";
                    dgv_lsp.Columns["TenLoai"].HeaderText = "Tên Loại";
                    dgv_lsp.Columns["GioiTinh"].HeaderText = "Giới Tính";



                    dgv_lsp.ColumnHeadersDefaultCellStyle.BackColor = Color.LightPink;
                    dgv_lsp.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                    dgv_lsp.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                    dgv_lsp.EnableHeadersVisualStyles = false;
                    dgv_lsp.ColumnHeadersHeight = 50;



                    dgv_lsp.Columns["MaLoai"].DisplayIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không có loại sản phẩm nào để hiển thị.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã có lỗi xảy ra: " + ex.Message);
            }
        }
        private void TextboxDisable()
        {
            txt_MaLoai.Enabled = false;
            txt_TenLoai.Enabled = false;
            rdo_Nam.Enabled = false;
            rdo_Nu.Enabled = false;
        }
        private void TextboxEnable()
        {
            txt_MaLoai.Enabled = false;
            txt_TenLoai.Enabled = true;
            rdo_Nam.Enabled = true;
            rdo_Nu.Enabled = true;
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            TextboxDisable();
            btn_Them.Enabled = true;
            btn_Xoa.Enabled = true;
            btn_Sua.Enabled = true;
            txt_MaLoai.Text = string.Empty;
            txt_TenLoai.Text = string.Empty;
            rdo_Nam.Checked = false;
            rdo_Nu.Checked = false;
            LoadNV();
        }

        private void dgv_lsp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgv_lsp.Rows[e.RowIndex];
                    txt_MaLoai.Text = row.Cells["MaLoai"].Value.ToString();
                    txt_TenLoai.Text = row.Cells["TenLoai"].Value.ToString();
                    string gioiTinh = row.Cells["GioiTinh"].Value.ToString();
                    if (gioiTinh == "Nam")
                    {
                        rdo_Nam.Checked = true;
                    }
                    else if (gioiTinh == "Nữ")
                    {
                        rdo_Nu.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu từ ô: " + ex.Message);
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_MaLoai.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã loại.");
                    return;
                }

                List<string> maLoaiList = new List<string> { txt_MaLoai.Text };
                string result = loaispBUS.DeleteLoaiSanPham(maLoaiList);
                MessageBox.Show(result);
                LoadNV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message);
            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Them.Enabled = true;
            btn_Xoa.Enabled = true;
            TextboxEnable(); ;
            if (string.IsNullOrEmpty(txt_MaLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn một loại sản phẩm để sửa!");

                return;
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            TextboxEnable();
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
            txt_MaLoai.Text = string.Empty;
            rdo_Nam.Checked = false;
            rdo_Nu.Checked = false;
        }

        private void btn_Tim_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txt_Tim.Text;
                List<LoaiSanPham> searchResults = loaispBUS.SearchLoaiSanPham(keyword);
                dgv_lsp.DataSource = searchResults;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm sản phẩm: " + ex.Message);
            }
        }
    }
}
