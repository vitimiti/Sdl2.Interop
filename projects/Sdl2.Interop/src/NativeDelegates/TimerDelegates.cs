using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;

namespace Sdl2.Interop.NativeDelegates;

internal static class TimerDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int AddTimerDelegate(uint interval, Sdl.TimerCallback callback,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object parameter);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void DelayDelegate(uint milliseconds);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong GetPerformanceCounterDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong GetPerformanceFrequencyDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong GetTicks64Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint GetTicksDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool RemoveTimerDelegate(int id);
}