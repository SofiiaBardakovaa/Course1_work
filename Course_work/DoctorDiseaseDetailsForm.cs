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
using Course_work.DoctorUC;
using Course_work.Models;

namespace Course_work
{
    public partial class DoctorDiseaseDetailsForm : Form
    {
        private UC_D_Medicine uc_D_Medicine;
        private Disease originalDisease;
        private DiseaseManager diseaseManager;

        public DoctorDiseaseDetailsForm(Disease originalDisease, DiseaseManager manager, UC_D_Medicine uc_D_Medicine)
        {
            InitializeComponent();
            this.originalDisease = originalDisease;
            this.diseaseManager = manager;
            this.uc_D_Medicine = uc_D_Medicine;

            dGridViewMedicines.Columns.Clear();
            dGridViewMedicines.Columns.Add("Name", "Назва препарату");
            dGridViewMedicines.Columns.Add("Quantity", "Кількість");

            lblName.Text = originalDisease.Name;
            richTxtShortInf.Text = originalDisease.ShortInfo;
            txtSymptoms.Text = string.Join(", ", originalDisease.Symptoms);
            txtProcedures.Text = string.Join(", ", originalDisease.Procedures);

            foreach (var med in originalDisease.RecommendedMedications)
            {
                dGridViewMedicines.Rows.Add(med.Name, med.Quantity);
            }
        }

        public void LoadDisease(Disease d)
        {
            originalDisease = d;

            lblName.Text = d.Name;
            richTxtShortInf.Text = d.ShortInfo;
            txtSymptoms.Text = string.Join(", ", d.Symptoms);
            txtProcedures.Text = string.Join(", ", d.Procedures);

            dGridViewMedicines.Rows.Clear();
            foreach (var med in d.RecommendedMedications)
            {
                dGridViewMedicines.Rows.Add(med.Name, med.Quantity);
            }
        }

        private void btnGotoFormPrescription_Click(object sender, EventArgs e)
        {
            uc_D_Medicine.LoadPrescriptionFromDisease(originalDisease);
            this.Close();
        }
    }
}
