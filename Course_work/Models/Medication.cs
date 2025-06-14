using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Course_work.Models
{
    public class Medication
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public List<string> Substitutes { get; set; }

        public static Medication FromCsv(string name, int quantity, string substitutesCsv)
        {
            return new Medication
            {
                Name = name,
                Quantity = quantity,
                Substitutes = substitutesCsv
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .ToList()
            };
        }

        public Medication() { }

        public string GetSubstitutesAsString()
        {
            return string.Join(", ", Substitutes);
        }

        public override string ToString()
        {
            return $"{Name} (на складі: {Quantity})";
        }
    }
}
