using AwesomeAssertions;

using Veggerby.Units.Fluent;
using Veggerby.Units.Fluent.Imperial;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class FluentCompositionTests
{
    [Fact]
    public void AddingTwoFluentLengths_SameUnit()
    {
        // Arrange
        var a = Veggerby.Units.Fluent.Quantity.Length(5.0); // m
        var b = Veggerby.Units.Fluent.Quantity.Length(7.0); // m

        // Act
        var sum = a + b;

        // Assert
        sum.Value.Should().Be(12.0);
        sum.Unit.Should().Be(QuantityKinds.Length.CanonicalUnit);
    }

    [Fact]
    public void MultiplyingLengthAndForce_YieldsCompositeUnit()
    {
        // Arrange
        var length = Veggerby.Units.Fluent.Quantity.Length(2.0); // m
        var force = Veggerby.Units.Fluent.Quantity.Force(3.0); // N

        // Act
        var workLike = length * force; // dimension m*N (J)

        // Assert
        workLike.Unit.Dimension.Should().Be(QuantityKinds.Energy.CanonicalUnit.Dimension);
    }

    [Fact]
    public void ConvertingFootPoundsToJoules_ViaMeasurementConversion()
    {
        // Arrange
        var ftLb = 10d.FootPounds();
        var jouleUnit = QuantityKinds.Energy.CanonicalUnit;

        // Act
        var converted = ftLb.ConvertTo(jouleUnit);

        // Assert
        converted.Unit.Should().Be(jouleUnit);
        converted.Value.Should().BeGreaterThan(10d); // 1 ft·lb ≈ 1.3558 J
    }

    [Fact]
    public void VelocityTimesTime_GivesLengthDimension()
    {
        // Arrange
        var v = Veggerby.Units.Fluent.Quantity.Velocity(3.0); // m/s
        var t = Veggerby.Units.Fluent.Quantity.Time(4.0);     // s

        // Act
        var displacement = v * t;

        // Assert
        displacement.Unit.Should().Be(QuantityKinds.Length.CanonicalUnit);
        displacement.Value.Should().Be(12.0);
    }
}