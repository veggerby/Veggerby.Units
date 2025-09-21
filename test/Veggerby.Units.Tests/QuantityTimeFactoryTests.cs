using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityTimeFactoryTests
{
    [Fact]
    public void GivenSeconds_WhenTimeFactory_ThenQuantityHasSecondsUnit()
    {
        // Arrange
        var seconds = 42.5;

        // Act
        var q = Quantity.Time(seconds);

        // Assert
        q.Kind.Should().Be(QuantityKinds.Time);
        q.Measurement.Unit.Should().Be(QuantityKinds.Time.CanonicalUnit);
        q.Measurement.Value.Should().Be(seconds);
    }

    [Fact]
    public void GivenTimeSpan_WhenTimeFactory_ThenQuantityMatchesTotalSeconds()
    {
        // Arrange
        var span = TimeSpan.FromMinutes(2.5); // 150 seconds

        // Act
        var q = Quantity.Time(span);

        // Assert
        q.Measurement.Value.Should().Be(span.TotalSeconds);
        q.Kind.Should().Be(QuantityKinds.Time);
    }
}