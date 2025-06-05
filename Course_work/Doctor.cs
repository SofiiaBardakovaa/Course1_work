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
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }

        private void btnDisease_Click(object sender, EventArgs e)
        {
            ResetButtonStyles();

            btnDisease.FillColor = Color.Crimson;

            //uC_Info1.Visible = true;
            //uC_Info1.BringToFront();
        }

        private void ResetButtonStyles()
        {
            btnMedicine.FillColor = Color.IndianRed;
            btnDisease.FillColor = Color.IndianRed;
        }


        private void btnMedicine_Click(object sender, EventArgs e)
        {
            ResetButtonStyles();

            btnMedicine.FillColor = Color.Crimson;

            //uC_AddUser1.Visible = true;
            //uC_AddUser1.BringToFront();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            btnDisease.PerformClick();
            //uC_AddUser1.Visible = false;
        }
    }
}
