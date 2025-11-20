using System.Linq;

using AwesomeAssertions;

using Veggerby.Units.Analysis;
using Veggerby.Units.Dimensions;

using Xunit;

namespace Veggerby.Units.Tests.Analysis;

public class DimensionExplainerTests
{
    [Fact]
    public void GivenBasicUnit_WhenExplainingDerivation_ThenReturnsDerivationSteps()
    {
        // Arrange
        var unit = Unit.SI.m;

        // Act
        var derivation = DimensionExplainer.ExplainDerivation(unit);

        // Assert
        derivation.OriginalUnit.Should().Be(unit);
        derivation.FinalDimension.Should().Be(Dimension.Length);
        derivation.Steps.Should().NotBeEmpty();
    }

    [Fact]
    public void GivenCompositeUnit_WhenExplainingDerivation_ThenReturnsMultipleSteps()
    {
        // Arrange
        var unit = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);

        // Act
        var derivation = DimensionExplainer.ExplainDerivation(unit);

        // Assert
        derivation.OriginalUnit.Should().Be(unit);
        derivation.FinalDimension.Should().Be(Dimension.Mass * Dimension.Length / (Dimension.Time ^ 2));
        derivation.Steps.Should().NotBeEmpty();
    }

    [Fact]
    public void GivenNullUnit_WhenExplainingDerivation_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit unit = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionExplainer.ExplainDerivation(unit));
    }

    [Fact]
    public void GivenCompatibleUnits_WhenExplainingConversion_ThenReturnsPossiblePath()
    {
        // Arrange
        var from = Unit.SI.m;
        var to = Unit.Imperial.ft;

        // Act
        var path = DimensionExplainer.ExplainConversion(from, to);

        // Assert
        path.IsPossible.Should().BeTrue();
        path.Explanation.Should().Contain("possible");
    }

    [Fact]
    public void GivenIncompatibleUnits_WhenExplainingConversion_ThenReturnsImpossiblePath()
    {
        // Arrange
        var from = Unit.SI.m;
        var to = Unit.SI.kg;

        // Act
        var path = DimensionExplainer.ExplainConversion(from, to);

        // Assert
        path.IsPossible.Should().BeFalse();
        path.Explanation.Should().Contain("not possible");
    }

    [Fact]
    public void GivenNullFromUnit_WhenExplainingConversion_ThenThrowsArgumentNullException()
    {
        // Arrange
        Unit from = null;
        var to = Unit.SI.m;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionExplainer.ExplainConversion(from, to));
    }

    [Fact]
    public void GivenNullToUnit_WhenExplainingConversion_ThenThrowsArgumentNullException()
    {
        // Arrange
        var from = Unit.SI.m;
        Unit to = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => DimensionExplainer.ExplainConversion(from, to));
    }
}

public class MeasurementValidatorTests
{
    [Fact]
    public void GivenValidSchema_WhenConstructing_ThenSucceeds()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "length", Dimension.Length },
            { "mass", Dimension.Mass }
        };

        // Act
        var validator = new MeasurementValidator(schema);

        // Assert
        validator.Should().NotBeNull();
    }

    [Fact]
    public void GivenNullSchema_WhenConstructing_ThenThrowsArgumentNullException()
    {
        // Arrange
        System.Collections.Generic.Dictionary<string, Dimension> schema = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => new MeasurementValidator(schema));
    }

    [Fact]
    public void GivenValidMeasurements_WhenValidatingCollection_ThenReturnsAllValid()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "distance", Dimension.Length },
            { "weight", Dimension.Mass }
        };
        var validator = new MeasurementValidator(schema);

        var measurements = new System.Collections.Generic.Dictionary<string, Measurement<double>>
        {
            { "distance", new DoubleMeasurement(100, Unit.SI.m) },
            { "weight", new DoubleMeasurement(50, Unit.SI.kg) }
        };

        // Act
        var report = validator.ValidateCollection(measurements);

        // Assert
        report.TotalItems.Should().Be(2);
        report.ValidItems.Should().Be(2);
        report.InvalidItems.Should().Be(0);
        report.Failures.Should().BeEmpty();
    }

    [Fact]
    public void GivenInvalidMeasurement_WhenValidatingCollection_ThenReportsFailure()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "distance", Dimension.Length },
            { "weight", Dimension.Mass }
        };
        var validator = new MeasurementValidator(schema);

        var measurements = new System.Collections.Generic.Dictionary<string, Measurement<double>>
        {
            { "distance", new DoubleMeasurement(100, Unit.SI.kg) }, // Wrong dimension
            { "weight", new DoubleMeasurement(50, Unit.SI.kg) }
        };

        // Act
        var report = validator.ValidateCollection(measurements);

        // Assert
        report.TotalItems.Should().Be(2);
        report.ValidItems.Should().Be(1);
        report.InvalidItems.Should().Be(1);
        report.Failures.Should().HaveCount(1);
        report.Failures[0].ItemKey.Should().Be("distance");
    }

    [Fact]
    public void GivenUnknownKey_WhenValidatingCollection_ThenReportsFailure()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "distance", Dimension.Length }
        };
        var validator = new MeasurementValidator(schema);

        var measurements = new System.Collections.Generic.Dictionary<string, Measurement<double>>
        {
            { "unknown", new DoubleMeasurement(100, Unit.SI.m) }
        };

        // Act
        var report = validator.ValidateCollection(measurements);

        // Assert
        report.TotalItems.Should().Be(1);
        report.ValidItems.Should().Be(0);
        report.InvalidItems.Should().Be(1);
        report.Failures.Should().HaveCount(1);
        report.Failures[0].Reason.Should().Contain("not found in validation schema");
    }

    [Fact]
    public void GivenNullMeasurements_WhenValidatingCollection_ThenThrowsArgumentNullException()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "distance", Dimension.Length }
        };
        var validator = new MeasurementValidator(schema);
        System.Collections.Generic.Dictionary<string, Measurement<double>> measurements = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => validator.ValidateCollection(measurements));
    }

    [Fact]
    public void GivenValidUnits_WhenValidatingUnits_ThenReturnsAllValid()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "length_unit", Dimension.Length },
            { "mass_unit", Dimension.Mass }
        };
        var validator = new MeasurementValidator(schema);

        var units = new System.Collections.Generic.Dictionary<string, Unit>
        {
            { "length_unit", Unit.SI.m },
            { "mass_unit", Unit.SI.kg }
        };

        // Act
        var report = validator.ValidateUnits(units);

        // Assert
        report.TotalItems.Should().Be(2);
        report.ValidItems.Should().Be(2);
        report.InvalidItems.Should().Be(0);
    }

    [Fact]
    public void GivenNullUnits_WhenValidatingUnits_ThenThrowsArgumentNullException()
    {
        // Arrange
        var schema = new System.Collections.Generic.Dictionary<string, Dimension>
        {
            { "distance", Dimension.Length }
        };
        var validator = new MeasurementValidator(schema);
        System.Collections.Generic.Dictionary<string, Unit> units = null;

        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => validator.ValidateUnits(units));
    }
}
