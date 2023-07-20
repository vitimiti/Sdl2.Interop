using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class SdlDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InitDelegate(SdlInit flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InitSubSystemDelegate(SdlInit flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void QuitDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void QuitSubSystemDelegate(SdlInit flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate SdlInit WasInitDelegate(SdlInit flags);
}