#!/usr/bin/env dotnet-script
// Usage: dotnet script extract-benchmark-summary.csx -- <inputMarkdown> <outputJson>
#r "nuget: Newtonsoft.Json, 13.0.3"
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

if (Args.Count < 2)
{
    Console.Error.WriteLine("Usage: extract-benchmark-summary.csx <inputMarkdown> <outputJson>");
    Environment.Exit(1);
}
var input = Args[0];
var output = Args[1];
var text = File.ReadAllText(input);
// BenchmarkDotNet table rows match pattern: | Method | Mean | Error | StdDev | Gen0 | Allocated |
var lines = text.Split('\n');
var map = new JObject();
foreach (var line in lines)
{
    if (!line.StartsWith("| ")) { continue; }
    var cells = line.Split('|').Select(c => c.Trim()).Where(c => c.Length > 0).ToArray();
    if (cells.Length < 3) { continue; }
    var method = cells[0];
    if (!method.StartsWith("Equality_") && !method.StartsWith("UnitEquality") && !method.StartsWith("DivisionCancellation_")) { continue; }
    // Mean cell like "123.45 ns"; Allocation may appear later (look for last cell with ' B')
    double ParseMean(string s)
    {
        var m = Regex.Match(s, "([0-9]+(\\.[0-9]+)?)");
        return m.Success ? double.Parse(m.Groups[1].Value) : 0d;
    }
    long ParseAlloc(string s)
    {
        var m = Regex.Match(s, "([0-9]+) B");
        return m.Success ? long.Parse(m.Groups[1].Value) : 0L;
    }
    double meanNs = 0;
    long allocB = 0;
    foreach (var c in cells)
    {
        if (meanNs == 0 && c.Contains(" ns")) { meanNs = ParseMean(c); continue; } // capture first (Mean) only
        if (c.Contains(" B")) { allocB = ParseAlloc(c); }
    }
    var obj = new JObject();
    obj["MeanNs"] = meanNs;
    obj["AllocB"] = allocB;
    map[method] = obj;
}
File.WriteAllText(output, map.ToString());
Console.WriteLine($"Wrote summary {output} with {map.Properties().Count()} benchmarks.");
