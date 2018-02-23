using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_3
{
    class Program
    {
        private const string MatrixA = "matr_a.txt";
        private const string MatrixB = "matr_b.txt";
        private const string MatrixRes = "matr_res.txt";
        private const string MatrixResParal = "matr_res_paral.txt";
        private const int ProcessorsCount = 4;

        static void Main(string[] args)
        {
            try
            {
                if (args.Length > 0 && "-generate".Equals(args[0]))
                {
                    ProceedGeneration(args);
                }
                else
                {
                    ProceedCounting();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed! {ex.Message}");
            }
            Console.WriteLine("\ndone");
            Console.ReadKey(true);
        }

        private static void ProceedGeneration(string[] args)
        {
            if (!int.TryParse(args[1], out int size))
            {
                throw new ArgumentException("Argument is not an integer");
            }

            new MatrixWriter().Generate(MatrixA, size);
            new MatrixWriter().Generate(MatrixB, size);
        }

        private static void ProceedCounting()
        {
            int iterations = 10;
            Console.WriteLine($"Iterations: {iterations}");

            Matrix a = new MatrixReader().Read(MatrixA);
            Matrix b = new MatrixReader().Read(MatrixB);
            Matrix res = null;

            int[] results = new int[iterations];
            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < iterations; ++i)
            {
                watch.Restart();
                res = a.Sum(b);
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int nonParallel = (int)Math.Floor(results.Average());
            Console.WriteLine($"\nNot parallel: avg time = {nonParallel} millisec\n");
            new MatrixWriter().Write(MatrixRes, res);

            for (int i = 0; i < iterations; ++i)
            {
                watch.Restart();
                res = a.SumAsync(b, ProcessorsCount);
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int parallelMilliseconds = (int)Math.Floor(results.Average());
            double coefficient = (double)nonParallel / parallelMilliseconds;
            Console.WriteLine($"Parallel ({ProcessorsCount} cores) : avg time =  {parallelMilliseconds} millisec\n");
            Console.WriteLine($"Coefficient: {coefficient:0.00}");
            new MatrixWriter().Write(MatrixResParal, res);
        }
    }
}
