using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO; 
namespace Main.DAO
{
    public class KhachHangDAO
    {
        private dbQLBanGiayDataContext db = new dbQLBanGiayDataContext();
        //public List<KhachHangDTO> GetAllKhachHang()
        //{
        //    var khachHangs = db.KhachHangs.Select(kh => new KhachHangDTO
        //    {
        //        MaKH = kh.MaKH,
        //        TenKH = kh.TenKH,
        //        SoDienThoai = kh.SoDienThoai,
        //        NgaySinh = kh.NgaySinh ?? DateTime.Now,
        //        GioiTinh = kh.GioiTinh
        //    }).ToList();

        //    return khachHangs;
        //}
        public bool AddKhachHang(KhachHangDTO khachHangDTO)
        {
            try
            {
                var khachHang = new KhachHang
                {
                    TenKH = khachHangDTO.TenKH,
                    SoDienThoai = khachHangDTO.SoDienThoai,
                    NgaySinh = khachHangDTO.NgaySinh,
                    GioiTinh = khachHangDTO.GioiTinh
                };

                db.KhachHangs.InsertOnSubmit(khachHang);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public bool UpdateKhachHang(KhachHangDTO khachHangDTO)
        //{
        //    try
        //    {
        //        var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == khachHangDTO.MaKH);
        //        if (khachHang != null)
        //        {
        //            khachHang.TenKH = khachHangDTO.TenKH;
        //            khachHang.SoDienThoai = khachHangDTO.SoDienThoai;
        //            khachHang.NgaySinh = khachHangDTO.NgaySinh;
        //            khachHang.GioiTinh = khachHangDTO.GioiTinh;

        //            db.SubmitChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public bool DeleteKhachHang(int maKH)
        //{
        //    try
        //    {
        //        var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
        //        if (khachHang != null)
        //        {
        //            db.KhachHangs.DeleteOnSubmit(khachHang);
        //            db.SubmitChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}



    }
}
