using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// International System of Units (SI). Provides programmatic access to the base SI units. All derived units in this
/// library ultimately reference these definitions for scale factor computation and dimensional analysis.
/// </summary>
/// <remarks>
/// Unit definitions align with the QUDT ontology's SI unit specifications and the SI Brochure (BIPM).
/// All base unit scale factors and dimensional exponents have been validated against QUDT canonical values.
/// <para>
/// QUDT SI Units: http://qudt.org/vocab/unit/
/// SI Brochure (9th edition): https://www.bipm.org/en/publications/si-brochure/
/// </para>
/// </remarks>
public sealed class InternationalUnitSystem : UnitSystem
{
    /// <summary>Metre – base unit of length.</summary>
    /// <remarks>QUDT: <c>unit:M</c> (http://qudt.org/vocab/unit/M)</remarks>
    public Unit m { get; }
    /// <summary>Gram – base mass unit in this library (kilogram derived via prefix k).</summary>
    /// <remarks>QUDT: <c>unit:GM</c> (http://qudt.org/vocab/unit/GM)</remarks>
    public Unit g { get; }
    /// <summary>Kilogram – SI base unit of mass (derived here from gram by applying kilo prefix).</summary>
    /// <remarks>QUDT: <c>unit:KiloGM</c> (http://qudt.org/vocab/unit/KiloGM)</remarks>
    public Unit kg { get; }
    /// <summary>Second – base unit of time.</summary>
    /// <remarks>QUDT: <c>unit:SEC</c> (http://qudt.org/vocab/unit/SEC)</remarks>
    public Unit s { get; }
    /// <summary>Ampere – base unit of electric current.</summary>
    /// <remarks>QUDT: <c>unit:A</c> (http://qudt.org/vocab/unit/A)</remarks>
    public Unit A { get; }
    /// <summary>Kelvin – base unit of thermodynamic temperature.</summary>
    /// <remarks>QUDT: <c>unit:K</c> (http://qudt.org/vocab/unit/K)</remarks>
    public Unit K { get; }
    /// <summary>Celsius – affine temperature unit (°C).</summary>
    /// <remarks>
    /// QUDT: <c>unit:DEG_C</c> (http://qudt.org/vocab/unit/DEG_C) with offset +273.15 from Kelvin.
    /// Represents absolute temperature on the Celsius scale (affine transformation).
    /// </remarks>
    public Unit C { get; }
    /// <summary>Candela – base unit of luminous intensity.</summary>
    /// <remarks>QUDT: <c>unit:CD</c> (http://qudt.org/vocab/unit/CD)</remarks>
    public Unit cd { get; }
    /// <summary>Mole – base unit of amount of substance.</summary>
    /// <remarks>QUDT: <c>unit:MOL</c> (http://qudt.org/vocab/unit/MOL)</remarks>
    public Unit n { get; }
    /// <summary>Radian – coherent SI unit of plane angle (dimensionless).</summary>
    /// <remarks>QUDT: <c>unit:RAD</c> (http://qudt.org/vocab/unit/RAD)</remarks>
    public Unit rad { get; }
    /// <summary>Steradian – coherent SI unit of solid angle (dimensionless).</summary>
    /// <remarks>QUDT: <c>unit:SR</c> (http://qudt.org/vocab/unit/SR)</remarks>
    public Unit sr { get; }

    /// <summary>
    /// Creates a new SI system instance with all base units initialised. Instances are immutable after construction.
    /// </summary>
    internal InternationalUnitSystem()
    {
        m = new BasicUnit("m", "meter", this, Dimension.Length);
        g = new BasicUnit("g", "gram", this, Dimension.Mass);
        kg = Prefix.k * g;
        s = new BasicUnit("s", "second", this, Dimension.Time);
        A = new BasicUnit("A", "ampere", this, Dimension.ElectricCurrent);
        K = new BasicUnit("K", "kelvin", this, Dimension.ThermodynamicTemperature);
        C = new AffineUnit("°C", "celsius", K, 1d, 273.15d);
        cd = new BasicUnit("cd", "candela", this, Dimension.LuminousIntensity);
        n = new BasicUnit("mol", "mole", this, Dimension.AmountOfSubstance);
        rad = new BasicUnit("rad", "radian", this, Dimension.None);
        sr = new BasicUnit("sr", "steradian", this, Dimension.None);
    }
}