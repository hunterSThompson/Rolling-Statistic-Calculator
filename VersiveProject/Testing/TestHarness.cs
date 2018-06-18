using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject.Testing
{
    /// <summary>
    /// Takes in a stream and a generator and evaluates correctness and performance of the generator.
    /// </summary>
    class TestHarness
    {
        ICheckableStream _stream;
        StatisticsGenerator _generator;
        int _frameSize1, _frameSize2;

        public TestHarness(ICheckableStream stream, StatisticsGenerator generator, int frameSize1, int frameSize2)
        {
            _stream = stream;
            _generator = generator;
            _frameSize1 = frameSize1;
            _frameSize2 = frameSize2;
        }

        public void Run(bool checkResults, bool printProgress)
        {
            int i = 0;

            var executionTimes = new List<long>();

            while (_generator.hasNext())
            {
                var sw = new Stopwatch();
                sw.Start();

                // Calculate the next result set.
                var results = _generator.getNext();
                var res1 = results[0];
                var res2 = results[1];

                sw.Stop();
                long microseconds = sw.ElapsedTicks / TimeSpan.TicksPerMillisecond;
                executionTimes.Add(microseconds);

                // Validate it
                if (checkResults)
                {
                    CheckResult(_frameSize1, i, _stream.getValues().ToArray(), res1);
                    CheckResult(_frameSize2, i, _stream.getValues().ToArray(), res2);
                }

                if (printProgress && i % 1000 == 0)
                {
                    Console.WriteLine($"Progress: {i} elements processed");
                }

                i++;
            }

            var average = executionTimes.Average();
            Console.WriteLine($"Average update time: {average} micro seconds");
        }

        void CheckValue(double target, double actual, string name, int start, int end)
        {
            if (target != actual)
            {
                Console.WriteLine($"From {start} to {end} {name} should be {target} but is {actual}");
            }
        }

        void CheckResult(int frameSize, int i, double[] stream, Statistics result)
        {
            // Select the correct portion of the list...
            int start = i < frameSize ? 0 : i - frameSize + 1;
            int count = i < frameSize ? i + 1 : frameSize;
            int end = start + count;
            var chunk = stream.ToList().GetRange(start, count);
            chunk.Sort();

            // Calculate mean/max/median
            double mean = 0.0, max = 0.0, median = 0.0;
            if (i >= frameSize - 1)
            {
                mean = chunk.Sum() / chunk.Count;
                max = chunk.Max();
                median = chunk[chunk.Count % 2 == 0 ? (chunk.Count / 2) - 1 : chunk.Count / 2];
            }

            // Check each values.
            CheckValue(result.mean, mean, "mean", start, end);
            CheckValue(result.median, median, "median", start, end);
            CheckValue(result.max, max, "max", start, end);
        }
    }
}
