using System;

using Veggerby.Units;
using Veggerby.Units.Conversion;

// This project is intended to demonstrate analyzer diagnostics.
// Build in an IDE with analyzers enabled to see squiggles and apply fixes.

var length = new DoubleMeasurement(5, Unit.SI.m);
var time = new DoubleMeasurement(3, Unit.SI.s);
// VUNITS001: incompatible unit addition (intentional)
var invalid = length + time; // Expect analyzer error; code fix: convert time

var energy = new DoubleMeasurement(1, Unit.SI.kg * Unit.SI.m * Unit.SI.m / (Unit.SI.s ^ 2));
// VUNITS002: ambiguous formatting (intentional)
var s = energy.ToString(); // Expect info diagnostic; code fix: add UnitFormat.Qualified

Console.WriteLine("Analyzer sample executed (diagnostics may have prevented build if not fixed).");