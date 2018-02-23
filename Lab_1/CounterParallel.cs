using System;
using System.Threading.Tasks;

namespace Lab_1
{
    public class CounterParallel : ICounter
    {
        private int[] _array;
        private int _processorsCount;

        public CounterParallel(int[] array, int processorsCount)
        {
            _array = array;
            _processorsCount = processorsCount;
        }

        public CountResult Count()
        {
            int blockLength = (int)Math.Ceiling((double)_array.Length / _processorsCount);
            int[] maxs = new int[_processorsCount];

            Task[] tasks = new Task[_processorsCount];
            for (int i = 0; i < _processorsCount; ++i)
            {
                int j = i;
                tasks[j] = Task.Run(() =>
                {
                    int startIndex = j * blockLength;
                    int length = startIndex + blockLength <= _array.Length ? blockLength : _array.Length - startIndex;
                    int max = FindMax(startIndex, length);
                    maxs[j] = max;
                });
            }
            Task.WaitAll(tasks);

            int maxResult = maxs.Max();
            int[] counts = new int[_processorsCount];
            for (int i = 0; i < _processorsCount; ++i)
            {
                int j = i;
                tasks[j] = Task.Run(() =>
                {
                    int startIndex = j * blockLength;
                    int length = startIndex + blockLength <= _array.Length ? blockLength : _array.Length - startIndex;
                    counts[j] = Count(startIndex, length, maxResult);
                });
            }
            Task.WaitAll(tasks);

            return new CountResult(maxResult, Aggregate(counts));
        }

        private int FindMax(int startIndex, int length)
        {
            int max = _array[startIndex];
            for (int i = startIndex; (i - startIndex) < length; ++i)
            {
                if (_array[i] > max)
                {
                    max = _array[i];
                }
            }
            return max;
        }

        private int Count(int startIndex, int length, int maxValue)
        {
            int count = 0;
            int bound = maxValue / 2;
            for (int i = startIndex; (i - startIndex) < length; ++i)
            {
                if (_array[i] <= bound)
                {
                    ++count;
                }
            }
            return count;
        }

        private int Aggregate(int[] counts)
        {
            int res = 0;
            for (int i = 0; i < counts.Length; ++i)
            {
                res += counts[i];
            }
            return res;
        }
    }
}
