using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    public class Frame
    {
        int _size;
        double _mean, _max, _median;

        Queue<double> _queue;        MeanTracker _meanTracker;        MedianMaxTracker _medianMaxTracker;        public double Mean { get { return _mean; } }        public double Max { get { return _max; } }        public double Median { get { return _median; } }        public Frame(int size)
        {
            _size = size;
            _meanTracker = new MeanTracker(size);
            _medianMaxTracker = new MedianMaxTracker();            _queue = new Queue<double>();        }        public void Add(double newValue)
        {
            // First enqueue the new item
            _queue.Enqueue(newValue);

            // Pop out the oldest value only IF we are overcapacity
            double? oldValue = null;
            if (_queue.Count > _size)
            {
                oldValue = _queue.Dequeue();
            }

            // Update the mean & median trackers with the new / old values
            _meanTracker.Add(newValue, oldValue);
            _medianMaxTracker.Add(newValue, oldValue);

            // Read & save the new max, mean, and median values
            _mean = _meanTracker.Mean;
            _max = _medianMaxTracker.Max;            _median = _medianMaxTracker.Median;        }    }
}
