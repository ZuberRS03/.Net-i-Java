using System.Diagnostics;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int size = 2000; // rozmiar macierzy NxN
            //int threads = 4; // liczba wątków, zmień dla testów

            //var multiplier = new ParallelMatrixMultiplier(size, threads);
            //multiplier.GenerateRandomMatrices();
            ////multiplier.PrintInputMatrices();

            //// Parallel.For
            //multiplier.MultiplyParallel();
            ////multiplier.PrintResultMatrix();

            //// Thread
            //var threadMultiplier = new ThreadMatrixMultiplier(
            //    multiplier.GetMatrixA(),
            //    multiplier.GetMatrixB(),
            //    size, 
            //    threads
            //);
            //threadMultiplier.MultiplyWithThreads();
            ////threadMultiplier.PrintResultMatrix();
            //

            int[] sizesToTest = new int[] { 100, 200, 400, 800, 1000, 2000 }; // <- tutaj możesz edytować rozmiary
            int threads = 4;
            int attempts = 5;

            string outputPath = "results.csv";
            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("Size,Threads,AverageTimeParallel(ms),AverageTimeThread(ms)");

                foreach (int size in sizesToTest)
                {
                    long totalParallel = 0;
                    long totalThread = 0;

                    for (int i = 0; i < attempts; i++)
                    {
                        var multiplier = new ParallelMatrixMultiplier(size, threads);
                        multiplier.GenerateRandomMatrices();

                        // Parallel
                        Stopwatch swParallel = Stopwatch.StartNew();
                        multiplier.MultiplyParallel();
                        swParallel.Stop();
                        totalParallel += swParallel.ElapsedMilliseconds;

                        // Thread
                        var threadMultiplier = new ThreadMatrixMultiplier(
                            multiplier.GetMatrixA(),
                            multiplier.GetMatrixB(),
                            size,
                            threads
                        );
                        Stopwatch swThread = Stopwatch.StartNew();
                        threadMultiplier.MultiplyWithThreads();
                        swThread.Stop();
                        totalThread += swThread.ElapsedMilliseconds;
                    }

                    double avgParallel = totalParallel / (double)attempts;
                    double avgThread = totalThread / (double)attempts;

                    writer.WriteLine($"{size},{threads},{avgParallel:F2},{avgThread:F2}");
                    Console.WriteLine($"Zapisano wyniki dla macierzy {size}x{size}");
                }
            }

            Console.WriteLine($"\nWyniki zapisane w pliku: {outputPath}");
        }
    }
}
