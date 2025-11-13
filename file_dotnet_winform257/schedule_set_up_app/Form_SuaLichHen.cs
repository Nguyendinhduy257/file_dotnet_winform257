using System;
using System.Data;
using System.Windows.Forms;

namespace schedule_set_up_app
{
    public partial class Form_SuaLichHen : Form
    {
        private int currentLichHenID;
        private DateTime originalThoiGian;
        private string originalTrangThai;

        public Form_SuaLichHen(int LichHenID)
        {
            InitializeComponent();
            this.currentLichHenID = LichHenID;
        }

        // HÀM LOAD 
        private void Form_SuaLichHen_Load_1(object sender, EventArgs e)
        {
            // CHỈ TẢI DỮ LIỆU
            DataTable dt = DatabaseHelper.GetLichHenDetailsByID(currentLichHenID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtUsername.Text = row["Username_KhachHang"].ToString();
                txtNoiDung.Text = row["NoiDung"].ToString();
                originalThoiGian = (DateTime)row["ThoiGianBatDau"];
                originalTrangThai = row["TrangThai"].ToString();
                RevertChanges();
            }
            else
            {
                MessageBox.Show("Không tìm thấy lịch hẹn để sửa.");
                this.Close();
            }
        }

        // HÀM HOÀN TÁC
        private void RevertChanges()
        {
            // SỬA 1: Dùng tên control mới
            dtpThoiGianHen1.Value = originalThoiGian;
            cmbTrangThai.Text = originalTrangThai;
        }

        // HÀM LƯU (Nút "Xác nhận Sửa")
        private void btnLuuThayDoi_Click_1(object sender, EventArgs e)
        {
            // SỬA 2: Dùng tên control mới
            DateTime thoiGianMoi = dtpThoiGianHen1.Value;
            string trangThaiMoi = cmbTrangThai.Text;

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu các thay đổi này không?",
                "Xác nhận lưu",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                bool success = DatabaseHelper.UpdateLichHen_Admin(currentLichHenID, thoiGianMoi, trangThaiMoi);
                if (success)
                {
                    MessageBox.Show("Cập nhật lịch hẹn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                RevertChanges();
                MessageBox.Show("Đã hoàn tác các thay đổi của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}