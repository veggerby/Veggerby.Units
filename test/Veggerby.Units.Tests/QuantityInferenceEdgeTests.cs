using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityInferenceEdgeTests
{
    public QuantityInferenceEdgeTests()
    {
        // Ensure transitive inference is disabled for these edge case tests
        QuantityKindInferenceRegistry.ResetForTests();
    }

    [Fact]
    public void GivenFluxDividedByTimeTwice_WhenNoTransitiveRule_ThenSecondDivisionThrows()
    {
        var flux = new Quantity<double>(new DoubleMeasurement(5.0, QuantityKinds.MagneticFlux.CanonicalUnit), QuantityKinds.MagneticFlux);
        var time = new Quantity<double>(new DoubleMeasurement(1.0, QuantityKinds.Time.CanonicalUnit), QuantityKinds.Time);
        var voltage = flux / time; // Flux / Time -> Voltage
        var act = () => { var _ = voltage / time; }; // no rule should exist
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenKindTimesDimensionlessMeasurement_WhenNoRule_ThenKindUnitRetained()
    {
        var energy = new Quantity<double>(new DoubleMeasurement(3.0, QuantityKinds.Energy.CanonicalUnit), QuantityKinds.Energy);
        var dimensionless = new DoubleMeasurement(2.0, Unit.None);
        var result = energy.Measurement * dimensionless; // raw measurement multiplication
        result.Unit.Should().Be(energy.Measurement.Unit);
    }

    [Fact]
    public void GivenUnsupportedCompositeCombination_WhenOperated_ThenException()
    {
        var stress = new Quantity<double>(new DoubleMeasurement(2.0, QuantityKinds.Stress.CanonicalUnit), QuantityKinds.Stress);
        var inductance = new Quantity<double>(new DoubleMeasurement(3.0, QuantityKinds.Inductance.CanonicalUnit), QuantityKinds.Inductance);
        var action = () => { var _ = stress * inductance; };
        action.Should().Throw<InvalidOperationException>();
    }
}