namespace Veggerby.Units.Parsing;

/// <summary>
/// Represents a token in a unit expression.
/// </summary>
internal class Token
{
    /// <summary>
    /// Gets the type of this token.
    /// </summary>
    public TokenType Type { get; }

    /// <summary>
    /// Gets the text value of this token.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Gets the position of this token in the input string.
    /// </summary>
    public int Position { get; }

    /// <summary>
    /// Creates a new token.
    /// </summary>
    /// <param name="type">The token type.</param>
    /// <param name="value">The token value.</param>
    /// <param name="position">The position in the input.</param>
    public Token(TokenType type, string value, int position)
    {
        Type = type;
        Value = value;
        Position = position;
    }

    public override string ToString() => $"{Type}: '{Value}' at {Position}";
}
