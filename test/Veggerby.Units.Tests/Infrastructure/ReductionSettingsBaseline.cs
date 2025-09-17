using Veggerby.Units.Reduction;

namespace Veggerby.Units.Tests.Infrastructure;

/// <summary>
/// Baseline assertion utility to ensure no leaked flag state crosses test boundaries.
/// </summary>
internal static class ReductionSettingsBaseline
{
    public static void AssertDefaults()
    {
        // Expected defaults per ReductionSettings definitions.
        AssertFlag(ReductionSettings.UseExponentMapForReduction == false, nameof(ReductionSettings.UseExponentMapForReduction));
        AssertFlag(ReductionSettings.DivisionSinglePass == false, nameof(ReductionSettings.DivisionSinglePass));
        AssertFlag(ReductionSettings.UseFactorVector == false, nameof(ReductionSettings.UseFactorVector));
        AssertFlag(ReductionSettings.LazyPowerExpansion == false, nameof(ReductionSettings.LazyPowerExpansion));
        AssertFlag(ReductionSettings.EqualityNormalizationEnabled == true, nameof(ReductionSettings.EqualityNormalizationEnabled));
    }

    private static void AssertFlag(bool condition, string flagName)
    {
        if (!condition)
        {
            var owner = ReductionSettingsScope.Owner ?? "<unknown>";
            throw new Xunit.Sdk.XunitException($"Baseline flag assertion failed for '{flagName}'. Last owner: {owner}");
        }
    }
}