package org.example.zad1;

import javafx.application.Platform;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.HBox;
import javafx.scene.layout.VBox;
import javafx.stage.FileChooser;

import javax.imageio.ImageIO;
import javafx.geometry.Insets;
import javafx.embed.swing.SwingFXUtils;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.BufferedWriter;
import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Objects;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;


public class HelloController {
    @FXML private ComboBox<String> operationComboBox;
    @FXML private ImageView logoImage;
    @FXML private ImageView originalImageView;
    @FXML private VBox dynamicOptionsBox;
    @FXML private Button executeButton;
    @FXML private Button saveResultButton;
    @FXML private HBox additionalResultsBox;

    private File currentImageFile;
    private final ExecutorService executor = Executors.newFixedThreadPool(4);

    @FXML
    public void initialize() {
        operationComboBox.getItems().addAll("Negatyw", "Progowanie", "Konturowanie", "Skalowanie", "Obrót");
        operationComboBox.setOnAction(e -> updateOptionsPanel());
        executeButton.setDisable(true);
        saveResultButton.setDisable(true);
        log("Aplikacja uruchomiona", "INFO");
        loadLogo();
    }

    private void loadLogo() {
        Image logo = new Image(Objects.requireNonNull(getClass().getResourceAsStream("/images/Logotyp-PWr.png")));
        logoImage.setImage(logo);
    }

    @FXML
    private void onLoadImageClick() {
        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Wybierz plik obrazu");
        fileChooser.getExtensionFilters().add(
                new FileChooser.ExtensionFilter("Pliki JPG", "*.jpg")
        );

        File selectedFile = fileChooser.showOpenDialog(null);
        if (selectedFile != null) {
            currentImageFile = selectedFile;
            Image image = new Image(selectedFile.toURI().toString());
            originalImageView.setImage(image);
            executeButton.setDisable(false);
            saveResultButton.setDisable(false);
            additionalResultsBox.getChildren().clear();
            log("Wczytano obraz: " + selectedFile.getName(), "INFO");
        }
    }

    @FXML
    private void onExecuteClick() {
        String selected = operationComboBox.getValue();
        if (selected == null || currentImageFile == null) {
            showToast("Wybierz operację i załaduj obraz.");
            return;
        }

        switch (selected) {
            case "Negatyw" -> applyNegative();
            case "Progowanie" -> applyThreshold();
            case "Konturowanie" -> applyContour();
            case "Skalowanie" -> applyScale();
        }
    }

    @FXML
    private void onSaveResultClick() {
        if (currentImageFile == null) return;

        FileChooser fileChooser = new FileChooser();
        fileChooser.setTitle("Zapisz obraz");
        fileChooser.setInitialFileName("wynik.png");
        fileChooser.getExtensionFilters().add(new FileChooser.ExtensionFilter("PNG", "*.png"));
        File saved = fileChooser.showSaveDialog(null);
        if (saved != null) {
            try {
                BufferedImage buffered = ImageIO.read(currentImageFile);
                ImageIO.write(buffered, "png", saved);
                showToast("Obraz zapisany.");
                log("Zapisano obraz do pliku: " + saved.getAbsolutePath(), "INFO");
            } catch (IOException e) {
                showToast("Błąd podczas zapisu.");
                log("Błąd zapisu: " + e.getMessage(), "ERROR");
            }
        }
    }

    @FXML
    private void onParallelExecuteClick() {
        if (currentImageFile == null) {
            showToast("Wczytaj obraz najpierw.");
            return;
        }
        additionalResultsBox.getChildren().clear();

        executor.submit(() -> runParallel("Negatyw"));
        executor.submit(() -> runParallel("Progowanie"));
        executor.submit(() -> runParallel("Konturowanie"));
    }

    private void runParallel(String type) {
        try {
            BufferedImage img = ImageIO.read(currentImageFile);
            BufferedImage result = switch (type) {
                case "Negatyw" -> negative(img);
                case "Progowanie" -> threshold(img, 128);
                case "Konturowanie" -> contour(img);
                default -> null;
            };

            if (result != null) {
                File tmp = new File("result_" + type + ".png");
                ImageIO.write(result, "png", tmp);
                Image fx = SwingFXUtils.toFXImage(result, null);

                ImageView view = new ImageView(fx);
                view.setPreserveRatio(true);
                view.setFitWidth(200);

                Label label = new Label(type);
                VBox box = new VBox(5, label, view);
                box.setStyle("-fx-padding: 10px;");

                Platform.runLater(() -> additionalResultsBox.getChildren().add(box));
            }
        } catch (IOException e) {
            log("Błąd przetwarzania " + type + ": " + e.getMessage(), "ERROR");
        }
    }

    private BufferedImage negative(BufferedImage img) {
        for (int y = 0; y < img.getHeight(); y++)
            for (int x = 0; x < img.getWidth(); x++) {
                int rgba = img.getRGB(x, y);
                Color col = new Color(rgba, true);
                Color neg = new Color(255 - col.getRed(), 255 - col.getGreen(), 255 - col.getBlue());
                img.setRGB(x, y, neg.getRGB());
            }
        return img;
    }

    private BufferedImage threshold(BufferedImage img, int threshold) {
        for (int y = 0; y < img.getHeight(); y++)
            for (int x = 0; x < img.getWidth(); x++) {
                Color col = new Color(img.getRGB(x, y));
                int gray = (col.getRed() + col.getGreen() + col.getBlue()) / 3;
                int binary = gray < threshold ? 0 : 255;
                Color result = new Color(binary, binary, binary);
                img.setRGB(x, y, result.getRGB());
            }
        return img;
    }

    private BufferedImage contour(BufferedImage img) {
        BufferedImage result = new BufferedImage(img.getWidth(), img.getHeight(), img.getType());
        for (int y = 0; y < img.getHeight() - 1; y++)
            for (int x = 0; x < img.getWidth() - 1; x++) {
                int rgb = img.getRGB(x, y);
                int rgbRight = img.getRGB(x + 1, y);
                int rgbBottom = img.getRGB(x, y + 1);
                int diff = Math.abs(rgb - rgbRight) + Math.abs(rgb - rgbBottom);
                int val = diff > 50 ? 0 : 255;
                Color edge = new Color(val, val, val);
                result.setRGB(x, y, edge.getRGB());
            }
        return result;
    }

    private void applyNegative() {
        try {
            BufferedImage img = ImageIO.read(currentImageFile);
            updateImage(negative(img));
            showToast("Negatyw został wygenerowany pomyślnie!");
        } catch (IOException e) {
            showToast("Nie udało się wykonać negatywu.");
        }
    }

    private void applyThreshold() {
        try {
            TextField field = (TextField) dynamicOptionsBox.getChildren().get(1);
            int threshold = Integer.parseInt(field.getText());
            if (threshold < 0 || threshold > 255) throw new NumberFormatException();
            BufferedImage img = ImageIO.read(currentImageFile);
            updateImage(threshold(img, threshold));
            showToast("Progowanie zakończone.");
        } catch (Exception e) {
            showToast("Błąd progowania. Sprawdź wartość progu.");
        }
    }

    private void applyContour() {
        try {
            BufferedImage img = ImageIO.read(currentImageFile);
            updateImage(contour(img));
            showToast("Konturowanie zakończone.");
        } catch (IOException e) {
            showToast("Błąd podczas konturowania.");
        }
    }

    private void applyScale() {
        try {
            TextField widthField = (TextField) dynamicOptionsBox.getChildren().get(1);
            TextField heightField = (TextField) dynamicOptionsBox.getChildren().get(3);
            int newWidth = Integer.parseInt(widthField.getText());
            int newHeight = Integer.parseInt(heightField.getText());
            if (newWidth <= 0 || newWidth > 3000 || newHeight <= 0 || newHeight > 3000)
                throw new NumberFormatException();
            BufferedImage img = ImageIO.read(currentImageFile);
            Image scaledImage = SwingFXUtils.toFXImage(img, null);
            originalImageView.setFitWidth(newWidth);
            originalImageView.setFitHeight(newHeight);
            originalImageView.setImage(scaledImage);
            showToast("Skalowanie zakończone.");
        } catch (Exception e) {
            showToast("Nieprawidłowe wymiary skalowania.");
        }
    }

    private void rotateImage(int angleDegrees) {
        if (currentImageFile == null) return;

        try {
            BufferedImage inputImage = ImageIO.read(currentImageFile);
            double radians = Math.toRadians(angleDegrees);
            double sin = Math.abs(Math.sin(radians));
            double cos = Math.abs(Math.cos(radians));
            int w = inputImage.getWidth();
            int h = inputImage.getHeight();
            int newWidth = (int) Math.floor(w * cos + h * sin);
            int newHeight = (int) Math.floor(h * cos + w * sin);

            BufferedImage rotated = new BufferedImage(newWidth, newHeight, inputImage.getType());
            Graphics2D g2d = rotated.createGraphics();
            g2d.translate((newWidth - w) / 2.0, (newHeight - h) / 2.0);
            g2d.rotate(radians, w / 2.0, h / 2.0);
            g2d.drawRenderedImage(inputImage, null);
            g2d.dispose();

            updateImage(rotated);
            showToast("Obraz obrócono o " + angleDegrees + "°.");
        } catch (IOException e) {
            showToast("Nie udało się obrócić obrazu.");
        }
    }

    private void updateOptionsPanel() {
        dynamicOptionsBox.getChildren().clear();
        String selected = operationComboBox.getValue();

        switch (selected) {
            case "Progowanie" -> {
                Label label = new Label("Wartość progu (0-255):");
                TextField field = new TextField();
                field.setPromptText("0-255");
                dynamicOptionsBox.getChildren().addAll(label, field);
            }
            case "Skalowanie" -> {
                Label wLabel = new Label("Szerokość (0-3000):");
                TextField widthField = new TextField();
                widthField.setPromptText("szerokość");

                Label hLabel = new Label("Wysokość (0-3000):");
                TextField heightField = new TextField();
                heightField.setPromptText("wysokość");

                dynamicOptionsBox.getChildren().addAll(wLabel, widthField, hLabel, heightField);
            }
            case "Obrót" -> {
                Button left = new Button("<");
                Button right = new Button(">");
                left.setOnAction(e -> rotateImage(-90));
                right.setOnAction(e -> rotateImage(90));
                HBox box = new HBox(10, left, right);
                box.setPadding(new Insets(10));
                dynamicOptionsBox.getChildren().add(box);
            }
        }
    }

    private void updateImage(BufferedImage img) throws IOException {
        File temp = new File("temp_result.png");
        ImageIO.write(img, "png", temp);
        currentImageFile = temp;
        originalImageView.setImage(SwingFXUtils.toFXImage(img, null));
    }

    private void showToast(String message) {
        Alert alert = new Alert(Alert.AlertType.INFORMATION);
        alert.setTitle("Komunikat");
        alert.setHeaderText(null);
        alert.setContentText(message);
        alert.showAndWait();
    }

    private void log(String message, String level) {
        try (BufferedWriter writer = Files.newBufferedWriter(Paths.get("logs.txt"), java.nio.file.StandardOpenOption.CREATE, java.nio.file.StandardOpenOption.APPEND)) {
            String time = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss").format(new Date());
            writer.write(String.format("[%s] [%s] %s%n", time, level, message));
        } catch (IOException e) {
            System.err.println("Nie mozna zapisac logu: " + e.getMessage());
        }
    }
}

//private void applyNegative() {
//    try {
//        BufferedImage img = ImageIO.read(currentImageFile);
//        for (int y = 0; y < img.getHeight(); y++)
//            for (int x = 0; x < img.getWidth(); x++) {
//                int rgba = img.getRGB(x, y);
//                Color col = new Color(rgba, true);
//                Color neg = new Color(255 - col.getRed(), 255 - col.getGreen(), 255 - col.getBlue());
//                img.setRGB(x, y, neg.getRGB());
//            }
//        updateImage(img);
//        showToast("Negatyw został wygenerowany pomyślnie!");
//    } catch (IOException e) {
//        showToast("Nie udało się wykonać negatywu.");
//    }
//}
//
//private void applyThreshold() {
//    try {
//        TextField field = (TextField) dynamicOptionsBox.getChildren().get(1);
//        int threshold = Integer.parseInt(field.getText());
//        if (threshold < 0 || threshold > 255) throw new NumberFormatException();
//
//        BufferedImage img = ImageIO.read(currentImageFile);
//        for (int y = 0; y < img.getHeight(); y++)
//            for (int x = 0; x < img.getWidth(); x++) {
//                Color col = new Color(img.getRGB(x, y));
//                int gray = (col.getRed() + col.getGreen() + col.getBlue()) / 3;
//                int binary = gray < threshold ? 0 : 255;
//                Color result = new Color(binary, binary, binary);
//                img.setRGB(x, y, result.getRGB());
//            }
//        updateImage(img);
//        showToast("Progowanie zakończone.");
//    } catch (Exception e) {
//        showToast("Błąd progowania. Sprawdź wartość progu.");
//    }
//}
//
//private void applyContour() {
//    try {
//        BufferedImage img = ImageIO.read(currentImageFile);
//        BufferedImage result = new BufferedImage(img.getWidth(), img.getHeight(), img.getType());
//
//        for (int y = 0; y < img.getHeight() - 1; y++)
//            for (int x = 0; x < img.getWidth() - 1; x++) {
//                int rgb = img.getRGB(x, y);
//                int rgbRight = img.getRGB(x + 1, y);
//                int rgbBottom = img.getRGB(x, y + 1);
//
//                int diff = Math.abs(rgb - rgbRight) + Math.abs(rgb - rgbBottom);
//                int val = diff > 50 ? 0 : 255;
//                Color edge = new Color(val, val, val);
//                result.setRGB(x, y, edge.getRGB());
//            }
//
//        updateImage(result);
//        showToast("Konturowanie zakończone.");
//    } catch (IOException e) {
//        showToast("Błąd podczas konturowania.");
//    }
//}
