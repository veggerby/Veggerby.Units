using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

using AwesomeAssertions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

using Veggerby.Units.Analyzers;

using Xunit;

namespace Veggerby.Units.Analyzers.Tests;

public class VUNITS002CodeFixTests
{
    private static async Task<(string NewText, ImmutableArray<Diagnostic> Diagnostics)> RunAndFixAsync(string source)
    {
        _action = null;
        var tree = CSharpSyntaxTree.ParseText(source);
        var refs = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Measurement<int>).Assembly.Location)
        };
        var compilation = CSharpCompilation.Create(
            assemblyName: "T",
            syntaxTrees: new[] { tree },
            references: refs,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var analyzer = new VUNITS002Analyzer();
        var diags = await compilation.WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(analyzer)).GetAnalyzerDiagnosticsAsync();
        var diag = diags.FirstOrDefault(d => d.Id == "VUNITS002");
        diag.Should().NotBeNull();

        var workspace = new AdhocWorkspace();
        var solution = workspace.CurrentSolution;
        var projectId = ProjectId.CreateNewId();
        solution = solution.AddProject(ProjectInfo.Create(projectId, VersionStamp.Create(), "P", "P", LanguageNames.CSharp)
            .WithCompilationOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .WithMetadataReferences(refs));
        var documentId = DocumentId.CreateNewId(projectId);
        solution = solution.AddDocument(documentId, "Test.cs", source);
        var doc = solution.GetDocument(documentId);

        doc.Should().NotBeNull();
        var fixProvider = new VUNITS002CodeFixProvider();
        var context = new CodeFixContext(doc!, diag, (a, d) => _action ??= a, default);
        await fixProvider.RegisterCodeFixesAsync(context);
        _action.Should().NotBeNull();
        var operations = await _action.GetOperationsAsync(default);
        var apply = operations.OfType<ApplyChangesOperation>().Single();
        var newDoc = apply.ChangedSolution.GetDocument(documentId);
        newDoc.Should().NotBeNull();
        var newText = await newDoc!.GetTextAsync();
        return (newText.ToString(), diags);
    }

    private static CodeAction? _action;

    [Fact]
    public async Task GivenAmbiguousFormatting_WhenFixApplied_ThenQualifiedArgumentAdded()
    {
        const string src = @"using Veggerby.Units;class T{void M(){var a=new Int32Measurement(1,Unit.SI.m * Unit.SI.m * Unit.SI.kg / (Unit.SI.s * Unit.SI.s));var s=a.ToString();}}";
        var (newText, _) = await RunAndFixAsync(src);
        newText.Should().Contain("ToString(UnitFormat.Qualified)");
    }
}