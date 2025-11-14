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
    public partial class Form_My_Booking : Form
    {
        // 1. Biến lưu trữ username của người đang đăng nhập
        private string _username;

        // 2. Sửa hàm khởi tạo (Constructor) để nhận username
        //    Khi bạn mở Form này từ Form_Main, bạn cần truyền username vào
        //    Ví dụ: Form_My_Booking frm = new Form_My_Booking(TenUserDangNhap);
        //           frm.Show();
        public Form_My_Booking(string username)
        {
            InitializeComponent();
            _username = username; // Lưu lại username
        }

        // 3. Tạo hàm riêng để tải/làm mới dữ liệu trên GridView
        private void LoadData()
        {
            // Gọi hàm mới từ DatabaseHelper, truyền vào username
            DataTable dt = DatabaseHelper.GetLichHenCaNhan(_username);
            dgvLichHen.DataSource = dt;

            // Tùy chỉnh hiển thị cột (nếu cần)
            dgvLichHen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvLichHen.Columns["DichVu"] != null)
            {
                dgvLichHen.Columns["DichVu"].FillWeight = 150; // Cho cột Dịch vụ rộng hơn
            }
        }

        private void Form_My_Booking_Load(object sender, EventArgs e)
        {
            // 4. Xóa toàn bộ code giả lập và gọi hàm LoadData
            LoadData();
        }

        private void btnHuyLich_Click(object sender, EventArgs e)
        {
            if (dgvLichHen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần hủy!");
                return;
            }

            // 5. Lấy dữ liệu từ dòng đang chọn (dùng DataRowView vì đang bind từ DataTable)
            DataRowView drv = (DataRowView)dgvLichHen.CurrentRow.DataBoundItem;

            int maLich = Convert.ToInt32(drv["MaLich"]);
            DateTime ngayHen = Convert.ToDateTime(drv["NgayHen"]);
            string dichVu = drv["DichVu"].ToString();
            string trangThai = drv["TrangThai"].ToString();

            // 6. Kiểm tra các điều kiện (logic này giữ nguyên, rất tốt)
            if (trangThai == "Đã hủy" || trangThai == "Hoàn thành")
            {
                MessageBox.Show("Không thể hủy lịch ở trạng thái này.");
                return;
            }

            if (ngayHen < DateTime.Now)
            {
                MessageBox.Show("Không thể hủy lịch đã qua!");
                return;
            }

            if ((ngayHen - DateTime.Now).TotalMinutes < 60)
            {
                MessageBox.Show("Không thể hủy lịch vì còn dưới 1 giờ trước giờ hẹn!");
                return;
            }

            // 7. Xác nhận và gọi DatabaseHelper
            DialogResult xacNhan = MessageBox.Show($"Bạn có chắc muốn hủy lịch '{dichVu}' không?", "Xác nhận", MessageBoxButtons.YesNo);

            if (xacNhan == DialogResult.Yes)
            {
                // 8. Gọi hàm Hủy Lịch từ DatabaseHelper
                bool success = DatabaseHelper.HuyLichHenUser(maLich);

                if (success)
                {
                    lblThongBao.Text = $"❌ Đã hủy lịch '{dichVu}'.";
                    MessageBox.Show("Hủy lịch thành công!");

                    // 9. Tải lại dữ liệu từ CSDL để cập nhật GridView
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Hủy lịch thất bại! (Lỗi CSDL)");
                }
            }
        }
    }
}
