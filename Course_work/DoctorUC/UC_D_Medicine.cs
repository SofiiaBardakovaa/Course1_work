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
using Course_work.Managers;
using Course_work.Models;

namespace Course_work.DoctorUC
{
    public partial class UC_D_Medicine : UserControl
    {
        private MedicationManager medicationManager;
        private List<PrescriptionItem> prescriptionList = new();
        private Dictionary<CheckBox, Medication> selectedCheckBoxes = new();
        private Prescription currentPrescription = new();
        private Dictionary<PrescriptionItem, Label> prescriptionLabels = new();


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

                int availableQty = med.Quantity;
                if (availableQty == 0)
                {
                    MessageBox.Show($"⚠ Препарат «{med.Name}» закінчився на складі.");
                    continue;
                }

                int finalQty = Math.Min(rec.Quantity, availableQty);

                if (finalQty < rec.Quantity)
                {
                    MessageBox.Show($"⚠ Недостатньо препарату «{med.Name}»: потрібно {rec.Quantity}, буде додано {finalQty}.");
                }

                var existingItem = prescriptionList.FirstOrDefault(p => p.Name == med.Name);
                if (existingItem != null)
                {
                    existingItem.Quantity += finalQty;

                    if (prescriptionLabels.TryGetValue(existingItem, out Label lbl))
                    {
                        lbl.Text = $"{existingItem.Name} — {existingItem.Quantity} шт.";
                    }
                }
                else
                {
                    var item = new PrescriptionItem
                    {
                        Name = med.Name,
                        Quantity = finalQty
                    };

                    prescriptionList.Add(item);
                    currentPrescription.Medications.Add(item);
                    AddPrescriptionItemToPanel(item);
                }

                med.Quantity -= finalQty;
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
            if (med.Quantity == 0)
                return; 

            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = flowLayoutPanelMedications.Width - 35,
                Height = 130
            };

            Label lblName = new Label
            {
                Text = med.Name,
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 13, FontStyle.Bold)
            };

            Label lblQty = new Label
            {
                Text = $"К-сть на складі: {med.Quantity}",
                Location = new Point(10, 45),
                AutoSize = true
            };

            CheckBox checkBox = new CheckBox
            {
                Text = "Обрати",
                Location = new Point(20, 85),
                AutoSize = true
            };

            Medication currentMed = med;
            selectedCheckBoxes[checkBox] = currentMed;
 
            checkBox.CheckedChanged += (s, e) =>
            {
                if (checkBox.Checked)
                {
                    Console.WriteLine($"Вибрано: {currentMed.Name}");
                }
            };

            Button btnInfo = new Button
            {
                Text = "Інфо",
                Location = new Point(300, 50),
                Width = 200,
                Height = 60
            };

            btnInfo.FlatAppearance.BorderColor = Color.Black;
            btnInfo.FlatAppearance.BorderSize = 2;
            btnInfo.FlatStyle = FlatStyle.Flat;
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
            selectedCheckBoxes.Clear();

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

            int qty = (int)btnNumQuantity.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Кількість має бути більшою за 0.");
                return;
            }

            foreach (var pair in selectedPairs)
            {
                CheckBox cb = pair.Key;
                Medication selectedMed = pair.Value; // створюємо окрему змінну

                if (qty > selectedMed.Quantity)
                {
                    MessageBox.Show($"Неможливо додати {qty} шт. — на складі лише {selectedMed.Quantity}.");
                    continue;
                }

                if (prescriptionList.Any(p => p.Name == selectedMed.Name))
                {
                    MessageBox.Show($"Препарат «{selectedMed.Name}» вже доданий до рецепту.");
                    continue;
                }

                var item = new PrescriptionItem
                {
                    Name = selectedMed.Name,
                    Quantity = qty
                };

                prescriptionList.Add(item);
                currentPrescription.Medications.Add(item);

                selectedMed.Quantity -= qty;
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
                Width = pnlPresctiptionList.Width - 40,
                Height = 100,
                Margin = new Padding(5)
            };

            Label lbl = new Label
            {
                Text = $"{item.Name} — {item.Quantity} шт.",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            prescriptionLabels[item] = lbl;

            Button btnEdit = new Button
            {
                Text = "Обрати",
                Size = new Size(110, 40),
                Location = new Point(10, 44)
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
                Size = new Size(110, 40),
                Location = new Point(130, 44)
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
                prescriptionLabels.Remove(item);
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
