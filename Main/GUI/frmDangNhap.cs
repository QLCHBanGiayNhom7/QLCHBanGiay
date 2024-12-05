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
    public partial class frmDangNhap : Form
    {
        private DangNhapBUS nguoiDungBUS = new DangNhapBUS();

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string tenTaiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            // Kiểm tra trường hợp trống
            if (string.IsNullOrWhiteSpace(tenTaiKhoan) || string.IsNullOrWhiteSpace(matKhau))
            {
                MessageBox.Show("Tài khoản và mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tài khoản và mật khẩu
            if (!nguoiDungBUS.DangNhap(tenTaiKhoan, matKhau))
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng hoặc không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra TonTai (mặc định đã kiểm tra = 1 ở DAL)
            MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Chuyển sang form chính
            frmTrangChu formMain = new frmTrangChu("a","a");
            formMain.Show();
            this.Hide();
        }
    }
}
