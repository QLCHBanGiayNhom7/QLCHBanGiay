using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class KhachHangBUS
    {
        private KhachHangDAO khachHangDAO = new KhachHangDAO();

        public KhachHangDTO GetKhachHangByMaKH(string maKH)
        {
            return khachHangDAO.GetKhachHangByMaKH(maKH);
        }
    }
}
