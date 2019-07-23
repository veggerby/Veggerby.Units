namespace Veggerby.Units
{
    public class ImperialUnitSystem : UnitSystem
    {
        public const double FeetToMetres = 0.3048;
        public const double PoundToKilogram = 0.45359237;
        public const double CubicInchToPint = 34.677;

        //length
        public Unit ft { get; }
        public Unit @in { get; }
        public Unit thou { get; }
        public Unit ya { get; }
        public Unit ch { get; }
        public Unit fur { get; }
        public Unit mi { get; }
        public Unit lea { get; }
        public Unit rod { get; }
        public Unit link { get; }

        //area
        private readonly Unit _cubic_inch; // only used as volume base
        public Unit acre { get; }
        public Unit rood { get; }
        public Unit perch { get; }

        // volume
        public Unit fl_oz { get; }
        public Unit gi { get; }
        public Unit pt { get; }
        public Unit qt { get; }
        public Unit gal { get; }

        //weight
        public Unit lb { get; }
        public Unit oz { get; }
        public Unit drc { get; }
        public Unit gr { get; }
        public Unit st { get; }
        public Unit qtr { get; }
        public Unit cwt { get; }
        public Unit t { get; }

        //other
        public Unit s => Unit.SI.s;
        public Unit A => Unit.SI.A;
        public Unit K => Unit.SI.K;
        public Unit cd => Unit.SI.cd;
        public Unit n => Unit.SI.n;

        public ImperialUnitSystem()
        {
            //length
            ft = new ScaleUnit("ft", "feet", FeetToMetres, Unit.SI.m, this);
            @in = new ScaleUnit("in", "inch", 1D / 12, ft);
            thou = new ScaleUnit("th", "thou", 1D / 1000, @in);
            ya = new ScaleUnit("yd", "yard", 3, ft);
            ch = new ScaleUnit("ch", "chain", 22, ya);
            fur = new ScaleUnit("fur", "furlong", 10, ch);
            mi = new ScaleUnit("mi", "mile", 8, fur);
            lea = new ScaleUnit("lea", "league", 3, mi);
            rod = new ScaleUnit("rod", "rod", 1D / 4, ch);
            link = new ScaleUnit("link", "link", 1D / 25, rod);

            //area
            acre = new DerivedUnit("acre", "acre", fur * ch);
            rood = new DerivedUnit("rood", "rood", fur * rod);
            perch = new DerivedUnit("perch", "perch", rod ^ 2);

            //volume
            _cubic_inch = @in ^ 3;
            pt = new ScaleUnit("pt", "pint", CubicInchToPint, _cubic_inch);
            gi = new ScaleUnit("gi", "gill", 1D / 4, pt);
            fl_oz = new ScaleUnit("fl oz", "fluid ounce", 1D / 5, gi);
            qt = new ScaleUnit("qt", "quart", 2, pt);
            gal = new ScaleUnit("gal", "gallon", 4, qt);

            //weight
            lb = new ScaleUnit("lb", "pound", PoundToKilogram, Unit.SI.kg, this);
            oz = new ScaleUnit("oz", "ounce", 1D / 16, lb);
            drc = new ScaleUnit("drc", "drachm", 1D / 16, oz);
            gr = new ScaleUnit("gr", "grain", 1D / 7000, lb);
            st = new ScaleUnit("st", "stone", 14, lb);
            qtr = new ScaleUnit("qtr", "quarter", 28, lb);
            cwt = new ScaleUnit("cwt", "hundredweight", 4, qtr);
            t = new ScaleUnit("t", "ton", 20, cwt);
        }
    }
}