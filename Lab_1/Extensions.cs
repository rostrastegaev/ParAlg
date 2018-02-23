namespace Lab_1
{
    public static class Extensions
    {
        public static int Max(this int[] array)
        {
            int max = array[0];
            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }
    }
}
