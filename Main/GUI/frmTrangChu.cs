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
using Sunny.UI;
namespace Main.GUI
{
    public partial class frmTrangChu : UIAsideHeaderMainFrame
    {
        private string taiKhoan = "a";
        private string matKhau = "a";
        private DangNhapBUS dangNhapBUS;
        public frmTrangChu(string taiKhoan, string matKhau)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
            lblTenDangNhap.Text = taiKhoan;
            dangNhapBUS = new DangNhapBUS();
            KiemTraQuyen();
        }
        private void KiemTraQuyen()
        {
            string chucVu = dangNhapBUS.LayChucVuTheoDangNhap(taiKhoan, matKhau);

            if (chucVu == "Nhân viên bán hàng")
            {
               
            }
            if (chucVu == "Nhân viên quản lý hàng hóa")
            {
                
            }
            if (chucVu == "Nhân viên bảo hành và đổi trả")
            {
               
            }
            if (chucVu == "Nhân viên marketing")
            {
                int pageIndex = 1000;
                TreeNode root = Aside.CreateNode("KHUYẾN MÃI", 61451, 24, pageIndex);
                Aside.NodeMouseClick += Aside_NodeMouseClick;
            }
            if (chucVu == "Nhân viên thống kê")
            {
               
            }
            if (chucVu == "Quản lý" || chucVu == null)
            {
                int pageIndex = 1000;
                TreeNode root = Aside.CreateNode("KHUYẾN MÃI", 61451, 24, pageIndex);

                //Aside.CreateChildNode(root, AddPage(new frmDanhMuc(), ++pageIndex));
                //Aside.CreateChildNode(root, AddPage(new frmSanPham(), ++pageIndex));

                pageIndex = 2000;
                root = Aside.CreateNode("QUẢN LÝ KHÁCH HÀNG", 61451, 24, pageIndex);

                pageIndex = 3000;
                root = Aside.CreateNode("QUẢN LÝ GIAO DỊCH", 61451, 24, pageIndex);
                //Aside.CreateChildNode(root, AddPage(new frmBanHang(), ++pageIndex));
                //Aside.CreateChildNode(root, AddPage(new frmNhapHang(), ++pageIndex));
                //Aside.CreateChildNode(root, AddPage(new frmTraHang(), ++pageIndex));
                //Aside.CreateChildNode(root, AddPage(new frmYeuCauBaoHanh(), ++pageIndex));

                pageIndex = 4000;
                root = Aside.CreateNode("SẢN PHẨM", 61451, 24, pageIndex);

                pageIndex = 5000;
                root = Aside.CreateNode("KHO HÀNG", 61451, 24, pageIndex);


                pageIndex = 7000;
                root = Aside.CreateNode("NHÂN VIÊN", 61451, 24, pageIndex);

                pageIndex = 8000;
                root = Aside.CreateNode("BÁO CÁO DOANH THU", 61451, 24, pageIndex);

                pageIndex = 9000;
                root = Aside.CreateNode("QUẢN LÝ GIAO HÀNG", 61451, 24, pageIndex);

                Aside.NodeMouseClick += Aside_NodeMouseClick;
            }
        }

        private void Aside_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text == "NHÂN VIÊN")
            {

                //var page = new frmNhanVien();
                //AddPage(page, 7000);
                //SelectPage(7000);
            }
            else if (e.Node.Text == "QUẢN LÝ KHÁCH HÀNG")
            {
                //var page = new frmThongTinKhachHang();
                //AddPage(page, 2000);
                //SelectPage(2000);
            }
            else if (e.Node.Text == "QUẢN LÝ GIAO HÀNG")
            {
                //var page = new frmGiaoHang();
                //AddPage(page, 9000);
                //SelectPage(9000);
            }
            else if (e.Node.Text == "KHO HÀNG")
            {
                var page = new frmDatHangNhaCungCap();
                AddPage(page, 5000);
                SelectPage(5000);
            }
            else if (e.Node.Text == "BÁO CÁO DOANH THU")
            {
                //var page = new frmBaoCaoDoanhThu();
                //AddPage(page, 8000);
                //SelectPage(8000);
            }
            else if (e.Node.Text == "KHUYẾN MÃI")
            {
                var page = new frmKhuyenMai();
                AddPage(page, 1000);
                SelectPage(1000);
            }
        }

    }
}
