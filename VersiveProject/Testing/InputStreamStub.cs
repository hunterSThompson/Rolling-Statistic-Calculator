using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject.Testing
{
    /// <summary>
    /// Simple test stream for debugging.
    /// </summary>
    class InputStreamStub : ICheckableStream
    {
        int idx = -1;
        public double[] lst = new double[] { 1, 2, 3, 4, 5, 6, -6, 7, 3, 4, 45, 4, 2, 2, 23, 3, 34, 34, 43, 34 };

        public double getNext()
        {
            idx++;
            return lst[idx];
        }

        public bool hasNext()
        {
            return idx < lst.Length - 1;
        }

        public List<double> getValues()
        {
            return lst.ToList();
        }
    }
}
