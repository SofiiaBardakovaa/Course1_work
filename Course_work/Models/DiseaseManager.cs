using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Course_work.Models
{
    public class DiseaseManager
    {
        public List<Disease> Diseases { get; private set; } = new();
        private readonly string filePath;

        public DiseaseManager(string filePath)
        {
            this.filePath = filePath;
            LoadFromFile();
        }

        public void Add(Disease disease)
        {
            Diseases.Add(disease);
            SaveToFile();
        }

        public void Edit(Disease original, Disease updated)
        {
            int index = Diseases.IndexOf(original);
            if (index != -1)
            {
                Diseases[index] = updated;
                SaveToFile();
            }
        }

        public void Delete(Disease disease)
        {
            Diseases.Remove(disease);
            SaveToFile();
        }

        public List<Disease> Search(string name, List<string> symptoms, List<string> procedures)
        {
            name = name?.ToLower() ?? "";
            return Diseases.Where(d =>
                (string.IsNullOrWhiteSpace(name) || d.Name.ToLower().Contains(name)) &&
                (symptoms.Count == 0 || symptoms.All(f => d.Symptoms.Any(s => s.ToLower().Contains(f)))) &&
                (procedures.Count == 0 || procedures.All(f => d.Procedures.Any(p => p.ToLower().Contains(f))))
            ).ToList();
        }

        public void SaveToFile()
        {
            var json = JsonSerializer.Serialize(Diseases, new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            });
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    Diseases = JsonSerializer.Deserialize<List<Disease>>(json, new JsonSerializerOptions
                    {
                        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                    }) ?? new();
                }
                catch
                {
                    Diseases = new();
                }
            }
        }
    }





}
