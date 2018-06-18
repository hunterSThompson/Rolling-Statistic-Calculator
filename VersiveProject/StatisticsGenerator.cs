using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    public interface StatisticsGenerator
    {
        bool hasNext();
        Statistics[] getNext();
    }
}
