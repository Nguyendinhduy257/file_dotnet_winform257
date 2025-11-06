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
            Form_quan_ly_users form_Quan_Ly_Users = new Form_quan_ly_users();
            form_Quan_Ly_Users.TopLevel = false;
            form_Quan_Ly_Users.FormBorderStyle = FormBorderStyle.None;
            form_Quan_Ly_Users.BackColor = Color.Blue;
            form_Quan_Ly_Users.Dock = DockStyle.Fill;
            panel1.Controls.Add(form_Quan_Ly_Users);
        }

        private void to_report_form_main_Click(object sender, EventArgs e)
        {
            // Lấy control đã được click (chính là cái PictureBox màu xanh)
            Control control = (Control)sender;

            // Lấy vị trí góc dưới bên trái của PictureBox
            Point pt = new Point(0, control.Height);

            // Hiển thị menu (contextMenuStrip1) của bạn tại vị trí đó
            contextMenuStrip1.Show(control, pt);
        }
    }
}
