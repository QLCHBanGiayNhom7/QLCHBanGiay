﻿using System;
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
using static iText.Layout.Borders.Border;

namespace Main.GUI
{
    public partial class frmTrangChu : UIAsideHeaderMainFrame
    {
        private string taiKhoan;
        private DangNhapBUS dangNhapBUS;
        public frmTrangChu(string taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            lblTenDangNhap.Text = taiKhoan;
            dangNhapBUS = new DangNhapBUS();
            KiemTraQuyen();
        }
        private void KiemTraQuyen()
        {
            string chucVu = dangNhapBUS.GetChucVuByTaiKhoan(taiKhoan);

            if (string.IsNullOrEmpty(chucVu))
            {
                chucVu = "Quản lý";
            }

            if (chucVu == "Nhân viên bán hàng")
            {
                int pageIndex = 2000;
                TreeNode root = Aside.CreateNode("HÓA ĐƠN", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmQLHoaDon(), ++pageIndex));
            }
            else if (chucVu == "Nhân viên quản lý hàng hóa")
            {
                int pageIndex = 3000;
                TreeNode root = Aside.CreateNode("KHO HÀNG", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmKho(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmQLNhaCungCap(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmDatHangNhaCungCap(), ++pageIndex));
            }
            else if (chucVu == "Nhân viên bảo hành và đổi trả")
            {
                int pageIndex = 4000;
                TreeNode root = Aside.CreateNode("ĐỔI TRẢ", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmPhieuDoiTraHang(), ++pageIndex));
            }
            else if (chucVu == "Nhân viên marketing")
            {
                int pageIndex = 5000;
                TreeNode root = Aside.CreateNode("KHUYẾN MÃI", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmKhuyenMai(), ++pageIndex));
            }
            else if (chucVu == "Nhân viên thống kê")
            {
                int pageIndex = 6000;
                TreeNode root = Aside.CreateNode("THỐNG KÊ", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmBaoCaoThongKe(), ++pageIndex));
            }
            else if (chucVu == "Quản lý")
            {
                int pageIndex = 1000;
                TreeNode root = Aside.CreateNode("KHUYẾN MÃI", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmKhuyenMai(), ++pageIndex));

                pageIndex = 3000;
                root = Aside.CreateNode("GIAO DỊCH KHÁCH HÀNG", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmKhachHang(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmQLHoaDon(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmPhieuDoiTraHang(), ++pageIndex));

                pageIndex = 5000;
                root = Aside.CreateNode("KHO HÀNG", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmKho(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmQLNhaCungCap(), ++pageIndex));
                Aside.CreateChildNode(root, AddPage(new frmDatHangNhaCungCap(), ++pageIndex));

                pageIndex = 7000;
                root = Aside.CreateNode("NHÂN VIÊN", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmQLNhanVien(), ++pageIndex));

                pageIndex = 9000;
                root = Aside.CreateNode("BÁO CÁO THỐNG KÊ", 61451, 24, pageIndex);
                Aside.CreateChildNode(root, AddPage(new frmBaoCaoThongKe(), ++pageIndex));
            }
        }


        private void Aside_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            
        }

    }
}
