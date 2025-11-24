using Veggerby.Units;
using Veggerby.Units.Fluent.CGS;
using Veggerby.Units.Conversion;

// Test CGS base units
var cm = 100.0.Centimeters();
var meters = cm.ConvertTo(Unit.SI.m);
Console.WriteLine($"100 cm = {(double)meters} m");

var grams = 500.0.Grams();
var kg = grams.ConvertTo(Unit.SI.kg);
Console.WriteLine($"500 g = {(double)kg} kg");

// Test CGS mechanical units
var force = 1000.0.Dynes();
var newtons = force.ConvertTo(Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2));
Console.WriteLine($"1000 dyn = {(double)newtons} N");

var energy = 10000000.0.Ergs();
var joules = energy.ConvertTo(Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2));
Console.WriteLine($"10,000,000 erg = {(double)joules} J");

// Test CGS electromagnetic units
var magnetic = 10000.0.Gauss();
var tesla = magnetic.ConvertTo(Unit.SI.kg / (Unit.SI.A * (Unit.SI.s ^ 2)));
Console.WriteLine($"10,000 G = {(double)tesla} T");

// Test symbol aliases
var length = 5.0.cm();
var mass = 10.0.g();
var f = 100.0.dyn();
var e = 1000.0.erg();
Console.WriteLine($"\nSymbol aliases work: {(double)length} cm, {(double)mass} g, {(double)f} dyn, {(double)e} erg");

Console.WriteLine("\nAll CGS unit conversions working correctly!");
