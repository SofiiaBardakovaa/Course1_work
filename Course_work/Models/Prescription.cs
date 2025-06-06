using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Models
{
    public class Prescription
    {
        public string PatientName { get; set; }
        public DateTime Date { get; set; }
        public List<PrescriptionItem> Medications { get; set; } = new();

        public override string ToString()
        {
            return $"Пацієнт: {PatientName}, Дата: {Date:dd.MM.yyyy}, Препаратів: {Medications.Count}";
        }
    }

    public class PrescriptionItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{Name} — {Quantity} шт.";
        }
    }
}
