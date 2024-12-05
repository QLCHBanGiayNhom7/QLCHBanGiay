using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    internal class KhoDAO
    {
        private string connectionString = "Server=DESKTOP-AQ2QICV\\SQLEXPRESS;Database=db_shopBanGiay;Trusted_Connection=True";
        public KhoDTO GetKho(string maSP)
        {
            KhoDTO kho = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Kho WHERE MaSP = @maSP";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@maSP", maSP);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                kho = new KhoDTO
                                {
                                    MaSP = reader["MaSP"].ToString(),
                                    TenSP = reader["TenSP"].ToString(),
                                    SoLuongTon = Convert.ToInt32(reader["SoLuongTon"]),
                                    GiaNhap = Convert.ToDouble(reader["GiaNhap"]),
                                    GiaBan = Convert.ToDouble(reader["GiaBan"]),
                                    NgayNhap = Convert.ToDateTime(reader["NgayNhap"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin kho: " + ex.Message);
            }
            return kho;
        }

        // Cập nhật số lượng trong kho (Khi trả hoặc đổi hàng)
        public bool CapNhatKho(string maSP, int soLuong, bool isTra)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = isTra
                        ? "UPDATE Kho SET SoLuongTon = SoLuongTon + @soLuong WHERE MaSP = @maSP" // Khi trả hàng
                        : "UPDATE Kho SET SoLuongTon = SoLuongTon - @soLuong WHERE MaSP = @maSP"; // Khi đổi hàng

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@soLuong", soLuong);
                        cmd.Parameters.AddWithValue("@maSP", maSP);
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật kho: " + ex.Message);
                return false;
            }
        }
    }
}
