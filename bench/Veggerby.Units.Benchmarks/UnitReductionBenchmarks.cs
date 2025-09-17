using BenchmarkDotNet.Attributes;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("reduction")]
public class UnitReductionBenchmarks
{
    private readonly Unit _a = Unit.SI.m;
    private readonly Unit _b = Unit.SI.s;
    private readonly Unit _c = Unit.SI.kg;

    [Benchmark]
    public Unit MultiplyThenDivideCancellation()
    {
        // (m * s * kg) / (kg * s) => m
        return (_a * _b * _c) / (_c * _b);
    }

    [Benchmark]
    public Unit NestedDivisionNormalization()
    {
        // m / (s / kg) => m * kg / s
        return _a / (_b / _c);
    }

    [Benchmark]
    public Unit PowerDistribution()
    {
        // (m * s)^3 => m^3 * s^3
        return (_a * _b) ^ 3;
    }
}