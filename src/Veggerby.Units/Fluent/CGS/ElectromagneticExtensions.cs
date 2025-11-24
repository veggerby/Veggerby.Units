namespace Veggerby.Units.Fluent.CGS;

/// <summary>CGS electromagnetic unit numeric extensions (Gaussian/EMU system).</summary>
public static class ElectromagneticExtensions
{
    /// <summary>Creates a magnetic flux density measurement in gauss (G).</summary>
    public static DoubleMeasurement Gauss(this double value) => new(value, Unit.CGS.G);
    /// <summary>Symbol alias for <see cref="Gauss(double)"/>.</summary>
    public static DoubleMeasurement G(this double value) => value.Gauss();
    /// <summary>Creates a decimal magnetic flux density measurement in gauss (G).</summary>
    public static DecimalMeasurement Gauss(this decimal value) => new(value, Unit.CGS.G);

    /// <summary>Creates a magnetic flux measurement in maxwells (Mx).</summary>
    public static DoubleMeasurement Maxwells(this double value) => new(value, Unit.CGS.Mx);
    /// <summary>Alias for <see cref="Maxwells(double)"/>.</summary>
    public static DoubleMeasurement Maxwell(this double value) => value.Maxwells();
    /// <summary>Symbol alias for <see cref="Maxwells(double)"/>.</summary>
    public static DoubleMeasurement Mx(this double value) => value.Maxwells();
    /// <summary>Creates a decimal magnetic flux measurement in maxwells (Mx).</summary>
    public static DecimalMeasurement Maxwells(this decimal value) => new(value, Unit.CGS.Mx);
    /// <summary>Alias for <see cref="Maxwells(decimal)"/>.</summary>
    public static DecimalMeasurement Maxwell(this decimal value) => value.Maxwells();

    /// <summary>Creates a magnetic field strength measurement in oersteds (Oe).</summary>
    public static DoubleMeasurement Oersteds(this double value) => new(value, Unit.CGS.Oe);
    /// <summary>Alias for <see cref="Oersteds(double)"/>.</summary>
    public static DoubleMeasurement Oersted(this double value) => value.Oersteds();
    /// <summary>Symbol alias for <see cref="Oersteds(double)"/>.</summary>
    public static DoubleMeasurement Oe(this double value) => value.Oersteds();
    /// <summary>Creates a decimal magnetic field strength measurement in oersteds (Oe).</summary>
    public static DecimalMeasurement Oersteds(this decimal value) => new(value, Unit.CGS.Oe);
    /// <summary>Alias for <see cref="Oersteds(decimal)"/>.</summary>
    public static DecimalMeasurement Oersted(this decimal value) => value.Oersteds();

    /// <summary>Creates an electric current measurement in abamperes (abA).</summary>
    public static DoubleMeasurement Abamperes(this double value) => new(value, Unit.CGS.abA);
    /// <summary>Alias for <see cref="Abamperes(double)"/>.</summary>
    public static DoubleMeasurement Abampere(this double value) => value.Abamperes();
    /// <summary>Symbol alias for <see cref="Abamperes(double)"/>.</summary>
    public static DoubleMeasurement abA(this double value) => value.Abamperes();
    /// <summary>Creates a decimal electric current measurement in abamperes (abA).</summary>
    public static DecimalMeasurement Abamperes(this decimal value) => new(value, Unit.CGS.abA);
    /// <summary>Alias for <see cref="Abamperes(decimal)"/>.</summary>
    public static DecimalMeasurement Abampere(this decimal value) => value.Abamperes();

    /// <summary>Creates an electric charge measurement in abcoulombs (abC).</summary>
    public static DoubleMeasurement Abcoulombs(this double value) => new(value, Unit.CGS.abC);
    /// <summary>Alias for <see cref="Abcoulombs(double)"/>.</summary>
    public static DoubleMeasurement Abcoulomb(this double value) => value.Abcoulombs();
    /// <summary>Symbol alias for <see cref="Abcoulombs(double)"/>.</summary>
    public static DoubleMeasurement abC(this double value) => value.Abcoulombs();
    /// <summary>Creates a decimal electric charge measurement in abcoulombs (abC).</summary>
    public static DecimalMeasurement Abcoulombs(this decimal value) => new(value, Unit.CGS.abC);
    /// <summary>Alias for <see cref="Abcoulombs(decimal)"/>.</summary>
    public static DecimalMeasurement Abcoulomb(this decimal value) => value.Abcoulombs();

    /// <summary>Creates an electric potential measurement in abvolts (abV).</summary>
    public static DoubleMeasurement Abvolts(this double value) => new(value, Unit.CGS.abV);
    /// <summary>Alias for <see cref="Abvolts(double)"/>.</summary>
    public static DoubleMeasurement Abvolt(this double value) => value.Abvolts();
    /// <summary>Symbol alias for <see cref="Abvolts(double)"/>.</summary>
    public static DoubleMeasurement abV(this double value) => value.Abvolts();
    /// <summary>Creates a decimal electric potential measurement in abvolts (abV).</summary>
    public static DecimalMeasurement Abvolts(this decimal value) => new(value, Unit.CGS.abV);
    /// <summary>Alias for <see cref="Abvolts(decimal)"/>.</summary>
    public static DecimalMeasurement Abvolt(this decimal value) => value.Abvolts();

    /// <summary>Creates an electrical resistance measurement in abohms.</summary>
    public static DoubleMeasurement Abohms(this double value) => new(value, Unit.CGS.abohm);
    /// <summary>Alias for <see cref="Abohms(double)"/>.</summary>
    public static DoubleMeasurement Abohm(this double value) => value.Abohms();
    /// <summary>Creates a decimal electrical resistance measurement in abohms.</summary>
    public static DecimalMeasurement Abohms(this decimal value) => new(value, Unit.CGS.abohm);
}
