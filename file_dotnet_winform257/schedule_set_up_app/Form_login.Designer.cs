namespace schedule_set_up_app
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            textBox_pass = new TextBox();
            textBox_username = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            errorProvider1 = new ErrorProvider(components);
            errorProvider2 = new ErrorProvider(components);
            label3 = new Label();
            label4 = new Label();
            guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(396, 90);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(197, 192);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.DodgerBlue;
            label1.Location = new Point(221, 304);
            label1.Name = "label1";
            label1.Size = new Size(532, 57);
            label1.TabIndex = 1;
            label1.Text = "ĐĂNG NHẬP TÀI KHOẢN";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.LightGray;
            pictureBox2.Cursor = Cursors.Hand;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(219, 556);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(84, 80);
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            pictureBox2.MouseEnter += pictureBox2_MouseEnter;
            pictureBox2.MouseLeave += pictureBox2_MouseLeave;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.LightGray;
            pictureBox3.Cursor = Cursors.Hand;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(221, 437);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(84, 76);
            pictureBox3.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox3.TabIndex = 3;
            pictureBox3.TabStop = false;
            // 
            // textBox_pass
            // 
            textBox_pass.AcceptsTab = true;
            textBox_pass.Cursor = Cursors.IBeam;
            textBox_pass.Font = new Font("Segoe UI", 14F);
            textBox_pass.Location = new Point(309, 577);
            textBox_pass.Name = "textBox_pass";
            textBox_pass.PlaceholderText = "                MẬT KHẨU";
            textBox_pass.Size = new Size(441, 51);
            textBox_pass.TabIndex = 4;
            // 
            // textBox_username
            // 
            textBox_username.AcceptsTab = true;
            textBox_username.Cursor = Cursors.IBeam;
            textBox_username.Font = new Font("Segoe UI", 14F);
            textBox_username.Location = new Point(311, 454);
            textBox_username.Name = "textBox_username";
            textBox_username.PlaceholderText = "          TÊN ĐĂNG NHẬP";
            textBox_username.Size = new Size(442, 51);
            textBox_username.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DodgerBlue;
            panel1.Location = new Point(222, 511);
            panel1.Name = "panel1";
            panel1.Size = new Size(530, 5);
            panel1.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DodgerBlue;
            panel2.Location = new Point(219, 634);
            panel2.Name = "panel2";
            panel2.Size = new Size(533, 5);
            panel2.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(288, 753);
            label2.Name = "label2";
            label2.Size = new Size(252, 38);
            label2.TabIndex = 6;
            label2.Text = "Chưa có tài khoản?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Cursor = Cursors.Hand;
            linkLabel1.Font = new Font("Segoe UI", 12F);
            linkLabel1.Location = new Point(546, 753);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(175, 38);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "sign-up here";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // timer1
            // 
            timer1.Interval = 8000;
            timer1.Tick += timer1_Tick;
            // 
            // guna2Button1
            // 
            guna2Button1.Animated = true;
            guna2Button1.BorderRadius = 10;
            guna2Button1.BorderThickness = 1;
            guna2Button1.Cursor = Cursors.Hand;
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.LightCoral;
            guna2Button1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            guna2Button1.ForeColor = Color.Black;
            guna2Button1.Location = new Point(792, 12);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(172, 70);
            guna2Button1.TabIndex = 5;
            guna2Button1.Text = "Close";
            guna2Button1.Click += guna2Button1_Click;
            // 
            // guna2Button2
            // 
            guna2Button2.Animated = true;
            guna2Button2.BorderRadius = 15;
            guna2Button2.BorderThickness = 2;
            guna2Button2.Cursor = Cursors.Hand;
            guna2Button2.CustomizableEdges = customizableEdges3;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.Font = new Font("Segoe UI", 14F);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.Location = new Point(372, 655);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2Button2.Size = new Size(315, 79);
            guna2Button2.TabIndex = 0;
            guna2Button2.Text = "Login";
            guna2Button2.Click += guna2Button2_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            errorProvider2.ContainerControl = this;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.OrangeRed;
            label3.Location = new Point(311, 407);
            label3.Name = "label3";
            label3.Size = new Size(129, 30);
            label3.TabIndex = 10;
            label3.Text = "Label error 1";
            label3.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.OrangeRed;
            label4.Location = new Point(311, 544);
            label4.Name = "label4";
            label4.Size = new Size(124, 30);
            label4.TabIndex = 11;
            label4.Text = "label error 2";
            label4.Visible = false;
            // 
            // guna2Button3
            // 
            guna2Button3.Animated = true;
            guna2Button3.BorderRadius = 10;
            guna2Button3.BorderThickness = 1;
            guna2Button3.Cursor = Cursors.Hand;
            guna2Button3.CustomizableEdges = customizableEdges5;
            guna2Button3.DisabledState.BorderColor = Color.DarkGray;
            guna2Button3.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button3.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button3.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button3.FillColor = Color.MediumTurquoise;
            guna2Button3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            guna2Button3.ForeColor = Color.Black;
            guna2Button3.Image = Properties.Resources.Assistant;
            guna2Button3.ImageSize = new Size(60, 60);
            guna2Button3.Location = new Point(12, 12);
            guna2Button3.Name = "guna2Button3";
            guna2Button3.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Button3.Size = new Size(189, 70);
            guna2Button3.TabIndex = 12;
            guna2Button3.Text = "Hỗ trợ";
            guna2Button3.Click += guna2Button3_Click;
            // 
            // Form1
            // 
            AcceptButton = guna2Button2;
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 836);
            Controls.Add(guna2Button3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(guna2Button2);
            Controls.Add(guna2Button1);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(textBox_username);
            Controls.Add(textBox_pass);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private TextBox textBox_pass;
        private TextBox textBox_username;
        private Panel panel1;
        private Panel panel2;
        private Label label2;
        private LinkLabel linkLabel1;
        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private ErrorProvider errorProvider1;
        private ErrorProvider errorProvider2;
        private Label label4;
        private Label label3;
        private Guna.UI2.WinForms.Guna2Button guna2Button3;
    }
}
