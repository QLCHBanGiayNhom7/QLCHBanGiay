using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Main.DAO
{
    public class NhaCungCapDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();

        // 1. Lấy danh sách nhà cung cấp
        public List<NhaCungCap> GetNhaCC()
        {
            try
            {
                return dbQLBanGiayDataContext.NhaCungCaps.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        // 2. Thêm nhà cung cấp mới
        public bool AddNhaCC(NhaCungCap ncc)
        {
            try
            {
                var lastEmployee = dbQLBanGiayDataContext.NhaCungCaps.OrderByDescending(n => n.MaNCC).FirstOrDefault();
                string lastCode = lastEmployee == null ? "NCC" : lastEmployee.MaNCC;
                string numberPart = lastCode.Substring(3);
                int newNumber = int.Parse(numberPart) + 1;
                string newCode = "NCC" + newNumber.ToString("D2");
                ncc.MaNCC = newCode;
                dbQLBanGiayDataContext.NhaCungCaps.InsertOnSubmit(ncc);
                dbQLBanGiayDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 3. Cập nhật nhà cung cấp
        public bool UpdateNhaCC(NhaCungCap ncc)
        {
            try
            {
                var existingNCC = dbQLBanGiayDataContext.NhaCungCaps.FirstOrDefault(x => x.MaNCC == ncc.MaNCC);
                if (existingNCC != null)
                {
                    existingNCC.TenNCC = ncc.TenNCC;
                    existingNCC.DiaChi = ncc.DiaChi;
                    existingNCC.SoDienThoai = ncc.SoDienThoai;

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

        // 4. Xóa nhà cung cấp
        public string DeleteNhaCC(List<string> maNCCList)
        {
            try
            {
                var nccWithOrders = dbQLBanGiayDataContext.DonDatHangNCCs
                   .Where(x => maNCCList.Contains(x.MaNCC))
                    .Select(x => x.MaNCC)
                    .ToList();

                if (nccWithOrders.Any())
                {
                    return "Không thể xóa nhà cung cấp đã có đơn đặt hàng!";
                }
                var nccToDelete = dbQLBanGiayDataContext.NhaCungCaps
                    .Where(x => maNCCList.Contains(x.MaNCC))
                    .ToList();

                if (nccToDelete.Any())
                {
                    dbQLBanGiayDataContext.NhaCungCaps.DeleteAllOnSubmit(nccToDelete);
                    dbQLBanGiayDataContext.SubmitChanges();
                    return "Xóa nhà cung cấp thành công!";
                }
                return "Không tìm thấy nhà cung cấp để xóa.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Đã xảy ra lỗi trong quá trình xóa nhà cung cấp.";
            }
        }
    

    // 5. Tìm kiếm nhà cung cấp
    public List<NhaCungCap> SearchNhaCC(string keyword)
        {
            try
            {
                return dbQLBanGiayDataContext.NhaCungCaps
                    .Where(ncc => ncc.TenNCC.Contains(keyword))
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
