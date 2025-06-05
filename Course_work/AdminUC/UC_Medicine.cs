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
using System.IO;
using System.Text.Json;

namespace Course_work.AdminUC
{
    public partial class UC_Medicine : UserControl
    {
        private MedicationManager medicationManager;
        private Medication selectedMedication = null;

        public UC_Medicine()
        {
            InitializeComponent();
            medicationManager = new MedicationManager("medications.json");
            RedrawMedicationList();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();
            var result = medicationManager.Search(searchText);

            flowLayoutPanelMedications.Controls.Clear();
            foreach (var med in result)
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
                    medicationManager.Delete(selectedMedication);
                    selectedMedication = null;
                    ClearInputFields();
                    RedrawMedicationList();
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
            foreach (var med in medicationManager.Medications)
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
            if (quantity <= 0)
            {
                MessageBox.Show("Введіть кількість препарату від 1");
                return;
            }

            var newMed = Medication.FromCsv(name, quantity, substitutes);

            if (selectedMedication != null)
            {
                medicationManager.Edit(selectedMedication, newMed);
                selectedMedication = null;
            }
            else
            {
                medicationManager.Add(newMed);
            }

            txtSearch.Text = "";
            ClearInputFields();
            RedrawMedicationList();
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
