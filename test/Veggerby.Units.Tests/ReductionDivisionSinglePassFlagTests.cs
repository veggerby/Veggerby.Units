using AwesomeAssertions;

using Veggerby.Units.Reduction;

using Xunit;

namespace Veggerby.Units.Tests;

public class ReductionDivisionSinglePassFlagTests
{
    private readonly Unit _a = Unit.SI.m;
    private readonly Unit _b = Unit.SI.s;
    private readonly Unit _c = Unit.SI.kg;
    private readonly Unit _d = Unit.SI.A;

    [Fact]
    public void GivenFullCancellationDivision_WhenComparingFlagOnOff_ThenResultsAreIdentical()
    {
        // Arrange
        var expr = (_a * _b) / (_a * _b); // => 1
        var original = ReductionSettings.DivisionSinglePass;

        try
        {
            ReductionSettings.DivisionSinglePass = false;
            var expected = expr;

            ReductionSettings.DivisionSinglePass = true;
            var actual = (_a * _b) / (_a * _b);

            // Assert
            (actual == expected).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.DivisionSinglePass = original;
        }
    }

    [Fact]
    public void GivenPartialCancellationDivision_WhenComparingFlagOnOff_ThenResultsAreIdentical()
    {
        // Arrange
        var original = ReductionSettings.DivisionSinglePass;
        try
        {
            ReductionSettings.DivisionSinglePass = false;
            var expected = (_a * _b * _c) / (_a * _b * _d); // => _c / _d

            ReductionSettings.DivisionSinglePass = true;
            var actual = (_a * _b * _c) / (_a * _b * _d);

            // Assert
            (actual == expected).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.DivisionSinglePass = original;
        }
    }

    [Fact]
    public void GivenNoCancellationDivision_WhenComparingFlagOnOff_ThenStructureIsUnchangedAndParityHolds()
    {
        // Arrange
        var leftDividend = _a * _b;
        var leftDivisor = _c * _d;
        var original = ReductionSettings.DivisionSinglePass;
        try
        {
            ReductionSettings.DivisionSinglePass = false;
            var expected = leftDividend / leftDivisor; // no common factors => unreduced division

            ReductionSettings.DivisionSinglePass = true;
            var actual = leftDividend / leftDivisor;

            // Assert
            (actual == expected).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.DivisionSinglePass = original;
        }
    }

    [Fact]
    public void GivenDeeplyNestedDivisionPattern_WhenComparingFlagOnOff_ThenResultsAreIdentical()
    {
        // Arrange   ((m/s)/(m/kg)) => kg/s
        var complex = (_a / _b) / (_a / _c);
        var original = ReductionSettings.DivisionSinglePass;
        try
        {
            ReductionSettings.DivisionSinglePass = false;
            var expected = complex;

            ReductionSettings.DivisionSinglePass = true;
            var actual = (_a / _b) / (_a / _c);

            // Assert
            (actual == expected).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.DivisionSinglePass = original;
        }
    }
}