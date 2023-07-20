using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>The prototype for the log output callback function.</summary>
    /// <param name="userData">
    ///     An <see cref="object" /> with the user data passed to <see cref="LogSetOutputFunction" />. You
    ///     may pass <see cref="NullVoidPointer" /> to represent a NULL void pointer.
    /// </param>
    /// <param name="category">An <see cref="int" /> that represents the log category.</param>
    /// <param name="priority">The <see cref="LogPriority" /> of the <paramref name="message" />.</param>
    /// <param name="message">A <see cref="string" /> with the message being output.</param>
    /// <remarks>This function is called by SDL when there is new text to be logged.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogOutputFunction(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object userData, int category, LogPriority priority,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    /// <summary>Set the priority of all log categories.</summary>
    /// <param name="priority">The <see cref="LogPriority" /> to assign.</param>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogSetPriority{TEnum}" />
    public void LogSetAllPriority(LogPriority priority)
    {
        Common.GetExport<LogDelegates.LogSetAllPriorityDelegate>(this, "SDL_LogSetAllPriority", new Version(2, 0, 0))(
            priority);
    }

    /// <summary>Set the priority of a particular log category.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category to assign a priority to.</param>
    /// <param name="priority">The <see cref="LogPriority" /> to assign.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogGetPriority{TEnum}" />
    /// <seealso cref="LogSetAllPriority" />
    public void LogSetPriority<TEnum>(TEnum category, LogPriority priority) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogSetPriorityDelegate>(this, "SDL_LogSetPriority", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), priority);
    }

    /// <summary>Get the priority of a particular log category.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category to query.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <returns>The <see cref="LogPriority" /> for the requested <paramref name="category" />.</returns>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogSetPriority{TEnum}" />
    public LogPriority LogGetPriority<TEnum>(TEnum category) where TEnum : Enum
    {
        return Common.GetExport<LogDelegates.LogGetPriorityDelegate>(this, "SDL_LogGetPriority", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)));
    }

    /// <summary>Reset all priorities to default.</summary>
    /// <remarks>
    ///     <para>This is called by <see cref="Subsystems.Dispose" />.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="LogSetAllPriority" />
    /// <seealso cref="LogSetPriority{TEnum}" />
    public void LogResetPriorities()
    {
        Common.GetExport<LogDelegates.LogResetPrioritiesDelegate>(this, "SDL_LogResetPriorities",
            new Version(2, 0, 0))();
    }

    /// <summary>Log a message with <see cref="LogCategory.Application" /> and <see cref="LogPriority.Information" />.</summary>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void Log(string message)
    {
        Common.GetExport<LogDelegates.LogDelegate>(this, "SDL_Log", new Version(2, 0, 0))(message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Verbose" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogVerbose<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogVerboseDelegate>(this, "SDL_LogVerbose", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Debug" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogDebug<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogDebugDelegate>(this, "SDL_LogDebug", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Information" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogInformation<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogInfoDelegate>(this, "SDL_LogInfo", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Warning" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    public void LogWarning<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogWarnDelegate>(this, "SDL_LogWarn", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Error" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogError<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogErrorDelegate>(this, "SDL_LogError", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with <see cref="LogPriority.Critical" />.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogMessage{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogCritical<TEnum>(TEnum category, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogCriticalDelegate>(this, "SDL_LogCritical", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), message);
    }

    /// <summary>Log a message with the specified category and priority.</summary>
    /// <param name="category">An <see cref="Enum" /> with the category of the message.</param>
    /// <param name="priority">The <see cref="LogPriority" /> of the message.</param>
    /// <param name="message">A <see cref="string" /> with the message to log.</param>
    /// <typeparam name="TEnum">
    ///     A type representing an <see cref="Enum" /> that holds log categories. The default categories
    ///     are in <see cref="LogCategory" />.
    /// </typeparam>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="Log" />
    /// <seealso cref="LogCritical{TEnum}" />
    /// <seealso cref="LogDebug{TEnum}" />
    /// <seealso cref="LogError{TEnum}" />
    /// <seealso cref="LogInformation{TEnum}" />
    /// <seealso cref="LogVerbose{TEnum}" />
    /// <seealso cref="LogWarning{TEnum}" />
    public void LogMessage<TEnum>(TEnum category, LogPriority priority, string message) where TEnum : Enum
    {
        Common.GetExport<LogDelegates.LogMessageDelegate>(this, "SDL_LogMessage", new Version(2, 0, 0))(
            (int)Convert.ChangeType(category, typeof(int)), priority, message);
    }

    /// <summary>Get the current log output function.</summary>
    /// <param name="callback">
    ///     An <see langword="out" /> parameter with the <see cref="LogOutputFunction" /> filled in with the
    ///     current log callback
    /// </param>
    /// <param name="userData">
    ///     An <see langword="out" /> parameter with the <see cref="object" /> filled in with the pointer
    ///     that is passed to <paramref name="callback" />.
    /// </param>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogSetOutputFunction" />
    public void LogGetOutputFunction(out LogOutputFunction callback, out object userData)
    {
        Common.GetExport<LogDelegates.LogGetOutputFunctionDelegate>(this, "SDL_LogGetOutputFunction",
            new Version(2, 0, 0))(out callback, out userData);
    }

    /// <summary>Replace the default log output function with one of your own.</summary>
    /// <param name="callback">The <see cref="LogOutputFunction" /> to call instead of the default.</param>
    /// <param name="userData">An <see cref="object" /> that is passed to <paramref name="callback" />.</param>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="LogGetOutputFunction" />
    public void LogSetOutputFunction(LogOutputFunction callback, object userData)
    {
        Common.GetExport<LogDelegates.LogSetOutputFunctionDelegate>(this, "SDL_LogSetOutputFunction",
            new Version(2, 0, 0))(callback, userData);
    }
}