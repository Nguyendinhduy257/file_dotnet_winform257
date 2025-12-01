namespace schedule_set_up_app
{
    partial class Form_tim_kiem_dich_vu
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
            cbLoaiDichVu = new ComboBox();
            btnSearch = new Button();
            txtKeyword = new TextBox();
            upHour = new DomainUpDown();
            upMinute = new DomainUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            SuspendLayout();
            // 
            // dgvDichVu
            // 
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(40, 291);
            dgvDichVu.Margin = new Padding(4, 4, 4, 4);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(1170, 424);
            dgvDichVu.TabIndex = 0;
            // 
            // cbLoaiDichVu
            // 
            cbLoaiDichVu.FormattingEnabled = true;
            cbLoaiDichVu.Location = new Point(388, 146);
            cbLoaiDichVu.Margin = new Padding(4, 4, 4, 4);
            cbLoaiDichVu.Name = "cbLoaiDichVu";
            cbLoaiDichVu.Size = new Size(244, 38);
            cbLoaiDichVu.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1026, 144);
            btnSearch.Margin = new Padding(4, 4, 4, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(141, 44);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(63, 147);
            txtKeyword.Margin = new Padding(4, 4, 4, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(244, 35);
            txtKeyword.TabIndex = 4;
            // 
            // upHour
            // 
            upHour.Location = new Point(728, 146);
            upHour.Margin = new Padding(4, 4, 4, 4);
            upHour.Name = "upHour";
            upHour.Size = new Size(82, 35);
            upHour.TabIndex = 5;
            upHour.Text = "domainUpDown1";
            // 
            // upMinute
            // 
            upMinute.Location = new Point(846, 144);
            upMinute.Margin = new Padding(4, 4, 4, 4);
            upMinute.Name = "upMinute";
            upMinute.Size = new Size(82, 35);
            upMinute.TabIndex = 6;
            upMinute.Text = "domainUpDown2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(819, 147);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(18, 30);
            label1.TabIndex = 7;
            label1.Text = ":";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 111);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(87, 30);
            label2.TabIndex = 8;
            label2.Text = "Từ khoá";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(388, 111);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(124, 30);
            label3.TabIndex = 9;
            label3.Text = "Loại dịch vụ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(728, 117);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(100, 30);
            label4.TabIndex = 10;
            label4.Text = "Thời gian";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 29F);
            label5.Location = new Point(470, 466);
            label5.Name = "label5";
            label5.Size = new Size(309, 91);
            label5.TabIndex = 11;
            label5.Text = "Khai tử??";
            // 
            // Form_tim_kiem_dich_vu
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 782);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(upMinute);
            Controls.Add(upHour);
            Controls.Add(txtKeyword);
            Controls.Add(btnSearch);
            Controls.Add(cbLoaiDichVu);
            Controls.Add(dgvDichVu);
            Name = "Form_tim_kiem_dich_vu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_tim_kiem_dich_vu";
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDichVu;
        private ComboBox cbLoaiDichVu;
        //private ComboBox cbKho;
        private Button btnSearch;
        private TextBox txtKeyword;
        private DomainUpDown upHour;
        private DomainUpDown upMinute;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}