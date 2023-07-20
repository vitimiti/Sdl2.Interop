using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class SdlDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InitDelegate(Sdl.InitializeFlags flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int InitSubSystemDelegate(Sdl.InitializeFlags flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void QuitDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void QuitSubSystemDelegate(Sdl.InitializeFlags flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Sdl.InitializeFlags WasInitDelegate(Sdl.InitializeFlags flags);
}