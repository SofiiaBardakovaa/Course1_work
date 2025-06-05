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
    public partial class UC_Medicine : UserControl
    {
        private List<Disease> diseases = new List<Disease>();
        private Disease selectedDisease = null;
        private readonly string dataFilePath = "diseases.json";

        public UC_Medicine()
        {
            InitializeComponent();
            LoadDiseasesFromFile();
        }

        private void UC_Info_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtSymptomes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProcedures_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

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

            var results = diseases.Where(d =>
                (string.IsNullOrWhiteSpace(nameSearch) || d.Name.ToLower().Contains(nameSearch)) &&
                (symptomFilters.Count == 0 || symptomFilters.All(f => d.Symptoms.Any(s => s.ToLower().Contains(f)))) &&
                (procedureFilters.Count == 0 || procedureFilters.All(f => d.Procedures.Any(p => p.ToLower().Contains(f))))
            ).ToList();

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
                    Width = flowLayoutPanelDiseases.ClientSize.Width - 25,
                    Height = 120
                };

                Label lblName = new Label
                {
                    Text = d.Name,
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                string shortText = d.ShortInfo.Length > 80 ? d.ShortInfo.Substring(0, 80) + "..." : d.ShortInfo;
                Label lblShortInfo = new Label
                {
                    Text = shortText,
                    Location = new Point(10, 30),
                    Size = new Size(panel.Width - 90, 40),
                    AutoEllipsis = true
                };

                Button btnInfo = new Button
                {
                    Text = "Інфо",
                    Location = new Point(10, 80),
                    Size = new Size(100, 30)
                };
                btnInfo.Click += (s, e) => ShowFullInfo(d);

                Button btnEdit = new Button
                {
                    Text = "Edit",
                    Location = new Point(panel.Width - 70, 10),
                    Size = new Size(60, 30)
                };
            btnEdit.Click += (s, e) => LoadDiseaseForEditing(d);

            panel.Controls.Add(lblName);
                panel.Controls.Add(lblShortInfo);
                panel.Controls.Add(btnInfo);
                panel.Controls.Add(btnEdit);

                flowLayoutPanelDiseases.Controls.Add(panel);
        }

        private void LoadDiseaseForEditing(Disease d)
        {
            selectedDisease = d;
            txtName.Text = d.Name;
            txtShortInfo.Text = d.ShortInfo;
            txtSymptomsAdd.Text = d.GetSymptomsAsString();
            txtProceduresAdd.Text = d.GetProceduresAsString();
            txtMedicinesAdd.Text = d.GetMedsAsString();
        }


        private void ShowFullInfo(Disease d)
        {
            MessageBox.Show(
                $"Назва: {d.Name}\nКоротка інфо: {d.ShortInfo}\nСимптоми: {d.GetSymptomsAsString()}\nПроцедури: {d.GetProceduresAsString()}\nЛіки: {d.GetMedsAsString()}",
                "Інформація про хворобу",
                MessageBoxButtons.OK
            );
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanelDiseases_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtShortInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSymptomsAdd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProceduresAdd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMedicinesAdd_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedDisease != null)
            {
                var result = MessageBox.Show(
                    $"Ви дійсно хочете видалити хворобу \"{selectedDisease.Name}\"?",
                    "Підтвердження видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    diseases.Remove(selectedDisease);
                    RedrawDiseaseList();
                    selectedDisease = null;
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Виберіть хворобу для видалення через кнопку Edit.");
            }
            SaveDiseasesToFile();

        }

        private void RedrawDiseaseList()
        {
            flowLayoutPanelDiseases.Controls.Clear();
            foreach (var d in diseases)
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

            if (selectedDisease != null)
            {
                // Редагування існуючої хвороби
                selectedDisease.Name = name;
                selectedDisease.ShortInfo = shortInfo;
                selectedDisease.Symptoms = symptoms.Split(',').Select(s => s.Trim()).Where(s => s != "").ToList();
                selectedDisease.Procedures = procedures.Split(',').Select(p => p.Trim()).Where(p => p != "").ToList();
                selectedDisease.RecommendedMedications = meds.Split(',').Select(m => m.Trim()).Where(m => m != "").ToList();

                selectedDisease = null;
            }
            else
            {
                // Додавання нової хвороби
                Disease newDisease = new Disease(name, shortInfo, symptoms, procedures, meds);
                diseases.Add(newDisease);
            }

            SaveDiseasesToFile();
            RedrawDiseaseList();
            ClearInputFields();
        }

        
        private void ClearInputFields()
        {
            txtName.Text = "";
            txtShortInfo.Text = "";
            txtSymptomsAdd.Text = "";
            txtProceduresAdd.Text = "";
            txtMedicinesAdd.Text = "";
        }

        private void SaveDiseasesToFile()
        {
            var json = JsonSerializer.Serialize(diseases, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(dataFilePath, json);
        }

        private void LoadDiseasesFromFile()
        {
            if (File.Exists(dataFilePath))
            {
                try
                {
                    string json = File.ReadAllText(dataFilePath);
                    diseases = JsonSerializer.Deserialize<List<Disease>>(json) ?? new List<Disease>();
                }
                catch
                {
                    MessageBox.Show("Не вдалося завантажити дані про хвороби.");
                    diseases = new List<Disease>();
                }
            }
            else
            {
                diseases = new List<Disease>();
            }

            RedrawDiseaseList();
        }
    }
}
