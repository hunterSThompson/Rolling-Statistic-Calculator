using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject.Testing
{
    /// <summary>
    /// Generates a random stream of integers of length size.
    /// Values will fall between the min and max parameter.
    /// </summary>
    class RandomInputStream : ICheckableStream
    {
        int _size, _min, _max, idx = 0;
        List<double> _values;

        public RandomInputStream(int size, int min, int max)
        {
            _size = size;
            _min = min;
            _max = max;
            _values = new List<double>();
        }

        public List<double> getValues()
        {
            return _values;
        }

        private double getRandom()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(_min, _max);
        }

        public double getNext()
        {
            idx++;

            var next = getRandom();
            _values.Add(next);

            return next;
        }

        public bool hasNext()
        {
            return idx < _size;
        }
    }
}
