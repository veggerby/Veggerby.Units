using AwesomeAssertions;

using Veggerby.Units.Fluent;
using Veggerby.Units.Formatting;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class FormattingTests
{
    [Fact]
    public void GivenBaseUnit_WhenFormattedBaseFactors_ShouldReturnSymbol()
    {
        // Arrange
        var m = new DoubleMeasurement(1, Unit.SI.m);

        // Act
        var formatted = m.Format(UnitFormat.BaseFactors);

        // Assert
        formatted.Should().Be("1 m");
    }

    [Fact]
    public void GivenNewton_WhenDerivedSymbols_ShouldReturnN()
    {
        // Arrange
        var f = new DoubleMeasurement(1, Unit.SI.kg * (Unit.SI.m / (Unit.SI.s ^ 2))); // canonical newton

        // Act
        var formatted = f.Format(UnitFormat.DerivedSymbols);

        // Assert
        formatted.Should().Be("1 N");
    }

    [Fact]
    public void GivenNewton_WhenBaseFactors_ShouldExpand()
    {
        // Arrange
        var f = new DoubleMeasurement(1, Unit.SI.kg * (Unit.SI.m / (Unit.SI.s ^ 2)));

        // Act
        var formatted = f.Format(UnitFormat.BaseFactors);

        // Assert
        formatted.Should().Be("1 mkg/s^2");
    }

    [Fact]
    public void GivenJouleAmbiguous_WhenQualifiedEnergy_ShouldAppendKind()
    {
        // Arrange
        var e = new DoubleMeasurement(1, QuantityKinds.Energy.CanonicalUnit);

        // Act
        var formatted = e.Format(UnitFormat.Qualified, QuantityKinds.Energy);

        // Assert
        formatted.Should().Be("1 J (Energy)");
    }

    [Fact]
    public void GivenJouleAmbiguous_WhenQualifiedTorque_ShouldAppendKind()
    {
        // Arrange
        var t = new DoubleMeasurement(1, QuantityKinds.Torque.CanonicalUnit);

        // Act
        var formatted = t.Format(UnitFormat.Qualified, QuantityKinds.Torque);

        // Assert
        formatted.Should().Be("1 J (Torque)");
    }

    [Fact]
    public void GivenJoule_WhenDerivedSymbols_ShouldReturnJ()
    {
        // Arrange
        var e = new DoubleMeasurement(1, QuantityKinds.Energy.CanonicalUnit);

        // Act
        var formatted = e.Format(UnitFormat.DerivedSymbols);

        // Assert
        formatted.Should().Be("1 J");
    }

    [Fact]
    public void GivenHorsepower_WhenDerivedSymbols_ShouldFallbackToBaseFactors()
    {
        // Arrange
        var hp = new DoubleMeasurement(1, Unit.Imperial.hp);

        // Act
        var formatted = hp.Format(UnitFormat.DerivedSymbols, strict: true);

        // Assert
        formatted.Should().Be("1 W"); // hp reduces to watt dimensionally; derived symbols maps to W
    }

    [Fact]
    public void GivenFootPound_WhenQualifiedEnergy_ShouldShowBaseFactorsWithKind()
    {
        // Arrange
        var ftLb = new DoubleMeasurement(1, Unit.Imperial.ft_lb);

        // Act
        var formatted = ftLb.Format(UnitFormat.Qualified, QuantityKinds.Torque);

        // Assert
        formatted.Should().Be("1 J (Torque)"); // foot-pound shares joule dimension; qualified shows ambiguity
    }

    [Fact]
    public void GivenJouleComponents_WhenMixed_ShouldCollapseToJ()
    {
        // Arrange
        var u = Unit.SI.kg * ((Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("1 J");
    }

    [Fact]
    public void GivenNewtonSecond_WhenMixed_ShouldRenderNs()
    {
        // Arrange
        var u = (Unit.SI.kg * (Unit.SI.m / (Unit.SI.s ^ 2))) * Unit.SI.s; // N * s
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("1 N·s");
    }

    [Fact]
    public void GivenNewtonMetre_WhenMixed_ShouldRenderNm()
    {
        // Arrange
        var u = (Unit.SI.kg * (Unit.SI.m / (Unit.SI.s ^ 2))) * Unit.SI.m; // N * m
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed, QuantityKinds.Torque);

        // Assert
        formatted.Should().Be("1 N·m");
    }

    [Fact]
    public void GivenJouleDimension_WhenMixed_EnergyVsTorque_ShouldDiffer()
    {
        // Arrange
        var dim = QuantityKinds.Energy.CanonicalUnit; // kg·m^2/s^2
        var energy = new DoubleMeasurement(1, dim);
        var torque = new DoubleMeasurement(1, dim); // same dimension but different semantic kind

        // Act
        var energyFormatted = energy.Format(UnitFormat.Mixed, QuantityKinds.Energy);
        var torqueFormatted = torque.Format(UnitFormat.Mixed, QuantityKinds.Torque);

        // Assert
        energyFormatted.Should().Be("1 J (Energy)");
        torqueFormatted.Should().Be("1 N·m");
    }

    [Fact]
    public void GivenCollapsedJouleStructure_WhenMixed_ShouldStayJ()
    {
        // Arrange (kg * m^2 / s^2) – no separable metre factor beyond m^2 aggregate
        var u = Unit.SI.kg * ((Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("1 J");
    }

    [Fact]
    public void GivenWattSecondPerAmperePattern_WhenMixed_ShouldRemainWattSecondPerAmpere()
    {
        // Arrange
        var u = (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) * (Unit.SI.s / Unit.SI.A); // W * s / A
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("1 W·s/A");
    }

    [Fact]
    public void GivenVoltSecondPerAmpere_WhenMixed_ShouldPreferWattThenSecondPerAmpere()
    {
        // Arrange
        var u = (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)) * (Unit.SI.s / Unit.SI.A); // W * s / A
        var m = new DoubleMeasurement(1, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("1 W·s/A");
    }

    [Fact]
    public void GivenOhm_WhenMixed_ShouldRenderOhm()
    {
        // Arrange
        var u = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3) / (Unit.SI.A ^ 2); // Ω
        var m = new DoubleMeasurement(2, u);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("2 Ω");
    }

    [Fact]
    public void GivenDimensionless_WhenMixed_ShouldBeValueOnly()
    {
        // Arrange
        var m = new DoubleMeasurement(5, Unit.None);

        // Act
        var formatted = m.Format(UnitFormat.Mixed);

        // Assert
        formatted.Should().Be("5 ");
    }

    [Fact]
    public void GivenEnergyJoule_WhenQualifiedWithEnergyKind_ThenShowsEnergySuffix()
    {
        // Arrange
        var m = new DoubleMeasurement(1.0, (Unit.SI.kg * (Unit.SI.m ^ 2)) / (Unit.SI.s ^ 2)); // J
        var energy = QuantityKinds.Energy; // assumed existing

        // Act
        var formatted = UnitFormatter.Format(m, UnitFormat.Qualified, energy);

        // Assert
        formatted.Should().Be("1 J (Energy)");
    }

    [Fact]
    public void GivenTorqueJoule_WhenMixedWithTorqueKind_ThenShowsNewtonMetre()
    {
        // Arrange
        var torque = QuantityKinds.Torque;
        var value = new DoubleMeasurement(1.0, (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2))); // J dimension

        // Act
        var mixed = UnitFormatter.Format(value, UnitFormat.Mixed, torque);

        // Assert
        mixed.Should().Be("1 N·m");
    }

    [Fact]
    public void GivenTorqueJoule_WhenQualifiedWithTorqueKind_ThenShowsJWithTorqueSuffix()
    {
        // Arrange
        var torque = QuantityKinds.Torque;
        var value = new DoubleMeasurement(1.0, (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2)));

        // Act
        var qualified = UnitFormatter.Format(value, UnitFormat.Qualified, torque);

        // Assert
        qualified.Should().Be("1 J (Torque)");
    }

    [Fact]
    public void GivenStressPascal_WhenQualifiedWithStressKind_ThenShowsPascalWithStress()
    {
        // Arrange
        var stress = QuantityKinds.Stress; // expected existing
        var pascalValue = new DoubleMeasurement(1.0, (Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2))));

        // Act
        var qualified = UnitFormatter.Format(pascalValue, UnitFormat.Qualified, stress);

        // Assert
        qualified.Should().Be("1 Pa (Stress)");
    }

    [Fact]
    public void GivenRadiantFluxWatt_WhenQualifiedWithRadiantFluxKind_ThenShowsWattWithRadiantFlux()
    {
        // Arrange
        var radiantFlux = QuantityKinds.RadiantFlux;
        var wattValue = new DoubleMeasurement(1.0, (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3)));

        // Act
        var qualified = UnitFormatter.Format(wattValue, UnitFormat.Qualified, radiantFlux);

        // Assert
        qualified.Should().Be("1 W (RadiantFlux)");
    }

    [Fact]
    public void GivenInductanceHenry_WhenQualifiedWithInductanceKind_ThenShowsHSuffix()
    {
        // Arrange
        var inductance = QuantityKinds.Inductance;
        var henryValue = new DoubleMeasurement(1.0, (Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2) / (Unit.SI.A ^ 2)));

        // Act
        var qualified = UnitFormatter.Format(henryValue, UnitFormat.Qualified, inductance);

        // Assert
        qualified.Should().Be("1 H (Inductance)");
    }

    [Fact]
    public void GivenMagneticFieldStrength_WhenQualifiedWithFieldKind_ThenShowsBaseFactorsWithSuffix()
    {
        // Arrange
        var field = QuantityKinds.MagneticFieldStrength;
        var value = new DoubleMeasurement(1.0, Unit.SI.A / Unit.SI.m);

        // Act
        var qualified = UnitFormatter.Format(value, UnitFormat.Qualified, field);

        // Assert
        qualified.Should().Be("1 A/m"); // no derived H for A/m; remain base factors (no suffix since not ambiguous symbol)
    }
}