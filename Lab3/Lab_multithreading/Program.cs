using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Lab_multithreading
{
    class MatrixCalculator
    {
        private int size;
        private double[,] matrixA;
        private double[,] matrixB;
        private double[,] result;

        public MatrixCalculator(int size)
        {
            this.size = size;
            matrixA = GenerateMatrix(size);
            matrixB = GenerateMatrix(size);
            result = new double[size, size];
        }

        private double[,] GenerateMatrix(int n)
        {
            Random rand = new Random();
            double[,] matrix = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = rand.NextDouble() * 10;
            return matrix;
        }

        //wyokopoziomowe
        public long highLevel(int maxThreads)
        {
            Array.Clear(result, 0, result.Length);
            var options = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };
            
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, size, options, i =>
            {
                for (int j = 0; j < size; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < size; k++)
                        sum += matrixA[i, k] * matrixB[k, j];
                    result[i, j] = sum;
                }
            });
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        // NIskopoziomowe
        public long lowLevel(int threadCount)
        {
            Array.Clear(result, 0, result.Length);
            Thread[] threads = new Thread[threadCount];
            int rowsPerThread = size / threadCount;

            Stopwatch sw = Stopwatch.StartNew();
            for (int t = 0; t < threadCount; t++)
            {
                int startRow = t * rowsPerThread;
                int endRow = (t == threadCount - 1) ? size : (t + 1) * rowsPerThread;

                threads[t] = new Thread(() => 
                {
                    for (int i = startRow; i < endRow; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            double sum = 0;
                            for (int k = 0; k < size; k++)
                                sum += matrixA[i, k] * matrixB[k, j];
                            result[i, j] = sum;
                        }
                    }
                });
                threads[t].Start();
            }

            foreach (var t in threads) t.Join();
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int size = 500; 
            int[] threadConfigs = { 1, 2, 4, 8, 12 };
            var calc = new MatrixCalculator(size);

            Console.WriteLine($"Mnożenie macierzy {size}x{size}\n");
            Console.WriteLine("| Wątki | HHigh (ms) | Low (ms) |");

            foreach (int t in threadConfigs)
            {
                // Uśrednianie wyników dla 3 razy dla akzdej ilosci watkow
                long timeParallel = 0;
                long timeThreads = 0;

                for (int i = 0; i < 3; i++)
                {
                    timeParallel += calc.highLevel(t);
                    timeThreads += calc.lowLevel(t);
                }

                Console.WriteLine($"| {t,5} | {timeParallel/3,10} | {timeThreads/3,8} |");
            }
        }
    }
}