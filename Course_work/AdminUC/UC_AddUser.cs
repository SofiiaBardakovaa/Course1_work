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

namespace Course_work.AdminUC
{
    public partial class UC_AddUser : UserControl
    {
        private List<Medication> medications = new List<Medication>();
        private Medication selectedMedication = null;
        public UC_AddUser()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                RedrawMedicationList();
                return;
            }

            var directMatches = medications
                .Where(m => m.Name.ToLower().Contains(searchText) ||
                            m.Substitutes.Any(s => s.ToLower().Contains(searchText)))
                .ToList();

            var relatedNames = new HashSet<string>(
                directMatches.Select(m => m.Name.ToLower())
                .Concat(directMatches.SelectMany(m => m.Substitutes.Select(s => s.ToLower())))
            );

            var finalResults = medications
                .Where(m =>
                    relatedNames.Contains(m.Name.ToLower()) ||
                    m.Substitutes.Any(s => relatedNames.Contains(s.ToLower()))
                )
                .Distinct()
                .ToList();

            flowLayoutPanelMedications.Controls.Clear();
            foreach (var med in finalResults)
            {
                AddMedicationToPanel(med);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedMedication != null)
            {
                var result = MessageBox.Show(
                    $"Ви дійсно хочете видалити медикамент \"{selectedMedication.Name}\"?",
                    "Підтвердження видалення",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    medications.Remove(selectedMedication);
                    RedrawMedicationList();
                    selectedMedication = null;
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Виберіть медикамент для видалення через кнопку Edit.");
            }

        }

        private void RedrawMedicationList()
        {
            flowLayoutPanelMedications.Controls.Clear();
            foreach (var med in medications)
            {
                AddMedicationToPanel(med);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string substitutes = txtSubstitutes.Text.Trim();
            int quantity = (int)txtQuantity.Value;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Введіть назву медикаменту");
                return;
            }
            if (txtQuantity.Value <= 0)
            {
                MessageBox.Show("Введіть кількість препарату від 1");
                return;
            }

            if (selectedMedication != null)
            {
                // Редагування
                selectedMedication.Name = name;
                selectedMedication.Quantity = quantity;
                selectedMedication.Substitutes = substitutes
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim()).ToList();

                RedrawMedicationList();
                selectedMedication = null;
            }
            else
            {
                // Додавання нового
                Medication med = new Medication(name, quantity, substitutes);
                medications.Add(med);
                AddMedicationToPanel(med);
            }

            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtName.Text = "";
            txtSubstitutes.Text = "";
            txtQuantity.Value = 0;
        }

        private void AddMedicationToPanel(Medication med)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Width = flowLayoutPanelMedications.Width - 25;
            panel.Height = 100;

            Label lblName = new Label();
            lblName.Text = med.Name;
            lblName.Location = new Point(10, 10);
            lblName.AutoSize = true;

            Label lblQty = new Label();
            lblQty.Text = $"К-сть на складі: {med.Quantity}";
            lblQty.Location = new Point(10, 30);
            lblQty.AutoSize = true;

            Button btnInfo = new Button();
            btnInfo.Text = "Відкрити повну інформацію";
            btnInfo.Location = new Point(10, 60);
            btnInfo.Click += (s, e) => ShowFullInfo(med);

            Button btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Size = new Size(60, 30);
            btnEdit.Location = new Point(panel.Width - 70, 10);
            btnEdit.Click += (s, e) => LoadMedicationForEditing(med);

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblQty);
            panel.Controls.Add(btnInfo);
            panel.Controls.Add(btnEdit);

            flowLayoutPanelMedications.Controls.Add(panel);
        }

        private void LoadMedicationForEditing(Medication med)
        {
            txtName.Text = med.Name;
            txtQuantity.Value = med.Quantity;
            txtSubstitutes.Text = med.GetSubstitutesAsString();
            selectedMedication = med;
        }

        private void ShowFullInfo(Medication med)
        {
            MessageBox.Show($"Назва: {med.Name}\nКількість: {med.Quantity}\nВзаємозамінність: {med.GetSubstitutesAsString()}",
                            "Інформація про медикамент", MessageBoxButtons.OK);
        }

        private void flowLayoutPanelMedications_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSubstitutes_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
