﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    /// <summary>
    /// Not actually used.  A different way to track the max. Costant runtime, but O(n) extra memory.
    /// </summary>
    public class TrackingQueue
    {
        private Queue<double> _queue = new Queue<double>();
        private Stack<double> _stack = new Stack<double>();

        int _size;

        public TrackingQueue() { }
        public TrackingQueue(int size)
        {
            _size = size;
        }

        public double Max
        {
            get
            {
                if (_stack.Count == 0)
                {
                    throw new IndexOutOfRangeException("Stack is empty so there isn't a max yet!");
                }
                return _stack.Peek();
            }
        }

        public int Count { get { return _queue.Count; } }

        public void Add(double item)
        {
            _queue.Enqueue(item);

            // Check if we have a new max.
            if (_stack.Count == 0 || item >= _stack.Peek())
            {
                _stack.Push(item);
            }
        }

            {
                _stack.Pop();
            }
}