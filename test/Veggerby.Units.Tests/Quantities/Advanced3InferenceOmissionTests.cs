using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class Advanced3InferenceOmissionTests
{
    [Fact]
    public void GivenStressAndArea_WhenMultiplied_ThenNoInferenceRule()
    {
        // Arrange
        var stress = new Quantity<double>(new DoubleMeasurement(5, QuantityKinds.Stress.CanonicalUnit), QuantityKinds.Stress);
        var area = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);

        // Act
        var act = () => _ = stress * area;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenShearStressAndArea_WhenMultiplied_ThenNoInferenceRule()
    {
        // Arrange
        var shearStress = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.ShearStress.CanonicalUnit), QuantityKinds.ShearStress);
        var area = new Quantity<double>(new DoubleMeasurement(4, QuantityKinds.Area.CanonicalUnit), QuantityKinds.Area);

        // Act
        var act = () => _ = shearStress * area;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenVorticityAndLength_WhenMultiplied_ThenNoInferenceRule()
    {
        // Arrange
        var vorticity = new Quantity<double>(new DoubleMeasurement(1, QuantityKinds.Vorticity.CanonicalUnit), QuantityKinds.Vorticity);
        var length = new Quantity<double>(new DoubleMeasurement(10, QuantityKinds.Length.CanonicalUnit), QuantityKinds.Length);

        // Act
        var act = () => _ = vorticity * length;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenLatentHeatSpecificAndMass_WhenMultiplied_ThenNoInferenceRule()
    {
        // Arrange
        var latent = new Quantity<double>(new DoubleMeasurement(100, QuantityKinds.SpecificLatentHeat.CanonicalUnit), QuantityKinds.SpecificLatentHeat);
        var mass = new Quantity<double>(new DoubleMeasurement(2, QuantityKinds.Mass.CanonicalUnit), QuantityKinds.Mass);

        // Act
        var act = () => _ = latent * mass;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenLatentHeatMolarAndAmount_WhenMultiplied_ThenNoInferenceRule()
    {
        // Arrange
        var latent = new Quantity<double>(new DoubleMeasurement(50, QuantityKinds.MolarLatentHeat.CanonicalUnit), QuantityKinds.MolarLatentHeat);
        var amount = new Quantity<double>(new DoubleMeasurement(3, QuantityKinds.CatalyticActivity.CanonicalUnit), QuantityKinds.CatalyticActivity);

        // Act
        var act = () => _ = latent * amount;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

}