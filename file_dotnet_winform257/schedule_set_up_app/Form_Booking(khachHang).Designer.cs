namespace schedule_set_up_app
{
    partial class Form_Booking
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            txtNoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            btnThem = new Guna.UI2.WinForms.Guna2Button();
            btnXoa = new Guna.UI2.WinForms.Guna2Button();
            btnSua = new Guna.UI2.WinForms.Guna2Button();
            btnTroLai = new Guna.UI2.WinForms.Guna2Button();
            dtpThoiGian1 = new DateTimePicker();
            dgvLichHenNgay3 = new Guna.UI2.WinForms.Guna2DataGridView();
            colSelectAll = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            lblNgayLapLich = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvLichHenNgay3).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtNoiDung
            // 
            txtNoiDung.BorderRadius = 20;
            txtNoiDung.CustomizableEdges = customizableEdges21;
            txtNoiDung.DefaultText = "";
            txtNoiDung.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtNoiDung.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtNoiDung.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtNoiDung.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtNoiDung.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNoiDung.Font = new Font("Segoe UI", 14F);
            txtNoiDung.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtNoiDung.Location = new Point(582, 248);
            txtNoiDung.Margin = new Padding(8, 9, 8, 9);
            txtNoiDung.Multiline = true;
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.PlaceholderText = "Nhập nội dung/mô tả lịch hẹn..";
            txtNoiDung.SelectedText = "";
            txtNoiDung.ShadowDecoration.CustomizableEdges = customizableEdges22;
            txtNoiDung.Size = new Size(1004, 141);
            txtNoiDung.TabIndex = 2;
            // 
            // btnThem
            // 
            btnThem.Animated = true;
            btnThem.BorderRadius = 20;
            btnThem.BorderThickness = 1;
            btnThem.Cursor = Cursors.Hand;
            btnThem.CustomizableEdges = customizableEdges23;
            btnThem.DisabledState.BorderColor = Color.DarkGray;
            btnThem.DisabledState.CustomBorderColor = Color.DarkGray;
            btnThem.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnThem.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnThem.Font = new Font("Segoe UI", 14F);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(582, 493);
            btnThem.Name = "btnThem";
            btnThem.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnThem.Size = new Size(315, 79);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnXoa
            // 
            btnXoa.Animated = true;
            btnXoa.BorderRadius = 20;
            btnXoa.BorderThickness = 1;
            btnXoa.Cursor = Cursors.Hand;
            btnXoa.CustomizableEdges = customizableEdges25;
            btnXoa.DisabledState.BorderColor = Color.DarkGray;
            btnXoa.DisabledState.CustomBorderColor = Color.DarkGray;
            btnXoa.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnXoa.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnXoa.Font = new Font("Segoe UI", 14F);
            btnXoa.ForeColor = Color.White;
            btnXoa.Location = new Point(1271, 493);
            btnXoa.Name = "btnXoa";
            btnXoa.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnXoa.Size = new Size(315, 79);
            btnXoa.TabIndex = 4;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnSua
            // 
            btnSua.Animated = true;
            btnSua.BorderRadius = 20;
            btnSua.BorderThickness = 1;
            btnSua.Cursor = Cursors.Hand;
            btnSua.CustomizableEdges = customizableEdges27;
            btnSua.DisabledState.BorderColor = Color.DarkGray;
            btnSua.DisabledState.CustomBorderColor = Color.DarkGray;
            btnSua.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnSua.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnSua.Font = new Font("Segoe UI", 14F);
            btnSua.ForeColor = Color.White;
            btnSua.Location = new Point(928, 493);
            btnSua.Name = "btnSua";
            btnSua.ShadowDecoration.CustomizableEdges = customizableEdges28;
            btnSua.Size = new Size(315, 79);
            btnSua.TabIndex = 5;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnTroLai
            // 
            btnTroLai.Animated = true;
            btnTroLai.BorderRadius = 20;
            btnTroLai.BorderThickness = 1;
            btnTroLai.Cursor = Cursors.Hand;
            btnTroLai.CustomizableEdges = customizableEdges29;
            btnTroLai.DisabledState.BorderColor = Color.DarkGray;
            btnTroLai.DisabledState.CustomBorderColor = Color.DarkGray;
            btnTroLai.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnTroLai.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnTroLai.Font = new Font("Segoe UI", 14F);
            btnTroLai.ForeColor = Color.White;
            btnTroLai.Location = new Point(23, 25);
            btnTroLai.Name = "btnTroLai";
            btnTroLai.ShadowDecoration.CustomizableEdges = customizableEdges30;
            btnTroLai.Size = new Size(315, 79);
            btnTroLai.TabIndex = 6;
            btnTroLai.Text = "Trở lại";
            btnTroLai.Click += btnTroLai_Click;
            // 
            // dtpThoiGian1
            // 
            dtpThoiGian1.Cursor = Cursors.Hand;
            dtpThoiGian1.Font = new Font("Segoe UI", 14F);
            dtpThoiGian1.Format = DateTimePickerFormat.Time;
            dtpThoiGian1.Location = new Point(582, 408);
            dtpThoiGian1.Name = "dtpThoiGian1";
            dtpThoiGian1.Size = new Size(315, 51);
            dtpThoiGian1.TabIndex = 7;
            // 
            // dgvLichHenNgay3
            // 
            dgvLichHenNgay3.AllowUserToAddRows = false;
            dgvLichHenNgay3.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = Color.White;
            dgvLichHenNgay3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 14F);
            dataGridViewCellStyle8.ForeColor = Color.Yellow;
            dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvLichHenNgay3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvLichHenNgay3.ColumnHeadersHeight = 100;
            dgvLichHenNgay3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvLichHenNgay3.Columns.AddRange(new DataGridViewColumn[] { colSelectAll });
            dgvLichHenNgay3.Cursor = Cursors.Hand;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.White;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 14F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle9.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            dgvLichHenNgay3.DefaultCellStyle = dataGridViewCellStyle9;
            dgvLichHenNgay3.Dock = DockStyle.Fill;
            dgvLichHenNgay3.GridColor = Color.FromArgb(231, 229, 255);
            dgvLichHenNgay3.Location = new Point(0, 0);
            dgvLichHenNgay3.Name = "dgvLichHenNgay3";
            dgvLichHenNgay3.ReadOnly = true;
            dgvLichHenNgay3.RowHeadersVisible = false;
            dgvLichHenNgay3.RowHeadersWidth = 72;
            dgvLichHenNgay3.ScrollBars = ScrollBars.Vertical;
            dgvLichHenNgay3.Size = new Size(2045, 433);
            dgvLichHenNgay3.TabIndex = 8;
            dgvLichHenNgay3.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvLichHenNgay3.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvLichHenNgay3.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvLichHenNgay3.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvLichHenNgay3.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvLichHenNgay3.ThemeStyle.BackColor = Color.White;
            dgvLichHenNgay3.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvLichHenNgay3.ThemeStyle.HeaderStyle.Height = 100;
            dgvLichHenNgay3.ThemeStyle.ReadOnly = true;
            dgvLichHenNgay3.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvLichHenNgay3.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvLichHenNgay3.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvLichHenNgay3.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvLichHenNgay3.ThemeStyle.RowsStyle.Height = 37;
            dgvLichHenNgay3.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvLichHenNgay3.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvLichHenNgay3.CellContentClick += dgvLichHenNgay3_CellContentClick;
            dgvLichHenNgay3.ColumnHeaderMouseClick += dgvLichHenNgay3_ColumnHeaderMouseClick;
            // 
            // colSelectAll
            // 
            colSelectAll.HeaderText = "Select All";
            colSelectAll.MinimumWidth = 9;
            colSelectAll.Name = "colSelectAll";
            colSelectAll.ReadOnly = true;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(302, 248);
            label1.Name = "label1";
            label1.Size = new Size(280, 141);
            label1.TabIndex = 9;
            label1.Text = "Nội dung mô tả:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(198, 408);
            label2.Name = "label2";
            label2.Size = new Size(384, 51);
            label2.TabIndex = 10;
            label2.Text = "Giờ đặt trong ngày:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(dgvLichHenNgay3);
            panel1.Location = new Point(83, 603);
            panel1.Name = "panel1";
            panel1.Size = new Size(2045, 433);
            panel1.TabIndex = 11;
            // 
            // lblNgayLapLich
            // 
            lblNgayLapLich.Font = new Font("Segoe UI", 24F);
            lblNgayLapLich.Location = new Point(23, 111);
            lblNgayLapLich.Name = "lblNgayLapLich";
            lblNgayLapLich.Size = new Size(2119, 100);
            lblNgayLapLich.TabIndex = 12;
            lblNgayLapLich.Text = "label3";
            lblNgayLapLich.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form_Booking
            // 
            AcceptButton = btnThem;
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(2184, 880);
            Controls.Add(lblNgayLapLich);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpThoiGian1);
            Controls.Add(btnTroLai);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnThem);
            Controls.Add(txtNoiDung);
            Name = "Form_Booking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Booking";
            Load += Form_Booking_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLichHenNgay3).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpThoiGian;
        private Guna.UI2.WinForms.Guna2TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnTroLai;
        private DateTimePicker dtpThoiGian1;
        private Guna.UI2.WinForms.Guna2DataGridView dgvLichHenNgay3;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private DataGridViewTextBoxColumn colSelectAll;
        private Label lblNgayLapLich;
    }
}