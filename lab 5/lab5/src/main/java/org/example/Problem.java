package org.example;

import java.util.*;

public class Problem {
    private List<Item> items;

    public Problem(int n, int seed, int lowerBound, int upperBound) {
        items = new ArrayList<>();
        Random random = new Random(seed);
        for (int i = 0; i < n; i++) {
            int value = random.nextInt(upperBound - lowerBound + 1) + lowerBound;
            int weight = random.nextInt(upperBound - lowerBound + 1) + lowerBound;
            items.add(new Item(value, weight));
        }
    }

    public Result solve(int capacity) {
        // 1. Posortuj przedmioty po opłacalności (value / weight)
        List<Item> sortedItems = new ArrayList<>(items);
        sortedItems.sort((a, b) -> Double.compare(b.getRatio(), a.getRatio()));

        Map<Integer, Integer> itemCounts = new HashMap<>();
        int totalWeight = 0;
        int totalValue = 0;

        for (int i = 0; i < sortedItems.size(); i++) {
            Item item = sortedItems.get(i);
            int index = items.indexOf(item); // oryginalny indeks

            int maxCount = (capacity - totalWeight) / item.weight;
            if (maxCount > 0) {
                itemCounts.put(index, maxCount);
                totalWeight += maxCount * item.weight;
                totalValue += maxCount * item.value;
            }

            if (totalWeight >= capacity) break;
        }

        return new Result(itemCounts, totalWeight, totalValue);
    }

    public List<Item> getItems() {
        return items;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder("Generated Items:\n");
        for (int i = 0; i < items.size(); i++) {
            sb.append(i).append(": ").append(items.get(i)).append("\n");
        }
        return sb.toString();
    }
}
