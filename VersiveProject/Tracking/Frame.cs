﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    /// <summary>
    /// Tracks a frame of data and maintains the median/max/mean.
    /// </summary>
    public class Frame
    {
        int _size;
        double _mean, _max, _median;

        Queue<double> _queue;
        {
            _size = size;
            _meanTracker = new MeanTracker(size);
            _medianMaxTracker = new MedianMaxTracker();
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
            _max = _medianMaxTracker.Max;
}