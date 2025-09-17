using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class MeasurementNullAndEqualityTests
{
    [Fact]
    public void GivenNullLeftMeasurement_WhenAdding_ThenReturnsRightMeasurement()
    {
        // Arrange
        Int32Measurement left = null;
        var right = new Int32Measurement(5, Unit.SI.m);

        // Act
        var sum = left + right;

        // Assert
        sum.Should().Be(right);
    }

    [Fact]
    public void GivenNullRightMeasurement_WhenAdding_ThenReturnsLeftMeasurement()
    {
        // Arrange
        var left = new Int32Measurement(5, Unit.SI.m);
        Int32Measurement right = null;

        // Act
        var sum = left + right;

        // Assert
        sum.Should().Be(left);
    }

    [Fact]
    public void GivenBothNullMeasurements_WhenAdding_ThenResultIsNull()
    {
        // Arrange
        Int32Measurement left = null;
        Int32Measurement right = null;

        // Act
        var sum = left + right;

        // Assert
        sum.Should().BeNull();
    }

    [Fact]
    public void GivenNullRightMeasurement_WhenSubtracting_ThenReturnsLeftMeasurement()
    {
        // Arrange
        var left = new Int32Measurement(5, Unit.SI.m);
        Int32Measurement right = null;

        // Act
        var diff = left - right;

        // Assert
        diff.Should().Be(left);
    }

    [Fact]
    public void GivenBothNullMeasurements_WhenSubtracting_ThenResultIsNull()
    {
        // Arrange
        Int32Measurement left = null;
        Int32Measurement right = null;

        // Act
        var diff = left - right;

        // Assert
        diff.Should().BeNull();
    }

    [Fact]
    public void GivenBothNullMeasurements_WhenCheckingEqualityOperator_ThenReturnsTrue()
    {
        // Arrange
        DoubleMeasurement a = null;
        DoubleMeasurement b = null;

        // Act
        var eq = a == b;

        // Assert
        eq.Should().BeTrue();
    }

    [Fact]
    public void GivenBothNullMeasurements_WhenCheckingInequalityOperator_ThenReturnsFalse()
    {
        // Arrange
        DoubleMeasurement a = null;
        DoubleMeasurement b = null;

        // Act
        var neq = a != b;

        // Assert
        neq.Should().BeFalse();
    }

    [Fact]
    public void GivenConvertibleButDifferentUnitInstances_WhenCheckingEquality_ThenReturnsFalse()
    {
        // Arrange
        var a = new DoubleMeasurement(1000, Unit.SI.m);
        var b = new DoubleMeasurement(1, Prefix.k * Unit.SI.m); // equal physical quantity but different unit instance

        // Act
        var eq = a == b; // no alignment in equality

        // Assert
        eq.Should().BeFalse();
    }

    [Fact]
    public void GivenMeasurement_WhenImplictlyConvertedToPrimitive_ThenReturnsUnderlyingValue()
    {
        // Arrange
        var m = new DoubleMeasurement(2.5, Unit.SI.s);

        // Act
        double value = m; // implicit

        // Assert
        value.Should().Be(2.5d);
    }

    [Fact]
    public void GivenIntLiteral_WhenImplicitlyCreatingInt32Measurement_ThenUnitIsNone()
    {
        // Arrange
        Int32Measurement m = 7; // implicit to Unit.None

        // Act
        // Assert
        m.Value.Should().Be(7);
        m.Unit.Should().Be(Unit.None);
    }

    [Fact]
    public void GivenIntLiteral_WhenImplicitlyCreatingDoubleMeasurement_ThenUnitIsNone()
    {
        // Arrange
        DoubleMeasurement m = 9; // implicit to Unit.None

        // Act
        // Assert
        m.Value.Should().Be(9d);
        m.Unit.Should().Be(Unit.None);
    }

    [Fact]
    public void GivenMeasurementsWithDifferentUnits_WhenSubtracting_ThenThrowsUnitException()
    {
        // Arrange
        var a = new Int32Measurement(5, Unit.SI.m);
        var b = new Int32Measurement(1, Unit.SI.s);

        // Act
        var act = () => { var _ = a - b; };

        // Assert
        act.Should().Throw<UnitException>();
    }
}