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
    public partial class form_profile : Form
    {

        private string currentUsername;
        private string currentUserRole;
        // BIẾN MỚI: Để lưu thông tin ban đầu
        private string originalPassword;
        private string originalHoten;
        private string originalEmail;
        public form_profile()
        {
            InitializeComponent();
            // Lấy tên người dùng từ Session
            this.currentUsername = UserSession.Username;
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void form_profile_Load(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = true;
            txtPassword.UseSystemPasswordChar = true;
            {
                if (string.IsNullOrEmpty(currentUsername))
                {
                    MessageBox.Show("Lỗi: Không tìm thấy người dùng đăng nhập.");
                    this.Close();
                    return;
                }

                txtUsername.Text = this.currentUsername;
                txtUsername.ReadOnly = true;

                DataTable dt = DatabaseHelper.GetTaiKhoanDetails(this.currentUsername);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    // LƯU LẠI GIÁ TRỊ GỐC (MỚI)
                    originalPassword = row["Password"].ToString();
                    originalHoten = row["Hoten"].ToString();
                    originalEmail = row["Email"].ToString();
                    currentUserRole = row["Role"].ToString();

                    // Tải dữ liệu vào textboxes
                    RevertChanges(); // Gọi hàm RevertChanges
                }
            }
        }
        // HÀM Dùng để tải/hoàn tác lại dữ liệu nếu nhấn Cancel không muốn cập nhật hồ sơ tài khoản nữa
        private void RevertChanges()
        {
            txtPassword.Text = originalPassword;
            txtConfirmPassword.Text = originalPassword; // Tải mật khẩu vào cả ô confirm
            txtHoten.Text = originalHoten;
            txtEmail.Text = originalEmail;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //kiểm tra trước khi Cập nhật hồ sơ
            {
                string newPass = txtPassword.Text;
                string confirmPass = txtConfirmPassword.Text;
                string newHoten = txtHoten.Text;
                string newEmail = txtEmail.Text;

                //kiểm tra nhập dữ liệu
                if (string.IsNullOrWhiteSpace(newPass))
                {
                    MessageBox.Show("Mật khẩu không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (newPass != confirmPass)
                {
                    MessageBox.Show("Mật khẩu và mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show(
                    "Bạn có chắc chắn muốn cập nhật thông tin hồ sơ không?",
                    "Xác nhận cập nhật",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);


                if (confirm == DialogResult.OK)
                {
                    // Người dùng nhấn OK -> Lưu
                    bool success = DatabaseHelper.UpdateTaiKhoan(this.currentUsername, newPass, newHoten, newEmail);

                    if (success)
                    {
                        MessageBox.Show("Cập nhật hồ sơ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại giá trị gốc (vì đã lưu thành công)
                        originalPassword = newPass;
                        originalHoten = newHoten;
                        originalEmail = newEmail;

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else // NGƯỜI DÙNG ẤN CANCEL 
                {
                    // Hoàn tác lại các thay đổi về giá trị gốc
                    RevertChanges();
                    MessageBox.Show("Đã hoàn tác các thay đổi của bạn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //kiểm tra /xac nhận xóa tài khoản 
            //TH đặc biệt: nếu tài khoản trong CSDL được gắn Role: admin và còn lại duy nhất 1 tài khoản là admin --> tuyệt đối cấm xóa
            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn XÓA tài khoản này không? Hành động này không thể hoàn tác và ứng dụng sẽ thoát.",
                "Xác nhận xóa tài khoản",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop);

            if (confirm == DialogResult.Yes)
            {
                // BƯỚC KIỂM TRA ADMIN CUỐI CÙNG 
                // 1. Kiểm tra xem tài khoản này có phải là Admin không
                if (this.currentUserRole == "Admin")
                {
                    // 2. Nếu đúng là Admin, đếm xem có bao nhiêu Admin trong CSDL
                    int adminCount = DatabaseHelper.GetAdminAccountCount();

                    // 3. Nếu chỉ có 1 Admin (là tài khoản này) -> tuyệt đối Cấm xóa
                    if (adminCount <= 1)
                    {
                        MessageBox.Show("Lỗi: Không thể xóa tài khoản Admin cuối cùng.\n" +
                                        "Bạn phải chỉ định một người dùng khác làm Admin trước khi xóa tài khoản này.",
                                        "Cấm xóa Admin cuối cùng",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return; // Dừng lại, không làm gì cả
                    }
                }
                //nếu kiểm tra xong, thực hiện thông báo đã xóa thành công
                bool success = DatabaseHelper.DeleteTaiKhoan(this.currentUsername);

                if (success)
                {
                    MessageBox.Show("Tài khoản của bạn đã được xóa. Ứng dụng sẽ quay về Login.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Application.Exit();
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_turn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            //Chỉnh sửa icon cho button Hiển bản rõ của mật khẩu
            //txtPassword.UseSystemPasswordChar = false;
            if (txtPassword.UseSystemPasswordChar == true)
            {
                //nếu đang mã hóa -> hiện bản rõ
                txtPassword.UseSystemPasswordChar = false;

                //cập nhật icon đã bẻ khóa thành bản rõ
                btn_hide_pass.Image = Properties.Resources.Icon_lock_open;

            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                //cập nhật icon bị khóa khi mã hóa
                btn_hide_pass.Image = Properties.Resources.Icon_lock_closed;
            }
        }

        private void btn_hide_pass_confirm_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.UseSystemPasswordChar == true)
            {
                //nếu đang mã hóa -> hiện bản rõ
                txtConfirmPassword.UseSystemPasswordChar = false;

                //cập nhật icon đã bẻ khóa thành bản rõ
                btn_hide_pass_confirm.Image = Properties.Resources.Icon_lock_open;

            }
            else
            {
                txtConfirmPassword.UseSystemPasswordChar = true;
                //cập nhật icon bị khóa khi mã hóa
                btn_hide_pass_confirm.Image = Properties.Resources.Icon_lock_closed;
            }
        }
    }
}

