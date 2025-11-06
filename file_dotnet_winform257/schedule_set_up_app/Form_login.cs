using System.Runtime.CompilerServices;

namespace schedule_set_up_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            textBox_pass.UseSystemPasswordChar = true;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            form_sign_up sign_up = new form_sign_up();
            sign_up.Show();

            //ẩn form login
            this.Hide();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.BorderThickness = 3;
            DialogResult result = MessageBox.Show("Bạn muốn thoát Form Login?", "Xác Nhận Thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
            if (result ==DialogResult.Yes)
            {
                this.Close();
                
            }
            else
            {
                //Không làm gì cả, đóng messageBox
                guna2Button1.BorderThickness = 1;
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // 1. Xóa mọi thông báo lỗi cũ (nếu có)
            errorProvider1.Clear();
            errorProvider2.Clear(); // Thêm cho errorProvider2
            label3.Visible = false; // mặc định Ẩn label lỗi
            label4.Visible = false; // mặc định Ẩn label lỗi

            // 2. Tạo các biến cờ để kiểm tra
            bool isUsernameValid = true;
            bool isPasswordValid = true;
            bool isErrorFound = false;

            // 3. KIỂM TRA TÊN NGƯỜI DÙNG (USERNAME)

            // 3a. Kiểm tra xem có trống không
            if (string.IsNullOrWhiteSpace(textBox_username.Text))
            {
                errorProvider1.SetError(textBox_username, "Vui lòng nhập tên người dùng!");
                label3.Text = "Vui lòng nhập tên người dùng";
                label3.Visible = true;
                isUsernameValid = false;
                isErrorFound = true;
            }
            // 3b. (Chỉ kiểm tra nếu không trống) Kiểm tra có dấu cách không
            else if (textBox_username.Text.Contains(" "))
            {
                errorProvider1.SetError(textBox_username, "Tên người dùng không được chứa dấu cách!");
                label3.Text = "Tên người dùng không được chứa SPACE";
                label3.Visible = true;
                isUsernameValid = false;
                isErrorFound = true;
            }

            // 4. KIỂM TRA MẬT KHẨU (PASSWORD)

            // Kiểm tra xem có khoảng trống SPACE không
            if (string.IsNullOrWhiteSpace(textBox_pass.Text))
            {
                errorProvider2.SetError(textBox_pass, "Vui lòng nhập mật khẩu!");
                label4.Text = "Vui lòng nhập mật khẩu";
                label4.Visible = true;
                isPasswordValid = false;
                isErrorFound = true;
            }

            // 5. NẾU CÓ LỖI VALIDATE (Lỗi trống/dấu cách) -> DỪNG LẠI
            if (isErrorFound == true)
            {
                // Nếu CÓ LỖI -> Bắt đầu đếm 8 giây, sau đó tắt thông báo lỗi
                timer1.Start();
                return; // Dừng lại, không chạy code kiểm tra CSDL
            }

            // 6. NẾU KHÔNG CÓ LỖI VALIDATE -> TIẾN HÀNH KIỂM TRA CSDL
            // Lấy dữ liệu từ TextBox
            string username = textBox_username.Text;
            string password = textBox_pass.Text; // Lấy mật khẩu

            // GỌI HÀM KIỂM TRA (từ DatabaseHelper.cs)
            string role = DatabaseHelper.CheckLogin(username, password);

            // Xử lý kết quả trả về
            if (role == "User")
            {
                // ĐĂNG NHẬP thành công có ROLE="User"

                // Mở Form trang chủ của khách hàng
                form_trang_chu form_Khach = new form_trang_chu(username);
                form_Khach.Show();
                this.Hide();
            }
            else if (role == "Admin")
            {
                // ĐĂNG NHẬP ADMIN
                Form_trang_chu_admin form_Trang_Chu_Admin = new Form_trang_chu_admin();
                form_Trang_Chu_Admin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai Tên Đăng Nhập Hoặc Mật Khẩu","Thông báo Sai",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


    //tắt thông báo sau 8s thông báo
        private void timer1_Tick(object sender, EventArgs e)
        {
            //tắt timer1
            timer1.Stop();
            //tắt errorProvider sau 1 thời gian (8 giây) thông báo
            errorProvider1.Clear();
            errorProvider2.Clear();
            //tắt label thông báo lỗi sau 1 thời gian (8 giây) thông báo
            label3.Visible=false;
            label4.Visible=false;
        }
    }
}
