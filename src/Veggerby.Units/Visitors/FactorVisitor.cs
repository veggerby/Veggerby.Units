namespace Veggerby.Units.Visitors;

// Deprecated: Scale factors now computed via Unit.GetScaleFactor overrides.
// Retained as an empty stub to avoid potential breaking changes for code using reflection-based discovery or
// future merges that relied on the earlier visitor based factor accumulation.
using System;

[Obsolete("FactorVisitor is deprecated; scale factors are computed directly via Unit.GetScaleFactor.")]
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
    public override double Visit(AffineUnit v) => 1d;
}