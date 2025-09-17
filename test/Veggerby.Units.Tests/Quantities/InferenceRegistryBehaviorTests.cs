using System;

using Veggerby.Units.Quantities;

namespace Veggerby.Units.Tests.Quantities;

public class InferenceRegistryBehaviorTests
{
    [Fact]
    public void GivenDuplicateRuleSameResult_WhenStrict_ThenNoThrow()
    {
        // Arrange
        QuantityKindInferenceRegistry.ResetForTests();
        var left = QuantityKinds.Force;
        var right = QuantityKinds.Length;
        var result = QuantityKinds.Energy; // Force * Length => Energy (seed rule already exists)

        // Act
        var act = () => QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, QuantityKindBinaryOperator.Multiply, right, result, true));

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void GivenDuplicateRuleDifferentResult_WhenStrict_ThenThrows()
    {
        // Arrange
        QuantityKindInferenceRegistry.ResetForTests();
        var left = QuantityKinds.Force;
        var right = QuantityKinds.Length;
        var fakeResult = QuantityKinds.Torque; // Different semantic kind, same dimension

        // Act
        var act = () => QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, QuantityKindBinaryOperator.Multiply, right, fakeResult, true));

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenDuplicateRuleDifferentResult_WhenNotStrict_ThenOverwrites()
    {
        // Arrange
        QuantityKindInferenceRegistry.ResetForTests();
        var left = QuantityKinds.Pressure;
        var right = QuantityKinds.Area;
        var original = QuantityKinds.Force; // Seed rule Pressure * Area => Force (commutative)
        var replacement = QuantityKinds.Energy; // nonsense replacement for test
        QuantityKindInferenceRegistry.StrictConflictDetection = false;

        try
        {
            // Act
            var act = () => QuantityKindInferenceRegistry.Register(new QuantityKindInference(left, QuantityKindBinaryOperator.Multiply, right, replacement, true));

            // Assert (should not throw)
            act.Should().NotThrow();
        }
        finally
        {
            QuantityKindInferenceRegistry.StrictConflictDetection = true;
        }
    }
}