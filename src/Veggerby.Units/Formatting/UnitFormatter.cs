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

    // Priority ordered list of derived vectors for Mixed mode substitution. Each entry stores the exponent vector and symbol.
    // Order enforced according to specification: primary (mechanical/electrical), photometric/radiation, then frequency/angle display tokens.
    private static readonly (ExponentVector Vector, string Symbol)[] _mixedPriority = CreateMixedPriority();

    private readonly struct DerivedSymbolToken(string symbol, int exponent)
    {
        public readonly string Symbol = symbol;
        public readonly int Exponent = exponent;

        public override string ToString() => Exponent == 1 ? Symbol : Symbol + "^" + Exponent.ToString(CultureInfo.InvariantCulture);
    }

    private readonly struct BaseFactorToken(string symbol, int exponent)
    {
        public readonly string Symbol = symbol;
        public readonly int Exponent = exponent;

        public override string ToString() => Exponent == 1 ? Symbol : Symbol + "^" + Exponent.ToString(CultureInfo.InvariantCulture);
    }

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

                    if (format == UnitFormat.Qualified)
                    {
                        if (AmbiguityRegistry.TryGetAmbiguities(sym, out var kinds))
                        {
                            if (kind != null)
                            {
                                return sym + " (" + kind.Name + ")"; // always show explicit semantic intent
                            }
                        }
                        // not ambiguous or no provided kind -> just symbol
                    }

                    return sym;
                }
            case UnitFormat.Mixed:
                return FormatMixed(unit, kind);
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
        var vec = ExponentVector.From(unit.Dimension);
        // QuantityKind-driven torque preference: when caller supplies Torque and dimension is Joule, render N·m.
        if (qualifyWith != null && qualifyWith.Name == "Torque" && EqualsVector(vec, 1, 2, -2))
        {
            return "N·m";
        }

        // Exact symbol short-circuit unless we have a forced decomposition rule (torque for J, always for Wb).
        if (_derived.TryGetValue(vec, out var exactSym))
        {
            if (!ShouldDecomposeInMixed(unit, exactSym))
            {
                if (qualifyWith != null && AmbiguityRegistry.TryGetAmbiguities(exactSym, out _))
                {
                    return exactSym + " (" + qualifyWith.Name + ")";
                }
                return exactSym;
            }
        }

        // Enumerate all subsets of derived candidates to find minimal cost factoring per scoring rules.
        // We allow each symbol at most once (adequate for Mixed readability goals) and bias against J in torque context and Wb always.
        Span<int> target = stackalloc int[7];
        vec.WriteTo(target);
        bool hasAmpere = target[3] != 0;

        var bestMask = 0;
        int bestCost = int.MaxValue;
        Span<int> sum = stackalloc int[7];
        int n = _mixedPriority.Length;
        for (int mask = 0; mask < (1 << n); mask++)
        {
            // quick pruning: if mask includes both J and any other symbol that would exceed target quickly skip? Rely on overshoot detection.
            bool overshoot = false;
            for (int i = 0; i < 7; i++)
            {
                sum[i] = 0;
            }

            int derivedCount = 0;
            bool includesJ = false;
            bool includesN = false;
            bool includesW = false;
            bool containsOmega = false;
            bool containsCoulomb = false;

            for (int i = 0; i < n; i++)
            {
                if (((mask >> i) & 1) == 0)
                {
                    continue;
                }

                var (v, sym) = _mixedPriority[i];
                derivedCount++;

                if (sym == "J")
                {
                    includesJ = true;
                }
                else if (sym == "N")
                {
                    includesN = true;
                }
                else if (sym == "W")
                {
                    includesW = true;
                }
                else if (sym == "Ω")
                {
                    containsOmega = true;
                }
                else if (sym == "C")
                {
                    containsCoulomb = true;
                }

                for (int c = 0; c < 7; c++)
                {
                    int add = v.Get(c);
                    if (add == 0)
                    {
                        continue;
                    }

                    int current = sum[c] + add;
                    int tgt = target[c];
                    // Overshoot rules: allow one-step more negative time exponent for N or W (enables N·s, W·s/A patterns)

                    if ((tgt == 0 && current != 0) || (tgt > 0 && current > tgt) || (tgt < 0 && current < tgt))
                    {
                        bool allowedTimeOvershoot = c == 2 && tgt < 0 && (sym == "N" || sym == "W") && current == tgt - 1;

                        if (!allowedTimeOvershoot)
                        {
                            overshoot = true;
                            break;
                        }
                    }

                    sum[c] = current;
                }

                if (overshoot)
                {
                    break;
                }
            }

            if (overshoot)
            {
                continue;
            }

            // Compute leftover base exponents.
            int baseTokenCount = 0;
            bool hasMetreLeftover = false;
            bool hasSecondLeftover = false;
            bool hasAmpereLeftoverNeg = false;

            for (int c = 0; c < 7; c++)
            {
                int leftover = target[c] - sum[c];

                if (leftover != 0)
                {
                    baseTokenCount++;
                    if (c == 0 && leftover > 0)
                    {
                        hasMetreLeftover = true;
                    }

                    if (c == 2 && leftover != 0)
                    {
                        hasSecondLeftover = true;
                    }

                    if (c == 3 && leftover < 0)
                    {
                        hasAmpereLeftoverNeg = true;
                    }
                }
            }

            int cost = derivedCount + baseTokenCount;

            // Bias rules:
            // 1. Always penalize Wb to force decomposition: large penalty ensures alternate factoring wins when feasible.
            if (IncludesSymbol(mask, "Wb"))
            {
                cost += 5;
            }

            // 2. (Removed torque heuristic penalty; handled by QuantityKind preference.)
            // 3. Mild bonus (negative penalty) for using W when Wb dimension targeted (helps W·s/A factoring).
            if (IncludesSymbol(mask, "W"))
            {
                cost -= 1;
            }

            // 4. If Ampere present favor W over J (power context). Penalize J alone when A exponent non-zero and W feasible.
            if (hasAmpere && includesJ && !includesW) { cost += 4; }

            // 5. Encourage N when a leftover metre exists to enable N·m torque readability.
            if (hasMetreLeftover && !includesN && !includesJ)
            {
                cost += 2; // selecting J would hide torque, so non-N sets pay a small cost
            }

            // 6. Encourage N when a leftover second exists and N present to form N·s pattern (reward combination N plus leftover s).
            if (hasSecondLeftover && includesN)
            {
                cost -= 1;
            }

            // 7. Discourage J if second leftover exists (prefer N·s when possible).
            if (hasSecondLeftover && includesJ)
            {
                cost += 2;
            }

            // 8. Discourage C·Ω combo which equals V·s/A; prefer W path if available
            if (containsCoulomb && containsOmega)
            {
                cost += 2;
            }

            // 9. (Removed torque hard block; handled by QuantityKind preference.)
            // 10. Hard block: if Ampere present and W achievable but mask uses J instead -> penalize heavily.
            if (hasAmpere && includesJ && includesW)
            {
                cost += 50;
            }

            // 11. Bonus for W when it enables W·s/A pattern (leftover +s and -A)
            if (includesW && hasSecondLeftover && hasAmpereLeftoverNeg)
            {
                cost -= 3;
            }

            // 12. Encourage N when time overshoot path used (second leftover) to realize N·s
            if (includesN && hasSecondLeftover)
            {
                cost -= 2;
            }

            if (cost < bestCost)
            {
                bestCost = cost;
                bestMask = mask;
            }
        }

        // Build tokens from bestMask and leftover base factors.
        var derivedTokens = new List<DerivedSymbolToken>();
        Span<int> used = stackalloc int[7];
        for (int i = 0; i < 7; i++)
        {
            used[i] = 0;
        }

        for (int i = 0; i < n; i++)
        {
            if (((bestMask >> i) & 1) == 0)
            {
                continue;
            }

            var (v, sym) = _mixedPriority[i];
            derivedTokens.Add(new DerivedSymbolToken(sym, 1));

            for (int c = 0; c < 7; c++)
            {
                int add = v.Get(c);
                if (add != 0)
                {
                    used[c] += add;
                }
            }
        }

        // Base leftovers
        var baseTokens = new List<BaseFactorToken>();
        ReadOnlySpan<string> baseSymbols = BaseFactorSymbols;
        for (int c = 0; c < 7; c++)
        {
            int leftover = target[c] - used[c];
            if (leftover != 0)
            {
                baseTokens.Add(new BaseFactorToken(baseSymbols[c], leftover));
            }
        }

        // Numerator / denominator assembly
        var partsNumerator = new List<string>(derivedTokens.Count + baseTokens.Count);
        var partsDenominator = new List<string>();
        // Keep priority order for derived tokens for determinism.
        foreach (var t in derivedTokens)
        {
            if (t.Exponent > 0)
            {
                partsNumerator.Add(RenderToken(t.Symbol, t.Exponent));
            }
            else if (t.Exponent < 0)
            {
                partsDenominator.Add(RenderToken(t.Symbol, -t.Exponent));
            }
        }

        foreach (var t in baseTokens)
        {
            if (t.Exponent > 0)
            {
                partsNumerator.Add(RenderToken(t.Symbol, t.Exponent));
            }
            else
            {
                partsDenominator.Add(RenderToken(t.Symbol, -t.Exponent));
            }
        }

        if (partsNumerator.Count == 0 && partsDenominator.Count == 0)
        {
            return string.Empty;
        }

        var core = string.Join("·", partsNumerator);
        if (partsDenominator.Count > 0)
        {
            core += "/" + string.Join("·", partsDenominator);
        }

        if (qualifyWith != null && derivedTokens.Count > 0 && ContainsAmbiguousDerived(derivedTokens))
        {
            core += " (" + qualifyWith.Name + ")";
        }

        return core;
    }

    private static void SeedExplicitDerivedOperands(Unit unit, Span<int> remaining, List<DerivedSymbolToken> tokens)
    {
        if (unit is not IProductOperation prod)
        {
            return;
        }

        // Build quick lookup from vector -> symbol for derived tokens we care about (exclude J here so we can allow N·m decomposition for torque pattern)
        var vectorToSymbol = _mixedPriority.Where(p => p.Symbol != "J").ToDictionary(p => p.Vector, p => p.Symbol);

        foreach (var op in prod.Operands)
        {
            if (op is Unit u)
            {
                var v = ExponentVector.From(u.Dimension);
                if (vectorToSymbol.TryGetValue(v, out var sym))
                {
                    // Ensure we can subtract
                    if (CanFormSymbol(remaining, v))
                    {
                        SubtractVector(remaining, v);
                        tokens.Add(new DerivedSymbolToken(sym, 1));
                    }
                }
            }
        }
        // Special case: If remaining forms J exactly but we already have an explicit N token plus leftover m, prefer N·m (leave J decomposition implicit).
    }

    private static bool CanFormSymbol(Span<int> remaining, ExponentVector candidate)
    {
        for (int c = 0; c < 7; c++)
        {
            int exp = candidate.Get(c);

            if (exp == 0)
            {
                continue;
            }

            int rem = remaining[c];
            if (rem == 0 || (rem > 0 && exp < 0) || (rem < 0 && exp > 0) || Math.Abs(rem) < Math.Abs(exp))
            {
                return false;
            }
        }

        return true;
    }

    private static void SubtractVector(Span<int> remaining, ExponentVector candidate)
    {
        for (int c = 0; c < 7; c++)
        {
            int exp = candidate.Get(c);
            if (exp != 0)
            {
                remaining[c] -= exp;
            }
        }
    }

    private static bool ContainsAmbiguousDerived(List<DerivedSymbolToken> tokens)
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            if (AmbiguityRegistry.TryGetAmbiguities(tokens[i].Symbol, out _))
            {
                return true;
            }
        }

        return false;
    }

    private static string RenderToken(string symbol, int exponent)
    {
        if (exponent == 1)
        {
            return symbol;
        }

        return symbol + "^" + exponent.ToString(CultureInfo.InvariantCulture);
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

    // Legacy heuristic removed; ambiguity now driven by AmbiguityRegistry.

    private static (ExponentVector, string)[] CreateMixedPriority()
    {
        // Order matches specification groups; we rely on dictionary map using same exponent construction.
        var list = new List<(ExponentVector, string)>
        {
            (new ExponentVector(1, 2, -2, 0, 0, 0, 0), "J"),
            (new ExponentVector(1, 1, -2, 0, 0, 0, 0), "N"),
            (new ExponentVector(1, -1, -2, 0, 0, 0, 0), "Pa"),
            (new ExponentVector(1, 2, -3, 0, 0, 0, 0), "W"),
            (new ExponentVector(0, 0, 1, 1, 0, 0, 0), "C"),
            (new ExponentVector(1, 2, -3, -1, 0, 0, 0), "V"),
            (new ExponentVector(1, 2, -3, -2, 0, 0, 0), "Ω"),
            (new ExponentVector(-1, -2, 3, 2, 0, 0, 0), "S"),
            (new ExponentVector(-1, -2, 4, 2, 0, 0, 0), "F"),
            (new ExponentVector(1, 2, -2, -2, 0, 0, 0), "H"),
            (new ExponentVector(1, 0, -2, -1, 0, 0, 0), "T"),
            (new ExponentVector(1, 2, -2, -1, 0, 0, 0), "Wb"),
            (new ExponentVector(0, 1, -2, 0, 0, 0, 1), "lm"),
            (new ExponentVector(0, -2, 0, 0, 0, 0, 1), "lx"),
            // Radiation / chemistry tokens Gy, Sv, kat not yet defined in dimension system (placeholders if later added)
        };

        return list.ToArray();
    }

    private static bool ShouldDecomposeInMixed(Unit unit, string symbol)
    {
        return symbol switch
        {
            "Wb" => true,
            _ => false,
        }; // J handled via kind preference
    }

    private static bool IncludesSymbol(int mask, string symbol)
    {
        for (int i = 0; i < _mixedPriority.Length; i++)
        {
            if (((mask >> i) & 1) != 0 && _mixedPriority[i].Symbol == symbol)
            {
                return true;
            }
        }

        return false;
    }


    // Component-wise exponent comparison helper (kg, m, s, A, K, mol, cd). Unused optional parameters default to 0.
    private static bool EqualsVector(ExponentVector v, int kg, int m, int s, int a = 0, int k = 0, int mol = 0, int cd = 0)
    {
        return v.Get(1) == kg && // kg index
               v.Get(0) == m &&  // m index
               v.Get(2) == s &&  // s index
               v.Get(3) == a &&
               v.Get(4) == k &&
               v.Get(5) == mol &&
               v.Get(6) == cd;
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

    private static ReadOnlySpan<string> BaseFactorSymbols => new[] { "m", "kg", "s", "A", "K", "mol", "cd" };

    /// <summary>Immutable exponent vector used as dictionary key.</summary>
    private readonly struct ExponentVector(int kg, int m, int s, int a, int k, int mol, int cd) : IEquatable<ExponentVector>
    {
        private readonly sbyte _kg = (sbyte)kg;
        private readonly sbyte _m = (sbyte)m;
        private readonly sbyte _s = (sbyte)s;
        private readonly sbyte _a = (sbyte)a;
        private readonly sbyte _k = (sbyte)k;
        private readonly sbyte _mol = (sbyte)mol;
        private readonly sbyte _cd = (sbyte)cd;

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

        public void WriteTo(Span<int> target)
        {
            target[0] = _m; // order aligned with BaseFactorSymbols mapping
            target[1] = _kg;
            target[2] = _s;
            target[3] = _a;
            target[4] = _k;
            target[5] = _mol;
            target[6] = _cd;
        }

        public int Get(int index)
        {
            return index switch
            {
                0 => _m,
                1 => _kg,
                2 => _s,
                3 => _a,
                4 => _k,
                5 => _mol,
                6 => _cd,
                _ => 0,
            };
        }
    }
}