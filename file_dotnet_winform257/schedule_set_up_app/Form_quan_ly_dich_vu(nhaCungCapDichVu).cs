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
    public partial class Form_quan_ly_dich_vu_nhaCungCapDichVu_ : Form
    {
        List<DichVu> danhSachDichVu = new List<DichVu>();

        void LoadData()
        {
            dgvDichVu.DataSource = null;
            dgvDichVu.DataSource = danhSachDichVu;
        }

        public Form_quan_ly_dich_vu_nhaCungCapDichVu_()
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
                string.IsNullOrWhiteSpace(textGia.Text) ||
                string.IsNullOrWhiteSpace(textThoiGian.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên, Giá và Thời gian.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xử lý lỗi cho trường Giá
            if (decimal.TryParse(textGia.Text, out decimal gia))
            {
                DichVu newDV = new DichVu()
                {
                    Ten = textTen.Text,
                    Gia = gia,
                    MoTa = textMoTa.Text,
                    ThoiGian = textThoiGian.Text
                };

                danhSachDichVu.Add(newDV);
                LoadData();
                ClearInputFields(); // Xóa nội dung trên TextBox sau khi thêm
            }
            else
            {
                MessageBox.Show("Giá trị nhập vào cho Giá không hợp lệ. Vui lòng nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CHỨC NĂNG CLICK VÀO DGV ---
        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < danhSachDichVu.Count) // Đảm bảo không click vào dòng header hoặc dòng trống
            {
                var dv = danhSachDichVu[e.RowIndex];
                textTen.Text = dv.Ten;
                textGia.Text = dv.Gia.ToString();
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
                    string.IsNullOrWhiteSpace(textGia.Text) ||
                    string.IsNullOrWhiteSpace(textThoiGian.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Tên, Giá và Thời gian.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xử lý lỗi cho trường Giá
                if (decimal.TryParse(textGia.Text, out decimal gia))
                {
                    int index = dgvDichVu.CurrentRow.Index;
                    danhSachDichVu[index].Ten = textTen.Text;
                    danhSachDichVu[index].Gia = gia;
                    danhSachDichVu[index].MoTa = textMoTa.Text;
                    danhSachDichVu[index].ThoiGian = textThoiGian.Text;
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Giá trị nhập vào cho Giá không hợp lệ. Vui lòng nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form_quan_ly_dich_vu_nhaCungCapDichVu__Load(object sender, EventArgs e)
        {
            // Thêm dữ liệu mẫu để kiểm tra ngay khi load Form
            danhSachDichVu.Add(new DichVu { Ten = "Cắt Tóc Nam", Gia = 50000m, MoTa = "Dịch vụ cắt tóc cơ bản", ThoiGian = "30 phút" });
            danhSachDichVu.Add(new DichVu { Ten = "Gội Đầu", Gia = 30000m, MoTa = "Chỉ gội đầu thư giãn", ThoiGian = "20 phút" });

            LoadData();
        }

        // Hàm hỗ trợ để xóa nội dung các TextBox
        private void ClearInputFields()
        {
            textTen.Text = "";
            textGia.Text = "";
            textMoTa.Text = "";
            textThoiGian.Text = "";
        }

        private void dgvDichVu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}