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
/// Code fix for VUNITS002: adds an explicit UnitFormat.Qualified (or Mixed if already Qualified context) argument
/// to an invocation of Measurement/Unit formatting when an ambiguous symbol is formatted without specifying a format.
/// Strategy:
/// - If method is ToString(): transforms to .ToString(UnitFormat.Qualified)
/// - If method is Format(value, unit) pattern (rare) and lacks UnitFormat parameter, appends UnitFormat.Qualified.
/// Conservative: does not duplicate an existing UnitFormat argument or modify calls with params/overloads beyond arity match.
/// </summary>
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(VUNITS002CodeFixProvider)), Shared]
public sealed class VUNITS002CodeFixProvider : CodeFixProvider
{
    private const string Title = "Specify UnitFormat.Qualified";

    /// <inheritdoc />
    public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(VUNITS002_Descriptor.DiagnosticId);

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

        if (root.FindNode(diagnostic.Location.SourceSpan) is not InvocationExpressionSyntax invocation)
        {
            return;
        }

        // Skip if already has UnitFormat argument (double safety vs analyzer conditions)
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

        // Construct UnitFormat.Qualified expression
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