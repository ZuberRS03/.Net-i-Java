# Domumentacja
## Opis projektu
Aplikacja desktopowa napisana w JavaFX umożliwia użytkownikowi:
- Wczytanie obrazu JPG
- Wykonanie operacji: **Negatyw**, **Progowanie**, **Konturowanie**, **Skalowanie**, **Obrót**
- Wykonanie rónoległego przetwarzania (multi-threading) kilku operacji na kopiach obrazu
- Zapis przetworzonego obrazu do pliku PNG

Aplikacja została zaprojektowana z myślą o prostym GUI i responsywności. Wspiera dynamiczne elementy interfejsu użytkownika, takie jak pola tekstowe i przyciski uzależnione od wybranej operacji.
## Struktura projektu
```graphql
└── zad1/
    ├── src/
    │   └── main/
    │       ├── java/
    │       │   └── org.example.zad1/
    │       │       ├── HelloApplication.java
    │       │       └── HelloController.java
    │       └── resources/
    │           ├── images/
    │           │   └── Logotyp-PWr.png
    │           └── org.example.zad1/
    │               └── hello-view.fxml
    ├── pom.xml
```

## Widok aplikacji
![image](https://github.com/user-attachments/assets/26ed95c0-4da3-4613-bfe7-d0430cae76f8)

## Kluczowe funkcjonalności i elementy kodu
### Dynamiczne GUI (FXML + Controller)
Plik `hello-view.fxml` oraz kontroler `HelloController.java` obsługują dynamiczne aktualizowanie paska bocznego na podstawie wybranej opcji. Przykład kodu:
```java
switch (selected) {
    case "Skalowanie" -> {
        TextField widthField = new TextField();
        TextField heightField = new TextField();
        dynamicOptionsBox.getChildren().addAll(...);
    }
    ...
}
```

### Negatyw
Negatyw obrazu generowany jest przez odwrócenie wartości RGB każdego piksela:

### Konturowanie
Operacja detekcji konturów jest uproszczoną wersją metody różnicowania RGB między pikselami sąsiednimi:
```java
int diff = Math.abs(rgb - rgbRight) + Math.abs(rgb - rgbBottom);
int val = diff > 50 ? 0 : 255;
```

### Progowanie
Działa na podstawie wartości średniej RGB. Piksele poniżej progu są czarne, powyżej – białe:
```java
int gray = (col.getRed() + col.getGreen() + col.getBlue()) / 3;
int binary = gray < threshold ? 0 : 255;
```

### Skalowanie
Umożliwia zmianę rozmiaru obrazu przez ustawienie nowych wartości `setFitWidth()` i `setFitHeight()` na `ImageView`, wcześniej parsując wartości z pól:
```java
originalImageView.setFitWidth(newWidth);
originalImageView.setFitHeight(newHeight);
```

### Obrót
Obraz obracany jest o 90° w lewo lub prawo przy użyciu `Graphics2D`, `rotate()` oraz `drawRenderedImage()`:
```java
g2d.rotate(radians, w / 2.0, h / 2.0);
g2d.drawRenderedImage(inputImage, null);
```
Nowy obraz jest tworzony jako `BufferedImage`, a następnie wstawiany do aplikacji.

### Wielowątkowość
Przycisk `Równolegle` tworzy 3 oddzielne wątki do operacji:
```java
Thread negativeThread = new Thread(() -> { ... });
Thread thresholdThread = new Thread(() -> { ... });
Thread contourThread = new Thread(() -> { ... });

negativeThread.start();
thresholdThread.start();
contourThread.start();
```
Wszystkie operacje odbywają się na osobnych kopiach obrazu.
```java
Color col = new Color(rgba, true);
Color neg = new Color(255 - col.getRed(), 255 - col.getGreen(), 255 - col.getBlue());
img.setRGB(x, y, neg.getRGB());
```
Operacja zachowuje przezroczystość (alpha channel) i działa na kopii obrazu.

### Obsługa zapisu i podglądu
Po przetworzeniu każda wersja obrazu jest wyświetlana z odpowiednim podpisem i zmniejszana tak, aby zmieściła się na ekranie.
