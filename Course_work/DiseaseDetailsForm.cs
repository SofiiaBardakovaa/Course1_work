using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Course_work.Models;

namespace Course_work
{
    public partial class DiseaseDetailsForm : Form
    {
        private Disease originalDisease;
        private DiseaseManager diseaseManager;

        public DiseaseDetailsForm(Disease originalDisease, DiseaseManager manager)
        {
            InitializeComponent();
            this.originalDisease = originalDisease;
            this.diseaseManager = manager;

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

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void richTxtShortInf_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSymptoms_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProcedures_TextChanged(object sender, EventArgs e)
        {

        }

        private void dGridViewMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            originalDisease.ShortInfo = richTxtShortInf.Text.Trim();
            originalDisease.Symptoms = txtSymptoms.Text.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList();
            originalDisease.Procedures = txtProcedures.Text.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList();

            List<RecommendedMedication> updatedMeds = new();

            foreach (DataGridViewRow row in dGridViewMedicines.Rows)
            {
                if (row.IsNewRow) continue;

                string name = row.Cells[0].Value?.ToString()?.Trim();
                int quantity = 1;
                int.TryParse(row.Cells[1].Value?.ToString(), out quantity);

                if (!string.IsNullOrWhiteSpace(name))
                {
                    updatedMeds.Add(new RecommendedMedication { Name = name, Quantity = quantity });
                }
            }

            var updatedDisease = new Disease(
                originalDisease.Name,
                richTxtShortInf.Text.Trim(),
                txtSymptoms.Text.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList(),
                txtProcedures.Text.Split(',').Select(p => p.Trim()).Where(p => p != "").ToList(),
                updatedMeds
            );

            diseaseManager.Edit(originalDisease, updatedDisease);

            MessageBox.Show("Зміни збережено.");
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
        $"Ви дійсно хочете видалити хворобу \"{originalDisease.Name}\"?",
        "Підтвердження видалення",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                diseaseManager.Delete(originalDisease);
                this.DialogResult = DialogResult.Yes; 
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string medName = txtAddRecommendedMedicine.Text.Trim();

            if (string.IsNullOrWhiteSpace(medName))
            {
                MessageBox.Show("Введіть назву медикаменту.");
                return;
            }

            // Додаємо новий препарат із початковою кількістю 1
            dGridViewMedicines.Rows.Add(medName, 1);
            txtAddRecommendedMedicine.Text = "";
        }

        private void txtAddRecommendedMedicine_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
