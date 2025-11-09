namespace schedule_set_up_app
{
    partial class Form_quan_ly_lich_Admin_
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
            dgvLichChoDuyet = new DataGridView();
            btnXacNhan = new Button();
            lblTrangThai = new Label();
            rdoDuyet = new RadioButton();
            rdoTuChoi = new RadioButton();
            txtLyDoTuChoi = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvLichChoDuyet).BeginInit();
            SuspendLayout();
            // 
            // dgvLichChoDuyet
            // 
            dgvLichChoDuyet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichChoDuyet.Location = new Point(54, 24);
            dgvLichChoDuyet.Name = "dgvLichChoDuyet";
            dgvLichChoDuyet.RowHeadersWidth = 51;
            dgvLichChoDuyet.Size = new Size(680, 216);
            dgvLichChoDuyet.TabIndex = 0;
            // 
            // btnXacNhan
            // 
            btnXacNhan.Location = new Point(66, 387);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(94, 29);
            btnXacNhan.TabIndex = 1;
            btnXacNhan.Text = "Xác nhận";
            btnXacNhan.UseVisualStyleBackColor = true;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(377, 396);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(75, 20);
            lblTrangThai.TabIndex = 2;
            lblTrangThai.Text = "Trạng thái";
            // 
            // rdoDuyet
            // 
            rdoDuyet.AutoSize = true;
            rdoDuyet.Location = new Point(66, 259);
            rdoDuyet.Name = "rdoDuyet";
            rdoDuyet.Size = new Size(69, 24);
            rdoDuyet.TabIndex = 3;
            rdoDuyet.TabStop = true;
            rdoDuyet.Text = "Duyệt";
            rdoDuyet.UseVisualStyleBackColor = true;
            // 
            // rdoTuChoi
            // 
            rdoTuChoi.AutoSize = true;
            rdoTuChoi.Location = new Point(273, 259);
            rdoTuChoi.Name = "rdoTuChoi";
            rdoTuChoi.Size = new Size(79, 24);
            rdoTuChoi.TabIndex = 4;
            rdoTuChoi.TabStop = true;
            rdoTuChoi.Text = "Từ chối";
            rdoTuChoi.UseVisualStyleBackColor = true;
            // 
            // txtLyDoTuChoi
            // 
            txtLyDoTuChoi.Location = new Point(66, 311);
            txtLyDoTuChoi.Name = "txtLyDoTuChoi";
            txtLyDoTuChoi.Size = new Size(206, 27);
            txtLyDoTuChoi.TabIndex = 5;
            txtLyDoTuChoi.Text = "Lý do từ chối";
            // 
            // Form_quan_ly_lich_Admin_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtLyDoTuChoi);
            Controls.Add(rdoTuChoi);
            Controls.Add(rdoDuyet);
            Controls.Add(lblTrangThai);
            Controls.Add(btnXacNhan);
            Controls.Add(dgvLichChoDuyet);
            Name = "Form_quan_ly_lich_Admin_";
            Text = "Form_quan_ly_lich_Admin_";
            ((System.ComponentModel.ISupportInitialize)dgvLichChoDuyet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLichChoDuyet;
        private Button btnXacNhan;
        private Label lblTrangThai;
        private RadioButton rdoDuyet;
        private RadioButton rdoTuChoi;
        private TextBox txtLyDoTuChoi;
    }
}