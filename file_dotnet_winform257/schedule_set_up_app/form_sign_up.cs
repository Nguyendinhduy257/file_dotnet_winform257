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
    public partial class form_sign_up : Form
    {
        public form_sign_up()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // 1. Xóa mọi thông báo lỗi cũ (nếu có)
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();

            // 2. Tạo các biến cờ để kiểm tra
            //bool isUsernameValid = true;
            //bool isPasswordValid = true;
            bool isErrorFound = false;

            // 3. KIỂM TRA TÊN NGƯỜI DÙNG (USERNAME)

            // 3a. Kiểm tra xem có trống không
            if (string.IsNullOrWhiteSpace(textBox_username.Text))
            {
                errorProvider1.SetError(textBox_username, "Vui lòng nhập tên người dùng!");
                label3.Text = "Vui lòng nhập tên người dùng";
                label3.Visible = true;
                //isUsernameValid = false;
                isErrorFound = true;
            }
            // 3b. (Chỉ kiểm tra nếu không trống) Kiểm tra có dấu cách không
            else if (textBox_username.Text.Contains(" "))
            {
                errorProvider1.SetError(textBox_username, "Tên người dùng không được chứa dấu cách!");
                label3.Text = "Tên người dùng không được chứa SPACE";
                label3.Visible = true;
                //isUsernameValid = false;
                isErrorFound = true;
            }

            // 4. KIỂM TRA MẬT KHẨU (PASSWORD)

            // Kiểm tra xem có khoảng trống SPACE không
            if (string.IsNullOrWhiteSpace(textBox_pass.Text))
            {
                errorProvider2.SetError(textBox_pass, "Vui lòng nhập mật khẩu!");
                label4.Text = "Vui lòng nhập mật khẩu";
                label4.Visible = true;
                //isPasswordValid = false;
                isErrorFound = true;
            }
            //5. Kiểm tra mật khẩu confirm(Password confirm)
            // 5. Kiểm tra mật khẩu confirm(Password confirm)
            if (string.IsNullOrWhiteSpace(textBox_pass_confirm.Text))
            {
                errorProvider3.SetError(textBox_pass_confirm, "Vui lòng nhập mật khẩu xác thực");
                label2.Text = "Vui lòng nhập mật khẩu xác thực";
                label2.Visible = true;
                //isPasswordValid = false;
                isErrorFound = true;
            }
            // (Kiểm tra 'textBox_pass' có trống không TRƯỚC khi so sánh)
            // ĐÂY LÀ PHẦN SO SÁNH HAI MẬT KHẨU
            else if (string.IsNullOrWhiteSpace(textBox_pass.Text) == false && string.Compare(textBox_pass.Text, textBox_pass_confirm.Text) != 0)
            {
                errorProvider3.SetError(textBox_pass_confirm, "Mật khẩu xác thực không khớp");
                label2.Visible = true;
                label2.Text = "Mật khẩu xác thực phải trùng với mật khẩu phía trên";
                //isPasswordValid = false;
                isErrorFound = true;
            }
            // 6. NẾU TẤT CẢ ĐỀU HỢP LỆ -> MỞ FORM Login
            if (isErrorFound == true)
            {
                // Nếu CÓ LỖI -> Bắt đầu đếm 8 giây, sau đó tắt thông báo lỗi sau 1 thời gian chạy
                timer1.Start();
                return;
            }
            // Lấy dữ liệu từ TextBox
            string username = textBox_username.Text;
            string password = textBox_pass.Text;

            // GỌI HÀM ĐĂNG KÝ (Hàm mới trả về string)
            // Mặc định đăng ký là "User", --> admin là tài khoản duy nhất; chỉ admin mới có quyền trao Role cho User khác là "Admin"
            string registrationStatus = DatabaseHelper.RegisterUser(username, password, "User");

            // Xử lý các "mã trạng thái" trả về
            if (registrationStatus == "SUCCESS")
            {
                // ĐĂNG KÝ THÀNH CÔNG
                MessageBox.Show("Bạn đã đăng ký thành công, Đang quay trở về Form Login", "Xác Nhận Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
            else if (registrationStatus == "EXISTED")
            {
                // ĐÂY LÀ YÊU CẦU CỦA BẠN: TÀI KHOẢN ĐÃ TỒN TẠI
                DialogResult result = MessageBox.Show("Tên đăng nhập này đã tồn tại.\nBạn có muốn quay lại trang Login không?","Tài khoản đã tồn tại",
                    MessageBoxButtons.OKCancel,  
                    MessageBoxIcon.Warning);    

                // Kiểm tra xem người dùng nhấn OK hay Cancel
                if (result == DialogResult.OK)
                {
                    // Nếu nhấn OK -> Quay về Form1 (Login)
                    Form1 form = new Form1();
                    form.Show();
                    this.Hide();
                }
                // Nếu nhấn Cancel -> Không làm gì cả, ở lại form đăng ký
            }
            else
            {
                // LỖI KHÁC
                MessageBox.Show("Đăng ký thất bại do lỗi hệ thống. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_pass.UseSystemPasswordChar == true)
                {
                    //nếu đang mã hóa -> hiện bản rõ
                    textBox_pass.UseSystemPasswordChar = false;

                    //cập nhật icon đã bẻ khóa thành bản rõ
                    pictureBox2.Image = Properties.Resources.Icon_lock_open;

                }
                else
                {
                    textBox_pass.UseSystemPasswordChar = true;
                    //cập nhật icon bị khóa khi mã hóa
                    pictureBox2.Image = Properties.Resources.Icon_lock_closed;
                }
            }
            catch (Exception ex)
            {
                // Báo lỗi nếu không tìm thấy file (ví dụ: gõ sai tên file)
                MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //tắt timer1
            timer1.Stop();
            //tắt errorProvider sau 1 thời gian (8 giây) thông báo
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            //tắt label thông báo lỗi sau 1 thời gian (8 giây) thông báo
            label3.Visible = false;
            label4.Visible = false;
            label2.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_pass_confirm.UseSystemPasswordChar == true)
                {
                    textBox_pass_confirm.UseSystemPasswordChar = false;

                    pictureBox4.Image = Properties.Resources.Icon_lock_open;
                }
                else
                {
                    textBox_pass_confirm.UseSystemPasswordChar = true;
                    //hiển thị icon biến bản mã thành bản rõ
                    pictureBox4.Image = Properties.Resources.icon_lock_question;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải ảnh: " + ex.Message);
            }
        }

        private void btn_close_sign_up_Click(object sender, EventArgs e)
        {
            guna2Button1.BorderThickness = 3;
            DialogResult result = MessageBox.Show("Bạn muốn thoát về Login?", "Xác Nhận Thoát Sign-up?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();

            }
            else
            {
                //Không làm gì cả, đóng messageBox
                guna2Button1.BorderThickness = 1;
            }
        }

        private void form_sign_up_Load(object sender, EventArgs e)
        {
            //đặt mã hóa mật khẩu ngay khi khởi chạy
            textBox_pass.UseSystemPasswordChar = true;
            textBox_pass_confirm.UseSystemPasswordChar = true;
        }
    }

}
