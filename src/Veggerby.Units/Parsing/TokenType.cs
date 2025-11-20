namespace Veggerby.Units.Parsing;

/// <summary>
/// Represents the type of token in a unit expression.
/// </summary>
internal enum TokenType
{
    /// <summary>End of input.</summary>
    EndOfInput,

    /// <summary>A numeric value (integer or decimal).</summary>
    Number,

    /// <summary>A unit symbol or identifier.</summary>
    Identifier,

    /// <summary>Multiplication operator (* or · or space).</summary>
    Multiply,

    /// <summary>Division operator (/).</summary>
    Divide,

    /// <summary>Power operator (^).</summary>
    Power,

    /// <summary>Left parenthesis (().</summary>
    LeftParen,

    /// <summary>Right parenthesis ()).</summary>
    RightParen,

    /// <summary>A superscript digit or power symbol.</summary>
    Superscript
}
