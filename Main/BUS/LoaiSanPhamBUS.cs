using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO; 
namespace Main.BUS
{
    public class LoaiSanPhamBUS
    {
        private LoaiSanPhamDAO loaiSanPhamDAO;

        public LoaiSanPhamBUS()
        {
            loaiSanPhamDAO = new LoaiSanPhamDAO();
        }

        // 1. Lấy danh sách loại sản phẩm
        public List<LoaiSanPham> GetLoaiSanPham()
        {
            return loaiSanPhamDAO.GetLoaiSanPham();
        }

        // 2. Thêm loại sản phẩm mới
        public bool AddLoaiSanPham(LoaiSanPham loaiSP)
        {
            return loaiSanPhamDAO.AddLoaiSanPham(loaiSP);
        }

        // 3. Cập nhật loại sản phẩm
        public bool UpdateLoaiSanPham(LoaiSanPham loaiSP)
        {
            return loaiSanPhamDAO.UpdateLoaiSanPham(loaiSP);
        }

        // 4. Xóa loại sản phẩm
        public string DeleteLoaiSanPham(List<string> maLoaiList)
        {
            return loaiSanPhamDAO.DeleteLoaiSanPham(maLoaiList);
        }

        // 5. Tìm kiếm loại sản phẩm
        public List<LoaiSanPham> SearchLoaiSanPham(string keyword)
        {
            return loaiSanPhamDAO.SearchLoaiSanPham(keyword);
        }
    }
}
