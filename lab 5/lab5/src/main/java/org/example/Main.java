package org.example;


public class Main {
    public static void main(String[] args) {
        Problem problem = new Problem(5, 124, 1, 10); // 5 przedmiotów, seed 123
        System.out.println(problem);

        Result result = problem.solve(25); // pojemność plecaka
        System.out.println(result);
    }
}