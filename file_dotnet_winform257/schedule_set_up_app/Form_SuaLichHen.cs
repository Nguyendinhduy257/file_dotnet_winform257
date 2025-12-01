using System;
using System.Data;
using System.Windows.Forms;

namespace schedule_set_up_app
{
    public partial class Form_SuaLichHen : Form
    {
        //biến cho chế độ sửa 1 dòng
        private int currentLichHenID=-1;
        private DateTime originalThoiGian;
        private string originalTrangThai;
        // BIẾN CHO CHẾ ĐỘ SỬA NHIỀU DÒNG (BULK EDIT)
        private List<int> bulkIds = null;
        private bool isBulkEdit = false;

        public Form_SuaLichHen(int LichHenID)
        {
            InitializeComponent();
            this.currentLichHenID = LichHenID;
            this.isBulkEdit = false;
        }
        //hàm cho sửa nhiều dòng
        public Form_SuaLichHen(List<int> listIDs)
        {
            InitializeComponent();
            this.bulkIds = listIDs;
            this.isBulkEdit = true;
        }

        // HÀM LOAD Form
        private void Form_SuaLichHen_Load_1(object sender, EventArgs e)
        {
            // CHỈ TẢI DỮ LIỆU
            //DataTable dt = DatabaseHelper.GetLichHenDetailsByID(currentLichHenID);

            //if (dt.Rows.Count > 0)
            //{
            //    DataRow row = dt.Rows[0];
            //    txtUsername.Text = row["Username_KhachHang"].ToString();
            //    txtNoiDung.Text = row["NoiDung"].ToString();
            //    originalThoiGian = (DateTime)row["ThoiGianBatDau"];
            //    originalTrangThai = row["TrangThai"].ToString();
            //    RevertChanges();
            //}
            //else
            //{
            //    MessageBox.Show("Không tìm thấy lịch hẹn để sửa.");
            //    this.Close();
            //}
            if (isBulkEdit)
            {
                SetupBulkEditMode();
            }
            else
            {
                SetupSingleEditMode();
            }
        }
        // LOGIC SỬA NHIỀU (BULK)
        private void SetupBulkEditMode()
        {
            this.Text = $"Đang sửa hàng loạt ({bulkIds.Count} lịch hẹn)";

            // 1. Vô hiệu hóa các trường KHÔNG được sửa chung
            dtpThoiGianHen1.Enabled = false; // Không cho sửa giờ
            txtUsername.Enabled = false;     // Không cho sửa tên
            txtNoiDung.Enabled = false;      // Không cho sửa nội dung

            // 2. Xóa trắng hoặc hiện thông báo giữ chỗ
            txtUsername.Text = "(Nhiều khách hàng)";
            txtNoiDung.Text = "(Nội dung được giữ nguyên)";

            // 3. Cho phép sửa Trạng Thái
            cmbTrangThai.Enabled = true;
            cmbTrangThai.SelectedIndex = 0; // Chọn mặc định cái đầu tiên
        }

        // LOGIC SỬA 1 (SINGLE) - Giống hệt code cũ của bạn
        private void SetupSingleEditMode()
        {
            this.Text = "Chỉnh sửa lịch hẹn chi tiết";

            DataTable dt = DatabaseHelper.GetLichHenDetailsByID(currentLichHenID);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtUsername.Text = row["Username_KhachHang"].ToString();
                txtNoiDung.Text = row["NoiDung"].ToString();

                // Lưu giá trị gốc để hoàn tác
                originalThoiGian = (DateTime)row["ThoiGianBatDau"];
                originalTrangThai = row["TrangThai"].ToString();

                // Đổ dữ liệu lên form
                RevertChanges();
            }
            else
            {
                MessageBox.Show("Không tìm thấy lịch hẹn để sửa.");
                this.Close();
            }
        }
        // HÀM HOÀN TÁC dùng cho chế độ sửa 1 dòng
        private void RevertChanges()
        {
            dtpThoiGianHen1.Value = originalThoiGian;
            cmbTrangThai.Text = originalTrangThai;
        }

        // HÀM LƯU (Nút "Xác nhận Sửa")
        private void btnLuuThayDoi_Click_1(object sender, EventArgs e)
        {
            string trangThaiMoi = cmbTrangThai.Text;

            // --- TRƯỜNG HỢP 1: SỬA HÀNG LOẠT ---
            if (isBulkEdit)
            {
                DialogResult confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn chuyển trạng thái của {bulkIds.Count} lịch hẹn này sang \"{trangThaiMoi}\" không?",
                    "Xác nhận cập nhật hàng loạt",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.OK)
                {
                    int successCount = 0;
                    foreach (int id in bulkIds)
                    {
                        // Gọi hàm cập nhật nhanh (Chỉ sửa Status)
                        if (DatabaseHelper.UpdateStatusOnly(id, trangThaiMoi))
                        {
                            successCount++;
                        }
                    }

                    MessageBox.Show($"Đã cập nhật thành công {successCount}/{bulkIds.Count} lịch hẹn.", "Hoàn tất");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                return; // Kết thúc
            }

            // --- TRƯỜNG HỢP 2: SỬA 1 DÒNG - có thể can thiệp cả thời gian và trạng thái ---
            DateTime thoiGianMoi = dtpThoiGianHen1.Value;

            DialogResult confirmSingle = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu các thay đổi này không?",
                "Xác nhận lưu",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirmSingle == DialogResult.OK)
            {
                bool success = DatabaseHelper.UpdateLichHen_Admin(currentLichHenID, thoiGianMoi, trangThaiMoi);
                if (success)
                {
                    MessageBox.Show("Cập nhật lịch hẹn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cập nhật lại giá trị gốc để tránh lỗi logic nếu form không đóng (dù ở đây có đóng)
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