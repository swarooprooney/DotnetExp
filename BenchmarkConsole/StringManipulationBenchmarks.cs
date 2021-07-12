using BenchmarkDotNet.Attributes;

namespace BenchmarkConsole
{
    [MemoryDiagnoser]
    [RankColumn]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [HtmlExporter]
    public class StringManipulationBenchmarks
    {
        private const string source = "are we equal";
        private const string destination = "are we equal";
        private static readonly StringManipulation stringManipulation = new StringManipulation();

        [Benchmark(Baseline =true)]
        public void CheckIfStringIsEqual()
        {
            stringManipulation.CheckIfStringIsEqual(source,destination);
        }

        [Benchmark]
        public void CheckIfStringIsEqualInBuilt()
        {
            stringManipulation.CheckIfStringIsEqualInBuilt(source, destination);
        }

    }
}
