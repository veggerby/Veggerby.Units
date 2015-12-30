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
        public Unit ft { get { return _ft; } }
        public Unit @in { get { return _in; } }
        public Unit thou { get { return _thou; } }
        public Unit ya { get { return _ya; } }
        public Unit ch { get { return _ch; } }
        public Unit fur { get { return _fur; } }
        public Unit mi { get { return _mi; } }
        public Unit lea { get { return _lea; } }
        public Unit rod { get { return _rod; } }
        public Unit link { get { return _link; } }

        //area
        public Unit acre { get { return _acre; } }
        public Unit rood { get { return _rood; } }
        public Unit perch { get { return _perch; } }
        
        // volume
        public Unit fl_oz { get { return _fl_oz; } }
        public Unit gi { get { return _gi; } }
        public Unit pt { get { return _pt; } }
        public Unit qt { get { return _qt; } }
        public Unit gal { get { return _gal; } }

        //weight
        public Unit lb { get { return _lb; } }
        public Unit oz { get { return _oz; } }
        public Unit drc { get { return _drc; } }
        public Unit gr { get { return _gr; } }
        public Unit st { get { return _st; } }
        public Unit qtr { get { return _qtr; } }
        public Unit cwt { get { return _cwt; } }
        public Unit t { get { return _t; } }

        //other
        public Unit s { get { return Unit.SI.s; } }
        public Unit A { get { return Unit.SI.A; } }
        public Unit K { get { return Unit.SI.K; } }
        public Unit cd { get { return Unit.SI.cd; } }
        public Unit n { get { return Unit.SI.n; } }

        public ImperialUnitSystem()
        {
            //length
            _ft = new ScaleUnit("ft", "feet", FeetToMetres, Unit.SI.m, this);
            _in = new ScaleUnit("in", "inch", 1D / 12, _ft);
            _thou = new ScaleUnit("th", "thou", 1D / 1000, _in);
            _ya = new ScaleUnit("yd", "yard", 3, _ft);
            _ch = new ScaleUnit("ch", "chain", 22, _ya);
            _fur = new ScaleUnit("fur", "furlong", 10, _ch);
            _mi = new ScaleUnit("mi", "mile", 8, _fur);
            _lea = new ScaleUnit("lea", "league", 3, _mi);
            _rod = new ScaleUnit("rod", "rod", 1D / 4, _ch);
            _link = new ScaleUnit("link", "link", 1D / 25, _rod);

            //area
            _acre = new DerivedUnit("acre", "acre", _fur * _ch);
            _rood = new DerivedUnit("rood", "rood", _fur * _rod);
            _perch = new DerivedUnit("perch", "perch", _rod ^ 2);

            //volume
            _cubic_inch = _in ^ 3;
            _pt = new ScaleUnit("pt", "pint", CubicInchToPint, _cubic_inch);
            _gi = new ScaleUnit("gi", "gill", 1D / 4, _pt);
            _fl_oz = new ScaleUnit("fl oz", "fluid ounce", 1D / 5, _gi);
            _qt = new ScaleUnit("qt", "quart", 2, _pt);
            _gal = new ScaleUnit("gal", "gallon", 4, _qt);

            //weight
            _lb = new ScaleUnit("lb", "pound", PoundToKilogram, Unit.SI.kg, this);
            _oz = new ScaleUnit("oz", "ounce", 1D / 16, _lb);
            _drc = new ScaleUnit("drc", "drachm", 1D / 16, _oz);
            _gr = new ScaleUnit("gr", "grain", 1D / 7000, _lb);
            _st = new ScaleUnit("st", "stone", 14, _lb);
            _qtr = new ScaleUnit("qtr", "quarter", 28, _lb);
            _cwt = new ScaleUnit("cwt", "hundredweight", 4, _qtr);
            _t = new ScaleUnit("t", "ton", 20, _cwt);
        }
    }
}