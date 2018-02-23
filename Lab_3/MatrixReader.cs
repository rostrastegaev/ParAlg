using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_3
{
    public class MatrixReader
    {
        public Matrix Read(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                List<int[]> arrays = new List<int[]>();
                while (!reader.EndOfStream)
                {
                    var array = reader.ReadLine().Split('\t').Select(int.Parse).ToArray();
                    arrays.Add(array);
                }
                return new Matrix(arrays.ToArray());
            }
        }
    }
}
