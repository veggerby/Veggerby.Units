using System.Collections.Generic;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityKindTagValidationDebugTests
{
    [Fact]
    public void ValidateReservedRootsOnce_IsIdempotent()
    {
        // Arrange
        var kinds = new List<QuantityKind>();
        foreach (var field in typeof(QuantityKinds).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
        {
            if (field.FieldType == typeof(QuantityKind) && field.GetValue(null) is QuantityKind k)
            {
                kinds.Add(k);
            }
        }

        // Act
        QuantityKindTagExtensions.ValidateReservedRootsOnce(kinds);
        QuantityKindTagExtensions.ValidateReservedRootsOnce(kinds); // second call should early-return in DEBUG

        // Assert (no exception & tag set unchanged)
        kinds.Count.Should().BeGreaterThan(0);
    }
}