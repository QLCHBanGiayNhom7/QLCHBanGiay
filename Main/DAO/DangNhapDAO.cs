using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    public class DangNhapDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();
        public string LayChucVuTheoDangNhap(string tenTaiKhoan, string matKhau)
        {
            try
            {
                var idNguoiDung = dbQLBanGiayDataContext.NguoiDungs
                    .Where(nd => nd.TenTaiKhoan == tenTaiKhoan && nd.MatKhau == matKhau && nd.TonTai == true)
                    .Select(nd => nd.IdNguoiDung)
                    .FirstOrDefault();

                //if (idNguoiDung == 0)
                //    return null;

                var chucVu = dbQLBanGiayDataContext.NhanViens
                    .Where(nv => nv.IdNguoiDung == idNguoiDung)
                    .Select(nv => nv.ChucVu)
                    .FirstOrDefault();

                return chucVu;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xác thực: {ex.Message}");
                return null;
            }
        }
    }
}
