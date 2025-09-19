using System;

using AwesomeAssertions;

using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class TemperatureAffineNegativeTests
{
    [Fact]
    public void GivenAddDelta_WhenDeltaIsAbsolute_ThenThrows()
    {
        // Arrange
        var abs = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var wrong = TemperatureQuantity.Absolute(5.0, Unit.SI.K); // should be delta

        // Act
        var act = () => TemperatureOps.AddDelta(abs, wrong);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*Expected 'TemperatureDelta'*got 'TemperatureAbsolute'*");
    }

    [Fact]
    public void GivenDelta_WhenDeltaCalledWithDeltaAndAbsoluteSwapped_ThenThrows()
    {
        // Arrange
        var delta = TemperatureQuantity.Delta(2.0);
        var absolute = TemperatureQuantity.Absolute(300.0, Unit.SI.K);

        // Act
        var act = () => TemperatureOps.Delta(delta, absolute); // first param must be absolute

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("*Expected 'TemperatureAbsolute'*got 'TemperatureDelta'*");
    }

    [Fact]
    public void GivenTwoAbsoluteQuantities_WhenMultiplied_ThenThrows()
    {
        // Arrange
        var t1 = TemperatureQuantity.Absolute(300.0, Unit.SI.K);
        var t2 = TemperatureQuantity.Absolute(310.0, Unit.SI.K);

        // Act
        var act = () => _ = t1 * t2;

        // Assert
        act.Should().Throw<InvalidOperationException>(); // Quantity layer forbids incompatible kinds
    }

    [Fact]
    public void GivenAbsoluteTemperatureUnit_WhenAttemptPrefixOnUnit_ThenThrows()
    {
        // Arrange
        var unit = Unit.SI.C; // affine

        // Act
        var act = () => _ = 2 * unit; // triggers UnitException internally

        // Assert
        act.Should().Throw<UnitException>();
    }
}