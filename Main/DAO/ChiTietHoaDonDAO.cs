using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Main.DTO;
namespace Main.DAO
{
    internal class ChiTietHoaDonDAO
    {
        private string connectionString = "Server=DESKTOP-AQ2QICV\\SQLEXPRESS;Database=db_shopBanGiay;Trusted_Connection=True";

        public List<ChiTietHoaDonDTO> GetSanPhamByMaHD(string maHD)
        {
            List<ChiTietHoaDonDTO> list = new List<ChiTietHoaDonDTO>();
            string query = "SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.GiaBan " +
                           "FROM ChiTietHoaDon ct " +
                           "JOIN SanPham sp ON ct.MaSP = sp.MaSP " +
                           "WHERE ct.MaHD = @MaHD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaHD", maHD);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ChiTietHoaDonDTO item = new ChiTietHoaDonDTO
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        GiaBan = Convert.ToDecimal(reader["GiaBan"]),
                        ThanhTien = Convert.ToInt32(reader["SoLuong"]) * Convert.ToDecimal(reader["GiaBan"])
                    };
                    list.Add(item);
                }
            }

            return list;
        }

        public bool CapNhatSoLuongTra(string maHD, string maSP, int soLuongTra)
        {
            string query = "UPDATE ChiTietHoaDon SET SoLuong = SoLuong - @SoLuongTra WHERE MaHD = @MaHD AND MaSP = @MaSP";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SoLuongTra", soLuongTra);
                command.Parameters.AddWithValue("@MaHD", maHD);
                command.Parameters.AddWithValue("@MaSP", maSP);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool CapNhatSoLuongDoi(int maHD, string maSP, int soLuongDoi)
        {
            // Viết mã để cập nhật số lượng sản phẩm đổi trong chi tiết hóa đơn
            string query = "UPDATE ChiTietHoaDon SET SoLuong = @soLuongDoi WHERE MaHD = @MaHD AND MaSP = @MaSP";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SoLuong", soLuongDoi);
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool ThemSanPhamDoiVaoChiTietHoaDon(string maHD, string maSPDoi, int soLuongDoi, decimal giaBan)
        {
            int mahd= Convert.ToInt32(maHD);
            int maspdoi = Convert.ToInt32(maSPDoi);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, GiaBan) " +
                                   "VALUES (@MaHD, @MaSP, @SoLuong, @GiaBan)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", mahd);
                        cmd.Parameters.AddWithValue("@MaSP", maspdoi);
                        cmd.Parameters.AddWithValue("@SoLuong", soLuongDoi);
                        cmd.Parameters.AddWithValue("@GiaBan", giaBan);

                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm sản phẩm vào chi tiết hóa đơn: " + ex.Message);
                return false;
            }
        }

        public bool XoaChiTietHoaDon(string maHD, string maSP)
        {
            // Viết mã để xóa sản phẩm khỏi chi tiết hóa đơn nếu số lượng = 0
            string query = "DELETE FROM ChiTietHoaDon WHERE MaHD = @MaHD AND MaSP = @MaSP";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", maHD);
                cmd.Parameters.AddWithValue("@MaSP", maSP);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public int LaySoLuongTrongHoaDon(string maHD, string maSP)
        {
            try
            {
                string query = "SELECT SoLuong FROM ChiTietHoaDon WHERE MaHD = @MaHD AND MaSP = @MaSP";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaHD", maHD);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy số lượng trong hóa đơn: " + ex.Message);
            }

            return 0; // Trả về 0 nếu không tìm thấy hoặc có lỗi
        }
    }
}
