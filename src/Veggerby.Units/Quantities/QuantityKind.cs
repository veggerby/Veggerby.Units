using System;
using System.Collections.Generic;

namespace Veggerby.Units.Quantities;

/// <summary>
/// Describes a semantic quantity kind (e.g. Energy, Torque, Entropy) that may share dimensions with other kinds
/// but represents a distinct physical concept. Provides optional dimensional guard and open tagging metadata.
/// </summary>
/// <remarks>
/// Conceptually aligned with the QUDT (Quantities, Units, Dimensions, and Types) ontology's
/// <c>qudt:QuantityKind</c> class, which provides standardized categorization of physical quantities.
/// While QUDT uses RDF/OWL ontologies for semantic reasoning, Veggerby.Units implements a deterministic,
/// explicit design without runtime dependencies. See <c>docs/qudt-alignment.md</c> for detailed mapping.
/// <para>
/// QUDT Reference: http://qudt.org/schema/qudt/QuantityKind
/// </para>
/// </remarks>
public sealed class QuantityKind
{
    /// <summary>Human readable name.</summary>
    public string Name { get; }
    /// <summary>Canonical unit for dimensional validation.</summary>
    public Unit CanonicalUnit { get; }
    /// <summary>Symbolic abbreviation.</summary>
    public string Symbol { get; }

    /// <summary>
    /// Indicates whether the <c>+</c> operator between two quantities of this kind should be permitted directly.
    /// For affine absolutes (e.g. absolute temperature) this is typically <c>false</c>.
    /// </summary>
    public bool AllowDirectAddition { get; }

    /// <summary>
    /// Indicates whether the <c>-</c> operator producing a same-kind quantity is permitted directly. For affine
    /// absolutes where subtraction should yield a delta kind, this is typically <c>false</c>.
    /// </summary>
    public bool AllowDirectSubtraction { get; }

    /// <summary>
    /// Semantic result kind for subtracting two quantities of this kind when direct subtraction is disallowed.
    /// Enables Point − Point → Vector style algebra (e.g. AbsoluteTemperature − AbsoluteTemperature → TemperatureDelta).
    /// When <c>null</c> and <see cref="AllowDirectSubtraction"/> is false, subtraction throws.
    /// </summary>
    public QuantityKind DifferenceResultKind { get; }

    private readonly HashSet<QuantityKindTag> _tags = [];

    /// <summary>
    /// Open semantic tag set (informational only). Tags provide arbitrary, extensible classification without
    /// impacting dimensional algebra. They enable policy rules, reporting, or domain grouping (e.g.
    /// Energy.StateFunction, Energy.PathFunction, Domain.Mechanical, Domain.Thermodynamic, Form.Dimensionless).
    /// Tags are canonical: requesting the same name via <see cref="QuantityKindTag.Get(string)"/> returns the
    /// same instance enabling fast reference comparisons.
    /// </summary>
    public IReadOnlySet<QuantityKindTag> Tags => _tags;

    /// <summary>True when the kind has a tag with the specified name.</summary>
    public bool HasTag(string name)
        => _tags.Contains(QuantityKindTag.Get(name));

    /// <summary>True when the kind has the specified tag instance.</summary>
    public bool HasTag(QuantityKindTag tag) => _tags.Contains(tag);

    /// <summary>Internal initializer adds provided tags.</summary>
    private void InitializeTags(IEnumerable<QuantityKindTag> userTags)
    {
        if (userTags is not null)
        {
            foreach (var t in userTags)
            {
                if (t is not null)
                {
                    _tags.Add(t);
                }
            }
        }
    }

    /// <summary>
    /// Creates a new semantic quantity kind.
    /// </summary>
    /// <param name="name">Human readable name.</param>
    /// <param name="canonicalUnit">Representative unit for dimensional validation.</param>
    /// <param name="symbol">Symbolic abbreviation (optional).</param>
    /// <param name="allowDirectAddition">If false, same-kind addition is disallowed unless mixed point/delta rule applies.</param>
    /// <param name="allowDirectSubtraction">If false, same-kind subtraction is disallowed unless <paramref name="differenceResultKind"/> provided.</param>
    /// <param name="differenceResultKind">Semantic result for Point − Point patterns (e.g. AbsoluteTemperature − AbsoluteTemperature → TemperatureDelta).</param>
    /// <param name="tags">Open semantic tag set (e.g. Energy.StateFunction, Energy.PathFunction, Domain.Mechanical).</param>
    public QuantityKind(string name, Unit canonicalUnit, string symbol, bool allowDirectAddition = true, bool allowDirectSubtraction = true, QuantityKind differenceResultKind = null, IEnumerable<QuantityKindTag> tags = null)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CanonicalUnit = canonicalUnit ?? throw new ArgumentNullException(nameof(canonicalUnit));
        Symbol = symbol ?? string.Empty;
        AllowDirectAddition = allowDirectAddition;
        AllowDirectSubtraction = allowDirectSubtraction;
        DifferenceResultKind = differenceResultKind;
        InitializeTags(tags);
    }

    /// <summary>Returns true when the supplied unit has the same dimension as the canonical unit.</summary>
    public bool Matches(Unit unit) => unit.Dimension == CanonicalUnit.Dimension;

    /// <summary>Returns a readable representation including symbol when present.</summary>
    public override string ToString() => string.IsNullOrEmpty(Symbol) ? Name : $"{Name} ({Symbol})";
}