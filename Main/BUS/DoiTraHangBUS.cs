using System;
using System.Collections.Generic;
using Main.DAO;
using Main.DTO;

namespace Main.BUS
{
    internal class DoiTraHangBUS
    {
        private DoiTraHangDAO doiTraHangDAO = new DoiTraHangDAO();

        // Lấy danh sách đổi trả hàng
        public List<DoiTraHangDTO> LayDanhSachDoiTraHang()
        {
            return doiTraHangDAO.LayDanhSachDoiTraHang();
        }

        // Thêm thông tin đổi trả hàng
        public bool ThemDoiTraHang(DoiTraHangDTO doiTraHangDTO)
        {
            // Thay vì truyền từng tham số riêng biệt, bạn truyền một đối tượng DoiTraHangDTO vào
            return doiTraHangDAO.ThemDoiTraHang(doiTraHangDTO);
        }
    }
}
