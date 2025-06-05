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
        public List<RecommendedMedication> RecommendedMedications { get; set; } = new();

        public Disease() { }

        public Disease(string name, string shortInfo, List<string> symptoms, List<string> procedures, List<RecommendedMedication> meds)
        {
            Name = name;
            ShortInfo = shortInfo;
            Symptoms = symptoms;
            Procedures = procedures;
            RecommendedMedications = meds;
        }

        public List<Medication> ResolveMedications(List<Medication> allMeds)
        {
            return allMeds
                .Where(m => RecommendedMedications.Any(rm => rm.Name.Equals(m.Name, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
