using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_work
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            ResetButtonStyles();

            btnInfo.FillColor = Color.RoyalBlue;

            uC_Info1.Visible = true;
            uC_Info1.BringToFront();
        }

        private void ResetButtonStyles()
        {
            btnInfo.FillColor = Color.CornflowerBlue;
            btnAddNewUser.FillColor = Color.CornflowerBlue;
            btnViewUser.FillColor = Color.CornflowerBlue;
            btnProfile.FillColor = Color.CornflowerBlue;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            btnInfo.PerformClick();
            uC_AddUser1.Visible = false;
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            ResetButtonStyles();

            btnAddNewUser.FillColor = Color.RoyalBlue;

            uC_AddUser1.Visible = true;
            uC_AddUser1.BringToFront();
        }
    }
}
