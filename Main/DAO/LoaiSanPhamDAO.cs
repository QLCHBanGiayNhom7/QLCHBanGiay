using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    public class LoaiSanPhamDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();
        public List<LoaiSanPham> GetLoaiSanPham()
        {
            try
            {
                return dbQLBanGiayDataContext.LoaiSanPhams.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // 2. Thêm loại sản phẩm mới
        public bool AddLoaiSanPham(LoaiSanPham loaiSP)
        {
            try
            {
                var lastLoai = dbQLBanGiayDataContext.LoaiSanPhams.OrderByDescending(l => l.MaLoai).FirstOrDefault();
                string lastCode = lastLoai == null ? "Lsp" : lastLoai.MaLoai;
                string numberPart = lastCode.Substring(3); // Bỏ "LSP"
                int newNumber = int.Parse(numberPart) + 1;
                string newCode = "Lsp" + newNumber.ToString("D2");
                loaiSP.MaLoai = newCode;

                dbQLBanGiayDataContext.LoaiSanPhams.InsertOnSubmit(loaiSP);
                dbQLBanGiayDataContext.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 3. Cập nhật loại sản phẩm
        public bool UpdateLoaiSanPham(LoaiSanPham loaiSP)
        {
            try
            {
                var existingLoai = dbQLBanGiayDataContext.LoaiSanPhams.FirstOrDefault(x => x.MaLoai == loaiSP.MaLoai);
                if (existingLoai != null)
                {
                    existingLoai.TenLoai = loaiSP.TenLoai;
                    existingLoai.GioiTinh = loaiSP.GioiTinh;

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

        // 4. Xóa loại sản phẩm
        public string DeleteLoaiSanPham(List<string> maLoaiList)
        {
            try
            {
                var loaiToDelete = dbQLBanGiayDataContext.LoaiSanPhams
                    .Where(x => maLoaiList.Contains(x.MaLoai))
                    .ToList();

                if (loaiToDelete.Any())
                {
                    dbQLBanGiayDataContext.LoaiSanPhams.DeleteAllOnSubmit(loaiToDelete);
                    dbQLBanGiayDataContext.SubmitChanges();
                    return "Xóa loại sản phẩm thành công!";
                }
                return "Không tìm thấy loại sản phẩm để xóa.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Đã xảy ra lỗi trong quá trình xóa loại sản phẩm.";
            }
        }

        // 5. Tìm kiếm loại sản phẩm
        public List<LoaiSanPham> SearchLoaiSanPham(string keyword)
        {
            try
            {
                return dbQLBanGiayDataContext.LoaiSanPhams
                    .Where(lsp => lsp.TenLoai.Contains(keyword))
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
