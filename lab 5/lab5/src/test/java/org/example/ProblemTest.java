package org.example;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ProblemTest {
    // 1. Sprawdzenie, czy jeśli co najmniej jeden przedmiot spełnia ograniczenia, to zwrócono co najmniej jeden element.
    @Test
    public void testAtLeastOneItemFits() {
        Problem problem = new Problem(3, 1, 1, 10);
        Result result = problem.solve(10);
        assertFalse(result.itemCounts.isEmpty(), "Powinno zostać wybrane co najmniej jeden przedmiot");
        int totalItems = result.itemCounts.values().stream().mapToInt(i -> i).sum();
        assertTrue(totalItems >= 1, "Liczba przedmiotów w plecaku powinna być >= 1");
    }

    // 2. Sprawdzenie, czy jeśli żaden przedmiot nie spełnia ograniczeń, to zwrócono puste rozwiązanie.
    @Test
    public void testNoItemFits() {
        Problem problem = new Problem(5, 42, 10, 10); // Wszystkie wagi = 10
        Result result = problem.solve(5); // Pojemność < każda waga
        assertTrue(result.itemCounts.isEmpty(), "Wynik powinien być pusty (żaden przedmiot się nie mieści)");
        assertEquals(0, result.totalWeight);
        assertEquals(0, result.totalValue);
    }

    // 3. Sprawdzenie, czy waga i wartość wszystkich przedmiotów z listy mieści się w założonym przedziale.
    @Test
    public void testItemWeightValueInRange() {
        int n = 20;
        int lower = 1, upper = 10;
        Problem problem = new Problem(n, 1234, lower, upper);
        for (Item item : problem.getItems()) {
            assertTrue(item.weight >= lower && item.weight <= upper, "Waga poza zakresem");
            assertTrue(item.value >= lower && item.value <= upper, "Wartość poza zakresem");
        }
    }

    // 4. Sprawdzenie poprawności wyniku (sumy wag i wartości w plecaku) dla konkretnej instancji.
    @Test
    public void testCorrectResultForKnownInstance() {
        // Ręcznie przygotowane przedmioty: 2 przedmioty, oba value=5, weight=2
        Problem problem = new Problem(0, 0, 0, 0) {
            {
                getItems().add(new Item(5, 2));
                getItems().add(new Item(5, 2));
            }
        };
        Result result = problem.solve(6); // powinny zmieścić się 3 przedmioty

        int expectedCount = 3;
        int expectedWeight = 6;
        int expectedValue = 15;

        int actualCount = result.itemCounts.values().stream().mapToInt(i -> i).sum();
        assertEquals(expectedCount, actualCount, "Niewłaściwa liczba przedmiotów");
        assertEquals(expectedWeight, result.totalWeight, "Błędna suma wag");
        assertEquals(expectedValue, result.totalValue, "Błędna suma wartości");
    }
}