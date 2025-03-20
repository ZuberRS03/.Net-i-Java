# Dokumentacja kodu z laboratoriów 1 i 2

## Aplikacja konsolowa
Stworzono następujące klasy:
### 1) Problem:
Zawiera dwa atrybuty:
* `int LiczbaPrzedmiotow` - przechowuje liczbę przedmiotów do wyboru/wygenerowania.
* `List<Przedmioty> Przedmioty` - przechowuje listę wygenerowanych przedmiotów.
  
Jeden konstruktor:
* `Problem (int liczbaPrzedmiotow, int ziarno, int minGranica, int maxGranica)` - przyjmuje dane i generuje zadaną ilość przedmiotów na podstawie podanego ziarna generatra stosując się do nadanych zakresów.

Oraz zawiera jedną główną metodę:
* `Rozwiazanie (int pojemnoscPlecaka)` - sortuje przedmioty względem opłacalności (wartość/waga), następnie tworzy obiekt klasy `Wynik` i dobiera kolejne przedmioty idąc od początku posortowanej listy do końca wybierając przedmioty. Zwraca wynik.
  
### 2) Przedmiot:
Zawiera trzy atrybuty:
* `int Wartosc` - przechowuje wartość przedmiotu
* `int Waga` - przechowuje waga przedmiotu
* `double Oplacalnosc` - wylicza się na podstawie atrybutów `Wartosc` i `Waga` (`Wartosc / Waga`)

Jeden konstruktor:
* `Przedmiot (int wartosc, int waga)` - przyjmuje dane bez dodatowego przekształcania
  
### 3) Wynik:
Zawiera trzy atrybuty:
* `List<int> WybranePrzedmioty` - przechowywuje listę indeksów wybranych przedmiotów
* `int WartoscCalkowita` - przechowywuje wartość całkowitą przedmiotów w plecaku
* `int WagaCalkowita` - przechowywuje wage całkowitą przedmiotów w plecaku

Jeden konstruktor:
* `Wynik()` - tworzy pustą listę `WybranePrzedmioty` oraz przypisuje wartość `0` dla `WartoscCalkowita` i `WagaCalkowita` 

Jedną metodę:
* `DodajPrzedmiot (int indeks, Przedmiot przedmiot)` - dodaje indeks przedmiotu do listy oraz powiększa `WartoscCalkowita` i `WagaCalkowita` o wartość i wagę nowego przedmiotu

### 4) Program:
Przyjmuje wartości `int ziarno`, `int liczbaPrzedmiotw` i `pojemnoscPlecaka` od użytkownika. 
Tworzy obiekt klasy `Problem` i wyświetla wygenerowane przedmioty. 
Na koniec Tworzy obiekt klasy `Wynik` i wyświetla otrzymane wyniki.

## Testy jednostkowe
Zaimplementowano metody sprawdzające następujące sytuacje:
1) Przy poprawnych danych w plecaku powinien być przynajmniej jeden przedmiot.
2) Gdy `pojemność < najlżejszy` przedmiot to plecak powinien być pusty.
3) Dla z góry określonych przedmiotów wynik powinien być zgodny z założeniem.
4) Wprowadzam do wyboru tylko przedmioty o jednakowej wadze i wartości.
5) Podaję jako daną pustą listę przedmiotów.

## GUI
Zgodnie z instrukcją utworzono wyskakujaće okno na zasadzie `drag and drop`. 

Dla odpowiednich `TextBox` ustawiono wartości `Multiline` i `ReadOnly` na true.

W prosty sposób zwalidowano dane wprowadzane do programu. Jeśli jakakolwiek dana jest nieprawidłowa to zmienia się kolor ramki oraz wyskakuje okienko o nieprawidłowcyh danych.

Kiedy dane będą poprawne to wyświetlane są wygenerowane przedmioty oraz, te które zostały zapakowane do plecaka.
