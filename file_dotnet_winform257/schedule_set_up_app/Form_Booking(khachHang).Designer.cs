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
            dgvDichVu = new DataGridView();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            textTen = new TextBox();
            textThoiGian = new TextBox();
            textMoTa = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            SuspendLayout();
            // 
            // dgvDichVu
            // 
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(361, 91);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(430, 237);
            dgvDichVu.TabIndex = 0;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(12, 299);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(73, 29);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(137, 299);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(73, 29);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(254, 299);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(73, 29);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xoa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // textTen
            // 
            textTen.Location = new Point(137, 107);
            textTen.Name = "textTen";
            textTen.Size = new Size(164, 27);
            textTen.TabIndex = 4;
            // 
            // textThoiGian
            // 
            textThoiGian.Location = new Point(137, 236);
            textThoiGian.Name = "textThoiGian";
            textThoiGian.Size = new Size(164, 27);
            textThoiGian.TabIndex = 5;
            // 
            // textMoTa
            // 
            textMoTa.Location = new Point(137, 169);
            textMoTa.Name = "textMoTa";
            textMoTa.Size = new Size(164, 27);
            textMoTa.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 110);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 7;
            label1.Text = "Tên dịch vụ";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 172);
            label2.Name = "label2";
            label2.Size = new Size(48, 20);
            label2.TabIndex = 8;
            label2.Text = "Mô tả";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 243);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 9;
            label3.Text = "Thời gian";
            // 
            // Form_Booking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(818, 432);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textMoTa);
            Controls.Add(textThoiGian);
            Controls.Add(textTen);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvDichVu);
            Margin = new Padding(2);
            Name = "Form_Booking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Booking";
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDichVu;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private TextBox textTen;
        private TextBox textThoiGian;
        private TextBox textMoTa;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}