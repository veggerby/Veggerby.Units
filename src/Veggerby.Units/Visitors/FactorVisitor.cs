namespace Veggerby.Units.Visitors;

// Deprecated: Scale factors now computed via Unit.GetScaleFactor overrides.
// Retained as an empty stub to avoid breaking existing reflection or future merges.
internal sealed class FactorVisitor : Visitor<double>
{
    public static readonly FactorVisitor Instance = new();
    private FactorVisitor() { }
    public override double Visit(BasicUnit v) => 1d;
    public override double Visit(ProductUnit v) => 1d;
    public override double Visit(DivisionUnit v) => 1d;
    public override double Visit(PowerUnit v) => 1d;
    public override double Visit(DerivedUnit v) => 1d;
    public override double Visit(PrefixedUnit v) => 1d;
    public override double Visit(NullUnit v) => 1d;
    public override double Visit(ScaleUnit v) => 1d;
}