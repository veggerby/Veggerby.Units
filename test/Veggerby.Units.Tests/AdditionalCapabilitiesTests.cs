using System.Collections.Generic;

using Veggerby.Units.Calculations;
using Veggerby.Units.Fluent;

namespace Veggerby.Units.Tests;

/// <summary>
/// Tests covering the additional capabilities added after the architectural review:
/// 1. Scalar multiplication / division operators on Measurement&lt;T&gt;
/// 2. Unary negation operator on Measurement&lt;T&gt;
/// 3. IComparable&lt;Measurement&lt;T&gt;&gt; / collection sorting
/// 4. UnitException typed Unit1 / Unit2 properties
/// 5. MeasurementFormattingExtensions.In() – delegates to ConvertTo (no truncation)
/// 6. Prefix == / != operators
/// 7. Calculator&lt;T&gt;.Negate
/// </summary>
public class AdditionalCapabilitiesTests
{
    // ─────────────────────────────────────────────────────────────────────────
    // 1. Scalar multiplication / division on Measurement<T>
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenDoubleMeasurement_WhenMultipliedByScalar_ThenValueScaled()
    {
        // Arrange
        var v = new DoubleMeasurement(5.0, Unit.SI.m);

        // Act
        var result = v * 3.0;

        // Assert
        result.Value.Should().BeApproximately(15.0, 1e-10);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenDoubleMeasurement_WhenScalarMultipliedOnLeft_ThenValueScaled()
    {
        // Arrange
        var v = new DoubleMeasurement(4.0, Unit.SI.s);

        // Act
        var result = 2.0 * v;

        // Assert
        result.Value.Should().BeApproximately(8.0, 1e-10);
        result.Unit.Should().Be(Unit.SI.s);
    }

    [Fact]
    public void GivenDoubleMeasurement_WhenDividedByScalar_ThenValueScaled()
    {
        // Arrange
        var v = new DoubleMeasurement(10.0, Unit.SI.m);

        // Act
        var result = v / 4.0;

        // Assert
        result.Value.Should().BeApproximately(2.5, 1e-10);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenInt32Measurement_WhenMultipliedByScalar_ThenValueScaled()
    {
        // Arrange
        var v = new Int32Measurement(7, Unit.SI.m);

        // Act
        var result = v * 3;

        // Assert
        result.Value.Should().Be(21);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenInt32Measurement_WhenScalarMultipliedOnLeft_ThenValueScaled()
    {
        // Arrange
        var v = new Int32Measurement(6, Unit.SI.s);

        // Act
        var result = 5 * v;

        // Assert
        result.Value.Should().Be(30);
        result.Unit.Should().Be(Unit.SI.s);
    }

    [Fact]
    public void GivenDecimalMeasurement_WhenMultipliedByScalar_ThenValueScaled()
    {
        // Arrange
        var v = new DecimalMeasurement(3.5m, Unit.SI.kg);

        // Act
        var result = v * 2m;

        // Assert
        result.Value.Should().Be(7m);
        result.Unit.Should().Be(Unit.SI.kg);
    }

    [Fact]
    public void GivenNullMeasurement_WhenScalarMultiplied_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v = null;

        // Act
        var act = () => { var _ = v * 2.0; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v");
    }

    [Fact]
    public void GivenNullMeasurement_WhenScalarMultipliedOnLeft_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v = null;

        // Act
        var act = () => { var _ = 2.0 * v; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v");
    }

    [Fact]
    public void GivenNullMeasurement_WhenDividedByScalar_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v = null;

        // Act
        var act = () => { var _ = v / 2.0; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 2. Unary negation
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenPositiveDoubleMeasurement_WhenNegated_ThenValueNegated()
    {
        // Arrange
        var v = new DoubleMeasurement(9.81, Unit.SI.m / Unit.SI.s ^ 2);

        // Act
        var result = -v;

        // Assert
        result.Value.Should().BeApproximately(-9.81, 1e-10);
        result.Unit.Should().Be(v.Unit);
    }

    [Fact]
    public void GivenNegativeInt32Measurement_WhenNegated_ThenValuePositive()
    {
        // Arrange
        var v = new Int32Measurement(-5, Unit.SI.m);

        // Act
        var result = -v;

        // Assert
        result.Value.Should().Be(5);
        result.Unit.Should().Be(Unit.SI.m);
    }

    [Fact]
    public void GivenNullMeasurement_WhenNegated_ThenThrowsArgumentNullException()
    {
        // Arrange
        DoubleMeasurement v = null;

        // Act
        var act = () => { var _ = -v; };

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("v");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 3. IComparable<Measurement<T>> / collection sorting
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenListOfMeasurements_WhenSorted_ThenOrderedByValue()
    {
        // Arrange
        var measurements = new List<DoubleMeasurement>
        {
            new(3.0, Unit.SI.m),
            new(1.0, Unit.SI.m),
            new(2.0, Unit.SI.m),
        };

        // Act
        measurements.Sort();

        // Assert
        measurements[0].Value.Should().Be(1.0);
        measurements[1].Value.Should().Be(2.0);
        measurements[2].Value.Should().Be(3.0);
    }

    [Fact]
    public void GivenTwoMeasurements_WhenComparingCompatibleUnits_ThenConvertedBeforeCompare()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m;
        var v1 = new DoubleMeasurement(1.0, km);  // 1 km = 1000 m
        var v2 = new DoubleMeasurement(500.0, Unit.SI.m); // 500 m

        // Act
        var result = v1.CompareTo(v2); // 1000 m vs 500 m → positive

        // Assert
        result.Should().BePositive();
    }

    [Fact]
    public void GivenMeasurementComparedToNull_WhenComparing_ThenReturnsPositive()
    {
        // Arrange
        var v = new DoubleMeasurement(5.0, Unit.SI.m);

        // Act
        var result = v.CompareTo(null);

        // Assert
        result.Should().BePositive();
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 4. UnitException typed Unit1 / Unit2 properties
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenIncompatibleUnits_WhenAdding_ThenUnitExceptionContainsTypedUnits()
    {
        // Arrange
        var a = new DoubleMeasurement(1.0, Unit.SI.m);
        var b = new DoubleMeasurement(1.0, Unit.SI.s);
        UnitException caught = null;

        // Act
        try
        {
            var _ = a + b;
        }
        catch (UnitException ex)
        {
            caught = ex;
        }

        // Assert
        caught.Should().NotBeNull();
        caught!.Unit1.Should().Be(Unit.SI.m);
        caught.Unit2.Should().Be(Unit.SI.s);
    }

    [Fact]
    public void GivenUnitException_WhenCreated_ThenMessageContainsBothSymbols()
    {
        // Arrange / Act
        var ex = new UnitException(Unit.SI.m, Unit.SI.s);

        // Assert
        ex.Message.Should().Contain("m");
        ex.Message.Should().Contain("s");
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 5. MeasurementFormattingExtensions.In() delegates to ConvertTo (no truncation)
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenDoubleMeasurementInMeters_WhenCallingIn_ThenConvertsToKilometers()
    {
        // Arrange
        var km = Prefix.k * Unit.SI.m;
        var m = new DoubleMeasurement(1000.0, Unit.SI.m);

        // Act
        var result = m.In(km);

        // Assert
        result.Value.Should().BeApproximately(1.0, 1e-9);
        result.Unit.Should().Be(km);
    }

    [Fact]
    public void GivenDoubleMeasurementAlreadyInTargetUnit_WhenCallingIn_ThenReturnsSameInstance()
    {
        // Arrange
        var m = new DoubleMeasurement(5.0, Unit.SI.m);

        // Act
        var result = m.In(Unit.SI.m);

        // Assert
        result.Should().BeSameAs(m);
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 6. Prefix == / != operators
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenSamePrefixInstances_WhenCheckingEquality_ThenReturnsTrue()
    {
        // Arrange
        var p1 = Prefix.k;
        var p2 = Prefix.k;

        // Act / Assert
        (p1 == p2).Should().BeTrue();
        (p1 != p2).Should().BeFalse();
    }

    [Fact]
    public void GivenDifferentPrefixes_WhenCheckingEquality_ThenReturnsFalse()
    {
        // Arrange
        var p1 = Prefix.k;
        var p2 = Prefix.M;

        // Act / Assert
        (p1 == p2).Should().BeFalse();
        (p1 != p2).Should().BeTrue();
    }

    [Fact]
    public void GivenNullPrefix_WhenCheckingEquality_ThenReturnsFalse()
    {
        // Arrange
        var p1 = Prefix.k;
        Prefix p2 = null;

        // Act / Assert
        (p1 == p2).Should().BeFalse();
        (p1 != p2).Should().BeTrue();
    }

    [Fact]
    public void GivenBothNullPrefixes_WhenCheckingEquality_ThenReturnsTrue()
    {
        // Arrange
        Prefix p1 = null;
        Prefix p2 = null;

        // Act / Assert
        (p1 == p2).Should().BeTrue();
        (p1 != p2).Should().BeFalse();
    }

    // ─────────────────────────────────────────────────────────────────────────
    // 7. Calculator<T>.Negate
    // ─────────────────────────────────────────────────────────────────────────

    [Fact]
    public void GivenInt32Calculator_WhenNegating_ThenValueNegated()
    {
        // Arrange
        var calc = Int32Calculator.Instance;

        // Act / Assert
        calc.Negate(5).Should().Be(-5);
        calc.Negate(-3).Should().Be(3);
        calc.Negate(0).Should().Be(0);
    }

    [Fact]
    public void GivenDoubleCalculator_WhenNegating_ThenValueNegated()
    {
        // Arrange
        var calc = DoubleCalculator.Instance;

        // Act / Assert
        calc.Negate(3.14).Should().BeApproximately(-3.14, 1e-10);
        calc.Negate(-2.71).Should().BeApproximately(2.71, 1e-10);
    }

    [Fact]
    public void GivenDecimalCalculator_WhenNegating_ThenValueNegated()
    {
        // Arrange
        var calc = DecimalCalculator.Instance;

        // Act / Assert
        calc.Negate(5.5m).Should().Be(-5.5m);
        calc.Negate(-1.25m).Should().Be(1.25m);
    }
}
