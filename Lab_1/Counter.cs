namespace Lab_1
{
    public class Counter : ICounter
    {
        private int[] _array;

        public Counter(int[] array)
        {
            _array = array;
        }

        public CountResult Count()
        {
            int min = _array.Max();
            return new CountResult(min, Count(min));
        }

        private int Count(int maxValue)
        {
            int count = 0;
            int bound = maxValue / 2;
            for (int i = 0; i < _array.Length; ++i)
            {
                if (_array[i] <= bound)
                {
                    ++count;
                }
            }
            return count;
        }
    }
}
