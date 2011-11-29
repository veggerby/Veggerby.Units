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

        public Unit m { get { return this._m; } }
        private Unit g { get { return this._g; } }
        public Unit kg { get { return this._kg; } }
        public Unit s { get { return this._s; } }
        public Unit A { get { return this._A; } }
        public Unit K { get { return this._K; } }
        public Unit cd { get { return this._cd; } }
        public Unit n { get { return this._n; } }

        public InternationalUnitSystem()
        {
            this._m = new BasicUnit("m", "meter", this, Dimension.Length);
            this._g = new BasicUnit("g", "gram", this, Dimension.Mass);
            this._kg = Prefix.k * this._g;
            this._s = new BasicUnit("s", "second", this, Dimension.Time);
            this._A = new BasicUnit("A", "ampere", this, Dimension.ElectricCurrent);
            this._K = new BasicUnit("K", "kelvin", this, Dimension.ThermodynamicTemperature);
            this._cd = new BasicUnit("cd", "candela", this, Dimension.LuminousIntensity);
            this._n = new BasicUnit("mol", "mole", this, Dimension.AmountOfSubstance);
        }
    }
}