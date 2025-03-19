using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ConsoleApp1;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void CapacityBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Zmienna pomocnicza do walidacji
            bool valid = true;

            // Sprawdzenie ziarna
            int ziarno;
            if (!int.TryParse(SeedBox.Text, out ziarno) || ziarno < 1 || ziarno > Int32.MaxValue)
            {
                SeedBox.BackColor = System.Drawing.Color.Red;  // Podœwietlanie na czerwono
                valid = false;
            }
            else
            {
                SeedBox.BackColor = System.Drawing.Color.White; // Przywracanie normalnego koloru
            }

            // Sprawdzenie liczby przedmiotów
            int liczbaPrzedmiotow;
            if (!int.TryParse(NumberItemsBox.Text, out liczbaPrzedmiotow) || liczbaPrzedmiotow < 1)
            {
                NumberItemsBox.BackColor = System.Drawing.Color.Red;  // Podœwietlanie na czerwono
                valid = false;
            }
            else
            {
                NumberItemsBox.BackColor = System.Drawing.Color.White; // Przywracanie normalnego koloru
            }

            // Sprawdzenie pojemnoœci plecaka
            int pojemnoscPlecaka;
            if (!int.TryParse(CapacityBox.Text, out pojemnoscPlecaka) || pojemnoscPlecaka < 1)
            {
                CapacityBox.BackColor = System.Drawing.Color.Red;  // Podœwietlanie na czerwono
                valid = false;
            }
            else
            {
                CapacityBox.BackColor = System.Drawing.Color.White; // Przywracanie normalnego koloru
            }

            if (valid)
            {
                Problem problem = new Problem(liczbaPrzedmiotow, ziarno, 1, 11);

                GenerationBox.Clear();
                foreach (var przedmiot in problem.Przedmioty)
                {
                    GenerationBox.AppendText($"ID: {problem.Przedmioty.IndexOf(przedmiot)} | Wartoœæ: {przedmiot.Wartosc}, Waga: {przedmiot.Waga}\r\n");
                }

                Wynik wynik = problem.Rozwiazanie(pojemnoscPlecaka);

                ResultsBox.AppendText($"Suma wartoœci: {wynik.WartoscCalkowita}\r\n");
                ResultsBox.AppendText($"Suma wag: {wynik.WagaCalkowita}\r\n");

                ResultsBox.AppendText("Lista wybranych przedmiotów:\r\n");
                ResultsBox.AppendText($"ID: ");
                foreach (var id in wynik.WybranePrzedmioty)
                {
                    ResultsBox.AppendText($"{id}, ");
                }

                // Okienko informuj¹ce o zakoñczeniu obliczeñ
                MessageBox.Show("Rozwi¹zanie plecakowe zosta³o obliczone!");
            } else {
                MessageBox.Show("Wprowadzono b³êdne dane. Proszê poprawiæ.");
            }
        }
    }
}
