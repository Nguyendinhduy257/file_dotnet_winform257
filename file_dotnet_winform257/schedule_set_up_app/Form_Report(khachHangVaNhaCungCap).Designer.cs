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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtpDenNgay = new DateTimePicker();
            btnTaoBaoCao = new Button();
            dgvBaoCao = new DataGridView();
            cboLoaiBaoCao = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
            SuspendLayout();
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Location = new Point(284, 91);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(193, 27);
            dtpDenNgay.TabIndex = 2;
            dtpDenNgay.Tag = "";
            // 
            // btnTaoBaoCao
            // 
            btnTaoBaoCao.Location = new Point(559, 73);
            btnTaoBaoCao.Name = "btnTaoBaoCao";
            btnTaoBaoCao.Size = new Size(123, 45);
            btnTaoBaoCao.TabIndex = 3;
            btnTaoBaoCao.Tag = "";
            btnTaoBaoCao.Text = "Tạo báo cáo";
            btnTaoBaoCao.UseVisualStyleBackColor = true;
            btnTaoBaoCao.Click += btnTaoBaoCao_Click;
            // 
            // dgvBaoCao
            // 
            dgvBaoCao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaoCao.Location = new Point(43, 184);
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.RowHeadersWidth = 51;
            dgvBaoCao.Size = new Size(639, 188);
            dgvBaoCao.TabIndex = 6;
            // 
            // cboLoaiBaoCao
            // 
            cboLoaiBaoCao.FormattingEnabled = true;
            cboLoaiBaoCao.Location = new Point(43, 90);
            cboLoaiBaoCao.Name = "cboLoaiBaoCao";
            cboLoaiBaoCao.Size = new Size(193, 28);
            cboLoaiBaoCao.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 57);
            label1.Name = "label1";
            label1.Size = new Size(95, 20);
            label1.TabIndex = 8;
            label1.Text = "Loại báo cáo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(284, 57);
            label2.Name = "label2";
            label2.Size = new Size(71, 20);
            label2.TabIndex = 9;
            label2.Text = "Thời gian";
            // 
            // Form_Report_khachHang_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(726, 465);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboLoaiBaoCao);
            Controls.Add(dgvBaoCao);
            Controls.Add(btnTaoBaoCao);
            Controls.Add(dtpDenNgay);
            Margin = new Padding(2);
            Name = "Form_Report_khachHang_";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Report_khachHang_";
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DateTimePicker dtpDenNgay;
        private Button btnTaoBaoCao;
        private DataGridView dgvBaoCao;
        private ComboBox cboLoaiBaoCao;
        private Label label1;
        private Label label2;
    }
}