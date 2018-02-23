using System.Threading.Tasks;

namespace Lab_2
{
    public class ParallelCounter : ICounter
    {
        private int _processorsCount;

        public ParallelCounter(int processorsCount)
        {
            _processorsCount = processorsCount;
        }

        public double[] Count(double[] array)
        {
            int initIndex = array.Length / 2 - 1;
            double[] prefixes = new double[(initIndex + 1) / 2];

            Task[] tasks = new Task[_processorsCount];
            int startI = initIndex;
            int startJ = startI;
            int coeff = _processorsCount * 2;

            for (int m = 0; m < _processorsCount; ++m)
            {
                int closure = m;
                tasks[closure] = Task.Run(() =>
                {
                    for (int i = initIndex - closure * 2, j = initIndex + closure * 2, k = closure; i >= 0 && j < array.Length; i -= coeff, j += coeff, k += _processorsCount)
                    {
                        for (int l = i; l <= j; ++l)
                        {
                            prefixes[k] += array[l];
                        }
                    }
                });
            }

            Task.WaitAll(tasks);
            return prefixes;
        }
    }
}
