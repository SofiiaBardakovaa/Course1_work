using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_work.AdminUC;

namespace Course_work
{
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            btnDisease.PerformClick();

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

            uC_D_Disease1.Visible = true;
            uC_D_Disease1.BringToFront();
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

            uC_D_Medicine1.Visible = true;
            uC_D_Medicine1.BringToFront();
        }

        public void ShowMedicineControl()
        {
            uC_D_Disease1.Visible = false;
            uC_D_Medicine1.Visible = true;
            uC_D_Medicine1.BringToFront();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
