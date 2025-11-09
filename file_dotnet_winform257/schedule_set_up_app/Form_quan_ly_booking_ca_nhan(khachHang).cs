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
        BindingList<LichHenCaNhan> danhSachLichHen = new BindingList<LichHenCaNhan>();

        public Form_My_Booking()
        {
            InitializeComponent();
        }

        private void Form_My_Booking_Load(object sender, EventArgs e)
        {
            // Giả lập dữ liệu — sau này thay bằng dữ liệu từ database theo user đang đăng nhập
            danhSachLichHen.Add(new LichHenCaNhan
            {
                MaLich = 1,
                DichVu = "Cắt tóc nam",
                NgayHen = DateTime.Now.AddHours(2), // sắp tới
                TrangThai = "Đã đặt"
            });

            danhSachLichHen.Add(new LichHenCaNhan
            {
                MaLich = 2,
                DichVu = "Massage toàn thân",
                NgayHen = DateTime.Now.AddDays(-1), // đã qua
                TrangThai = "Hoàn thành"
            });

            danhSachLichHen.Add(new LichHenCaNhan
            {
                MaLich = 3,
                DichVu = "Trang điểm cô dâu",
                NgayHen = DateTime.Now.AddMinutes(30), // gần giờ hẹn
                TrangThai = "Đã đặt"
            });

            dgvLichHen.DataSource = danhSachLichHen;
            dgvLichHen.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnHuyLich_Click(object sender, EventArgs e)
        {
            if (dgvLichHen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch hẹn cần hủy!");
                return;
            }

            LichHenCaNhan lich = dgvLichHen.CurrentRow.DataBoundItem as LichHenCaNhan;

            // Nếu lịch đã qua thì không cho hủy
            if (lich.NgayHen < DateTime.Now)
            {
                MessageBox.Show("Không thể hủy lịch đã qua!");
                return;
            }

            // Nếu lịch còn dưới 1 giờ thì không cho hủy
            if ((lich.NgayHen - DateTime.Now).TotalMinutes < 60)
            {
                MessageBox.Show("Không thể hủy lịch vì còn dưới 1 giờ trước giờ hẹn!");
                return;
            }

            // Xác nhận
            DialogResult xacNhan = MessageBox.Show($"Bạn có chắc muốn hủy lịch '{lich.DichVu}' không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (xacNhan == DialogResult.Yes)
            {
                lich.TrangThai = "Đã hủy";
                dgvLichHen.Refresh();
                lblThongBao.Text = $"❌ Đã hủy lịch '{lich.DichVu}'.";
                MessageBox.Show("Hủy lịch thành công!");
            }
        }
    }

    public class LichHenCaNhan
    {
        public int MaLich { get; set; }
        public string DichVu { get; set; }
        public DateTime NgayHen { get; set; }
        public string TrangThai { get; set; }
    }
}
//can them tinh nang xem lich o qk, tl va tinh nag tim kiem 