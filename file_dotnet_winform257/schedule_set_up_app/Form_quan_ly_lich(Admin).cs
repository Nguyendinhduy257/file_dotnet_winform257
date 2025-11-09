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
    public partial class Form_quan_ly_lich_Admin_ : Form
    {
        BindingList<LichHen> danhSachChoDuyet = new BindingList<LichHen>();

        public Form_quan_ly_lich_Admin_()
        {
            InitializeComponent();
        }

        private void Form_QuanLyLichHen_Load(object sender, EventArgs e)
        {
            // Giả lập dữ liệu (lịch khách đã đặt)
            danhSachChoDuyet.Add(new LichHen
            {
                MaLichHen = 1,
                TenKhachHang = "Nguyễn Văn A",
                DichVu = "Cắt tóc nam",
                NgayHen = new DateTime(2025, 11, 10, 9, 0, 0),
                TrangThai = "Chờ duyệt"
            });

            danhSachChoDuyet.Add(new LichHen
            {
                MaLichHen = 2,
                TenKhachHang = "Trần Thị B",
                DichVu = "Massage thư giãn",
                NgayHen = new DateTime(2025, 11, 11, 14, 0, 0),
                TrangThai = "Chờ duyệt"
            });

            dgvLichChoDuyet.DataSource = danhSachChoDuyet;
            dgvLichChoDuyet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            rdoDuyet.Checked = true;
            txtLyDoTuChoi.Enabled = false; // Mặc định ẩn lý do từ chối
        }

        private void rdoTuChoi_CheckedChanged(object sender, EventArgs e)
        {
            // Bật / tắt textbox lý do
            txtLyDoTuChoi.Enabled = rdoTuChoi.Checked;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (dgvLichChoDuyet.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch hẹn trong danh sách!");
                return;
            }

            LichHen lich = dgvLichChoDuyet.CurrentRow.DataBoundItem as LichHen;

            if (rdoDuyet.Checked)
            {
                lich.TrangThai = "Đã duyệt";
                lich.LyDoTuChoi = "";
                lblTrangThai.Text = $" Đã duyệt lịch cho {lich.TenKhachHang}";
            }
            else if (rdoTuChoi.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtLyDoTuChoi.Text))
                {
                    MessageBox.Show("Vui lòng nhập lý do từ chối!");
                    return;
                }
                lich.TrangThai = "Từ chối";
                lich.LyDoTuChoi = txtLyDoTuChoi.Text.Trim();
                lblTrangThai.Text = $" Từ chối lịch của {lich.TenKhachHang}";
            }

            dgvLichChoDuyet.Refresh();
            MessageBox.Show("Cập nhật trạng thái lịch hẹn thành công!");
        }
    }

    // ==== CLASS LỊCH HẸN ====
    public class LichHen
    {
        public int MaLichHen { get; set; }
        public string TenKhachHang { get; set; }
        public string DichVu { get; set; }
        public DateTime NgayHen { get; set; }
        public string TrangThai { get; set; }
        public string LyDoTuChoi { get; set; }
    }
}
