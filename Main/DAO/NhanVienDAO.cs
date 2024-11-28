using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;

namespace Main.DAO
{
    public class NhanVienDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();

        // 1. Lấy danh sách nhân viên
        public List<NhanVien> GetNhanVien()
        {
            try
            {
                return dbQLBanGiayDataContext.NhanViens.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // 2. Thêm nhân viên mới
        public bool AddNhanVien(NhanVien nv)
        {
            try
            {
                dbQLBanGiayDataContext.NhanViens.InsertOnSubmit(nv);
                dbQLBanGiayDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 3. Cập nhật thông tin nhân viên
        public bool UpdateNhanVien(NhanVien nv)
        {
            try
            {
                var existingNV = dbQLBanGiayDataContext.NhanViens.FirstOrDefault(x => x.MaNV == nv.MaNV);
                if (existingNV != null)
                {
                    existingNV.TenNV = nv.TenNV;
                    existingNV.NgaySinh = nv.NgaySinh;
                    existingNV.GioiTinh = nv.GioiTinh;
                    existingNV.NoiSinh = nv.NoiSinh;
                    existingNV.SoDienThoai = nv.SoDienThoai;
                    existingNV.CCCD = nv.CCCD;
                    existingNV.ChucVu = nv.ChucVu;

                    dbQLBanGiayDataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 4. Xóa nhân viên
        public string DeleteNhanVien(List<int?> maNVList)
        {
            try
            {
                var nvWithOrders = dbQLBanGiayDataContext.HoaDons
                    .Where(x => maNVList.Contains(x.MaNV))
                    .Select(x => x.MaNV)
                    .ToList();

                if (nvWithOrders.Any())
                {
                    return "Không thể xóa nhân viên đã bán hàng (có hóa đơn)!";
                }

                var nvToDelete = dbQLBanGiayDataContext.NhanViens
                    .Where(x => maNVList.Contains(x.MaNV))
                    .ToList();

                if (nvToDelete.Any())
                {
                    dbQLBanGiayDataContext.NhanViens.DeleteAllOnSubmit(nvToDelete);
                    dbQLBanGiayDataContext.SubmitChanges();
                    return "Xóa nhân viên thành công!";
                }
                return "Không tìm thấy nhân viên để xóa.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Đã xảy ra lỗi trong quá trình xóa nhân viên.";
            }
        }


        // 5. Tìm kiếm nhân viên theo tên
        public List<NhanVien> SearchNhanVien(string keyword)
        {
            try
            {
                return dbQLBanGiayDataContext.NhanViens
                    .Where(nv => nv.TenNV.Contains(keyword))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
