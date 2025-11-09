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
            btnSearch = new Button();
            btnLock = new Button();
            btnUnlock = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(120, 121);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.Size = new Size(496, 188);
            dgvUsers.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(522, 46);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnLock
            // 
            btnLock.Location = new Point(120, 366);
            btnLock.Name = "btnLock";
            btnLock.Size = new Size(94, 29);
            btnLock.TabIndex = 2;
            btnLock.Text = "Khoá";
            btnLock.UseVisualStyleBackColor = true;
            // 
            // btnUnlock
            // 
            btnUnlock.Location = new Point(326, 366);
            btnUnlock.Name = "btnUnlock";
            btnUnlock.Size = new Size(94, 29);
            btnUnlock.TabIndex = 3;
            btnUnlock.Text = "Mở khoá";
            btnUnlock.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(522, 366);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(120, 48);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(371, 27);
            txtSearch.TabIndex = 5;
            txtSearch.Text = "Tên tài khoản";
            // 
            // Form_quan_ly_users
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(743, 460);
            Controls.Add(txtSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnUnlock);
            Controls.Add(btnLock);
            Controls.Add(btnSearch);
            Controls.Add(dgvUsers);
            Margin = new Padding(2);
            Name = "Form_quan_ly_users";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_quan_ly_users";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsers;
        private Button btnSearch;
        private Button btnLock;
        private Button btnUnlock;
        private Button btnDelete;
        private TextBox txtSearch;
    }
}