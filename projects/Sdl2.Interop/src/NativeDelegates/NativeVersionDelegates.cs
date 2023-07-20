using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;
using Sdl2.Interop.Interop;

namespace Sdl2.Interop.NativeDelegates;

internal static class NativeVersionDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public delegate string GetRevisionDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetRevisionNumberDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void GetVersionDelegate(out SdlVersion version);
}