using Veggerby.Units.Reduction;

namespace Veggerby.Units.Tests.Infrastructure;

internal readonly struct ReductionSettingsSnapshot
{
    private readonly bool _useExponentMapForReduction;
    private readonly bool _divisionSinglePass;
    private readonly bool _useFactorVector;
    private readonly bool _lazyPowerExpansion;
    private readonly bool _equalityNormalizationEnabled;

    private ReductionSettingsSnapshot(bool useExponentMapForReduction, bool divisionSinglePass, bool useFactorVector, bool lazyPowerExpansion, bool equalityNormalizationEnabled)
    {
        _useExponentMapForReduction = useExponentMapForReduction;
        _divisionSinglePass = divisionSinglePass;
        _useFactorVector = useFactorVector;
        _lazyPowerExpansion = lazyPowerExpansion;
        _equalityNormalizationEnabled = equalityNormalizationEnabled;
    }

    public static ReductionSettingsSnapshot Capture() => new(
        ReductionSettings.UseExponentMapForReduction,
        ReductionSettings.DivisionSinglePass,
        ReductionSettings.UseFactorVector,
        ReductionSettings.LazyPowerExpansion,
        ReductionSettings.EqualityNormalizationEnabled);

    public void Restore()
    {
        ReductionSettings.UseExponentMapForReduction = _useExponentMapForReduction;
        ReductionSettings.DivisionSinglePass = _divisionSinglePass;
        ReductionSettings.UseFactorVector = _useFactorVector;
        ReductionSettings.LazyPowerExpansion = _lazyPowerExpansion;
        ReductionSettings.EqualityNormalizationEnabled = _equalityNormalizationEnabled;
    }
}