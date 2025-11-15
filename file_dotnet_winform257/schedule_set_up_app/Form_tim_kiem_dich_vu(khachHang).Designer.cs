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
            ((System.ComponentModel.ISupportInitialize)dgvDichVu).BeginInit();
            SuspendLayout();
            // 
            // dgvDichVu
            // 
            dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDichVu.Location = new Point(27, 194);
            dgvDichVu.Name = "dgvDichVu";
            dgvDichVu.RowHeadersWidth = 51;
            dgvDichVu.Size = new Size(780, 283);
            dgvDichVu.TabIndex = 0;
            // 
            // cbLoaiDichVu
            // 
            cbLoaiDichVu.FormattingEnabled = true;
            cbLoaiDichVu.Location = new Point(259, 97);
            cbLoaiDichVu.Name = "cbLoaiDichVu";
            cbLoaiDichVu.Size = new Size(164, 28);
            cbLoaiDichVu.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(684, 96);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 3;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(42, 98);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.Size = new Size(164, 27);
            txtKeyword.TabIndex = 4;
            // 
            // upHour
            // 
            upHour.Location = new Point(485, 97);
            upHour.Name = "upHour";
            upHour.Size = new Size(55, 27);
            upHour.TabIndex = 5;
            upHour.Text = "domainUpDown1";
            // 
            // upMinute
            // 
            upMinute.Location = new Point(564, 96);
            upMinute.Name = "upMinute";
            upMinute.Size = new Size(55, 27);
            upMinute.TabIndex = 6;
            upMinute.Text = "domainUpDown2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(546, 98);
            label1.Name = "label1";
            label1.Size = new Size(12, 20);
            label1.TabIndex = 7;
            label1.Text = ":";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 74);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 8;
            label2.Text = "Từ khoá";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(259, 74);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 9;
            label3.Text = "Loại dịch vụ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(485, 78);
            label4.Name = "label4";
            label4.Size = new Size(71, 20);
            label4.TabIndex = 10;
            label4.Text = "Thời gian";
            // 
            // Form_tim_kiem_dich_vu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 521);
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
            Margin = new Padding(2);
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
    }
}