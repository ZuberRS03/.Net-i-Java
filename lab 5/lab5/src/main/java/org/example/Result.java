package org.example;

import java.util.Map;

public class Result {
    public Map<Integer, Integer> itemCounts; // index -> count
    public int totalWeight;
    public int totalValue;

    public Result(Map<Integer, Integer> itemCounts, int totalWeight, int totalValue) {
        this.itemCounts = itemCounts;
        this.totalWeight = totalWeight;
        this.totalValue = totalValue;
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder("Result:\n");
        for (Map.Entry<Integer, Integer> entry : itemCounts.entrySet()) {
            sb.append("Item ").append(entry.getKey())
                    .append(" x").append(entry.getValue()).append("\n");
        }
        sb.append("Total weight: ").append(totalWeight).append("\n");
        sb.append("Total value: ").append(totalValue).append("\n");
        return sb.toString();
    }
}
