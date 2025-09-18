using AwesomeAssertions;

using Veggerby.Units.Fluent;
using Veggerby.Units.Fluent.Imperial;
using Veggerby.Units.Fluent.SI;
using Veggerby.Units.Quantities;

using Xunit;

using FluentQuantity = Veggerby.Units.Fluent.Quantity;

namespace Veggerby.Units.Tests;

public class FluentFacadeAndExtensionsTests
{
    [Fact]
    public void GivenQuantityFactory_WhenCreatingEnergyDouble_ShouldUseJouleUnit()
    {
        // Arrange
        // Act
        var e = FluentQuantity.Energy(12.5);

        // Assert
        e.Unit.Should().Be(QuantityKinds.Energy.CanonicalUnit);
        e.Value.Should().Be(12.5);
    }

    [Fact]
    public void GivenQuantityFactoryInt_WhenCreatingLength_ShouldUseMetre()
    {
        // Arrange
        // Act
        var l = FluentQuantity.Length(5);

        // Assert
        l.Unit.Should().Be(QuantityKinds.Length.CanonicalUnit);
        l.Value.Should().Be(5);
    }

    [Fact]
    public void GivenKmExtension_WhenCallingKilometers_ShouldScaleToMetres()
    {
        // Arrange
        double value = 3.2; // km

        // Act
        var m = value.Kilometers();

        // Assert
        m.Unit.Should().Be(Unit.SI.m);
        m.Value.Should().Be(3200d);
    }

    [Fact]
    public void GivenImperialFeetExtension_WhenCallingFeet_ShouldUseFootUnit()
    {
        // Arrange
        double value = 10;

        // Act
        var ft = value.Feet();

        // Assert
        ft.Unit.Should().Be(Unit.Imperial.ft);
        ft.Value.Should().Be(10d);
    }

    [Fact]
    public void GivenImperialInchExtension_WhenCallingInches_ShouldUseInchUnit()
    {
        // Arrange
        double value = 24;

        // Act
        var inches = value.Inches();

        // Assert
        inches.Unit.Should().Be(Unit.Imperial.@in);
        inches.Value.Should().Be(24d);
    }

    [Fact]
    public void GivenPoundForce_WhenUsingExtension_ShouldUseLbf()
    {
        // Arrange
        double value = 2.5;

        // Act
        var force = value.PoundForce();

        // Assert
        force.Unit.Should().Be(Unit.Imperial.lbf);
        force.Value.Should().Be(2.5d);
    }

    [Fact]
    public void GivenFootPound_WhenUsingExtension_ShouldReduceToJouleDimension()
    {
        // Arrange
        double value = 3;

        // Act
        var work = value.FootPounds();

        // Assert
        work.Unit.Dimension.Should().Be(QuantityKinds.Energy.CanonicalUnit.Dimension);
    }

    [Fact]
    public void GivenMeasurement_WhenConvertedIn_ShouldProduceEquivalentValue()
    {
        // Arrange
        var metres = 1000d.Meters();
        var km = 1d.Kilometers();

        // Act
        var metresToKm = metres.In(km.Unit);
        var kmToMetres = km.In(metres.Unit);

        // Assert
        metresToKm.Value.Should().Be(1000d); // still numeric value (no automatic scaling) because target is canonical metre base of km scaling
        kmToMetres.Value.Should().Be(1000d);
    }

    [Fact]
    public void GivenEnergyTorqueAmbiguity_WhenFormattingQualified_ShouldAppendKind()
    {
        // Arrange
        var energy = FluentQuantity.Energy(1);
        var torque = new DoubleMeasurement(1, QuantityKinds.Torque.CanonicalUnit);

        // Act
        var eFormatted = energy.Format(Formatting.UnitFormat.Qualified, QuantityKinds.Energy);
        var tFormatted = torque.Format(Formatting.UnitFormat.Qualified, QuantityKinds.Torque);

        // Assert
        eFormatted.Should().EndWith("(Energy)");
        tFormatted.Should().EndWith("(Torque)");
    }
}