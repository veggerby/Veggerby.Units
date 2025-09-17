using System;
using System.Linq;
using AwesomeAssertions;
using Veggerby.Units.Reduction;
using Xunit;

namespace Veggerby.Units.Tests;

/// <summary>
/// Tests covering Steps 1+2+3 canonical factor multiset equality & normalisation idempotence.
/// </summary>
public class EqualityNormalizationTests
{
    [Fact]
    public void ProductPowerEquality_Canonical_Parity_LazyVsEager()
    {
        // Arrange
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        var originalNorm = ReductionSettings.EqualityNormalizationEnabled;
        try
        {
            ReductionSettings.EqualityNormalizationEnabled = true;
            ReductionSettings.LazyPowerExpansion = false; // eager baseline
            var composite = Unit.SI.m * Unit.SI.s * Unit.SI.kg * Unit.SI.m; // m s kg m => m^2 s kg
            var eager = composite ^ 5; // distributed: m^10 s^5 kg^5

            ReductionSettings.LazyPowerExpansion = true; // now lazy path
            var lazy = composite ^ 5; // PowerUnit(Product,5)

            // Act / Assert
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = originalLazy;
            ReductionSettings.EqualityNormalizationEnabled = originalNorm;
        }
    }

    [Fact]
    public void ProductPowerEquality_Adversarial_Orderings()
    {
        // Arrange
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        var originalNorm = ReductionSettings.EqualityNormalizationEnabled;
        try
        {
            ReductionSettings.EqualityNormalizationEnabled = true;
            ReductionSettings.LazyPowerExpansion = true;
            var bases = new[] { Unit.SI.m, Unit.SI.s };
            var shuffled = bases.Reverse().ToArray();
            var left = ((bases[0] * bases[1]) ^ 2) * Unit.SI.kg; // ((m*s)^2)*kg lazy wrapper inside power
            var right = (bases[0] ^ 2) * (bases[1] ^ 2) * Unit.SI.kg; // distributed eager form
            var leftAlt = ((shuffled[0] * shuffled[1]) ^ 2) * Unit.SI.kg; // swapped order

            // Act / Assert
            (left == right).Should().BeTrue();
            (leftAlt == right).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = originalLazy;
            ReductionSettings.EqualityNormalizationEnabled = originalNorm;
        }
    }

    [Fact]
    public void Normalization_AlreadyDistributedVsLazyPowerProduct_SameFactors()
    {
        // Arrange
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        var originalNorm = ReductionSettings.EqualityNormalizationEnabled;
        try
        {
            ReductionSettings.EqualityNormalizationEnabled = true;
            var composite = Unit.SI.m * Unit.SI.s;
            ReductionSettings.LazyPowerExpansion = false;
            var eager = composite ^ 3; // m^3 s^3
            ReductionSettings.LazyPowerExpansion = true;
            var lazy = composite ^ 3; // Power(Product,3)

            // Assert
            (lazy == eager).Should().BeTrue();
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = originalLazy;
            ReductionSettings.EqualityNormalizationEnabled = originalNorm;
        }
    }

    [Fact]
    public void Normalization_Idempotence()
    {
        // Arrange
        var originalNorm = ReductionSettings.EqualityNormalizationEnabled;
        ReductionSettings.EqualityNormalizationEnabled = true;
        try
        {
            var expr = (Unit.SI.m * Unit.SI.s * Unit.SI.m) ^ 2; // (m s m)^2 -> m^4 s^2

            // Act
            var factors1 = OperationUtility.TryGetCanonicalFactorsForDiagnostics(expr);
            var factors2 = OperationUtility.TryGetCanonicalFactorsForDiagnostics(expr);

            // Assert
            factors1.Length.Should().Be(factors2.Length);
            for (int i = 0; i < factors1.Length; i++)
            {
                factors1[i].Symbol.Should().Be(factors2[i].Symbol);
                factors1[i].Exponent.Should().Be(factors2[i].Exponent);
            }
        }
        finally
        {
            ReductionSettings.EqualityNormalizationEnabled = originalNorm;
        }
    }

    [Fact]
    public void Equality_Idempotent_TwoConsecutiveCalls()
    {
        // Arrange
        var originalLazy = ReductionSettings.LazyPowerExpansion;
        var originalNorm = ReductionSettings.EqualityNormalizationEnabled;
        try
        {
            ReductionSettings.EqualityNormalizationEnabled = true;
            ReductionSettings.LazyPowerExpansion = true;
            var a = (Unit.SI.m * Unit.SI.s * Unit.SI.m) ^ 5; // lazy power-of-product potential
            var b = (Unit.SI.m ^ 10) * (Unit.SI.s ^ 5); // distributed equivalent (m^2)^5 -> m^10
            Equality.EqualityTestHelper.AssertIdempotentEquality(a, b, expected: true);
        }
        finally
        {
            ReductionSettings.LazyPowerExpansion = originalLazy;
            ReductionSettings.EqualityNormalizationEnabled = originalNorm;
        }
    }
}
