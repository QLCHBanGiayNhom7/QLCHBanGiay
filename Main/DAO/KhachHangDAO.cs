using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO; 
namespace Main.DAO
{
    public class KhachHangDAO
    {
        private string connectionString = "Server=DESKTOP-AQ2QICV\\SQLEXPRESS;Database=db_shopBanGiay;Trusted_Connection=True";
        private dbQLBanGiayDataContext db = new dbQLBanGiayDataContext();
        public List<KhachHangDTO> GetAllKhachHang()
        {
            var khachHangs = db.KhachHangs.Select(kh => new KhachHangDTO
            {
                MaKH = kh.MaKH.ToString(),
                TenKH = kh.TenKH,
                SoDienThoai = kh.SoDienThoai,
                NgaySinh = kh.NgaySinh ?? DateTime.Now,
                GioiTinh = kh.GioiTinh
            }).ToList();

            return khachHangs;
        }

        public KhachHangDTO GetKhachHangByMaHD(string maHD)
        {
            KhachHangDTO khachHang = null;

            try
            {
                // Lấy MaKH từ HoaDon bằng MaHD
                string query = "SELECT h.MaKH, k.TenKH, k.SoDienThoai, k.NgaySinh, k.GioiTinh " +
                               "FROM HoaDon h " +
                               "JOIN KhachHang k ON h.MaKH = k.MaKH " +
                               "WHERE h.MaHD = @MaHD";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaHD", maHD);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        khachHang = new KhachHangDTO
                        {
                            MaKH = reader["MaKH"].ToString(),
                            TenKH = reader["TenKH"].ToString(),
                            SoDienThoai = reader["SoDienThoai"].ToString(),
                            NgaySinh = reader["NgaySinh"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["NgaySinh"]),
                            GioiTinh = reader["GioiTinh"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin khách hàng từ mã hóa đơn: " + ex.Message);
            }

            return khachHang;
        }
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
        public bool DeleteKhachHang(int maKH)
        {
            try
            {
                var khachHang = db.KhachHangs.FirstOrDefault(kh => kh.MaKH == maKH);
                if (khachHang != null)
                {
                    db.KhachHangs.DeleteOnSubmit(khachHang);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }



    }
}
