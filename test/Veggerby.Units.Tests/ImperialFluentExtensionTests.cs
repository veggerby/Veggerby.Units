using AwesomeAssertions;

using Veggerby.Units.Fluent.Imperial;

using Xunit;

namespace Veggerby.Units.Tests;

public class ImperialFluentExtensionTests
{
    [Fact]
    public void PoundsAndAliases_ReturnExpectedUnits()
    {
        // Arrange
        double v = 5;

        // Act
        var a = v.Pounds();
        var b = v.Pound();
        var c = v.lb();

        // Assert
        a.Unit.Should().Be(Unit.Imperial.lb);
        b.Unit.Should().Be(Unit.Imperial.lb);
        c.Unit.Should().Be(Unit.Imperial.lb);
    }

    [Fact]
    public void PoundsDecimalAliases_ReturnExpectedUnits()
    {
        // Arrange
        decimal v = 8m;

        // Act
        var a = v.Pounds();
        var b = v.Pound();

        // Assert
        a.Unit.Should().Be(Unit.Imperial.lb);
        b.Unit.Should().Be(Unit.Imperial.lb);
        a.Value.Should().Be(v);
    }

    [Fact]
    public void InchesAndAliases_ReturnExpectedUnits()
    {
        double v = 2;
        var a = v.Inches();
        var b = v.Inch();
        var c = v.@in();
        a.Unit.Should().Be(Unit.Imperial.@in);
        b.Unit.Should().Be(Unit.Imperial.@in);
        c.Unit.Should().Be(Unit.Imperial.@in);
    }

    [Fact]
    public void InchesDecimalAliases_ReturnExpectedUnits()
    {
        decimal v = 4m;
        var a = v.Inches();
        var b = v.Inch();
        a.Unit.Should().Be(Unit.Imperial.@in);
        b.Unit.Should().Be(Unit.Imperial.@in);
        a.Value.Should().Be(v);
    }

    [Fact]
    public void FootPoundsAliases_ReturnExpectedUnits()
    {
        double v = 3;
        var a = v.FootPounds();
        var b = v.FootPound();
        a.Unit.Should().Be(Unit.Imperial.ft_lb);
        b.Unit.Should().Be(Unit.Imperial.ft_lb);
    }

    [Fact]
    public void PsiAliases_ReturnExpectedUnits()
    {
        double v = 7;
        var a = v.PoundsPerSquareInch();
        var b = v.psi();
        a.Unit.Should().Be(Unit.Imperial.psi);
        b.Unit.Should().Be(Unit.Imperial.psi);
    }

    [Fact]
    public void HorsepowerAliases_ReturnExpectedUnits()
    {
        double v = 10;
        var a = v.Horsepower();
        var b = v.hp();
        a.Unit.Should().Be(Unit.Imperial.hp);
        b.Unit.Should().Be(Unit.Imperial.hp);
    }

    [Fact]
    public void FeetPerSecondAliases_ReturnExpectedUnits()
    {
        double v = 12;
        var a = v.FeetPerSecond();
        var b = v.ftps();
        a.Unit.Should().Be(Unit.Imperial.ft / Unit.Imperial.s);
        b.Unit.Should().Be(Unit.Imperial.ft / Unit.Imperial.s);
    }

    [Fact]
    public void StonesAndAliases_ReturnExpectedUnits()
    {
        double v = 5;
        var a = v.Stones();
        var b = v.Stone();
        var c = v.st();
        a.Unit.Should().Be(Unit.Imperial.st);
        b.Unit.Should().Be(Unit.Imperial.st);
        c.Unit.Should().Be(Unit.Imperial.st);
    }

    [Fact]
    public void StonesDecimalAliases_ReturnExpectedUnits()
    {
        decimal v = 2m;
        var a = v.Stones();
        var b = v.Stone();
        a.Unit.Should().Be(Unit.Imperial.st);
        b.Unit.Should().Be(Unit.Imperial.st);
        a.Value.Should().Be(v);
    }

    [Fact]
    public void OuncesAndAliases_ReturnExpectedUnits()
    {
        double v = 11;
        var a = v.Ounces();
        var b = v.Ounce();
        var c = v.oz();
        a.Unit.Should().Be(Unit.Imperial.oz);
        b.Unit.Should().Be(Unit.Imperial.oz);
        c.Unit.Should().Be(Unit.Imperial.oz);
    }

    [Fact]
    public void OuncesDecimalAliases_ReturnExpectedUnits()
    {
        decimal v = 6m;
        var a = v.Ounces();
        var b = v.Ounce();
        a.Unit.Should().Be(Unit.Imperial.oz);
        b.Unit.Should().Be(Unit.Imperial.oz);
        a.Value.Should().Be(v);
    }

    [Fact]
    public void MilesAndAliases_ReturnExpectedUnits()
    {
        double v = 1.5;
        var a = v.Miles();
        var b = v.Mile();
        var c = v.mi();
        a.Unit.Should().Be(Unit.Imperial.mi);
        b.Unit.Should().Be(Unit.Imperial.mi);
        c.Unit.Should().Be(Unit.Imperial.mi);
    }

    [Fact]
    public void MilesDecimalAliases_ReturnExpectedUnits()
    {
        decimal v = 3m;
        var a = v.Miles();
        var b = v.Mile();
        a.Unit.Should().Be(Unit.Imperial.mi);
        b.Unit.Should().Be(Unit.Imperial.mi);
        a.Value.Should().Be(v);
    }

    [Fact]
    public void FeetPerSecondDecimalAliases_ReturnExpectedUnits()
    {
        decimal v = 9m;
        var a = v.FeetPerSecond();
        var b = v.ftps();
        var expected = Unit.Imperial.ft / Unit.Imperial.s;
        a.Unit.Should().Be(expected);
        b.Unit.Should().Be(expected);
        a.Value.Should().Be(v);
    }
}