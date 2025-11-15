using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace schedule_set_up_app
{
    public partial class form_trang_chu : Form
    {
        // 1. Tạo một biến (field) để lưu tên người dùng
        private string tenNguoiDung;
        private DateTime currentMonday;
        public form_trang_chu(string username)
        {
            InitializeComponent();
            this.tenNguoiDung = username;
            //Viết Hoa cả tên để: Dễ nhận biết
            this.tenNguoiDung = this.tenNguoiDung.ToUpper();
        }

        // khai báo 2 hàm mặc định màu highlight của trang chủ
        private Color highlightColor = Color.AliceBlue;
        private Color defaultColor = Color.White;

        // Hàm tắt mọi Panel và chỉ bật sáng 1 Panel được chọn
        //sử dụng "?" để tắt Warning không đáng có
        private void UpdateHighlight(Panel? panelToHighlight)
        {
            // 1. TẮT TẤT CẢ HIGHLIGHT
            highligh_monday.BackColor = defaultColor;
            highlight_tuesday.BackColor = defaultColor;
            highlight_wednesday.BackColor = defaultColor;
            highlight_thurday.BackColor = defaultColor;
            highlight_friday.BackColor = defaultColor;
            highlight_saturday.BackColor = defaultColor;
            highlight_sunday.BackColor = defaultColor;

            // 2. BẬT HIGHLIGHT (có màu cho trước là mặc định) CHO PANEL ĐƯỢC CHỌN
            if (panelToHighlight != null)
            {
                panelToHighlight.BackColor = highlightColor;
            }
        }

        private void form_trang_chu_Load(object sender, EventArgs e)
        {
            dateTimePicker1_ValueChanged(null, null);
            // Label 1
            label2.Text = "Xin chào";
            label2.AutoSize = true; // Phải AutoSize

            // Label  (Tên người dùng)
            label_ten_nguoi_dung.Text = this.tenNguoiDung; // Tên đã .ToUpper()
            label_ten_nguoi_dung.Font = new Font(label_ten_nguoi_dung.Font, FontStyle.Bold); // In đậm
            label_ten_nguoi_dung.ForeColor = Color.Red;
            label_ten_nguoi_dung.AutoSize = true;

            // Label 3
            label4.Text = ", cùng lập lịch nào (´｡• ◡ •｡`) ~♡💖";
            label4.AutoSize = true; // Phải AutoSize
            //thử nghiệm highlight thứ mà bạn click chuột
            // Lấy ngày hôm nay
            DayOfWeek today = DateTime.Today.DayOfWeek;

            // Mặc định không highlight gì
            Panel panelHomNay = null;

            // Tìm xem Panel nào tương ứng với hôm nay
            switch (today)
            {
                case DayOfWeek.Monday:
                    panelHomNay = highligh_monday;
                    break;
                case DayOfWeek.Tuesday:
                    panelHomNay = highlight_tuesday;
                    break;
                case DayOfWeek.Wednesday:
                    panelHomNay = highlight_wednesday;
                    break;
                case DayOfWeek.Thursday:
                    panelHomNay = highlight_thurday;
                    break;
                case DayOfWeek.Friday:
                    panelHomNay = highlight_friday;
                    break;
                case DayOfWeek.Saturday:
                    panelHomNay = highlight_saturday;
                    break;
                case DayOfWeek.Sunday:
                    panelHomNay = highlight_sunday;
                    break;
            }

            // Gọi hàm cập nhật highlight cho ngày mà bạn chọn button thứ 2,3,4,5,6,7,CN
            //mỗi lần gọi, reset highlight để đảm bảo chỉ highlight  1 panel của button thứ 2,3,... mà bạn chọn
            UpdateHighlight(panelHomNay);

        }


        private void btn_monday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highligh_monday);
        }

        private void btn_tuesday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_tuesday);
        }

        private void btn_wednesday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_wednesday);
        }

        private void btn_thursday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_thurday);
        }

        private void btn_friday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_friday);
        }

        private void btn_saturday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_saturday);
        }

        private void btn_sunday_Click(object sender, EventArgs e)
        {
            UpdateHighlight(highlight_sunday);
        }

        //Thử chuyển sang tuần sau, chuyển sang tuần trước, hoặc chọn thủ công trên Lịch dateTimePicker
        private void btn_next_week_Click(object sender, EventArgs e)
        {
            //Lấy ngày hiện tại trong lịch,cộng thêm 7 ngày 
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(7);
        }

        private void btn_previous_week_Click(object sender, EventArgs e)
        {
            //lấy ngày hiện tại trong lịch, trừ đi 7 ngày
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-7);
        }

        //thử trở về ngày hiện tại bằng button "btn_today"
        private void Btn_today_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today;
        }


        //thử lấy dữ liệu lấy từ form này để form khác sử dụng được
        // hàm tìm ngày chính xác (Dựa trên dateTimePicker1)
        private DateTime TimNgayTrongTuan(DayOfWeek thuMuonTim)
        {
            // Lấy ngày đang được chọn trên lịch (giả sử tên là dateTimePicker1)
            DateTime ngayDaChon = dateTimePicker1.Value;

            // Tính toán chênh lệch
            int chenhLech = (int)thuMuonTim - (int)ngayDaChon.DayOfWeek;

            // Cộng (hoặc trừ) số ngày chênh lệch để ra đúng ngày
            return ngayDaChon.AddDays(chenhLech);
        }
        //double-click để mở form Booking (Đức sẽ làm)
        private void btn_monday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday; // Lấy Thứ 2
            Form_Booking formBooking = new Form_Booking(selectedDate);

            // Dùng ShowDialog() và kiểm tra kết quả
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan(); // Tải lại 7 panel
            }
        }
        // HÀM TẢI DỮ LIỆU (load lại form trang chủ ngay sau khi khách đóng form booking xong mà có dữ liệu)
        private void LoadLichHenTuan()
        {
            // 1. Xóa sạch 7 panel
            highligh_monday.Controls.Clear();
            highlight_tuesday.Controls.Clear();
            highlight_wednesday.Controls.Clear();
            highlight_thurday.Controls.Clear();
            highlight_friday.Controls.Clear();
            highlight_saturday.Controls.Clear();
            highlight_sunday.Controls.Clear();

            // 2. Lấy username
            string username = this.tenNguoiDung.ToUpper();

            // 3. Lấy dữ liệu của CẢ TUẦN
            DateTime startDate = currentMonday;
            DateTime endDate = currentMonday.AddDays(7);

            // (Bạn phải đảm bảo DatabaseHelper.cs có hàm này)
            DataTable dtTuan = DatabaseHelper.GetLichHenTrongTuan(username, startDate, endDate);

            // 4. "Vẽ" lịch hẹn lên 7 panel
            foreach (DataRow row in dtTuan.Rows)
            {
                DateTime ngayHen = (DateTime)row["ThoiGianBatDau"];
                string noiDung = row["NoiDung"].ToString();
                string trangThai = row["TrangThai"].ToString();

                // Tạo 1 Label mới
                Label lblHen = new Label();
                lblHen.Text = $"{ngayHen.ToString("HH:mm")} - {noiDung} ({trangThai})";

                lblHen.AutoSize = true;

                lblHen.MinimumSize = new Size(212, 100);
                lblHen.MaximumSize = new Size(212, 300);
                lblHen.BorderStyle = BorderStyle.FixedSingle;
                lblHen.BackColor = Color.LightYellow;
                lblHen.Margin = new Padding(3);

                // Quyết định xem Label này thuộc về panel (là panel dóng từ thứ 2/3/4/5/6/7/CN xuống)
                switch (ngayHen.DayOfWeek)
                {
                    case DayOfWeek.Monday: highligh_monday.Controls.Add(lblHen); break;
                    case DayOfWeek.Tuesday: highlight_tuesday.Controls.Add(lblHen); break;
                    case DayOfWeek.Wednesday: highlight_wednesday.Controls.Add(lblHen); break;
                    case DayOfWeek.Thursday: highlight_thurday.Controls.Add(lblHen); break;
                    case DayOfWeek.Friday: highlight_friday.Controls.Add(lblHen); break;
                    case DayOfWeek.Saturday: highlight_saturday.Controls.Add(lblHen); break;
                    case DayOfWeek.Sunday: highlight_sunday.Controls.Add(lblHen); break;
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            btn_close.BorderThickness = 3;
            DialogResult ketqua = MessageBox.Show("Bạn chắc chắn muốn Đăng Xuất", "Xác Nhận Đăng Xuất?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ketqua == DialogResult.Yes)
            {
                Form1 form_login = new Form1();
                this.Close();
                form_login.Show();
            }
            else
            {
                //không có gì xảy ra cả
                btn_close.BorderThickness = 1;
            }
        }

        private void form_trang_chu_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            to_report_form_main.BackColor = Color.LightGreen;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            to_report_form_main.BackColor = Color.DarkSeaGreen;
        }

        private void to_report_form_main_Click(object sender, EventArgs e)
        {
            // Lấy control đã được click (chính là cái PictureBox màu xanh)
            Control control = (Control)sender;

            // Lấy vị trí góc dưới bên trái của PictureBox
            Point pt = new Point(0, control.Height);

            // Hiển thị menu (contextMenuStrip1) tại vị trí đó
            contextMenuStrip1.Show(control, pt);
        }

        //truy cập form_report khi click
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form_Report_khachHang_ Report = new Form_Report_khachHang_(this.tenNguoiDung);
            Report.Show();
        }

        private void thêmSửaXóaProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_profile form_profile_tai_khoan = new form_profile();
            form_profile_tai_khoan.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form_My_Booking form_My_Booking = new Form_My_Booking(this.tenNguoiDung);
            form_My_Booking.Show();
        }
        private void highligh_monday_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // 1. Tính toán ngày Thứ 2 và lưu vào biến toàn cục
            currentMonday = GetMondayOfWeek(dateTimePicker1.Value);

            // 2. Tải lại dữ liệu cho 7 panel bên dưới các nút thứ 2,3,4,5,6,7,CN
            LoadLichHenTuan();
        }
        private DateTime GetMondayOfWeek(DateTime date)
        {
            int diff = (7 + date.DayOfWeek - DayOfWeek.Monday) % 7;
            return date.AddDays(-1 * diff);
        }

        private void btn_tuesday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(1); // Lấy Thứ 3
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();
            }
        }

        private void btn_wednesday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(2); // Lấy Thứ 4
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();
            }
        }

        private void btn_thursday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(3); // Lấy Thứ 5
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();
            }
        }

        private void btn_friday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(4); // Lấy Thứ 6
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();
            }
        }

        private void btn_saturday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(5); // Lấy Thứ 7
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();
            }
        }

        private void btn_sunday_DoubleClick(object sender, EventArgs e)
        {
            DateTime selectedDate = currentMonday.AddDays(6); // Lấy Chủ Nhật
            Form_Booking formBooking = new Form_Booking(selectedDate);
            if (formBooking.ShowDialog() == DialogResult.OK)
            {
                LoadLichHenTuan();  
            }
        }
    }
}
