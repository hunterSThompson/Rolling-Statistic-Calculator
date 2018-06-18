using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VersiveProject.Testing;
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
            //Test(1000000, -2000, 2000, 100000, 10);
            //Test(5000000, -2000, 2000, 1000000, 10);

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
}
