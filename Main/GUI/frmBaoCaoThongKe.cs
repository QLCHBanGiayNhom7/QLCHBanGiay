using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main.GUI
{
    public partial class frmBaoCaoThongKe : UIPage
    {
        public frmBaoCaoThongKe()
        {
            InitializeComponent();
        }
        private void dgvBaoCaoThongKe_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex % 2 != 0)
                {
                    dgvBaoCaoThongKe.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
                    dgvBaoCaoThongKe.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgvBaoCaoThongKe.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
