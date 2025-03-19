using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("TestProject1"), InternalsVisibleTo("WinFormsApp")]
namespace ConsoleApp1
{
    class Problem
    {
        public int LiczbaPrzedmiotow { get;  set; }
        public List<Przedmiot> Przedmioty { get; set; }

        public Problem(int liczbaPrzedmiotow, int ziarno, int minGranica, int maxGranica)
        {
            LiczbaPrzedmiotow = liczbaPrzedmiotow;
            Przedmioty = new List<Przedmiot>();

            Random random = new Random(ziarno);
            for (int i = 0; i < liczbaPrzedmiotow; i++)
            {
                int wartosc = random.Next(minGranica, maxGranica);
                int waga = random.Next(minGranica, maxGranica);
                Przedmioty.Add(new Przedmiot(wartosc, waga));
            }
        }

        public Wynik Rozwiazanie(int pojemnoscPlecaka)
        {
            // Sortowanie przedmiotów po opłacalności (wartość/waga)
            var posortowanePrzedmioty = Przedmioty
            .Select((przedmiot, index) => new { Przedmiot = przedmiot, Index = index })
            .OrderByDescending(p => p.Przedmiot.Oplacalnosc)
            .ToList();

            Wynik wynik = new Wynik();
            int aktualnaWaga = 0;

            foreach (var element in posortowanePrzedmioty)
            {
                if (aktualnaWaga + element.Przedmiot.Waga <= pojemnoscPlecaka)
                {
                    wynik.DodajPrzedmiot(element.Index, element.Przedmiot);
                    aktualnaWaga += element.Przedmiot.Waga;
                }
            }

            return wynik;

        }

        public override string ToString()
        {
            string wynik = $"Liczba przedmiotow: {LiczbaPrzedmiotow}\n";
            for (int i = 0; i < Przedmioty.Count; i++)
            {
                wynik += $"[{i}] {Przedmioty[i]}\n";
            }
            return wynik;
        }
    }
}
