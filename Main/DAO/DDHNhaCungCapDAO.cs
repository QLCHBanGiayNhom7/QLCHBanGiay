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

        public List<dynamic> GetChiTietDonDH(int maDDH)
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

        public List<int> GetMaDonDatHangList()
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
                return new List<int>();
            }
        }

        public int AddDonDatHang(int maNCC)
        {
            try
            {
                DonDatHangNCC newOrder = new DonDatHangNCC
                {
                    NgayLapDDH = DateTime.Now,
                    MaNCC = maNCC,
                    TongTien = 0,
                    TrangThai = "Đang lên đơn"
                };

                dbQLBanGiayDataContext.DonDatHangNCCs.InsertOnSubmit(newOrder);
                dbQLBanGiayDataContext.SubmitChanges();

                return newOrder.MaDDH;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm đơn đặt hàng: {ex.Message}");
                return -1;
            }
        }
        public int AddChiTietDonDatHang(int maDDH, int maSP, int soLuong, decimal donGia, decimal thanhTien)
        {
            try
            {
                ChiTietDonDatHang chiTiet = new ChiTietDonDatHang
                {
                    MaDDH = maDDH,
                    MaSP = maSP,
                    SoLuong = soLuong,
                    DonGia = donGia,
                    ThanhTien = thanhTien
                };

                dbQLBanGiayDataContext.ChiTietDonDatHangs.InsertOnSubmit(chiTiet);

                var donDatHang = dbQLBanGiayDataContext.DonDatHangNCCs
                    .FirstOrDefault(ddh => ddh.MaDDH == maDDH);

                if (donDatHang != null)
                {
                   
                    donDatHang.TongTien += thanhTien;
                }

                
                dbQLBanGiayDataContext.SubmitChanges();

                return chiTiet.MaDDH;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm chi tiết đơn đặt hàng: {ex.Message}");
                return -1;
            }
        }

        public int UpdateChiTietDonDatHang(int maDDH, int maSP, int soLuong, decimal donGia)
        {
            try
            {
                var chiTiet = dbQLBanGiayDataContext.ChiTietDonDatHangs
                    .FirstOrDefault(ct => ct.MaDDH == maDDH && ct.MaSP == maSP);

                if (chiTiet == null)
                {
                    return -1; // Không tìm thấy chi tiết đơn đặt hàng
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
                return -1;
            }
        }


        public int DeleteChiTietDonDatHang(int maDDH)
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

        public int DeleteDonDatHang(int maDDH)
        {
            try
            {
                var donDatHangToDelete = dbQLBanGiayDataContext.DonDatHangNCCs
                    .FirstOrDefault(ddh => ddh.MaDDH == maDDH);

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
        public bool CheckChiTietDonDatHang(int maDDH, int maSP)
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
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                MaNCC = ncc.MaNCC,
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
                                NgayLapDDH = ddh.NgayLapDDH,
                                TongTien = ddh.TongTien,
                                MaNCC = ncc.MaNCC,
                                TenNCC = ncc.TenNCC,
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
        public int UpdateDonDatHangStatus(int maDDH, string newStatus)
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
