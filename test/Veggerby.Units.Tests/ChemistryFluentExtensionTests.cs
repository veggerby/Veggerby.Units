using AwesomeAssertions;

using Veggerby.Units.Fluent.Chemistry;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class ChemistryFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingMoles_ShouldCreateAmountOfSubstanceMeasurement()
    {
        // Arrange
        double value = 2.5;

        // Act
        var amount = value.Moles();

        // Assert
        amount.Unit.Should().Be(Unit.SI.n);
        amount.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMoleAlias_ShouldCreateAmountOfSubstanceMeasurement()
    {
        // Arrange
        double value = 2.5;

        // Act
        var amount = value.Mole();

        // Assert
        amount.Unit.Should().Be(Unit.SI.n);
        amount.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingKilogramsPerMole_ShouldCreateMolarMassMeasurement()
    {
        // Arrange
        double value = 0.018;

        // Act
        var molarMass = value.KilogramsPerMole();

        // Assert
        molarMass.Unit.Should().Be(QuantityKinds.MolarMass.CanonicalUnit);
        molarMass.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMolarVolume_ShouldCreateMolarVolumeMeasurement()
    {
        // Arrange
        double value = 0.0224;

        // Act
        var molarVolume = value.MolarVolume();

        // Assert
        molarVolume.Unit.Should().Be(QuantityKinds.MolarVolume.CanonicalUnit);
        molarVolume.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMolesPerCubicMeter_ShouldCreateMolarConcentrationMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var concentration = value.MolesPerCubicMeter();

        // Assert
        concentration.Unit.Should().Be(QuantityKinds.MolarConcentration.CanonicalUnit);
        concentration.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingMolesPerLiter_ShouldCreateMolarConcentrationInLitersMeasurement()
    {
        // Arrange
        double value = 1.0;

        // Act
        var concentration = value.MolesPerLiter();

        // Assert
        concentration.Unit.Should().Be(Unit.SI.n / (Prefix.d * (Unit.SI.m ^ 3)));
        concentration.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingKatals_ShouldCreateCatalyticActivityMeasurement()
    {
        // Arrange
        double value = 0.5;

        // Act
        var activity = value.Katals();

        // Assert
        activity.Unit.Should().Be(QuantityKinds.CatalyticActivity.CanonicalUnit);
        activity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingReactionRate_ShouldCreateReactionRateMeasurement()
    {
        // Arrange
        double value = 100.0;

        // Act
        var rate = value.ReactionRate();

        // Assert
        rate.Unit.Should().Be(QuantityKinds.ReactionRate.CanonicalUnit);
        rate.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingDiffusionCoefficient_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 1.5e-9;

        // Act
        var diffusion = value.DiffusionCoefficient();

        // Assert
        diffusion.Unit.Should().Be(QuantityKinds.DiffusionCoefficient.CanonicalUnit);
        diffusion.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingMoles_ShouldCreateAmountOfSubstanceMeasurement()
    {
        // Arrange
        decimal value = 2.5m;

        // Act
        var amount = value.Moles();

        // Assert
        amount.Unit.Should().Be(Unit.SI.n);
        amount.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingNumberDensity_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 2.5e25;

        // Act
        var density = value.NumberDensity();

        // Assert
        density.Unit.Should().Be(QuantityKinds.NumberDensity.CanonicalUnit);
        density.Value.Should().Be(value);
    }
}
