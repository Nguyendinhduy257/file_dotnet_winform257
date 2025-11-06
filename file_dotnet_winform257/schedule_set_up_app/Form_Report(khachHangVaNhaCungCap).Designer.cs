namespace schedule_set_up_app
{
    partial class Form_Report_khachHang_
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

        #region 

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblTongSoLieu = new Label();
            cboLoaiBaoCao = new ComboBox();
            dtpTuNgay = new DateTimePicker();
            dtpDenNgay = new DateTimePicker();
            dgvBaoCao = new DataGridView();
            btnTaoBaoCao = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(255, 192, 128);
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Location = new Point(63, 31);
            label1.Name = "label1";
            label1.Size = new Size(151, 25);
            label1.TabIndex = 0;
            label1.Text = "Chọn loại báo cáo";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(255, 192, 128);
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Location = new Point(242, 31);
            label2.Name = "label2";
            label2.Size = new Size(151, 25);
            label2.TabIndex = 1;
            label2.Text = "Từ ngày";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += label2_Click_1;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(255, 192, 128);
            label3.BorderStyle = BorderStyle.FixedSingle;
            label3.Location = new Point(441, 31);
            label3.Name = "label3";
            label3.Size = new Size(151, 25);
            label3.TabIndex = 2;
            label3.Text = "Đến ngày";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTongSoLieu
            // 
            lblTongSoLieu.AutoSize = true;
            lblTongSoLieu.Location = new Point(95, 355);
            lblTongSoLieu.Name = "lblTongSoLieu";
            lblTongSoLieu.Size = new Size(90, 20);
            lblTongSoLieu.TabIndex = 3;
            lblTongSoLieu.Text = "Tổng số liệu";
            lblTongSoLieu.Click += label4_Click_1;
            // 
            // cboLoaiBaoCao
            // 
            cboLoaiBaoCao.FormattingEnabled = true;
            cboLoaiBaoCao.Location = new Point(63, 54);
            cboLoaiBaoCao.Name = "cboLoaiBaoCao";
            cboLoaiBaoCao.Size = new Size(151, 28);
            cboLoaiBaoCao.TabIndex = 4;
            // 
            // dtpTuNgay
            // 
            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.Location = new Point(242, 55);
            dtpTuNgay.Name = "dtpTuNgay";
            dtpTuNgay.Size = new Size(151, 27);
            dtpTuNgay.TabIndex = 5;
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.Location = new Point(441, 55);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(151, 27);
            dtpDenNgay.TabIndex = 6;
            dtpDenNgay.ValueChanged += dateTimePicker2_ValueChanged_1;
            // 
            // dgvBaoCao
            // 
            dgvBaoCao.BackgroundColor = Color.White;
            dgvBaoCao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaoCao.Location = new Point(95, 104);
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.RowHeadersWidth = 51;
            dgvBaoCao.Size = new Size(573, 207);
            dgvBaoCao.TabIndex = 7;
            dgvBaoCao.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnTaoBaoCao
            // 
            btnTaoBaoCao.BackColor = Color.FromArgb(128, 255, 128);
            btnTaoBaoCao.Location = new Point(619, 31);
            btnTaoBaoCao.Name = "btnTaoBaoCao";
            btnTaoBaoCao.Size = new Size(101, 51);
            btnTaoBaoCao.TabIndex = 8;
            btnTaoBaoCao.Text = "Tạo báo cáo";
            btnTaoBaoCao.UseVisualStyleBackColor = false;
            btnTaoBaoCao.Click += btnTaoBaoCao_Click;
            // 
            // Form_Report_khachHang_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(785, 450);
            Controls.Add(btnTaoBaoCao);
            Controls.Add(dgvBaoCao);
            Controls.Add(dtpDenNgay);
            Controls.Add(dtpTuNgay);
            Controls.Add(cboLoaiBaoCao);
            Controls.Add(lblTongSoLieu);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form_Report_khachHang_";
            Text = "Form_Report_khachHang_";
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblTongSoLieu;
        private ComboBox cboLoaiBaoCao;
        private DateTimePicker dtpTuNgay;
        private DateTimePicker dtpDenNgay;
        private DataGridView dgvBaoCao;
        private Button btnTaoBaoCao;
    }
}