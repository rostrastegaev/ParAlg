using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_1
{
    class Program
    {
        private const int MaxValue = 100;
        private const string FileName = "file.txt";

        private static void Main(string[] args)
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
            if (!int.TryParse(args[1], out int arrayLength))
            {
                throw new ArgumentException("Argument is not an integer");
            }

            new FileGenerator(FileName, MaxValue).Generate(arrayLength);
        }

        private static void ProceedCounting()
        {
            if (!File.Exists(FileName))
            {
                throw new ArgumentException("File doesn't exist");
            }

            int iterations = 10;
            var array = new FileReader(File.OpenRead(FileName)).Read();
            Console.WriteLine($"Iterations: {iterations}");

            int[] results = new int[iterations];
            var counter = new Counter(array);
            var watch = new Stopwatch();
            (int max, int count) result = (0, 0);
            for (int i = 0; i < iterations; ++i)
            {
                watch.Reset();
                watch.Start();
                result = counter.Count();
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int notParallelMilliseconds = (int)Math.Floor(results.Average());
            Console.WriteLine($"\nNot parallel: max = {result.max}, count = {result.count}, avg time = {notParallelMilliseconds} millisec\n");

            for (int i = 2; i < 6; ++i)
            {
                var parallelCounter = new CounterParallel(array, i);
                for (int j = 0; j < iterations; ++j)
                {
                    watch.Reset();
                    watch.Start();
                    result = parallelCounter.Count();
                    watch.Stop();
                    results[j] = watch.Elapsed.Milliseconds;
                }
                int parallelMilliseconds = (int)Math.Floor(results.Average());
                double coefficient = (double)notParallelMilliseconds / parallelMilliseconds;
                Console.WriteLine($"Parallel ({i} cores): max = {result.max}, count = {result.count}, avg time =  {parallelMilliseconds} millisec\n");
                Console.WriteLine($"Coefficient: {coefficient:0.00}");
            }
        }
    }
}
