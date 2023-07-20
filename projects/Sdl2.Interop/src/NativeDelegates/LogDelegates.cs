using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;

namespace Sdl2.Interop.NativeDelegates;

internal static class LogDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogCriticalDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogDebugDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogErrorDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogGetOutputFunctionDelegate(out Sdl.LogOutputFunction callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        out object userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Sdl.LogPriority LogGetPriorityDelegate(int category);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogInfoDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogMessageDelegate(int category, Sdl.LogPriority priority,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogResetPrioritiesDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogSetAllPriorityDelegate(Sdl.LogPriority priority);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogSetOutputFunctionDelegate(Sdl.LogOutputFunction callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogSetPriorityDelegate(int category, Sdl.LogPriority priority);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogVerboseDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate void LogWarnDelegate(int category,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string message);
}