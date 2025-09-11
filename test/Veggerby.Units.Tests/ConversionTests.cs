using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class ConversionTests
{
    [Fact]
    public void Convert_Kilometre_To_Metre()
    {
        var m = new DoubleMeasurement(3.5, Prefix.k * Unit.SI.m); // 3.5 km
        var converted = m.ConvertTo(Unit.SI.m);
        Assert.Equal(3500d, (double)converted, 10);
    }

    [Fact]
    public void Convert_Foot_To_Metre()
    {
        var m = new DoubleMeasurement(10, Unit.Imperial.ft);
        var converted = m.ConvertTo(Unit.SI.m);
        Assert.Equal(10 * ImperialUnitSystem.FeetToMetres, (double)converted, 10);
    }

    [Fact]
    public void AlignUnits_ComparesDifferentButCompatible()
    {
        var v1 = new DoubleMeasurement(1, Prefix.k * Unit.SI.m); // 1000 m
        var v2 = new DoubleMeasurement(500, Unit.SI.m);
        Assert.True(v2 < v1); // 500 m < 1000 m
    }
}