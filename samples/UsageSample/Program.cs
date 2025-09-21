using System;

using Veggerby.Units;
using Veggerby.Units.Conversion;
using Veggerby.Units.Quantities; // assuming quantities namespace

// Basic unit composition and equality
var m = Unit.SI.m;
var s = Unit.SI.s;
var speedUnit = m / s;               // m/s
var altSpeedUnit = Unit.SI.m * (Unit.SI.s ^ -1); // also m/s
Console.WriteLine($"Units equal: {speedUnit == altSpeedUnit}");

// Measurement arithmetic
var distance = new DoubleMeasurement(120.0, m); // 120 m
var time = new DoubleMeasurement(10.0, s);      // 10 s
var speed = distance / time;                   // 12 m/s
Console.WriteLine($"Speed: {speed.Value} {speed.Unit}");

// Conversion
var feet = new DoubleMeasurement(1.0, Unit.Imperial.ft);
var inMeters = feet.ConvertTo(m);
Console.WriteLine($"1 ft = {inMeters.Value} m");

// Prefix usage (kilometre)
var km = Prefix.k * m;
var race = new DoubleMeasurement(5, km); // 5 km
var raceMeters = race.ConvertTo(m);
Console.WriteLine($"5 km = {raceMeters.Value} m");

// Temperature (affine)
var c = Temperature.Celsius(25.0);
var f = c.ConvertTo(Unit.Imperial.F);
Console.WriteLine($"25 °C = {f.Value:F2} °F");

// Quantities (Energy vs Torque semantics)
var energy = Quantity.Energy(50.0); // 50 J
var torque = Quantity.Torque(50.0); // 50 J (Torque)
Console.WriteLine($"Energy == Torque kinds? {energy.Kind == torque.Kind}");

// Inference example: Power * Time = Energy
var power = Quantity.Power(250.0); // 250 W
// Create a Time quantity manually (no direct factory in current API) using canonical unit: s
var timeQuantity = Quantity.Time(30.0); // 30 s
if (Quantity<double>.TryMultiply(power, timeQuantity, out var inferredEnergy))
{
    Console.WriteLine($"Inferred energy: {inferredEnergy.Measurement.Value} {inferredEnergy.Measurement.Unit} ({inferredEnergy.Kind.Name})");
}

// Attempting cross-kind addition (will throw)
try
{
    var bad = energy + torque;
}
catch (Exception ex)
{
    Console.WriteLine($"Expected failure: {ex.Message}");
}