using AwesomeAssertions;

using Veggerby.Units;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class ImperialExtendedUnitsTests
{
    private readonly ImperialUnitSystem _imp = Unit.Imperial;
    private readonly InternationalUnitSystem _si = Unit.SI;

    [Fact]
    public void GivenFathom_WhenComparedToSixFeet_ThenEqual()
    {
        // Arrange
        var fathom = _imp.fathom;
        var foot = _imp.ft;
        // Act
        var oneFathomInFeet = new DoubleMeasurement(1d, fathom).ConvertTo(foot);
        // Assert
        ((double)oneFathomInFeet).Should().BeApproximately(6d, 1e-12);
    }

    [Fact]
    public void GivenCable_WhenComparedToHundredFathoms_ThenEqual()
    {
        // Arrange
        var cable = _imp.cable;
        var fathom = _imp.fathom;
        // Act
        var oneCableInFathoms = new DoubleMeasurement(1d, cable).ConvertTo(fathom);
        // Assert
        ((double)oneCableInFathoms).Should().BeApproximately(100d, 1e-9);
    }

    [Fact]
    public void GivenNauticalMile_WhenComparedTo1852Metres_ThenEqual()
    {
        // Arrange
        var nmi = _imp.nmi;
        var metre = _si.m;
        // Act
        var oneNmiInMetres = new DoubleMeasurement(1d, nmi).ConvertTo(metre);
        // Assert
        ((double)oneNmiInMetres).Should().BeApproximately(1852d, 1e-9);
    }

    [Fact]
    public void GivenPoundForce_WhenComparedToDefinition_ThenEqual()
    {
        // Arrange
        var lbf = _imp.lbf;
        var newton = _si.kg * _si.m / (_si.s ^ 2);
        // Act
        var oneLbfInNewtons = new DoubleMeasurement(1d, lbf).ConvertTo(newton);
        // Assert (tolerance due to double chain)
        ((double)oneLbfInNewtons).Should().BeApproximately(ImperialUnitSystem.PoundForceToNewton, 1e-9);
    }

    [Fact]
    public void GivenPsi_WhenExpanded_ThenIsPoundForcePerSquareInch()
    {
        // Arrange
        var psi = _imp.psi;
        var lbf = _imp.lbf;
        var sqInch = _imp.@in ^ 2;
        var onePsiInLbfPerSqIn = new DoubleMeasurement(1d, psi).ConvertTo(lbf / sqInch);
        // Assert
        ((double)onePsiInLbfPerSqIn).Should().BeApproximately(1d, 1e-12);
    }

    [Fact]
    public void GivenHorsepower_WhenExpanded_ThenIs550FootPoundsPerSecond()
    {
        // Arrange
        var hp = _imp.hp;
        var footPoundPerSecond = _imp.ft_lb / _si.s;
        // Act
        var oneHpInFootPoundPerSecond = new DoubleMeasurement(1d, hp).ConvertTo(footPoundPerSecond);
        // Assert
        ((double)oneHpInFootPoundPerSecond).Should().BeApproximately(550d, 1e-9);
    }
}