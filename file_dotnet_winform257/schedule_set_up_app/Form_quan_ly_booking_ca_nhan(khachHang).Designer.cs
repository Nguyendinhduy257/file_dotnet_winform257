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
            btnHuyLich.Location = new Point(157, 339);
            btnHuyLich.Name = "btnHuyLich";
            btnHuyLich.Size = new Size(94, 29);
            btnHuyLich.TabIndex = 0;
            btnHuyLich.Text = "Huỷ";
            btnHuyLich.UseVisualStyleBackColor = true;
            // 
            // lblThongBao
            // 
            lblThongBao.AutoSize = true;
            lblThongBao.Location = new Point(444, 343);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(75, 20);
            lblThongBao.TabIndex = 1;
            lblThongBao.Text = "Trạng thái";
            // 
            // dgvLichHen
            // 
            dgvLichHen.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLichHen.Location = new Point(65, 39);
            dgvLichHen.Name = "dgvLichHen";
            dgvLichHen.RowHeadersWidth = 51;
            dgvLichHen.Size = new Size(609, 241);
            dgvLichHen.TabIndex = 2;
            // 
            // Form_My_Booking
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 419);
            Controls.Add(dgvLichHen);
            Controls.Add(lblThongBao);
            Controls.Add(btnHuyLich);
            Margin = new Padding(2, 2, 2, 2);
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