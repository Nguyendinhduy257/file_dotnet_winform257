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
            cboLoaiBaoCao.Items.Add("Doanh thu");
            cboLoaiBaoCao.Items.Add("Lượt đặt lịch");
            cboLoaiBaoCao.SelectedIndex = 0;

            // Set ngày mặc định
            dtpTuNgay.Value = DateTime.Today.AddDays(-7);
            dtpDenNgay.Value = DateTime.Today;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            string loai = cboLoaiBaoCao.SelectedItem.ToString();
            var data = new List<BaoCaoItem>();

            // DỮ LIỆU MẪU (chạy được không cần DB)
            if (loai == "Doanh thu")
            {
                data.Add(new BaoCaoItem("01/11/2025", 500000));
                data.Add(new BaoCaoItem("02/11/2025", 650000));
                data.Add(new BaoCaoItem("03/11/2025", 300000));
            }
            else // Lượt đặt lịch
            {
                data.Add(new BaoCaoItem("01/11/2025", 5));
                data.Add(new BaoCaoItem("02/11/2025", 7));
                data.Add(new BaoCaoItem("03/11/2025", 3));
            }

            dgvBaoCao.DataSource = null;
            dgvBaoCao.DataSource = data;

            // Tính tổng
            decimal tong = 0;
            foreach (var item in data)
                tong += item.GiaTri;

            lblTongSoLieu.Text = $"Tổng: {tong}";
        }
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
