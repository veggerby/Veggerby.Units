namespace Veggerby.Units;

/// <summary>
/// Imperial unit system subset used for inter‑system conversions (length, area, volume, weight). Where possible the
/// underlying scale factors are expressed relative to SI base units to allow seamless composition with SI units.
/// </summary>
public class ImperialUnitSystem : UnitSystem
{
    /// <summary>Scale factor: 1 ft = 0.3048 m.</summary>
    public const double FeetToMetres = 0.3048;
    /// <summary>Scale factor: 1 lb = 0.45359237 kg.</summary>
    public const double PoundToKilogram = 0.45359237;
    /// <summary>Scale factor used for volume construction (cubic inches to pint).</summary>
    public const double CubicInchToPint = 34.677;

    //length
    /// <summary>Foot.</summary>
    public Unit ft { get; }
    /// <summary>Inch.</summary>
    public Unit @in { get; }
    /// <summary>Thou (mil).</summary>
    public Unit thou { get; }
    /// <summary>Yard.</summary>
    public Unit ya { get; }
    /// <summary>Chain.</summary>
    public Unit ch { get; }
    /// <summary>Furlong.</summary>
    public Unit fur { get; }
    /// <summary>Mile.</summary>
    public Unit mi { get; }
    /// <summary>League.</summary>
    public Unit lea { get; }
    /// <summary>Rod.</summary>
    public Unit rod { get; }
    /// <summary>Link.</summary>
    public Unit link { get; }

    //area
    private readonly Unit _cubic_inch; // only used as volume base
    /// <summary>Acre.</summary>
    public Unit acre { get; }
    /// <summary>Rood.</summary>
    public Unit rood { get; }
    /// <summary>Perch.</summary>
    public Unit perch { get; }

    // volume
    /// <summary>Fluid ounce.</summary>
    public Unit fl_oz { get; }
    /// <summary>Gill.</summary>
    public Unit gi { get; }
    /// <summary>Pint.</summary>
    public Unit pt { get; }
    /// <summary>Quart.</summary>
    public Unit qt { get; }
    /// <summary>Gallon.</summary>
    public Unit gal { get; }

    //weight
    /// <summary>Pound.</summary>
    public Unit lb { get; }
    /// <summary>Ounce.</summary>
    public Unit oz { get; }
    /// <summary>Drachm.</summary>
    public Unit drc { get; }
    /// <summary>Grain.</summary>
    public Unit gr { get; }
    /// <summary>Stone.</summary>
    public Unit st { get; }
    /// <summary>Quarter.</summary>
    public Unit qtr { get; }
    /// <summary>Hundredweight.</summary>
    public Unit cwt { get; }
    /// <summary>Ton.</summary>
    public Unit t { get; }

    //other
    /// <summary>Second (reuses SI).</summary>
    public Unit s => Unit.SI.s;
    /// <summary>Ampere (reuses SI).</summary>
    public Unit A => Unit.SI.A;
    /// <summary>Kelvin (reuses SI).</summary>
    public Unit K => Unit.SI.K;
    /// <summary>Candela (reuses SI).</summary>
    public Unit cd => Unit.SI.cd;
    /// <summary>Mole (reuses SI).</summary>
    public Unit n => Unit.SI.n;

    /// <summary>
    /// Initializes the imperial units and their scale relationships. All units remain immutable after construction.
    /// </summary>
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