using Microsoft.CodeAnalysis;

namespace Veggerby.Units.Analyzers;

internal static class VUNITS001_Descriptor
{
    public const string DiagnosticId = "VUNITS001";

    public static readonly DiagnosticDescriptor Rule = new(
        id: DiagnosticId,
        title: "Incompatible unit addition/subtraction",
        messageFormat: "Add/subtract measurements with incompatible units – convert one side explicitly",
        category: "Veggerby.Units.Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Measurements may only be added or subtracted when units are identical. Convert explicitly using In().");
}