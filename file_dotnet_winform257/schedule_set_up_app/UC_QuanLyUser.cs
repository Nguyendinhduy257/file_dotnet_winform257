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
    public partial class UC_QuanLyUser : UserControl
    {
        public UC_QuanLyUser()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void UC_QuanLyUser_Load(object sender, EventArgs e)
        {
            dgvUsers.DefaultCellStyle.Font=new Font("Segoe UI", 14F);
            dgvUsers.AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells;
            dgvUsers.AutoGenerateColumns = true;
            dgvUsers.ReadOnly = true;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //gọi hàm LoadUsers để load dữ liệu lên DataGridView khi UserControl được load
            LoadUsers();
        }
        private void LoadUsers()
        {
            DataTable usersTable = DatabaseHelper.GetAllTaiKhoan();
            dgvUsers.DataSource = usersTable;
        }

        private void btnXoaUser_Click(object sender, EventArgs e)
        {
            //kiểm tra nếu ko chọn hàng nào cần xóa
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 tài khoản cần xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dgvUsers.SelectedRows[0];
            string username = row.Cells["Username"].Value.ToString();
            string role = row.Cells["Role"].Value.ToString();
            //CHECK KHÔNG CHO XÓA CHÍNH MÌNH
            if (username.Equals(UserSession.Username, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không thể xóa tài khoản của chính mình tại đây.\n" +
                                "Vui lòng sử dụng Form Profile (Setting).",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //CHECK KHÔNG CHO XÓA ADMIN CUỐI CÙNG
            if (role == "Admin")
            {
                int adminCount = DatabaseHelper.GetAdminAccountCount();
                if (adminCount <= 1)
                {
                    MessageBox.Show("Không thể xóa tài khoản Admin cuối cùng.\n" +
                                    "Hãy chỉ định một Admin khác trước.",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // XÁC NHẬN MUỐN XÓA TÀI KHOẢN KHÔNG
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa tài khoản '{username}'?",
                                                 "Xác nhận xóa",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                if (DatabaseHelper.DeleteTaiKhoan(username))
                {
                    MessageBox.Show("Xóa tài khoản thành công.");
                    LoadUsers(); // Tải lại bảng
                }
            }
        }

        private void btnCapNhatRole_Click(object sender, EventArgs e)
        {
            // Kiểm tra 1: Đã chọn hàng chưa?
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra 2: Đã chọn Role mới chưa?
            if (cmbSetRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò mới (User/Admin) trong hộp thả xuống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dgvUsers.SelectedRows[0];
            string username = row.Cells["Username"].Value.ToString();
            string oldRole = row.Cells["Role"].Value.ToString();
            string newRole = cmbSetRole.Text;

            //CHECK AN TOÀN 3: KHÔNG CHO ĐỔI ROLE ADMIN CUỐI CÙNG 
            if (oldRole == "Admin" && newRole == "User")
            {
                int adminCount = DatabaseHelper.GetAdminAccountCount();
                if (adminCount <= 1)
                {
                    MessageBox.Show("Không thể đổi vai trò của Admin cuối cùng thành User.",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //  CẬP NHẬT VAI TRÒ 
            bool success = DatabaseHelper.UpdateUserRole(username, newRole);
            if (success)
            {
                MessageBox.Show($"Đã cập nhật vai trò của '{username}' thành '{newRole}'.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers(); // Tải lại bảng
            }
        }
    }
}
