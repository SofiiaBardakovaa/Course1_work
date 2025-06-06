using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Course_work.Models;

namespace Course_work.DoctorUC
{
    public partial class UC_D_Disease : UserControl
    {
        private DiseaseManager diseaseManager;
        public UC_D_Disease()
        {
            InitializeComponent();
            diseaseManager = new DiseaseManager("diseases.json");
            RedrawDiseaseList();
        }

        private void txtSymptoms_TextChanged(object sender, EventArgs e)
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
                Width = flowLayoutPanelDiseases.ClientSize.Width - 40,
                Height = 150
            };

            Label lblName = new Label { Text = d.Name, Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 13, FontStyle.Bold) };

            string shortText = d.ShortInfo.Length > 80 ? d.ShortInfo.Substring(0, 80) + "..." : d.ShortInfo;
            Label lblShortInfo = new Label
            {
                Text = shortText,
                Location = new Point(10, 45),
                Size = new Size(panel.Width - 90, 40),
                AutoEllipsis = true
            };

            Button btnInfo = new Button
            {
                Text = "Переглянути",
                Location = new Point(10, 80),
                Size = new Size(200, 60)
            };
            btnInfo.FlatAppearance.BorderColor = Color.Black;
            btnInfo.FlatAppearance.BorderSize = 2;
            btnInfo.FlatStyle = FlatStyle.Flat;
            btnInfo.Click += (s, e) => ShowFullInfo(d);

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblShortInfo);
            panel.Controls.Add(btnInfo);
            flowLayoutPanelDiseases.Controls.Add(panel);
        }

        private void ShowFullInfo(Disease d)
        {
            var doctorForm = (Doctor)this.ParentForm;
            var form = new DoctorDiseaseDetailsForm(d, diseaseManager, doctorForm.uC_D_Medicine1);
            form.LoadDisease(d);
            form.ShowDialog();
        }

        private void RedrawDiseaseList()
        {
            flowLayoutPanelDiseases.Controls.Clear();
            foreach (var d in diseaseManager.Diseases)
                AddDiseaseToPanel(d);
        }

        private void flowLayoutPanelDiseases_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
