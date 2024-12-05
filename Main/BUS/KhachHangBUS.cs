using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DTO;
using Main.DAO;
namespace Main.BUS
{
   public class KhachHangBUS
    {
        KhachHangDAO khDAO;
        public KhachHangBUS()
        {
            khDAO = new KhachHangDAO(); 
        }

        public List<KhachHangDTO> GetKhachHang()
        {
            return khDAO.GetAllKhachHang();
        }
        public KhachHangDTO GetKhachHangByMaHD(string maHD)
        {
            try
            {
                return khDAO.GetKhachHangByMaHD(maHD);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin khách hàng từ BUS: " + ex.Message);
                return null;
            }
        }
        public bool AddKhachHang(KhachHangDTO khachHangDTO)
        {
            return khDAO.AddKhachHang(khachHangDTO);
        }
        //public bool UpdateKhachHang(KhachHangDTO khachHangDTO)
        //{
        //    return khDAO.UpdateKhachHang(khachHangDTO);
        //}

        public bool DeleteKhachHang(int maKH)
        {
            return khDAO.DeleteKhachHang(maKH);
        }

    }
}
