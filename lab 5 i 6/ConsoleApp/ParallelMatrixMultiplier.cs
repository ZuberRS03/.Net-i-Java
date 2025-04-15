using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    // Mnożenie z użyciem Parallel.For
    public class ParallelMatrixMultiplier
    {
        private int[,] A;
        private int[,] B;
        private int[,] Result;
        private int Size;
        private int Threads;
        public int[,] GetMatrixA() => A;
        public int[,] GetMatrixB() => B;

        public ParallelMatrixMultiplier(int size, int threads)
        {
            Size = size;
            Threads = threads;
            A = new int[size, size];
            B = new int[size, size];
            Result = new int[size, size];
        }

        public void GenerateRandomMatrices()
        {
            Random rand = new Random();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    A[i, j] = rand.Next(1, 20);
                    B[i, j] = rand.Next(1, 20);
                }
            }
        }

        public void MultiplyParallel()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var options = new ParallelOptions { MaxDegreeOfParallelism = Threads };

            Parallel.For(0, Size, options, i =>
            {
                for (int j = 0; j < Size; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < Size; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    Result[i, j] = sum;
                }
            });

            stopwatch.Stop();
            Console.WriteLine($"Czas mnożenia (Parallel) {Size}x{Size} macierzy na {Threads} wątkach: {stopwatch.ElapsedMilliseconds} ms");
        }

        public void PrintInputMatrices()
        {
            PrintMatrix(A, "Macierz A");
            PrintMatrix(B, "Macierz B");
        }

        public void PrintResultMatrix()
        {
            PrintMatrix(Result, "Macierz wynikowa");
        }

        private void PrintMatrix(int[,] matrix, string name)
        {
            Console.WriteLine($"\n{name}:");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write($"{matrix[i, j],4}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
