using System.Linq;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Tests.Equality;

/// <summary>
/// Helper utilities for asserting deterministic and idempotent equality behaviour.
/// </summary>
internal static class EqualityTestHelper
{
    public static void AssertIdempotentEquality(Unit a, Unit b, bool expected = true)
    {
        // Act
        var first = a == b;
        var second = a == b;

        if (first != second || first != expected)
        {
            var fa = OperationUtility.TryGetCanonicalFactorsForDiagnostics(a);
            var fb = OperationUtility.TryGetCanonicalFactorsForDiagnostics(b);
            var formattedA = string.Join(" ", fa.Select(f => $"{f.Symbol}^{f.Exponent}"));
            var formattedB = string.Join(" ", fb.Select(f => $"{f.Symbol}^{f.Exponent}"));
            throw new Xunit.Sdk.XunitException($"Equality idempotence failure: first={first} second={second} expected={expected}\nA: {formattedA}\nB: {formattedB}");
        }
    }
}