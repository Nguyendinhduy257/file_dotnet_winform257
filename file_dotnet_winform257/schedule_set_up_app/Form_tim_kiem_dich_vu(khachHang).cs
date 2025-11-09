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
    public partial class Form_tim_kiem_dich_vu : Form
    {
        BindingList<DichVu2> danhSachDichVu = new BindingList<DichVu2>();

        public Form_tim_kiem_dich_vu()
        {
            InitializeComponent();
        }

        private void Form_tim_kiem_dich_vu_Load(object sender, EventArgs e)
        {
            // ======= DỮ LIỆU MẪU =======
            danhSachDichVu.Add(new DichVu2 { TenDichVu = "Cắt tóc nam", LoaiDichVu = "Salon tóc", Gia = 100000, DiaDiem = "Hà Nội", ThoiGianPhucVu = new TimeSpan(0, 30, 0) });
            danhSachDichVu.Add(new DichVu2 { TenDichVu = "Massage toàn thân", LoaiDichVu = "Spa", Gia = 400000, DiaDiem = "TP.HCM", ThoiGianPhucVu = new TimeSpan(1, 0, 0) });
            danhSachDichVu.Add(new DichVu2 { TenDichVu = "Sửa xe máy", LoaiDichVu = "Sửa chữa", Gia = 150000, DiaDiem = "Đà Nẵng", ThoiGianPhucVu = new TimeSpan(0, 45, 0) });
            danhSachDichVu.Add(new DichVu2 { TenDichVu = "Trang điểm cô dâu", LoaiDichVu = "Trang điểm", Gia = 1200000, DiaDiem = "Hà Nội", ThoiGianPhucVu = new TimeSpan(2, 0, 0) });

            dgvDichVu.DataSource = danhSachDichVu;
            dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ======= DỮ LIỆU COMBOBOX =======
            cbLoaiDichVu.Items.AddRange(new string[] { "Tất cả", "Salon tóc", "Spa", "Sửa chữa", "Trang điểm" });
            cbLoaiDichVu.SelectedIndex = 0;

            // ======= DỮ LIỆU DOMAINUPDOWN =======
            for (int h = 0; h < 24; h++) upHour.Items.Add(h.ToString("D2"));
            for (int m = 0; m < 60; m += 5) upMinute.Items.Add(m.ToString("D2"));
            upHour.SelectedIndex = 0;
            upMinute.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim().ToLower();
            string loai = cbLoaiDichVu.SelectedItem.ToString();

            int gioChon = int.Parse(upHour.Text);
            int phutChon = int.Parse(upMinute.Text);
            TimeSpan thoiGianMucTieu = new TimeSpan(gioChon, phutChon, 0);

            // Giả sử: tìm dịch vụ có thời gian phục vụ <= thời gian chọn
            var ketQua = danhSachDichVu.Where(d =>
                (d.TenDichVu.ToLower().Contains(keyword) ||
                 d.DiaDiem.ToLower().Contains(keyword)) &&
                (loai == "Tất cả" || d.LoaiDichVu == loai) &&
                d.ThoiGianPhucVu <= thoiGianMucTieu
            ).ToList();

            if (ketQua.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!");
            }

            dgvDichVu.DataSource = ketQua;
        }
    }

    // ======= CLASS DỊCH VỤ =======
    public class DichVu2
    {
        public string TenDichVu { get; set; }
        public string LoaiDichVu { get; set; }
        public decimal Gia { get; set; }
        public string DiaDiem { get; set; }
        public TimeSpan ThoiGianPhucVu { get; set; }
    }
}
