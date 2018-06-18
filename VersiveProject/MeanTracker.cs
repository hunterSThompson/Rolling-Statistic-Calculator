using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    class MeanTracker
    {
        double _sum = 0;
        int _targetSize;
        int _currentSize = 0;

        public MeanTracker(int size)
        {
            _targetSize = size;
        }

        public double Mean { get { return _sum / _currentSize; } }

        public void Add(double newValue, double? toRemove)
        {
            if (_currentSize < _targetSize)
            {
                _currentSize++;
            }

            if (toRemove.HasValue)
            {
                _sum -= toRemove.Value;
            }
            _sum += newValue;
        }
    }
}
