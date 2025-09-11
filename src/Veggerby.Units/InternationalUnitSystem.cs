using Veggerby.Units.Dimensions;

namespace Veggerby.Units;

/// <summary>
/// SI Unit System. See http://en.wikipedia.org/wiki/International_System_of_Units.
/// </summary>
public class InternationalUnitSystem : UnitSystem
{
    public Unit m { get; }
    public Unit g { get; }
    public Unit kg { get; }
    public Unit s { get; }
    public Unit A { get; }
    public Unit K { get; }
    public Unit cd { get; }
    public Unit n { get; }

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