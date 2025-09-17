using System;
using System.Runtime.CompilerServices;

using Veggerby.Units.Reduction;

namespace Veggerby.Units.Tests.Infrastructure;

/// <summary>
/// Disposable scope that snapshots and restores all <see cref="ReductionSettings"/> values.
/// Also records <see cref="Owner"/> for diagnostic baseline assertions.
/// </summary>
public sealed class ReductionSettingsScope : IDisposable
{
    private readonly ReductionSettingsSnapshot _snapshot;
    private readonly ReductionSettingsFixture _fixture;
    private bool _disposed;

    internal static string Owner { get; private set; }

    private readonly bool _lockTaken;

    public ReductionSettingsScope(ReductionSettingsFixture fixture,
        bool? useExponentMapForReduction = null,
        bool? divisionSinglePass = null,
        bool? useFactorVector = null,
        bool? lazyPowerExpansion = null,
        bool? equalityNormalizationEnabled = null,
        [CallerMemberName] string owner = null)
    {
        _fixture = fixture;
        System.Threading.Monitor.Enter(_fixture.SyncRoot, ref _lockTaken);
        _snapshot = ReductionSettingsSnapshot.Capture();
        Owner = owner;

        if (useExponentMapForReduction.HasValue)
        {
            ReductionSettings.UseExponentMapForReduction = useExponentMapForReduction.Value;
        }

        if (divisionSinglePass.HasValue)
        {
            ReductionSettings.DivisionSinglePass = divisionSinglePass.Value;
        }

        if (useFactorVector.HasValue)
        {
            ReductionSettings.UseFactorVector = useFactorVector.Value;
        }

        if (lazyPowerExpansion.HasValue)
        {
            ReductionSettings.LazyPowerExpansion = lazyPowerExpansion.Value;
        }

        if (equalityNormalizationEnabled.HasValue)
        {
            ReductionSettings.EqualityNormalizationEnabled = equalityNormalizationEnabled.Value;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _snapshot.Restore();
        Owner = null;

        if (_lockTaken)
        {
            System.Threading.Monitor.Exit(_fixture.SyncRoot);
        }

        _disposed = true;
    }
}