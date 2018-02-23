using System.IO;

namespace Lab_2
{
    public class FileWriter
    {
        public void Write(StreamWriter writer, double[] array)
        {
            int init = array.Length * 2;
            for (int i = init, j = init, k = 0; k < array.Length; ++k, i -= 2, j += 2)
            {
                writer.WriteLine($"S[{i}, {j}] = {array[k]}");
            }
        }
    }
}
