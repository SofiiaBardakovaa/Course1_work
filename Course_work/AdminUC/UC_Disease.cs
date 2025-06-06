using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_work.Models;

namespace Course_work.AdminUC
{
    public partial class UC_Disease : UserControl
    {
        private List<Medication> allMedications;
        private DiseaseManager diseaseManager;
        private Disease selectedDisease = null;

        public UC_Disease()
        {
            InitializeComponent();
            diseaseManager = new DiseaseManager("diseases.json");
            RedrawDiseaseList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string nameSearch = txtSearch.Text.Trim().ToLower();
            string symptomsInput = txtSymptoms.Text.Trim().ToLower();
            string proceduresInput = txtProcedures.Text.Trim().ToLower();

            var symptomFilters = symptomsInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(s => s.Trim()).ToList();
            var procedureFilters = proceduresInput.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                  .Select(p => p.Trim()).ToList();

            var results = diseaseManager.Search(nameSearch, symptomFilters, procedureFilters);

            flowLayoutPanelDiseases.Controls.Clear();
            if (results.Count == 0)
                return;

            foreach (var d in results)
                AddDiseaseToPanel(d);

        }

        private void AddDiseaseToPanel(Disease d)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 580,
                Height = 140
            };

            Label lblName = new Label
            {
                Text = d.Name,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Bold)
            };

            string shortText = d.ShortInfo.Length > 80 ? d.ShortInfo.Substring(0, 80) + "..." : d.ShortInfo;
            Label lblShortInfo = new Label
            {
                Text = shortText,
                Location = new Point(10, 45),
                Size = new Size(530, 40),
                AutoEllipsis = true
            };

            Button btnInfo = new Button
            {
                Text = "Редагувати",
                Location = new Point(10, 80),
                Size = new Size(170, 50),
                FlatStyle = FlatStyle.Flat
            };
            btnInfo.FlatAppearance.BorderColor = Color.Black;
            btnInfo.FlatAppearance.BorderSize = 2;
            btnInfo.Click += (s, e) => ShowFullInfo(d);

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblShortInfo);
            panel.Controls.Add(btnInfo);
            flowLayoutPanelDiseases.Controls.Add(panel);
        }

        private void ShowFullInfo(Disease d)
        {
            var form = new DiseaseDetailsForm(d, diseaseManager);
            form.LoadDisease(d);
            if (form.ShowDialog() == DialogResult.Yes)
            {
                RedrawDiseaseList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedDisease != null)
            {
                var result = MessageBox.Show(
                    $"Ви дійсно хочете видалити хворобу \"{selectedDisease.Name}\"?",
                    "Підтвердження видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    diseaseManager.Delete(selectedDisease);
                    selectedDisease = null;
                    ClearInputFields();
                    RedrawDiseaseList();
                }
            }
            else
            {
                MessageBox.Show("Виберіть хворобу для видалення через кнопку Edit.");
            }

        }

        private void RedrawDiseaseList()
        {
            flowLayoutPanelDiseases.Controls.Clear();
            foreach (var d in diseaseManager.Diseases)
                AddDiseaseToPanel(d);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string shortInfo = txtShortInfo.Text.Trim();
            string symptoms = txtSymptomsAdd.Text.Trim();
            string procedures = txtProceduresAdd.Text.Trim();
            string meds = txtMedicinesAdd.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введіть назву хвороби.");
                return;
            }

            var symptomsList = symptoms.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList();
            var proceduresList = procedures.Split(',').Select(p => p.Trim()).Where(p => p != "").ToList();
            var medList = meds.Split(',').Select(m => m.Trim()).Where(m => m != "").ToList();
            var recommendedMeds = medList
            .Select(m => new RecommendedMedication { Name = m, Quantity = 1 })
            .ToList();

            var newDisease = new Disease(name, shortInfo, symptomsList, proceduresList, recommendedMeds);


            if (selectedDisease != null)
            {
                diseaseManager.Edit(selectedDisease, newDisease);
                selectedDisease = null;
            }
            else
            {
                diseaseManager.Add(newDisease);
            }

            ClearInputFields();
            RedrawDiseaseList();
        }


        private void ClearInputFields()
        {
            txtName.Text = "";
            txtShortInfo.Text = "";
            txtSymptomsAdd.Text = "";
            txtProceduresAdd.Text = "";
            txtMedicinesAdd.Text = "";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtSymptoms_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProcedures_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
