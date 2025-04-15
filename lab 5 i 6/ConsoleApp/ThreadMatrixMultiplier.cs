using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class ThreadMatrixMultiplier
    {
        private int[,] A;
        private int[,] B;
        private int[,] Result;
        private int Size;
        private int ThreadCount;

        public ThreadMatrixMultiplier(int[,] a, int[,] b, int size, int threadCount)
        {
            A = a;
            B = b;
            Size = size;
            ThreadCount = threadCount;
            Result = new int[size, size];
        }

        public void MultiplyWithThreads()
        {
            Thread[] threads = new Thread[ThreadCount];
            int rowsPerThread = Size / ThreadCount;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int t = 0; t < ThreadCount; t++)
            {
                int startRow = t * rowsPerThread;
                int endRow = (t == ThreadCount - 1) ? Size : startRow + rowsPerThread;

                threads[t] = new Thread(() => MultiplyRange(startRow, endRow));
                threads[t].Start();
            }

            foreach (Thread t in threads)
                t.Join();

            stopwatch.Stop();
            Console.WriteLine($"\nCzas mnożenia (Thread) {Size}x{Size} macierzy na {ThreadCount} wątkach: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void MultiplyRange(int fromRow, int toRow)
        {
            for (int i = fromRow; i < toRow; i++)
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
            }
        }

        public void PrintResultMatrix()
        {
            Console.WriteLine("\nMacierz wynikowa (Thread):");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write($"{Result[i, j],4}");
                }
                Console.WriteLine();
            }
        }

        public int[,] GetResult() => Result;
    }
}
