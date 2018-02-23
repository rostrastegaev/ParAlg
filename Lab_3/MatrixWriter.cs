using System;
using System.IO;

namespace Lab_3
{
    public class MatrixWriter
    {
        private const int Max = 100;

        public void Write(StreamWriter writer, Matrix matrix)
        {
            for (int i = 0; i < matrix.Size; ++i)
            {
                writer.Write(string.Join(" ", matrix[i]));
            }
            writer.WriteLine();
        }

        public void Generate(StreamWriter writer, int size)
        {
            var random = new Random();
            var matrix = new Matrix(size);

            for (int i = 0; i < matrix.Size; ++i)
            {
                for (int j = 0; j < matrix.Size; ++j)
                {
                    matrix[i, j] = random.Next(Max);
                }
            }

            Write(writer, matrix);
        }
    }
}
