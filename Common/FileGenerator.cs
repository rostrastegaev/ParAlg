using System;
using System.IO;

namespace Common
{
    public class FileGenerator
    {
        private string _fileName;
        private int _maxValue;

        public FileGenerator(string fileName, int maxValue)
        {
            _fileName = fileName;
            _maxValue = maxValue;
        }

        public void Generate(int arrayLength)
        {
            var rand = new Random();
            int[] array = new int[arrayLength];

            for (int i = 0; i < arrayLength; ++i)
            {
                array[i] = rand.Next(_maxValue);
            }

            using (var stream = File.Create(_fileName))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{arrayLength}");
                writer.WriteLine(string.Join(" ", array));
            }
        }

        public void GenerateDouble(int arrayLength)
        {
            var rand = new Random();
            double[] array = new double[arrayLength];

            for (int i = 0; i < arrayLength; ++i)
            {
                array[i] = rand.NextDouble();
            }

            using (var stream = File.Create(_fileName))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine(string.Join(" ", array));
            }
        }
    }
}
