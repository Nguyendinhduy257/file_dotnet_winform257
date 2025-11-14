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
        // 1. Biến lưu username
        private string _username;

        // 2. Sửa hàm khởi tạo để nhận username
        public Form_Report_khachHang_(string username)
        {
            InitializeComponent();
            _username = username; // Lưu lại username

            // Thêm lựa chọn vào combobox (Giữ nguyên)
            cboLoaiBaoCao.Items.Add("Hỗ trợ");
            cboLoaiBaoCao.Items.Add("Lỗi");
            cboLoaiBaoCao.Items.Add("Khác");
            cboLoaiBaoCao.SelectedIndex = 0;

            // dtpDenNgay.Value = DateTime.Today; (Không cần thiết lắm, có thể xóa)
        }

        // 3. Thêm sự kiện Form_Load
        private void Form_Report_khachHang__Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // 4. Tạo hàm tải dữ liệu (Lịch sử báo cáo)
        private void LoadData()
        {
            // Gọi hàm mới từ DatabaseHelper
            DataTable dt = DatabaseHelper.GetMyReports(_username);
            dgvBaoCao.DataSource = dt;
            dgvBaoCao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tùy chỉnh cột Nội dung cho rộng hơn
            if (dgvBaoCao.Columns["NoiDung"] != null)
            {
                dgvBaoCao.Columns["NoiDung"].FillWeight = 200;
            }
        }

        // 5. Sửa lại hàm Click (nút này giờ sẽ là GỬI BÁO CÁO)
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            string loai = cboLoaiBaoCao.SelectedItem.ToString();

            // Lấy nội dung từ TextBox bạn vừa thêm
            // Hãy chắc chắn bạn đã thêm TextBox tên là 'txtNoiDung'
            string noiDung = txtNoiDung.Text;

            if (string.IsNullOrWhiteSpace(noiDung))
            {
                MessageBox.Show("Vui lòng nhập nội dung báo cáo!");
                return;
            }

            // 6. Gọi DatabaseHelper để GỬI
            bool success = DatabaseHelper.SubmitReport(_username, loai, noiDung);

            if (success)
            {
                MessageBox.Show("Gửi báo cáo thành công!");
                txtNoiDung.Clear(); // Xóa nội dung đã nhập
                LoadData(); // 7. Tải lại lưới để thấy báo cáo mới
            }
            else
            {
                MessageBox.Show("Gửi báo cáo thất bại! (Lỗi CSDL)");
            }
        }
    }
}