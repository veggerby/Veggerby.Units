using System;

using AwesomeAssertions;

using Veggerby.Units.Conversion;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class TemperatureSemanticsTests
{
    [Fact]
    public void GivenAbsolutes_WhenSubtract_ThenDeltaKind()
    {
        // Arrange
        var t2 = TemperatureQuantity.Absolute(20.0, Unit.SI.C);
        var t1 = TemperatureQuantity.Absolute(10.0, Unit.SI.C);

        // Act
        var d = t2 - t1;

        // Assert
        d.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        d.Measurement.Unit.Should().Be(Unit.SI.K);
        d.Measurement.Value.Should().BeApproximately(10.0, 1e-12);
    }

    [Fact]
    public void GivenFahrenheitAbsolutes_WhenSubtract_ThenDeltaScaled()
    {
        // Arrange
        var t70 = TemperatureQuantity.Absolute(70.0, Unit.Imperial.F);
        var t60 = TemperatureQuantity.Absolute(60.0, Unit.Imperial.F);

        // Act
        var d = t70 - t60;

        // Assert
        d.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        var dK = d.Measurement.ConvertTo(Unit.SI.K);
        dK.Value.Should().BeApproximately(10.0 * 5.0 / 9.0, 1e-10);
    }

    [Fact]
    public void GivenAbsoluteAndDelta_WhenAdd_ThenAbsolute()
    {
        // Arrange
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var d = TemperatureQuantity.Delta(5.0);

        // Act
        var t2 = t + d;

        // Assert
        t2.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        t2.Measurement.Value.Should().BeApproximately(305.0, 1e-12);
    }

    [Fact]
    public void GivenDeltaAndAbsolute_WhenAdd_ThenAbsolute()
    {
        // Arrange
        var d = TemperatureQuantity.Delta(5.0);
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);

        // Act
        var t2 = d + t;

        // Assert
        t2.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        t2.Measurement.Value.Should().BeApproximately(305.0, 1e-12);
    }

    [Fact]
    public void GivenAbsoluteAndDelta_WhenSubtract_ThenAbsolute()
    {
        // Arrange
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var d = TemperatureQuantity.Delta(5.0);

        // Act
        var t2 = t - d;

        // Assert
        t2.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        t2.Measurement.Value.Should().BeApproximately(295.0, 1e-12);
    }

    [Fact]
    public void GivenAbsolute_WhenScaleWithDimensionless_ThenThrows()
    {
        // Arrange
        var t = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var scalar = new DoubleMeasurement(2.0, Unit.None);

        // Act
        var act = () => _ = t * scalar;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenFahrenheitPair_WhenDelta_ThenScaledKelvin()
    {
        // Arrange
        var t70F = TemperatureQuantity.Absolute(70.0, Unit.Imperial.F);
        var t60F = TemperatureQuantity.Absolute(60.0, Unit.Imperial.F);

        // Act
        var d = TemperatureOps.Delta(t70F, t60F); // 10°F difference

        // Assert
        d.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        var dK = d.Measurement.ConvertTo(Unit.SI.K);
        dK.Value.Should().BeApproximately(10.0 * 5.0 / 9.0, 1e-12);
    }

    [Fact]
    public void GivenAbsoluteAndDelta_WhenAddDelta_ThenAbsoluteAdvanced()
    {
        // Arrange
        var t20C = TemperatureQuantity.Absolute(20.0, Unit.SI.C);
        var d5K = TemperatureQuantity.Delta(5.0); // 5 K

        // Act
        var t25C = TemperatureOps.AddDelta(t20C, d5K);

        // Assert
        t25C.Kind.Should().BeSameAs(QuantityKinds.TemperatureAbsolute);
        t25C.Measurement.Unit.Should().Be(t20C.Measurement.Unit);
        t25C.Measurement.Value.Should().BeApproximately(25.0, 1e-12);
    }

    [Fact]
    public void GivenTwoAbsolutes_WhenDeltaNegative_ThenSignPreserved()
    {
        // Arrange
        var t10C = TemperatureQuantity.Absolute(10.0, Unit.SI.C);
        var t30C = TemperatureQuantity.Absolute(30.0, Unit.SI.C);

        // Act
        var d = TemperatureOps.Delta(t10C, t30C);

        // Assert
        d.Measurement.Value.Should().BeApproximately(-20.0, 1e-12);
    }

    [Fact]
    public void GivenAbsolutePlusAbsolute_WhenAddOperatorUsed_ThenThrows()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(10.0, Unit.SI.K);
        var t2 = TemperatureQuantity.Absolute(20.0, Unit.SI.K);

        // Act
        var act = () => _ = t1 + t2;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDeltaC_WhenCreated_ThenScaleMatchesKelvin()
    {
        // Arrange / Act
        var d = TemperatureQuantity.DeltaC(12.34);

        // Assert
        d.Kind.Should().BeSameAs(QuantityKinds.TemperatureDelta);
        d.Measurement.Value.Should().BeApproximately(12.34, 1e-12); // Δ°C == K scale
    }

    [Fact]
    public void GivenDeltaF_WhenCreated_ThenScaledToKelvin()
    {
        // Arrange / Act
        var d = TemperatureQuantity.DeltaF(10.0); // 10°F difference => 10 * 5/9 K

        // Assert
        d.Measurement.Value.Should().BeApproximately(10.0 * 5.0 / 9.0, 1e-12);
    }
}