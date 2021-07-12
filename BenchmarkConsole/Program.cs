using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringManipulationBenchmarks>();

            Console.ReadLine();
        }
    }
}
