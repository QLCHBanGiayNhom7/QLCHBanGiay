using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class SanPhamBUS
    {
        private SanPhamDAO sanPhamDAO = new SanPhamDAO();

        public List<SanPhamDTO> GetSanPham()
        {
            return sanPhamDAO.GetSanPham();
        }
        public bool CapNhatSoLuongKho(string maSP, int soLuongTra)
        {
            return sanPhamDAO.CapNhatSoLuongKho(maSP, soLuongTra);
        }
        public SanPhamDTO SearchSanPhamByMaSP(string maSP)
        {
            // Gọi phương thức từ DAO để lấy DataTable
            DataTable dt = sanPhamDAO.SearchSanPhamByKeyword(maSP);

            // Kiểm tra nếu có kết quả trả về
            if (dt.Rows.Count > 0)
            {
                // Chuyển dữ liệu từ DataRow sang DTO (SanPhamDTO)
                DataRow row = dt.Rows[0];
                return new SanPhamDTO
                {
                    MaSP = row["MaSP"].ToString(),
                    TenSP = row["TenSP"].ToString(),
                    Mau = row["Mau"].ToString(),
                    Size = row["Size"].ToString(),
                    ThuongHieu = row["ThuongHieu"].ToString(),
                    GiaBan = Convert.ToDecimal(row["GiaBan"])
                };
            }
            return null; // Trả về null nếu không tìm thấy sản phẩm
        }
    }
}
