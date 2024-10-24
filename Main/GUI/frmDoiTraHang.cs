using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GUI
{
    public partial class frmDoiTraHang : Form
    {
        public SqlConnection conn;
        public void Connnect()
        {
            conn = new SqlConnection();
            string stringConnect = "Server=CAT-JUNIOR\\SQLEXPRESS;Database=QLVT;integrated security=true";
            conn.ConnectionString = stringConnect;
            conn.Open();
        }
        public frmDoiTraHang()
        {
            InitializeComponent();
        }

        private void frmLoaiVatTu_Load(object sender, EventArgs e)
        {
           
        }
        public void ExportToExcel(DataGridView dataGridView)
        {
        }

        // Hàm tạo một cell với giá trị được cung cấp
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            ExportToExcel(dtgvLoai);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
  
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
                    }

        private void dtgvLoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
        }
    }
}
