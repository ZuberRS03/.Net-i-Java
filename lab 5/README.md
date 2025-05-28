# Domumentacja
## Opis projektu
Projekt jest implementacją nieograniczonego problemu plecakowego (unbounded knapsack problem) w języku Java.
Celem jest stworzenie konsolowej aplikacji, która:
- Generuje losową instancję problemu z określoną liczbą przedmiotów,
- Rozwiązuje problem metodą zachłanną (algorytm Dantziga),
- Zwraca optymalne w przybliżeniu zestawienie przedmiotów mieszczących się w plecaku o zadanej pojemności,
- Zawiera testy jednostkowe sprawdzające poprawność działania.

## Struktura projektu
```graphql
lab5/
├── pom.xml                      # Konfiguracja Maven
└── src/
    ├── main/
    │   └── java/
    │       └── org.example/
    │           ├── Main.java       # Punkt startowy aplikacji
    │           ├── Problem.java    # Klasa implementująca problem plecakowy i algorytm rozwiązujący
    │           ├── Item.java       # Klasa reprezentująca pojedynczy przedmiot (wartość, waga)
    │           └── Result.java     # Klasa przechowująca wynik rozwiązania (przedmioty, suma wag i wartości)
    └── test/
        └── java/
            └── org.example/
                └── ProblemTest.java # Testy jednostkowe JUnit 5

```

## Opis najważniejszych klas i metod
### `Main.java`
- **Funkcja:** Punkt wejścia do aplikacji.
- **Działanie:** Tworzy instancję problemu, wypisuje wygenerowane przedmioty i wyświetla rozwiązanie dla zadanego rozmiaru plecaka.


### `Item.java`
- Funkcja: Reprezentuje pojedynczy przedmiot.
- Pola:
    - `int value` — wartość przedmiotu,
    - `int weight` — waga przedmiotu.
- Metoda:
    - `getRatio()` — zwraca stosunek wartości do wagi (opłacalność).
    - `toString()` — zwraca tekstową reprezentację obiektu.

### `Problem.java`
- Funkcja:
    - Generuje losową listę przedmiotów o zadanych parametrach.
    - Implementuje metodę zachłannego rozwiązania `solve(int capacity)`.
- Konstruktor:
    - `Problem(int n, int seed, int lowerBound, int upperBound)` — generuje `n` przedmiotów o wartościach i wagach z podanego zakresu, ziarno `seed` do generatora liczb pseudolosowych.
- Metody:
    - `solve(int capacity)` — rozwiązuje problem metodą zachłanną, zwraca obiekt `Result`.
    - `toString()` — zwraca tekst z listą wygenerowanych przedmiotów.

### `ProblemTest.java`
- Funkcja: Testy jednostkowe sprawdzające:
    - generowanie poprawnej liczby przedmiotów,
    - poprawność zakresów wag i wartości,
    - zwracanie pustego wyniku, gdy plecak jest za mały,
    - poprawność rozwiązania dla znanej instancji.
