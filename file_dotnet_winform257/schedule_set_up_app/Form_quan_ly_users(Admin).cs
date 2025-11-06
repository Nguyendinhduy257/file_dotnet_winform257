using Microsoft.VisualBasic.ApplicationServices;
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
    public partial class Form_quan_ly_users : Form
    {

        BindingList<User> danhSachNguoiDung = new BindingList<User>();
        public Form_quan_ly_users()
        {
            InitializeComponent();
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form_quan_ly_users_Load(object sender, EventArgs e)
        {
            // Dữ liệu mẫu
            danhSachNguoiDung.Add(new User { Username = "admin", Role = "Quản trị", IsLocked = false });
            danhSachNguoiDung.Add(new User { Username = "user01", Role = "Khách hàng", IsLocked = false });
            danhSachNguoiDung.Add(new User { Username = "user02", Role = "Khách hàng", IsLocked = true });

            dgvUsers.DataSource = danhSachNguoiDung;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            dgvUsers.DataSource = danhSachNguoiDung
                .Where(u => u.Username.ToLower().Contains(keyword))
                .ToList();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            User selectedUser = dgvUsers.CurrentRow.DataBoundItem as User;
            selectedUser.IsLocked = true;
            dgvUsers.Refresh();
            MessageBox.Show("Đã khóa tài khoản!");
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            User selectedUser = dgvUsers.CurrentRow.DataBoundItem as User;
            selectedUser.IsLocked = false;
            dgvUsers.Refresh();
            MessageBox.Show("Đã mở khóa tài khoản!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null) return;
            User selectedUser = dgvUsers.CurrentRow.DataBoundItem as User;
            danhSachNguoiDung.Remove(selectedUser);
            MessageBox.Show("Đã xóa tài khoản!");
        }

    }

    public class User
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public bool IsLocked { get; set; }
    }
}
