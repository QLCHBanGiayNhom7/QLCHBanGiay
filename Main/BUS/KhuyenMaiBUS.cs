using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.BUS
{
    public class KhuyenMaiBUS
    {
        KhuyenMaiDAO khuyenMaiDAO;
        public KhuyenMaiBUS()
        {
            khuyenMaiDAO = new KhuyenMaiDAO();
        }
        public List<KhuyenMai> GetKhuyenMai()
        {
            return khuyenMaiDAO.GetKhuyenMai();
        }
        public bool AddKhuyenMai(KhuyenMai km)
        {
            return khuyenMaiDAO.AddKhuyenMai(km);
        }
        public bool UpdateKhuyenMai(KhuyenMai km)
        {
            return khuyenMaiDAO.UpdateKhuyenMai(km);
        }
        public bool DeleteKhuyenMai(List<int> maKMList)
        {
            return khuyenMaiDAO.DeleteKhuyenMai(maKMList);
        }
        public List<KhuyenMai> SearchKhuyenMai(string keyword)
        {
            return khuyenMaiDAO.SearchKhuyenMai(keyword);
        }
    }
}
