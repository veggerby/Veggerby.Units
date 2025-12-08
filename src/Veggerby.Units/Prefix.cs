using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units;

/// <summary>
/// Represents a decimal prefix (SI style) that scales a base unit by a power of ten. Equality and hash code are based
/// solely on the numeric factor to ensure structural equivalence irrespective of symbol or name uniqueness.
/// </summary>
/// <remarks>
/// Metric prefix definitions align with QUDT ontology's <c>qudt:DecimalPrefixType</c> specifications.
/// All prefix factors match QUDT canonical values exactly (e.g., kilo = 10³, milli = 10⁻³).
/// Binary prefixes (kibi, mebi, etc.) are intentionally excluded as they are not part of SI/QUDT.
/// <para>
/// QUDT Prefix Vocabulary: http://qudt.org/vocab/prefix/
/// </para>
/// <para>
/// See <c>docs/qudt-alignment.md</c> for prefix system validation.
/// </para>
/// </remarks>
public class Prefix
{
    /// <summary>Identity prefix (factor = 1).</summary>
    public static Prefix Empty = new(string.Empty, string.Empty, 1);

    /// <summary>deca (1E1)</summary>
    public static Prefix da = new("deca", "da", 1E1);
    /// <summary>hecto (1E2)</summary>
    public static Prefix h = new("hecto", "h", 1E2);
    /// <summary>kilo (1E3)</summary>
    public static Prefix k = new("kilo", "k", 1E3);
    /// <summary>mega (1E6)</summary>
    public static Prefix M = new("mega", "M", 1E6);
    /// <summary>giga (1E9)</summary>
    public static Prefix G = new("giga", "G", 1E9);
    /// <summary>tera (1E12)</summary>
    public static Prefix T = new("tera", "T", 1E12);
    /// <summary>peta (1E15)</summary>
    public static Prefix P = new("peta", "P", 1E15);
    /// <summary>exa (1E18)</summary>
    public static Prefix E = new("exa", "E", 1E18);
    /// <summary>zetta (1E21)</summary>
    public static Prefix Z = new("zetta", "Z", 1E21);
    /// <summary>yotta (1E24)</summary>
    public static Prefix Y = new("yotta", "Y", 1E24);

    /// <summary>deci (1E-1)</summary>
    public static Prefix d = new("deci", "d", 1E-1);
    /// <summary>centi (1E-2)</summary>
    public static Prefix c = new("centi", "c", 1E-2);
    /// <summary>milli (1E-3)</summary>
    public static Prefix m = new("milli", "m", 1E-3);
    /// <summary>micro (1E-6)</summary>
    public static Prefix μ = new("micro", "μ", 1E-6);
    /// <summary>nano (1E-9)</summary>
    public static Prefix n = new("nano", "n", 1E-9);
    /// <summary>pico (1E-12)</summary>
    public static Prefix p = new("pico", "p", 1E-12);
    /// <summary>femto (1E-15)</summary>
    public static Prefix f = new("femto", "f", 1E-15);
    /// <summary>atto (1E-18)</summary>
    public static Prefix a = new("atto", "a", 1E-18);
    /// <summary>zepto (1E-21)</summary>
    public static Prefix z = new("zepto", "z", 1E-21);
    /// <summary>yocto (1E-24)</summary>
    public static Prefix y = new("yocto", "y", 1E-24);

    /// <summary>Enumerates prefixes from smallest to largest including the identity prefix.</summary>
    public static IEnumerable<Prefix> All
    {
        get
        {
            yield return y;
            yield return z;
            yield return a;
            yield return f;
            yield return p;
            yield return n;
            yield return μ;
            yield return m;
            yield return c;
            yield return d;
            yield return Empty;
            yield return da;
            yield return h;
            yield return k;
            yield return M;
            yield return G;
            yield return T;
            yield return P;
            yield return E;
            yield return Z;
            yield return Y;
        }
    }

    private Prefix(string name, string symbol, double factor)
    {
        Name = name;
        Symbol = symbol;
        Factor = factor;
    }

    /// <summary>Full textual name (e.g. kilo).</summary>
    public string Name { get; }
    /// <summary>Symbol representation (e.g. k).</summary>
    public string Symbol { get; }
    /// <summary>Decimal multiplication factor (e.g. 1E3 for kilo).</summary>
    public double Factor { get; }

    /// <summary>Implicit conversion to the underlying numeric factor.</summary>
    public static implicit operator double(Prefix p)
    {
        return p.Factor;
    }

    /// <summary>
    /// Resolves a prefix instance by exact factor match. Returns null when no prefix matches (caller validates).
    /// </summary>
    public static implicit operator Prefix(double value)
    {
        return
            All
            .SingleOrDefault(x => x.Factor == value);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is Prefix)
        {
            return Factor == (obj as Prefix).Factor;
        }

        return base.Equals(obj);
    }

    /// <inheritdoc />
    public override int GetHashCode() => Factor.GetHashCode();
}