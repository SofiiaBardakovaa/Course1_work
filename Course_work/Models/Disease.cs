using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Models
{
    public class Disease
    {
        public string Name { get; set; }
        public string ShortInfo { get; set; }
        public List<string> Symptoms { get; set; }
        public List<string> Procedures { get; set; }
        public List<string> RecommendedMedications { get; set; }

        public Disease() { }
        public Disease(string name, string shortInfo, string symptomsCsv, string proceduresCsv, string medsCsv)
        {
            Name = name;
            ShortInfo = shortInfo;
            Symptoms = ParseList(symptomsCsv);
            Procedures = ParseList(proceduresCsv);
            RecommendedMedications = ParseList(medsCsv);
        }

        private List<string> ParseList(string csv)
        {
            return csv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => s.Trim())
                      .ToList();
        }

        public string GetSymptomsAsString() => string.Join(", ", Symptoms);
        public string GetProceduresAsString() => string.Join(", ", Procedures);
        public string GetMedsAsString() => string.Join(", ", RecommendedMedications);
    }
}
