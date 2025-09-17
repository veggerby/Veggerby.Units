using System;

using AwesomeAssertions;

using Veggerby.Units.Conversion;

using Xunit;

namespace Veggerby.Units.Tests;

public class MeasurementTests
{
    [Fact]
    public void GivenTwoMeasurementsWithSameUnit_WhenAdding_ThenValuesSumAndUnitPreserved()
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
    public void GivenMeasurementsWithDifferentUnits_WhenAdding_ThenThrowsUnitException()
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
    public void GivenNullLeftOperand_WhenSubtracting_ThenReturnsRightOperand()
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
    public void GivenTwoMeasurements_WhenMultiplying_ThenValuesAndUnitsAreMultiplied()
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
    public void GivenTwoMeasurements_WhenDividing_ThenValuesAndUnitsAreDivided()
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
    public void GivenConvertibleMeasurements_WhenComparing_ThenUnitsAlignForComparison()
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
    public void GivenDifferentInstancesSameValueSameUnit_WhenCheckingEquality_ThenReturnsTrue()
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
    public void GivenMeasurement_WhenConvertingToIncompatibleDimension_ThenThrowsConversionException()
    {
        // Arrange
        var time = new DoubleMeasurement(5, Unit.SI.s);

        // Act
        var act = () => time.ConvertTo(Unit.SI.m);

        // Assert
        act.Should().Throw<MeasurementConversionException>();
    }

    [Fact]
    public void GivenMeasurement_WhenConvertingToSameUnit_ThenReturnsSameReference()
    {
        // Arrange
        var distance = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var converted = distance.ConvertTo(Unit.SI.m);

        // Assert
        ReferenceEquals(distance, converted).Should().BeTrue();
    }

    [Fact]
    public void GivenIntMeasurementWithPrefix_WhenConverting_ThenResultIsRounded()
    {
        // Arrange
        var distance = new Int32Measurement(1, Prefix.k * Unit.SI.m); // 1000 m

        // Act
        var converted = distance.ConvertTo(Unit.SI.m);

        // Assert
        converted.Value.Should().Be(1000);
    }

    [Fact]
    public void GivenTwoIdenticalUnits_WhenAdding_ThenReturnsSameUnitReference()
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
    public void GivenDifferentUnits_WhenAdding_ThenThrowsUnitException()
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
    public void GivenTwoIdenticalUnits_WhenSubtracting_ThenReturnsSameUnitReference()
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
    public void GivenDifferentUnits_WhenSubtracting_ThenThrowsUnitException()
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
    public void GivenNullMeasurement_WhenConverting_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement value = null;

        // Act
        var act = () => value.ConvertTo(Unit.SI.m);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GivenMeasurement_WhenConvertingToNullUnit_ThenThrowsArgumentNullException()
    {
        // Arrange
        var value = new DoubleMeasurement(1, Unit.SI.m);

        // Act
        var act = () => value.ConvertTo(null);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GivenNullAndNonNullUnit_WhenComparingInequality_ThenReturnsTrue()
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
    public void GivenNullAndNonNullMeasurement_WhenComparingInequality_ThenReturnsTrue()
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
    public void GivenEquivalentPrefixedUnits_WhenCheckingEqualityAndHashCode_ThenBothMatch()
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
    public void GivenNegativeExponent_WhenRaisingUnit_ThenCreatesReciprocalPowerStructure()
    {
        // Arrange
        var expected = Unit.None / (Unit.SI.m ^ 2); // 1/m^2

        // Act
        var actual = Unit.SI.m ^ -2;

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void GivenDivisionWithNoCommonOperands_WhenReducing_ThenStructureRemainsUnchanged()
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