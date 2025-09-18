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
/// Code fix for VUNITS001: rewrites an addition/subtraction of mismatched units by converting the right operand
/// to the left operand's unit via <c>ConvertTo(left.Unit)</c> when both operands expose a <c>Unit</c> property.
/// Conservative: only offered when a safe transformation pattern is detected.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(VUNITS001CodeFixProvider)), Shared]
public sealed class VUNITS001CodeFixProvider : CodeFixProvider
{
    private const string Title = "Convert right operand to left unit";

    /// <inheritdoc />
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(VUNITS001_Descriptor.DiagnosticId);

    /// <inheritdoc />
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    /// <inheritdoc />
    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.FirstOrDefault();
        if (diagnostic == null)
        {
            return;
        }

        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root == null)
        {
            return;
        }

        var node = root.FindNode(diagnostic.Location.SourceSpan) as BinaryExpressionSyntax;
        if (node == null)
        {
            return;
        }

        // We require left to expose .Unit and right not already converted.
        var leftUnitAccess = TryGetUnitAccess(node.Left);
        if (leftUnitAccess == null)
        {
            return; // cannot safely construct conversion call
        }

        // Avoid duplicate suggestion if right already has ConvertTo(...)
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
        // Patterns: <identifier>.Unit, (<expr>).Unit
        if (expression is MemberAccessExpressionSyntax mae && mae.Name.Identifier.Text == "Unit")
        {
            return mae;
        }

        if (expression is ParenthesizedExpressionSyntax par && par.Expression is MemberAccessExpressionSyntax inner && inner.Name.Identifier.Text == "Unit")
        {
            return inner;
        }

        return null;
    }

    private static bool IsConvertToInvocation(ExpressionSyntax expression)
    {
        if (expression is InvocationExpressionSyntax inv && inv.Expression is MemberAccessExpressionSyntax mae)
        {
            if (mae.Name.Identifier.Text == "ConvertTo")
            {
                return true;
            }
        }
        return false;
    }

    private static Task<Document> ApplyConversionAsync(Document document, SyntaxNode root, BinaryExpressionSyntax binary, MemberAccessExpressionSyntax leftUnit, CancellationToken ct)
    {
        // Build: right.ConvertTo(left.Unit)
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