using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;

namespace Sdl2.Interop.NativeDelegates;

internal static class ClipboardDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public delegate string GetClipboardTextDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    [return:
        MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
    public delegate string GetPrimarySelectionTextDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasClipboardText();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasPrimarySelectionText();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate int SetClipboardTextDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string text);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate int SetPrimarySelectionTextDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string text);
}