using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersiveProject
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var stream = new InputStreamStub();
            var generator = new MyStatsGen(8, 11, stream);

            var testHarness = new TestHarness(stream, generator, 8, 11);
            testHarness.Run();

            var randomStream = new RandomInputStream(150, -100, 280);
            var generator2 = new MyStatsGen(10, 15, randomStream);
            */

            var randomStream = new RandomInputStream(150, -20, 20);
            var generator2 = new MyStatsGen(10, 5, randomStream);
            var testHarness2 = new TestHarness(randomStream, generator2, 10, 5);
            testHarness2.Run();

            Console.WriteLine("Complete!");

            /*
            // Run the generator and verify results.
            int i = 0;
            while (generator.hasNext())
            {
                var results = generator.getNext();
                var res1 = results[0];
                var res2 = results[1];

                CheckResult(3, i, stream.lst, res1);
                CheckResult(5, i, stream.lst, res2);

                i++;
            }
            */

            Console.ReadKey();
        }

        /*
        static void CheckResult(int frameSize, int i, double[] stream, Statistics result)
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
        */
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

        public void Run()
        {
            // Run the generator and verify results.
            int i = 0;
            while (_generator.hasNext())
            {
                var results = _generator.getNext();
                var res1 = results[0];
                var res2 = results[1];

                CheckResult(_frameSize1, i, _stream.getValues().ToArray(), res1);
                CheckResult(_frameSize2, i, _stream.getValues().ToArray(), res2);

                i++;
            }
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
