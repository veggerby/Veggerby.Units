namespace Veggerby.Units.Tests.Infrastructure;

/// <summary>
/// Fixture holds the global lock used by <see cref="ReductionSettingsScope"/>.
/// </summary>
public sealed class ReductionSettingsFixture
{
    internal readonly object SyncRoot = new();
}