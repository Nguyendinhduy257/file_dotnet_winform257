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
    public partial class Form_Booking : Form
    {
        List<DichVu> danhSachDichVu = new List<DichVu>();

        void LoadData()
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = danhSachDichVu;
        }

        public Form_Booking()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow != null)
            {
                // Xác nhận trước khi xóa
                DialogResult dialogResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa dịch vụ này không?",
                    "Xác nhận Xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    int index = dgvDichVu.CurrentRow.Index;
                    danhSachDichVu.RemoveAt(index);
                    LoadData();
                    // Xóa nội dung trên TextBox sau khi xóa thành công
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- CHỨC NĂNG THÊM (Đã thêm TryParse và kiểm tra rỗng) ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường không được để trống
            if (string.IsNullOrWhiteSpace(textTen.Text) ||
                string.IsNullOrWhiteSpace(textThoiGian.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên, Giá và Thời gian.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        // --- CHỨC NĂNG CLICK VÀO DGV ---
        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < danhSachDichVu.Count) // Đảm bảo không click vào dòng header hoặc dòng trống
            {
                var dv = danhSachDichVu[e.RowIndex];
                textTen.Text = dv.Ten;
                textMoTa.Text = dv.MoTa;
                textThoiGian.Text = dv.ThoiGian;
            }
        }

        // --- CHỨC NĂNG SỬA (Đã thêm TryParse và kiểm tra rỗng) ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow != null)
            {
                // Kiểm tra các trường không được để trống
                if (string.IsNullOrWhiteSpace(textTen.Text) ||
                    string.IsNullOrWhiteSpace(textThoiGian.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Tên, Giá và Thời gian.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }

        private void Form_Booking_Load(object sender, EventArgs e)
        {
            // Thêm dữ liệu mẫu để kiểm tra ngay khi load Form
            danhSachDichVu.Add(new DichVu { Ten = "Cắt Tóc Nam", MoTa = "Dịch vụ cắt tóc cơ bản", ThoiGian = "30 phút" });
            danhSachDichVu.Add(new DichVu { Ten = "Gội Đầu", MoTa = "Chỉ gội đầu thư giãn", ThoiGian = "20 phút" });

            LoadData();
        }

        // Hàm hỗ trợ để xóa nội dung các TextBox
        private void ClearInputFields()
        {
            textTen.Text = "";
            textMoTa.Text = "";
            textThoiGian.Text = "";
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        string username;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ 3 control của bạn
            DateTime thoiGianHen;
            try
            {
                // Nó sẽ đọc chuỗi (string) từ TextBox và chuyển thành DateTime
                thoiGianHen = DateTime.Parse(textThoiGian.Text);
            }
            catch (Exception ex)
            {
                // Lỗi này xảy ra nếu người dùng gõ "abc" thay vì ngày tháng
                MessageBox.Show("Thời gian hẹn không hợp lệ. Vui lòng nhập đúng định dạng (ví dụ: dd/MM/yyyy HH:mm).");
                return;
            }

            // Lấy nội dung
            string noiDung = $"Tên: {textTen.Text} - Mô tả: {textMoTa.Text}";

            // 2. Lấy username của người đang đăng nhập
            string username = UserSession.Username; // Lấy từ Session

            // 3. Lưu vào CSDL (Duy là đã tạo hàm riêng trên DataBaseHelper.cs)
            bool success = DatabaseHelper.TaoLichHenMoi(username, thoiGianHen, noiDung);

            if (success)
            {
                MessageBox.Show("Đặt lịch thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Đặt lịch thất bại. Vui lòng thử lại.");
            }
        }
    }
}