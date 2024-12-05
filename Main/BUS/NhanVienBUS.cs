using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAO nhanVienDAO;

        public NhanVienBUS()
        {
            nhanVienDAO = new NhanVienDAO(); 
        }

        // 1. Lấy danh sách nhân viên
        public List<NhanVien> GetNhanVien()
        {
            try
            {
                return nhanVienDAO.GetNhanVien();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
                return null;
            }
        }

        // 2. Thêm nhân viên mới
        public bool AddNhanVien(NhanVien nv)
        {
            try
            {
                // Kiểm tra thông tin nhân viên trước khi thêm
                if (string.IsNullOrEmpty(nv.TenNV) || string.IsNullOrEmpty(nv.CCCD))
                {
                    Console.WriteLine("Thông tin nhân viên không đầy đủ.");
                    return false;
                }

                return nhanVienDAO.AddNhanVien(nv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm nhân viên: " + ex.Message);
                return false;
            }
        }

        // 3. Cập nhật thông tin nhân viên
        public bool UpdateNhanVien(NhanVien nv)
        {
            try
            {
                if (nv.MaNV == null)
                {
                    Console.WriteLine("Mã nhân viên không hợp lệ.");
                    return false;
                }

                return nhanVienDAO.UpdateNhanVien(nv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật nhân viên: " + ex.Message);
                return false;
            }
        }

        // 4. Xóa nhân viên
        public string DeleteNhanVien(List<string> maNVList)
        {
            try
            {
                foreach (var maNV in maNVList)
                {
                    var result = nhanVienDAO.DeleteNhanVien(new List<string> { maNV });
                    if (result.Contains("Không thể xóa"))
                    {
                        return result;
                    }
                }
                return "Xóa nhân viên thành công!";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa nhân viên: " + ex.Message);
                return "Đã xảy ra lỗi trong quá trình xóa nhân viên.";
            }
        }

        // 5. Tìm kiếm nhân viên theo tên
        public List<NhanVien> SearchNhanVien(string keyword)
        {
            try
            {
                if (string.IsNullOrEmpty(keyword))
                {
                    Console.WriteLine("Từ khóa tìm kiếm không hợp lệ.");
                    return null;
                }

                return nhanVienDAO.SearchNhanVien(keyword);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm kiếm nhân viên: " + ex.Message);
                return null;
            }
        }

    }
}
