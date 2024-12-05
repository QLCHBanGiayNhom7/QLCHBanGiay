using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    internal class SanPhamDAO
    {
        private string connectionString = "Server=ACER\\Fuang;Database=db_qlshopBanGiay;Trusted_Connection=True;";

        public List<SanPhamDTO> GetSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = "SELECT MaSP, TenSP, GiaBan, Mau, Size, ThuongHieu, MaLoai FROM SanPham";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SanPhamDTO item = new SanPhamDTO
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"])
                    };
                    list.Add(item);
                }
            }

            return list;
        }

        public bool CapNhatSoLuongKho(string maSP, int soLuongTra)
        {
            string query = "UPDATE Kho SET SoLuongTon = SoLuongTon + @SoLuongTra WHERE MaSP = @MaSP";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SoLuongTra", soLuongTra);
                command.Parameters.AddWithValue("@MaSP", maSP);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public DataTable SearchSanPhamByKeyword(string keyword)
        {
            string query = "SELECT MaSP, TenSP, Mau, Size, ThuongHieu, GiaBan FROM SanPham WHERE TenSP LIKE @Keyword OR MaSP LIKE @Keyword";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");  // Tìm kiếm với từ khóa nhập vào

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
