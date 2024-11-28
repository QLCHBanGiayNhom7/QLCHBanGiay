using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DTO
{
    internal class HoaDonDTO
    {
        public string MaHD { get; set; }
        public DateTime NgayLapHD { get; set; }
        public decimal TongTien { get; set; }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public string MaKM { get; set; }

        public HoaDonDTO() { }

        public HoaDonDTO(string maHD, DateTime ngayLapHD, decimal tongTien, string maKH, string maNV, string maKM)
        {
            MaHD = maHD;
            NgayLapHD = ngayLapHD;
            TongTien = tongTien;
            MaKH = maKH;
            MaNV = maNV;
            MaKM = maKM;
        }
    }
}
