# Dokumentacja
## Opis aplikacji
Aplikacja webowa stworzona w technologii Blazor Server umożliwia zarządzanie listą filmów oraz recenzji. Użytkownik może przeglądać filmy w formie kafelków, a po zalogowaniu – dodawać nowe filmy, edytować je, usuwać oraz zarządzać recenzjami. Projekt wykorzystuje bazę danych SQLite oraz Entity Framework Core.

## Struktura bazy danych
Baza danych SQLite zawiera dwie główne tabele:

Tabela: `Movie`
| Pole             | Typ                   | Opis               |
| ---------------- | --------------------- | ------------------ |
| `Id`             | `int`                 | Klucz główny       |
| `Title`          | `string`              | Tytuł filmu        |
| `Description`    | `string`              | Opis filmu         |
| `Year`           | `int`                 | Rok produkcji      |
| `Rating`         | `double`              | Ocena              |
| `CoverImagePath` | `string?`             | Ścieżka do okładki |
| `Reviews`        | `ICollection<Review>` | Recenzje powiązane |


Tabela: `Review`
| Pole           | Typ      | Opis                  |
| -------------- | -------- | --------------------- |
| `Id`           | `int`    | Klucz główny          |
| `MovieId`      | `int`    | Klucz obcy do `Movie` |
| `Content`      | `string` | Treść recenzji        |
| `ReviewerName` | `string` | Imię recenzenta       |
| `Movie`        | `Movie`  | Nawigacja do filmu    |

## Kluczowe fragmenty kodu
### Połączenie z bazą danych – `Program.cs`

```C#
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
```

### Widok kafelkowy filmów – `Pages/MoviePages/Index.razor`
Zamiast tabeli wykorzystano siatkę kart Bootstrap:
```C#
<div class="row row-cols-1 row-cols-md-3 g-4">
    @foreach (var movie in movies)
    {
        <div class="col">
            <div class="card h-100">
                <img src="@movie.CoverImagePath" class="card-img-top" />
                <div class="card-body">
                    <h5>@movie.Title</h5>
                    <p>Ocena: @movie.Rating</p>
                </div>
            </div>
        </div>
    }
</div>
```

### Model danych – `Movie.cs`
```C#
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public string? CoverImagePath { get; set; }
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
```

### Przykład recenzji – `Review.cs`
```C#
public class Review
{
    public int Id { get; set; }
    public int MovieId { get; set; }
    public string Content { get; set; }
    public string ReviewerName { get; set; }

    public Movie Movie { get; set; } = null!;
}
```

## Funkcjonalności aplikacji
- Wyświetlanie listy filmów (kafelki)
- Przeglądanie szczegółów filmu i recenzji
- Dodawanie/edycja/usuwanie filmów (dla zalogowanych)
- Edycja recenzji filmu
- Obsługa bazy danych SQLite (Entity Framework Core)
- Autoryzacja – przyciski edycji widoczne tylko dla zalogowanych użytkowników

## Technologie
- .NET 8.0
- Blazor Server
- Entity Framework Core
- SQLite
- Bootstrap (do wyglądu kafelków)
