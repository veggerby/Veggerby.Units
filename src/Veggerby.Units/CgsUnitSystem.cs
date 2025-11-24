using System;

namespace Veggerby.Units;

/// <summary>
/// CGS (Centimeter-Gram-Second) unit system. Provides access to fundamental CGS units and common derived units
/// used in physics, chemistry, and astronomy. All CGS units are defined with scale factors relative to SI base
/// units to enable seamless conversion between systems.
/// </summary>
public sealed class CgsUnitSystem : UnitSystem
{
    /// <summary>Scale factor: 1 cm = 0.01 m.</summary>
    public const double CentimeterToMeter = 0.01;
    /// <summary>Scale factor: 1 g = 0.001 kg.</summary>
    public const double GramToKilogram = 0.001;
    /// <summary>Scale factor: 1 dyn = 10^-5 N = g·cm/s².</summary>
    public const double DyneToNewton = 1e-5;
    /// <summary>Scale factor: 1 erg = 10^-7 J = dyn·cm.</summary>
    public const double ErgToJoule = 1e-7;
    /// <summary>Scale factor: 1 Ba (barye) = 0.1 Pa = dyn/cm².</summary>
    public const double BaryeToPascal = 0.1;
    /// <summary>Scale factor: 1 P (poise) = 0.1 Pa·s = g/(cm·s).</summary>
    public const double PoiseToPascalSecond = 0.1;
    /// <summary>Scale factor: 1 St (stokes) = 10^-4 m²/s = cm²/s.</summary>
    public const double StokesToSquareMeterPerSecond = 1e-4;
    /// <summary>Scale factor: 1 G (gauss) = 10^-4 T.</summary>
    public const double GaussToTesla = 1e-4;
    /// <summary>Scale factor: 1 Mx (maxwell) = 10^-8 Wb.</summary>
    public const double MaxwellToWeber = 1e-8;
    /// <summary>Scale factor: 1 abA (abampere) = 10 A.</summary>
    public const double AbampereToAmpere = 10.0;

    // Base units
    /// <summary>Centimeter – base unit of length in CGS.</summary>
    public Unit cm { get; }
    /// <summary>Gram – base unit of mass in CGS.</summary>
    public Unit g { get; }
    /// <summary>Second – base unit of time (reuses SI).</summary>
    public Unit s => Unit.SI.s;

    // Mechanical derived units
    /// <summary>Dyne (dyn) – CGS unit of force = g·cm/s².</summary>
    public Unit dyn { get; }
    /// <summary>Erg – CGS unit of energy/work = dyn·cm = g·cm²/s².</summary>
    public Unit erg { get; }
    /// <summary>Barye (Ba) – CGS unit of pressure = dyn/cm².</summary>
    public Unit Ba { get; }
    /// <summary>Poise (P) – CGS unit of dynamic viscosity = g/(cm·s).</summary>
    public Unit P { get; }
    /// <summary>Stokes (St) – CGS unit of kinematic viscosity = cm²/s.</summary>
    public Unit St { get; }

    // Electromagnetic units (Gaussian/EMU)
    /// <summary>Gauss (G) – CGS unit of magnetic flux density.</summary>
    public Unit G { get; }
    /// <summary>Maxwell (Mx) – CGS unit of magnetic flux.</summary>
    public Unit Mx { get; }
    /// <summary>Oersted (Oe) – CGS unit of magnetic field strength.</summary>
    public Unit Oe { get; }
    /// <summary>Abampere (abA) – CGS electromagnetic unit of electric current.</summary>
    public Unit abA { get; }
    /// <summary>Abcoulomb (abC) – CGS electromagnetic unit of electric charge.</summary>
    public Unit abC { get; }
    /// <summary>Abvolt (abV) – CGS electromagnetic unit of electric potential.</summary>
    public Unit abV { get; }
    /// <summary>Abohm – CGS electromagnetic unit of electrical resistance.</summary>
    public Unit abohm { get; }

    /// <summary>
    /// Initializes CGS units and their relationships to SI. All units remain immutable after construction.
    /// </summary>
    internal CgsUnitSystem()
    {
        // Base units
        cm = new ScaleUnit("cm", "centimeter", CentimeterToMeter, Unit.SI.m, this);
        g = new ScaleUnit("g", "gram", GramToKilogram, Unit.SI.kg, this);

        // Mechanical derived units
        // dyne = g·cm/s²
        dyn = new ScaleUnit("dyn", "dyne", DyneToNewton, Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2), this);
        
        // erg = dyn·cm = g·cm²/s²
        erg = new ScaleUnit("erg", "erg", ErgToJoule, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2), this);
        
        // barye = dyn/cm²
        Ba = new ScaleUnit("Ba", "barye", BaryeToPascal, Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2)), this);
        
        // poise = g/(cm·s)
        P = new ScaleUnit("P", "poise", PoiseToPascalSecond, Unit.SI.kg / (Unit.SI.m * Unit.SI.s), this);
        
        // stokes = cm²/s
        St = new ScaleUnit("St", "stokes", StokesToSquareMeterPerSecond, (Unit.SI.m ^ 2) / Unit.SI.s, this);

        // Electromagnetic units (Gaussian/EMU)
        // gauss = 10^-4 tesla
        G = new ScaleUnit("G", "gauss", GaussToTesla, Unit.SI.kg / (Unit.SI.A * (Unit.SI.s ^ 2)), this);
        
        // maxwell = 10^-8 weber
        Mx = new ScaleUnit("Mx", "maxwell", MaxwellToWeber, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.A * (Unit.SI.s ^ 2)), this);
        
        // oersted = 1000/(4π) A/m ≈ 79.5774715459 A/m
        // For simplicity, using the exact conversion factor
        Oe = new ScaleUnit("Oe", "oersted", 1000.0 / (4.0 * Math.PI), Unit.SI.A / Unit.SI.m, this);
        
        // abampere = 10 A
        abA = new ScaleUnit("abA", "abampere", AbampereToAmpere, Unit.SI.A, this);
        
        // abcoulomb = 10 C = 10 A·s
        abC = new ScaleUnit("abC", "abcoulomb", 10.0, Unit.SI.A * Unit.SI.s, this);
        
        // abvolt = 10^-8 V
        abV = new ScaleUnit("abV", "abvolt", 1e-8, Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.A * (Unit.SI.s ^ 3)), this);
        
        // abohm = 10^-9 Ω
        abohm = new ScaleUnit("abΩ", "abohm", 1e-9, Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.A ^ 2) * (Unit.SI.s ^ 3)), this);
    }
}
