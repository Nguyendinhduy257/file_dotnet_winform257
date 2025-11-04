using System.Runtime.CompilerServices;

namespace schedule_set_up_app
{
    public partial class Form1 : Form
    {
        private Color originalColor;      // Biến để lưu màu gốc
        private Color targetColor;        // Màu sắc mà nút đang hướng tới
        private int animationStep = 25;   // Tốc độ chuyển màu (càng lớn càng nhanh)
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
            //originalColor = button_close1.BackColor;
            targetColor = originalColor;
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
            //tạo đường dẫn
            string appPath = Application.StartupPath;
            string lock_close_path = Path.Combine(appPath, "img_logo", "Icon_lock_closed.png");
            string lock_open_path = Path.Combine(appPath, "img_logo", "Icon_lock_open.png");
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
            DialogResult result = MessageBox.Show("Bạn muốn thoát Form Login?", "Xác Nhận Thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Information,MessageBoxDefaultButton.Button2);
            if (result ==DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                //Không làm gì cả, đóng messageBox
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            // 1. Xóa mọi thông báo lỗi cũ (nếu có)
            errorProvider1.Clear();

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
            // 5. NẾU TẤT CẢ ĐỀU HỢP LỆ -> MỞ FORM TRANG CHỦ
            if (isErrorFound == true)
            {
                // Nếu CÓ LỖI -> Bắt đầu đếm 8 giây, sau đó tắt thông báo lỗi sau 1 thời gian chạy
                timer1.Start(); 
            }
            else if(isPasswordValid==true&&isUsernameValid==true&&isErrorFound==false)
            {
                // Nếu KHÔNG có lỗi -> Mở Form Trang chủ
                form_trang_chu trang_chu = new form_trang_chu();
                trang_chu.Show();
                this.Hide();
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //tắt timer1
            timer1.Stop();
            //tắt errorProvider sau 1 thời gian thông báo
            errorProvider1.Clear();
            errorProvider2.Clear();
            //tắt label thông báo lỗi sau 1 thời gian thông báo
            label3.Visible=false;
            label4.Visible=false;
        }
    }
}
