namespace Veggerby.Units.Dimensions;

/// <summary>
/// Dimensionless identity used whenever a unit has no physical dimension (e.g. the neutral element of algebraic
/// operations or the result of raising a unit to the power 0).
/// </summary>
public class NullDimension : Dimension
{
    /// <inheritdoc />
    public override string Symbol => string.Empty;
    /// <inheritdoc />
    public override string Name => string.Empty;
}