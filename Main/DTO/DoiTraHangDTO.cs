using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DTO
{
    public class DoiTraHangDTO
    {
        public string MaHDDT { get; set; }  // Mã hóa đơn đổi/trả
        public string MaHD { get; set; }    // Mã hóa đơn
        public string MaSP { get; set; }    // Mã sản phẩm
        public int SoLuong { get; set; }    // Số lượng sản phẩm đổi/trả
        public string LyDo { get; set; }    // Lý do đổi/trả
        public bool IsTra { get; set; }     // Trả hàng (True) hay Đổi hàng (False)

        // Constructor không tham số
        public DoiTraHangDTO() { }

        // Constructor có tham số để khởi tạo các thuộc tính
        public DoiTraHangDTO(string maHDDT, string maHD, string maSP, int soLuong, string lyDo, bool isTra)
        {
            MaHDDT = maHDDT;
            MaHD = maHD;
            MaSP = maSP;
            SoLuong = soLuong;
            LyDo = lyDo;
            IsTra = isTra;
        }
    }
}
