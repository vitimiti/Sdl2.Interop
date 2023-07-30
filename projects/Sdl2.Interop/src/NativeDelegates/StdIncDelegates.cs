using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class StdIncDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeDelegate(IntPtr memory);
}