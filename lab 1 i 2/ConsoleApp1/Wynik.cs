using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Wynik
    {
        public List<int> WybranePrzedmioty { get; set; }
        public int WartoscCalkowita { get; set; }
        public int WagaCalkowita { get; set; }

        public Wynik()
        {
            WybranePrzedmioty = new List<int>();
            WartoscCalkowita = 0;
            WagaCalkowita = 0;
        }

        
        public void DodajPrzedmiot(int indeks, Przedmiot przedmiot)
        {
            WybranePrzedmioty.Add(indeks);
            WartoscCalkowita += przedmiot.Wartosc;
            WagaCalkowita += przedmiot.Waga;
        }

        public override string ToString()
        {
            return $"Wybrane przedmioty: {string.Join(", ", WybranePrzedmioty)}\n" +
               $"Sumaryczna wartość: {WartoscCalkowita}, Sumaryczna waga: {WagaCalkowita}";
        }
    }
}
