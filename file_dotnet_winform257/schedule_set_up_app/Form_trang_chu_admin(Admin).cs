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
        private DataTable dtFullLichSu;
        // Biến này sẽ giữ control "Quản lý User"
        private UC_QuanLyUser ucQuanLyUser;
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
            //gọi hàm load dữ liệu lịch sử đặt lịch
            LoadLichSuDataGridView();
            // Bật khu vực chọn (row header) và ô chọn tất cả trong DâtaGridView
            guna2DataGridView1.RowHeadersVisible = true;
            timer1.Enabled = true;
            timer1.Interval = 15000;
            //resize Panel2 và hàm Panel2_Resize
            splitContainer2.Panel2.Resize += new EventHandler(panel2_resize);
        }
        private void panel2_resize(object sender, EventArgs e)
        {
            // Căn giữa UC_QuanLyUser nếu nó đang hiển thị
            if (ucQuanLyUser != null && ucQuanLyUser.Visible)
            {
                CenterUC(ucQuanLyUser);
            }
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
            Title title1 = chart1.Titles.Add("Lịch hẹn trong tuần");
            title1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
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
            Title title2 = chart2.Titles.Add("Tỷ lệ đặt lịch theo buổi");
            title2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
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
            //Form_quan_ly_users form_Quan_Ly_Users = new Form_quan_ly_users();
            //form_Quan_Ly_Users.TopLevel = false;
            //form_Quan_Ly_Users.FormBorderStyle = FormBorderStyle.None;
            //form_Quan_Ly_Users.BackColor = Color.Blue;
            //form_Quan_Ly_Users.Dock = DockStyle.Fill;
            //panel1.Controls.Add(form_Quan_Ly_Users);
            label1.Text = "              Thông Tin Các Tài Khoản và Role";
            // 1. Ẩn panel Tổng quan đi
            button_tong_quan.BorderThickness = 1;
            btn_quan_ly_lich_hen.BorderThickness = 1;
            btn_quan_ly_user.BorderThickness = 3;
            btn_setting.BorderThickness = 1;
            panel_tong_quat.Visible = false;

            // 2. Kiểm tra xem UC_QuanLyUser đã được tạo chưa
            if (ucQuanLyUser == null)
            {
                // Nếu chưa, tạo nó LẦN ĐẦU TIÊN
                ucQuanLyUser = new UC_QuanLyUser();
                ucQuanLyUser.Dock = DockStyle.Fill;

                // Thêm nó vào Panel2 (nó đang bị "ẩn" đằng sau panelTongQuan)
                splitContainer2.Panel2.Controls.Add(ucQuanLyUser);
            }

            // 3. Hiển thị nó và đưa lên trên cùng
            ucQuanLyUser.Visible = true;
            ucQuanLyUser.BringToFront();
            //Căn giữa
            CenterUC(ucQuanLyUser);
        }
        // HÀM CenterUC() để căn giữa UserControl trong Panel2
        private void CenterUC(UserControl uc)
        {
            if (uc == null) return;

            // Lấy panel cha (chính là Panel2)
            Control parent = splitContainer2.Panel2;

            // Tính toán vị trí X, Y mới
            int newX = (parent.Width - uc.Width) / 2;
            int newY = (parent.Height - uc.Height) / 2;

            // Đảm bảo không bị âm nếu panel quá nhỏ
            if (newX < 0) newX = 0;
            if (newY < 0) newY = 0;

            // Đặt vị trí (Location)
            uc.Location = new Point(newX, newY);
        }

        private void to_report_form_main_Click(object sender, EventArgs e)
        {
            // Lấy control đã được click (chính là cái PictureBox màu xanh)
            Control control = (Control)sender;

            // Lấy vị trí góc dưới bên trái của PictureBox
            Point pt = new Point(0, control.Height);

            // Hiển thị menu (contextMenuStrip1) dưới đó
            contextMenuStrip1.Show(control, pt);
        }
        //Kiểm tra CSDL với mỗi 1 phút, có dữ liệu nào được cập nhật vào trong bảng LichHen không?
        //Nếu có cập nhật, thì cập nhật luôn cả 2 biểu đồ cột và tròn nữa
        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadChartCurrentWeek();
            LoadChartBuoi();
            // LoadDataGridView();  //bảng dự phòng
            UpdateAllStatistics();
            LoadLichSuDataGridView();
        }
        private void LoadLichSuDataGridView()
        {
            ////  Lấy dữ liệu từ CSDL
            //DataTable data = DatabaseHelper.GetLichSuDatLich();

            // Lấy dữ liệu 1 lần
            dtFullLichSu = DatabaseHelper.GetLichSuDatLich();

            ////  Gán vào DataGridView
            //guna2DataGridView1.DataSource = data;

            // Gán vào DataGridView
            guna2DataGridView1.DataSource = dtFullLichSu;

            // Sửa tên cột cho đẹp
            if (dtFullLichSu.Rows.Count > 0)
            {
                guna2DataGridView1.Columns["ID"].HeaderText = "Mã \n(Want to Sort?)";
                guna2DataGridView1.Columns["Username_KhachHang"].HeaderText = "Khách Hàng \n(Want to Sort?)";
                guna2DataGridView1.Columns["ThoiGianBatDau"].HeaderText = "Thời Gian Hẹn \n(Want to sort?)";
                guna2DataGridView1.Columns["NoiDung"].HeaderText = "Nội Dung \n(Want to sort?)";
                guna2DataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái \n(Want to sort?)";
            }

            // Chỉnh độ rộng cột cho đẹp
            guna2DataGridView1.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void guna2DataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có chọn hàng nào không
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng để xóa.");
                return;
            }

            int soHangChon = guna2DataGridView1.SelectedRows.Count;

            // 2. Hộp thoại XÁC NHẬN
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa {soHangChon} lịch hẹn đã chọn?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                int soHangXoaThanhCong = 0;

                // 3. Lặp qua các hàng đã chọn và xóa
                // Phải lặp ngược để tránh lỗi index
                for (int i = guna2DataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = guna2DataGridView1.SelectedRows[i];

                    // Giả sử cột (Name) của ID là "ID"
                    int idCanXoa = Convert.ToInt32(row.Cells["ID"].Value);

                    if (DatabaseHelper.DeleteLichHen(idCanXoa))
                    {
                        soHangXoaThanhCong++;
                    }
                }

                MessageBox.Show($"Đã xóa thành công {soHangXoaThanhCong} / {soHangChon} lịch hẹn.");

                // 4. Tải lại dữ liệu
                LoadLichSuDataGridView();
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            // 1. Chỉ cho phép chọn 1 hàng để sửa ,nếu chọn nhiều hơn 1 hoặc chưa chọn hàng nào cả thì báo lỗi NGAY LẬP TỨC
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hàng để chỉnh sửa.");
                return;
            }
            if (guna2DataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Chỉ có thể chỉnh sửa một hàng mỗi lần.");
                return;
            }

            // 2. Lấy ID của hàng được chọn
            DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
            int idCanSua = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            // Ví dụ:
            Form_SuaLichHen formSua = new Form_SuaLichHen(idCanSua);
            formSua.ShowDialog(); // Hiển thị form

            //MessageBox.Show("Đang mở form chỉnh sửa cho ID = " + idCanSua);

            // 4. Tải lại bảng sau khi formSua đóng lại
            LoadLichSuDataGridView();
        }
        // Thử nghiệm tìm kiếm không phân biệt chữ có dấu
        public static string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                                            "đ",
                                            "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
                                            "í", "ì", "ỉ", "ĩ", "ị",
                                            "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
                                            "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
                                            "ý", "ỳ", "ỷ", "ỹ", "ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                            "d",
                                            "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
                                            "i", "i", "i", "i", "i",
                                            "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                                            "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                                            "y", "y", "y", "y", "y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
            }
            return text;
        }
        //hàm thực hiện tìm kiếm
        private void PerformSearch()
        {
            // Lấy từ khóa, bỏ dấu, chuyển về chữ thường
            string keyword = RemoveAccents(txtTimKiem.Text.Trim());

            // Nếu từ khóa trống, hiển thị lại bảng đầy đủ
            if (string.IsNullOrEmpty(keyword))
            {
                guna2DataGridView1.DataSource = dtFullLichSu;
                return;
            }

            // Tạo bảng mới để chứa kết quả lọc
            DataTable dtFiltered = dtFullLichSu.Clone(); // Copy cấu trúc

            // Lọc dữ liệu bằng C# (vì RowFilter không hỗ trợ bỏ dấu)
            foreach (DataRow row in dtFullLichSu.Rows)
            {
                string username = RemoveAccents(row["Username_KhachHang"].ToString());
                string noidung = RemoveAccents(row["NoiDung"].ToString());
                string trangthai = RemoveAccents(row["TrangThai"].ToString());

                // Kiểm tra xem có chứa từ khóa không
                if (username.Contains(keyword) ||
                    noidung.Contains(keyword) ||
                    trangthai.Contains(keyword))
                {
                    dtFiltered.ImportRow(row); // Nếu khớp, thêm vào bảng lọc
                }
            }

            // Hiển thị kết quả đã lọc
            guna2DataGridView1.DataSource = dtFiltered;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Dừng timer (nếu đang chạy) và tìm kiếm ngay
            //Nếu người dùng nhấn nút tìm kiếm thì sẽ ra lệnh tìm tiếm luôn

            searchTimer.Stop();
            PerformSearch();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            // Mỗi khi gõ, reset và khởi động lại timer
            // Nếu không nhấn "tìm kiếm" sau 3 giây, timer sẽ tự động thông báo thực hiện tìm kiếm
            //Nếu người dùng nhấn nút tìm kiếm thì sẽ ra lệnh tìm tiếm luôn
            searchTimer.Stop();
            searchTimer.Start(); // Timer sẽ tick sau 3 giây(3000ms)
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            // nếu Hết 3 giây, thông báo tự động tìm kiếm
            searchTimer.Stop();
            PerformSearch();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            // 1. Xóa nội dung tìm kiếm (nếu có)
            txtTimKiem.Clear();

            // 2. Tải lại toàn bộ dữ liệu gốc từ CSDL
            LoadLichSuDataGridView();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wa..ha..ha quả là một chức năng tình huống ngu ngốc (ᵕ—ᴗ—)💧💦... - Đình Zuy", "Tôi thật ngu ngốc ¯\\_ (ᵕ—ᴗ—)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void button_tong_quan_Click(object sender, EventArgs e)
        {
            label1.Text = "Thống kê luồng người dùng trong tuần hiện tại";
            button_tong_quan.BorderThickness = 3;
            btn_quan_ly_lich_hen.BorderThickness = 1;
            btn_quan_ly_user.BorderThickness = 1;
            btn_setting.BorderThickness = 1;
            // 1. Ẩn UC_QuanLyUser đi (nếu nó tồn tại)
            if (ucQuanLyUser != null)
            {
                ucQuanLyUser.Visible = false;
                ucQuanLyUser.SendToBack();
            }

            // 2. "Khôi phục" bằng cách hiển thị lại panelTongQuan
            panel_tong_quat.Visible = true;
            panel_tong_quat.BringToFront();
        }

        private void panel_tong_quat_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_quan_ly_lich_hen_Click(object sender, EventArgs e)
        {
            button_tong_quan.BorderThickness = 1;
            btn_quan_ly_lich_hen.BorderThickness = 3;
            btn_quan_ly_user.BorderThickness = 1;
            btn_setting.BorderThickness = 1;
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            button_tong_quan.BorderThickness = 1;
            btn_quan_ly_lich_hen.BorderThickness = 1;
            btn_quan_ly_user.BorderThickness = 1;
            btn_setting.BorderThickness = 3;

            form_profile form_profile_tai_khoan = new form_profile();
            form_profile_tai_khoan.ShowDialog();
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_profile form_profile_tai_khoan = new form_profile();
            form_profile_tai_khoan.ShowDialog();
        }

        private void xemReportUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Report_khachHang_ form_Report_KhachHang_ = new Form_Report_khachHang_();
            form_Report_KhachHang_.ShowDialog();
        }
    }
}
