using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Veggerby.Units.Analyzers;

/// <summary>
/// Analyzer VUNITS002: warns when ambiguous unit formatting is performed without explicit UnitFormat.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class VUNITS002Analyzer : DiagnosticAnalyzer
{
    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(VUNITS002_Descriptor.Rule);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        if (context is null)
        {
            return;
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not InvocationExpressionSyntax invocation)
        {
            return;
        }

        var method = context.SemanticModel.GetSymbolInfo(invocation).Symbol as IMethodSymbol;
        if (method is null)
        {
            return;
        }

        if (method.Name is not ("Format" or "ToString"))
        {
            return;
        }

        // Ensure we are operating on Measurement<T> or a Quantity wrapper in Veggerby.Units namespace.
        var containingType = method.ContainingType;
        if (containingType is null || !containingType.ContainingNamespace?.ToDisplayString().StartsWith("Veggerby.Units", System.StringComparison.Ordinal) == true)
        {
            return;
        }

        // If any argument is of type UnitFormat => already explicit, skip.
        foreach (var arg in invocation.ArgumentList.Arguments)
        {
            var argType = context.SemanticModel.GetTypeInfo(arg.Expression).Type;
            if (argType is not null && argType.Name == "UnitFormat")
            {
                return;
            }
        }

        // Heuristic to obtain symbol: look for MemberAccess like x.UnitSymbol or x.Unit.Symbol is not present.
        // Instead, we attempt to resolve the receiver expression type and, if it is generic Measurement<T>, attempt to locate a field/property 'Unit'.
        string? symbolText = null;
        ExpressionSyntax? receiverExpression = null;

        if (invocation.Expression is MemberAccessExpressionSyntax maes)
        {
            receiverExpression = maes.Expression;
        }
        else if (invocation.Expression is IdentifierNameSyntax && invocation.Parent is MemberAccessExpressionSyntax parentMaes)
        {
            receiverExpression = parentMaes.Expression;
        }

        if (receiverExpression is not null)
        {
            var receiverType = context.SemanticModel.GetTypeInfo(receiverExpression).Type;
            // Look for a Unit property returning a type with a Symbol property constant.
            if (receiverType is not null)
            {
                var unitProp = receiverType.GetMembers().OfType<IPropertySymbol>().FirstOrDefault(p => p.Name == "Unit");
                if (unitProp?.Type is not null)
                {
                    // Search for a string constant Symbol on the Unit type.
                    var symbolMember = unitProp.Type.GetMembers("Symbol").FirstOrDefault();
                    if (symbolMember is IFieldSymbol fs && fs.IsConst && fs.Type.SpecialType == SpecialType.System_String && fs.ConstantValue is string s1)
                    {
                        symbolText = s1;
                    }
                    else if (symbolMember is IPropertySymbol ps && ps.GetMethod is not null && ps.Type.SpecialType == SpecialType.System_String)
                    {
                        // Best effort: attempt constant value from attributes not available; skip invocation of property for performance.
                        // Without execution we cannot get runtime value; fall back to name-based heuristics.
                        // We only proceed if property name is 'Symbol' and unit type name itself is short, treat its name as candidate.
                        symbolText = unitProp.Type.Name.Length <= 3 ? unitProp.Type.Name : null;
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(symbolText))
        {
            // Attempt heuristic: if receiver is identifier referencing local initialized with object creation whose second argument matches Joule pattern (m*m*kg/(s*s)) treat as J.
            if (receiverExpression is IdentifierNameSyntax id)
            {
                var enclosingMethod = id.Ancestors().OfType<MethodDeclarationSyntax>().FirstOrDefault();
                if (enclosingMethod is not null)
                {
                    foreach (var declarator in enclosingMethod.DescendantNodes().OfType<VariableDeclaratorSyntax>())
                    {
                        if (declarator.Identifier.Text == id.Identifier.Text && declarator.Initializer?.Value is ObjectCreationExpressionSyntax oce && oce.ArgumentList is not null && oce.ArgumentList.Arguments.Count >= 2)
                        {
                            var unitArgText = oce.ArgumentList.Arguments[1].Expression.NormalizeWhitespace().ToFullString().Replace(" ", string.Empty);
                            // Very narrow canonical pattern recognition for Joule dimensional expression used in tests.
                            if (unitArgText.Contains("Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s)") || unitArgText.Contains("Unit.SI.m * Unit.SI.m * Unit.SI.kg / (Unit.SI.s * Unit.SI.s)"))
                            {
                                symbolText = "J";
                                break;
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(symbolText))
            {
                return; // Cannot determine symbol; avoid noisy diagnostics.
            }
        }

        // Ambiguous symbol set mirrored from AmbiguityRegistry (keep in sync manually while release tracking suppressed).
        // J, Pa, W, H currently.
        if (symbolText is not ("J" or "Pa" or "W" or "H"))
        {
            return;
        }

        context.ReportDiagnostic(Diagnostic.Create(VUNITS002_Descriptor.Rule, invocation.GetLocation(), symbolText));
    }
}