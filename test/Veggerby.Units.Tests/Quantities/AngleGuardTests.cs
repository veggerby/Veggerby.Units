namespace Veggerby.Units.Tests.Quantities;

public class AngleGuardTests
{
    [Fact]
    public void GivenAngleAndEnergy_WhenMultiplyWithoutRule_ThenThrows()
    {
        // Arrange
        var angle = Quantity.Angle(2.0); // dimensionless semantic
        var energy = Quantity.Energy(3.0);

        // Act
        var act = () => _ = angle * energy; // currently would fallback preserving Energy unless guard added

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenTorqueAndAngle_WhenMultiply_ThenEnergy()
    {
        // Arrange
        var torque = Quantity.Torque(4.0);
        var angle = Quantity.Angle(0.5);

        // Act
        var res = torque * angle; // should infer Energy by existing rule

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)res.Measurement).Should().BeApproximately(2.0, 1e-12);
    }

    [Fact]
    public void GivenAngleAndTorque_WhenMultiply_ThenEnergy_Commutative()
    {
        // Arrange
        var angle = Quantity.Angle(0.5);
        var torque = Quantity.Torque(4.0);

        // Act
        var res = angle * torque;

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.Energy);
        ((double)res.Measurement).Should().BeApproximately(2.0, 1e-12);
    }

    [Fact]
    public void GivenEnergyDividedByAngle_WhenDivide_ThenTorque()
    {
        // Arrange
        var energy = Quantity.Energy(10.0);
        var angle = Quantity.Angle(2.0);

        // Act
        var res = energy / angle;

        // Assert
        res.Kind.Should().BeSameAs(QuantityKinds.Torque);
        ((double)res.Measurement).Should().BeApproximately(5.0, 1e-12);
    }
}