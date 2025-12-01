namespace schedule_set_up_app
{
    partial class Form_My_Booking
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
            btnHuyLich = new Button();
            lblThongBao = new Label();
            dgvLichHen = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvLichHen).BeginInit();
            SuspendLayout();
            // 
            // btnHuyLich
            // 
            btnHuyLich.Location = new Point(236, 508);
            btnHuyLich.Margin = new Padding(4, 4, 4, 4);
            btnHuyLich.Name = "btnHuyLich";
            btnHuyLich.Size = new Size(141, 44);
            btnHuyLich.TabIndex = 0;
            btnHuyLich.Text = "Huỷ";
            btnHuyLich.UseVisualStyleBackColor = true;
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.Font = new Font("Segoe UI", 39F);
            lblThongBao.Location = new Point(407, 200);
            lblThongBao.Margin = new Padding(4, 0, 4, 0);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(331, 120);
            lblThongBao.TabIndex = 1;
            lblThongBao.Text = "Khai tử";
            // 
            // dgvLichHen
            // 
            dgvLichHen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichHen.Location = new Point(141, 77);
            dgvLichHen.Margin = new Padding(4, 4, 4, 4);
            dgvLichHen.Name = "dgvLichHen";
            dgvLichHen.RowHeadersWidth = 51;
            dgvLichHen.Size = new Size(914, 362);
            dgvLichHen.TabIndex = 2;
            // 
            // Form_My_Booking
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1118, 628);
            Controls.Add(lblThongBao);
            Controls.Add(dgvLichHen);
            Controls.Add(btnHuyLich);
            Name = "Form_My_Booking";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form_My_Booking";
            ((System.ComponentModel.ISupportInitialize)dgvLichHen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnHuyLich;
        private Label lblThongBao;
        private DataGridView dgvLichHen;
    }
}