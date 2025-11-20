using AwesomeAssertions;

using Veggerby.Units.Fluent.Nuclear;
using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class NuclearFluentExtensionTests
{
    [Fact]
    public void GivenDoubleValue_WhenCallingBecquerels_ShouldCreateRadioactivityMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var radioactivity = value.Becquerels();

        // Assert
        radioactivity.Unit.Should().Be(QuantityKinds.Radioactivity.CanonicalUnit);
        radioactivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingBecquerelAlias_ShouldCreateRadioactivityMeasurement()
    {
        // Arrange
        double value = 1000.0;

        // Act
        var radioactivity = value.Becquerel();

        // Assert
        radioactivity.Unit.Should().Be(QuantityKinds.Radioactivity.CanonicalUnit);
        radioactivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGrays_ShouldCreateAbsorbedDoseMeasurement()
    {
        // Arrange
        double value = 0.5;

        // Act
        var dose = value.Grays();

        // Assert
        dose.Unit.Should().Be(QuantityKinds.AbsorbedDose.CanonicalUnit);
        dose.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingGrayAlias_ShouldCreateAbsorbedDoseMeasurement()
    {
        // Arrange
        double value = 0.5;

        // Act
        var dose = value.Gray();

        // Assert
        dose.Unit.Should().Be(QuantityKinds.AbsorbedDose.CanonicalUnit);
        dose.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSieverts_ShouldCreateDoseEquivalentMeasurement()
    {
        // Arrange
        double value = 0.02;

        // Act
        var doseEquivalent = value.Sieverts();

        // Assert
        doseEquivalent.Unit.Should().Be(QuantityKinds.DoseEquivalent.CanonicalUnit);
        doseEquivalent.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingSievertAlias_ShouldCreateDoseEquivalentMeasurement()
    {
        // Arrange
        double value = 0.02;

        // Act
        var doseEquivalent = value.Sievert();

        // Assert
        doseEquivalent.Unit.Should().Be(QuantityKinds.DoseEquivalent.CanonicalUnit);
        doseEquivalent.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingRadiationExposure_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 2.58e-4;

        // Act
        var exposure = value.RadiationExposure();

        // Assert
        exposure.Unit.Should().Be(QuantityKinds.RadiationExposure.CanonicalUnit);
        exposure.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDecimalValue_WhenCallingBecquerels_ShouldCreateRadioactivityMeasurement()
    {
        // Arrange
        decimal value = 1000.0m;

        // Act
        var radioactivity = value.Becquerels();

        // Assert
        radioactivity.Unit.Should().Be(QuantityKinds.Radioactivity.CanonicalUnit);
        radioactivity.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingAbsorbedDoseRate_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 0.001;

        // Act
        var doseRate = value.AbsorbedDoseRate();

        // Assert
        doseRate.Unit.Should().Be(QuantityKinds.AbsorbedDoseRate.CanonicalUnit);
        doseRate.Value.Should().Be(value);
    }

    [Fact]
    public void GivenDoubleValue_WhenCallingEquivalentDoseRate_ShouldCreateMeasurement()
    {
        // Arrange
        double value = 0.001;

        // Act
        var doseRate = value.EquivalentDoseRate();

        // Assert
        doseRate.Unit.Should().Be(QuantityKinds.EquivalentDoseRate.CanonicalUnit);
        doseRate.Value.Should().Be(value);
    }
}
