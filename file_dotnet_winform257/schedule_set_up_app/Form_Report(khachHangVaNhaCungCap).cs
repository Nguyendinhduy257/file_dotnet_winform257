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

        // Thêm biến cờ để nhận biết chế độ
        private bool _isGuestMode = false;

        // 2. Sửa hàm khởi tạo để nhận username
        // Constructor 1: Dành cho User đã đăng nhập
        public Form_Report_khachHang_(string username)
        {
            InitializeComponent();
            _username = username; // Lưu lại username
            _isGuestMode = false; // Đây là User đã đăng nhập
            SetupComboBox();
        }
        // Constructor 2: Dành cho Guest/Bị khóa
        // Thêm tham số isGuest để phân biệt, nếu là true thì là Guest
        public Form_Report_khachHang_(string username, bool isGuest)
        {
            InitializeComponent();
            _username = username;
            _isGuestMode = isGuest; // Đây là Guest
            SetupComboBox();
        }
        private void SetupComboBox()
        {
            cboLoaiBaoCao.Items.Add("Hỗ trợ");
            cboLoaiBaoCao.Items.Add("Lỗi");
            cboLoaiBaoCao.Items.Add("Khác");
            cboLoaiBaoCao.Items.Add("Mở khóa tài khoản"); //mục này cho Guest
            cboLoaiBaoCao.SelectedIndex = 0;
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

        // 5. hàm Click nộp báo cáo(nút này giờ sẽ là GỬI BÁO CÁO)
        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            // 1. Lấy thông tin từ form
            if (cboLoaiBaoCao.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại báo cáo!","Phát hiện rỗng ô chọn",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string loai = cboLoaiBaoCao.SelectedItem.ToString();
            string noiDung = txtNoiDung.Text;

            // 2. Validate nội dung (Kiểm tra rỗng NULL nội dung)
            if (string.IsNullOrWhiteSpace(noiDung) || noiDung.Contains("Vui lòng nhập"))
            {
                MessageBox.Show("Vui lòng nhập nội dung báo cáo!","Nội dung không được rỗng!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            // 3. Xử lý tên người gửi
            string senderUsername = _username;

            // Nếu tên rỗng (do chưa nhập ở form login), gán mặc định là Guest
            if (string.IsNullOrEmpty(senderUsername))
            {
                senderUsername = "Guest";
            }

            // 4. Gọi DatabaseHelper để GỬI
            // Nếu là Guest, sử dụng senderUsername: username là "Guest"
            bool success = DatabaseHelper.SubmitReport(senderUsername, loai, noiDung);

            // 5. Xử lý kết quả
            if (success)
            {
                MessageBox.Show("Gửi báo cáo thành công! Admin sẽ sớm phản hồi.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoiDung.Clear();

                // Chỉ tải lại dữ liệu nếu KHÔNG phải là Guest (Guest không có bảng để xem)
                if (!_isGuestMode)
                {
                    LoadData();
                }
                else
                {
                    // Nếu là Guest thì đóng form luôn cho gọn
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Gửi báo cáo thất bại! (Lỗi kết nối CSDL)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // 3. Thêm sự kiện Form_Load
        private void Form_Report_khachHang__Load_1(object sender, EventArgs e)
        {
            //LoadData();
            if (_isGuestMode)
            {
                // --- CHẾ ĐỘ GUEST ---
                // 1. Ẩn bảng lịch sử đi (Bảo mật)
                dgvBaoCao.Visible = false;

                // 2. Đổi tiêu đề form
                this.Text = "Liên hệ Admin (Mở khóa/Hỗ trợ)";

                // 3.Đặt placeholder cho TextBox nội dung
                if (!string.IsNullOrEmpty(_username))
                {
                    txtNoiDung.PlaceholderText = $"Xin mở khóa cho tài khoản: {_username}...";
                }
                else
                {
                    txtNoiDung.PlaceholderText = "Bạn muốn mở khóa cho tài khoản nào?";
                }

                // 4. Tự động chọn loại báo cáo là "Mở khóa"
                cboLoaiBaoCao.SelectedItem = "Mở khóa tài khoản";
            }
            else
            {
                // --- CHẾ ĐỘ USER ---
                // Hiện bảng và tải dữ liệu như bình thường
                dgvBaoCao.Visible = true;
                LoadData();
            }
        }
    }
}