namespace schedule_set_up_app
{
    partial class Form_quan_ly_users
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnLock = new Button();
            btnUnlock = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(56, 78);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(436, 141);
            dgvUsers.TabIndex = 0;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(85, 33);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(287, 27);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(378, 33);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnLock
            // 
            btnLock.BackColor = Color.FromArgb(255, 128, 255);
            btnLock.Location = new Point(205, 236);
            btnLock.Name = "btnLock";
            btnLock.Size = new Size(129, 29);
            btnLock.TabIndex = 3;
            btnLock.Text = "Khóa tài khoản";
            btnLock.UseVisualStyleBackColor = false;
            btnLock.Click += btnLock_Click;
            // 
            // btnUnlock
            // 
            btnUnlock.BackColor = Color.Lime;
            btnUnlock.Location = new Point(205, 292);
            btnUnlock.Name = "btnUnlock";
            btnUnlock.Size = new Size(129, 29);
            btnUnlock.TabIndex = 4;
            btnUnlock.Text = "Mở khóa ";
            btnUnlock.UseVisualStyleBackColor = false;
            btnUnlock.Click += btnUnlock_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(255, 128, 128);
            btnDelete.Location = new Point(205, 344);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(129, 29);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Xóa tài khoản";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // Form_quan_ly_users
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(565, 421);
            Controls.Add(btnDelete);
            Controls.Add(btnUnlock);
            Controls.Add(btnLock);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(dgvUsers);
            Name = "Form_quan_ly_users";
            Text = "Form_quan_ly_users";
            Load += Form_quan_ly_users_Load;
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsers;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnLock;
        private Button btnUnlock;
        private Button btnDelete;
    }
}