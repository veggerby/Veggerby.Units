using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    /// <summary>
    /// SI Unit System. See http://en.wikipedia.org/wiki/International_System_of_Units.
    /// </summary>
    public class InternationalUnitSystem : UnitSystem
    {
        private readonly Unit _m;
        private readonly Unit _g;
        private readonly Unit _kg;
        private readonly Unit _s;
        private readonly Unit _A;
        private readonly Unit _K;
        private readonly Unit _cd;
        private readonly Unit _n;

        public Unit m { get { return _m; } }
        public Unit g { get { return _g; } }
        public Unit kg { get { return _kg; } }
        public Unit s { get { return _s; } }
        public Unit A { get { return _A; } }
        public Unit K { get { return _K; } }
        public Unit cd { get { return _cd; } }
        public Unit n { get { return _n; } }

        public InternationalUnitSystem()
        {
            _m = new BasicUnit("m", "meter", this, Dimension.Length);
            _g = new BasicUnit("g", "gram", this, Dimension.Mass);
            _kg = Prefix.k * _g;
            _s = new BasicUnit("s", "second", this, Dimension.Time);
            _A = new BasicUnit("A", "ampere", this, Dimension.ElectricCurrent);
            _K = new BasicUnit("K", "kelvin", this, Dimension.ThermodynamicTemperature);
            _cd = new BasicUnit("cd", "candela", this, Dimension.LuminousIntensity);
            _n = new BasicUnit("mol", "mole", this, Dimension.AmountOfSubstance);
        }
    }
}