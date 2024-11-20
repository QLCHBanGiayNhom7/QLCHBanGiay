using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;
namespace Main.GUI
{
    public partial class frmTrangChu : UIAsideHeaderMainFrame
    {
        private string taiKhoan;
        public string TaiKhoan
        {
            get { return taiKhoan; }
        }
        public frmTrangChu(string taiKhoan)
        {
            InitializeComponent();
            this.taiKhoan = taiKhoan;
            KiemTraQuyen();
        }
       
    }
}
