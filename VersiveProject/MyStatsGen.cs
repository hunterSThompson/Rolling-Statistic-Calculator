using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    public class MyStatsGen : StatisticsGenerator
    {
        InputStream inputStream;

        Frame frame1;
        Frame frame2;

        public MyStatsGen(int window_size1, int window_size2, InputStream input_stream)
        {
            frame1 = new Frame(window_size1);
            frame2 = new Frame(window_size2);

            inputStream = input_stream;
        }

        public bool hasNext()
        {
            return inputStream.hasNext();
        }

        public Statistics[] getNext()
        {
            // Get the next integer in the stream
            double nextNum = inputStream.getNext();

            // Update the frames
            frame1.Add(nextNum);
            frame2.Add(nextNum);

            // Create Statistic objects
            var stat1 = new Statistics(frame1.Mean, frame1.Median, frame1.Max);
            var stat2 = new Statistics(frame2.Mean, frame2.Median, frame2.Max);

            Statistics[] result = new Statistics[] { stat1, stat2 };
            return result;
        }
    }
}
