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
            txtNoiDung = new TextBox();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvBaoCao).BeginInit();
            SuspendLayout();
            // 
            // dtpDenNgay
            // 
            dtpDenNgay.Cursor = Cursors.IBeam;
            dtpDenNgay.Location = new Point(492, 140);
            dtpDenNgay.Margin = new Padding(6);
            dtpDenNgay.Name = "dtpDenNgay";
            dtpDenNgay.Size = new Size(593, 51);
            dtpDenNgay.TabIndex = 2;
            dtpDenNgay.Tag = "";
            // 
            // btnTaoBaoCao
            // 
            btnTaoBaoCao.Cursor = Cursors.Hand;
            btnTaoBaoCao.Location = new Point(591, 371);
            btnTaoBaoCao.Margin = new Padding(6);
            btnTaoBaoCao.Name = "btnTaoBaoCao";
            btnTaoBaoCao.Size = new Size(393, 102);
            btnTaoBaoCao.TabIndex = 3;
            btnTaoBaoCao.Tag = "";
            btnTaoBaoCao.Text = "Report đến Admin";
            btnTaoBaoCao.UseVisualStyleBackColor = true;
            btnTaoBaoCao.Click += btnTaoBaoCao_Click;
            // 
            // dgvBaoCao
            // 
            dgvBaoCao.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBaoCao.Location = new Point(15, 503);
            dgvBaoCao.Margin = new Padding(6);
            dgvBaoCao.Name = "dgvBaoCao";
            dgvBaoCao.RowHeadersWidth = 51;
            dgvBaoCao.Size = new Size(1452, 423);
            dgvBaoCao.TabIndex = 6;
            // 
            // cboLoaiBaoCao
            // 
            cboLoaiBaoCao.Cursor = Cursors.IBeam;
            cboLoaiBaoCao.FormattingEnabled = true;
            cboLoaiBaoCao.Location = new Point(492, 65);
            cboLoaiBaoCao.Margin = new Padding(6);
            cboLoaiBaoCao.Name = "cboLoaiBaoCao";
            cboLoaiBaoCao.Size = new Size(593, 53);
            cboLoaiBaoCao.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(276, 72);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(211, 45);
            label1.TabIndex = 8;
            label1.Text = "Loại báo cáo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(324, 140);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(163, 45);
            label2.TabIndex = 9;
            label2.Text = "Thời gian:";
            // 
            // txtNoiDung
            // 
            txtNoiDung.Cursor = Cursors.IBeam;
            txtNoiDung.Location = new Point(492, 206);
            txtNoiDung.Margin = new Padding(6);
            txtNoiDung.Multiline = true;
            txtNoiDung.Name = "txtNoiDung";
            txtNoiDung.Size = new Size(593, 153);
            txtNoiDung.TabIndex = 10;
            // 
            // label3
            // 
            label3.Location = new Point(303, 206);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(170, 153);
            label3.TabIndex = 11;
            label3.Text = "Nội dung:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form_Report_khachHang_
            // 
            AutoScaleDimensions = new SizeF(18F, 45F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 951);
            Controls.Add(label3);
            Controls.Add(txtNoiDung);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cboLoaiBaoCao);
            Controls.Add(dgvBaoCao);
            Controls.Add(btnTaoBaoCao);
            Controls.Add(dtpDenNgay);
            Font = new Font("Segoe UI", 14F);
            Margin = new Padding(4);
            Name = "Form_Report_khachHang_";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_Report_khachHang_";
            Load += Form_Report_khachHang__Load_1;
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
        private TextBox txtNoiDung;
        private Label label3;
    }
}