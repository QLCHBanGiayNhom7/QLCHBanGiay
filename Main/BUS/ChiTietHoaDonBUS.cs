using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class ChiTietHoaDonBUS
    {
        private ChiTietHoaDonDAO chiTietHoaDonDAO = new ChiTietHoaDonDAO();

        public List<ChiTietHoaDonDTO> GetSanPhamByMaHD(string maHD)
        {
            return chiTietHoaDonDAO.GetSanPhamByMaHD(maHD);
        }
        public bool CapNhatSoLuongTra(string maHD, string maSP, int soLuongTra)
        {
            return chiTietHoaDonDAO.CapNhatSoLuongTra(maHD, maSP, soLuongTra);
        }

        public bool CapNhatSoLuongDoi(string maHD, string maSP, int soLuongDoi)
        {
            return chiTietHoaDonDAO.CapNhatSoLuongTra(maHD, maSP, soLuongDoi);
        }

        public bool XoaChiTietHoaDon(string maHD, string maSP)
        {
            return chiTietHoaDonDAO.XoaChiTietHoaDon(maHD, maSP);
        }
        public bool ThemChiTietHoaDon(string maHD, string maSP, int soLuong, decimal giaBan)
        {
            return chiTietHoaDonDAO.ThemSanPhamDoiVaoChiTietHoaDon( maHD, maSP, soLuong, giaBan);
        }
        public int LaySoLuongTrongHoaDon(string maHD, string maSP)
        {
            try
            {
                return chiTietHoaDonDAO.LaySoLuongTrongHoaDon(maHD, maSP);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi gọi dữ liệu số lượng từ DAO: " + ex.Message);
            }
        }
    }
}
