using System;

using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class MeasurementTests
{
    [Fact]
    public void Add_SameUnit_AddsValues()
    {
        // Arrange
        var m1 = new Int32Measurement(2, Unit.SI.m);
        var m2 = new Int32Measurement(3, Unit.SI.m);

        // Act
        var sum = m1 + m2;

        // Assert
        sum.Value.Should().Be(5);
        sum.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void Add_DifferentUnits_Throws()
    {
        // Arrange
        var m1 = new Int32Measurement(2, Unit.SI.m);
        var m2 = new Int32Measurement(3, Unit.SI.s);

        // Act
        var act = () => { var _ = m1 + m2; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void Subtract_NullOperands_ReturnsOther()
    {
        // Arrange
        Int32Measurement m1 = null;
        var m2 = new Int32Measurement(3, Unit.SI.m);

        // Act
        var result = m1 - m2; // null - m2 returns m2 per operator implementation

        // Assert
        result.Should().Be(m2);
    }

    [Fact]
    public void Multiply_MultipliesValuesAndUnits()
    {
        // Arrange
        var m1 = new DoubleMeasurement(2.5, Unit.SI.m);
        var m2 = new DoubleMeasurement(4, Unit.SI.s);

        // Act
        var product = m1 * m2;

        // Assert
        product.Value.Should().Be(10d);
        product.Unit.Should().Be(Unit.SI.m * Unit.SI.s);
    }

    [Fact]
    public void Divide_DividesValuesAndUnits()
    {
        // Arrange
        var m1 = new DoubleMeasurement(10, Unit.SI.m);
        var m2 = new DoubleMeasurement(2, Unit.SI.s);

        // Act
        var quotient = m1 / m2;

        // Assert
        quotient.Value.Should().Be(5d);
        quotient.Unit.Should().Be(Unit.SI.m / Unit.SI.s);
    }

    [Fact]
    public void Comparison_AlignsUnitsBeforeComparing()
    {
        // Arrange
        var bigger = new DoubleMeasurement(1, Prefix.k * Unit.SI.m); // 1000 m
        var smaller = new DoubleMeasurement(750, Unit.SI.m);

        // Act
        var lessThan = smaller < bigger;
        var greaterThan = bigger > smaller;
        var lessOrEqual = smaller <= bigger;
        var greaterOrEqual = bigger >= smaller;

        // Assert
        lessThan.Should().BeTrue();
        greaterThan.Should().BeTrue();
        lessOrEqual.Should().BeTrue();
        greaterOrEqual.Should().BeTrue();
    }

    [Fact]
    public void Equality_WithDifferentInstancesSameValueAndUnit_IsTrue()
    {
        // Arrange
        var m1 = new DoubleMeasurement(5, Unit.SI.s);
        var m2 = new DoubleMeasurement(5, Unit.SI.s);

        // Act
        var equal = m1 == m2;

        // Assert
        equal.Should().BeTrue();
    }

    [Fact]
    public void ConvertTo_IncompatibleDimension_Throws()
    {
        // Arrange
        var time = new DoubleMeasurement(5, Unit.SI.s);

        // Act
        var act = () => time.ConvertTo(Unit.SI.m);

        // Assert
        act.Should().Throw<MeasurementConversionException>();
    }

    [Fact]
    public void ConvertTo_SameUnit_ReturnsSameInstance()
    {
        // Arrange
        var distance = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var converted = distance.ConvertTo(Unit.SI.m);

        // Assert
        ReferenceEquals(distance, converted).Should().BeTrue();
    }

    [Fact]
    public void ConvertTo_IntMeasurement_RoundsResult()
    {
        // Arrange
        var distance = new Int32Measurement(1, Prefix.k * Unit.SI.m); // 1000 m

        // Act
        var converted = distance.ConvertTo(Unit.SI.m);

        // Assert
        converted.Value.Should().Be(1000);
    }

    [Fact]
    public void Unit_Addition_WithSameUnits_ReturnsSameUnit()
    {
        // Arrange
        var u1 = Unit.SI.m;
        var u2 = Unit.SI.m;

        // Act
        var result = u1 + u2;

        // Assert
        result.Should().Be(u1);
    }

    [Fact]
    public void Unit_Addition_WithDifferentUnits_Throws()
    {
        // Arrange
        var u1 = Unit.SI.m;
        var u2 = Unit.SI.s;

        // Act
        var act = () => { var _ = u1 + u2; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void Unit_Subtraction_WithSameUnits_ReturnsSameUnit()
    {
        // Arrange
        var u1 = Unit.SI.m;
        var u2 = Unit.SI.m;

        // Act
        var result = u1 - u2;

        // Assert
        result.Should().Be(u1);
    }

    [Fact]
    public void Unit_Subtraction_WithDifferentUnits_Throws()
    {
        // Arrange
        var u1 = Unit.SI.m;
        var u2 = Unit.SI.kg;

        // Act
        var act = () => { var _ = u1 - u2; };

        // Assert
        act.Should().Throw<UnitException>();
    }

    [Fact]
    public void ConvertTo_NullValue_Throws()
    {
        // Arrange
        DoubleMeasurement value = null;

        // Act
        var act = () => value.ConvertTo(Unit.SI.m);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ConvertTo_NullTargetUnit_Throws()
    {
        // Arrange
        var value = new DoubleMeasurement(1, Unit.SI.m);

        // Act
        var act = () => value.ConvertTo(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Unit_Inequality_WithNullAndNonNull_ReturnsTrue()
    {
        // Arrange
        Unit u1 = null;
        var u2 = Unit.SI.m;

        // Act
        var result = u1 != u2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Measurement_Inequality_WithNullAndNonNull_ReturnsTrue()
    {
        // Arrange
        DoubleMeasurement m1 = null;
        var m2 = new DoubleMeasurement(1, Unit.SI.s);

        // Act
        var result = m1 != m2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void PrefixedUnit_EqualityAndHashCode()
    {
        // Arrange
        var u1 = Prefix.k * Unit.SI.m; // km
        var u2 = Prefix.k * Unit.SI.m; // km (new instance)

        // Act
        var equal = u1 == u2;
        var hashEqual = u1.GetHashCode() == u2.GetHashCode();

        // Assert
        equal.Should().BeTrue();
        hashEqual.Should().BeTrue();
    }

    [Fact]
    public void NegativePower_CreatesDivisionStructure()
    {
        // Arrange
        var expected = Unit.None / (Unit.SI.m ^ 2); // 1/m^2

        // Act
        var actual = Unit.SI.m ^ -2;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Division_NoCommonOperands_NoReductionOccurs()
    {
        // Arrange
        var dividend = Unit.SI.m * Unit.SI.kg; // m*kg
        var divisor = Unit.SI.s * Unit.SI.A; // s*A

        // Act
        var result = dividend / divisor;

        // Assert
        result.Should().Be(Unit.Divide(Unit.SI.m * Unit.SI.kg, Unit.SI.s * Unit.SI.A));
    }
}