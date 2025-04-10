using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

//Do bazy dodać jakieś pole od siebie, np. Dziesięć nazw krajów żeby po prostu wyszła jakaś relacja
// API id: 89e9faca49514c8bb3c92b9541261253
namespace ConsoleApp
{

    internal class Program
    {
        static void Main(string[] args)
        {
            //Testowe wywołania funkcji

            var service = new ExchangeRatesService();

            service.ClearDatabase();

            string dateArg = "2024-03-26";
            string currencyCode = "EUR";

            service.GetData(dateArg).Wait();
            service.PrintDatabaseContents();

            Console.WriteLine("----------------------------------------");
            service.PrintCountriesByCurrency(currencyCode);

            Console.WriteLine("----------------------------------------");
            string dateArg2 = "2024-03-27";
            string countryName = "Poland";
            service.GetCurrencyRateForCountry(countryName, dateArg2).Wait();
            service.PrintDatabaseContents();

            Console.WriteLine("----------------------------------------");
            service.AddCountryToCurrency("Italy", "EUR", new DateTime(2024, 03, 27));
            service.PrintDatabaseContents();



            //================================================================================================

            //var service = new ExchangeRatesService();

            //if (args.Length == 0)
            //{
            //    // Domyślne działanie – pobieramy dane z dzisiejszej daty
            //    string date = DateTime.Today.ToString("yyyy-MM-dd");
            //    Console.WriteLine("Nie podano argumentów – pobieram dane z dzisiaj.");
            //    service.GetData(date).Wait();
            //}
            //else if (args.Length == 1)
            //{
            //    switch (args[0])
            //    {
            //        case "--showAll":
            //            service.PrintDatabaseContents();
            //            break;
            //        case "--clear":
            //            service.ClearDatabase();
            //            break;
            //        default:
            //            // Spróbuj sparsować jako datę
            //            if (DateTime.TryParse(args[0], out var parsedDate))
            //            {
            //                service.GetData(parsedDate.ToString("yyyy-MM-dd")).Wait();
            //            }
            //            else
            //            {
            //                Console.WriteLine("❌ Nieznany argument lub błędna data.");
            //            }
            //            break;
            //    }
            //}
            //else if (args.Length == 2 && args[0] == "--countriesByCurrency")
            //{
            //    string currencyCode = args[1];
            //    service.PrintCountriesByCurrency(currencyCode);
            //}
            //else if (args.Length == 3 && args[0] == "--rateForCountry")
            //{
            //    string countryName = args[1];
            //    string date = args[2];
            //    service.GetCurrencyRateForCountry(countryName, date).Wait();
            //}
            //else
            //{
            //    Console.WriteLine("❌ Niepoprawne argumenty. Przykłady użycia:");
            //    Console.WriteLine("  ConsoleApp.exe");
            //    Console.WriteLine("  ConsoleApp.exe 2024-03-26");
            //    Console.WriteLine("  ConsoleApp.exe --showAll");
            //    Console.WriteLine("  ConsoleApp.exe --clear");
            //    Console.WriteLine("  ConsoleApp.exe --countriesByCurrency EUR");
            //    Console.WriteLine("  ConsoleApp.exe --rateForCountry Poland 2024-03-27");
            //}
        }
    }
}
