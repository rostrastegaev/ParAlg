using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lab_2
{
    class Program
    {
        private const int MaxValue = 100;
        private const string FileName = "numbers.txt";
        private const string ResultFile = "result.txt";
        private const string ParallelResultFile = "result_parallel.txt";
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
            int arrayLength;
            if (!int.TryParse(args[1], out arrayLength))
            {
                throw new ArgumentException("Argument is not an integer");
            }

            new FileGenerator(FileName, MaxValue).GenerateDouble(arrayLength);
        }

        private static void ProceedCounting()
        {
            int iterations = 10;
            var array = new FileReader(File.OpenRead(FileName)).ReadDouble();
            Console.WriteLine($"Iterations: {iterations}");

            int[] results = new int[iterations];
            var counter = new Counter();
            var watch = new Stopwatch();
            double[] result = null;
            for (int i = 0; i < iterations; ++i)
            {
                watch.Restart();
                result = counter.Count(array);
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int nonParallel = (int)Math.Floor(results.Average());
            Console.WriteLine($"\nNot parallel: avg time = {nonParallel} millisec\n");
            new FileWriter().Write(File.CreateText(ResultFile), result);

            var paralCounter = new ParallelCounter(ProcessorsCount);
            for (int i = 0; i < iterations; ++i)
            {
                watch.Restart();
                result = paralCounter.Count(array);
                watch.Stop();
                results[i] = watch.Elapsed.Milliseconds;
            }

            int parallelMilliseconds = (int)Math.Floor(results.Average());
            double coefficient = (double)nonParallel / parallelMilliseconds;
            Console.WriteLine($"Parallel ({ProcessorsCount} cores) : avg time =  {parallelMilliseconds} millisec\n");
            Console.WriteLine($"Coefficient: {coefficient:0.00}");
            new FileWriter().Write(File.CreateText(ParallelResultFile), result);
        }
    }
}
