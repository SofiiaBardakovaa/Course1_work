using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_work.Models
{
    public class RecommendedMedication
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public override string ToString() => $"{Name} (кількість: {Quantity})";
    }
}
