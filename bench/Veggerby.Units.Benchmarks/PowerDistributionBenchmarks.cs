using BenchmarkDotNet.Attributes;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("power")]
public class PowerDistributionBenchmarks
{
    private readonly Unit _a = Unit.SI.m * Unit.SI.s * Unit.SI.kg; // composite

    [Params(false, true)]
    public bool Lazy;

    [GlobalSetup]
    public void Setup() => ReductionSettings.LazyPowerExpansion = Lazy;

    [Benchmark]
    public Unit PowerCube() => _a ^ 3;

    [Benchmark]
    public Unit PowerFourth() => _a ^ 4;
}