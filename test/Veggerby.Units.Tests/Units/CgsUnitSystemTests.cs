using AwesomeAssertions;

using Veggerby.Units;

using Xunit;

namespace Veggerby.Units.Tests.Units;

public class CgsUnitSystemTests
{
    private readonly CgsUnitSystem _cgs = Unit.CGS;
    private readonly InternationalUnitSystem _si = Unit.SI;

    // Base units tests
    [Fact]
    public void GivenCentimeter_WhenComparedToMeter_ThenScalesBy0Point01()
    {
        // Arrange
        var cm = _cgs.cm;
        var m = _si.m;

        // Act
        var oneCmInMeters = new DoubleMeasurement(1d, cm).ConvertTo(m);

        // Assert
        ((double)oneCmInMeters).Should().Be(CgsUnitSystem.CentimeterToMeter);
    }

    [Fact]
    public void GivenGram_WhenComparedToKilogram_ThenScalesBy0Point001()
    {
        // Arrange
        var g = _cgs.g;
        var kg = _si.kg;

        // Act
        var oneGramInKg = new DoubleMeasurement(1d, g).ConvertTo(kg);

        // Assert
        ((double)oneGramInKg).Should().Be(CgsUnitSystem.GramToKilogram);
    }

    [Fact]
    public void GivenCgsSecond_WhenComparedToSiSecond_ThenIdentical()
    {
        // Arrange
        var cgsSecond = _cgs.s;
        var siSecond = _si.s;

        // Act & Assert
        (cgsSecond == siSecond).Should().BeTrue();
    }

    [Fact]
    public void GivenHundredCentimeters_WhenConvertedToMeter_ThenEqualsOne()
    {
        // Arrange
        var cm100 = new DoubleMeasurement(100d, _cgs.cm);

        // Act
        var inMeters = cm100.ConvertTo(_si.m);

        // Assert
        ((double)inMeters).Should().Be(1.0);
    }

    // Mechanical derived units tests
    [Fact]
    public void GivenDyne_WhenConvertedToNewton_ThenEquals1e5()
    {
        // Arrange
        var dyn = _cgs.dyn;
        var newton = _si.kg * _si.m / (_si.s ^ 2);

        // Act
        var oneDynInNewtons = new DoubleMeasurement(1d, dyn).ConvertTo(newton);

        // Assert
        ((double)oneDynInNewtons).Should().Be(CgsUnitSystem.DyneToNewton);
    }

    [Fact]
    public void GivenEachDyne_WhenConvertedToGramCentimeterPerSquareSecond_ThenEqualsOne()
    {
        // Arrange
        var dyn = _cgs.dyn;
        var gCmPerS2 = _cgs.g * _cgs.cm / (_cgs.s ^ 2);

        // Act
        var oneDynInGCmPerS2 = new DoubleMeasurement(1d, dyn).ConvertTo(gCmPerS2);

        // Assert
        ((double)oneDynInGCmPerS2).Should().BeApproximately(1d, 1e-9);
    }

    [Fact]
    public void GivenErg_WhenConvertedToJoule_ThenEquals1e7()
    {
        // Arrange
        var erg = _cgs.erg;
        var joule = _si.kg * (_si.m ^ 2) / (_si.s ^ 2);

        // Act
        var oneErgInJoules = new DoubleMeasurement(1d, erg).ConvertTo(joule);

        // Assert
        ((double)oneErgInJoules).Should().Be(CgsUnitSystem.ErgToJoule);
    }

    [Fact]
    public void GivenErg_WhenConvertedToDyneCentimeter_ThenEqualsOne()
    {
        // Arrange
        var erg = _cgs.erg;
        var dynCm = _cgs.dyn * _cgs.cm;

        // Act
        var oneErgInDynCm = new DoubleMeasurement(1d, erg).ConvertTo(dynCm);

        // Assert
        ((double)oneErgInDynCm).Should().BeApproximately(1d, 1e-9);
    }

    [Fact]
    public void GivenBarye_WhenConvertedToPascal_ThenEquals0Point1()
    {
        // Arrange
        var barye = _cgs.Ba;
        var pascal = _si.kg / (_si.m * (_si.s ^ 2));

        // Act
        var oneBaryeInPascals = new DoubleMeasurement(1d, barye).ConvertTo(pascal);

        // Assert
        ((double)oneBaryeInPascals).Should().Be(CgsUnitSystem.BaryeToPascal);
    }

    [Fact]
    public void GivenBarye_WhenConvertedToDynePerSquareCentimeter_ThenEqualsOne()
    {
        // Arrange
        var barye = _cgs.Ba;
        var dynPerCm2 = _cgs.dyn / (_cgs.cm ^ 2);

        // Act
        var oneBaryeInDynPerCm2 = new DoubleMeasurement(1d, barye).ConvertTo(dynPerCm2);

        // Assert
        ((double)oneBaryeInDynPerCm2).Should().BeApproximately(1d, 1e-9);
    }

    [Fact]
    public void GivenPoise_WhenConvertedToPascalSecond_ThenEquals0Point1()
    {
        // Arrange
        var poise = _cgs.P;
        var pascalSecond = _si.kg / (_si.m * _si.s);

        // Act
        var onePoiseInPascalSeconds = new DoubleMeasurement(1d, poise).ConvertTo(pascalSecond);

        // Assert
        ((double)onePoiseInPascalSeconds).Should().Be(CgsUnitSystem.PoiseToPascalSecond);
    }

    [Fact]
    public void GivenPoise_WhenConvertedToGramPerCentimeterSecond_ThenEqualsOne()
    {
        // Arrange
        var poise = _cgs.P;
        var gPerCmS = _cgs.g / (_cgs.cm * _cgs.s);

        // Act
        var onePoiseInGPerCmS = new DoubleMeasurement(1d, poise).ConvertTo(gPerCmS);

        // Assert
        ((double)onePoiseInGPerCmS).Should().BeApproximately(1d, 1e-9);
    }

    [Fact]
    public void GivenStokes_WhenConvertedToSquareMeterPerSecond_ThenEquals1e4()
    {
        // Arrange
        var stokes = _cgs.St;
        var m2PerS = (_si.m ^ 2) / _si.s;

        // Act
        var oneStokesInM2PerS = new DoubleMeasurement(1d, stokes).ConvertTo(m2PerS);

        // Assert
        ((double)oneStokesInM2PerS).Should().Be(CgsUnitSystem.StokesToSquareMeterPerSecond);
    }

    [Fact]
    public void GivenStokes_WhenConvertedToSquareCentimeterPerSecond_ThenEqualsOne()
    {
        // Arrange
        var stokes = _cgs.St;
        var cm2PerS = (_cgs.cm ^ 2) / _cgs.s;

        // Act
        var oneStokesInCm2PerS = new DoubleMeasurement(1d, stokes).ConvertTo(cm2PerS);

        // Assert
        ((double)oneStokesInCm2PerS).Should().BeApproximately(1d, 1e-9);
    }

    // Electromagnetic units tests
    [Fact]
    public void GivenGauss_WhenConvertedToTesla_ThenEquals1e4()
    {
        // Arrange
        var gauss = _cgs.G;
        var tesla = _si.kg / (_si.A * (_si.s ^ 2));

        // Act
        var oneGaussInTesla = new DoubleMeasurement(1d, gauss).ConvertTo(tesla);

        // Assert
        ((double)oneGaussInTesla).Should().Be(CgsUnitSystem.GaussToTesla);
    }

    [Fact]
    public void GivenMaxwell_WhenConvertedToWeber_ThenEquals1e8()
    {
        // Arrange
        var maxwell = _cgs.Mx;
        var weber = _si.kg * (_si.m ^ 2) / (_si.A * (_si.s ^ 2));

        // Act
        var oneMaxwellInWeber = new DoubleMeasurement(1d, maxwell).ConvertTo(weber);

        // Assert
        ((double)oneMaxwellInWeber).Should().Be(CgsUnitSystem.MaxwellToWeber);
    }

    [Fact]
    public void GivenOersted_WhenConvertedToAmperePerMeter_ThenCorrect()
    {
        // Arrange
        var oersted = _cgs.Oe;
        var amperePerMeter = _si.A / _si.m;
        var expectedFactor = 1000.0 / (4.0 * Math.PI);

        // Act
        var oneOerstedInAPerM = new DoubleMeasurement(1d, oersted).ConvertTo(amperePerMeter);

        // Assert
        ((double)oneOerstedInAPerM).Should().BeApproximately(expectedFactor, 1e-9);
    }

    [Fact]
    public void GivenAbampere_WhenConvertedToAmpere_ThenEquals10()
    {
        // Arrange
        var abampere = _cgs.abA;
        var ampere = _si.A;

        // Act
        var oneAbampereInAmperes = new DoubleMeasurement(1d, abampere).ConvertTo(ampere);

        // Assert
        ((double)oneAbampereInAmperes).Should().Be(CgsUnitSystem.AbampereToAmpere);
    }

    [Fact]
    public void GivenAbcoulomb_WhenConvertedToCoulomb_ThenEquals10()
    {
        // Arrange
        var abcoulomb = _cgs.abC;
        var coulomb = _si.A * _si.s;

        // Act
        var oneAbcoulombInCoulombs = new DoubleMeasurement(1d, abcoulomb).ConvertTo(coulomb);

        // Assert
        ((double)oneAbcoulombInCoulombs).Should().BeApproximately(10d, 1e-9);
    }

    [Fact]
    public void GivenAbvolt_WhenConvertedToVolt_ThenEquals1e8()
    {
        // Arrange
        var abvolt = _cgs.abV;
        var volt = _si.kg * (_si.m ^ 2) / (_si.A * (_si.s ^ 3));

        // Act
        var oneAbvoltInVolts = new DoubleMeasurement(1d, abvolt).ConvertTo(volt);

        // Assert
        ((double)oneAbvoltInVolts).Should().BeApproximately(1e-8, 1e-16);
    }

    [Fact]
    public void GivenAbohm_WhenConvertedToOhm_ThenEquals1e9()
    {
        // Arrange
        var abohm = _cgs.abohm;
        var ohm = _si.kg * (_si.m ^ 2) / ((_si.A ^ 2) * (_si.s ^ 3));

        // Act
        var oneAbohmInOhms = new DoubleMeasurement(1d, abohm).ConvertTo(ohm);

        // Assert
        ((double)oneAbohmInOhms).Should().BeApproximately(1e-9, 1e-17);
    }

    // Round-trip conversion tests
    [Fact]
    public void GivenSiToChsAndBack_WhenRoundTrip_ThenPreservesValue()
    {
        // Arrange
        var originalValue = 42.5;
        var siMeasurement = new DoubleMeasurement(originalValue, _si.m);

        // Act
        var cgsMeasurement = siMeasurement.ConvertTo(_cgs.cm);
        var backToSi = cgsMeasurement.ConvertTo(_si.m);

        // Assert
        ((double)backToSi).Should().BeApproximately(originalValue, 1e-12);
    }

    [Fact]
    public void GivenErgToJouleAndBack_WhenRoundTrip_ThenPreservesValue()
    {
        // Arrange
        var originalValue = 1000.0;
        var ergMeasurement = new DoubleMeasurement(originalValue, _cgs.erg);

        // Act
        var joule = _si.kg * (_si.m ^ 2) / (_si.s ^ 2);
        var jouleMeasurement = ergMeasurement.ConvertTo(joule);
        var backToErg = jouleMeasurement.ConvertTo(_cgs.erg);

        // Assert
        ((double)backToErg).Should().BeApproximately(originalValue, 1e-9);
    }

    // Dimensional consistency tests
    [Fact]
    public void GivenDyne_WhenCheckingDimension_ThenMatchesForce()
    {
        // Arrange
        var dyn = _cgs.dyn;
        var newton = _si.kg * _si.m / (_si.s ^ 2);

        // Act & Assert
        dyn.Dimension.Should().Be(newton.Dimension);
    }

    [Fact]
    public void GivenErg_WhenCheckingDimension_ThenMatchesEnergy()
    {
        // Arrange
        var erg = _cgs.erg;
        var joule = _si.kg * (_si.m ^ 2) / (_si.s ^ 2);

        // Act & Assert
        erg.Dimension.Should().Be(joule.Dimension);
    }

    [Fact]
    public void GivenBarye_WhenCheckingDimension_ThenMatchesPressure()
    {
        // Arrange
        var barye = _cgs.Ba;
        var pascal = _si.kg / (_si.m * (_si.s ^ 2));

        // Act & Assert
        barye.Dimension.Should().Be(pascal.Dimension);
    }

    [Fact]
    public void GivenGauss_WhenCheckingDimension_ThenMatchesMagneticFluxDensity()
    {
        // Arrange
        var gauss = _cgs.G;
        var tesla = _si.kg / (_si.A * (_si.s ^ 2));

        // Act & Assert
        gauss.Dimension.Should().Be(tesla.Dimension);
    }
}
