namespace Veggerby.Units
{
    public class Prefix
    {
        public static Prefix Empty = new Prefix(string.Empty, string.Empty, 1);

        public static Prefix da = new Prefix("deca", "da", 1E1);
        public static Prefix h = new Prefix("hecto", "h", 1E2);
        public static Prefix k = new Prefix("kilo", "k", 1E3);
        public static Prefix M = new Prefix("mega", "M", 1E6);
        public static Prefix G = new Prefix("giga", "G", 1E9);
        public static Prefix T = new Prefix("tera", "T", 1E12);
        public static Prefix P = new Prefix("peta", "P", 1E15);
        public static Prefix E = new Prefix("exa", "E", 1E18);
        public static Prefix Z = new Prefix("zetta", "Z", 1E21);
        public static Prefix Y = new Prefix("yotta", "Y", 1E24);

        public static Prefix d = new Prefix("deci", "d", 1E-1);
        public static Prefix c = new Prefix("centi", "c", 1E-2);
        public static Prefix m = new Prefix("milli", "m", 1E-3);
        public static Prefix μ = new Prefix("micro", "μ", 1E-6);
        public static Prefix n = new Prefix("nano", "n", 1E-9);
        public static Prefix p = new Prefix("pico", "p", 1E-12);
        public static Prefix f = new Prefix("femto", "f", 1E-15);
        public static Prefix a = new Prefix("atto", "a", 1E-18);
        public static Prefix z = new Prefix("zepto", "z", 1E-21);
        public static Prefix y = new Prefix("yocto", "y", 1E-24);

        private Prefix(string name, string symbol, double factor)
        {
            this._Name = name;
            this._Symbol = symbol;
            this._Factor = factor;
        }

        private readonly string _Name;
        private readonly string _Symbol;
        private readonly double _Factor;

        public string Name
        {
            get { return this._Name; }
        }

        public string Symbol
        {
            get { return this._Symbol; }
        }

        public double Factor
        {
            get { return this._Factor; }
        }
    }
}
