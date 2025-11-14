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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            SuspendLayout();
            // 
            // dgvDichVu
            // 
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(542, 136);
            dgvDichVu.Margin = new Padding(4, 4, 4, 4);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(645, 356);
            dgvDichVu.TabIndex = 0;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(18, 448);
            btnThem.Margin = new Padding(4, 4, 4, 4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 44);
            btnThem.TabIndex = 1;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(206, 448);
            btnSua.Margin = new Padding(4, 4, 4, 4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 44);
            btnSua.TabIndex = 2;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(381, 448);
            btnXoa.Margin = new Padding(4, 4, 4, 4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 44);
            btnXoa.TabIndex = 3;
            btnXoa.Text = "Xoa";
            btnXoa.UseVisualStyleBackColor = true;
            // 
            // textTen
            // 
            textTen.Location = new Point(206, 160);
            textTen.Margin = new Padding(4, 4, 4, 4);
            textTen.Name = "textTen";
            textTen.Size = new Size(244, 35);
            textTen.TabIndex = 4;
            // 
            // textThoiGian
            // 
            textThoiGian.Location = new Point(206, 354);
            textThoiGian.Margin = new Padding(4, 4, 4, 4);
            textThoiGian.Name = "textThoiGian";
            textThoiGian.Size = new Size(244, 35);
            textThoiGian.TabIndex = 5;
            // 
            // textMoTa
            // 
            textMoTa.Location = new Point(206, 254);
            textMoTa.Margin = new Padding(4, 4, 4, 4);
            textMoTa.Name = "textMoTa";
            textMoTa.Size = new Size(244, 35);
            textMoTa.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(38, 165);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(118, 30);
            label1.TabIndex = 7;
            label1.Text = "Tên dịch vụ";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 258);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 30);
            label2.TabIndex = 8;
            label2.Text = "Mô tả";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 364);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(100, 30);
            label3.TabIndex = 9;
            label3.Text = "Thời gian";
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(736, 561);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(315, 79);
            guna2Button1.TabIndex = 10;
            guna2Button1.Text = "Qua trở về Trang chủ";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // Form_Booking
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1227, 648);
            Controls.Add(guna2Button1);
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
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}