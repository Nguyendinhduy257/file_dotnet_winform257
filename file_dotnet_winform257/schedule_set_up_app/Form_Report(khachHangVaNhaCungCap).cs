using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace schedule_set_up_app
{
    public partial class Form_Report_khachHang_ : Form
    {
        public Form_Report_khachHang_()
        {
            InitializeComponent();
            // Thêm lựa chọn vào combobox
            cboLoaiBaoCao.Items.Add("Hỗ trợ");
            cboLoaiBaoCao.Items.Add("Lỗi");
            cboLoaiBaoCao.Items.Add("Khác");
            cboLoaiBaoCao.SelectedIndex = 0;

            dtpDenNgay.Value = DateTime.Today;
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            string loai = cboLoaiBaoCao.SelectedItem.ToString();
            var data = new List<BaoCaoItem>();

            // DỮ LIỆU MẪU (chạy được không cần DB)


            dgvBaoCao.DataSource = null;
            dgvBaoCao.DataSource = data;
        }

            

        public class BaoCaoItem
        {
            public string Ngay { get; set; }
            public decimal GiaTri { get; set; }

            public BaoCaoItem(string ngay, decimal giaTri)
            {
                Ngay = ngay;
                GiaTri = giaTri;
            }
        }
    }
}