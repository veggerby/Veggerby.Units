namespace Veggerby.Units.Fluent.CGS;

/// <summary>CGS mechanical unit numeric extensions (force, energy, pressure, viscosity).</summary>
public static class MechanicalExtensions
{
    /// <summary>Creates a force measurement in dynes (dyn).</summary>
    public static DoubleMeasurement Dynes(this double value) => new(value, Unit.CGS.dyn);
    /// <summary>Alias for <see cref="Dynes(double)"/>.</summary>
    public static DoubleMeasurement Dyne(this double value) => value.Dynes();
    /// <summary>Symbol alias for <see cref="Dynes(double)"/>.</summary>
    public static DoubleMeasurement dyn(this double value) => value.Dynes();
    /// <summary>Creates a decimal force measurement in dynes (dyn).</summary>
    public static DecimalMeasurement Dynes(this decimal value) => new(value, Unit.CGS.dyn);
    /// <summary>Alias for <see cref="Dynes(decimal)"/>.</summary>
    public static DecimalMeasurement Dyne(this decimal value) => value.Dynes();

    /// <summary>Creates an energy measurement in ergs.</summary>
    public static DoubleMeasurement Ergs(this double value) => new(value, Unit.CGS.erg);
    /// <summary>Alias for <see cref="Ergs(double)"/>.</summary>
    public static DoubleMeasurement Erg(this double value) => value.Ergs();
    /// <summary>Symbol alias for <see cref="Ergs(double)"/>.</summary>
    public static DoubleMeasurement erg(this double value) => value.Ergs();
    /// <summary>Creates a decimal energy measurement in ergs.</summary>
    public static DecimalMeasurement Ergs(this decimal value) => new(value, Unit.CGS.erg);
    /// <summary>Alias for <see cref="Ergs(decimal)"/>.</summary>
    public static DecimalMeasurement Erg(this decimal value) => value.Ergs();

    /// <summary>Creates a pressure measurement in baryes (Ba).</summary>
    public static DoubleMeasurement Baryes(this double value) => new(value, Unit.CGS.Ba);
    /// <summary>Alias for <see cref="Baryes(double)"/>.</summary>
    public static DoubleMeasurement Barye(this double value) => value.Baryes();
    /// <summary>Symbol alias for <see cref="Baryes(double)"/>.</summary>
    public static DoubleMeasurement Ba(this double value) => value.Baryes();
    /// <summary>Creates a decimal pressure measurement in baryes (Ba).</summary>
    public static DecimalMeasurement Baryes(this decimal value) => new(value, Unit.CGS.Ba);
    /// <summary>Alias for <see cref="Baryes(decimal)"/>.</summary>
    public static DecimalMeasurement Barye(this decimal value) => value.Baryes();

    /// <summary>Creates a dynamic viscosity measurement in poise (P).</summary>
    public static DoubleMeasurement Poise(this double value) => new(value, Unit.CGS.P);
    /// <summary>Symbol alias for <see cref="Poise(double)"/>.</summary>
    public static DoubleMeasurement P(this double value) => value.Poise();
    /// <summary>Creates a decimal dynamic viscosity measurement in poise (P).</summary>
    public static DecimalMeasurement Poise(this decimal value) => new(value, Unit.CGS.P);

    /// <summary>Creates a kinematic viscosity measurement in stokes (St).</summary>
    public static DoubleMeasurement Stokes(this double value) => new(value, Unit.CGS.St);
    /// <summary>Symbol alias for <see cref="Stokes(double)"/>.</summary>
    public static DoubleMeasurement St(this double value) => value.Stokes();
    /// <summary>Creates a decimal kinematic viscosity measurement in stokes (St).</summary>
    public static DecimalMeasurement Stokes(this decimal value) => new(value, Unit.CGS.St);
}
