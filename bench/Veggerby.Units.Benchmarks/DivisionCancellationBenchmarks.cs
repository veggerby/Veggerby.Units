using BenchmarkDotNet.Attributes;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("division")]
public class DivisionCancellationBenchmarks
{
    private readonly Unit _a = Unit.SI.m;
    private readonly Unit _b = Unit.SI.s;
    private readonly Unit _c = Unit.SI.kg;
    private readonly Unit _d = Unit.SI.A;

    [Params(false, true)]
    public bool DivisionSinglePass;

    [GlobalSetup]
    public void Setup()
    {
        ReductionSettings.DivisionSinglePass = DivisionSinglePass;
    }

    [Benchmark]
    public Unit FullCancellation() => (_a * _b) / (_a * _b); // -> 1

    [Benchmark]
    public Unit PartialCancellation() => (_a * _b * _c) / (_a * _b * _d); // -> c/d

    [Benchmark]
    public Unit NoCancellation() => (_a * _b) / (_c * _d); // unreduced
}
