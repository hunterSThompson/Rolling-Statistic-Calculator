using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    class MedianMaxTracker
    {
        RbTree lower = new RbTree();
        RbTree upper = new RbTree();
        
        double _mean = double.MaxValue;

        public double Median { get { return _mean; } }

        public double Max {
            get
            {
                if (upper.Count == 0) return lower.Max; // Edge case. Upper tree might be empty!
                return upper.Max;
            }
        }

        public void Add(double newValue, double? toRemove)
        {
            double upperTreeMin = upper.Count > 0 ? upper.Min : double.MaxValue;

            // First remove the old value if there is one.
            if (toRemove.HasValue)
            {
                if (!lower.Remove(toRemove.Value))
                {
                    upper.Remove(toRemove.Value);
                }
            }

            // Determine which tree to insert into
            if (newValue <= upperTreeMin)
            {
                lower.Add(newValue);
            }
            else
            {
                upper.Add(newValue);
            }
            
            // Now rebalance the trees if necessary
            if (upper.Count > lower.Count)
            {
                ShiftToLowTree();
            }
            else
            {
                ShitToUpperTree();
            }

            // Now update the mean
            _mean = lower.Count >= upper.Count ? lower.Max : upper.Min;
        }

        void ShitToUpperTree()
        {
            // If the lower tree is too big, shift it's maximum to the upper tree
            while (lower.Count - upper.Count > 1)
            {
                double maximum = lower.Max;
                lower.Remove(maximum);
                upper.Add(maximum);
            }
        }

        void ShiftToLowTree()
        {
            // If the upper tree is too big, shift it's minimum to the lower tree
            while (upper.Count - lower.Count > 1)
            {
                double mininum = upper.Min;
                upper.Remove(mininum);
                lower.Add(mininum);
            }
        }
    }
}
