using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace schedule_set_up_app
{
    public partial class Form_Booking : Form
    {
        // BIẾN khai báo
        private DateTime selectedDate;
        private string currentUsername;
        private int selectedLichHenID = -1;
        private bool daCoThayDoi = false; //nếu có thay đổi thì load lại panel cho thứ 2/3/4/5/6/7/CN

        public Form_Booking(DateTime date)
        {
            InitializeComponent();
            this.selectedDate = date.Date; // Chỉ lưu ngày
            this.currentUsername = UserSession.Username;
        }

        private void Form_Booking_Load(object sender, EventArgs e)
        {
            // 1. Tạo chuỗi ngày tháng (với "Thứ" bằng tiếng Việt)
            CultureInfo ci = new CultureInfo("vi-VN");
            string ngayHienThi = selectedDate.ToString("dddd, 'ngày' dd 'tháng' MM 'năm' yyyy", ci);

            // 2. Gán vào Label lập lịch cho ngày đó
            lblNgayLapLich.Text = $"Lập lịch cho {ngayHienThi}";

            // Cài đặt control chọn giờ (từ 0h - 24h)
            dtpThoiGian1.Format = DateTimePickerFormat.Custom;
            dtpThoiGian1.CustomFormat = "HH:mm"; // 24 giờ
            dtpThoiGian1.Value = DateTime.Now.Date; // 00:00 giá trị mặc định khi load là 00:00:00
            dtpThoiGian1.ShowUpDown = true;

            dgvLichHenNgay3.MultiSelect = true;

            this.Text = $"Quản lý lịch hẹn ngày: {selectedDate.ToString("dd/MM/yyyy")}";
            LoadDataGridView();
        }
        // 3. HÀM TẢI DATAGRIDVIEW, lấy
        private void LoadDataGridView()
        {
            DataTable dt = DatabaseHelper.GetLichHenTrongNgay(this.currentUsername, this.selectedDate);
            dgvLichHenNgay3.DataSource = dt;

            if (dgvLichHenNgay3.Columns.Contains("ID"))
            {
                dgvLichHenNgay3.Columns["ID"].Visible = false;
            }
            //thêm các cột tiêu đề với thông tin lấy được từ SQL
            dgvLichHenNgay3.Columns["ThoiGianBatDau"].HeaderText = "Giờ hẹn                             📶↕️";
            dgvLichHenNgay3.Columns["NoiDung"].HeaderText = "Nội dung                            📶↕️";
            dgvLichHenNgay3.Columns["TrangThai"].HeaderText = "Trạng thái                           📶↕️";
        }
        //4. hàm sự kiện click vào datagridview
        private void dgvLichHenNgay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo click vào hàng
            {
                DataGridViewRow row = dgvLichHenNgay3.Rows[e.RowIndex];

                // Lấy dữ liệu từ hàng
                this.selectedLichHenID = Convert.ToInt32(row.Cells["ID"].Value);
                DateTime thoiGianSQL = (DateTime)row.Cells["ThoiGianBatDau"].Value;
                string noiDungSQL = row.Cells["NoiDung"].Value.ToString();

                // Đổ dữ liệu lên control
                dtpThoiGian1.Value = thoiGianSQL;
                txtNoiDung.Text = noiDungSQL;
            }
        }
        //5. hàm sự kiện click thêm,sửa,xóa,trở lại
        //private void btnThem_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnSua_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnXoa_Click(object sender, EventArgs e)
        //{

        //}
        //quay trở lại Form trang chu Khach Hang
        private void btnTroLai_Click(object sender, EventArgs e)
        {
            if (this.daCoThayDoi)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        private void dgvLichHenNgay3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                // Chọn tất cả các hàng
                dgvLichHenNgay3.SelectAll();
            }
        }

        private void dgvLichHenNgay3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            DateTime time = dtpThoiGian1.Value;
            string noiDung = txtNoiDung.Text;
            DateTime thoiGianHen = new DateTime(
                selectedDate.Year, selectedDate.Month, selectedDate.Day,
                time.Hour, time.Minute, 0);

            if (string.IsNullOrWhiteSpace(noiDung))
            {
                MessageBox.Show("Vui lòng nhập nội dung.", "Nội dung bị để là NULL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNoiDung.Focus();
                return;
            }
            //gọi hàm kiểm tra lịch trùng từ DatabaseHelper
            if (DatabaseHelper.KiemTraLichTrung(this.currentUsername, thoiGianHen))
            {
                MessageBox.Show("Lịch hẹn bị trùng! Vui lòng chọn một giờ khác.", "Cảnh báo lịch trùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpThoiGian1.Focus(); // Chỉ vào ô chọn giờ
                return; // Dừng lại
            }
            bool success = DatabaseHelper.TaoLichHenMoi(this.currentUsername, thoiGianHen, noiDung);
            if (success)
            {
                LoadDataGridView();
                this.daCoThayDoi = true;
                txtNoiDung.Text = "";
                txtNoiDung.PlaceholderText = "Nhập nội dung/mô tả lịch hẹn..";
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            // 1. Kiểm tra (chỉ cho phép sửa 1)
            if (dgvLichHenNgay3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một lịch hẹn để sửa.");
                return;
            }
            if (dgvLichHenNgay3.SelectedRows.Count > 1)
            {
                MessageBox.Show("Bạn chỉ có thể sửa một lịch hẹn mỗi lần.");
                return;
            }

            // 2. Lấy ID từ hàng đã chọn (hàng 0)
            // (Đảm bảo hàng được chọn không phải hàng rỗng nếu có)
            if (dgvLichHenNgay3.SelectedRows[0].Cells["ID"].Value == null)
            {
                return;
            }
            this.selectedLichHenID = Convert.ToInt32(dgvLichHenNgay3.SelectedRows[0].Cells["ID"].Value);

            // 3. Logic Sửa (sửa tất cả thông tin Date ngày đó)
            DateTime time = dtpThoiGian1.Value;
            string noiDungMoi = txtNoiDung.Text;
            DateTime thoiGianHenMoi = new DateTime(
                selectedDate.Year,
                selectedDate.Month,
                selectedDate.Day,
                time.Hour,
                time.Minute,
                0);

            //(Kiểm tra rỗng)
            if (string.IsNullOrWhiteSpace(noiDungMoi))
            {
                MessageBox.Show("Nội dung không được để trống.");
                txtNoiDung.Focus();
                return;
            }

            //(Kiểm tra trùng thời gian)
            if (DatabaseHelper.KiemTraLichTrung(this.currentUsername, thoiGianHenMoi, this.selectedLichHenID))
            {
                MessageBox.Show("Lịch hẹn bị trùng! Vui lòng chọn một giờ khác.");
                dtpThoiGian1.Focus();
                return;
            }
            DialogResult confirmResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật lịch hẹn này không?", // Nội dung thông báo
                    "Xác Nhận Sửa Lịch Hẹn", // Tiêu đề cửa sổ
                    MessageBoxButtons.OKCancel, // Hiển thị nút OK và Cancel
                    MessageBoxIcon.Question // Icon hỏi
                );

            // Kiểm tra kết quả
            if (confirmResult == DialogResult.OK)
            {
                // Người dùng chọn OK -> Tiến hành Sửa
                bool success = DatabaseHelper.UpdateLichHen(this.selectedLichHenID, thoiGianHenMoi, noiDungMoi);

                if (success)
                {
                    LoadDataGridView();
                    MessageBox.Show("Sửa thành công và đã cập nhật bảng!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.selectedLichHenID = -1; // Reset ID sau khi sửa
                    this.daCoThayDoi = true;
                }
                else
                {
                    MessageBox.Show("Sửa thất bại! Vui lòng kiểm tra kết nối hoặc lỗi SQL.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Người dùng chọn Cancel -> Không làm gì cả và kết thúc hàm
                MessageBox.Show("Thao tác sửa đã bị hủy.", "Đã Hủy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem có hàng nào được chọn không
            if (dgvLichHenNgay3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lịch hẹn để xóa.");
                return;
            }

            // 2. Xác nhận
            int soLuongChon = dgvLichHenNgay3.SelectedRows.Count;
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa {soLuongChon} lịch hẹn đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                int successCount = 0;

                // 3. Lặp qua các hàng ĐÃ CHỌN (SelectedRows)
                foreach (DataGridViewRow row in dgvLichHenNgay3.SelectedRows)
                {
                    int id = Convert.ToInt32(row.Cells["ID"].Value);
                    if (DatabaseHelper.DeleteLichHen(id))
                    {
                        successCount++;
                    }
                }

                MessageBox.Show($"Đã xóa thành công {successCount} / {soLuongChon} lịch hẹn.");

                LoadDataGridView();
                this.daCoThayDoi = true;
                this.selectedLichHenID = -1; // Reset
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}