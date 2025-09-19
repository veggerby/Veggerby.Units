using BenchmarkDotNet.Attributes;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

namespace Veggerby.Units.Benchmarks;

[MemoryDiagnoser]
[BenchmarkCategory("formatting")]
public class FormattingBenchmarks
{
    private Unit _energyUnit = null!; // Joule kg·m^2/s^2
    private Unit _torqueUnit = null!; // Newton·metre kg·m^2/s^2 (same dimension)
    private Unit _complex = null!; // composite with multiple factors to stress Mixed algorithm

    [GlobalSetup]
    public void Setup()
    {
        var m = Unit.SI.m; var s = Unit.SI.s; var kg = Unit.SI.kg; var A = Unit.SI.A;
        // Joule = kg * m^2 / s^2
        _energyUnit = kg * (m ^ 2) / (s ^ 2);
        // Newton = kg * m / s^2; torque N·m = kg * m^2 / s^2 (same as Joule)
        var newton = kg * m / (s ^ 2);
        _torqueUnit = newton * m;
        // Complex synthetic: (J / s) * (J / A) => kg^2 * m^4 / (s^5 * A)
        _complex = (_energyUnit / s) * (_energyUnit / A);
    }

    [Benchmark]
    public string Energy_DerivedSymbols() => UnitFormatter.Format(_energyUnit, UnitFormat.DerivedSymbols);

    [Benchmark]
    public string Energy_Mixed() => UnitFormatter.Format(_energyUnit, UnitFormat.Mixed);

    [Benchmark]
    public string Torque_MixedQualified() => UnitFormatter.Format(_torqueUnit, UnitFormat.Mixed, QuantityKinds.Torque);

    [Benchmark]
    public string Complex_DerivedSymbols() => UnitFormatter.Format(_complex, UnitFormat.DerivedSymbols);

    [Benchmark]
    public string Complex_Mixed() => UnitFormatter.Format(_complex, UnitFormat.Mixed);
}