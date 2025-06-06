namespace Course_work
{
    partial class Admin
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel1 = new Panel();
            btnLogOut = new Guna.UI2.WinForms.Guna2Button();
            btnAddNewUser = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            btnInfo = new Guna.UI2.WinForms.Guna2Button();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            uC_AddUser1 = new Course_work.AdminUC.UC_Medicine();
            uC_Info1 = new Course_work.AdminUC.UC_Disease();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(components);
            guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightCyan;
            panel1.Controls.Add(btnLogOut);
            panel1.Controls.Add(btnAddNewUser);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnInfo);
            panel1.Controls.Add(pictureBox1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(275, 769);
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
            btnLogOut.FillColor = Color.CornflowerBlue;
            btnLogOut.Font = new Font("Century Gothic", 10F);
            btnLogOut.ForeColor = Color.White;
            btnLogOut.HoverState.BorderColor = Color.Black;
            btnLogOut.HoverState.FillColor = Color.RoyalBlue;
            btnLogOut.Image = (Image)resources.GetObject("btnLogOut.Image");
            btnLogOut.ImageSize = new Size(30, 30);
            btnLogOut.Location = new Point(12, 677);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnLogOut.Size = new Size(245, 68);
            btnLogOut.TabIndex = 4;
            btnLogOut.Text = "Log Out";
            btnLogOut.Click += btnLogOut_Click;
            // 
            // btnAddNewUser
            // 
            btnAddNewUser.BorderRadius = 10;
            btnAddNewUser.CustomizableEdges = customizableEdges3;
            btnAddNewUser.DisabledState.BorderColor = Color.DarkGray;
            btnAddNewUser.DisabledState.CustomBorderColor = Color.DarkGray;
            btnAddNewUser.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnAddNewUser.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnAddNewUser.FillColor = Color.CornflowerBlue;
            btnAddNewUser.Font = new Font("Century Gothic", 10F);
            btnAddNewUser.ForeColor = Color.White;
            btnAddNewUser.HoverState.BorderColor = Color.Black;
            btnAddNewUser.HoverState.FillColor = Color.RoyalBlue;
            btnAddNewUser.Location = new Point(12, 349);
            btnAddNewUser.Name = "btnAddNewUser";
            btnAddNewUser.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnAddNewUser.Size = new Size(245, 68);
            btnAddNewUser.TabIndex = 1;
            btnAddNewUser.Text = "Медикаменти";
            btnAddNewUser.Click += btnAddNewUser_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Cambria", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(85, 198);
            label1.Name = "label1";
            label1.Size = new Size(101, 33);
            label1.TabIndex = 1;
            label1.Text = "Admin";
            // 
            // btnInfo
            // 
            btnInfo.BorderRadius = 10;
            btnInfo.CheckedState.FillColor = Color.RoyalBlue;
            btnInfo.CustomizableEdges = customizableEdges5;
            btnInfo.DisabledState.BorderColor = Color.DarkGray;
            btnInfo.DisabledState.CustomBorderColor = Color.DarkGray;
            btnInfo.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnInfo.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnInfo.FillColor = Color.CornflowerBlue;
            btnInfo.Font = new Font("Century Gothic", 10F);
            btnInfo.ForeColor = Color.White;
            btnInfo.HoverState.BorderColor = Color.Black;
            btnInfo.HoverState.FillColor = Color.RoyalBlue;
            btnInfo.Location = new Point(12, 258);
            btnInfo.Name = "btnInfo";
            btnInfo.PressedColor = Color.RoyalBlue;
            btnInfo.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnInfo.Size = new Size(245, 68);
            btnInfo.TabIndex = 0;
            btnInfo.Text = "Хвороби";
            btnInfo.Click += btnInfo_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(31, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(214, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(uC_AddUser1);
            panel2.Controls.Add(uC_Info1);
            panel2.Location = new Point(278, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(1102, 766);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // uC_AddUser1
            // 
            uC_AddUser1.BackColor = Color.White;
            uC_AddUser1.Font = new Font("Microsoft Sans Serif", 12F);
            uC_AddUser1.Location = new Point(0, 0);
            uC_AddUser1.Margin = new Padding(4, 3, 4, 3);
            uC_AddUser1.Name = "uC_AddUser1";
            uC_AddUser1.Size = new Size(1102, 766);
            uC_AddUser1.TabIndex = 1;
            uC_AddUser1.Load += uC_AddUser1_Load;
            // 
            // uC_Info1
            // 
            uC_Info1.BackColor = Color.White;
            uC_Info1.Location = new Point(3, 3);
            uC_Info1.Name = "uC_Info1";
            uC_Info1.Size = new Size(1102, 766);
            uC_Info1.TabIndex = 0;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.TargetControl = panel2;
            // 
            // guna2Elipse2
            // 
            guna2Elipse2.TargetControl = panel2;
            // 
            // guna2Elipse3
            // 
            guna2Elipse3.TargetControl = panel2;
            // 
            // Admin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1380, 768);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Admin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin";
            Load += Admin_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnAddNewUser;
        private Guna.UI2.WinForms.Guna2Button btnInfo;
        private Guna.UI2.WinForms.Guna2Button btnLogOut;
        private AdminUC.UC_Disease uC_Info1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse3;
        private AdminUC.UC_Medicine uC_AddUser1;
    }
}