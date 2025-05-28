namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj ziarno generatora przedmiotów: ");
            int ziarno = int.Parse(Console.ReadLine());

            Console.Write("Podaj ilość przedmiotów: ");
            int liczbaPrzedmiotow = int.Parse(Console.ReadLine());

            Problem problem = new Problem(liczbaPrzedmiotow, ziarno, 1, 11);
            Console.WriteLine("Wygenerowane przedmioty: ");
            Console.WriteLine(problem);

            Console.WriteLine("------------------------------------");

            Console.Write("Podaj pojemność plecaka: ");
            int pojemnoscPlecaka = int.Parse(Console.ReadLine());

            Wynik wynik = problem.Rozwiazanie(pojemnoscPlecaka);
            Console.WriteLine("Rozwiązanie: ");
            Console.WriteLine(wynik);
        }
    }
}
