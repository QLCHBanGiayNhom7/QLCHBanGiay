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
        public List<dynamic> GetChiTietDonDH(string maDDH)
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

        public List<string> GetMaDonDatHangList()
        {
            return don.GetMaDonDatHangList();
        }
        public string layMaKMMoi()
        {
            return don.layMaDDH();
        }
        public string AddDonDatHang(string maDDH, string maNCC)
        {
            return don.AddDonDatHang(maDDH, maNCC);
        }

        public string AddChiTietDonDatHang(string maDDH, string maSP, int soLuong, decimal donGia)

        {
            return don.AddChiTietDonDatHang(maDDH, maSP, soLuong, donGia);
        }

        public int DeleteChiTietDonDatHang(string maDDH)
        {
            return don.DeleteChiTietDonDatHang(maDDH);
        }

        public int DeleteDonDatHang(string maDDH)
        {
            return don.DeleteDonDatHang(maDDH);
        }

        public string UpdateChiTietDonDatHang(string maDDH, string maSP, int soLuong, decimal donGia)
        {
            return don.UpdateChiTietDonDatHang(maDDH, maSP, soLuong,donGia); 
        }
        public bool CheckChiTietDonDatHang(string maDDH, string maSP)
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
        public int UpdateDonDatHangStatus(string maDDH, string newStatus)
        {
            return don.UpdateDonDatHangStatus(maDDH, newStatus);
        }
    }
}
