using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    /// <summary>
    /// Wrapper class to emulate a Red-Black tree with duplicates in C#
    /// </summary>
    public class RbTree
    {
        SortedSet<double> set;
        Dictionary<double, int> map;

        public RbTree()
        {
            set = new SortedSet<double>();
            map = new Dictionary<double, int>();
        }

        private int _count;
        public int Count { get { return _count; } }

        public double Max { get { return set.Max; } }
        public double Min { get { return set.Min; } }

        public void Add(double item)
        {
            _count++;

            if (set.Contains(item))
            {
                map[item]++;
            }
            else
            {
                set.Add(item);
                map[item] = 1;
            }
        }


        public bool Remove(double item)
        {
            if (!set.Contains(item))
            {
                return false;
            }

            _count--;

            map[item]--;
            if (map[item] == 0)
            {
                set.Remove(item);
                map.Remove(item);
            }

            return true;
        }
    }}
