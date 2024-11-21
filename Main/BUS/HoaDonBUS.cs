using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class HoaDonBUS
    {
        private HoaDonDAO hoaDonDAO = new HoaDonDAO();

        // Lấy tất cả hóa đơn
        public List<HoaDonDTO> GetAllHoaDon()
        {
            DataTable dt = hoaDonDAO.GetAllHoaDon();
            List<HoaDonDTO> hoaDonList = new List<HoaDonDTO>();

            foreach (DataRow row in dt.Rows)
            {
                HoaDonDTO hoaDon = new HoaDonDTO
                {
                    MaHD = row["MaHD"].ToString(),
                    NgayLapHD = Convert.ToDateTime(row["NgayLapHD"]),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    MaKH = row["MaKH"].ToString(),
                    MaNV = row["MaNV"].ToString(),
                    MaKM = row["MaKM"].ToString()
                };
                hoaDonList.Add(hoaDon);
            }

            return hoaDonList;
        }

        // Tìm kiếm hóa đơn theo mã
        public HoaDonDTO SearchHoaDonByMaHD(string maHD)
        {
            DataTable dt = hoaDonDAO.SearchHoaDonByMaHD(maHD);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new HoaDonDTO
                {
                    MaHD = row["MaHD"].ToString(),
                    NgayLapHD = Convert.ToDateTime(row["NgayLapHD"]),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    MaKH = row["MaKH"].ToString(),
                    MaNV = row["MaNV"].ToString(),
                    MaKM = row["MaKM"].ToString()
                };
            }
            return null; // Không tìm thấy hóa đơn
        }

        public bool CapNhatTongTien(string maHD)
        {
            return hoaDonDAO.CapNhatTongTien(maHD);
        }
    }
}
