using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class KhoBUS
    {
        private KhoDAO khoDAO;

        public KhoBUS()
        {
            khoDAO = new KhoDAO();
        }

        // Cập nhật kho sau khi trả hoặc đổi hàng
        public bool CapNhatKho(string maSP, int soLuong, bool isTra)
        {
            return khoDAO.CapNhatKho(maSP, soLuong, isTra);
        }

        // Lấy thông tin kho
        public KhoDTO GetKho(string maSP)
        {
            return khoDAO.GetKho(maSP);
        }
    }
}
