using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        registry["°C"] = Unit.SI.C;
        registry["C"] = Unit.SI.C;
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
        registry["°F"] = Unit.Imperial.F;
        registry["F"] = Unit.Imperial.F;

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
        registry["Cb"] = Unit.SI.A * Unit.SI.s;

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

        internal Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _current = 0;
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
            while (Match(TokenType.Identifier, TokenType.LeftParen) && !IsAtEnd)
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
                return ResolveUnit(identifierToken.Value, identifierToken.Position);
            }

            if (Match(TokenType.Number))
            {
                // If we encounter a number in unit context, it should be dimensionless
                // But this is likely an error in unit-only parsing
                var numberToken = Advance();
                throw new ParseException($"Unexpected number '{numberToken.Value}' in unit expression", numberToken.Position);
            }

            throw new ParseException($"Expected unit identifier or '(' but found '{CurrentToken.Value}'", CurrentToken.Position);
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

            throw new ParseException($"Unknown unit symbol '{symbol}'", position);
        }
    }
}
