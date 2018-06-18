using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(100, -2000, 2000, 10, 10);
            Test(1000, -2000, 2000, 100, 10);
            Test(10000, -2000, 2000, 1000, 10);
            Test(100000, -2000, 2000, 10000, 10);
            Test(1000000, -2000, 2000, 100000, 10);
            Test(5000000, -2000, 2000, 1000000, 10);

            Console.WriteLine("Complete!");
            Console.ReadKey();
        }

        static void Test(int size, int min, int max, int windowSize1, int windowSize2)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine($"Beginning test with {size} elements from {min} to {max}, with window sizes of {windowSize1} and {windowSize2}");

            var randomStream = new RandomInputStream(size, min, max);
            var generator = new MyStatsGen(windowSize1, windowSize2, randomStream);
            var testHarness = new TestHarness(randomStream, generator, windowSize1, windowSize2);
            testHarness.Run(checkResults: false, printProgress: false);

            var time = stopwatch.Elapsed;
            Console.WriteLine($"Compeleted in {time.TotalSeconds} seconds\n");
        }
    }

    interface CheckableStream : InputStream
    {
        List<double> getValues();
    }

    class TestHarness
    {
        CheckableStream _stream;
        StatisticsGenerator _generator;
        int _frameSize1, _frameSize2;

        public TestHarness(CheckableStream stream, StatisticsGenerator generator, int frameSize1, int frameSize2)
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

                var results = _generator.getNext();
                var res1 = results[0];
                var res2 = results[1];

                sw.Stop();
                long microseconds = sw.ElapsedTicks / TimeSpan.TicksPerMillisecond;
                executionTimes.Add(microseconds);

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

        void CheckResult(int frameSize, int i, double[] stream, Statistics result)
        {
            // Select the correct portion of the list...
            int start = i < frameSize ? 0 : i - frameSize + 1;
            int count = i < frameSize ? i + 1 : frameSize;
            var chunk = stream.ToList().GetRange(start, count);
            chunk.Sort();

            // Calculate mean/max/median
            var mean = chunk.Sum() / chunk.Count;
            var max = chunk.Max();
            var median = chunk[chunk.Count % 2 == 0 ? (chunk.Count / 2) - 1 : chunk.Count / 2];

            // Now check it...
            if (result.mean != mean)
            {
                Console.WriteLine($"From {start} to {start + count} mean should be {mean} but is {result.mean}");
            }
            if (result.median != median)
            {
                Console.WriteLine($"From {start} to {start + count} median should be {median} but is {result.median}");
            }
            if (result.max != max)
            {
                Console.WriteLine($"From {start} to {start + count} max should be {max} but is {result.max}");
            }
        }
    }

    class InputStreamStub : CheckableStream
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

    class RandomInputStream: CheckableStream
    {
        int _size, _min, _max, idx = 0;
        List<double> _values;

        public RandomInputStream(int size, int min, int max)
        {
            _size = size;
            _min = min;
            _max = max;
            _values = new List<double>();
        }

        public List<double> getValues()
        {
            return _values;
        }

        private double getRandom()
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return rnd.Next(_min, _max);
        }

        public double getNext()
        {
            idx++;

            var next = getRandom();
            _values.Add(next);

            return next;
        }

        public bool hasNext()
        {
            return idx < _size;
        }
    }
}
