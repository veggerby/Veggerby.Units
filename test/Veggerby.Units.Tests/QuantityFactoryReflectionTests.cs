using System;
using System.Linq;
using System.Reflection;

using AwesomeAssertions;

using Veggerby.Units.Quantities;

using Xunit;

namespace Veggerby.Units.Tests;

public class QuantityFactoryReflectionTests
{
    [Fact]
    public void AllPublicStaticQuantityFactories_ReturnQuantityWithExpectedKindAndUnitCompatibility()
    {
        // Arrange
        var methods = typeof(Quantity).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.ReturnType.IsGenericType && m.ReturnType.GetGenericTypeDefinition() == typeof(Quantity<>))
            .Where(m => m.GetParameters().Length == 1) // single-value factories (exclude Of or multi-parameter if any introduced later)
            .ToList();

        methods.Should().NotBeEmpty();

        foreach (var m in methods)
        {
            // Act
            var q = m.Invoke(null, new object[] { 1d });

            // Assert
            q.Should().NotBeNull();
            var kindProp = q.GetType().GetProperty("Kind");
            kindProp.Should().NotBeNull();
            var kind = kindProp.GetValue(q) as QuantityKind;
            kind.Should().NotBeNull();
            var measProp = q.GetType().GetProperty("Measurement");
            measProp.Should().NotBeNull();
            var measurement = measProp.GetValue(q) as DoubleMeasurement;
            measurement.Should().NotBeNull();
            // Ensure dimensional consistency
            measurement.Unit.Dimension.Should().Be(kind.CanonicalUnit.Dimension);
        }
    }
}