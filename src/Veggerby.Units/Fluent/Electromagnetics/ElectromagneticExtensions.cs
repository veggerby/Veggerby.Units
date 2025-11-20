using Veggerby.Units.Quantities;

namespace Veggerby.Units.Fluent.Electromagnetics;

/// <summary>Electromagnetic quantity numeric extensions (current, charge, fields, circuits, etc.).</summary>
public static class ElectromagneticExtensions
{
    /// <summary>Creates a measurement in amperes (A) for electric current.</summary>
    public static DoubleMeasurement Amperes(this double value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Alias for <see cref="Amperes(double)"/>.</summary>
    public static DoubleMeasurement Ampere(this double value) => value.Amperes();
    /// <summary>Symbol alias for <see cref="Amperes(double)"/>.</summary>
    public static DoubleMeasurement A(this double value) => value.Amperes();
    /// <summary>Creates a decimal measurement in amperes (A).</summary>
    public static DecimalMeasurement Amperes(this decimal value) => new(value, QuantityKinds.ElectricCurrent.CanonicalUnit);
    /// <summary>Alias for <see cref="Amperes(decimal)"/>.</summary>
    public static DecimalMeasurement Ampere(this decimal value) => value.Amperes();

    /// <summary>Creates a measurement in coulombs (C) for electric charge.</summary>
    public static DoubleMeasurement Coulombs(this double value) => new(value, QuantityKinds.ElectricCharge.CanonicalUnit);
    /// <summary>Alias for <see cref="Coulombs(double)"/>.</summary>
    public static DoubleMeasurement Coulomb(this double value) => value.Coulombs();
    /// <summary>Symbol alias for <see cref="Coulombs(double)"/>.</summary>
    public static DoubleMeasurement C(this double value) => value.Coulombs();
    /// <summary>Creates a decimal measurement in coulombs (C).</summary>
    public static DecimalMeasurement Coulombs(this decimal value) => new(value, QuantityKinds.ElectricCharge.CanonicalUnit);
    /// <summary>Alias for <see cref="Coulombs(decimal)"/>.</summary>
    public static DecimalMeasurement Coulomb(this decimal value) => value.Coulombs();

    /// <summary>Creates a measurement in ohms (Ω) for electric resistance.</summary>
    public static DoubleMeasurement Ohms(this double value) => new(value, QuantityKinds.ElectricResistance.CanonicalUnit);
    /// <summary>Alias for <see cref="Ohms(double)"/>.</summary>
    public static DoubleMeasurement Ohm(this double value) => value.Ohms();
    /// <summary>Creates a decimal measurement in ohms (Ω).</summary>
    public static DecimalMeasurement Ohms(this decimal value) => new(value, QuantityKinds.ElectricResistance.CanonicalUnit);
    /// <summary>Alias for <see cref="Ohms(decimal)"/>.</summary>
    public static DecimalMeasurement Ohm(this decimal value) => value.Ohms();

    /// <summary>Creates a measurement in siemens (S) for electric conductance.</summary>
    public static DoubleMeasurement Siemens(this double value) => new(value, QuantityKinds.ElectricConductance.CanonicalUnit);
    /// <summary>Symbol alias for <see cref="Siemens(double)"/>.</summary>
    public static DoubleMeasurement S(this double value) => value.Siemens();
    /// <summary>Creates a decimal measurement in siemens (S).</summary>
    public static DecimalMeasurement Siemens(this decimal value) => new(value, QuantityKinds.ElectricConductance.CanonicalUnit);

    /// <summary>Creates a measurement in farads (F) for capacitance.</summary>
    public static DoubleMeasurement Farads(this double value) => new(value, QuantityKinds.Capacitance.CanonicalUnit);
    /// <summary>Alias for <see cref="Farads(double)"/>.</summary>
    public static DoubleMeasurement Farad(this double value) => value.Farads();
    /// <summary>Symbol alias for <see cref="Farads(double)"/>.</summary>
    public static DoubleMeasurement F(this double value) => value.Farads();
    /// <summary>Creates a decimal measurement in farads (F).</summary>
    public static DecimalMeasurement Farads(this decimal value) => new(value, QuantityKinds.Capacitance.CanonicalUnit);
    /// <summary>Alias for <see cref="Farads(decimal)"/>.</summary>
    public static DecimalMeasurement Farad(this decimal value) => value.Farads();

    /// <summary>Creates a measurement in henries (H) for inductance.</summary>
    public static DoubleMeasurement Henries(this double value) => new(value, QuantityKinds.Inductance.CanonicalUnit);
    /// <summary>Alias for <see cref="Henries(double)"/>.</summary>
    public static DoubleMeasurement Henry(this double value) => value.Henries();
    /// <summary>Symbol alias for <see cref="Henries(double)"/>.</summary>
    public static DoubleMeasurement H(this double value) => value.Henries();
    /// <summary>Creates a decimal measurement in henries (H).</summary>
    public static DecimalMeasurement Henries(this decimal value) => new(value, QuantityKinds.Inductance.CanonicalUnit);
    /// <summary>Alias for <see cref="Henries(decimal)"/>.</summary>
    public static DecimalMeasurement Henry(this decimal value) => value.Henries();

    /// <summary>Creates a measurement in webers (Wb) for magnetic flux.</summary>
    public static DoubleMeasurement Webers(this double value) => new(value, QuantityKinds.MagneticFlux.CanonicalUnit);
    /// <summary>Alias for <see cref="Webers(double)"/>.</summary>
    public static DoubleMeasurement Weber(this double value) => value.Webers();
    /// <summary>Symbol alias for <see cref="Webers(double)"/>.</summary>
    public static DoubleMeasurement Wb(this double value) => value.Webers();
    /// <summary>Creates a decimal measurement in webers (Wb).</summary>
    public static DecimalMeasurement Webers(this decimal value) => new(value, QuantityKinds.MagneticFlux.CanonicalUnit);
    /// <summary>Alias for <see cref="Webers(decimal)"/>.</summary>
    public static DecimalMeasurement Weber(this decimal value) => value.Webers();

    /// <summary>Creates a measurement in teslas (T) for magnetic flux density.</summary>
    public static DoubleMeasurement Teslas(this double value) => new(value, QuantityKinds.MagneticFluxDensity.CanonicalUnit);
    /// <summary>Alias for <see cref="Teslas(double)"/>.</summary>
    public static DoubleMeasurement Tesla(this double value) => value.Teslas();
    /// <summary>Symbol alias for <see cref="Teslas(double)"/>.</summary>
    public static DoubleMeasurement T(this double value) => value.Teslas();
    /// <summary>Creates a decimal measurement in teslas (T).</summary>
    public static DecimalMeasurement Teslas(this decimal value) => new(value, QuantityKinds.MagneticFluxDensity.CanonicalUnit);
    /// <summary>Alias for <see cref="Teslas(decimal)"/>.</summary>
    public static DecimalMeasurement Tesla(this decimal value) => value.Teslas();

    /// <summary>Creates a measurement in hertz (Hz) for frequency.</summary>
    public static DoubleMeasurement Hertz(this double value) => new(value, QuantityKinds.Frequency.CanonicalUnit);
    /// <summary>Symbol alias for <see cref="Hertz(double)"/>.</summary>
    public static DoubleMeasurement Hz(this double value) => value.Hertz();
    /// <summary>Creates a decimal measurement in hertz (Hz).</summary>
    public static DecimalMeasurement Hertz(this decimal value) => new(value, QuantityKinds.Frequency.CanonicalUnit);

    /// <summary>Creates a measurement representing electric field strength (V/m).</summary>
    public static DoubleMeasurement VoltsPerMeter(this double value) => new(value, QuantityKinds.ElectricFieldStrength.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing electric field strength (V/m).</summary>
    public static DecimalMeasurement VoltsPerMeter(this decimal value) => new(value, QuantityKinds.ElectricFieldStrength.CanonicalUnit);

    /// <summary>Creates a measurement representing magnetic field strength (A/m).</summary>
    public static DoubleMeasurement AmperesPerMeter(this double value) => new(value, QuantityKinds.MagneticFieldStrength.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing magnetic field strength (A/m).</summary>
    public static DecimalMeasurement AmperesPerMeter(this decimal value) => new(value, QuantityKinds.MagneticFieldStrength.CanonicalUnit);

    /// <summary>Creates a measurement representing electric charge density (C/m³).</summary>
    public static DoubleMeasurement ElectricChargeDensity(this double value) => new(value, QuantityKinds.ElectricChargeDensity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing electric charge density (C/m³).</summary>
    public static DecimalMeasurement ElectricChargeDensity(this decimal value) => new(value, QuantityKinds.ElectricChargeDensity.CanonicalUnit);

    /// <summary>Creates a measurement representing electric current density (A/m²).</summary>
    public static DoubleMeasurement ElectricCurrentDensity(this double value) => new(value, QuantityKinds.ElectricCurrentDensity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing electric current density (A/m²).</summary>
    public static DecimalMeasurement ElectricCurrentDensity(this decimal value) => new(value, QuantityKinds.ElectricCurrentDensity.CanonicalUnit);

    /// <summary>Creates a measurement representing permittivity (F/m).</summary>
    public static DoubleMeasurement Permittivity(this double value) => new(value, QuantityKinds.Permittivity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing permittivity (F/m).</summary>
    public static DecimalMeasurement Permittivity(this decimal value) => new(value, QuantityKinds.Permittivity.CanonicalUnit);

    /// <summary>Creates a measurement representing permeability (H/m).</summary>
    public static DoubleMeasurement Permeability(this double value) => new(value, QuantityKinds.Permeability.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing permeability (H/m).</summary>
    public static DecimalMeasurement Permeability(this decimal value) => new(value, QuantityKinds.Permeability.CanonicalUnit);

    /// <summary>Creates a measurement representing electrical conductivity (S/m).</summary>
    public static DoubleMeasurement ElectricalConductivity(this double value) => new(value, QuantityKinds.ElectricalConductivity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing electrical conductivity (S/m).</summary>
    public static DecimalMeasurement ElectricalConductivity(this decimal value) => new(value, QuantityKinds.ElectricalConductivity.CanonicalUnit);

    /// <summary>Creates a measurement representing electrical resistivity (Ω·m).</summary>
    public static DoubleMeasurement ElectricalResistivity(this double value) => new(value, QuantityKinds.ElectricalResistivity.CanonicalUnit);
    /// <summary>Creates a decimal measurement representing electrical resistivity (Ω·m).</summary>
    public static DecimalMeasurement ElectricalResistivity(this decimal value) => new(value, QuantityKinds.ElectricalResistivity.CanonicalUnit);
}
