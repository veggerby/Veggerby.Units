using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Veggerby.Units.Analyzers;

/// <summary>
/// Code fix provider for diagnostic <c>VUNITS001</c> that offers to insert a
/// <c>.ConvertTo(left.Unit)</c> call on the right-hand side of a binary operation
/// when two <c>Measurement&lt;T&gt;</c> instances of differing units are combined.
/// </summary>
/// <remarks>
/// The diagnostic is reported by the analyzer when an addition or subtraction
/// occurs between two measurements with different units. This provider rewrites
/// the right operand so that its value is explicitly converted to the left
/// operand's unit, preserving intent and dimensional correctness.
/// </remarks>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(VUNITS001CodeFixProvider)), Shared]
public sealed class VUNITS001CodeFixProvider : CodeFixProvider
{
    private const string Title = "Convert right operand to left unit";

    /// <summary>
    /// Gets the set of diagnostic IDs this provider can fix (only <c>VUNITS001</c>).
    /// </summary>
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(VUNITS001_Descriptor.DiagnosticId);

    /// <summary>
    /// Returns the standard batch fixer so multiple occurrences can be fixed together.
    /// </summary>
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    /// <summary>
    /// Registers the code fix that inserts a <c>ConvertTo(left.Unit)</c> invocation
    /// on the right-hand side expression when applicable.
    /// </summary>
    /// <param name="context">The Roslyn code fix context.</param>
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.FirstOrDefault();
        if (diagnostic is null)
        {
            return;
        }

        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root is null)
        {
            return;
        }

        if (root.FindNode(diagnostic.Location.SourceSpan) is not BinaryExpressionSyntax node)
        {
            return;
        }

        var leftUnitAccess = TryGetUnitAccess(node.Left);
        if (leftUnitAccess is null)
        {
            return;
        }

        if (IsConvertToInvocation(node.Right))
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                Title,
                ct => ApplyConversionAsync(context.Document, root, node, leftUnitAccess, ct),
                equivalenceKey: Title),
            diagnostic);
    }

    private static MemberAccessExpressionSyntax? TryGetUnitAccess(ExpressionSyntax expression)
    {
        if (expression is MemberAccessExpressionSyntax mae && mae.Name.Identifier.Text == "Unit")
        {
            return mae;
        }

        if (expression is ParenthesizedExpressionSyntax par && par.Expression is MemberAccessExpressionSyntax inner && inner.Name.Identifier.Text == "Unit")
        {
            return inner;
        }

        if (expression is IdentifierNameSyntax id)
        {
            return SyntaxFactory.MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                id,
                SyntaxFactory.IdentifierName("Unit"));
        }

        return null;
    }

    private static bool IsConvertToInvocation(ExpressionSyntax expression)
    {
        return expression is InvocationExpressionSyntax inv && inv.Expression is MemberAccessExpressionSyntax mae && mae.Name.Identifier.Text == "ConvertTo";
    }

    private static Task<Document> ApplyConversionAsync(Document document, SyntaxNode root, BinaryExpressionSyntax binary, MemberAccessExpressionSyntax leftUnit, CancellationToken ct)
    {
        var convertIdentifier = SyntaxFactory.IdentifierName("ConvertTo");
        var memberAccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, binary.Right.WithoutTrailingTrivia(), convertIdentifier);
        var argument = SyntaxFactory.Argument(leftUnit.WithoutLeadingTrivia());
        var argList = SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(new[] { argument }));
        var invocation = SyntaxFactory.InvocationExpression(memberAccess, argList)
            .WithTrailingTrivia(binary.Right.GetTrailingTrivia())
            .WithLeadingTrivia(binary.Right.GetLeadingTrivia());

        var newBinary = binary.WithRight(invocation);
        var newRoot = root.ReplaceNode(binary, newBinary);
        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }
}