﻿using System;
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
                                MaNCC = ncc.MaNCC,
                                TenNCC = ncc.TenNCC,
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                TrangThai = ddh.TrangThai
                            };

                return query.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy thông tin chi tiết đơn đặt hàng: {ex.Message}");
                return null;
            }
        }

        public List<dynamic> GetChiTietDonDH(string maDDH)
        {
            try
            {
                var query = from ct in dbQLBanGiayDataContext.ChiTietDonDatHangs
                            join sp in dbQLBanGiayDataContext.SanPhams
                            on ct.MaSP equals sp.MaSP
                            where ct.MaDDH == maDDH
                            select new
                            {
                                ct.MaDDH,
                                ct.MaSP,
                                sp.TenSP,
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

        public List<string> GetMaDonDatHangList()
        {
            try
            {
                return dbQLBanGiayDataContext.DonDatHangNCCs
                       .Select(ddh => ddh.MaDDH)
                       .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh sách mã đơn đặt hàng: {ex.Message}");
                return new List<string>();
            }
        }
        public string layMaDDH()
        {
            var lastKhuyenMai = dbQLBanGiayDataContext.DonDatHangNCCs.OrderByDescending(khuyenmai => khuyenmai.MaDDH).FirstOrDefault();
            string lastCode = lastKhuyenMai == null ? "DDH00" : lastKhuyenMai.MaDDH;
            string numberPart = lastCode.Substring(3);
            int newNumber = int.Parse(numberPart) + 1;
            string newCode = "DDH" + newNumber.ToString("D2");
            return newCode;
        }

        public string AddDonDatHang(string maDDH, string maNCC)
        {
            try
            {

                DonDatHangNCC newOrder = new DonDatHangNCC
                {
                    MaDDH = maDDH,
                    NgayLapDDH = DateTime.Now,
                    MaNCC = maNCC,
                    TongTien = 0,
                    TrangThai = "Đơn nháp"
                };

                dbQLBanGiayDataContext.DonDatHangNCCs.InsertOnSubmit(newOrder);
                dbQLBanGiayDataContext.SubmitChanges();

                return newOrder.MaDDH;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm đơn đặt hàng: {ex.Message}");
                return null;
            }
        }
        public string AddChiTietDonDatHang(string maDDH, string maSP, int soLuong, decimal donGia)
        {
            try
            {
                ChiTietDonDatHang chiTiet = new ChiTietDonDatHang
                {
                    MaDDH = maDDH,
                    MaSP = maSP,
                    SoLuong = soLuong,
                    DonGia = donGia,
                };

                dbQLBanGiayDataContext.ChiTietDonDatHangs.InsertOnSubmit(chiTiet);

                var donDatHang = dbQLBanGiayDataContext.DonDatHangNCCs
                    .FirstOrDefault(ddh => ddh.MaDDH == maDDH);

                if (donDatHang != null)
                {

                    //donDatHang.TongTien += thanhTien;
                }


                dbQLBanGiayDataContext.SubmitChanges();

                return chiTiet.MaDDH;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm chi tiết đơn đặt hàng: {ex.Message}");
                return null;
            }
        }

        public string UpdateChiTietDonDatHang(string maDDH, string maSP, int soLuong, decimal donGia)
        {
            try
            {
                var chiTiet = dbQLBanGiayDataContext.ChiTietDonDatHangs
                    .FirstOrDefault(ct => ct.MaDDH == maDDH && ct.MaSP == maSP);

                if (chiTiet == null)
                {
                    return null; // Không tìm thấy chi tiết đơn đặt hàng
                }

                // Cập nhật các giá trị cần thiết
                chiTiet.SoLuong = soLuong;
                chiTiet.DonGia = donGia;

                // Không thay đổi `ThanhTien` vì nó được tính toán tự động

                dbQLBanGiayDataContext.SubmitChanges();

                return chiTiet.MaDDH;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật chi tiết đơn đặt hàng: {ex.Message}");
                return null;
            }
        }


        public int DeleteChiTietDonDatHang(string maDDH)
        {
            try
            {
                var chiTietToDelete = dbQLBanGiayDataContext.ChiTietDonDatHangs
                    .Where(ct => ct.MaDDH == maDDH);

                dbQLBanGiayDataContext.ChiTietDonDatHangs.DeleteAllOnSubmit(chiTietToDelete);
                dbQLBanGiayDataContext.SubmitChanges();

                return chiTietToDelete.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa chi tiết đơn đặt hàng: {ex.Message}");
                return -1;
            }
        }

        public int DeleteDonDatHang(string maDDH)
        {
            try
            {
                var donDatHangToDelete = dbQLBanGiayDataContext.DonDatHangNCCs
                    .FirstOrDefault(ddh => ddh.MaDDH == maDDH.Trim());

                if (donDatHangToDelete != null)
                {
                    dbQLBanGiayDataContext.DonDatHangNCCs.DeleteOnSubmit(donDatHangToDelete);
                    dbQLBanGiayDataContext.SubmitChanges();
                    return 1;
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa đơn đặt hàng: {ex.Message}");
                return -1;
            }
        }
        public bool CheckChiTietDonDatHang(string maDDH, string maSP)
        {
            try
            {
                var chiTiet = dbQLBanGiayDataContext.ChiTietDonDatHangs
                    .FirstOrDefault(ct => ct.MaDDH == maDDH && ct.MaSP == maSP);

                return chiTiet != null; // Trả về true nếu tồn tại, ngược lại false
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra chi tiết đơn đặt hàng: {ex.Message}");
                return false;
            }
        }

        public List<dynamic> GetDDHByMonth(int month, int year)
        {
            try
            {
                var query = from ddh in dbQLBanGiayDataContext.DonDatHangNCCs
                            join ncc in dbQLBanGiayDataContext.NhaCungCaps
                            on ddh.MaNCC equals ncc.MaNCC
                            where ddh.NgayLapDDH.Value.Month == month && ddh.NgayLapDDH.Value.Year == year
                            select new
                            {
                                MaDDH = ddh.MaDDH,
                                MaNCC = ncc.MaNCC,
                                TenNCC = ncc.TenNCC,
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                TrangThai = ddh.TrangThai
                            };

                return query.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lọc đơn đặt hàng theo tháng: {ex.Message}");
                return null;
            }
        }

        public List<dynamic> SearchDDH(string keyword)
        {
            try
            {
                var query = from ddh in dbQLBanGiayDataContext.DonDatHangNCCs
                            join ncc in dbQLBanGiayDataContext.NhaCungCaps
                            on ddh.MaNCC equals ncc.MaNCC
                            where ddh.MaDDH.ToString().Contains(keyword)
                                  || ncc.TenNCC.Contains(keyword)
                                  || ncc.MaNCC.ToString().Contains(keyword)
                                  || ddh.TrangThai.Contains(keyword)
                            select new
                            {
                                MaDDH = ddh.MaDDH,
                                MaNCC = ncc.MaNCC,
                                TenNCC = ncc.TenNCC,
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                TrangThai = ddh.TrangThai
                            };

                return query.ToList<dynamic>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm đơn đặt hàng: {ex.Message}");
                return null;
            }
        }
        public int UpdateDonDatHangStatus(string maDDH, string newStatus)
        {
            try
            {
                var donDatHang = dbQLBanGiayDataContext.DonDatHangNCCs.FirstOrDefault(ddh => ddh.MaDDH == maDDH);
                if (donDatHang != null)
                {
                    donDatHang.TrangThai = newStatus;
                    dbQLBanGiayDataContext.SubmitChanges();
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật trạng thái đơn đặt hàng: {ex.Message}");
                return -1;
            }
        }


    }
}
