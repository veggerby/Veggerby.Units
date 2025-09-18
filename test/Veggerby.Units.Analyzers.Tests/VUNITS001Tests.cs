using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Veggerby.Units.Analyzers;
using Xunit;
using AwesomeAssertions;

namespace Veggerby.Units.Analyzers.Tests;

public class VUNITS001Tests
{
    private static (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) Run(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var refs = new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(Measurement<int>).Assembly.Location) };
        var compilation = CSharpCompilation.Create("TestAsm", new[] { syntaxTree }, refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var analyzer = new VUNITS001Analyzer();
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(analyzer);
        var compilationWithAnalyzers = compilation.WithAnalyzers(analyzers);
        var diags = compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().Result;
        return (compilation, diags);
    }

    [Fact]
    public void GivenDifferentUnitsAddition_WhenNoConversion_ThenDiagnostic()
    {
        // Arrange
        const string src = @"using Veggerby.Units;class T{void M(){var a=new Measurement<int>(1,Unit.SI.m);var b=new Measurement<int>(2,Unit.SI.s);var c=a+b;}}";

        // Act
        var (_, diags) = Run(src);

        // Assert
        diags.Should().Contain(d => d.Id == VUNITS001_Descriptor.DiagnosticId);
    }

    [Fact]
    public void GivenSameUnitsAddition_WhenSafe_ThenNoDiagnostic()
    {
        // Arrange
        const string src = @"using Veggerby.Units;class T{void M(){var a=new Measurement<int>(1,Unit.SI.m);var b=new Measurement<int>(2,Unit.SI.m);var c=a+b;}}";

        // Act
        var (_, diags) = Run(src);

        // Assert
        diags.Should().BeEmpty();
    }
}
