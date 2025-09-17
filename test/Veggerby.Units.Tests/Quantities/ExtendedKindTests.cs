using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class ExtendedKindTests
{
    [Fact]
    public void GivenMassDensityAndMassConcentration_WhenCompared_ThenKindsAreDistinctButDimensionsEqual()
    {
        // Arrange
        var density = Quantity.MassDensity(1000);
        var concentration = Quantity.MassConcentration(1000);

        // Act
        var densityDimMatches = QuantityKinds.MassDensity.Matches(QuantityKinds.MassConcentration.CanonicalUnit);

        // Assert
        density.Kind.Should().NotBeSameAs(concentration.Kind);
        density.Kind.CanonicalUnit.Should().Be(concentration.Kind.CanonicalUnit);
        densityDimMatches.Should().BeTrue();
    }

    [Fact]
    public void GivenElectricChargeFactory_WhenCreated_ThenTagPresent()
    {
        // Arrange
        var charge = Quantity.ElectricCharge(5);

        // Act
        var hasTag = charge.Kind.HasTag("Domain.Electromagnetic");

        // Assert
        hasTag.Should().BeTrue();
    }

    [Fact]
    public void GivenSpecificEnergyAndSpecificPower_WhenDimensionsInspected_ThenDistinct()
    {
        // Arrange
        var se = Quantity.SpecificEnergy(1);
        var sp = Quantity.SpecificPower(1);

        // Act
        var dimEnergy = se.Kind.CanonicalUnit.Dimension;
        var dimPower = sp.Kind.CanonicalUnit.Dimension;

        // Assert
        dimEnergy.Should().NotBe(dimPower);
    }

    [Fact]
    public void GivenHeatFlux_WhenCreated_ThenHasPathFunctionTag()
    {
        // Arrange
        var hf = Quantity.HeatFlux(10);

        // Act
        var hasTag = hf.Kind.HasTag("Energy.PathFunction");

        // Assert
        hasTag.Should().BeTrue();
    }

    [Fact]
    public void GivenMagneticFluxDensityFactory_WhenCreated_ThenMatchesCanonicalDimension()
    {
        // Arrange
        var b = Quantity.MagneticFluxDensity(2);

        // Act
        var matches = b.Kind.Matches(b.Measurement.Unit);

        // Assert
        matches.Should().BeTrue();
    }
}