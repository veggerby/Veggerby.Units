using Veggerby.Units.Dimensions;

namespace Veggerby.Units
{
    public class ImperialUnitSystem : UnitSystem
    {
        public const double FeetToMetres = 0.3048;
        public const double PoundToKilogram = 0.45359237;
        public const double CubicInchToPint = 34.677;

        //length
        private readonly Unit _ft;
        private readonly Unit _in;
        private readonly Unit _thou;
        private readonly Unit _ya;
        private readonly Unit _ch;
        private readonly Unit _fur;
        private readonly Unit _mi;
        private readonly Unit _lea;
        private readonly Unit _rod;
        private readonly Unit _link;

        //area
        private readonly Unit _acre;
        private readonly Unit _rood;
        private readonly Unit _perch;

        //volume
        private readonly Unit _cubic_inch; // only used as volume base
        private readonly Unit _fl_oz;
        private readonly Unit _gi;
        private readonly Unit _pt;
        private readonly Unit _qt;
        private readonly Unit _gal;

        //weight
        private readonly Unit _lb;
        private readonly Unit _oz;
        private readonly Unit _drc;
        private readonly Unit _gr;
        private readonly Unit _st;
        private readonly Unit _qtr;
        private readonly Unit _cwt;
        private readonly Unit _t;

        //length
        public Unit ft { get { return this._ft; } }
        public Unit @in { get { return this._in; } }
        public Unit thou { get { return this._thou; } }
        public Unit ya { get { return this._ya; } }
        public Unit ch { get { return this._ch; } }
        public Unit fur { get { return this._fur; } }
        public Unit mi { get { return this._mi; } }
        public Unit lea { get { return this._lea; } }
        public Unit rod { get { return this._rod; } }
        public Unit link { get { return this._link; } }

        //area
        public Unit acre { get { return this._acre; } }
        public Unit rood { get { return this._rood; } }
        public Unit perch { get { return this._perch; } }
        
        // volume
        public Unit fl_oz { get { return this._fl_oz; } }
        public Unit gi { get { return this._gi; } }
        public Unit pt { get { return this._pt; } }
        public Unit qt { get { return this._qt; } }
        public Unit gal { get { return this._gal; } }

        //weight
        public Unit lb { get { return this._lb; } }
        public Unit oz { get { return this._oz; } }
        public Unit drc { get { return this._drc; } }
        public Unit gr { get { return this._gr; } }
        public Unit st { get { return this._st; } }
        public Unit qtr { get { return this._qtr; } }
        public Unit cwt { get { return this._cwt; } }
        public Unit t { get { return this._t; } }

        //other
        public Unit s { get { return Unit.SI.s; } }
        public Unit A { get { return Unit.SI.A; } }
        public Unit K { get { return Unit.SI.K; } }
        public Unit cd { get { return Unit.SI.cd; } }
        public Unit n { get { return Unit.SI.n; } }

        public ImperialUnitSystem()
        {
            //length
            this._ft = new ScaleUnit("ft", "feet", FeetToMetres, Unit.SI.m, this);
            this._in = new ScaleUnit("in", "inch", 1D / 12, this._ft);
            this._thou = new ScaleUnit("th", "thou", 1D / 1000, this._in);
            this._ya = new ScaleUnit("yd", "yard", 3, this._ft);
            this._ch = new ScaleUnit("ch", "chain", 22, this._ya);
            this._fur = new ScaleUnit("fur", "furlong", 10, this._ch);
            this._mi = new ScaleUnit("mi", "mile", 8, this._fur);
            this._lea = new ScaleUnit("lea", "league", 3, this._mi);
            this._rod = new ScaleUnit("rod", "rod", 1D / 4, this._ch);
            this._link = new ScaleUnit("link", "link", 1D / 25, this._rod);

            //area
            this._acre = new DerivedUnit("acre", "acre", this._fur * this._ch);
            this._rood = new DerivedUnit("rood", "rood", this._fur * this._rod);
            this._perch = new DerivedUnit("perch", "perch", this._rod ^ 2);

            //volume
            this._cubic_inch = this._in ^ 3;
            this._pt = new ScaleUnit("pt", "pint", CubicInchToPint, this._cubic_inch);
            this._gi = new ScaleUnit("gi", "gill", 1D / 4, this._pt);
            this._fl_oz = new ScaleUnit("fl oz", "fluid ounce", 1D / 5, this._gi);
            this._qt = new ScaleUnit("qt", "quart", 2, this._pt);
            this._gal = new ScaleUnit("gal", "gallon", 4, this._qt);

            //weight
            this._lb = new ScaleUnit("lb", "pound", PoundToKilogram, Unit.SI.kg, this);
            this._oz = new ScaleUnit("oz", "ounce", 1D / 16, this._lb);
            this._drc = new ScaleUnit("drc", "drachm", 1D / 16, this._oz);
            this._gr = new ScaleUnit("gr", "grain", 1D / 7000, this._lb);
            this._st = new ScaleUnit("st", "stone", 14, this._lb);
            this._qtr = new ScaleUnit("qtr", "quarter", 28, this._lb);
            this._cwt = new ScaleUnit("cwt", "hundredweight", 4, this._qtr);
            this._t = new ScaleUnit("t", "ton", 20, this._cwt);
        }
    }
}