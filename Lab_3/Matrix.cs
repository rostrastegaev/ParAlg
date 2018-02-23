using System;
using System.Threading.Tasks;

namespace Lab_3
{
    public class Matrix
    {
        private int[][] _matrix;
        private int _size;

        public int Size
        {
            get { return _size; }
        }

        public int this[int i, int j]
        {
            get { return _matrix[i][j]; }
            set { _matrix[i][j] = value; }
        }

        public int[] this[int i]
        {
            get { return _matrix[i]; }
        }

        public Matrix(int size)
        {
            Init(size);
        }

        public Matrix(int[][] array)
        {
            _size = array[0].Length;
            _matrix = array;
        }

        private void Init(int size)
        {
            _size = size;
            _matrix = new int[_size][];
            for (int i = 0; i < _size; ++i)
            {
                _matrix[i] = new int[_size];
            }
        }

        public Matrix Sum(Matrix matrix)
        {
            if (!HasSameSize(matrix))
            {
                throw new Exception("Matrix have different size");
            }

            var result = new Matrix(_size);
            for (int i = 0; i < _size; ++i)
            {
                for (int j = 0; j < _size; ++j)
                {
                    result[i, j] = _matrix[i][j] + matrix[i, j];
                }
            }

            return result;
        }

        public Matrix SumAsync(Matrix matrix, int processorsCount)
        {
            if (!HasSameSize(matrix))
            {
                throw new Exception("Matrix have different size");
            }

            var result = new Matrix(_size);
            var tasks = new Task[processorsCount];

            for (int k = 0; k < processorsCount; ++k)
            {
                int closure = k;
                tasks[closure] = Task.Run(() =>
                {
                    for (int i = closure; i < _size; i += processorsCount)
                    {
                        for (int j = 0; j < _size; ++j)
                        {
                            result[i, j] = _matrix[i][j] + matrix[i, j];
                        }
                    }
                });
            }

            Task.WaitAll(tasks);
            return result;
        }

        private bool HasSameSize(Matrix matrix) =>
            _size == matrix._size;
    }
}
