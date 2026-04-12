using Veggerby.Units.Conversion;
using Veggerby.Units.Dimensions;
using Veggerby.Units.Parsing;

namespace Veggerby.Units.Tests;

/// <summary>
/// Tests covering the architectural fixes applied in the review:
/// 1. Dimension.operator!= null-safety bug
/// 2. Measurement relational operators null safety (ArgumentNullException instead of NullReferenceException)
/// 3. IEquatable implementations on Unit, Dimension, Measurement, Prefix
/// 4. ConvertTo / TryConvertTo refactored (same behaviour preserved)
/// 5. MeasurementParser int/long rounding instead of truncation
/// </summary>
public class ArchitecturalFixTests
{
    // ─────────────────────────────────────────────────────────────────────────
    // 1. Dimension.operator!= null-safety
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenNullLeftDimension_WhenCheckingInequality_ThenReturnsTrue()
    {
        // Arrange
        Dimension d1 = null;
        var d2 = Dimension.Length;

        // Act
        var result = d1 != d2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNullRightDimension_WhenCheckingInequality_ThenReturnsTrue()
    {
        // Arrange
        var d1 = Dimension.Length;
        Dimension d2 = null;

        // Act
        var result = d1 != d2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenBothNullDimensions_WhenCheckingInequality_ThenReturnsFalse()
    {
        // Arrange
        Dimension d1 = null;
        Dimension d2 = null;

        // Act
        var result = d1 != d2;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenNullLeftDimension_WhenCheckingEquality_ThenReturnsFalse()
    {
        // Arrange
        Dimension d1 = null;
        var d2 = Dimension.Length;

        // Act
        var result = d1 == d2;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenBothNullDimensions_WhenCheckingEquality_ThenReturnsTrue()
    {
        // Arrange
        Dimension d1 = null;
        Dimension d2 = null;

        // Act
        var result = d1 == d2;

        // Assert
        result.Should().BeTrue();
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 2. Measurement<T> relational operators null safety
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenNullLeftMeasurement_WhenLessThan_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v1 = null;
        var v2 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var act = () => { var _ = v1 < v2; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v1");
    }

    [Fact]
    public void GivenNullRightMeasurement_WhenLessThan_ThenThrowsArgumentNullException()
    {
        // Arrange
        var v1 = new DoubleMeasurement(5, Unit.SI.m);
        DoubleMeasurement v2 = null;

        // Act
        var act = () => { var _ = v1 < v2; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v2");
    }

    [Fact]
    public void GivenNullLeftMeasurement_WhenLessThanOrEqual_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v1 = null;
        var v2 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var act = () => { var _ = v1 <= v2; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v1");
    }

    [Fact]
    public void GivenNullLeftMeasurement_WhenGreaterThan_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v1 = null;
        var v2 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var act = () => { var _ = v1 > v2; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v1");
    }

    [Fact]
    public void GivenNullLeftMeasurement_WhenGreaterThanOrEqual_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v1 = null;
        var v2 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var act = () => { var _ = v1 >= v2; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v1");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 3. IEquatable implementations
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenTwoIdenticalUnits_WhenUsingTypedEquals_ThenReturnsTrue()
    {
        // Arrange
        Unit u1 = Unit.SI.m;
        Unit u2 = Unit.SI.m;

        // Act
        var result = u1.Equals(u2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNullUnit_WhenUsingTypedEquals_ThenReturnsFalse()
    {
        // Arrange
        Unit u1 = Unit.SI.m;

        // Act
        var result = u1.Equals((Unit)null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTwoIdenticalDimensions_WhenUsingTypedEquals_ThenReturnsTrue()
    {
        // Arrange
        Dimension d1 = Dimension.Length;
        Dimension d2 = Dimension.Length;

        // Act
        var result = d1.Equals(d2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNullDimension_WhenUsingTypedEquals_ThenReturnsFalse()
    {
        // Arrange
        Dimension d1 = Dimension.Length;

        // Act
        var result = d1.Equals((Dimension)null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTwoIdenticalMeasurements_WhenUsingTypedEquals_ThenReturnsTrue()
    {
        // Arrange
        var m1 = new DoubleMeasurement(5, Unit.SI.m);
        var m2 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var result = m1.Equals(m2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNullMeasurement_WhenUsingTypedEquals_ThenReturnsFalse()
    {
        // Arrange
        var m1 = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var result = m1.Equals((Measurement<double>)null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenTwoIdenticalPrefixes_WhenUsingTypedEquals_ThenReturnsTrue()
    {
        // Arrange
        var p1 = Prefix.k;
        var p2 = Prefix.k;

        // Act
        var result = p1.Equals(p2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GivenNullPrefix_WhenUsingTypedEquals_ThenReturnsFalse()
    {
        // Arrange
        var p1 = Prefix.k;

        // Act
        var result = p1.Equals((Prefix)null);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentPrefixes_WhenUsingTypedEquals_ThenReturnsFalse()
    {
        // Arrange
        var p1 = Prefix.k;
        var p2 = Prefix.M;

        // Act
        var result = p1.Equals(p2);

        // Assert
        result.Should().BeFalse();
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 4. ConvertTo / TryConvertTo behaviour preserved after refactor
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenMeasurementInMeters_WhenConvertingToKilometers_ThenValueScalesCorrectly()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m;
        var distance = new DoubleMeasurement(1000, Unit.SI.m);

        // Act
        var result = distance.ConvertTo(km);

        // Assert
        result.Value.Should().BeApproximately(1.0, 1e-9);
        result.Unit.Should().Be(km);
    }

    [Fact]
    public void GivenMeasurementAlreadyInTargetUnit_WhenConvertingTo_ThenReturnsSameInstance()
    {
        // Arrange
        var m = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var result = m.ConvertTo(Unit.SI.m);

        // Assert
        result.Should().BeSameAs(m);
    }

    [Fact]
    public void GivenIncompatibleDimensions_WhenTryConvertTo_ThenReturnsFalse()
    {
        // Arrange
        var m = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var success = m.TryConvertTo(Unit.SI.s, out var result);

        // Assert
        success.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void GivenCompatibleDimensions_WhenTryConvertTo_ThenReturnsTrueAndConverted()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m;
        var m = new DoubleMeasurement(500, Unit.SI.m);

        // Act
        var success = m.TryConvertTo(km, out var result);

        // Assert
        success.Should().BeTrue();
        result.Should().NotBeNull();
        result!.Value.Should().BeApproximately(0.5, 1e-9);
    }

    [Fact]
    public void GivenNullValue_WhenConvertingTo_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement m = null;

        // Act
        var act = () => m.ConvertTo(Unit.SI.m);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("value");
    }

    [Fact]
    public void GivenNullUnit_WhenConvertingTo_ThenThrowsArgumentNullException()
    {
        // Arrange
        var m = new DoubleMeasurement(5, Unit.SI.m);

        // Act
        var act = () => m.ConvertTo(null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("unit");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 5. MeasurementParser int/long rounding instead of truncation
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenDecimalValueRoundingUp_WhenParsingAsInt_ThenValueIsRounded()
    {
        // Arrange / Act
        var result = MeasurementParser.Parse<int>("3.7 m");

        // Assert
        result.Value.Should().Be(4); // rounds up, not truncates to 3
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenDecimalValueRoundingDown_WhenParsingAsInt_ThenValueIsRounded()
    {
        // Arrange / Act
        var result = MeasurementParser.Parse<int>("3.2 m");

        // Assert
        result.Value.Should().Be(3); // rounds down
        result.Unit.Should().Be(Unit.SI.m);
    }
}
