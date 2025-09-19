using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityInferenceAsymmetryTests
{
    [Fact]
    public void GivenForceDividedByPressure_WhenReverseRuleMissing_ThenThrows()
    {
        // Arrange
        var force = new Quantity<double>(new DoubleMeasurement(10.0, QuantityKinds.Force.CanonicalUnit), QuantityKinds.Force);
        var pressure = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Pressure.CanonicalUnit), QuantityKinds.Pressure);

        // Act
        var act = () => { var _ = pressure / force; }; // Pressure / Force (no rule registered)

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenSurfaceChargeDensityDividedByElectricCharge_WhenNoRule_ThenThrows()
    {
        // Arrange
        var surfaceChargeDensity = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.SurfaceChargeDensity.CanonicalUnit), QuantityKinds.SurfaceChargeDensity);
        var electricCharge = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.ElectricCharge.CanonicalUnit), QuantityKinds.ElectricCharge);

        // Act
        var act = () => { var _ = surfaceChargeDensity / electricCharge; }; // missing rule

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenElectricChargeDividedByArea_WhenRuleExists_ThenSurfaceChargeDensity()
    {
        // Arrange
        var charge = new Quantity<double>(new DoubleMeasurement(3.0, QuantityKinds.ElectricCharge.CanonicalUnit), QuantityKinds.ElectricCharge);
        var area = new Quantity<double>(new DoubleMeasurement(1.0, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);

        // Act
        var result = charge / area; // rule exists -> SurfaceChargeDensity

        // Assert
        result.Kind.Should().Be(QuantityKinds.SurfaceChargeDensity);
    }
}