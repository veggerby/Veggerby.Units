using System;

using AwesomeAssertions;

using Veggerby.Units.Reduction;
using Veggerby.Units.Tests.Infrastructure;

using Xunit;

namespace Veggerby.Units.Tests;

[Collection(ReductionSettingsCollection.Name)]
public class ReductionSettingsExceptionScopeTests
{
    private readonly ReductionSettingsFixture _fixture;

    public ReductionSettingsExceptionScopeTests(ReductionSettingsFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void GivenScopeThrows_WhenDisposed_ThenSettingsRestored()
    {
        // Arrange
        ReductionSettingsBaseline.AssertDefaults();
        var originalLazy = ReductionSettings.LazyPowerExpansion; // should be default false

        // Act
        try
        {
            using (new ReductionSettingsScope(_fixture))
            {
                ReductionSettings.LazyPowerExpansion = !originalLazy; // flip inside scope
                throw new InvalidOperationException("boom");
            }
        }
        catch
        {
            // swallow for assertion
        }

        // Assert
        ReductionSettings.LazyPowerExpansion.Should().Be(originalLazy);
        ReductionSettingsBaseline.AssertDefaults(); // full baseline verification
    }
}