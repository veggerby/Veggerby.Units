using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// International System of Units (SI). Provides programmatic access to the base SI units. All derived units in this
/// library ultimately reference these definitions for scale factor computation and dimensional analysis.
/// </summary>
public class InternationalUnitSystem : UnitSystem
{
    /// <summary>Metre – base unit of length.</summary>
    public Unit m { get; }
    /// <summary>Gram – base mass unit in this library (kilogram derived via prefix k).</summary>
    public Unit g { get; }
    /// <summary>Kilogram – SI base unit of mass (derived here from gram by applying kilo prefix).</summary>
    public Unit kg { get; }
    /// <summary>Second – base unit of time.</summary>
    public Unit s { get; }
    /// <summary>Ampere – base unit of electric current.</summary>
    public Unit A { get; }
    /// <summary>Kelvin – base unit of thermodynamic temperature.</summary>
    public Unit K { get; }
    /// <summary>Candela – base unit of luminous intensity.</summary>
    public Unit cd { get; }
    /// <summary>Mole – base unit of amount of substance.</summary>
    public Unit n { get; }

    /// <summary>
    /// Creates a new SI system instance with all base units initialised. Instances are immutable after construction.
    /// </summary>
    public InternationalUnitSystem()
    {
        m = new BasicUnit("m", "meter", this, Dimension.Length);
        g = new BasicUnit("g", "gram", this, Dimension.Mass);
        kg = Prefix.k * g;
        s = new BasicUnit("s", "second", this, Dimension.Time);
        A = new BasicUnit("A", "ampere", this, Dimension.ElectricCurrent);
        K = new BasicUnit("K", "kelvin", this, Dimension.ThermodynamicTemperature);
        cd = new BasicUnit("cd", "candela", this, Dimension.LuminousIntensity);
        n = new BasicUnit("mol", "mole", this, Dimension.AmountOfSubstance);
    }
}