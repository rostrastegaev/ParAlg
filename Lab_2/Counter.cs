namespace Lab_2
{
    public class Counter : ICounter
    {
        public double[] Count(double[] array)
        {
            int initIndex = array.Length / 2 - 1;

            double[] prefixes = new double[(initIndex + 1) / 2];
            for (int i = initIndex, j = initIndex, k = 0; i >= 0 && j < array.Length; i -= 2, j += 2, ++k)
            {
                for (int l = i; l <= j; ++l)
                {
                    prefixes[k] += array[l];
                }
            }

            return prefixes;
        }
    }
}
