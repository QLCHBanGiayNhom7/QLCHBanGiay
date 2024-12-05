using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Main.BUS
{
    internal class DDHNhaCungCapBUS
    {
        DDHNhaCungCapDAO don;
        public DDHNhaCungCapBUS()
        {
            don = new DDHNhaCungCapDAO();
        }
        public List<dynamic> GetChiTietDonDH(int maDDH)
        {
            return don.GetChiTietDonDH(maDDH);
        }
        public List<dynamic> GetDonDH()
        {
            return don.GetDDH();
        }
        public List<SanPham> GetSanPham()
        {
            return don.GetSanPham();
        }
        public List<NhaCungCap> GetNhaCungCap()
        {
            return don.GetNhaCungCap();
        }

        //public List<int> GetMaDonDatHangList()
        //{
        //    return don.GetMaDonDatHangList();
        //}

    }
}
