using System.Collections.Immutable;
using System.Linq;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Veggerby.Units.Analyzers;

/// <summary>
/// Analyzer VUNITS001: flags addition/subtraction of different measurement types (MVP heuristic).
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class VUNITS001Analyzer : DiagnosticAnalyzer
{
    /// <inheritdoc />
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(VUNITS001_Descriptor.Rule);

    /// <inheritdoc />
    public override void Initialize(AnalysisContext context)
    {
        if (context == null)
        {
            return;
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeBinary, SyntaxKind.AddExpression, SyntaxKind.SubtractExpression);
    }

    private static void AnalyzeBinary(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not BinaryExpressionSyntax binary)
        {
            return;
        }

        var leftType = context.SemanticModel.GetTypeInfo(binary.Left).Type;
        var rightType = context.SemanticModel.GetTypeInfo(binary.Right).Type;
        if (leftType == null || rightType == null)
        {
            return;
        }

        if (!IsMeasurementType(leftType) || !IsMeasurementType(rightType))
        {
            return;
        }
        // Generic parameter (numeric type) must match; otherwise the operator would already be invalid at compile time.
        if (leftType is INamedTypeSymbol leftNamed && rightType is INamedTypeSymbol rightNamed)
        {
            if (!SymbolEqualityComparer.Default.Equals(leftNamed.TypeArguments.FirstOrDefault(), rightNamed.TypeArguments.FirstOrDefault()))
            {
                return; // different numeric generic argument => unrelated to unit mismatch for this rule
            }
        }

        // Attempt to extract unit expressions from both operands. If both resolvable and structurally differ -> diagnostic.
        var leftUnitSyntax = ExtractUnitExpression(binary.Left);
        var rightUnitSyntax = ExtractUnitExpression(binary.Right);

        if (leftUnitSyntax == null || rightUnitSyntax == null)
        {
            return; // cannot confidently resolve both units => no flag (avoid false positives)
        }

        if (!AreUnitsStructurallyEqual(leftUnitSyntax, rightUnitSyntax, context.SemanticModel, context.CancellationToken))
        {
            context.ReportDiagnostic(Diagnostic.Create(VUNITS001_Descriptor.Rule, binary.GetLocation()));
        }
    }

    private static bool IsMeasurementType(ITypeSymbol type)
    {
        if (type is INamedTypeSymbol named)
        {
            // Direct generic Measurement<T>
            if (named.IsGenericType && named.Name == "Measurement" && named.ContainingNamespace.ToDisplayString().StartsWith("Veggerby.Units", System.StringComparison.Ordinal))
            {
                return true;
            }

            // Derived concrete measurement types (e.g., Int32Measurement) inherit from Measurement<T>
            var baseType = named.BaseType;
            while (baseType is INamedTypeSymbol baseNamed)
            {
                if (baseNamed.IsGenericType && baseNamed.Name == "Measurement" && baseNamed.ContainingNamespace.ToDisplayString().StartsWith("Veggerby.Units", System.StringComparison.Ordinal))
                {
                    return true;
                }
                baseType = baseNamed.BaseType;
            }
        }
        return false;
    }

    // Heuristic: Supported patterns
    // 1. IdentifierName or MemberAccess that resolves to a local/parameter/field/prop of Measurement<T> => use its trailing '.Unit' if present or attempt constructor argument extraction.
    // 2. Object creation: new Int32Measurement(value, <unitExpression>) => capture second argument.
    // 3. MemberAccess: <expr>.Unit
    // 4. Simple parenthesis wrappers.
    private static ExpressionSyntax? ExtractUnitExpression(ExpressionSyntax expression)
    {
        // Unwrap casts & parentheses
        while (true)
        {
            if (expression is ParenthesizedExpressionSyntax p)
            {
                expression = p.Expression;
                continue;
            }

            if (expression is CastExpressionSyntax cast)
            {
                expression = cast.Expression;
                continue;
            }
            break;
        }

        // Direct member access ending with .Unit
        if (expression is MemberAccessExpressionSyntax mae && mae.Name.Identifier.Text == "Unit")
        {
            return mae; // already a unit property access (treat as canonical form)
        }

        // Object creation pattern new <MeasurementType>(value, unit)
        if (expression is ObjectCreationExpressionSyntax oce && oce.ArgumentList != null && oce.ArgumentList.Arguments.Count >= 2)
        {
            return oce.ArgumentList.Arguments[1].Expression;
        }

        // Identifier referencing a local variable with initializer new <MeasurementType>(value, unit)
        if (expression is IdentifierNameSyntax id)
        {
            var symbol = id.AncestorsAndSelf()
                .OfType<CompilationUnitSyntax>()
                .FirstOrDefault();
            // Fallback traversal: search variable declarators with same identifier within method body scope
            var method = id.Ancestors().OfType<MethodDeclarationSyntax>().FirstOrDefault();
            if (method != null)
            {
                foreach (var declarator in method.DescendantNodes().OfType<VariableDeclaratorSyntax>())
                {
                    if (declarator.Identifier.Text == id.Identifier.Text && declarator.Initializer?.Value is ObjectCreationExpressionSyntax initOce && initOce.ArgumentList != null && initOce.ArgumentList.Arguments.Count >= 2)
                    {
                        return initOce.ArgumentList.Arguments[1].Expression;
                    }
                }
            }
        }

        // For now, we skip identifier/other expressions because retrieving their 'Unit' property would need points-to analysis.
        return null;
    }

    private static bool AreUnitsStructurallyEqual(ExpressionSyntax left, ExpressionSyntax right, SemanticModel model, CancellationToken token)
    {
        // Compare semantic symbols when both are member access .Unit referencing same instance OR both constant/singleton units
        var leftSymbol = model.GetSymbolInfo(left, token).Symbol;
        var rightSymbol = model.GetSymbolInfo(right, token).Symbol;

        if (leftSymbol != null && rightSymbol != null)
        {
            if (SymbolEqualityComparer.Default.Equals(leftSymbol, rightSymbol))
            {
                return true;
            }
        }

        // Fallback: compare stringified syntax for simple singleton references (e.g., Unit.SI.m)
        var leftText = left.NormalizeWhitespace().ToFullString();
        var rightText = right.NormalizeWhitespace().ToFullString();
        if (leftText == rightText)
        {
            return true;
        }

        return false;
    }
}