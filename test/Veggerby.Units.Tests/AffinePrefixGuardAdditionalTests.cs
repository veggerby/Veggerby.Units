using System;

using AwesomeAssertions;

using Xunit;

namespace Veggerby.Units.Tests;

public class AffinePrefixGuardAdditionalTests
{
    [Fact]
    public void GivenFahrenheit_WhenPrefixed_ThenThrows()
    {
        Action act = () => { var _ = Prefix.k * Unit.Imperial.F; };
        act.Should().Throw<UnitException>();
    }
}