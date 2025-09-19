using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class Advanced3QuantityKindDefinitionsTests
{
    [Fact]
    public void GivenStress_WhenInspect_ThenMatchesPressureDimensions()
    {
        // Arrange / Act
        var k = QuantityKinds.Stress;

        // Assert
        k.CanonicalUnit.Should().Be(QuantityKinds.Pressure.CanonicalUnit);
        k.Tags.Select(t => t.Name).Should().Contain(["Domain.Mechanics", "Domain.Material"]);
    }

    [Fact]
    public void GivenDimensionlessGroups_WhenInspect_ThenAllDimensionless()
    {
        // Arrange
        var kinds = new[]
        {
            QuantityKinds.MachNumber,
            QuantityKinds.FroudeNumber,
            QuantityKinds.WeberNumber,
            QuantityKinds.PecletNumber,
            QuantityKinds.StantonNumber,
            QuantityKinds.StrouhalNumber,
            QuantityKinds.EulerNumber,
            QuantityKinds.KnudsenNumber,
            QuantityKinds.DamkohlerNumber,
            QuantityKinds.RichardsonNumber,
            QuantityKinds.IsentropicExponent,
            QuantityKinds.CoefficientOfFriction,
            QuantityKinds.PoissonRatio,
            QuantityKinds.RelativePermittivity,
            QuantityKinds.RelativePermeability,
            QuantityKinds.MagneticSusceptibility,
            QuantityKinds.ElectricSusceptibility,
            QuantityKinds.OpticalDepth
        };

        // Act / Assert
        foreach (var k in kinds)
        {
            k.CanonicalUnit.Should().Be(Unit.None);
        }
    }

    [Fact]
    public void GivenRadiantExposure_WhenInspect_ThenEnergyPerArea()
    {
        QuantityKinds.RadiantExposure.CanonicalUnit.Should().Be(QuantityKinds.Energy.CanonicalUnit / (Unit.SI.m ^ 2));
    }

    [Fact]
    public void GivenLuminousExposure_WhenInspect_ThenLuxSeconds()
    {
        QuantityKinds.LuminousExposure.CanonicalUnit.Should().Be((Unit.SI.cd / (Unit.SI.m ^ 2)) * Unit.SI.s);
    }

    [Fact]
    public void GivenHallCoefficient_WhenInspect_ThenMatchesDefinition()
    {
        QuantityKinds.HallCoefficient.CanonicalUnit.Should().Be((Unit.SI.m ^ 3) / (Unit.SI.A * Unit.SI.s));
    }

    [Fact]
    public void GivenActivityConcentration_WhenInspect_ThenBqPerM3()
    {
        QuantityKinds.ActivityConcentration.CanonicalUnit.Should().Be((1 / Unit.SI.s) / (Unit.SI.m ^ 3));
    }

    [Fact]
    public void GivenFluenceRate_WhenInspect_ThenFluencePerSecond()
    {
        QuantityKinds.FluenceRate.CanonicalUnit.Should().Be((1 / (Unit.SI.m ^ 2)) / Unit.SI.s);
    }
}