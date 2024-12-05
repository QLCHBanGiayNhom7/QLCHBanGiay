using Main.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.BUS
{
    public class NhaCungCapBUS
    {
        private NhaCungCapDAO nhaCungCapDAO;

        public NhaCungCapBUS()
        {
            nhaCungCapDAO = new NhaCungCapDAO();
        }

        // 1. Lấy danh sách nhà cung cấp
        public List<NhaCungCap> GetNhaCC()
        {
            return nhaCungCapDAO.GetNhaCC();
        }

        // 2. Thêm nhà cung cấp mới
        public bool AddNhaCC(NhaCungCap ncc)
        {
            return nhaCungCapDAO.AddNhaCC(ncc);
        }

        // 3. Cập nhật nhà cung cấp
        public bool UpdateNhaCC(NhaCungCap ncc)
        {
            return nhaCungCapDAO.UpdateNhaCC(ncc);
        }

        // 4. Xóa nhà cung cấp
        public string DeleteNhaCC(List<string> maNCCList)
        {
            return nhaCungCapDAO.DeleteNhaCC(maNCCList);
        }

        // 5. Tìm kiếm nhà cung cấp
        public List<NhaCungCap> SearchNhaCC(string keyword)
        {
            return nhaCungCapDAO.SearchNhaCC(keyword);
        }
    }
}
