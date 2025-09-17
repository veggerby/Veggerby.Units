using Veggerby.Units.Reduction;

using Xunit;

namespace Veggerby.Units.Tests.Infrastructure;

/// <summary>
/// xUnit test collection definition that serializes all tests mutating <see cref="ReductionSettings"/>.
/// Ensures no parallel interference between feature flag mutations eliminating first-run flakes.
/// </summary>
[CollectionDefinition(Name)]
public sealed class ReductionSettingsCollection : ICollectionFixture<ReductionSettingsFixture>
{
    public const string Name = "ReductionSettings";
}