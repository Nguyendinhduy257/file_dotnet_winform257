namespace schedule_set_up_app
{
    partial class Form_quan_ly_dich_vu_nhaCungCapDichVu_
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
            lblTenDichVu = new Label();
            lblGia = new Label();
            lblMoTa = new Label();
            lblThoiGian = new Label();
            textTen = new TextBox();
            textGia = new TextBox();
            textMoTa = new TextBox();
            textThoiGian = new TextBox();
            dgvDichVu = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            SuspendLayout();
            // 
            // lblTenDichVu
            // 
            lblTenDichVu.BackColor = Color.FromArgb(255, 192, 128);
            lblTenDichVu.BorderStyle = BorderStyle.FixedSingle;
            lblTenDichVu.Location = new Point(12, 94);
            lblTenDichVu.Name = "lblTenDichVu";
            lblTenDichVu.Size = new Size(104, 27);
            lblTenDichVu.TabIndex = 0;
            lblTenDichVu.Text = "Tên dịch vụ";
            lblTenDichVu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblGia
            // 
            lblGia.BackColor = Color.FromArgb(255, 192, 128);
            lblGia.BorderStyle = BorderStyle.FixedSingle;
            lblGia.Location = new Point(12, 144);
            lblGia.Name = "lblGia";
            lblGia.Size = new Size(104, 27);
            lblGia.TabIndex = 1;
            lblGia.Text = "Giá";
            lblGia.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMoTa
            // 
            lblMoTa.BackColor = Color.FromArgb(255, 192, 128);
            lblMoTa.BorderStyle = BorderStyle.FixedSingle;
            lblMoTa.Location = new Point(12, 196);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(104, 27);
            lblMoTa.TabIndex = 2;
            lblMoTa.Text = "Mô tả";
            lblMoTa.TextAlign = ContentAlignment.MiddleCenter;
            lblMoTa.Click += label3_Click;
            // 
            // lblThoiGian
            // 
            lblThoiGian.BackColor = Color.FromArgb(255, 192, 128);
            lblThoiGian.BorderStyle = BorderStyle.FixedSingle;
            lblThoiGian.Cursor = Cursors.IBeam;
            lblThoiGian.Location = new Point(12, 242);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(104, 27);
            lblThoiGian.TabIndex = 3;
            lblThoiGian.Text = "Thời gian";
            lblThoiGian.TextAlign = ContentAlignment.MiddleCenter;
            lblThoiGian.Click += label4_Click;
            // 
            // textTen
            // 
            textTen.Location = new Point(122, 94);
            textTen.Name = "textTen";
            textTen.Size = new Size(182, 27);
            textTen.TabIndex = 4;
            // 
            // textGia
            // 
            textGia.Location = new Point(122, 144);
            textGia.Name = "textGia";
            textGia.Size = new Size(182, 27);
            textGia.TabIndex = 5;
            // 
            // textMoTa
            // 
            textMoTa.Location = new Point(122, 196);
            textMoTa.Name = "textMoTa";
            textMoTa.Size = new Size(182, 27);
            textMoTa.TabIndex = 6;
            // 
            // textThoiGian
            // 
            textThoiGian.Location = new Point(122, 242);
            textThoiGian.Name = "textThoiGian";
            textThoiGian.Size = new Size(182, 27);
            textThoiGian.TabIndex = 7;
            // 
            // dgvDichVu
            // 
            dgvDichVu.BackgroundColor = Color.White;
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(327, 93);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(553, 237);
            dgvDichVu.TabIndex = 8;
            dgvDichVu.CellClick += dgvDichVu_CellClick;
            dgvDichVu.CellContentClick += dgvDichVu_CellContentClick;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.FromArgb(128, 255, 128);
            btnThem.Location = new Point(24, 296);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(71, 34);
            btnThem.TabIndex = 9;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.BackColor = Color.DodgerBlue;
            btnSua.Location = new Point(122, 296);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(71, 34);
            btnSua.TabIndex = 10;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = false;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.BackColor = Color.FromArgb(255, 128, 128);
            btnXoa.Location = new Point(217, 296);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(71, 34);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = false;
            btnXoa.Click += btnXoa_Click;
            // 
            // Form_quan_ly_dich_vu_nhaCungCapDichVu_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(914, 416);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvDichVu);
            Controls.Add(textThoiGian);
            Controls.Add(textMoTa);
            Controls.Add(textGia);
            Controls.Add(textTen);
            Controls.Add(lblThoiGian);
            Controls.Add(lblMoTa);
            Controls.Add(lblGia);
            Controls.Add(lblTenDichVu);
            Name = "Form_quan_ly_dich_vu_nhaCungCapDichVu_";
            Text = "Form_quan_ly_dich_vu_nhaCungCapDichVu_";
            Load += Form_quan_ly_dich_vu_nhaCungCapDichVu__Load;
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTenDichVu;
        private Label lblGia;
        private Label lblMoTa;
        private Label lblThoiGian;
        private TextBox textTen;
        private TextBox textGia;
        private TextBox textMoTa;
        private TextBox textThoiGian;
        private DataGridView dgvDichVu;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
    }
}