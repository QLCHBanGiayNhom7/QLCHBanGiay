using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    public class DangNhapDAO
    {
        private string connectionString = "Server=ACER\\FUANG;Database=db_shopBanGiay;Trusted_Connection=True";
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();
        public bool KiemTraDangNhap(string tenTaiKhoan, string matKhau)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenTaiKhoan = @TenTaiKhoan AND MatKhau = @MatKhau AND TonTai = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenTaiKhoan", tenTaiKhoan);
                    cmd.Parameters.AddWithValue("@MatKhau", matKhau);
                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
        }

        public string GetChucVuByTaiKhoan(string taiKhoan)
        {
            var nhanVien = dbQLBanGiayDataContext.NhanViens.FirstOrDefault(nv => nv.IdNguoiDung == taiKhoan);
            return nhanVien?.ChucVu;
        }

    }
}
