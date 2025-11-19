using System;

namespace Veggerby.Units.Parsing;

/// <summary>
/// Exception thrown when parsing a unit or measurement expression fails.
/// </summary>
public class ParseException : Exception
{
    /// <summary>
    /// Gets the position in the input string where the error occurred.
    /// </summary>
    public int Position { get; }

    /// <summary>
    /// Creates a new parse exception with the specified message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ParseException(string message) : base(message)
    {
        Position = -1;
    }

    /// <summary>
    /// Creates a new parse exception with the specified message and position.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="position">The position in the input where the error occurred.</param>
    public ParseException(string message, int position) : base(message)
    {
        Position = position;
    }

    /// <summary>
    /// Creates a new parse exception with the specified message and inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public ParseException(string message, Exception innerException) : base(message, innerException)
    {
        Position = -1;
    }
}
