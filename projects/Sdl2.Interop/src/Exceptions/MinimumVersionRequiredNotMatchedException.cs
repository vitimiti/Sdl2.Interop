using System.Runtime.Serialization;

namespace Sdl2.Interop.Exceptions;

/// <summary>
///     Represents an error that occurs when the minimum SDL version loaded by the library isn't high enough for the
///     function that is being loaded.
/// </summary>
[Serializable]
public sealed class MinimumVersionRequiredNotMatchedException : Exception
{
    /// <summary>Initializes a new instance of the <see cref="MinimumVersionRequiredNotMatchedException" /> class.</summary>
    public MinimumVersionRequiredNotMatchedException()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinimumVersionRequiredNotMatchedException" /> class with a
    ///     specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public MinimumVersionRequiredNotMatchedException(string? message) : base(message)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinimumVersionRequiredNotMatchedException" /> class with a
    ///     specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    ///     The exception that is the cause of the current exception, or a null reference (
    ///     <see langword="Nothing" /> in Visual Basic) if no inner exception is specified.
    /// </param>
    public MinimumVersionRequiredNotMatchedException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="MinimumVersionRequiredNotMatchedException" /> class with
    ///     serialized data.
    /// </summary>
    /// <param name="info">
    ///     The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being
    ///     thrown.
    /// </param>
    /// <param name="context">
    ///     The <see cref="StreamingContext" /> that contains contextual information about the source or
    ///     destination.
    /// </param>
    /// <exception cref="ArgumentNullException"><paramref name="info" /> is <see langword="null" />.</exception>
    /// <exception cref="SerializationException">
    ///     The class name is <see langword="null" /> or <see cref="Exception.HResult" />
    ///     is zero (0).
    /// </exception>
    public MinimumVersionRequiredNotMatchedException(SerializationInfo info, StreamingContext context)
    {
    }
}