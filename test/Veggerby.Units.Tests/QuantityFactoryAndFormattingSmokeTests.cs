using AwesomeAssertions;

using Veggerby.Units;
using Veggerby.Units.Fluent;
using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityFactoryAndFormattingSmokeTests
{
    [Fact]
    public void GivenAngularVelocityFactory_WhenCreated_ThenCanonicalUnit()
    {
        // Arrange / Act
        var q = Veggerby.Units.Quantities.Quantity.AngularVelocity(2.5);

        // Assert
        q.Kind.Should().Be(QuantityKinds.AngularVelocity);
        q.Measurement.Unit.Symbol.Should().Be("rad/s");
    }

    [Fact]
    public void GivenThermalConductivityFactory_WhenCreated_ThenUnitFormatNonEmpty()
    {
        var q = Veggerby.Units.Quantities.Quantity.ThermalConductivity(15.2);
        q.Kind.Should().Be(QuantityKinds.ThermalConductivity);
        var formatted = UnitFormatter.Format(q.Measurement.Unit, UnitFormat.DerivedSymbols);
        formatted.Should().NotBeNullOrWhiteSpace();
        formatted.Should().Contain("K");
    }

    [Fact]
    public void GivenStressFactory_WhenFormattedDerived_ThenContainsPa()
    {
        var q = Veggerby.Units.Quantities.Quantity.Stress(123.4);
        var derived = UnitFormatter.Format(q.Measurement.Unit, UnitFormat.DerivedSymbols);
        derived.Should().Be("Pa");
    }

    [Fact]
    public void GivenMagneticFluxFactory_WhenFormattedDerived_ThenWeber()
    {
        var q = Veggerby.Units.Quantities.Quantity.MagneticFlux(3.0);
        UnitFormatter.Format(q.Measurement.Unit, UnitFormat.DerivedSymbols).Should().Be("Wb");
    }

    [Fact]
    public void GivenEnergyDimension_WhenQualified_WithKind_ThenNonEmpty()
    {
        // Create an energy measurement directly using canonical Joule unit (kg·m^2/s^2)
        var jouleUnit = Unit.SI.kg * Unit.SI.m * Unit.SI.m / (Unit.SI.s * Unit.SI.s);
        var energyMeasurement = new DoubleMeasurement(10, jouleUnit);
        var formatted = energyMeasurement.Format(UnitFormat.Qualified, QuantityKinds.Energy);
        formatted.Should().NotBeNullOrWhiteSpace();
        formatted.Should().Contain("J (Energy)");
    }

    [Fact]
    public void GivenConversionFluent_WhenSameUnit_ReturnsSameInstance()
    {
        var m = new Int32Measurement(5, Unit.SI.m);
        var result = m.In(Unit.SI.m);
        result.Should().BeSameAs(m);
    }
}