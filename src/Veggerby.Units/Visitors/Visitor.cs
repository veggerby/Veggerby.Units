namespace Veggerby.Units.Visitors;

/// <summary>
/// Visitor pattern abstraction enabling traversal without exposing concrete unit internal state externally.
/// Implementations should be side‑effect free and may assume immutability of the visited structures.
/// New unit varieties should extend this contract to avoid default switch / type checks elsewhere.
/// </summary>
public abstract class Visitor<T>
{
    /// <summary>Visits a basic unit.</summary>
    public abstract T Visit(BasicUnit v);
    /// <summary>Visits a product unit.</summary>
    public abstract T Visit(ProductUnit v);
    /// <summary>Visits a division unit.</summary>
    public abstract T Visit(DivisionUnit v);
    /// <summary>Visits a power unit.</summary>
    public abstract T Visit(PowerUnit v);
    /// <summary>Visits a derived unit.</summary>
    public abstract T Visit(DerivedUnit v);
    /// <summary>Visits a prefixed unit.</summary>
    public abstract T Visit(PrefixedUnit v);
    /// <summary>Visits the null (identity) unit.</summary>
    public abstract T Visit(NullUnit v);
    /// <summary>Visits a scale unit.</summary>
    public abstract T Visit(ScaleUnit v);
    /// <summary>Visits an affine unit (e.g. Celsius).</summary>
    public abstract T Visit(AffineUnit v);
}