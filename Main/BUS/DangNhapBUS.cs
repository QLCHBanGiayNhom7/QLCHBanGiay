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
        DangNhapDAO dangNhapDAO;
        public DangNhapBUS()
        {
            dangNhapDAO = new DangNhapDAO();
        }
        public string LayChucVuTheoDangNhap(string tenTaiKhoan, string matKhau)
        {
            return dangNhapDAO.LayChucVuTheoDangNhap(tenTaiKhoan, matKhau);
        }
    }
}
