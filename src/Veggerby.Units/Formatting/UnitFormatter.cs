using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Veggerby.Units.Dimensions;
using Veggerby.Units.Quantities;
using Veggerby.Units.Reduction;

namespace Veggerby.Units.Formatting;

/// <summary>
/// Formats <see cref="Unit"/> and measurement instances into human friendly strings with optional derived
/// SI symbol substitution and quantity kind qualification.
/// </summary>
public static class UnitFormatter
{
    /// <summary>
    /// Immutable mapping of fully reduced base exponent vectors to preferred derived symbols.
    /// Vector order: (kg, m, s, A, K, mol, cd).
    /// </summary>
    private static readonly IReadOnlyDictionary<ExponentVector, string> _derived = CreateDerivedMap();

    /// <summary>Formats the supplied <paramref name="unit"/> per <paramref name="format"/>.</summary>
    public static string Format(Unit unit, UnitFormat format, QuantityKind kind = null, bool strict = false)
    {
        if (unit is null)
        {
            return string.Empty;
        }

        switch (format)
        {
            case UnitFormat.BaseFactors:
                return unit.Symbol;
            case UnitFormat.DerivedSymbols:
            case UnitFormat.Qualified:
                {
                    var sym = TryGetDerivedSymbol(unit);
                    if (sym is null)
                    {
                        if (strict)
                        {
                            // strict derived request but unknown => fall back to mixed behaviour
                            return FormatMixed(unit, format == UnitFormat.Qualified ? kind : null);
                        }

                        return unit.Symbol; // treat as base factor expression (no partial substitutions)
                    }

                    if (format == UnitFormat.Qualified && IsAmbiguous(sym))
                    {
                        var suffix = kind is null ? string.Empty : $" ({kind.Name})";
                        return sym + suffix;
                    }

                    return sym;
                }
            case UnitFormat.Mixed:
                return FormatMixed(unit, null);
            default:
                return unit.Symbol;
        }
    }

    /// <summary>Formats a numeric value and unit.</summary>
    public static string Format<T>(Measurement<T> measurement, UnitFormat format, QuantityKind kind = null, bool strict = false) where T : IComparable
    {
        return string.Create(CultureInfo.InvariantCulture, $"{measurement.Value} {Format(measurement.Unit, format, kind, strict)}");
    }

    private static string FormatMixed(Unit unit, QuantityKind qualifyWith)
    {
        // For now Mixed is identical to base factors until sub-expression substitution is implemented.
        // This keeps the surface minimal; future enhancement could greedily factor and replace.
        if (qualifyWith != null)
        {
            return unit.Symbol + $" ({qualifyWith.Name})";
        }

        return unit.Symbol;
    }

    private static string TryGetDerivedSymbol(Unit unit)
    {
        if (unit == Unit.None)
        {
            return string.Empty;
        }

        var vec = ExponentVector.From(unit.Dimension);
        if (_derived.TryGetValue(vec, out var symbol))
        {
            return symbol;
        }

        return null;
    }

    private static bool IsAmbiguous(string symbol)
    {
        // Current simple heuristic: known ambiguous base symbol set. Could be extended via governance reflection.
        return symbol switch
        {
            "J" => true, // Joule vs N*m (Energy vs Torque) – same dimension, different KOQ
            _ => false,
        };
    }

    private static IReadOnlyDictionary<ExponentVector, string> CreateDerivedMap()
    {
        // Populate with common SI derived units. Exponent vector order: kg, m, s, A, K, mol, cd
        var dict = new Dictionary<ExponentVector, string>
        {
            [new ExponentVector(1, 1, -2, 0, 0, 0, 0)] = "N",   // Newton kg·m/s^2
            [new ExponentVector(1, 2, -2, 0, 0, 0, 0)] = "J",   // Joule kg·m^2/s^2
            [new ExponentVector(1, -1, -2, 0, 0, 0, 0)] = "Pa",  // Pascal kg/(m·s^2)
            [new ExponentVector(1, 2, -3, 0, 0, 0, 0)] = "W",   // Watt kg·m^2/s^3
            [new ExponentVector(0, 0, 1, 1, 0, 0, 0)] = "C",    // Coulomb s·A
            [new ExponentVector(1, 2, -3, -1, 0, 0, 0)] = "V",  // Volt kg·m^2/(s^3·A)
            [new ExponentVector(1, 2, -3, -2, 0, 0, 0)] = "Ω",  // Ohm kg·m^2/(s^3·A^2)
            [new ExponentVector(-1, -2, 3, 2, 0, 0, 0)] = "S",  // Siemens s^3·A^2/(kg·m^2)
            [new ExponentVector(-1, -2, 4, 2, 0, 0, 0)] = "F",  // Farad s^4·A^2/(kg·m^2)
            [new ExponentVector(1, 2, -2, -2, 0, 0, 0)] = "H",  // Henry kg·m^2/(s^2·A^2)
            [new ExponentVector(1, 0, -2, -1, 0, 0, 0)] = "T",  // Tesla kg/(s^2·A)
            [new ExponentVector(1, 2, -2, -1, 0, 0, 0)] = "Wb", // Weber kg·m^2/(s^2·A)
            [new ExponentVector(0, 1, -2, 0, 0, 0, 1)] = "lm",  // Lumen (simplified mapping)
            [new ExponentVector(0, -2, 0, 0, 0, 0, 1)] = "lx",  // Lux (lm/m^2) simplified
            [new ExponentVector(0, 0, -1, 0, 0, 0, 0)] = "Hz",  // Hertz 1/s
            [new ExponentVector(0, 0, 1, 0, 0, 0, 0)] = "s",   // Second
            [new ExponentVector(0, 1, 0, 0, 0, 0, 0)] = "m",   // Metre
            [new ExponentVector(1, 0, 0, 0, 0, 0, 0)] = "kg",  // Kilogram
            [new ExponentVector(0, 0, 0, 1, 0, 0, 0)] = "A",   // Ampere
            [new ExponentVector(0, 0, 0, 0, 1, 0, 0)] = "K",   // Kelvin
            [new ExponentVector(0, 0, 0, 0, 0, 1, 0)] = "mol", // Mole
            [new ExponentVector(0, 0, 0, 0, 0, 0, 1)] = "cd",  // Candela
            [new ExponentVector(0, 0, 0, 0, 0, 0, 0)] = string.Empty // dimensionless
        };

        return dict;
    }

    /// <summary>Immutable exponent vector used as dictionary key.</summary>
    private readonly struct ExponentVector : IEquatable<ExponentVector>
    {
        private readonly sbyte _kg;
        private readonly sbyte _m;
        private readonly sbyte _s;
        private readonly sbyte _a;
        private readonly sbyte _k;
        private readonly sbyte _mol;
        private readonly sbyte _cd;

        public ExponentVector(int kg, int m, int s, int a, int k, int mol, int cd)
        {
            _kg = (sbyte)kg; _m = (sbyte)m; _s = (sbyte)s; _a = (sbyte)a; _k = (sbyte)k; _mol = (sbyte)mol; _cd = (sbyte)cd;
        }

        public static ExponentVector From(Dimension d)
        {
            Span<int> acc = stackalloc int[7];
            Aggregate(d, 1, acc);
            return new ExponentVector(acc[1], acc[0], acc[2], acc[3], acc[4], acc[5], acc[6]);
        }

        private static void Aggregate(Dimension d, int factor, Span<int> acc)
        {
            if (d is BasicDimension)
            {
                AssignBase(d.Symbol, factor, acc);
                return;
            }

            if (d is IProductOperation prod)
            {
                foreach (var o in prod.Operands)
                {
                    Aggregate((Dimension)o, factor, acc);
                }
                return;
            }

            if (d is IDivisionOperation div)
            {
                Aggregate((Dimension)div.Dividend, factor, acc);
                Aggregate((Dimension)div.Divisor, -factor, acc);
                return;
            }

            if (d is IPowerOperation pow)
            {
                Aggregate((Dimension)pow.Base, factor * pow.Exponent, acc);
                return;
            }

            // NullDimension or unknown => no contribution
        }

        private static void AssignBase(string symbol, int factor, Span<int> acc)
        {
            switch (symbol)
            {
                case "L": acc[0] += factor; break;
                case "M": acc[1] += factor; break;
                case "T": acc[2] += factor; break;
                case "I": acc[3] += factor; break;
                case "Θ": acc[4] += factor; break;
                case "N": acc[5] += factor; break;
                case "J": acc[6] += factor; break;
                default: break;
            }
        }

        public bool Equals(ExponentVector other)
        {
            return _kg == other._kg && _m == other._m && _s == other._s && _a == other._a && _k == other._k && _mol == other._mol && _cd == other._cd;
        }

        public override bool Equals(object obj) => obj is ExponentVector other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 31) + _kg.GetHashCode();
                hash = (hash * 31) + _m.GetHashCode();
                hash = (hash * 31) + _s.GetHashCode();
                hash = (hash * 31) + _a.GetHashCode();
                hash = (hash * 31) + _k.GetHashCode();
                hash = (hash * 31) + _mol.GetHashCode();
                hash = (hash * 31) + _cd.GetHashCode();
                return hash;
            }
        }
    }
}