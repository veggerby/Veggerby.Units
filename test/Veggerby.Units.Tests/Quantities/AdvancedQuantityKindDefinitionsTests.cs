using System.Linq;

using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class AdvancedQuantityKindDefinitionsTests
{
    [Fact]
    public void GivenImpulse_ThenCanonicalUnitAndDimensionMatch()
    {
        // Arrange
        var kind = QuantityKinds.Impulse;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg * Unit.SI.m / Unit.SI.s);
        kind.Tags.Any(t => t.Name == "Domain.Mechanics").Should().BeTrue();
    }

    [Fact]
    public void GivenAction_ThenCanonicalUnitAndDimensionMatch()
    {
        // Arrange
        var kind = QuantityKinds.Action;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg * Unit.SI.m * Unit.SI.m / Unit.SI.s);
        kind.Tags.Any(t => t.Name == "Domain.Mechanics").Should().BeTrue();
    }

    [Fact]
    public void GivenSpecificAngularMomentum_ThenCanonicalUnitAndTags()
    {
        // Arrange
        var kind = QuantityKinds.SpecificAngularMomentum;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.m * Unit.SI.m / Unit.SI.s);
        kind.Tags.Any(t => t.Name == "Domain.Mechanics").Should().BeTrue();
    }

    [Fact]
    public void GivenVolumetricHeatCapacity_ThenCanonicalUnitAndTag()
    {
        // Arrange
        var kind = QuantityKinds.VolumetricHeatCapacity;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg / (Unit.SI.m * Unit.SI.s * Unit.SI.s * Unit.SI.K));
        kind.Tags.Any(t => t.Name == "Domain.Thermodynamic").Should().BeTrue();
    }

    [Fact]
    public void GivenSpecificWeight_ThenCanonicalUnitAndTag()
    {
        // Arrange
        var kind = QuantityKinds.SpecificWeight;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg / (Unit.SI.m * Unit.SI.m * Unit.SI.s * Unit.SI.s));
        kind.Tags.Any(t => t.Name == "Domain.Transport").Should().BeTrue();
    }

    [Fact]
    public void GivenSpectralRadiance_ThenCanonicalUnitAndTag()
    {
        // Arrange
        var kind = QuantityKinds.SpectralRadiance;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg / (Unit.SI.s * Unit.SI.s * Unit.SI.s * Unit.SI.m));
        kind.Tags.Any(t => t.Name == "Domain.Radiation").Should().BeTrue();
    }

    [Fact]
    public void GivenPartialPressure_ThenCanonicalUnitAndTag()
    {
        // Arrange
        var kind = QuantityKinds.PartialPressure;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.SI.kg / (Unit.SI.m * Unit.SI.s * Unit.SI.s));
        kind.Tags.Any(t => t.Name == "Domain.Chemistry").Should().BeTrue();
    }

    [Fact]
    public void GivenActivityCoefficient_ThenDimensionlessAndTag()
    {
        // Arrange
        var kind = QuantityKinds.ActivityCoefficient;

        // Act
        var unit = kind.CanonicalUnit;

        // Assert
        unit.Should().Be(Unit.None);
        kind.Tags.Any(t => t.Name == "Domain.Chemistry").Should().BeTrue();
    }
}