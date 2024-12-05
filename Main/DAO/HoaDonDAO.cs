using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    internal class HoaDonDAO
    {
        private string connectionString = "Server=HUY-PC;Database=db_ShopBanGiay;Trusted_Connection=True;";

        public DataTable GetAllHoaDon()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT * FROM HoaDon"; // Kiểm tra câu truy vấn SQL
                using (SqlConnection conn = new SqlConnection(connectionString)) // Đảm bảo connectionString hợp lệ
                {
                    conn.Open(); // Mở kết nối đến cơ sở dữ liệu
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt); // Đổ dữ liệu vào DataTable
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Lỗi SQL: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi không xác định: " + ex.Message);
            }

            return dt; // Luôn trả về một DataTable (có thể rỗng)
        }
        //public DataTable GetAllHoaDon()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string query = "SELECT * FROM HoaDon";
        //            SqlCommand cmd = new SqlCommand(query, conn);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);

        //            DataTable dt = new DataTable();
        //            da.Fill(dt); // Đổ dữ liệu từ database vào DataTable
        //            return dt;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
        //        }
        //    }
        //}

        public DataTable SearchHoaDonByMaHD(string maHD)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM HoaDon WHERE MaHD = @MaHD";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHD", maHD);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt); // Đổ dữ liệu vào DataTable
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi tìm hóa đơn: " + ex.Message);
                }
            }
        }
        public bool AddHoaDon(HoaDonDTO hoaDon)
        {
            try
            {
                string query = "INSERT INTO HoaDon (MaHD, NgayLapHD, TongTien, MaNV, MaKH, MaKM) " +
                               "VALUES (@MaHD, @NgayLapHD, @TongTien, @MaNV, @MaKH, @MaKM)";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHD", hoaDon.MaHD); // Kiểu string
                    cmd.Parameters.AddWithValue("@NgayLapHD", hoaDon.NgayLapHD);
                    cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                    cmd.Parameters.AddWithValue("@MaNV", hoaDon.MaNV); // Kiểu string
                    cmd.Parameters.AddWithValue("@MaKH", hoaDon.MaKH); // Kiểu string
                    cmd.Parameters.AddWithValue("@MaKM", hoaDon.MaKM ?? (object)DBNull.Value); // Kiểm tra null

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu thêm thành công
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm hóa đơn: " + ex.Message);
                return false;
            }
        }

        public bool UpdateHoaDon(HoaDonDTO hoaDon)
        {
            try
            {
                string query = "UPDATE HoaDon SET NgayLapHD = @NgayLapHD, TongTien = @TongTien, MaNV = @MaNV, MaKH = @MaKH, MaKM = @MaKM WHERE MaHD = @MaHD";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHD", hoaDon.MaHD); // Kiểu string
                    cmd.Parameters.AddWithValue("@NgayLapHD", hoaDon.NgayLapHD);
                    cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                    cmd.Parameters.AddWithValue("@MaNV", hoaDon.MaNV); // Kiểu string
                    cmd.Parameters.AddWithValue("@MaKH", hoaDon.MaKH); // Kiểu string
                    cmd.Parameters.AddWithValue("@MaKM", hoaDon.MaKM ?? (object)DBNull.Value); // Kiểm tra null

                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Trả về true nếu cập nhật thành công
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật hóa đơn: " + ex.Message);
                return false;

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
