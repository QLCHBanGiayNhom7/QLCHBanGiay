using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    internal class HoaDonDAO
    {
        private string connectionString = "Server=DESKTOP-AQ2QICV\\SQLEXPRESS;Database=db_shopBanGiay;Trusted_Connection=True";

        // Lấy toàn bộ danh sách hóa đơn
        public DataTable GetAllHoaDon()
        {
            string query = "SELECT MaHD, NgayLapHD, TongTien, MaKH, MaNV, MaKM FROM HoaDon";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        // Tìm kiếm hóa đơn theo mã
        public DataTable SearchHoaDonByMaHD(string maHD)
        {
            string query = "SELECT MaHD, NgayLapHD, TongTien, MaKH, MaNV, MaKM FROM HoaDon WHERE MaHD = @MaHD";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHD", maHD);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public bool CapNhatTongTien(string maHD)
        {
            string query = "UPDATE HoaDon SET TongTien = (SELECT SUM(ThanhTien) FROM ChiTietHoaDon WHERE MaHD = @MaHD) WHERE MaHD = @MaHD";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaHD", maHD);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
