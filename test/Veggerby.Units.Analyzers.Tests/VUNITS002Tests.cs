using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Veggerby.Units.Analyzers;
using Xunit;
using AwesomeAssertions;

namespace Veggerby.Units.Analyzers.Tests;

public class VUNITS002Tests
{
    private static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) Run(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var refs = new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(Measurement<int>).Assembly.Location) };
        var compilation = CSharpCompilation.Create("TestAsm", new[] { syntaxTree }, refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var analyzer = new VUNITS002Analyzer();
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(analyzer);
        var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);
        var diags = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;
        return (compilation, diags);
    }

    [Fact]
    public void GivenAmbiguousSymbolFormattingWithoutUnitFormat_ThenDiagnostic()
    {
        // Arrange (Joule dimension ambiguous: Energy/Torque etc.)
        const string src = @"using Veggerby.Units;class T{void M(){var e=new Measurement<int>(1,Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s));var s=e.ToString();}}";

        // Act
        var (_, diags) = Run(src);

        // Assert
        diags.Should().Contain(d => d.Id == VUNITS002_Descriptor.DiagnosticId);
    }

    [Fact]
    public void GivenFormattingWithExplicitUnitFormat_ThenNoDiagnostic()
    {
        // Arrange
        const string src = @"using Veggerby.Units;using Veggerby.Units.Formatting;class T{void M(){var e=new Measurement<int>(1,Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s));var s=e.Format(UnitFormat.Qualified);}}";

        // Act
        var (_, diags) = Run(src);

        // Assert
        diags.Should().BeEmpty();
    }
}
