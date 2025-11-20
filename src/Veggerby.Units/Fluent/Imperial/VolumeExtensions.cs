using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Imperial;

/// <summary>Imperial volume numeric extensions (gallons, quarts, pints, fluid ounces, etc.).</summary>
public static class VolumeExtensions
{
    /// <summary>Creates a volume measurement in gallons (gal).</summary>
    public static DoubleMeasurement Gallons(this double value) => new(value, Unit.Imperial.gal);
    /// <summary>Alias for <see cref="Gallons(double)"/>.</summary>
    public static DoubleMeasurement Gallon(this double value) => value.Gallons();
    /// <summary>Symbol alias for <see cref="Gallons(double)"/>.</summary>
    public static DoubleMeasurement gal(this double value) => value.Gallons();
    /// <summary>Creates a decimal volume measurement in gallons (gal).</summary>
    public static DecimalMeasurement Gallons(this decimal value) => new(value, Unit.Imperial.gal);
    /// <summary>Alias for <see cref="Gallons(decimal)"/>.</summary>
    public static DecimalMeasurement Gallon(this decimal value) => value.Gallons();

    /// <summary>Creates a volume measurement in quarts (qt).</summary>
    public static DoubleMeasurement Quarts(this double value) => new(value, Unit.Imperial.qt);
    /// <summary>Alias for <see cref="Quarts(double)"/>.</summary>
    public static DoubleMeasurement Quart(this double value) => value.Quarts();
    /// <summary>Symbol alias for <see cref="Quarts(double)"/>.</summary>
    public static DoubleMeasurement qt(this double value) => value.Quarts();
    /// <summary>Creates a decimal volume measurement in quarts (qt).</summary>
    public static DecimalMeasurement Quarts(this decimal value) => new(value, Unit.Imperial.qt);
    /// <summary>Alias for <see cref="Quarts(decimal)"/>.</summary>
    public static DecimalMeasurement Quart(this decimal value) => value.Quarts();

    /// <summary>Creates a volume measurement in pints (pt).</summary>
    public static DoubleMeasurement Pints(this double value) => new(value, Unit.Imperial.pt);
    /// <summary>Alias for <see cref="Pints(double)"/>.</summary>
    public static DoubleMeasurement Pint(this double value) => value.Pints();
    /// <summary>Symbol alias for <see cref="Pints(double)"/>.</summary>
    public static DoubleMeasurement pt(this double value) => value.Pints();
    /// <summary>Creates a decimal volume measurement in pints (pt).</summary>
    public static DecimalMeasurement Pints(this decimal value) => new(value, Unit.Imperial.pt);
    /// <summary>Alias for <see cref="Pints(decimal)"/>.</summary>
    public static DecimalMeasurement Pint(this decimal value) => value.Pints();

    /// <summary>Creates a volume measurement in fluid ounces (fl oz).</summary>
    public static DoubleMeasurement FluidOunces(this double value) => new(value, Unit.Imperial.fl_oz);
    /// <summary>Alias for <see cref="FluidOunces(double)"/>.</summary>
    public static DoubleMeasurement FluidOunce(this double value) => value.FluidOunces();
    /// <summary>Creates a decimal volume measurement in fluid ounces (fl oz).</summary>
    public static DecimalMeasurement FluidOunces(this decimal value) => new(value, Unit.Imperial.fl_oz);
    /// <summary>Alias for <see cref="FluidOunces(decimal)"/>.</summary>
    public static DecimalMeasurement FluidOunce(this decimal value) => value.FluidOunces();

    /// <summary>Creates a volume measurement in gills (gi).</summary>
    public static DoubleMeasurement Gills(this double value) => new(value, Unit.Imperial.gi);
    /// <summary>Alias for <see cref="Gills(double)"/>.</summary>
    public static DoubleMeasurement Gill(this double value) => value.Gills();
    /// <summary>Symbol alias for <see cref="Gills(double)"/>.</summary>
    public static DoubleMeasurement gi(this double value) => value.Gills();
    /// <summary>Creates a decimal volume measurement in gills (gi).</summary>
    public static DecimalMeasurement Gills(this decimal value) => new(value, Unit.Imperial.gi);
    /// <summary>Alias for <see cref="Gills(decimal)"/>.</summary>
    public static DecimalMeasurement Gill(this decimal value) => value.Gills();

    /// <summary>Creates a volume measurement in pecks (pk).</summary>
    public static DoubleMeasurement Pecks(this double value) => new(value, Unit.Imperial.peck);
    /// <summary>Alias for <see cref="Pecks(double)"/>.</summary>
    public static DoubleMeasurement Peck(this double value) => value.Pecks();
    /// <summary>Creates a decimal volume measurement in pecks (pk).</summary>
    public static DecimalMeasurement Pecks(this decimal value) => new(value, Unit.Imperial.peck);
    /// <summary>Alias for <see cref="Pecks(decimal)"/>.</summary>
    public static DecimalMeasurement Peck(this decimal value) => value.Pecks();

    /// <summary>Creates a volume measurement in bushels (bu).</summary>
    public static DoubleMeasurement Bushels(this double value) => new(value, Unit.Imperial.bushel);
    /// <summary>Alias for <see cref="Bushels(double)"/>.</summary>
    public static DoubleMeasurement Bushel(this double value) => value.Bushels();
    /// <summary>Creates a decimal volume measurement in bushels (bu).</summary>
    public static DecimalMeasurement Bushels(this decimal value) => new(value, Unit.Imperial.bushel);
    /// <summary>Alias for <see cref="Bushels(decimal)"/>.</summary>
    public static DecimalMeasurement Bushel(this decimal value) => value.Bushels();

    /// <summary>Creates a volume measurement in barrels (bbl).</summary>
    public static DoubleMeasurement Barrels(this double value) => new(value, Unit.Imperial.barrel);
    /// <summary>Alias for <see cref="Barrels(double)"/>.</summary>
    public static DoubleMeasurement Barrel(this double value) => value.Barrels();
    /// <summary>Creates a decimal volume measurement in barrels (bbl).</summary>
    public static DecimalMeasurement Barrels(this decimal value) => new(value, Unit.Imperial.barrel);
    /// <summary>Alias for <see cref="Barrels(decimal)"/>.</summary>
    public static DecimalMeasurement Barrel(this decimal value) => value.Barrels();
}
