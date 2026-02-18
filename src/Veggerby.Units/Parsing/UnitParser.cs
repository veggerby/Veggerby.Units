using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Veggerby.Units.Quantities;

namespace Veggerby.Units.Parsing;

/// <summary>
/// Parses string representations of units into <see cref="Unit"/> objects.
/// Supports SI units, Imperial units, derived units, prefixes, and composite expressions.
/// </summary>
public static class UnitParser
{
    private static readonly Dictionary<string, Unit> _unitRegistry = BuildUnitRegistry();
    private static readonly Dictionary<string, Prefix> _prefixRegistry = BuildPrefixRegistry();

    /// <summary>
    /// Parses a unit expression string into a <see cref="Unit"/>.
    /// </summary>
    /// <param name="expression">The unit expression to parse (e.g., "m", "kg", "m/s", "N·m").</param>
    /// <returns>The parsed unit.</returns>
    /// <exception cref="ParseException">Thrown when the expression cannot be parsed.</exception>
    public static Unit Parse(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ParseException("Unit expression cannot be null or empty.");
        }

        var lexer = new Lexer(expression);
        var tokens = lexer.Tokenize();
        var parser = new Parser(tokens);
        return parser.ParseUnit();
    }

    /// <summary>
    /// Tries to parse a unit expression string into a <see cref="Unit"/>.
    /// </summary>
    /// <param name="expression">The unit expression to parse.</param>
    /// <param name="unit">The parsed unit if successful, otherwise null.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParse(string expression, out Unit unit)
    {
        return TryParse(expression, out unit, out _);
    }

    /// <summary>
    /// Tries to parse a unit expression string into a <see cref="Unit"/>.
    /// </summary>
    /// <param name="expression">The unit expression to parse.</param>
    /// <param name="unit">The parsed unit if successful, otherwise null.</param>
    /// <param name="errorMessage">The error message if parsing failed.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParse(string expression, out Unit unit, out string errorMessage)
    {
        unit = null;
        errorMessage = null;

        try
        {
            unit = Parse(expression);
            return true;
        }
        catch (ParseException ex)
        {
            errorMessage = ex.Message;
            return false;
        }
        catch (Exception ex)
        {
            errorMessage = $"Unexpected error: {ex.Message}";
            return false;
        }
    }

    /// <summary>
    /// Parses a qualified unit expression (e.g., "J (Energy)", "Pa (Pressure)") into a unit and quantity kind.
    /// </summary>
    /// <param name="expression">The qualified unit expression to parse.</param>
    /// <returns>A tuple containing the parsed unit and its quantity kind.</returns>
    /// <exception cref="ParseException">Thrown when the expression cannot be parsed.</exception>
    public static (Unit Unit, QuantityKind Kind) ParseQualified(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            throw new ParseException("Unit expression cannot be null or empty.");
        }

        var lexer = new Lexer(expression);
        var tokens = lexer.Tokenize();
        var parser = new Parser(tokens, qualifiedMode: true);
        return parser.ParseQualifiedUnit();
    }

    /// <summary>
    /// Tries to parse a qualified unit expression into a unit and quantity kind.
    /// </summary>
    /// <param name="expression">The qualified unit expression to parse.</param>
    /// <param name="unit">The parsed unit if successful, otherwise null.</param>
    /// <param name="kind">The parsed quantity kind if successful, otherwise null.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParseQualified(string expression, out Unit unit, out QuantityKind kind)
    {
        return TryParseQualified(expression, out unit, out kind, out _);
    }

    /// <summary>
    /// Tries to parse a qualified unit expression into a unit and quantity kind.
    /// </summary>
    /// <param name="expression">The qualified unit expression to parse.</param>
    /// <param name="unit">The parsed unit if successful, otherwise null.</param>
    /// <param name="kind">The parsed quantity kind if successful, otherwise null.</param>
    /// <param name="errorMessage">The error message if parsing failed.</param>
    /// <returns>True if parsing succeeded, false otherwise.</returns>
    public static bool TryParseQualified(string expression, out Unit unit, out QuantityKind kind, out string errorMessage)
    {
        unit = null;
        kind = null;
        errorMessage = null;

        try
        {
            (unit, kind) = ParseQualified(expression);
            return true;
        }
        catch (ParseException ex)
        {
            errorMessage = ex.Message;
            return false;
        }
        catch (Exception ex)
        {
            errorMessage = $"Unexpected error: {ex.Message}";
            return false;
        }
    }

    private static Dictionary<string, Unit> BuildUnitRegistry()
    {
        var registry = new Dictionary<string, Unit>();

        // SI base units
        registry["m"] = Unit.SI.m;
        registry["g"] = Unit.SI.g;
        registry["kg"] = Unit.SI.kg;
        registry["s"] = Unit.SI.s;
        registry["A"] = Unit.SI.A;
        registry["K"] = Unit.SI.K;
        registry["°C"] = Unit.SI.C;  // Celsius (temperature) - use degree symbol only
        registry["cd"] = Unit.SI.cd;
        registry["mol"] = Unit.SI.n;
        registry["rad"] = Unit.SI.rad;
        registry["sr"] = Unit.SI.sr;

        // Imperial units
        registry["ft"] = Unit.Imperial.ft;
        registry["in"] = Unit.Imperial.@in;
        registry["yd"] = Unit.Imperial.ya;
        registry["mi"] = Unit.Imperial.mi;
        registry["lb"] = Unit.Imperial.lb;
        registry["oz"] = Unit.Imperial.oz;
        registry["°F"] = Unit.Imperial.F;  // Fahrenheit (temperature) - use degree symbol only

        // Common derived SI units (these will be added as we discover them in the codebase)
        // For now, we'll construct them on the fly from their base units
        // N = kg·m/s²
        registry["N"] = Unit.SI.kg * Unit.SI.m / (Unit.SI.s ^ 2);
        // J = N·m = kg·m²/s²
        registry["J"] = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 2);
        // Pa = N/m² = kg/(m·s²)
        registry["Pa"] = Unit.SI.kg / (Unit.SI.m * (Unit.SI.s ^ 2));
        // W = J/s = kg·m²/s³
        registry["W"] = Unit.SI.kg * (Unit.SI.m ^ 2) / (Unit.SI.s ^ 3);
        // V = W/A = kg·m²/(s³·A)
        registry["V"] = Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 3) * Unit.SI.A);
        // Ω = V/A = kg·m²/(s³·A²)
        registry["Ω"] = Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 3) * (Unit.SI.A ^ 2));
        registry["ohm"] = registry["Ω"];
        // Hz = 1/s
        registry["Hz"] = Unit.None / Unit.SI.s;
        // C (Coulomb) = A·s
        registry["C"] = Unit.SI.A * Unit.SI.s;
        // F (Farad) = C/V = s⁴·A²/(kg·m²)
        registry["F"] = (Unit.SI.s ^ 4) * (Unit.SI.A ^ 2) / (Unit.SI.kg * (Unit.SI.m ^ 2));
        // H (Henry) = Wb/A = kg·m²/(s²·A²)
        registry["H"] = Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 2) * (Unit.SI.A ^ 2));
        // S (Siemens) = 1/Ω = s³·A²/(kg·m²)
        registry["S"] = (Unit.SI.s ^ 3) * (Unit.SI.A ^ 2) / (Unit.SI.kg * (Unit.SI.m ^ 2));
        // T (Tesla) = Wb/m² = kg/(s²·A)
        registry["T"] = Unit.SI.kg / ((Unit.SI.s ^ 2) * Unit.SI.A);
        // Wb (Weber) = V·s = kg·m²/(s²·A)
        registry["Wb"] = Unit.SI.kg * (Unit.SI.m ^ 2) / ((Unit.SI.s ^ 2) * Unit.SI.A);
        // lm (lumen) = cd·sr
        registry["lm"] = Unit.SI.cd * Unit.SI.sr;
        // lx (lux) = lm/m²
        registry["lx"] = Unit.SI.cd * Unit.SI.sr / (Unit.SI.m ^ 2);

        return registry;
    }

    private static Dictionary<string, Prefix> BuildPrefixRegistry()
    {
        var registry = new Dictionary<string, Prefix>();

        // SI prefixes (large)
        registry["Y"] = Prefix.Y;
        registry["Z"] = Prefix.Z;
        registry["E"] = Prefix.E;
        registry["P"] = Prefix.P;
        registry["T"] = Prefix.T;
        registry["G"] = Prefix.G;
        registry["M"] = Prefix.M;
        registry["k"] = Prefix.k;
        registry["h"] = Prefix.h;
        registry["da"] = Prefix.da;

        // SI prefixes (small)
        registry["d"] = Prefix.d;
        registry["c"] = Prefix.c;
        registry["m"] = Prefix.m;
        registry["μ"] = Prefix.μ;
        registry["n"] = Prefix.n;
        registry["p"] = Prefix.p;
        registry["f"] = Prefix.f;
        registry["a"] = Prefix.a;
        registry["z"] = Prefix.z;
        registry["y"] = Prefix.y;

        return registry;
    }

    /// <summary>
    /// Internal parser class that processes tokens into units.
    /// </summary>
    internal class Parser
    {
        private readonly List<Token> _tokens;
        private int _current;
        private bool _qualifiedMode;

        internal Parser(List<Token> tokens, bool qualifiedMode = false)
        {
            _tokens = tokens;
            _current = 0;
            _qualifiedMode = qualifiedMode;
        }

        private Token CurrentToken => _tokens[_current];
        private bool IsAtEnd => CurrentToken.Type == TokenType.EndOfInput;

        private Token Advance()
        {
            if (!IsAtEnd)
            {
                _current++;
            }

            return _tokens[_current - 1];
        }

        private bool Match(params TokenType[] types)
        {
            foreach (var type in types)
            {
                if (CurrentToken.Type == type)
                {
                    return true;
                }
            }

            return false;
        }

        private Token Consume(TokenType type, string message)
        {
            if (CurrentToken.Type == type)
            {
                return Advance();
            }

            throw new ParseException(message, CurrentToken.Position);
        }

        internal Unit ParseUnit()
        {
            var result = ParseExpression();

            if (!IsAtEnd)
            {
                throw new ParseException($"Unexpected token '{CurrentToken.Value}' at position {CurrentToken.Position}", CurrentToken.Position);
            }

            return result;
        }

        internal (Unit, QuantityKind) ParseQualifiedUnit()
        {
            var unit = ParseExpression();

            // Optionally parse qualifier: whitespace followed by (KindName)
            QuantityKind kind = null;

            // Check if we have a qualifier pattern: ( <identifier> )
            if (!IsAtEnd && Match(TokenType.LeftParen))
            {
                // Peek ahead to see if this looks like a qualifier (not a complex expression)
                // A qualifier is: ( <single identifier> )
                if (_current + 2 < _tokens.Count &&
                    _tokens[_current + 1].Type == TokenType.Identifier &&
                    _tokens[_current + 2].Type == TokenType.RightParen)
                {
                    // This looks like a qualifier, consume it
                    Advance(); // consume (
                    var kindToken = Advance(); // consume identifier
                    Advance(); // consume )

                    // Parse the kind name
                    kind = QuantityParser.Parse(kindToken.Value);
                }
                else
                {
                    // This is a complex parenthesized expression, let the normal parser handle it
                    // by throwing an error since we already parsed the unit
                    throw new ParseException($"Unexpected token '(' at position {CurrentToken.Position}", CurrentToken.Position);
                }
            }

            if (!IsAtEnd)
            {
                throw new ParseException($"Unexpected token '{CurrentToken.Value}' at position {CurrentToken.Position}", CurrentToken.Position);
            }

            return (unit, kind);
        }

        private Unit ParseExpression()
        {
            return ParseAdditive();
        }

        private Unit ParseAdditive()
        {
            return ParseMultiplicative();
        }

        private Unit ParseMultiplicative()
        {
            var left = ParsePower();

            while (Match(TokenType.Multiply, TokenType.Divide))
            {
                var operatorToken = Advance();

                // Check if next token looks like start of a new term (implicit multiplication)
                if (operatorToken.Type == TokenType.Multiply)
                {
                    var right = ParsePower();
                    left = left * right;
                }
                else if (operatorToken.Type == TokenType.Divide)
                {
                    var right = ParsePower();
                    left = left / right;
                }
            }

            // Handle implicit multiplication (when two identifiers or a closing paren followed by identifier)
            // In qualified mode, skip implicit multiplication for '(' as it may be a qualifier
            while (Match(TokenType.Identifier) || (!_qualifiedMode && Match(TokenType.LeftParen)) && !IsAtEnd)
            {
                var right = ParsePower();
                left = left * right;
            }

            return left;
        }

        private Unit ParsePower()
        {
            var @base = ParsePrimary();

            if (Match(TokenType.Power))
            {
                Advance();
                var exponentToken = Consume(TokenType.Number, "Expected exponent after '^'");

                if (!int.TryParse(exponentToken.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var exponent))
                {
                    throw new ParseException($"Invalid exponent '{exponentToken.Value}'", exponentToken.Position);
                }

                return @base ^ exponent;
            }

            if (Match(TokenType.Superscript))
            {
                var superscriptToken = Advance();

                if (!int.TryParse(superscriptToken.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var exponent))
                {
                    throw new ParseException($"Invalid superscript exponent '{superscriptToken.Value}'", superscriptToken.Position);
                }

                return @base ^ exponent;
            }

            return @base;
        }

        private Unit ParsePrimary()
        {
            if (Match(TokenType.LeftParen))
            {
                Advance();
                var expr = ParseExpression();
                Consume(TokenType.RightParen, "Expected ')' after expression");
                return expr;
            }

            if (Match(TokenType.Identifier))
            {
                var identifierToken = Advance();
                
                // Check if there's a power operator following this identifier
                // If so, and if the identifier can be split into units, we need to handle this carefully
                // The convention is that "kgm^2" means "kg * m^2", not "(kg * m)^2"
                if (Match(TokenType.Power, TokenType.Superscript))
                {
                    // Try to split the identifier and apply power only to the last part
                    var splitResult = TrySplitForPower(identifierToken.Value, identifierToken.Position);
                    if (splitResult.HasValue)
                    {
                        var (prefix, suffix) = splitResult.Value;
                        
                        // Get the exponent
                        int exponent;
                        if (Match(TokenType.Power))
                        {
                            Advance();
                            var exponentToken = Consume(TokenType.Number, "Expected exponent after '^'");
                            if (!int.TryParse(exponentToken.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out exponent))
                            {
                                throw new ParseException($"Invalid exponent '{exponentToken.Value}'", exponentToken.Position);
                            }
                        }
                        else // Superscript
                        {
                            var superscriptToken = Advance();
                            if (!int.TryParse(superscriptToken.Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out exponent))
                            {
                                throw new ParseException($"Invalid superscript exponent '{superscriptToken.Value}'", superscriptToken.Position);
                            }
                        }
                        
                        // Return prefix * (suffix ^ exponent)
                        return prefix * (suffix ^ exponent);
                    }
                }
                
                return ResolveUnit(identifierToken.Value, identifierToken.Position);
            }

            if (Match(TokenType.Number))
            {
                var numberToken = Advance();
                
                // Special case: "1" represents dimensionless unit (Unit.None)
                // This is used in expressions like "1/s" for Hz
                if (numberToken.Value == "1")
                {
                    return Unit.None;
                }
                
                throw new ParseException($"Unexpected number '{numberToken.Value}' in unit expression", numberToken.Position);
            }

            throw new ParseException($"Expected unit identifier or '(' but found '{CurrentToken.Value}'", CurrentToken.Position);
        }

        private (Unit prefix, Unit suffix)? TrySplitForPower(string symbol, int position)
        {
            // Try to split the symbol such that we can apply power to just the last part
            // Strategy: Try shortest valid suffix first (rightmost unit gets the exponent)
            // This handles "kgm^2" -> kg * (m^2) correctly
            // Start from the right and look for the shortest unit that can take the exponent
            for (int splitAt = symbol.Length - 1; splitAt >= 1; splitAt--)
            {
                var leftPart = symbol.Substring(0, splitAt);
                var rightPart = symbol.Substring(splitAt);
                
                // Try to resolve right part as a simple unit (not prefixed, to avoid ambiguity)
                Unit rightUnit = null;
                if (_unitRegistry.TryGetValue(rightPart, out rightUnit))
                {
                    // Found a valid suffix, now try to resolve the left part
                    var leftUnit = TryResolveUnitForSplit(leftPart);
                    if (leftUnit != null)
                    {
                        return (leftUnit, rightUnit);
                    }
                }
            }
            
            return null;
        }

        private Unit TryResolveUnitForSplit(string symbol)
        {
            // Try as a direct unit
            if (_unitRegistry.TryGetValue(symbol, out var unit))
            {
                return unit;
            }

            // Try recursive implicit product split first (prefer multiplication over prefixing)
            // This ensures "kgm" splits as "kg * m" not "k * gm"
            var splitProduct = TrySplitImplicitProduct(symbol, 0);
            if (splitProduct != null)
            {
                return splitProduct;
            }

            // Only try as a prefixed unit if implicit product splitting failed
            foreach (var prefixLength in new[] { 2, 1 })
            {
                if (symbol.Length > prefixLength)
                {
                    var prefixPart = symbol.Substring(0, prefixLength);
                    var unitPart = symbol.Substring(prefixLength);

                    if (_prefixRegistry.TryGetValue(prefixPart, out var prefix) &&
                        _unitRegistry.TryGetValue(unitPart, out var baseUnit))
                    {
                        return prefix * baseUnit;
                    }
                }
            }

            return null;
        }

        private Unit ResolveUnit(string symbol, int position)
        {
            // First, check if it's a known unit
            if (_unitRegistry.TryGetValue(symbol, out var unit))
            {
                return unit;
            }

            // Check if it's a prefixed unit
            // Try longest prefix first
            foreach (var prefixLength in new[] { 2, 1 })
            {
                if (symbol.Length > prefixLength)
                {
                    var prefixPart = symbol.Substring(0, prefixLength);
                    var unitPart = symbol.Substring(prefixLength);

                    if (_prefixRegistry.TryGetValue(prefixPart, out var prefix) &&
                        _unitRegistry.TryGetValue(unitPart, out var baseUnit))
                    {
                        return prefix * baseUnit;
                    }
                }
            }

            // Try splitting as implicit multiplication of consecutive units (e.g., "kgm" -> "kg" * "m")
            // This handles base factor format output like "kgm^2/s^2"
            var implicitProduct = TrySplitImplicitProduct(symbol, position);
            if (implicitProduct != null)
            {
                return implicitProduct;
            }

            throw new ParseException($"Unknown unit symbol '{symbol}'", position);
        }

        private Unit TrySplitImplicitProduct(string symbol, int position)
        {
            // Try all possible split points to find valid unit combinations
            // Prefer longest matches first (greedy left-to-right)
            for (int splitAt = symbol.Length - 1; splitAt >= 1; splitAt--)
            {
                var leftPart = symbol.Substring(0, splitAt);
                var rightPart = symbol.Substring(splitAt);

                // Try to resolve left part (may be prefixed)
                Unit leftUnit = null;
                if (_unitRegistry.TryGetValue(leftPart, out leftUnit))
                {
                    // Found left part as a unit, try to resolve right recursively
                    var rightUnit = TryResolveUnitForSplit(rightPart);
                    if (rightUnit != null)
                    {
                        return leftUnit * rightUnit;
                    }
                }
                else
                {
                    // Try left part as a prefixed unit
                    foreach (var prefixLength in new[] { 2, 1 })
                    {
                        if (leftPart.Length > prefixLength)
                        {
                            var prefixPart = leftPart.Substring(0, prefixLength);
                            var unitPart = leftPart.Substring(prefixLength);

                            if (_prefixRegistry.TryGetValue(prefixPart, out var prefix) &&
                                _unitRegistry.TryGetValue(unitPart, out var baseUnit))
                            {
                                leftUnit = prefix * baseUnit;
                                var rightUnit = TryResolveUnitForSplit(rightPart);
                                if (rightUnit != null)
                                {
                                    return leftUnit * rightUnit;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
