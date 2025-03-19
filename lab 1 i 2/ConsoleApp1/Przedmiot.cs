using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Przedmiot
    {
        public int Wartosc { get; set; }
        public int Waga { get; set; }

        public Przedmiot(int wartosc, int waga)
        {
            Wartosc = wartosc;
            Waga = waga;
        }

        public double Oplacalnosc => (double)Wartosc / Waga;

        public override string ToString()
        {
            return $"Wartosc: {Wartosc}, Waga: {Waga}";
        }
    }
}
