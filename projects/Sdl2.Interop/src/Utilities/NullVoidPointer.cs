namespace Sdl2.Interop.Utilities;

/// <summary>
///     An empty object that can be used to tell the library's marshalers that a C# object is meant to be a C null
///     void pointer.
/// </summary>
public class NullVoidPointer
{
    /// <summary>Gets the internal handle.</summary>
    /// <remarks>
    ///     This is a null void pointer representation (void* ptr = NULL), and so this function will always return
    ///     <see cref="IntPtr.Zero" />.
    /// </remarks>
    public IntPtr Handle => IntPtr.Zero;
}