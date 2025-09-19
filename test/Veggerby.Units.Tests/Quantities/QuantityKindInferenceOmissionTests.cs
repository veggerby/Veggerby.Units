using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityKindInferenceOmissionTests
{
    [Fact]
    public void GivenHenrysConstantAndMoleFraction_WhenMultiplied_ThenDoesNotInferPartialPressure()
    {
        // Arrange
        var henry = new Quantity<double>(new DoubleMeasurement(1.0, QuantityKinds.HenrysConstant.CanonicalUnit), QuantityKinds.HenrysConstant);
        var moleFraction = new Quantity<double>(new DoubleMeasurement(0.5, QuantityKinds.MoleFraction.CanonicalUnit), QuantityKinds.MoleFraction);

        // Act
        var product = henry * moleFraction;

        // Assert
        // Expect no remapping to PartialPressure or Pressure; remains HenrysConstant (left operand kind retained)
        product.Kind.Should().BeSameAs(QuantityKinds.HenrysConstant);
    }

    [Fact]
    public void GivenPartialPressureAndMoleFraction_WhenDivided_ThenPressureInferredNotHenrysConstant()
    {
        // Arrange
        var partial = new Quantity<double>(new DoubleMeasurement(5000, QuantityKinds.PartialPressure.CanonicalUnit), QuantityKinds.PartialPressure);
        var moleFraction = new Quantity<double>(new DoubleMeasurement(0.25, QuantityKinds.MoleFraction.CanonicalUnit), QuantityKinds.MoleFraction);

        // Act
        var pressure = partial / moleFraction;

        // Assert
        pressure.Kind.Should().BeSameAs(QuantityKinds.Pressure);
    }
}