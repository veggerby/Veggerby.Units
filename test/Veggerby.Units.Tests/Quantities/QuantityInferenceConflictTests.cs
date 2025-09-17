using System;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests.Quantities;

public class QuantityInferenceConflictTests
{
    [Fact]
    public void GivenConflictingMapping_WhenRegister_ThenThrows()
    {
        // Arrange
        // Use a rarely combined pair to avoid pre-seeded rules
        var left = QuantityKinds.Momentum;
        var right = QuantityKinds.Velocity;
        var op = QuantityKindBinaryOperator.Multiply;

        // First registration (custom arbitrary target) – should succeed
        var customTarget = QuantityKinds.Energy; // arbitrary existing kind for test purposes
        QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, op, right, customTarget));

        // Act
        var act = () => QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, op, right, QuantityKinds.Force));

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}