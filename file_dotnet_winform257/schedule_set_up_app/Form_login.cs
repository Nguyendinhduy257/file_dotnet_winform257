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
            DialogResult result = MessageBox.Show("Bạn muốn thoát Form Login?", "Xác Nhận Thoát?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.Close();

            }
            else
            {
                //Không làm gì cả, đóng messageBox
                guna2Button1.BorderThickness = 1;
            }
        }
        //khi ấn nuts đăng nhập, kiểm tra thông tin đăng nhập
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // 1. Xóa mọi thông báo lỗi cũ
            errorProvider1.Clear();
            errorProvider2.Clear();
            label3.Visible = false;
            label4.Visible = false;

            bool isErrorFound = false;

            // 3. KIỂM TRA VALIDATE trong window form login
            // 3a. Kiểm tra User
            if (string.IsNullOrWhiteSpace(textBox_username.Text))
            {
                errorProvider1.SetError(textBox_username, "Vui lòng nhập tên người dùng!");
                label3.Text = "Vui lòng nhập tên người dùng";
                label3.Visible = true;
                isErrorFound = true;
            }
            else if (textBox_username.Text.Contains(" "))
            {
                errorProvider1.SetError(textBox_username, "Tên người dùng không được chứa dấu cách!");
                label3.Text = "Tên người dùng không được chứa SPACE";
                label3.Visible = true;
                isErrorFound = true;
            }

            // 4. KIỂM TRA PASSWORD 
            if (string.IsNullOrWhiteSpace(textBox_pass.Text))
            {
                errorProvider2.SetError(textBox_pass, "Vui lòng nhập mật khẩu!");
                label4.Text = "Vui lòng nhập mật khẩu";
                label4.Visible = true;
                isErrorFound = true;
            }

            // 5. NẾU CÓ LỖI VALIDATE -> DỪNG
            if (isErrorFound == true)
            {
                timer1.Start();
                return;
            }

            // 6. KIỂM TRA ngoài winfow form login --> kiểm tra CSDL
            string username = textBox_username.Text;
            string password = textBox_pass.Text;

            // GỌI HÀM KIỂM TRA
            string rawResult = DatabaseHelper.CheckLogin(username, password);

            // --- BƯỚC MỚI: TÁCH CHUỖI ĐỂ LẤY THÔNG TIN ---
            // Ví dụ: nhận được "WrongPass|3" -> tách thành parts[0]="WrongPass", parts[1]="3"
            string[] parts = rawResult.Split('|');
            string status = parts[0]; // Lấy trạng thái chính (User, Admin, WrongPass...)

            switch (status)
            {
                //nếu đúng thì vào trang chủ tương ứng với user hoặc admin
                case "User":
                    UserSession.SetUser(username);
                    form_trang_chu form_Khach = new form_trang_chu(username);
                    form_Khach.Show();
                    this.Hide();
                    break;

                case "Admin":
                    UserSession.SetUser(username);
                    Form_trang_chu_admin form_Trang_Chu_Admin = new Form_trang_chu_admin(username);
                    form_Trang_Chu_Admin.Show();
                    this.Hide();
                    break;
                //nếu sai mật khẩu quá 5 lần thì khóa tài khoản
                case "Locked":
                    MessageBox.Show("Tài khoản này đang bị KHÓA do nhập sai quá 5 lần.\nVui lòng liên hệ Admin.",
                                    "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                //nếu vừa sai lần thứ 5 thì thông báo khóa ngay là mới bị khóa tài khoản
                case "LockedNow":
                    MessageBox.Show("Bạn đã nhập sai 5/5 lần.\nTài khoản CHÍNH THỨC BỊ KHÓA ngay bây giờ!",
                                    "Đã khóa tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                //nếu sai mật khẩu thì thông báo số lần đã sai ví dụ: bạn sai 3/5 lần
                case "WrongPass":
                    // --- XỬ LÝ HIỂN THỊ SỐ LẦN SAI ---
                    string soLan = "0";
                    if (parts.Length > 1) soLan = parts[1]; // Lấy con số sau dấu |

                    if (soLan == "Admin")
                    {
                        // Admin thì không hiện số lần
                        MessageBox.Show("Sai mật khẩu Admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // User thường thì hiện số lần: "Bạn đã nhập sai 3/5 lần"
                        MessageBox.Show($"Sai mật khẩu!\nBạn đã nhập sai {soLan}/5 lần.\n(Tài khoản sẽ bị khóa nếu sai 5 lần)",
                                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;

                case "Invalid":
                    MessageBox.Show("Tài khoản không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    MessageBox.Show("Lỗi hệ thống: " + rawResult);
                    break;
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
            label3.Visible = false;
            label4.Visible = false;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.DarkGray;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.LightGray;
        }
    }
}
