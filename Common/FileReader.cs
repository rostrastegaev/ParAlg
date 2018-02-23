using System;
using System.IO;

namespace Common
{
    public class FileReader
    {
        private const char SEPARATOR = ' ';
        private Stream _stream;

        public FileReader(Stream stream)
        {
            _stream = stream;
        }

        public int[] Read()
        {
            if (!_stream.CanRead)
            {
                throw new ArgumentException("File is not available to read");
            }

            using (var reader = new StreamReader(_stream))
            {
                var arrayLength = ParseInfo(reader.ReadLine());
                var array = ParseArray(reader.ReadLine(), arrayLength);
                return array;
            }
        }

        public double[] ReadDouble()
        {
            using (var reader = new StreamReader(_stream))
            {
                string[] arrayString = reader.ReadLine().Split(' ');
                double[] array = new double[arrayString.Length];

                for (int i = 0; i < array.Length; ++i)
                {
                    array[i] = double.Parse(arrayString[i]);
                }

                return array;
            }
        }

        private int ParseInfo(string info)
        {
            var splittedInfo = SplitLine(info);
            int arrayLength;
            if (splittedInfo.Length != 1 ||
                !int.TryParse(splittedInfo[0], out arrayLength))
            {
                throw new ArgumentException("File header has invalid format");
            }

            return arrayLength;
        }

        private int[] ParseArray(string arrayStr, int arrayLength)
        {
            int[] array = new int[arrayLength];
            var splittedArray = SplitLine(arrayStr);
            if (splittedArray.Length != arrayLength)
            {
                throw new ArgumentException("Array have incorrect length");
            }

            for (int i = 0; i < arrayLength; ++i)
            {
                if (!int.TryParse(splittedArray[i], out array[i]))
                {
                    throw new ArgumentException("Invalid array element");
                }
            }

            return array;
        }

        private string[] SplitLine(string line) =>
            line.Split(new[] { SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
    }
}
