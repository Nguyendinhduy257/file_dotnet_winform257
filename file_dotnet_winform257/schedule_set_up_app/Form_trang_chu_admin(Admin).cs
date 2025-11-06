using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.SqlClient;

namespace schedule_set_up_app
{
    public partial class Form_trang_chu_admin : Form
    {
        public string tenNguoiDung;
        public Form_trang_chu_admin(string username)
        {
            InitializeComponent();
            this.tenNguoiDung = username.ToUpper();
        }

        private void Form_trang_chu_admin_Load(object sender, EventArgs e)
        {
            // Label 1
            label2.Text = "Hello Master";
            label2.AutoSize = true; // Phải AutoSize

            // Label  (Tên người dùng)
            label_ten_nguoi_dung.Text = this.tenNguoiDung; // Tên đã .ToUpper()
            label_ten_nguoi_dung.Font = new Font(label_ten_nguoi_dung.Font, FontStyle.Bold); // In đậm
            label_ten_nguoi_dung.ForeColor = Color.Red;
            label_ten_nguoi_dung.AutoSize = true;

            // Label 3
            label4.Text = ", Kiểm tra cập nhật (ᓀ‸ᓂ)👉 📊📈?";
            label4.AutoSize = true; // Phải AutoSize
                                    //thử nghiệm highlight thứ mà bạn click chuột
                                    // Lấy ngày hôm nay

            //gọi hàm tạo biểu đồ cột
            LoadChartCurrentWeek();
            //gọi hàm tạo biểu đồ tròn
            LoadChartBuoi();
            //gọi hàm chứa dữ liệu của 3 thống kê tổng quát
            UpdateAllStatistics();
        }
        private void LoadChartCurrentWeek()
        {
            // Xóa mọi thứ
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Tạo lại ChartArea
            ChartArea chartArea1 = new ChartArea("MainArea");
            chart1.ChartAreas.Add(chartArea1);

            // ÉP TRỤC X,Y SANG DẠNG CHỮ (STRING)
            chartArea1.AxisX.Interval = 1; 
            chartArea1.AxisY.Interval = 1;
            chartArea1.AxisY.Maximum = 10;

            // TẠO SERIES
            Series seriesMoi = new Series("Lịch hẹn");
            seriesMoi.ChartType = SeriesChartType.Column;
            seriesMoi.ChartArea = "MainArea";

            // --- ĐÂY LÀ 2 DÒNG SỬA LỖI QUAN TRỌNG ---
            seriesMoi.XValueType = ChartValueType.String;
            seriesMoi.IsXValueIndexed = true;             //     Ép nó phải "index" (Thứ 2, Thứ 3,...)


            // 1. LẤY DỮ LIỆU TỪ CSDL
            Dictionary<DateTime, int> dataTuDB = DatabaseHelper.GetAppointmentCountsForCurrentWeek();

            // 2. TÌM NGÀY THỨ 2 ĐẦU TUẦN
            DateTime homNay = DateTime.Today;
            int daysToSubtract = 0;
            if (homNay.DayOfWeek == DayOfWeek.Sunday) { daysToSubtract = 6; }
            else { daysToSubtract = (int)homNay.DayOfWeek - (int)DayOfWeek.Monday; }

            DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract);

            // 3. VÒNG LẶP 7 NGÀY (THỨ 2 -> CN)
            string[] tenCacNgay = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "CN" };

            for (int i = 0; i < 7; i++)
            {
                DateTime ngayCanKiemTra = ngayDauTuan.AddDays(i);
                string tenNgay = tenCacNgay[i];

                int soLuong = 0;
                if (dataTuDB.ContainsKey(ngayCanKiemTra))
                {
                    soLuong = dataTuDB[ngayCanKiemTra];
                }

                // LUÔN VẼ (kể cả khi soLuong = 0)
                seriesMoi.Points.AddXY(tenNgay, soLuong);
            }

            // 4. Thêm Series vào Chart
            chart1.Series.Add(seriesMoi);
            chart1.Legends[0].Enabled = false;
            chart1.Titles.Add("Lịch hẹn trong tuần");
        }
        private void LoadChartBuoi()
        {
            // 1. LẤY DỮ LIỆU (Từ hàm mới ở Bước 2)
            Dictionary<string, int> dataTuDB = DatabaseHelper.GetAppointmentCountsByTimeOfDay();

            // 2. Lấy số lượng cho từng buổi (đặt là 0 nếu CSDL không trả về)
            int soLuongSang = dataTuDB.ContainsKey("BuoiSang") ? dataTuDB["BuoiSang"] : 0;
            int soLuongChieu = dataTuDB.ContainsKey("BuoiChieu") ? dataTuDB["BuoiChieu"] : 0;
            int soLuongToi = dataTuDB.ContainsKey("BuoiToi") ? dataTuDB["BuoiToi"] : 0;

            // 3. THIẾT LẬP BIỂU ĐỒ TRÒN (CHART 2)
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Titles.Clear();

            ChartArea areaBuoi = new ChartArea("BuoiArea");
            chart2.ChartAreas.Add(areaBuoi);

            Series seriesBuoi = new Series("TyLeBuoi");
            seriesBuoi.ChartArea = "BuoiArea";
            seriesBuoi.ChartType = SeriesChartType.Pie; // Đổi thành Biểu đồ Tròn

            // 4. THÊM 3 MIẾNG BÁNH (Sáng, Chiều, Tối)
            seriesBuoi.Points.AddXY("Giờ buổi sáng", soLuongSang);
            seriesBuoi.Points.AddXY("Giờ buổi chiều", soLuongChieu);
            seriesBuoi.Points.AddXY("Giờ buổi tối", soLuongToi);

            // 5. HIỂN THỊ PHẦN TRĂM (%)
            seriesBuoi.IsValueShownAsLabel = true;     // Hiển thị số
            seriesBuoi.Label = "#PERCENT";             // Chuyển số đó thành %
            seriesBuoi.LegendText = "#VALX";           // Chú thích được gọi là (Legend) sẽ đặt tên là "Giờ buổi sáng/chiều/tối"...

            chart2.Series.Add(seriesBuoi);
            chart2.Legends[0].Enabled = true; // Bật chú thích (Legend)
            chart2.Titles.Add("Tỷ lệ đặt lịch theo buổi");
        }
        // Hàm này sẽ được gọi trong Form_Load và timer1_Tick
        private void UpdateAllStatistics()
        {
            // 1. Gọi hàm đếm tổng lịch hẹn
            int totalLich = DatabaseHelper.GetTotalAppointmentsCurrentWeek();
            // Gắn vào Text của Button 1
            btn_thong_ke_lap_lich.Text = "Tổng Lịch Hẹn\n" + totalLich.ToString();

            // 2. Gọi hàm đếm lịch chờ
            int totalCho = DatabaseHelper.GetPendingAppointmentCount();
            // Gắn vào Text của Button 2
            btn_thong_ke_danh_sach_cho_duyet.Text = "Chờ Duyệt\n" + totalCho.ToString();

            // 3. Gọi hàm đếm user mới
            int totalUserMoi = DatabaseHelper.GetNewAccountsCurrentWeek();
            // Gắn vào Text của Button 3
            btn_thong_ke_tai_khoan_moi.Text = "Tài Khoản Mới\n" + totalUserMoi.ToString();
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            DialogResult ketqua = MessageBox.Show("Xác nhận thoát về Login?", "Xác nhận thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (ketqua == DialogResult.Yes)
            {
                this.Close();
                Form1 form_login = new Form1();
                form_login.Show();
            }
        }

        private void button_tong_quan_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button_tong_quan_MouseLeave(object sender, EventArgs e)
        {

        }

        private void btn_quan_ly_user_Click(object sender, EventArgs e)
        {
            Form_quan_ly_users form_Quan_Ly_Users = new Form_quan_ly_users();
            form_Quan_Ly_Users.TopLevel = false;
            form_Quan_Ly_Users.FormBorderStyle = FormBorderStyle.None;
            form_Quan_Ly_Users.BackColor = Color.Blue;
            form_Quan_Ly_Users.Dock = DockStyle.Fill;
            panel1.Controls.Add(form_Quan_Ly_Users);
        }

        private void to_report_form_main_Click(object sender, EventArgs e)
        {
            // Lấy control đã được click (chính là cái PictureBox màu xanh)
            Control control = (Control)sender;

            // Lấy vị trí góc dưới bên trái của PictureBox
            Point pt = new Point(0, control.Height);

            // Hiển thị menu (contextMenuStrip1) của bạn tại vị trí đó
            contextMenuStrip1.Show(control, pt);
        }
        //Kiểm tra CSDL với mỗi 5 giây, có dữ liệu nào được cập nhật vào trong bảng LichHen không?
        //Nếu có cập nhật, thì cập nhật luôn cả 2 biểu đồ cột và tròn nữa
        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadChartCurrentWeek();
            LoadChartBuoi();
            // LoadDataGridView();  //bảng dự phòng
            UpdateAllStatistics();
        }
    }
}
