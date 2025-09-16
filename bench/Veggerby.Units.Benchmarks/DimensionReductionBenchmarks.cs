using BenchmarkDotNet.Attributes;

using Veggerby.Units.Dimensions;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("reduction")]
public class DimensionReductionBenchmarks
{
    private readonly Dimension _l = Dimension.Length;
    private readonly Dimension _t = Dimension.Time;
    private readonly Dimension _m = Dimension.Mass;

    [Benchmark]
    public Dimension MultiplyThenDivideCancellation()
    {
        // (L * T * M)/(M * T) => L
        return (_l * _t * _m) / (_m * _t);
    }

    [Benchmark]
    public Dimension NestedDivisionNormalization()
    {
        // L / (T / M) => L * M / T
        return _l / (_t / _m);
    }

    [Benchmark]
    public Dimension PowerDistribution()
    {
        // (L * T)^4 => L^4 * T^4
        return (_l * _t) ^ 4;
    }
}