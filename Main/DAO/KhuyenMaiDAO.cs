using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    public class KhuyenMaiDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();
        public List<KhuyenMai> GetKhuyenMai()
        {
            try
            {
                var danhSachKhuyenMai = dbQLBanGiayDataContext.KhuyenMais.ToList();
                return danhSachKhuyenMai;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool AddKhuyenMai(KhuyenMai km)
        {
            try
            {
                dbQLBanGiayDataContext.KhuyenMais.InsertOnSubmit(km);
                dbQLBanGiayDataContext.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public string layMaKMMoi()
        {
            var lastKhuyenMai = dbQLBanGiayDataContext.KhuyenMais.OrderByDescending(khuyenmai => khuyenmai.MaKM).FirstOrDefault();
            string lastCode = lastKhuyenMai == null ? "KM01" : lastKhuyenMai.MaKM;

            string numberPart = lastCode.Substring(2);
            int newNumber = int.Parse(numberPart) + 1;
            string newCode = "KM" + newNumber.ToString("D2");
            return newCode;
        }


        public bool UpdateKhuyenMai(KhuyenMai km)
        {
            try
            {
                var existingKM = dbQLBanGiayDataContext.KhuyenMais.FirstOrDefault(x => x.MaKM == km.MaKM);
                if (existingKM != null)
                {
                    existingKM.TenChuongTrinh = km.TenChuongTrinh;
                    existingKM.GiaTriGiamGia = km.GiaTriGiamGia;
                    existingKM.NgayBatDau = km.NgayBatDau;
                    existingKM.NgayKetThuc = km.NgayKetThuc;
                    existingKM.HoaDonTu = km.HoaDonTu;
                    existingKM.DieuKienPhu = km.DieuKienPhu;
                    existingKM.TrangThai = km.TrangThai;

                    dbQLBanGiayDataContext.SubmitChanges(); 
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteKhuyenMai(List<string> maKMList)
        {
            try
            {
                var kmToDelete = dbQLBanGiayDataContext.KhuyenMais
                    .Where(x => maKMList.Contains(x.MaKM))
                    .ToList();

                if (kmToDelete.Any())
                {
                    dbQLBanGiayDataContext.KhuyenMais.DeleteAllOnSubmit(kmToDelete);
                    dbQLBanGiayDataContext.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<KhuyenMai> SearchKhuyenMai(string keyword)
        {
            return dbQLBanGiayDataContext.KhuyenMais
                .Where(km => km.MaKM.ToString().Contains(keyword) || km.TenChuongTrinh.Contains(keyword) || km.TrangThai.Contains(keyword))
                .ToList();
        }
    }
}
