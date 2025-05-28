using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Currency
    {
        public int Id { get; set; } // klucz główny
        public required string Code { get; set; } // np. "PLN"
        public required decimal Rate { get; set; } // np. 3.99
        public DateTime Date { get; set; } // z jakiej daty jest kurs

        public List<Country> Countries { get; set; } = new();
    }
}
