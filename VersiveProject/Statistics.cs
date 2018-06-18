using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    public class Statistics
    {
        public bool valid;
        public double mean, median, max;
        public Statistics(double mean, double median, double max)
        {
            this.valid = true;
            this.mean = mean;
            this.median = median;
            this.max = max;
        }
        public Statistics()
        {
            this.valid = false;
            this.mean = Double.NaN;
            this.median = Double.NaN;
            this.max = Double.NaN;
        }
        public String toString()
        {
            return "" + mean + ", " + median + ", " + max;
        }
    }
}
