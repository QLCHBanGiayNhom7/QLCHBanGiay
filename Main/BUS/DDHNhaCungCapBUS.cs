using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Main.BUS
{
    internal class DDHNhaCungCapBUS
    {
        DDHNhaCungCapDAO don;
        public DDHNhaCungCapBUS()
        {
            don = new DDHNhaCungCapDAO();
        }
        public List<dynamic> GetChiTietDonDH(int maDDH)
        {
            return don.GetChiTietDonDH(maDDH);
        }
        public List<dynamic> GetDonDH()
        {
            return don.GetDDH();
        }
        public List<SanPham> GetSanPham()
        {
            return don.GetSanPham();
        }
        public List<NhaCungCap> GetNhaCungCap()
        {
            return don.GetNhaCungCap();
        }

        public List<int> GetMaDonDatHangList()
        {
            return don.GetMaDonDatHangList();
        }
        public int AddDonDatHang(int maNCC)
        {
            return don.AddDonDatHang(maNCC);
        }

        public int AddChiTietDonDatHang(int maDDH, int maSP, int soLuong, decimal donGia, decimal thanhTien)

        {
            return don.AddChiTietDonDatHang(maDDH, maSP, soLuong, donGia, thanhTien);
        }

        public int DeleteChiTietDonDatHang(int maDDH)
        {
            return don.DeleteChiTietDonDatHang(maDDH);
        }

        public int DeleteDonDatHang(int maDDH)
        {
            return don.DeleteDonDatHang(maDDH);
        }

        public int UpdateChiTietDonDatHang(int maDDH, int maSP, int soLuong, decimal donGia)
        {
            return don.UpdateChiTietDonDatHang(maDDH, maSP, soLuong,donGia); 
        }
        public bool CheckChiTietDonDatHang(int maDDH, int maSP)
        {
            return don.CheckChiTietDonDatHang(maDDH, maSP);
        }
        public List<dynamic> GetDDHByMonth(int month, int year)
        {
            return don.GetDDHByMonth(month, year);
        }
        public List<dynamic> SearchDDH(string keyword)
        {
            return don.SearchDDH(keyword);
        }
    }
}
