using System.IO;
using System.Linq;

namespace Lab_3
{
    public class MatrixReader
    {
        public Matrix Read(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                var array = reader.ReadLine().Split(' ').Select(int.Parse).ToArray();
                return new Matrix(array);
            }
        }
    }
}
