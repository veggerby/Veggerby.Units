using Microsoft.CodeAnalysis;

namespace Veggerby.Units.Analyzers;

internal static class VUNITS002_Descriptor
{
    public const string DiagnosticId = "VUNITS002";

    public static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId,
        title: "Ambiguous unit formatting",
        messageFormat: "Ambiguous unit symbol '{0}' formatted without Qualified or Mixed format specifier",
        category: "Veggerby.Units.Formatting",
        defaultSeverity: DiagnosticSeverity.Info,
        isEnabledByDefault: true,
        description: "Explicitly choose UnitFormat.Qualified or UnitFormat.Mixed when formatting ambiguous symbols (e.g., J, Pa, W, H).");
}