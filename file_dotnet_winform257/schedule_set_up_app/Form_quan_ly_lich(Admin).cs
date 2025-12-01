using Guna.UI2.WinForms;
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
        private DataTable dtFullLichSu;

        public Form_quan_ly_lich_Admin_()
        {
            InitializeComponent();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
        }

        private void dgvLichChoDuyet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            // lấy đúng tên cột (là tên trong SQL: "TrangThai")
            if (dgv.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                if (e.RowIndex >= 0)
                {
                    object trangThaiValue = e.Value;

                    if (trangThaiValue != null && trangThaiValue != DBNull.Value)
                    {
                        string trangThai = trangThaiValue.ToString().Trim().ToLower();

                        // Logic tô màu
                        if (trangThai == "chưa duyệt")
                        {
                            // Màu Cam nhạt cho chưa duyệt
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 204);
                            dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                        }
                        else if (trangThai.Contains("đã duyệt") || trangThai.Contains("đã đặt"))
                        {
                            // Màu Xanh nhạt cho đã duyệt
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                            dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                        }
                        else
                        {
                            // Reset màu mặc định
                            dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = dgv.DefaultCellStyle.BackColor;
                            dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = dgv.DefaultCellStyle.ForeColor;
                        }
                    }
                }
            }
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            {
                // 1. Kiểm tra chọn
                if (guna2DataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một hàng để chỉnh sửa.");
                    return;
                }

                // 2. Phân loại Sửa 1 hay Sửa Nhiều
                if (guna2DataGridView1.SelectedRows.Count == 1)
                {
                    // --- SỬA 1 HÀNG ---
                    DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
                    if (selectedRow.Cells["ID"].Value != null)
                    {
                        int idCanSua = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                        Form_SuaLichHen formSua = new Form_SuaLichHen(idCanSua); // Constructor cũ
                        formSua.ShowDialog();
                    }
                }
                else
                {
                    // --- SỬA NHIỀU HÀNG ---
                    List<int> listIDs = new List<int>();
                    foreach (DataGridViewRow row in guna2DataGridView1.SelectedRows)
                    {
                        if (row.Cells["ID"].Value != null)
                        {
                            listIDs.Add(Convert.ToInt32(row.Cells["ID"].Value));
                        }
                    }

                    // Gọi Constructor MỚI (truyền List)
                    Form_SuaLichHen formSua = new Form_SuaLichHen(listIDs);
                    formSua.ShowDialog();
                }
                // 3. Tải lại dữ liệu
                LoadLichSuDataGridView();
            }
        }
        //hàm cập nhât dữ liệu lên datagridview
        private void LoadLichSuDataGridView()
        {
            guna2DataGridView1.AutoGenerateColumns = true;
            // Lấy dữ liệu từ CSDL vào biến toàn cục
            dtFullLichSu = DatabaseHelper.GetLichSuDatLich();// Gán vào DataGridView
            guna2DataGridView1.DataSource = dtFullLichSu;

            // 1. Lấy dữ liệu từ CSDL
            // (Hàm này đã có logic trạng thái ORDER BY: "Chưa duyệt" đưa lên đầu -> Mới nhất)
            DataTable dt = DatabaseHelper.GetLichSuDatLich();

            // 2. Gán vào DataGridView
            guna2DataGridView1.DataSource = dt;

            // 3. Tùy chỉnh hiển thị cột (Chỉ làm khi có dữ liệu/cột)
            if (dt != null && guna2DataGridView1.Columns.Count > 0)
            {
                // --- Đặt Tên Cột Hiển Thị (HeaderText) ---

                // Cột ID
                if (guna2DataGridView1.Columns.Contains("ID"))
                {
                    guna2DataGridView1.Columns["ID"].HeaderText = "Mã ID                      📶↕️";
                }

                // Cột Khách Hàng
                if (guna2DataGridView1.Columns.Contains("Username_KhachHang"))
                {
                    guna2DataGridView1.Columns["Username_KhachHang"].HeaderText = "Khách Hàng           📶↕️";
                }

                // Cột Thời Gian
                if (guna2DataGridView1.Columns.Contains("ThoiGianBatDau"))
                {
                    guna2DataGridView1.Columns["ThoiGianBatDau"].HeaderText = "Thời Gian Hẹn        📶↕️";
                    // Định dạng hiển thị: 30/12/2023 14:30
                    guna2DataGridView1.Columns["ThoiGianBatDau"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                // Cột Nội Dung (Cho tự động giãn đầy bảng)
                if (guna2DataGridView1.Columns.Contains("NoiDung"))
                {
                    guna2DataGridView1.Columns["NoiDung"].HeaderText = "Nội Dung               📶↕️";
                    guna2DataGridView1.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                // Cột Trạng Thái
                if (guna2DataGridView1.Columns.Contains("TrangThai"))
                {
                    guna2DataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái              📶↕️";
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        // --- HÀM không phân biệt chữ có dấu hay không dấu ---
        public static string RemoveAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            text = text.ToLower();
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ", "đ", "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ", "í", "ì", "ỉ", "ĩ", "ị", "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ", "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự", "ý", "ỳ", "ỷ", "ỹ", "ỵ" };
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "d", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "i", "i", "i", "i", "i", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "y", "y", "y", "y", "y" };
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
            }
            return text;
        }

        // --- LOGIC TÌM KIẾM ---
        private void PerformSearch()
        {
            // Lấy từ khóa, , ko quan tâm có dấu hay không --> mặc định bỏ dấu
            string keyword = RemoveAccents(txtTimKiem.Text.Trim());

            // Nếu từ khóa trống -> Hiện lại toàn bộ danh sách
            if (string.IsNullOrEmpty(keyword))
            {
                guna2DataGridView1.DataSource = dtFullLichSu;
                return;
            }

            // Nếu DataTable chưa có dữ liệu -> Thoát
            if (dtFullLichSu == null) return;

            // Tạo bản sao để lọc
            // Dùng Clone để giữ lại dữ liệu full ban đầu
            DataTable dtFiltered = dtFullLichSu.Clone();

            foreach (DataRow row in dtFullLichSu.Rows)
            {
                // Lấy dữ liệu các cột cần tìm, bỏ dấu
                string username = RemoveAccents(row["Username_KhachHang"].ToString());
                string noidung = RemoveAccents(row["NoiDung"].ToString());
                string trangthai = RemoveAccents(row["TrangThai"].ToString());

                // Kiểm tra chứa từ khóa
                if (username.Contains(keyword) || noidung.Contains(keyword) || trangthai.Contains(keyword))
                {
                    dtFiltered.ImportRow(row);
                }
            }

            // Hiển thị kết quả
            guna2DataGridView1.DataSource = dtFiltered;
        }

        // SỰ KIỆN NÚT TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        // SỰ KIỆN GÕ CHỮ (Tìm luôn khi gõ)
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            PerformSearch();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra có chọn hàng nào không
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một hàng để xóa.");
                return;
            }

            int soHangChon = guna2DataGridView1.SelectedRows.Count;

            // 2. Hỏi xác nhận
            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa {soHangChon} lịch hẹn đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                int soHangXoaThanhCong = 0;

                // 3. Lặp ngược từ dưới lên để xóa
                for (int i = guna2DataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = guna2DataGridView1.SelectedRows[i];

                    // Lấy ID
                    if (row.Cells["ID"].Value != null)
                    {
                        int idCanXoa = Convert.ToInt32(row.Cells["ID"].Value);

                        // Gọi hàm xóa trong DatabaseHelper
                        if (DatabaseHelper.DeleteLichHen(idCanXoa))
                        {
                            soHangXoaThanhCong++;
                        }
                    }
                }

                // 4. Thông báo và Tải lại
                MessageBox.Show($"Đã xóa thành công {soHangXoaThanhCong} / {soHangChon} lịch hẹn.");
                LoadLichSuDataGridView();
            }
        }
        private void Form_quan_ly_lich_Admin__Load(object sender, EventArgs e)
        {
            //gọi hàm tải dữ liệu lên datagridview
            LoadLichSuDataGridView();
        }
    }
}
