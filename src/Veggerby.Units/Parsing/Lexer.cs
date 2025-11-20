using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Veggerby.Units.Parsing;

/// <summary>
/// Tokenizes unit expression strings into a sequence of tokens.
/// </summary>
internal class Lexer
{
    private readonly string _input;
    private int _position;

    /// <summary>
    /// Creates a new lexer for the specified input string.
    /// </summary>
    /// <param name="input">The input string to tokenize.</param>
    public Lexer(string input)
    {
        _input = input ?? string.Empty;
        _position = 0;
    }

    /// <summary>
    /// Tokenizes the input string and returns all tokens.
    /// </summary>
    public List<Token> Tokenize()
    {
        var tokens = new List<Token>();

        while (_position < _input.Length)
        {
            SkipWhitespace();
            if (_position >= _input.Length)
            {
                break;
            }

            var token = ReadToken();
            if (token != null)
            {
                tokens.Add(token);
            }
        }

        tokens.Add(new Token(TokenType.EndOfInput, string.Empty, _position));
        return tokens;
    }

    private void SkipWhitespace()
    {
        while (_position < _input.Length && char.IsWhiteSpace(_input[_position]))
        {
            _position++;
        }
    }

    private Token ReadToken()
    {
        var startPos = _position;
        var ch = _input[_position];

        // Check for operators and special characters
        switch (ch)
        {
            case '(':
                _position++;
                return new Token(TokenType.LeftParen, "(", startPos);

            case ')':
                _position++;
                return new Token(TokenType.RightParen, ")", startPos);

            case '/':
                _position++;
                return new Token(TokenType.Divide, "/", startPos);

            case '*':
                _position++;
                return new Token(TokenType.Multiply, "*", startPos);

            case '·':
            case '⋅':
                _position++;
                return new Token(TokenType.Multiply, "·", startPos);

            case '^':
                _position++;
                return new Token(TokenType.Power, "^", startPos);

            case '²':
                _position++;
                return new Token(TokenType.Superscript, "2", startPos);

            case '³':
                _position++;
                return new Token(TokenType.Superscript, "3", startPos);

            case '⁴':
                _position++;
                return new Token(TokenType.Superscript, "4", startPos);

            case '⁵':
                _position++;
                return new Token(TokenType.Superscript, "5", startPos);

            case '⁶':
                _position++;
                return new Token(TokenType.Superscript, "6", startPos);

            case '⁷':
                _position++;
                return new Token(TokenType.Superscript, "7", startPos);

            case '⁸':
                _position++;
                return new Token(TokenType.Superscript, "8", startPos);

            case '⁹':
                _position++;
                return new Token(TokenType.Superscript, "9", startPos);

            case '⁰':
                _position++;
                return new Token(TokenType.Superscript, "0", startPos);

            case '⁻':
                // Negative superscript, read the following superscript digits
                _position++;
                var superscriptValue = ReadSuperscriptNumber();
                return new Token(TokenType.Superscript, "-" + superscriptValue, startPos);
        }

        // Check for numbers (including scientific notation)
        if (char.IsDigit(ch) || (ch == '-' && _position + 1 < _input.Length && char.IsDigit(_input[_position + 1])))
        {
            return ReadNumber(startPos);
        }

        // Check for identifiers (unit symbols)
        if (char.IsLetter(ch) || ch == '°' || ch == 'μ')
        {
            return ReadIdentifier(startPos);
        }

        throw new ParseException($"Unexpected character '{ch}' at position {_position}", _position);
    }

    private Token ReadNumber(int startPos)
    {
        var sb = new StringBuilder();

        // Handle optional negative sign
        if (_input[_position] == '-')
        {
            sb.Append(_input[_position]);
            _position++;
        }

        // Read integer part
        while (_position < _input.Length && char.IsDigit(_input[_position]))
        {
            sb.Append(_input[_position]);
            _position++;
        }

        // Read decimal part
        if (_position < _input.Length && _input[_position] == '.')
        {
            sb.Append(_input[_position]);
            _position++;

            while (_position < _input.Length && char.IsDigit(_input[_position]))
            {
                sb.Append(_input[_position]);
                _position++;
            }
        }

        // Read scientific notation (e or E)
        if (_position < _input.Length && (_input[_position] == 'e' || _input[_position] == 'E'))
        {
            sb.Append(_input[_position]);
            _position++;

            // Handle optional sign
            if (_position < _input.Length && (_input[_position] == '+' || _input[_position] == '-'))
            {
                sb.Append(_input[_position]);
                _position++;
            }

            // Read exponent
            while (_position < _input.Length && char.IsDigit(_input[_position]))
            {
                sb.Append(_input[_position]);
                _position++;
            }
        }

        return new Token(TokenType.Number, sb.ToString(), startPos);
    }

    private Token ReadIdentifier(int startPos)
    {
        var sb = new StringBuilder();

        while (_position < _input.Length)
        {
            var ch = _input[_position];

            // Allow letters, degrees symbol, micro symbol
            if (char.IsLetter(ch) || ch == '°' || ch == 'μ')
            {
                sb.Append(ch);
                _position++;
            }
            else
            {
                break;
            }
        }

        return new Token(TokenType.Identifier, sb.ToString(), startPos);
    }

    private string ReadSuperscriptNumber()
    {
        var sb = new StringBuilder();

        while (_position < _input.Length)
        {
            var ch = _input[_position];
            var digit = ConvertSuperscriptToDigit(ch);

            if (digit.HasValue)
            {
                sb.Append(digit.Value);
                _position++;
            }
            else
            {
                break;
            }
        }

        return sb.ToString();
    }

    private int? ConvertSuperscriptToDigit(char ch)
    {
        return ch switch
        {
            '⁰' => 0,
            '¹' => 1,
            '²' => 2,
            '³' => 3,
            '⁴' => 4,
            '⁵' => 5,
            '⁶' => 6,
            '⁷' => 7,
            '⁸' => 8,
            '⁹' => 9,
            _ => null
        };
    }
}
