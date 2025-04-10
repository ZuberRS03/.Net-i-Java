using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Country
    {
        public int Id { get; set; }
        public required string Name { get; set; } // np. "Poland"

        public int CurrencyId { get; set; } // klucz obcy
        public Currency Currency { get; set; } = null!;
    }
}
