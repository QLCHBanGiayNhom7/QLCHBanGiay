using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Main.BUS
{
    public class DangNhapBUS
    {
        private DangNhapDAO dangNhapDAO = new DangNhapDAO();

        public bool DangNhap(string tenTaiKhoan, string matKhau)
        {
            return dangNhapDAO.KiemTraDangNhap(tenTaiKhoan, matKhau);
        }
    }
}
