using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
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
        public double Remove()        {            double toReturn = _queue.Dequeue();            // Did we just dequeue the current max value? If so, pop it off the stack.            if (toReturn == _stack.Peek())
            {
                _stack.Pop();
            }            return toReturn;        }    }
}
