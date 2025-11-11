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
    public partial class Form_SuaLichHen : Form
    {
        public int currentLichHenID;
        // BIẾN Để lưu thông tin ban đầu
        private DateTime originalThoiGian;
        private string originalTrangThai;
        public Form_SuaLichHen(int LichHenID)
        {
            InitializeComponent();
            this.currentLichHenID = LichHenID;
        }
        // 2. Form_Load: Tải dữ liệu lên các control

        // 3. Nút Lưu: Chỉ lưu 2 trường đã sửa
        // (Vào [Design], nhấp đúp vào nút "Lưu thay đổi")
        private void btnLuuThayDoi_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu MỚI
            DateTime thoiGianMoi = dtpThoiGianHen.Value;
            string trangThaiMoi = cmbTrangThai.Text;

            // Gọi hàm MỚI (UpdateLichHen_Admin)
            bool success = DatabaseHelper.UpdateLichHen_Admin(currentLichHenID, thoiGianMoi, trangThaiMoi);

            if (success)
            {
                MessageBox.Show("Cập nhật lịch hẹn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form_SuaLichHen_Load_1(object sender, EventArgs e)
        {
            DataTable dt = DatabaseHelper.GetLichHenDetailsByID(currentLichHenID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                // Tải dữ liệu không sửa
                txtUsername.Text = row["Username_KhachHang"].ToString();
                txtNoiDung.Text = row["NoiDung"].ToString();

                // LƯU LẠI GIÁ TRỊ GỐC (MỚI)
                originalThoiGian = (DateTime)row["ThoiGianBatDau"];
                originalTrangThai = row["TrangThai"].ToString();

                // Tải dữ liệu gốc vào control
                RevertChanges();
            }
            else
            {
                MessageBox.Show("Không tìm thấy lịch hẹn để sửa.");
                this.Close();
            }
        }
        // HÀM nếu ấn nút Cancel, tải/hoàn tác lại dữ liệu như ban đầu
        private void RevertChanges()
        {
            dtpThoiGianHen.Value = originalThoiGian;
            cmbTrangThai.Text = originalTrangThai;
        }

        private void btnLuuThayDoi_Click_1(object sender, EventArgs e)
        {
            // Lấy dữ liệu MỚI từ control
            DateTime thoiGianMoi = dtpThoiGianHen.Value;
            string trangThaiMoi = cmbTrangThai.Text;

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu các thay đổi này không?",
                "Xác nhận lưu",
                MessageBoxButtons.OKCancel, // Tạo nút OK và Cancel
                MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                // Người dùng nhấn OK -> Thực hiện Lưu
                bool success = DatabaseHelper.UpdateLichHen_Admin(currentLichHenID, thoiGianMoi, trangThaiMoi);

                if (success)
                {
                    MessageBox.Show("Cập nhật lịch hẹn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Cập nhật lại giá trị gốc (vì đã lưu thành công)
                    originalThoiGian = thoiGianMoi;
                    originalTrangThai = trangThaiMoi;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // NGƯỜI DÙNG ẤN CANCEL
            {
                // Hoàn tác lại các thay đổi về giá trị gốc
                RevertChanges();
                MessageBox.Show("Đã hoàn tác các thay đổi của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
