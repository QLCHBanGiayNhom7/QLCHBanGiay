using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    internal class KhachHangDAO
    {
        private string connectionString = "Server=DESKTOP-AQ2QICV\\SQLEXPRESS;Database=db_qlshopBanGiay;Trusted_Connection=True;";

        public KhachHangDTO GetKhachHangByMaKH(string maKH)
        {
            string query = "SELECT MaKH, TenKH, SoDienThoai,NgaySinh, GioiTinh FROM KhachHang WHERE MaKH = @MaKH";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaKH", maKH);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new KhachHangDTO
                    {
                        MaKH = reader["MaKH"].ToString(),
                        TenKH = reader["TenKH"].ToString(),
                        NgaySinh = reader["NgaySinh"].ToString(),
                        GioiTinh = reader["GioiTinh"].ToString(),
                        SoDienThoai = reader["SoDienThoai"].ToString()
                    };
                }
            }
            return null;
        }
    }
}
