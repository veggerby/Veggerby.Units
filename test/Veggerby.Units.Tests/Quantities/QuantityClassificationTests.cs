namespace Veggerby.Units.Tests.Quantities;

public class QuantityClassificationTests
{
    [Fact]
    public void GivenWorkAndHeat_WhenCreated_ThenShareEnergyDimensionDifferentKinds()
    {
        // Arrange
        var w = Quantity.Work(10.0);
        var q = Quantity.Heat(5.0);

        // Act
        var dimEqual = w.Measurement.Unit.Dimension == q.Measurement.Unit.Dimension;

        // Assert
        dimEqual.Should().BeTrue();
        w.Kind.Should().NotBe(q.Kind);
        w.Kind.HasTag("Energy.PathFunction").Should().BeTrue();
        q.Kind.HasTag("Energy.PathFunction").Should().BeTrue();
    }

    [Fact]
    public void GivenWorkPlusEnergy_WhenAdd_ThenThrowsCrossKind()
    {
        // Arrange
        var w = Quantity.Work(3.0);
        var e = Quantity.Energy(2.0);

        // Act
        var act = () => _ = w + e;

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GivenEnergyBalance_WhenAggregated_ThenNetMatches()
    {
        // Arrange
        var balance = new EnergyBalance();
        balance.AddWork(Quantity.Work(4.0));
        balance.AddHeat(Quantity.Heat(-1.0));

        // Act
        var net = balance.NetTransfer();

        // Assert
        net.Kind.Should().BeSameAs(QuantityKinds.Energy);
        net.Measurement.Value.Should().BeApproximately(3.0, 1e-12);
        balance.TotalWork().Measurement.Value.Should().BeApproximately(4.0, 1e-12);
        balance.TotalHeat().Measurement.Value.Should().BeApproximately(-1.0, 1e-12);
    }
}