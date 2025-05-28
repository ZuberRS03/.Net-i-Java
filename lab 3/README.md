# Dokumentacja kodu z laboratoriów 5 i 6

## Mnożenie macierzy - wersja konsolowa
### Struktura projektu:
Projekt składa się z trzech głównych klas:
1) **`Program.cs`:**

   Główna logika sterująca – przeprowadza testy wydajności dla różnych rozmiarów macierzy.
   ##### Kluczowe elementy:
    - `sizesToTest`: tablica testowanych rozmiarów macierzy (np. `100`, `200`, ..., `2000`).
    - `threads`: liczba wątków używana podczas mnożenia.
    - `attempts`: liczba prób wykonywanych dla każdego testu (wynik jest uśredniany).
    - Poprzez pętle wielokrotne wywołanie obliczeń dla różnych zestawów danych oraz liczenie średnich czasów dla każdego rozmiaru macierzy. 
    - Zapis wyników do pliku `results.csv`
    
2) **`ThreadMatrixMultiplier.cs`:**
   
   Implementacja mnożenia przy użyciu klasy `Thread`.
   ##### Kluczowe metody:
    - `MultiplyWithThreads()` – dzieli zadanie mnożenia na N niezależnych wątków, każdy przetwarza inną część wierszy.
    - `MultiplyRange(int fromRow, int toRow)` – pomocnicza funkcja wykonująca mnożenie fragmentu macierzy.
    - `PrintResultMatrix()` – wypisuje wynik.
    - `GetResult()` – dostęp do wynikowej macierzy.
    
3) **`ParallelMatrixMultiplier.cs`:**

   Implementacja mnożenia przy użyciu klasy `Parallel.For`.
   ##### Kluczowe metody:
    - `GenerateRandomMatrices()` – generuje dwie macierze `A` i `B` z losowymi liczbami całkowitymi (1–20).
    - `MultiplyParallel()` – mnoży macierze z wykorzystaniem `Parallel.For` i zadaną liczbą wątków (MaxDegreeOfParallelism).
    - `PrintInputMatrices()` / `PrintResultMatrix()` – wypisują dane do konsoli.
    - `GetMatrixA()` / `GetMatrixB()` – dostęp do macierzy źródłowych.

   
## Przetwarzanie obrazów z GUI – Windows Forms

### Cel aplikacji

Aplikacja umożliwia użytkownikowi:
- załadowanie obrazu z pliku (`.jpg`, `.png`),
- przetworzenie obrazu za pomocą czterech filtrów:
  - skala szarości (Grayscale),
  - negatyw (Negative),
  - detekcja krawędzi (Edge Detection – filtr Sobela),
  - odbicie lustrzane (Mirror),
- wyświetlenie wyników w czterech `PictureBoxach`.

Każdy filtr jest wykonywany równolegle przy użyciu `Parallel.Invoke`.

### Najważniejsze częście projektu

1) **`Program.cs`**
   Uruchamia aplikację okienkową z formularzem `Form1`.
   
2) **`Form1.cs`**
   Logika interfejsu graficznego.
   #### Główne kontrolki:
   - `pictureBox1` – załadowany obraz źródłowy,
   - `pictureBox2` – obraz przetworzony filtrem Grayscale,
   - `pictureBox3` – obraz przetworzony filtrem Negative,
   - `pictureBox4` – obraz przetworzony filtrem Edge Detection (Sobel),
   - `pictureBox5` – obraz przetworzony filtrem Mirror,
   - `button1` – przycisk „Załaduj obraz”,
   - `button2` – przycisk „Wykonaj” (wywołuje filtry).

   #### Główne zadania:
   - Tworzy kopie obrazu źródłowego.
   - Uruchamia równolegle 4 filtry (`Parallel.Invoke`).
   - Przypisuje przetworzone obrazy do `pictureBox2–5`.
     
3) **`ImageProcessor.cs`**
   Zawiera metody przetwarzania obrazu (filtry).
   #### Dostępne metody:
   - `Grayscale(Bitmap input)` - Zamienia obraz na odcienie szarości (weighted average RGB).
   - `Negative(Bitmap input)` - Tworzy negatyw – odwraca kanały R, G, B.
   - `Mirror(Bitmap input)` - Odbija obraz lustrzanie względem osi pionowej.
   - `EdgeDetection(Bitmap input)` - Wykrywa krawędzie przy użyciu maski Sobela. Używa dwóch filtrów (X i Y), oblicza gradient, wynik jako szarość.

---
# Analiza wyników pomiarów czasów obliczeń
## Wyniki pomiarów:
| Rozmiar macierzy | Ilość Wątków | Średni czas Parallel (ms)| Średni czas Thread (ms) |
|------|--------|:-------------------------:|:------------------------:|
| 100  | 1      | 17                        | 17                       |
| 200  | 1      | 79                        | 89                       |
| 400  | 1      | 639                       | 633                      |
| 800  | 1      | 5413                      | 5647                     |
| 1000 | 1      | 9632                      | 9854                     |
| 2000 | 1      | 107974                    | 89296                    |
| 100  | 2      | 10                        | 21                       |
| 200  | 2      | 31                        | 35                       |
| 400  | 2      | 209                       | 232                      |
| 800  | 2      | 1834                      | 1777                     |
| 1000 | 2      | 3758                      | 3835                     |
| 2000 | 2      | 46262                     | 47405                    |
| 100  | 4      | 8                         | 33                       |
| 200  | 4      | 13                        | 52                       |
| 400  | 4      | 132                       | 197                      |
| 800  | 4      | 991                       | 1080                     |
| 1000 | 4      | 2091                      | 2105                     |
| 2000 | 4      | 26499                     | 25246                    |

## Omówienie wyników:
W ogólnym rozrachunku czasy są zbliżone niezależnie czy jest użyta wysokopoziomowa metoda Parallel czy niskopoziomowa Thread.
"Dołożenie" wątków w widoczny sposób zwiększa prędkość obliczeń. Przy takich wielkościach macierzy (łatwo podzielnych na poszczególne wątki) przyspieszenie jest zbliżone do odwrotnie proporcjonalnego czyli po podwojeniu liczby wątków, dwókrotnie spada czas wykonania operacji. W obecnej implementacji dla niesprzyjających rozmiarów macierzy metoda Thread powinna być wolniejsza ze względu na sposób dzielenia zadań między wątki, w którym jeżeli wysepuje niepodzielnośćto ostatni wątek dostaje całość naddatku. 

## Wykresy z wynikami:
![image](https://github.com/user-attachments/assets/b785f2e2-8d12-43f4-9f46-2926d7f5534f)

![image](https://github.com/user-attachments/assets/775c6939-0f4e-4593-a3be-d5759e6edd06)

![image](https://github.com/user-attachments/assets/2748d325-7bf0-40ed-a1ba-e9113bc9936e)



