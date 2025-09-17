using System;
using System.Collections.Generic;
using System.Linq;

namespace Veggerby.Units.Reduction;

/// <summary>
/// Core internal algorithms for structural normalisation of unit and dimension expressions. Responsibilities:
/// <list type="bullet">
/// <item><description>Reassociation so that division becomes outermost (enabling simpler cancellation rules)</description></item>
/// <item><description>Factor cancellation across products and divisions</description></item>
/// <item><description>Aggregation of repeated factors into power expressions</description></item>
/// <item><description>Distribution / consolidation of powers (e.g. (A*B)^n => A^n*B^n)</description></item>
/// <item><description>Structural equality checks decoupled from public API equality to avoid recursion</description></item>
/// </list>
/// The utility operates purely on the marker / operation interfaces so it can be shared between Units and Dimensions.
/// </summary>
internal static class OperationUtility
{
    /// <summary>
    /// Structural equality comparison for two operands. When <see cref="ReductionSettings.EqualityNormalizationEnabled"/>
    /// is true (default) all algebraic nodes (product / division / power) are first normalised into a canonical
    /// factor multiset (base -> exponent) and the multisets are compared. This removes ordering dependencies and
    /// lazy (Product)^n distribution differences. Non–algebraic leaves fall back to raw structural comparison.
    /// </summary>
    internal static bool Equals(IOperand o1, IOperand o2)
    {
        if (ReferenceEquals(o1, o2))
        {
            return true;
        }

        if (o1 == null || o2 == null)
        {
            return false;
        }

        // Canonical factor multiset path (Steps 1 + 2)
        if (ReductionSettings.EqualityNormalizationEnabled && (IsAlgebraic(o1) || IsAlgebraic(o2)))
        {
            if (TryCanonicalFactorCompare(o1, o2, out var result))
            {
#if DEBUG
                if (!result)
                {
                    try
                    {
                        var f1 = TryGetCanonicalFactorsForDiagnostics(o1);
                        var f2 = TryGetCanonicalFactorsForDiagnostics(o2);
                        System.Diagnostics.Debug.WriteLine($"[Units.EQ] canonical-mismatch | L=({string.Join(' ', f1.Select(f => f.Symbol + "^" + f.Exponent))}) R=({string.Join(' ', f2.Select(f => f.Symbol + "^" + f.Exponent))})");
                    }
                    catch { /* diagnostics must never throw */ }
                }
#endif
                if (!result)
                {
                    // Defensive fallback: recompute ordered (symbol, exponent) canonical sequences and compare directly.
                    // This guards against any rare key identity edge case in the primary dictionary-based path.
                    var a1 = TryGetCanonicalFactorsForDiagnostics(o1);
                    var a2 = TryGetCanonicalFactorsForDiagnostics(o2);

                    if (a1.Length == a2.Length)
                    {
                        bool all = true;
                        for (int i = 0; i < a1.Length; i++)
                        {
                            if (!string.Equals(a1[i].Symbol, a2[i].Symbol, System.StringComparison.Ordinal) || a1[i].Exponent != a2[i].Exponent)
                            {
                                all = false; break;
                            }
                        }

                        if (all)
                        {
                            return true;
                        }
                    }
                }

                return result;
            }
        }

        // Legacy lazy power canonicalisation (kept as safety net for disabled normalisation flag)
        if (ReductionSettings.LazyPowerExpansion && !ReductionSettings.EqualityNormalizationEnabled)
        {
            var c1 = CanonicalizePowerProduct(o1);
            var c2 = CanonicalizePowerProduct(o2);

            if (!ReferenceEquals(c1, o1) || !ReferenceEquals(c2, o2))
            {
                return RawEquals(c1, c2);
            }
        }

        return RawEquals(o1, o2);
    }

    /// <summary>For test diagnostics: returns a stable ordered list of canonical factors (symbol + exponent).</summary>
    internal static (string Symbol, int Exponent)[] TryGetCanonicalFactorsForDiagnostics(IOperand operand)
    {
        var map = new Dictionary<IOperand, int>();
        AccumulateFactors(map, operand, 1);

        // remove zeros
        var filtered = map.Where(kv => kv.Value != 0)
            .Select(kv => (kv.Key.ToString(), kv.Value))
            .OrderBy(x => x.Item1, StringComparer.Ordinal)
            .ThenBy(x => x.Value)
            .ToArray();

        return filtered;
    }

    private static bool IsAlgebraic(IOperand op) => op is IProductOperation || op is IDivisionOperation || op is IPowerOperation;

    /// <summary>
    /// Attempts canonical factor multiset comparison. Returns false if unable to build canonical form
    /// (should not happen with current operand graph) so caller can fall back.
    /// </summary>
    private static bool TryCanonicalFactorCompare(IOperand o1, IOperand o2, out bool result)
    {
        // Pre-normalize to proactively distribute powers over products for deterministic factor accumulation.
        var n1 = PreNormalize(o1);
        var n2 = PreNormalize(o2);

        var map1 = new Dictionary<IOperand, int>();
        var map2 = new Dictionary<IOperand, int>();
        AccumulateFactors(map1, n1, 1);
        AccumulateFactors(map2, n2, 1);

        // Fast path: identical dictionary reference equality already handled earlier; now compare counts
        if (map1.Count != map2.Count)
        {
            result = false; return true;
        }

        // For each base in map1 attempt match in map2 with same exponent (structural leaf equality)
        foreach (var kv in map1)
        {
            var exp = kv.Value;
            if (exp == 0)
            {
                continue;
            }

            if (map2.TryGetValue(kv.Key, out var exp2))
            {
                if (exp2 != exp) { result = false; return true; }
                continue;
            }

            // Structural search amongst remaining unmatched keys (rare path when leaf references differ)
            bool matched = false;
            foreach (var other in map2.Keys)
            {
                if (other == kv.Key)
                {
                    continue;
                }

                if (RawEquals(other, kv.Key))
                {
                    if (map2[other] != exp) { result = false; return true; }
                    matched = true; break;
                }
            }
            if (!matched)
            {
                result = false; return true;
            }
        }

        // Ensure exponents in map2 not present in map1 are zero/absent (already covered by count compare)
        result = true;
        return true;
    }

    /// <summary>
    /// Returns an operand where simple (Product)^n shapes are eagerly distributed to a product of powered leaves.
    /// This eliminates a structural asymmetry between lazy and eager forms that could surface as a rare canonical mismatch.
    /// Only applied for exponent > 1 to avoid unnecessary allocation for identity exponents.
    /// </summary>
    private static IOperand PreNormalize(IOperand operand)
    {
        if (operand is IPowerOperation p && p.Exponent > 1 && p.Base is IProductOperation prod)
        {
            // Build distributed product: (a*b*c)^n => a^n * b^n * c^n (preserving leaf order for determinism)
            // If an inner operand is itself a power, combine exponents multiplicatively.
            var factors = new List<Unit>();
            bool allUnits = true;

            foreach (var op in prod.Operands)
            {
                if (op is IPowerOperation innerPow && innerPow.Base is Unit ub)
                {
                    factors.Add((Unit)(ub ^ (innerPow.Exponent * p.Exponent)));
                }
                else if (op is Unit u)
                {
                    factors.Add((Unit)(u ^ p.Exponent));
                }
                else
                {
                    allUnits = false; break; // fallback to original (mixed dimensions or composite types not handled here)
                }
            }

            if (allUnits && factors.Count > 0)
            {
                Unit acc = null;
                foreach (var f in factors)
                {
                    acc = acc == null ? f : acc * f;
                }

                return acc ?? operand;
            }
        }

        return operand;
    }

    /// <summary>
    /// Recursively accumulates factors into <paramref name="map"/> producing a power-only representation.
    /// Division contributes negative exponents, nested powers multiply exponents, products add exponents.
    /// </summary>
    private static void AccumulateFactors(Dictionary<IOperand, int> map, IOperand operand, int multiplier)
    {
        switch (operand)
        {
            case IProductOperation prod:
                foreach (var op in prod.Operands)
                {
                    AccumulateFactors(map, op, multiplier);
                }
                break;

            case IDivisionOperation div:
                AccumulateFactors(map, div.Dividend, multiplier);
                AccumulateFactors(map, div.Divisor, -multiplier);
                break;

            case IPowerOperation pow:
                AccumulateFactors(map, pow.Base, multiplier * pow.Exponent);
                break;

            default:
                map[operand] = !map.TryGetValue(operand, out var current) ? multiplier : current + multiplier;
                break;
        }
    }

    private static bool RawEquals(IOperand o1, IOperand o2)
    {
        if (ReductionSettings.UseFactorVector &&
            o1 is ICanonicalFactorsProvider c1 &&
            o2 is ICanonicalFactorsProvider c2)
        {
            var fv1 = c1.GetCanonicalFactors();
            var fv2 = c2.GetCanonicalFactors();

            if (fv1.HasValue && fv2.HasValue)
            {
                if (ReferenceEquals(fv1.Value.Factors, fv2.Value.Factors))
                {
                    return true;
                }

                var a = fv1.Value.Factors;
                var b = fv2.Value.Factors;

                if (a.Length == b.Length)
                {
                    bool all = true;

                    for (int i = 0; i < a.Length; i++)
                    {
                        if (!Equals(a[i].Base, b[i].Base) || a[i].Exponent != b[i].Exponent)
                        {
                            all = false; break;
                        }
                    }

                    if (all)
                    {
                        return true;
                    }
                }
            }
        }

        if (Equals(o1 as IProductOperation, o2 as IProductOperation) ||
            Equals(o1 as IDivisionOperation, o2 as IDivisionOperation) ||
            Equals(o1 as IPowerOperation, o2 as IPowerOperation) ||
            Equals(o1 as PrefixedUnit, o2 as PrefixedUnit))
        {
            return true;
        }

        if (ReductionSettings.LazyPowerExpansion)
        {
            if (o1 is IPowerOperation p1 && p1.Base is IProductOperation bp1 && o2 is IProductOperation prod2)
            {
                if (EqualsDistributedPower(bp1, p1.Exponent, prod2))
                {
                    return true;
                }

                if (CompareWithForcedDistribution(p1, prod2))
                {
                    return true;
                }
            }
            else if (o2 is IPowerOperation p2 && p2.Base is IProductOperation bp2 && o1 is IProductOperation prod1)
            {
                if (EqualsDistributedPower(bp2, p2.Exponent, prod1))
                {
                    return true;
                }

                if (CompareWithForcedDistribution(p2, prod1))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static IOperand CanonicalizePowerProduct(IOperand operand)
    {
        if (operand is IPowerOperation p && p.Exponent > 1 && p.Base is IProductOperation prod)
        {
            var first = prod.Operands.FirstOrDefault();
            if (first == null)
            {
                return operand;
            }

            if (first is Unit)
            {
                var distributed = prod.Operands
                    .Select(o => (Unit)o)
                    .Select(u => u ^ p.Exponent)
                    .OrderBy(u => u.Symbol)
                    .Aggregate((Unit)null, (acc, next) => acc == null ? next : acc * next);

                return distributed ?? operand;
            }

            if (first is Dimensions.Dimension)
            {
                var distributed = prod.Operands
                    .Select(o => (Dimensions.Dimension)o)
                    .Select(d => d ^ p.Exponent)
                    .OrderBy(d => d.Symbol)
                    .Aggregate((Dimensions.Dimension)null, (acc, next) => acc == null ? next : acc * next);

                return distributed ?? operand;
            }
        }

        return operand;
    }

    private static bool EqualsDistributedPower(IProductOperation productBase, int exponent, IProductOperation distributed)
    {
        // Build multiplicity map for canonical (root) base operands. If the base product already
        // contains inner power operands (e.g. m^2 * s * kg) we treat that as the base factor 'm'
        // with multiplicity 2 so that an outer exponent ((m^2)*s*kg)^5 can match a distributed
        // form containing m^10 (where 10 = 2 * 5).
        var counts = new Dictionary<IOperand, int>();
        foreach (var operand in productBase.Operands)
        {
            if (operand is IPowerOperation innerPow && innerPow.Exponent > 0)
            {
                var root = innerPow.Base;

                counts[root] = !counts.TryGetValue(root, out var c) ? innerPow.Exponent : c + innerPow.Exponent;
            }
            else
            {
                counts[operand] = !counts.TryGetValue(operand, out var c) ? 1 : c + 1;
            }
        }

        // Each distributed operand should be a power with exponent that is a multiple of the original exponent
        foreach (var op in distributed.Operands)
        {
            if (op is not IPowerOperation p)
            {
                return false; // distributed form must be powers
            }

            // Exponent must be positive multiple of original exponent
            if (p.Exponent <= 0 || p.Exponent % exponent != 0)
            {
                return false;
            }

            var factorMultiplicity = p.Exponent / exponent; // how many times this base appeared originally

            // Attempt direct key first (most common case). If no direct hit, attempt structural match
            // across existing keys (covers reference differences for equivalent base operands).
            if (counts.TryGetValue(p.Base, out var existing))
            {
                var remainingDirect = existing - factorMultiplicity;

                if (remainingDirect < 0)
                {
                    return false;
                }

                counts[p.Base] = remainingDirect;
                continue;
            }

            IOperand match = null;
            foreach (var key in counts.Keys.ToList())
            {
                if (counts[key] > 0 && Equals(key, p.Base))
                {
                    match = key;
                    break;
                }
            }

            if (match == null)
            {
                return false; // no structural base match
            }

            var remaining = counts[match] - factorMultiplicity;
            if (remaining < 0)
            {
                return false;
            }

            counts[match] = remaining;
        }

        // All multiplicities must be consumed
        return counts.Values.All(v => v == 0);
    }

    /// <summary>
    /// Compares a lazy power (Product)^n with a candidate distributed product by constructing a deterministic
    /// distributed form (without mutating global feature flags). Only used as a fallback when structural multiplicity
    /// matching fails, ensuring we do not depend on global state flips.
    /// </summary>
    private static bool CompareWithForcedDistribution(IPowerOperation powerOp, IProductOperation otherProduct)
    {
        if (powerOp.Base is not IProductOperation bp || !bp.Operands.Any())
        {
            return false;
        }

        var first = bp.Operands.First();

        if (first is Unit)
        {
            // Build distributed canonical product: sort factor base symbols for determinism
            var distributedUnits = bp.Operands
                .Select(o => (Unit)o)
                .Select(u => u ^ powerOp.Exponent)
                .OrderBy(u => u.Symbol)
                .ToArray();

            var expanded = distributedUnits.Aggregate((Unit)null, (acc, next) => acc == null ? next : acc * next);

            return Equals(expanded as IOperand, otherProduct);
        }

        if (first is Dimensions.Dimension)
        {
            var distributedDims = bp.Operands
                .Select(o => (Dimensions.Dimension)o)
                .Select(d => d ^ powerOp.Exponent)
                .OrderBy(d => d.Symbol)
                .ToArray();
            var expanded = distributedDims.Aggregate((Dimensions.Dimension)null, (acc, next) => acc == null ? next : acc * next);
            return Equals(expanded as IOperand, otherProduct);
        }
        return false;
    }

    private static bool Equals(IProductOperation o1, IProductOperation o2) => o1 != null && o2 != null && o1.Operands.Count() == o2.Operands.Count() && o1.Operands.Zip(o2.Operands, (a, b) => Equals(a, b)).All(x => x);

    private static bool Equals(IDivisionOperation o1, IDivisionOperation o2)
    {
        if (o1 == null || o2 == null)
        {
            return false;
        }

        return Equals(o1.Dividend, o2.Dividend) && Equals(o1.Divisor, o2.Divisor);
    }

    private static bool Equals(IPowerOperation o1, IPowerOperation o2)
    {
        if (o1 == null || o2 == null)
        {
            return false;
        }

        return Equals(o1.Base, o2.Base) && Equals(o1.Exponent, o2.Exponent);
    }

    private static bool Equals(PrefixedUnit o1, PrefixedUnit o2)
    {
        if (o1 == null || o2 == null)
        {
            return false;
        }

        return Equals(o1.BaseUnit, o2.BaseUnit) && Equals(o1.Prefix, o2.Prefix);
    }

    /// <summary>
    /// Reassociates multiplication operands so that any embedded division becomes outermost (A*(B/C) => (A*B)/C).
    /// Simplifies downstream cancellation logic.
    /// </summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="multiply">Multiplication function <u>with</u> reduction/rearrange</param>
    /// <param name="divide">Division function <u>with</u> reduction/rearrange</param>
    /// <param name="operands">The operands to rearrange</param>
    /// <returns>A rearrange operand <b>if</b> any rearranging has occurred, otherwise default(T)</returns>
    internal static T RearrangeMultiplication<T>(Func<IEnumerable<T>, T> multiply, Func<T, T, T> divide, params T[] operands)
        where T : IOperand
    {
        // if A*(B/C) ensure division is outermost => A*B/C

        var divisions = operands.OfType<IDivisionOperation>();
        var rest = operands.Where(x => !(x is IDivisionOperation));

        if (divisions.Any())
        {
            var dividends = divisions.Select(x => (T)x.Dividend).Concat(rest);
            var divisors = divisions.Select(x => (T)x.Divisor);
            return divide(multiply(dividends), multiply(divisors));
        }

        return default;
    }

    /// <summary>
    /// Reassociates nested division so only a single outermost division remains ( (A/B)/(C/D) => (A*D)/(B*C) ).
    /// </summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="multiply">Multiplication function <u>with</u> reduction/rearrange</param>
    /// <param name="divide">Division function <u>with</u> reduction/rearrange</param>
    /// <param name="dividend">The dividend</param>
    /// <param name="divisor">The divisor</param>
    /// <returns>A rearrange operand <b>if</b> any rearranging has occurred, otherwise default(T)</returns>
    /// <remarks>Complexity: O(1) pattern inspection; no enumeration except nested apply.</remarks>
    internal static T RearrangeDivision<T>(Func<T, T, T> multiply, Func<T, T, T> divide, T dividend, T divisor)
        where T : IOperand
    {
        var d1 = dividend as IDivisionOperation;
        var d2 = divisor as IDivisionOperation;

        if (d1 != null && d2 != null) // (A/B) / (C/D) => AD/BC
        {
            return divide(multiply((T)d1.Dividend, (T)d2.Divisor), multiply((T)d1.Divisor, (T)d2.Dividend));
        }

        if (d1 != null) // (A/B) / C => A/BC
        {
            return divide((T)d1.Dividend, multiply((T)d1.Divisor, divisor));
        }

        if (d2 != null)  // A / (B/C) => AC/B
        {
            return divide(multiply(dividend, (T)d2.Divisor), (T)d2.Dividend);
        }

        return default;
    }

    /// <summary>Reduces a division by cancelling common factors ( (A*C)/(A*B) => C/B ).</summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="multiply">Multiplication function <u>with</u> reduction/rearrange</param>
    /// <param name="divide">Division function <u>with</u> reduction/rearrange</param>
    /// <param name="pow">Power function <u>with</u> reduction/rearrange</param>
    /// <param name="dividend">The dividend</param>
    /// <param name="divisor">The divisor</param>
    /// <returns>A reduced operand <b>if</b> any reduction has occurred, otherwise default(T)</returns>
    /// <remarks>Complexity: O(n log n) grouping (n = total flattened factors across dividend &amp; divisor).</remarks>
    internal static T ReduceDivision<T>(Func<IEnumerable<T>, T> multiply, Func<T, T, T> divide, Func<T, int, T> pow, T dividend, T divisor)
        where T : IOperand
    {
        var mapDiv = ExponentMap<T>.Rent();
        try
        {
            var cancellation = Factorization.AccumulateDivision(mapDiv, dividend, divisor);
            if (!cancellation)
            {
                return default;
            }

            var positives = new List<T>();
            var negatives = new List<T>();

            foreach (var kv in Factorization.EnumerateNonZero(mapDiv))
            {
                if (kv.Value > 0)
                {
                    positives.Add(pow(kv.Key, kv.Value));
                }
                else if (kv.Value < 0)
                {
                    negatives.Add(pow(kv.Key, -kv.Value));
                }
            }

            return divide(multiply(positives), multiply(negatives));
        }
        finally
        {
            mapDiv.Return();
        }
    }

    /// <summary>Aggregates repeated multiplicative factors into powers ( (A*A*A*B) => A^3*B ).</summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="multiply">Multiplication function <u>with</u> reduction/rearrange</param>
    /// <param name="power">Power function <u>with</u> reduction/rearrange</param>
    /// <param name="operands">The operands to reduce</param>
    /// <returns>A reduced operand <b>if</b> any reduction has occurred, otherwise default(T)</returns>
    /// <remarks>Complexity: O(n) factor enumeration then grouping (hash-based). n = total flattened multiplicative factors.</remarks>
    internal static T ReduceMultiplication<T>(Func<IEnumerable<T>, T> multiply, Func<T, int, T> power, params T[] operands)
        where T : IOperand
    {
        var mapMul = ExponentMap<T>.Rent();
        try
        {
            var reduced = Factorization.AccumulateProduct(mapMul, operands);
            if (!reduced)
            {
                return default;
            }

            var list = new List<T>();

            foreach (var kv in Factorization.EnumerateNonZero(mapMul))
            {
                list.Add(power(kv.Key, kv.Value));
            }

            return multiply(list);
        }
        finally
        {
            mapMul.Return();
        }
    }

    /// <summary>Flattens nested product structures ( (A*B)*(C*D*E)*F => A*B*C*D*E*F ).</summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="operands">The operands to linearize</param>
    /// <returns>A linear set of operands, i.e. if multiplications occur, the operands will all be included in list</returns>
    /// <remarks>Complexity: O(n) with n flattened multiplicative terms.</remarks>
    internal static IEnumerable<T> LinearizeMultiplication<T>(params T[] operands) where T : IOperand
    {
        return operands.SelectMany(x => x.ExpandMultiplication().OfType<T>());
    }

    /// <summary>
    /// Expands power expressions over composite structures and consolidates nested powers / division forms.
    /// Handles cases such as (A/B)^n and (A^m)^n. Returns default when no transformation applies.
    /// </summary>
    /// <typeparam name="T">The type of operand</typeparam>
    /// <param name="multiply">Multiplication function <u>with</u> reduction/rearrange</param>
    /// <param name="divide">Division function <u>with</u> reduction/rearrange</param>
    /// <param name="power">Power function <u>with</u> reduction/rearrange</param>
    /// <param name="base">The power base operand</param>
    /// <param name="exponent">The power exponent</param>
    /// <returns>An operand representing expanded power operation</returns>
    /// <remarks>Complexity: O(k) where k = operand count in product or division base; O(1) for simple or nested power.</remarks>
    internal static T ExpandPower<T>(Func<IEnumerable<T>, T> multiply, Func<T, T, T> divide, Func<T, int, T> power, T @base, int exponent) where T : IOperand
    {
        if (@base is IPowerOperation)
        {
            return power((T)(@base as IPowerOperation).Base, (@base as IPowerOperation).Exponent * exponent);
        }

        if (@base is IDivisionOperation)
        {
            var dividend = (T)(@base as IDivisionOperation).Dividend;
            var divisor = (T)(@base as IDivisionOperation).Divisor;

            // If dividend is a NullUnit/NullDimension (symbol empty) treat any power as identity (1^n == 1)
            if (dividend is NullUnit || dividend is Dimensions.NullDimension)
            {
                return divide(dividend, power(divisor, exponent));
            }

            return divide(power(dividend, exponent), power(divisor, exponent));
        }

        if (@base is IProductOperation)
        {
            if (ReductionSettings.LazyPowerExpansion)
            {
                return default; // caller constructs Power wrapper
            }

            return multiply((@base as IProductOperation).Operands.Select(x => power((T)x, exponent)));
        }

        return default;
    }
}