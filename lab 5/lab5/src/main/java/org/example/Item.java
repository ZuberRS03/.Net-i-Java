package org.example;

public class Item {
    public int value;
    public int weight;

    public Item(int value, int weight) {
        this.value = value;
        this.weight = weight;
    }

    public double getRatio() {
        return (double) value / weight;
    }

    @Override
    public String toString() {
        return "Item{" + "value=" + value + ", weight=" + weight + '}';
    }
}
