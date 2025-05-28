using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ExchangeRatesService
    {
        public HttpClient client;
        public async Task GetData(String date)
        {
            var appId = "89e9faca49514c8bb3c92b9541261253";

            client = new HttpClient();
            var url = $"https://openexchangerates.org/api/historical/{date}.json?app_id={appId}&base=USD";
            
            string response = await client.GetStringAsync(url);

            // Wypisz otrzymane dane z API
            //Console.WriteLine("Odebrano dane z API:\n" + response);

            ExchangeData? data = JsonSerializer.Deserialize<ExchangeData>(response);

            if (data?.Rates == null)
            {
                Console.WriteLine("Błąd: brak danych kursów walut.");
                return;
            }

            //// Wypisz kursy walut po deserjalizacji
            //foreach (var kvp in data?.Rates!)
            //{
            //    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            //}

            // Inicjalizacja bazy danych
            using var context = new ExchangeDbContext();

            string[] selectedCodes = { "USD", "EUR", "PLN", "CHF", "JPY", "GBP", "CAD", "AUD", "SEK", "NOK" };
            var dateParsed = data.TimestampToDateTime();

            foreach (var kvp in data.Rates)
            {
                if (!selectedCodes.Contains(kvp.Key)) continue;

                bool exists = context.Currencies.Any(c => c.Code == kvp.Key && c.Date == dateParsed);
                if (exists) continue;

                var currency = new Currency
                {
                    Code = kvp.Key,
                    Rate = kvp.Value,
                    Date = dateParsed
                };

                context.Currencies.Add(currency);
            }

            context.SaveChanges();

            // Dodaj kraje, jeśli jeszcze ich nie ma
            if (!context.Countries.Any())
            {
                var usd = context.Currencies.FirstOrDefault(c => c.Code == "USD" && c.Date == dateParsed);
                var eur = context.Currencies.FirstOrDefault(c => c.Code == "EUR" && c.Date == dateParsed);
                var pln = context.Currencies.FirstOrDefault(c => c.Code == "PLN" && c.Date == dateParsed);

                if (usd != null && eur != null && pln != null)
                {
                    context.Countries.AddRange(
                        new Country { Name = "United States", CurrencyId = usd.Id },
                        new Country { Name = "Germany", CurrencyId = eur.Id },
                        new Country { Name = "Poland", CurrencyId = pln.Id },
                        new Country { Name = "France", CurrencyId = eur.Id }
                    );

                    context.SaveChanges();
                }
            }
        }

        public void PrintCountriesByCurrency(string currencyCode)
        {
            using var context = new ExchangeDbContext();

            var countries = context.Countries
                .Include(c => c.Currency)
                .Where(c => c.Currency.Code == currencyCode.ToUpper())
                .ToList();

            if (!countries.Any())
            {
                Console.WriteLine($"Brak krajów używających waluty {currencyCode}");
                return;
            }

            Console.WriteLine($"\nKraje używające waluty {currencyCode.ToUpper()}:");
            foreach (var country in countries)
            {
                Console.WriteLine($"- {country.Name}");
            }
        }

        public async Task GetCurrencyRateForCountry(string countryName, string date)
        {
            var targetDate = DateTime.Parse(date);

            using var context = new ExchangeDbContext();

            // Sprawdź, czy kraj istnieje
            var country = context.Countries
                .Include(c => c.Currency)
                .FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());

            if (country == null)
            {
                Console.WriteLine($"Błąd: kraj '{countryName}' nie istnieje w bazie.");
                return;
            }

            // Sprawdź, czy kursy z tej daty już są
            bool hasRates = context.Currencies.Any(c => c.Date.Date == targetDate.Date);

            if (!hasRates)
            {
                Console.WriteLine($"Brak danych kursowych z {date} – pobieram z API...");

                await GetData(date); // pobiera dane i zapisuje do bazy

                // Odśwież kontekst po dodaniu danych
                using var freshContext = new ExchangeDbContext();

                var freshCountry = freshContext.Countries
                    .Include(c => c.Currency)
                    .FirstOrDefault(c => c.Name.ToLower() == countryName.ToLower());

                if (freshCountry == null)
                {
                    Console.WriteLine("Błąd krytyczny: kraj zniknął z bazy po pobraniu danych.");
                    return;
                }

                var freshRate = freshContext.Currencies
                    .FirstOrDefault(c => c.Code == freshCountry.Currency.Code && c.Date.Date == targetDate.Date);

                if (freshRate == null)
                {
                    Console.WriteLine($"Nie udało się znaleźć kursu dla waluty {freshCountry.Currency.Code} z dnia {date}.");
                    return;
                }

                Console.WriteLine($"\nKraj: {freshCountry.Name}\nWaluta: {freshRate.Code}\nData: {freshRate.Date:yyyy-MM-dd}\nKurs: {freshRate.Rate}");
                return;
            }

            // Dane są już w bazie — szukaj bez pobierania
            var rate = context.Currencies
                .FirstOrDefault(c => c.Code == country.Currency.Code && c.Date.Date == targetDate.Date);

            if (rate == null)
            {
                Console.WriteLine($"Nie udało się znaleźć kursu dla waluty {country.Currency.Code} z dnia {date}.");
                return;
            }

            Console.WriteLine($"\nKraj: {country.Name}\nWaluta: {rate.Code}\nData: {rate.Date:yyyy-MM-dd}\nKurs: {rate.Rate}");
        }

        public void PrintDatabaseContents()
        {
            using var context = new ExchangeDbContext();

            Console.WriteLine("=== Waluty w bazie ===");
            foreach (var currency in context.Currencies.OrderBy(c => c.Code))
            {
                Console.WriteLine($"{currency.Code} - {currency.Rate} ({currency.Date:yyyy-MM-dd})");
            }

            Console.WriteLine("\n=== Kraje w bazie ===");
            foreach (var country in context.Countries.Include(c => c.Currency))
            {
                Console.WriteLine($"{country.Name} → {country.Currency.Code}");
            }
        }

        public void ClearDatabase()
        {
            using var context = new ExchangeDbContext();

            context.Countries.RemoveRange(context.Countries);
            context.Currencies.RemoveRange(context.Currencies);

            context.SaveChanges();

            Console.WriteLine("Baza danych została wyczyszczona.");
        }

        public void AddCountryToCurrency(string countryName, string currencyCode, DateTime date)
        {
            using var context = new ExchangeDbContext();

            // Znajdź walutę po kodzie i dacie
            var currency = context.Currencies
                .FirstOrDefault(c => c.Code == currencyCode.ToUpper() && c.Date.Date == date.Date);

            if (currency == null)
            {
                Console.WriteLine($"Nie znaleziono waluty {currencyCode.ToUpper()} z dnia {date:yyyy-MM-dd} w bazie.");
                return;
            }

            // Sprawdź, czy kraj już istnieje
            bool exists = context.Countries.Any(c => c.Name.ToLower() == countryName.ToLower());

            if (exists)
            {
                Console.WriteLine($"Kraj '{countryName}' już istnieje w bazie.");
                return;
            }

            // Dodaj kraj
            var country = new Country
            {
                Name = countryName,
                CurrencyId = currency.Id
            };

            context.Countries.Add(country);
            context.SaveChanges();

            Console.WriteLine($"Dodano kraj '{countryName}' powiązany z walutą {currency.Code} ({currency.Date:yyyy-MM-dd})");
        }
    }
}
