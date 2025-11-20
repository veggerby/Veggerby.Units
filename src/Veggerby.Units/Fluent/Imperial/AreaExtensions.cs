using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>Imperial area numeric extensions (square inches, acres, etc.).</summary>
public static class AreaExtensions
{
    /// <summary>Creates an area measurement in square inches (sq in).</summary>
    public static DoubleMeasurement SquareInches(this double value) => new(value, Unit.Imperial.sq_in);
    /// <summary>Alias for <see cref="SquareInches(double)"/>.</summary>
    public static DoubleMeasurement SquareInch(this double value) => value.SquareInches();
    /// <summary>Creates a decimal area measurement in square inches (sq in).</summary>
    public static DecimalMeasurement SquareInches(this decimal value) => new(value, Unit.Imperial.sq_in);
    /// <summary>Alias for <see cref="SquareInches(decimal)"/>.</summary>
    public static DecimalMeasurement SquareInch(this decimal value) => value.SquareInches();

    /// <summary>Creates an area measurement in square feet (sq ft).</summary>
    public static DoubleMeasurement SquareFeet(this double value) => new(value, Unit.Imperial.sq_ft);
    /// <summary>Alias for <see cref="SquareFeet(double)"/>.</summary>
    public static DoubleMeasurement SquareFoot(this double value) => value.SquareFeet();
    /// <summary>Creates a decimal area measurement in square feet (sq ft).</summary>
    public static DecimalMeasurement SquareFeet(this decimal value) => new(value, Unit.Imperial.sq_ft);
    /// <summary>Alias for <see cref="SquareFeet(decimal)"/>.</summary>
    public static DecimalMeasurement SquareFoot(this decimal value) => value.SquareFeet();

    /// <summary>Creates an area measurement in square yards (sq yd).</summary>
    public static DoubleMeasurement SquareYards(this double value) => new(value, Unit.Imperial.sq_yd);
    /// <summary>Alias for <see cref="SquareYards(double)"/>.</summary>
    public static DoubleMeasurement SquareYard(this double value) => value.SquareYards();
    /// <summary>Creates a decimal area measurement in square yards (sq yd).</summary>
    public static DecimalMeasurement SquareYards(this decimal value) => new(value, Unit.Imperial.sq_yd);
    /// <summary>Alias for <see cref="SquareYards(decimal)"/>.</summary>
    public static DecimalMeasurement SquareYard(this decimal value) => value.SquareYards();

    /// <summary>Creates an area measurement in acres.</summary>
    public static DoubleMeasurement Acres(this double value) => new(value, Unit.Imperial.acre);
    /// <summary>Alias for <see cref="Acres(double)"/>.</summary>
    public static DoubleMeasurement Acre(this double value) => value.Acres();
    /// <summary>Creates a decimal area measurement in acres.</summary>
    public static DecimalMeasurement Acres(this decimal value) => new(value, Unit.Imperial.acre);
    /// <summary>Alias for <see cref="Acres(decimal)"/>.</summary>
    public static DecimalMeasurement Acre(this decimal value) => value.Acres();

    /// <summary>Creates an area measurement in square miles (sq mi).</summary>
    public static DoubleMeasurement SquareMiles(this double value) => new(value, Unit.Imperial.sq_mi);
    /// <summary>Alias for <see cref="SquareMiles(double)"/>.</summary>
    public static DoubleMeasurement SquareMile(this double value) => value.SquareMiles();
    /// <summary>Creates a decimal area measurement in square miles (sq mi).</summary>
    public static DecimalMeasurement SquareMiles(this decimal value) => new(value, Unit.Imperial.sq_mi);
    /// <summary>Alias for <see cref="SquareMiles(decimal)"/>.</summary>
    public static DecimalMeasurement SquareMile(this decimal value) => value.SquareMiles();

    /// <summary>Creates an area measurement in roods.</summary>
    public static DoubleMeasurement Roods(this double value) => new(value, Unit.Imperial.rood);
    /// <summary>Alias for <see cref="Roods(double)"/>.</summary>
    public static DoubleMeasurement Rood(this double value) => value.Roods();
    /// <summary>Creates a decimal area measurement in roods.</summary>
    public static DecimalMeasurement Roods(this decimal value) => new(value, Unit.Imperial.rood);
    /// <summary>Alias for <see cref="Roods(decimal)"/>.</summary>
    public static DecimalMeasurement Rood(this decimal value) => value.Roods();

    /// <summary>Creates an area measurement in perches.</summary>
    public static DoubleMeasurement Perches(this double value) => new(value, Unit.Imperial.perch);
    /// <summary>Alias for <see cref="Perches(double)"/>.</summary>
    public static DoubleMeasurement Perch(this double value) => value.Perches();
    /// <summary>Creates a decimal area measurement in perches.</summary>
    public static DecimalMeasurement Perches(this decimal value) => new(value, Unit.Imperial.perch);
    /// <summary>Alias for <see cref="Perches(decimal)"/>.</summary>
    public static DecimalMeasurement Perch(this decimal value) => value.Perches();
}
