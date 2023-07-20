using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;

namespace Sdl2.Interop.NativeDelegates;

internal static class PlatformDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public delegate string GetPlatformDelegate();
}