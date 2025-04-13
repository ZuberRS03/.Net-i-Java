# Dokumentacja kodu z laboratoriów 3 i 4
## Informacje ogólne
### Wybrane API:
Na potrzeby zadania wybrano API dotyczące kursów walut:  
**Open Exchange Rates**  
Link: [`https://openexchangerates.org`](https://openexchangerates.org)

Aplikacja pobiera dane historyczne o kursach walut (np. USD → PLN) z wybranego dnia. Dane te są przetwarzane i zapisywane do lokalnej bazy danych SQLite.

### Użyta baza danych:
- Format: SQLite
- ORM: Entity Framework Core
- Ścieżka bazy: lokalny plik `exchange.db` w folderze projektu

Zastosowano relację jeden-do-wielu (`1:N`) między tabelami `Currency` a `Country`. Oznacza to, że **wiele krajów może korzystać z tej samej waluty**.

Została ona ustawiona automatycznie przez konwencję Entity Framework Core na podstawie:

- właściwości `CurrencyId` i `Currency` w klasie `Country`
- oraz listy `Countries` w klasie `Currency`

---

## Wersja konsolowa
### Struktura projektu:

#### 1) `Country.cs`
Reprezentuje tabelę `Countries` w bazie danych.

**Kolumny:**
- `Id` – klucz główny
- `Name` – nazwa kraju (np. "Poland")
- `CurrencyId` – klucz obcy do tabeli `Currency`
- `Currency` – właściwość nawigacyjna (relacja)

#### 2) `Currency.cs`
Reprezentuje tabelę `Currencies` w bazie danych.

**Kolumny:**
- `Id` – klucz główny
- `Code` – kod waluty (np. "PLN")
- `Rate` – kurs waluty (w stosunku do USD)
- `Date` – data, dla której podano kurs
- `Countries` – lista krajów korzystających z tej waluty

#### 3) `ExchangeData.cs`
Model do deserializacji danych z API.  
Zawiera:
- `Timestamp` – czas w formacie UNIX
- `Rates` – słownik par: kod waluty → kurs

Dodatkowo zawiera metodę `TimestampToDateTime()` przeliczającą znacznik czasu UNIX na `DateTime`.

#### 4) `ExchangeDbContext.cs`
Klasa kontekstu bazy danych dla Entity Framework Core.

- Ustawia połączenie do pliku SQLite (`Data Source=exchange.db`)
- Definiuje tabele: `Currencies`, `Countries`
- (opcjonalnie) dodaje dane testowe w `OnModelCreating`

#### 5) `ExchangeRatesService.cs`
Zawiera całą logikę aplikacji:

- `GetData(date)` – pobiera dane z API, filtruje wybrane waluty i zapisuje je do bazy.
- `PrintDatabaseContents()` – wypisuje wszystkie waluty i kraje w bazie.
- `PrintCountriesByCurrency(code)` – wypisuje kraje używające danej waluty.
- `GetCurrencyRateForCountry(country, date)` – wypisuje kurs waluty kraju z podanej daty (jeśli trzeba – pobiera dane z API).
- `ClearDatabase()` – usuwa wszystkie dane z tabel.
- `AddCountryToCurrency(country, currency, date)` – przypisuje kraj do istniejącej waluty (z konkretnej daty).

#### 6) `Program.cs`
Punkt wejścia aplikacji.

- Tworzy instancję serwisu `ExchangeRatesService`
- Wykonuje testowe operacje:
  - czyszczenie bazy
  - pobieranie danych z API
  - dodanie krajów
  - wypisanie zawartości bazy
  - pokazanie kursu dla kraju w danym dniu

---
## Wersja z GUI w MAUI

Aplikacja została rozbudowana o interfejs graficzny (GUI) z użyciem .NET MAUI – technologią pozwalającą tworzyć aplikacje mobilne i desktopowe w jednym projekcie.

### Zmiany w bazie danych
- Lokalizacja bazy: lokalny katalog aplikacji (`FileSystem.AppDataDirectory`)
- Baza jest automatycznie tworzona przy uruchomieniu aplikacji, jeśli nie istnieje

### Struktura projektu

#### 1) `ExchangeDbContext.cs`
Odpowiada za połączenie z bazą danych SQLite i konfigurację relacji.

- Tworzy bazę danych `exchange.db` w katalogu `FileSystem.AppDataDirectory`:
  ```csharp
  var dbPath = Path.Combine(FileSystem.AppDataDirectory, "exchange.db");
  optionsBuilder.UseSqlite($"Data Source={dbPath}");
- Relacja między encjami:
  ```csharp
  modelBuilder.Entity<Currency>()
    .HasMany(c => c.Countries)
    .WithOne(c => c.Currency)
    .HasForeignKey(c => c.CurrencyId);

#### 2) `ExchangeRatesService.cs`
Główna logika aplikacji:
- `GetData(date)` – pobiera kursy walut z danego dnia i zapisuje do bazy
- `AddCountry(name, code, date)` – dodaje kraj do istniejącej waluty
- `ClearDatabase()` – usuwa wszystkie dane z bazy
- `GetCountriesByCurrency(code)` – zwraca kraje z daną walutą
- `GetRateForCountry(country, date)` – zwraca kurs waluty kraju w danym dniu
- `GetAllCountriesWithCurrencies()` – wypisuje wszystkie kraje z przypisanymi walutami

#### 3) `MainPage.xaml`
Zawiera graficzny interfejs użytkownika:
- Pola do wprowadzania daty, kraju, waluty
- Przycisk do pobierania danych z API
- Przycisk do dodawania kraju
- Przycisk do filtrowania po walucie
- Przycisk do sprawdzania kursu
- Lista wyników (`CollectionView`)

#### 4) `MainPage.xaml.cs`
Logika zdarzeń (eventów) przycisków:
- `OnFetchClicked` – wywołuje `GetData(date)`
- `OnAddCountryClicked` – dodaje kraj do waluty
- `OnSearchCountriesClicked` – wyświetla kraje z daną walutą
- `OnShowRateClicked` – pokazuje kurs dla kraju
- `OnShowCountriesClicked` – wyświetla wszystkie kraje z walutami
- `OnClearDatabaseClicked` – usuwa dane z bazy

#### 5) `MauiProgram.cs`
Rejestracja zależności:
- Dodaje `ExchangeRatesService` i `MainPage` do Dependency Injection
- Konfiguruje czcionki i logger debugowania
