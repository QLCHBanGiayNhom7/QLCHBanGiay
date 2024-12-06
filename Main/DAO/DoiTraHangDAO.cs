using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    public class DoiTraHangDAO
    {
        private string connectionString = "Server=ACER\\FUANG;Database=db_shopBanGiay;Trusted_Connection=True";

        // Lấy danh sách đổi trả hàng
        public List<DoiTraHangDTO> LayDanhSachDoiTraHang()
        {
            List<DoiTraHangDTO> list = new List<DoiTraHangDTO>();
            string query = "SELECT MaHDDT, MaHD, MaSP, SoLuong, LyDo, IsTra FROM DoiTraHang";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DoiTraHangDTO item = new DoiTraHangDTO
                    {
                        MaHDDT = reader["MaHDDT"].ToString(),
                        MaHD = reader["MaHD"].ToString(),
                        MaSP = reader["MaSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        LyDo = reader["LyDo"].ToString(),
                        IsTra = Convert.ToBoolean(reader["IsTra"]) // Xác định nếu là trả hàng (true) hay đổi hàng (false)
                    };
                    list.Add(item);
                }
            }

            return list;
        }

        // Thêm thông tin đổi trả hàng
        public bool ThemDoiTraHang(DoiTraHangDTO doiTraHang)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO DoiTraHang (MaHD, MaSP, SoLuong, LyDo, IsTra) " +
                                   "VALUES (@MaHD, @MaSP, @SoLuong, @LyDo, @IsTra)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaHD", doiTraHang.MaHD);
                        command.Parameters.AddWithValue("@MaSP", doiTraHang.MaSP);
                        command.Parameters.AddWithValue("@SoLuong", doiTraHang.SoLuong);
                        command.Parameters.AddWithValue("@LyDo", doiTraHang.LyDo);
                        command.Parameters.AddWithValue("@IsTra", doiTraHang.IsTra);

                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm thông tin đổi trả hàng: " + ex.Message);
                return false;
            }
        }

    }
}
