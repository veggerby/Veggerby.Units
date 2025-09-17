namespace Veggerby.Units;

/// <summary>
/// Imperial unit system subset used for inter‑system conversions (length, area, volume, weight). Where possible the
/// underlying scale factors are expressed relative to SI base units to allow seamless composition with SI units.
/// </summary>
public sealed class ImperialUnitSystem : UnitSystem
{
    /// <summary>Scale factor: 1 ft = 0.3048 m.</summary>
    public const double FeetToMetres = 0.3048;
    /// <summary>Scale factor: 1 lb = 0.45359237 kg.</summary>
    public const double PoundToKilogram = 0.45359237;
    /// <summary>Scale factor used for volume construction (cubic inches to pint).</summary>
    public const double CubicInchToPint = 34.677;
    /// <summary>Scale factor: 1 lbf = 0.45359237 kg * 9.80665 m/s^2 ≈ 4.44822161526037 N.</summary>
    public const double PoundForceToNewton = PoundToKilogram * 9.80665;

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

    // nautical length
    /// <summary>Fathom (6 ft).</summary>
    public Unit fathom { get; }
    /// <summary>Cable (100 fathoms).</summary>
    public Unit cable { get; }
    /// <summary>Nautical mile (1852 m exact) – uses scale over metre.</summary>
    public Unit nmi { get; }

    // agricultural / commodity volume
    /// <summary>Peck.</summary>
    public Unit peck { get; }
    /// <summary>Bushel.</summary>
    public Unit bushel { get; }
    /// <summary>Barrel (oil barrel 42 gal US approximation using gallon chain).</summary>
    public Unit barrel { get; }
    /// <summary>Tun (252 gal – 6 barrels).</summary>
    public Unit tun { get; }

    // specialized weight (troy/apothecaries simplified)
    /// <summary>Troy ounce (31.1034768 g).</summary>
    public Unit ozt { get; }
    /// <summary>Troy pound (12 troy ounces).</summary>
    public Unit lbt { get; }

    // engineering derived
    /// <summary>Pound-force (defined via exact product lb * g0; represented as scale to SI newton).</summary>
    public Unit lbf { get; }
    /// <summary>Foot-pound (pound-force * foot).</summary>
    public Unit ft_lb { get; }
    /// <summary>Pound per square inch (psi).</summary>
    public Unit psi { get; }
    /// <summary>Horsepower (mechanical) 550 ft·lb/s.</summary>
    public Unit hp { get; }

    // area convenience
    /// <summary>Square inch.</summary>
    public Unit sq_in { get; }
    /// <summary>Square foot.</summary>
    public Unit sq_ft { get; }
    /// <summary>Square yard.</summary>
    public Unit sq_yd { get; }
    /// <summary>Square mile.</summary>
    public Unit sq_mi { get; }

    //other
    /// <summary>Second (reuses SI).</summary>
    public Unit s => Unit.SI.s;
    /// <summary>Ampere (reuses SI).</summary>
    public Unit A => Unit.SI.A;
    /// <summary>Kelvin (reuses SI).</summary>
    public Unit K => Unit.SI.K;
    /// <summary>Fahrenheit – affine temperature unit (°F) referenced to Kelvin.</summary>
    public Unit F { get; }
    /// <summary>Candela (reuses SI).</summary>
    public Unit cd => Unit.SI.cd;
    /// <summary>Mole (reuses SI).</summary>
    public Unit n => Unit.SI.n;

    /// <summary>
    /// Initializes the imperial units and their scale relationships. All units remain immutable after construction.
    /// </summary>
    internal ImperialUnitSystem()
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

        // nautical
        // Fathom is a pure scalar multiple of the foot (6 ft) so represent as a scale unit
        fathom = new ScaleUnit("fathom", "fathom", 6, ft);
        cable = new ScaleUnit("cable", "cable", 100, fathom);
        nmi = new ScaleUnit("nmi", "nautical mile", 1852d / FeetToMetres, ft); // exact 1852 m

        // agricultural / commodity volume (using gallon chain -> qt -> pt -> cubic inches)
        // Peck = 2 gallons (US dry ~ 8 dry quarts) – approximate via existing gallon chain
        peck = new ScaleUnit("pk", "peck", 2, gal);
        bushel = new ScaleUnit("bu", "bushel", 4, peck);
        barrel = new ScaleUnit("bbl", "barrel", 42, gal);
        tun = new ScaleUnit("tun", "tun", 6, barrel);

        // troy weights (exact factors relative to gram via SI kilogram)
        ozt = new ScaleUnit("ozt", "troy ounce", 31.1034768 / 1000d, Unit.SI.kg, this);
        lbt = new ScaleUnit("lbt", "troy pound", 12, ozt);

        // engineering derived
        // Standard gravity constant
        lbf = new ScaleUnit("lbf", "pound-force", PoundForceToNewton, Unit.SI.kg * (Unit.SI.m / (Unit.SI.s ^ 2)), this);
        ft_lb = new DerivedUnit("ft·lb", "foot-pound", lbf * ft);
        psi = new DerivedUnit("psi", "pound per square inch", lbf / (@in ^ 2));
        hp = new ScaleUnit("hp", "horsepower", 550, ft_lb / Unit.SI.s);

        // area convenience
        sq_in = @in ^ 2;
        sq_ft = ft ^ 2;
        sq_yd = ya ^ 2;
        sq_mi = mi ^ 2;

        // temperature (affine) – defined relative to absolute Kelvin base. K = (F * 5/9) + 255.3722222222222
        F = new AffineUnit("°F", "fahrenheit", K, 5d / 9d, 255.3722222222222d);
    }
}