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
    public partial class Form_QuanLyBaoCao : Form
    {
        public Form_QuanLyBaoCao()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form_QuanLyBaoCao_Load(object sender, EventArgs e)
        {
            LoadBaoCao();
        }
        // (Trong file Form_QuanLyBaoCao.cs)

        private void LoadBaoCao(string filter = null)
        {
            // 1. Gọi hàm tìm kiếm 
            DataTable dt = DatabaseHelper.SearchReportsForAdmin(filter);

            // 2. Gán dữ liệu vào DataGridView
            dgvBaoCao.DataSource = dt;

            // 3. Tùy chỉnh hiển thị cột, chỉ thực hiện nếu có dữ liệu
            if (dt != null && dt.Rows.Count > 0)
            {
                // Ẩn cột ID
                if (dgvBaoCao.Columns.Contains("ID"))
                {
                    dgvBaoCao.Columns["ID"].Visible = false;
                }

                //  CHỈNH SỬA TÊN CỘT ĐỂ HIỂN THỊ TÊN TÀI KHOẢN (USERNAME)
                if (dgvBaoCao.Columns.Contains("Username_NguoiGui"))
                {
                    dgvBaoCao.Columns["Username_NguoiGui"].HeaderText = "Tên Tài Khoản"; // Tiêu đề mới
                }

                if (dgvBaoCao.Columns.Contains("LoaiBaoCao"))
                {
                    dgvBaoCao.Columns["LoaiBaoCao"].HeaderText = "Loại Báo Cáo";
                }


                if (dgvBaoCao.Columns.Contains("NoiDung"))
                {
                    dgvBaoCao.Columns["NoiDung"].HeaderText = "Nội Dung";
                    dgvBaoCao.Columns["NoiDung"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvBaoCao.Columns.Contains("NgayGui"))
                {
                    dgvBaoCao.Columns["NgayGui"].HeaderText = "Ngày Gửi";
                }

                if (dgvBaoCao.Columns.Contains("TrangThai"))
                {
                    dgvBaoCao.Columns["TrangThai"].HeaderText = "Trạng Thái";
                }
            }
        }
        private void txtTimKiemLoaiBaoCao_TextChanged(object sender, EventArgs e)
        {
            // 1. Dừng Timer (nếu nó đang chạy)
            timerSearch.Stop();

            // 2. Kích hoạt lại Timer để bắt đầu đếm 3 giây
            // Nếu người dùng gõ tiếp trong 3 giây này, hàm TextChanged sẽ được gọi lại, và Timer lại bị reset.
            timerSearch.Start();
        }
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadBaoCao();
        }
        private void btnXuLy_Click(object sender, EventArgs e)
        {
            if (dgvBaoCao.SelectedRows.Count > 0)
            {
                // Lấy ID của báo cáo đang được chọn
                int reportID = Convert.ToInt32(dgvBaoCao.SelectedRows[0].Cells["ID"].Value);

                // Xác nhận
                DialogResult result = MessageBox.Show("Bạn có muốn đánh dấu báo cáo này là 'Đã xử lý'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Gọi hàm xử lý
                    if (DatabaseHelper.UpdateReportStatus(reportID, "Đã xử lý"))
                    {
                        MessageBox.Show("Cập nhật trạng thái thành công!", "Thành công");
                        LoadBaoCao(); // Tải lại lưới để thấy sự thay đổi
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật trạng thái thất bại.", "Lỗi");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một báo cáo để xử lý.", "Thông báo");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerSearch_Tick(object sender, EventArgs e)
        {
            // 1. Dừng Timer ngay lập tức để tránh nó chạy lại liên tục
            timerSearch.Stop();

            // 2. Lấy từ khóa và gọi hàm tìm kiếm
            string keyword = txtTimKiemLoaiBaoCao.Text.Trim();

            // Gọi hàm tìm kiếm dữ liệu
            LoadBaoCao(keyword);
        }
    }
}
