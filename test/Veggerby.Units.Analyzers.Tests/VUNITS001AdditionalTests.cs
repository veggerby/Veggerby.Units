using System.Collections.Immutable;

using AwesomeAssertions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

using Veggerby.Units.Analyzers;

using Xunit;

namespace Veggerby.Units.Analyzers.Tests;

public class VUNITS001AdditionalTests
{
    private static ImmutableArray<Diagnostic> Run(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var refs = new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(Measurement<int>).Assembly.Location) };
        var compilation = CSharpCompilation.Create("TestAsm", new[] { syntaxTree }, refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(new VUNITS001Analyzer());
        return compilation.WithAnalyzers(analyzers).GetAnalyzerDiagnosticsAsync().Result;
    }

    [Fact]
    public void GivenDifferentUnitsSubtraction_WhenNoConversion_ThenDiagnostic()
    {
        const string src = @"using Veggerby.Units;class T{void M(){var a=new Int32Measurement(5,Unit.SI.m);var b=new Int32Measurement(2,Unit.SI.s);var c=a-b;}}";
        var diags = Run(src);
        diags.Should().Contain(d => d.Id == "VUNITS001");
    }

    [Fact]
    public void GivenManualConversionPriorToAddition_WhenUnitsMatch_ThenNoDiagnostic()
    {
        // Simulate user converting b into metres before addition (pretend simple scaling by prefix) so expression units match
        const string src = @"using Veggerby.Units;class T{Unit U(Unit u)=>u;void M(){var a=new Int32Measurement(5,Unit.SI.m);var b=new Int32Measurement(2,Unit.SI.m);var c=a+ b;}}";
        var diags = Run(src);
        diags.Should().BeEmpty();
    }
}