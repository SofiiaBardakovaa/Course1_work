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

namespace Course_work.DoctorUC
{
    public partial class UC_D_Medicine : UserControl
    {
        private MedicationManager medicationManager;
        private List<PrescriptionItem> prescriptionList = new();
        private Dictionary<CheckBox, Medication> selectedCheckBoxes = new();
        private Prescription currentPrescription = new();

        public UC_D_Medicine()
        {
            InitializeComponent();
            medicationManager = new MedicationManager("medications.json");
            RedrawMedicationList();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void LoadPrescriptionFromDisease(Disease disease)
        {
            currentPrescription.Medications.Clear();
            prescriptionList.Clear();
            pnlPresctiptionList.Controls.Clear();

            foreach (var rec in disease.RecommendedMedications)
            {
                var med = medicationManager.Medications.FirstOrDefault(m => m.Name == rec.Name);

                if (med == null)
                {
                    med = medicationManager.Medications.FirstOrDefault(m =>
                        m.Substitutes.Any(sub => sub.Equals(rec.Name, StringComparison.OrdinalIgnoreCase)));

                    if (med != null)
                    {
                        MessageBox.Show($"⚠ Препарат «{rec.Name}» відсутній на складі. Заміна на «{med.Name}».", "Заміна");
                    }
                    else
                    {
                        MessageBox.Show($"⚠ Препарат «{rec.Name}» та його замінники відсутні на складі.");
                        continue;
                    }
                }

                if (rec.Quantity > med.Quantity)
                {
                    MessageBox.Show($"⚠ Недостатньо препарату «{med.Name}»: потрібно {rec.Quantity}, є {med.Quantity}.");
                    continue;
                }

                var item = new PrescriptionItem
                {
                    Name = med.Name,
                    Quantity = rec.Quantity
                };

                prescriptionList.Add(item);
                currentPrescription.Medications.Add(item);
                AddPrescriptionItemToPanel(item);

                med.Quantity -= rec.Quantity;
            }

            medicationManager.SaveToFile();
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

        private void AddMedicationToPanel(Medication med)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = flowLayoutPanelMedications.Width - 25,
                Height = 130
            };

            Label lblName = new Label
            {
                Text = med.Name,
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label lblQty = new Label
            {
                Text = $"К-сть на складі: {med.Quantity}",
                Location = new Point(10, 30),
                AutoSize = true
            };

            CheckBox checkBox = new CheckBox
            {
                Text = "Обрати",
                Location = new Point(10, 60),
                AutoSize = true
            };

            selectedCheckBoxes[checkBox] = med;

            Button btnInfo = new Button
            {
                Text = "Інфо",
                Location = new Point(150, 60),
                Width = 100
            };
            btnInfo.Click += (s, e) => ShowFullInfo(med);

            panel.Controls.Add(lblName);
            panel.Controls.Add(lblQty);
            panel.Controls.Add(checkBox);
            panel.Controls.Add(btnInfo);

            flowLayoutPanelMedications.Controls.Add(panel);
        }

        private void ShowFullInfo(Medication med)
        {
            MessageBox.Show($"Назва: {med.Name}\nКількість: {med.Quantity}\nВзаємозамінність: {med.GetSubstitutesAsString()}",
                            "Інформація про медикамент", MessageBoxButtons.OK);
        }

        private void RedrawMedicationList()
        {
            flowLayoutPanelMedications.Controls.Clear();
            foreach (var med in medicationManager.Medications)
            {
                AddMedicationToPanel(med);
            }
        }

        private void btnNumQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (selectedCheckBoxes.All(cb => !cb.Key.Checked))
                return;

            foreach (var pair in selectedCheckBoxes)
            {
                if (pair.Key.Checked)
                {
                    var med = pair.Value;
                    if (btnNumQuantity.Value > med.Quantity)
                    {
                        MessageBox.Show($"Максимальна доступна кількість для «{med.Name}» — {med.Quantity}.",
                            "Перевищення ліміту", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnNumQuantity.Value = med.Quantity;
                    }
                }
            }
        }

        private void btnAddToList_Click(object sender, EventArgs e)
        {
                   var selectedPairs = selectedCheckBoxes
                .Where(pair => pair.Key.Checked)
                .ToList(); 

            foreach (var pair in selectedPairs)
            {
                CheckBox cb = pair.Key;
                Medication med = pair.Value;
                int qty = (int)btnNumQuantity.Value;

                if (qty > med.Quantity)
                {
                    MessageBox.Show($"Неможливо додати {qty} шт. — на складі лише {med.Quantity}.");
                    continue;
                }

                if (prescriptionList.Any(p => p.Name == med.Name))
                {
                    MessageBox.Show($"Препарат «{med.Name}» вже доданий до рецепту.");
                    continue;
                }

                var item = new PrescriptionItem
                {
                    Name = med.Name,
                    Quantity = qty
                };

                prescriptionList.Add(item);
                currentPrescription.Medications.Add(item);

                med.Quantity -= qty;
                medicationManager.Save(); 

                AddPrescriptionItemToPanel(item);
                cb.Checked = false;
                btnNumQuantity.Value = 1;
            }

            RedrawMedicationList(); 
        }

        private void AddPrescriptionItemToPanel(PrescriptionItem item)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = pnlPresctiptionList.Width - 25,
                Height = 50,
                Margin = new Padding(5)
            };

            Label lbl = new Label
            {
                Text = $"{item.Name} — {item.Quantity} шт.",
                Location = new Point(10, 15),
                AutoSize = true
            };

            Button btnEdit = new Button
            {
                Text = "Обрати",
                Size = new Size(75, 25),
                Location = new Point(panel.Width - 160, 10)
            };
            btnEdit.Click += (s, e) =>
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Введіть нову кількість для {item.Name}:",
                    "Змінити кількість",
                    item.Quantity.ToString());

                if (int.TryParse(input, out int newQty))
                {
                    var med = medicationManager.Medications.FirstOrDefault(m => m.Name == item.Name);
                    if (med != null)
                    {
                        int delta = newQty - item.Quantity;

                        if (med.Quantity >= delta && newQty > 0)
                        {
                            med.Quantity -= delta;
                            item.Quantity = newQty;
                            lbl.Text = $"{item.Name} — {item.Quantity} шт.";
                            medicationManager.Save();
                            RedrawMedicationList();
                        }
                        else
                        {
                            MessageBox.Show("Невірна кількість або перевищує наявну.");
                        }
                    }
                }
            };

            Button btnDelete = new Button
            {
                Text = "Видалити",
                Size = new Size(75, 25),
                Location = new Point(panel.Width - 80, 10)
            };
            btnDelete.Click += (s, e) =>
            {
                prescriptionList.Remove(item);
                currentPrescription.Medications.Remove(item);

                var med = medicationManager.Medications.FirstOrDefault(m => m.Name == item.Name);
                if (med != null)
                {
                    med.Quantity += item.Quantity; 
                    medicationManager.Save();
                }

                pnlPresctiptionList.Controls.Remove(panel);
                RedrawMedicationList();
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(btnEdit);
            panel.Controls.Add(btnDelete);
            pnlPresctiptionList.Controls.Add(panel);
        }

        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
            currentPrescription.PatientName = txtPatientName.Text.Trim();
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            currentPrescription.Date = guna2DateTimePicker1.Value;
        }

        private void btnPrintPrescription_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentPrescription.PatientName))
            {
                MessageBox.Show("Введіть ім’я пацієнта перед друком рецепта.");
                return;
            }

            if (currentPrescription.Medications.Count == 0)
            {
                MessageBox.Show("Додайте хоча б один препарат до рецепта.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Текстові файли (*.txt)|*.txt",
                Title = "Зберегти рецепт",
                FileName = $"Рецепт_{currentPrescription.PatientName}_{DateTime.Now:yyyyMMdd}.txt"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("=== РЕЦЕПТ ===");
                    writer.WriteLine($"Пацієнт: {currentPrescription.PatientName}");
                    writer.WriteLine($"Дійсний до: {currentPrescription.Date:dd.MM.yyyy}");
                    writer.WriteLine();
                    writer.WriteLine("Препарати:");
                    foreach (var med in currentPrescription.Medications)
                    {
                        writer.WriteLine($"- {med}");
                    }
                    writer.WriteLine();
                    writer.WriteLine("Прізвище, ініціали лікаря: Бардакова Софія");
                    writer.WriteLine("Найменування закладу охорони здоров'я: Обласна лікарня");
                    writer.WriteLine("Код за ЄДРПОУ/РНОКПП: 123223212");
                    writer.WriteLine();
                    writer.WriteLine($"Дата створення: {DateTime.Now:dd.MM.yyyy HH:mm}");
                }

                MessageBox.Show("Рецепт успішно збережено!", "Успіх");
                prescriptionList.Clear();
                currentPrescription = new Prescription();
                pnlPresctiptionList.Controls.Clear();
                btnNumQuantity.Value = 1;
                txtPatientName.Text = "";
                guna2DateTimePicker1.Value = DateTime.Now;
                RedrawMedicationList();
            }
        }
    }
}
