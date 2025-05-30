module org.example.zad1 {
    requires javafx.controls;
    requires javafx.fxml;
    requires javafx.swing;

    requires com.dlsc.formsfx;
    requires java.desktop;

    opens org.example.zad1 to javafx.fxml;
    exports org.example.zad1;
}