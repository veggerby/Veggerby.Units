using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class TemperatureMeasurementMisuseTests
{
    [Fact]
    public void GivenAbsoluteTemperature_WhenMultipliedByScalarMeasurement_ThenThrows()
    {
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var scalar = new DoubleMeasurement(2.0, Unit.None);
        var act = () => _ = abs * scalar;
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenScalarMeasurement_WhenMultipliedByAbsoluteTemperature_ThenThrows()
    {
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var scalar = new DoubleMeasurement(2.0, Unit.None);
        var act = () => _ = scalar * abs;
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDeltaTemperature_WhenScaledByScalarMeasurementLeft_ThenScalesValue()
    {
        var d = TemperatureQuantity.Delta(5.0); // 5 K difference
        var scalar = new DoubleMeasurement(3.0, Unit.None);
        var result = scalar * d; // expected 15 K delta
        result.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        result.Measurement.Value.Should().BeApproximately(15.0, 1e-12);
    }

    [Fact]
    public void GivenDeltaTemperature_WhenScaledByScalarMeasurementRight_ThenScalesValue()
    {
        var d = TemperatureQuantity.Delta(5.0);
        var scalar = new DoubleMeasurement(4.0, Unit.None);
        var result = d * scalar;
        result.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        result.Measurement.Value.Should().BeApproximately(20.0, 1e-12);
    }

    [Fact]
    public void GivenTwoDeltaTemperatures_WhenMultiplied_ThenThrows()
    {
        var d1 = TemperatureQuantity.Delta(3.0);
        var d2 = TemperatureQuantity.Delta(4.0);
        var act = () => _ = d1 * d2;
        act.Should().Throw<InvalidOperationException>();
    }
}