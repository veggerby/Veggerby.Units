using System.Collections.Immutable;

using AwesomeAssertions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

using Veggerby.Units.Analyzers;

using Xunit;

namespace Veggerby.Units.Analyzers.Tests;

public class VUNITS002AdditionalTests
{
    private static ImmutableArray<Diagnostic> Run(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var refs = new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(Measurement<int>).Assembly.Location) };
        var compilation = CSharpCompilation.Create("TestAsm", new[] { syntaxTree }, refs, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var analyzers = ImmutableArray.Create<DiagnosticAnalyzer>(new VUNITS002Analyzer());
        return compilation.WithAnalyzers(analyzers).GetAnalyzerDiagnosticsAsync().Result;
    }

    [Fact]
    public void GivenBaseFactorsFormattingOnAmbiguousUnit_ThenNoDiagnostic()
    {
        // Using explicit BaseFactors path via Format extension should suppress ambiguity diagnostic
        const string src = @"using Veggerby.Units;using Veggerby.Units.Formatting;class T{void M(){var e=new Int32Measurement(1,Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s));var s=e.Format(UnitFormat.BaseFactors);}}";
        var diags = Run(src);
        diags.Should().BeEmpty();
    }

    [Fact]
    public void GivenQualifiedFormattingOnAmbiguousUnit_ThenNoDiagnostic()
    {
        const string src = @"using Veggerby.Units;using Veggerby.Units.Formatting;class T{void M(){var e=new Int32Measurement(1,Unit.SI.m*Unit.SI.m*Unit.SI.kg/(Unit.SI.s*Unit.SI.s));var s=e.Format(UnitFormat.Qualified);}}";
        var diags = Run(src);
        diags.Should().BeEmpty();
    }
}