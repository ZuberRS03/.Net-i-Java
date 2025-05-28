using ExchangeMauiApp.Core;

namespace ExchangeMauiApp;

public partial class MainPage : ContentPage
{
    private readonly ExchangeRatesService _service;

    public MainPage(ExchangeRatesService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnFetchClicked(object sender, EventArgs e)
    {
        string date = DateEntry.Text ?? "";
        if (DateTime.TryParse(date, out _))
        {
            await _service.GetData(date);
            await DisplayAlert("Sukces", $"Dane z {date} zostały pobrane.", "OK");
        }
        else
        {
            await DisplayAlert("Błąd", "Nieprawidłowa data!", "OK");
        }
    }

    private void OnShowClicked(object sender, EventArgs e)
    {
        using var context = new ExchangeDbContext();
        var currencies = context.Currencies
            .OrderBy(c => c.Code)
            .Select(c => $"{c.Code} - {c.Rate} ({c.Date:yyyy-MM-dd})")
            .ToList();

        CurrencyList.ItemsSource = currencies;
    }

    private void OnAddCountryClicked(object sender, EventArgs e)
    {
        string country = CountryNameEntry.Text ?? "";
        string currencyCode = CountryCurrencyEntry.Text ?? "";
        string date = DateEntry.Text ?? "";

        if (DateTime.TryParse(date, out _))
        {
            _service.AddCountry(country, currencyCode, date);
            DisplayAlert("OK", $"Dodano kraj {country}", "OK");
        }
        else
        {
            DisplayAlert("Błąd", "Nieprawidłowa data", "OK");
        }
    }

    private void OnSearchCountriesClicked(object sender, EventArgs e)
    {
        string currencyCode = SearchCurrencyEntry.Text ?? "";
        var countries = _service.GetCountriesByCurrency(currencyCode);

        CurrencyList.ItemsSource = countries.Any()
            ? countries.Select(c => $"{c.Name} → {currencyCode}")
            : new[] { "Nie znaleziono krajów." };
    }

    private async void OnShowRateClicked(object sender, EventArgs e)
    {
        string country = CountryRateEntry.Text ?? "";
        string date = CountryRateDateEntry.Text ?? "";

        var result = await _service.GetRateForCountry(country, date);
        CurrencyList.ItemsSource = new[] { result };
    }

    private void OnShowCountriesClicked(object sender, EventArgs e)
    {
        var result = _service.GetAllCountriesWithCurrencies();
        CurrencyList.ItemsSource = result.Any()
            ? result
            : new[] { "Brak danych w bazie." };
    }

    private async void OnClearDatabaseClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Uwaga", "Czy na pewno chcesz wyczyścić bazę danych?", "Tak", "Nie");
        if (confirm)
        {
            _service.ClearDatabase();
            CurrencyList.ItemsSource = new[] { "Baza danych została wyczyszczona." };
        }
    }
}
