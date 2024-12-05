using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.DAO
{
    public class DDHNhaCungCapDAO
    {
        dbQLBanGiayDataContext dbQLBanGiayDataContext = new dbQLBanGiayDataContext();
        public List<dynamic> GetDDH()
        {
            try
            {
                var query = from ddh in dbQLBanGiayDataContext.DonDatHangNCCs
                            join ncc in dbQLBanGiayDataContext.NhaCungCaps
                            on ddh.MaNCC equals ncc.MaNCC
                            select new
                            {
                                MaDDH = ddh.MaDDH,
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                MaNCC = ncc.MaNCC,
                              //  TrangThai = ddh.TrangThai
                            };

                return query.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy thông tin chi tiết đơn đặt hàng: {ex.Message}");
                return null;
            }
        }

        public List<dynamic> GetChiTietDonDH(int maDDH)
        {
            try
            {
                var query = from ct in dbQLBanGiayDataContext.ChiTietDonDatHangs
                            join sp in dbQLBanGiayDataContext.SanPhams
                            on ct.MaSP equals sp.MaSP
                            //where ct.MaDDH == maDDH
                            select new
                            {
                                ct.MaDDH,
                                ct.MaSP,
                                ct.SoLuong,
                                ct.DonGia,
                                ct.ThanhTien
                            };

                return query.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy chi tiết đơn đặt hàng: {ex.Message}");
                return null;
            }
        }


        public List<NhaCungCap> GetNhaCungCap()
        {
            try
            {
                var ncc = dbQLBanGiayDataContext.NhaCungCaps.ToList();
                return ncc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SanPham> GetSanPham()
        {
            try
            {
                var sp = dbQLBanGiayDataContext.SanPhams.ToList();
                return sp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public List<int> GetMaDonDatHangList()
        //{
        //    try
        //    {
        //        //return dbQLBanGiayDataContext.DonDatHangNCCs
        //        //       .Select(ddh => ddh.MaDDH)
        //        //       .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi khi lấy danh sách mã đơn đặt hàng: {ex.Message}");
        //        return new List<int>();
        //    }
        //}

    }
}
