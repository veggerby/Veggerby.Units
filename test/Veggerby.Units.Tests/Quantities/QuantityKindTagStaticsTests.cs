using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityKindTagStaticsTests
{
    [Fact]
    public void GivenStatic_WhenRequestedByName_ThenReferenceEqual()
    {
        // Arrange
        var viaName = QuantityKindTag.Get("Domain.Mechanics");

        // Act
        var viaStatic = QuantityKindTags.DomainMechanics;

        // Assert
        viaStatic.Should().BeSameAs(viaName);
    }

    [Fact]
    public void GivenMultipleGets_WhenSameName_ThenReferenceEqual()
    {
        // Arrange
        var a = QuantityKindTag.Get("Form.Dimensionless");

        // Act
        var b = QuantityKindTag.Get("Form.Dimensionless");

        // Assert
        b.Should().BeSameAs(a);
    }

    [Fact]
    public void GivenEnergyTags_WhenUsingStatics_ThenMatchNames()
    {
        // Arrange & Act
        var state = QuantityKindTags.EnergyStateFunction;
        var path = QuantityKindTags.EnergyPathFunction;

        // Assert
        state.Name.Should().Be("Energy.StateFunction");
        path.Name.Should().Be("Energy.PathFunction");
    }
}