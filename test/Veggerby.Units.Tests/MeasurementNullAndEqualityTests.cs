using AwesomeAssertions;
using Xunit;

namespace Veggerby.Units.Tests;

public class MeasurementNullAndEqualityTests
{
    [Fact]
    public void Measurement_Add_NullLeft_ReturnsRight()
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
    public void Measurement_Add_NullRight_ReturnsLeft()
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
    public void Measurement_Add_BothNull_ReturnsNull()
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
    public void Measurement_Subtract_NullRight_ReturnsLeft()
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
    public void Measurement_Subtract_BothNull_ReturnsNull()
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
    public void Measurement_Equality_NullNull_True()
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
    public void Measurement_Inequality_NullNull_False()
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
    public void Measurement_Equality_DifferentConvertibleUnits_False()
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
    public void Measurement_ImplicitConversion_ReturnsValue()
    {
        // Arrange
        var m = new DoubleMeasurement(2.5, Unit.SI.s);

        // Act
        double value = m; // implicit

        // Assert
        value.Should().Be(2.5d);
    }

    [Fact]
    public void Measurement_ImplicitConstruction_FromInt()
    {
        // Arrange
        Int32Measurement m = 7; // implicit to Unit.None

        // Act
        // Assert
        m.Value.Should().Be(7);
        m.Unit.Should().Be(Unit.None);
    }

    [Fact]
    public void Measurement_ImplicitConstruction_DoubleFromInt()
    {
        // Arrange
        DoubleMeasurement m = 9; // implicit to Unit.None

        // Act
        // Assert
        m.Value.Should().Be(9d);
        m.Unit.Should().Be(Unit.None);
    }

    [Fact]
    public void Measurement_Subtract_DifferentUnits_Throws()
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
