namespace Course_work
{
    partial class Doctor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Doctor));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel1 = new Panel();
            btnLogOut = new Guna.UI2.WinForms.Guna2Button();
            btnMedicine = new Guna.UI2.WinForms.Guna2Button();
            btnDisease = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSalmon;
            panel1.Controls.Add(btnLogOut);
            panel1.Controls.Add(btnMedicine);
            panel1.Controls.Add(btnDisease);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(275, 768);
            panel1.TabIndex = 0;
            // 
            // btnLogOut
            // 
            btnLogOut.BorderRadius = 10;
            btnLogOut.CustomizableEdges = customizableEdges1;
            btnLogOut.DisabledState.BorderColor = Color.DarkGray;
            btnLogOut.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogOut.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogOut.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogOut.FillColor = Color.IndianRed;
            btnLogOut.Font = new Font("Century Gothic", 10F);
            btnLogOut.ForeColor = Color.White;
            btnLogOut.HoverState.BorderColor = Color.Black;
            btnLogOut.HoverState.FillColor = Color.Crimson;
            btnLogOut.Image = (Image)resources.GetObject("btnLogOut.Image");
            btnLogOut.ImageSize = new Size(30, 30);
            btnLogOut.Location = new Point(15, 675);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnLogOut.Size = new Size(245, 68);
            btnLogOut.TabIndex = 5;
            btnLogOut.Text = "Log Out";
            btnLogOut.Click += btnLogOut_Click;
            // 
            // btnMedicine
            // 
            btnMedicine.BorderRadius = 10;
            btnMedicine.CustomizableEdges = customizableEdges3;
            btnMedicine.DisabledState.BorderColor = Color.DarkGray;
            btnMedicine.DisabledState.CustomBorderColor = Color.DarkGray;
            btnMedicine.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnMedicine.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnMedicine.FillColor = Color.IndianRed;
            btnMedicine.Font = new Font("Century Gothic", 10F);
            btnMedicine.ForeColor = Color.White;
            btnMedicine.HoverState.BorderColor = Color.Black;
            btnMedicine.HoverState.FillColor = Color.Crimson;
            btnMedicine.Location = new Point(15, 373);
            btnMedicine.Name = "btnMedicine";
            btnMedicine.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnMedicine.Size = new Size(245, 68);
            btnMedicine.TabIndex = 4;
            btnMedicine.Text = "Медикаменти";
            btnMedicine.Click += btnMedicine_Click;
            // 
            // btnDisease
            // 
            btnDisease.BorderRadius = 10;
            btnDisease.CheckedState.FillColor = Color.RoyalBlue;
            btnDisease.CustomizableEdges = customizableEdges5;
            btnDisease.DisabledState.BorderColor = Color.DarkGray;
            btnDisease.DisabledState.CustomBorderColor = Color.DarkGray;
            btnDisease.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnDisease.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnDisease.FillColor = Color.IndianRed;
            btnDisease.Font = new Font("Century Gothic", 10F);
            btnDisease.ForeColor = Color.White;
            btnDisease.HoverState.BorderColor = Color.Black;
            btnDisease.HoverState.FillColor = Color.Crimson;
            btnDisease.Location = new Point(15, 286);
            btnDisease.Name = "btnDisease";
            btnDisease.PressedColor = Color.Crimson;
            btnDisease.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnDisease.Size = new Size(245, 68);
            btnDisease.TabIndex = 3;
            btnDisease.Text = "Хвороби";
            btnDisease.Click += btnDisease_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(85, 234);
            label1.Name = "label1";
            label1.Size = new Size(103, 33);
            label1.TabIndex = 2;
            label1.Text = "Doctor";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(35, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(203, 219);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Location = new Point(276, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1104, 766);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // Doctor
            // 
            AutoScaleDimensions = new SizeF(14F, 29F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1380, 768);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Doctor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Doctor";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private PictureBox pictureBox1;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Button btnDisease;
        private Guna.UI2.WinForms.Guna2Button btnMedicine;
        private Guna.UI2.WinForms.Guna2Button btnLogOut;
    }
}