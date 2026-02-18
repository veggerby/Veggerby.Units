# Parsing & Round-Trip Serialization

This document describes the parsing capabilities for reversing formatter output back into `Unit` and `QuantityKind` instances.

## Overview

The parsing layer supports full round-trip serialization for all formatting modes:
- **BaseFactors**: Raw base unit composition with concatenation (`kgm^2/s^2` - no separators, uses `^` for exponents)
- **DerivedSymbols**: Recognized derived SI symbols (`J`, `Pa`, `W`)
- **Mixed**: Optimal token substitution with separators (`N·m` for torque)
- **Qualified**: Disambiguated symbols (`J (Energy)`, `Pa (Pressure)`)

## Basic Unit Parsing

### UnitParser.Parse(string)

Parses standard unit expressions including:
- Base SI units: `m`, `kg`, `s`, `A`, `K`, `mol`, `cd`
- Derived units: `N`, `J`, `Pa`, `W`, `V`, `Ω`, `Hz`, `H`, `T`, `Wb`, `lm`, `lx`
- Prefixed units: `km`, `mm`, `μm`, `kW`, `MHz`
- Composite expressions: `m/s`, `kg·m²/s²`, `N·m`
- Power notation: `m^2`, `s^-1`, `m²` (superscripts)
- Dimensionless numerators: `1/s` (for Hz)

**Examples:**
```csharp
var meter = UnitParser.Parse("m");                    // SI.m
var velocity = UnitParser.Parse("m/s");               // SI.m / SI.s
var energy = UnitParser.Parse("J");                   // Joule
var frequency = UnitParser.Parse("Hz");               // 1/s
var torque = UnitParser.Parse("N·m");                 // Newton-meter
```

### TryParse

Safe parsing with error handling:
```csharp
if (UnitParser.TryParse("kW", out var kilowatt, out var error))
{
    // Use kilowatt
}
else
{
    Console.WriteLine($"Parse error: {error}");
}
```

## Qualified Parsing

### UnitParser.ParseQualified(string)

Parses qualified expressions that include quantity kind disambiguation:

**Examples:**
```csharp
var (unit1, kind1) = UnitParser.ParseQualified("J (Energy)");
// unit1 = Joule, kind1 = QuantityKinds.Energy

var (unit2, kind2) = UnitParser.ParseQualified("J (Torque)");
// unit2 = Joule, kind2 = QuantityKinds.Torque

var (unit3, kind3) = UnitParser.ParseQualified("Pa (Pressure)");
// unit3 = Pascal, kind3 = QuantityKinds.Pressure

var (unit4, kind4) = UnitParser.ParseQualified("N");
// unit4 = Newton, kind4 = null (no qualifier)
```

### TryParseQualified

Safe qualified parsing:
```csharp
if (UnitParser.TryParseQualified("W (Power)", out var unit, out var kind, out var error))
{
    // Use unit and kind
}
```

## Quantity Kind Parsing

### QuantityParser.Parse(string)

Maps quantity kind names to `QuantityKind` instances:

**Examples:**
```csharp
var energy = QuantityParser.Parse("Energy");          // QuantityKinds.Energy
var torque = QuantityParser.Parse("Torque");          // QuantityKinds.Torque
var pressure = QuantityParser.Parse("Pressure");      // QuantityKinds.Pressure
```

### Supported Quantity Kinds

All ~189 quantity kinds are supported, including:
- **Mechanics**: Energy, Work, Heat, Torque, Force, Pressure, Stress, Power
- **Electromagnetics**: Voltage, Current, Resistance, Capacitance, Inductance
- **Thermodynamics**: Entropy, Enthalpy, HeatCapacity
- **Optics**: Luminous flux, Illuminance, Radiance
- And many more...

Use `QuantityParser.GetAllKindNames()` to retrieve the complete list.

## Round-Trip Examples

### BaseFactors Format
```csharp
var unit = QuantityKinds.Energy.CanonicalUnit;
var formatted = UnitFormatter.Format(unit, UnitFormat.BaseFactors);
// formatted = "kgm^2/s^2"

var parsed = UnitParser.Parse(formatted);
// parsed dimensionally equivalent to unit
```

### DerivedSymbols Format
```csharp
var unit = QuantityKinds.Power.CanonicalUnit;
var formatted = UnitFormatter.Format(unit, UnitFormat.DerivedSymbols);
// formatted = "W"

var parsed = UnitParser.Parse(formatted);
// parsed == unit
```

### Qualified Format
```csharp
var unit = QuantityKinds.Energy.CanonicalUnit;
var kind = QuantityKinds.Energy;
var formatted = UnitFormatter.Format(unit, UnitFormat.Qualified, kind);
// formatted = "J (Energy)"

var (parsedUnit, parsedKind) = UnitParser.ParseQualified(formatted);
// parsedUnit == unit, parsedKind == kind
```

### Ambiguous Symbol Handling
```csharp
// J is ambiguous (Energy, Work, Heat, Torque)
var formatted1 = UnitFormatter.Format(joule, UnitFormat.Qualified, QuantityKinds.Energy);
// "J (Energy)"

var formatted2 = UnitFormatter.Format(joule, UnitFormat.Qualified, QuantityKinds.Torque);
// "J (Torque)" or "N·m" depending on mixed mode

var (u1, k1) = UnitParser.ParseQualified("J (Energy)");
var (u2, k2) = UnitParser.ParseQualified("J (Torque)");
// Correctly distinguishes between semantic meanings
```

## Format-Specific Notes

### BaseFactors Limitations
- BaseFactors format concatenates units without separators (ProductUnit.Symbol joins with empty string)
- Examples: `kgm^2/s^2` (not `kg·m²/s²`), uses `^` notation (not superscripts)
- Concatenated units (e.g., `kgm^2`) may parse with different structure than originally created
- Dimensional equivalence is preserved but structural equality may differ
- Parser uses smart splitting to apply exponents to the rightmost unit segment

### Qualified Mode
- Qualifiers are optional; unqualified units parse normally
- Invalid qualifiers throw `ParseException`
- Lookahead prevents qualifiers from being interpreted as unit expressions

### Supported Derived Units
The parser recognizes these derived SI units:
- Mechanical: `N` (Newton), `J` (Joule), `Pa` (Pascal), `W` (Watt)
- Electrical: `V` (Volt), `Ω` (Ohm), `A` (Ampere), `C` (Coulomb)
- Magnetic: `H` (Henry), `T` (Tesla), `Wb` (Weber), `F` (Farad), `S` (Siemens)
- Optical: `lm` (lumen), `lx` (lux)
- Frequency: `Hz` (Hertz)

## Error Handling

All parsing methods throw `ParseException` on invalid input:
```csharp
try
{
    var unit = UnitParser.Parse("xyz");  // Unknown symbol
}
catch (ParseException ex)
{
    Console.WriteLine(ex.Message);  // "Unknown unit symbol 'xyz'"
}
```

Use `TryParse` methods for safe parsing without exceptions.

## Performance Considerations

- Parser uses direct lookup for known units (O(1))
- Implicit multiplication splitting has worst-case O(n²) for length n
- Qualified mode adds minimal overhead (single lookahead)
- Recommended: Cache parsed units for frequently used expressions

## See Also

- `docs/format-mixed-and-ambiguity.md` - Formatting algorithm details
- `docs/serialization.md` - Full serialization examples
- `docs/quantities.md` - Quantity kind overview
