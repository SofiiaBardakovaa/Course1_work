using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Course_work.Models;

namespace Course_work.Managers
{
    public class MedicationManager
    {
        public List<Medication> Medications { get; private set; } = new();
        private readonly string filePath;

        public MedicationManager(string filePath)
        {
            this.filePath = filePath;
            LoadFromFile();
        }

        public void Add(Medication med)
        {
            Medications.Add(med);
            SaveToFile();
        }

        public void Edit(Medication original, Medication updated)
        {
            int index = Medications.IndexOf(original);
            if (index != -1)
            {
                Medications[index] = updated;
                SaveToFile();
            }
        }

        public void Delete(Medication med)
        {
            Medications.Remove(med);
            SaveToFile();
        }

        public List<Medication> Search(string searchText)
        {
            searchText = searchText?.Trim().ToLower() ?? "";

            if (string.IsNullOrWhiteSpace(searchText))
                return new List<Medication>(Medications);

            var directMatches = Medications
                .Where(m => m.Name.ToLower().Contains(searchText) ||
                            m.Substitutes.Any(s => s.ToLower().Contains(searchText)))
                .ToList();

            var relatedNames = new HashSet<string>(
                directMatches.Select(m => m.Name.ToLower())
                .Concat(directMatches.SelectMany(m => m.Substitutes.Select(s => s.ToLower())))
            );

            var finalResults = Medications
                .Where(m =>
                    relatedNames.Contains(m.Name.ToLower()) ||
                    m.Substitutes.Any(s => relatedNames.Contains(s.ToLower()))
                )
                .Distinct()
                .ToList();

            return finalResults;
        }

        public void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Medications, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public void Save()
        {
            SaveToFile();
        }

        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    Medications = JsonSerializer.Deserialize<List<Medication>>(json) ?? new();
                }
                catch
                {
                    Medications = new();
                }
            }
        }
    }

}
