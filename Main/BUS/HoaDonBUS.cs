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

        public List<HoaDonDTO> GetAllHoaDon()
        {
            DataTable dt = hoaDonDAO.GetAllHoaDon();
            List<HoaDonDTO> hoaDonList = new List<HoaDonDTO>();

            if (dt != null && dt.Rows.Count > 0) // Kiểm tra null và số lượng dòng
            {
                foreach (DataRow row in dt.Rows)
                {
                    HoaDonDTO hoaDon = new HoaDonDTO
                    {
                        MaHD = row["MaHD"]?.ToString() ?? string.Empty, // Kiểm tra null
                        NgayLapHD = row["NgayLapHD"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayLapHD"]),
                        TongTien = row["TongTien"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TongTien"]),
                        MaNV = row["MaNV"]?.ToString() ?? string.Empty,
                        MaKH = row["MaKH"]?.ToString() ?? string.Empty
                    };

                    hoaDonList.Add(hoaDon);
                }
            }
            else
            {
                // Bạn có thể thêm thông báo nếu cần
                Console.WriteLine("Không có dữ liệu hóa đơn để xử lý.");
            }

            return hoaDonList;
        }

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
                    MaNV = row["MaNV"].ToString(),
                    MaKH = row["MaKH"].ToString()
                };
            }

            return null; // Không tìm thấy hóa đơn
        }
        public bool AddHoaDon(HoaDonDTO hoaDonDTO)
        {
            HoaDonDAO hoaDonDAO = new HoaDonDAO();
            return hoaDonDAO.AddHoaDon(hoaDonDTO);
        }

        public bool UpdateHoaDon(HoaDonDTO hoaDonDTO)
        {
            HoaDonDAO hoaDonDAO = new HoaDonDAO();
            return hoaDonDAO.UpdateHoaDon(hoaDonDTO);
        }
    }
}
