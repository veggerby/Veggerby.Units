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
/// Code fix provider for diagnostic <c>VUNITS002</c> that ensures calls to unit
/// formatting APIs specify <c>UnitFormat.Qualified</c> explicitly to avoid
/// ambiguities and improve code readability.
/// </summary>
/// <remarks>
/// The analyzer flags invocations where a formatting method omits an argument
/// selecting the qualified unit format. This provider appends the
/// argument if it is not already present.
/// </remarks>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(VUNITS002CodeFixProvider)), Shared]
public sealed class VUNITS002CodeFixProvider : CodeFixProvider
{
    private const string Title = "Specify UnitFormat.Qualified";

    /// <summary>
    /// Gets the diagnostic IDs this provider can fix (only <c>VUNITS002</c>).
    /// </summary>
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(VUNITS002_Descriptor.DiagnosticId);

    /// <summary>
    /// Returns the batch fixer enabling Fix All operations.
    /// </summary>
    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    /// <summary>
    /// Registers the code fix that inserts <c>UnitFormat.Qualified</c> as the
    /// final argument when it is missing.
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

        if (root.FindNode(diagnostic.Location.SourceSpan) is not InvocationExpressionSyntax invocation)
        {
            return;
        }

        if (invocation.ArgumentList.Arguments.Any(a => a.Expression is MemberAccessExpressionSyntax maes && maes.Name.Identifier.Text == "Qualified"))
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                Title,
                ct => ApplyAsync(context.Document, root, invocation, ct),
                equivalenceKey: Title),
            diagnostic);
    }

    private static Task<Document> ApplyAsync(Document document, SyntaxNode root, InvocationExpressionSyntax invocation, CancellationToken ct)
    {
        var argList = invocation.ArgumentList ?? SyntaxFactory.ArgumentList();
        var unitFormatId = SyntaxFactory.IdentifierName("UnitFormat");
        var qualifiedId = SyntaxFactory.IdentifierName("Qualified");
        var memberAccess = SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, unitFormatId, qualifiedId);
        var newArg = SyntaxFactory.Argument(memberAccess);
        var newArgs = argList.Arguments.Add(newArg);
        var newArgList = argList.WithArguments(newArgs);
        var newInvocation = invocation.WithArgumentList(newArgList);
        var newRoot = root.ReplaceNode(invocation, newInvocation);
        return Task.FromResult(document.WithSyntaxRoot(newRoot));
    }
}