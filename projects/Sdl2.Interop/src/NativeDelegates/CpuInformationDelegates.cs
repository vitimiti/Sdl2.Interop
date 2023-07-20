using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class CpuInformationDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetCpuCacheLineSizeDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetCpuCountDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetSystemRamDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool Has3DNowDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAltiVecDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasArmSimdDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvx2Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvx512FDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvxDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasLasxDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasLsxDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasMmxDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasNeonDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasRdtscDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse2Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse3Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse41Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse42Delegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSseDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SimdAllocDelegate(CULong length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SimdFreeDelegate(IntPtr ptr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong SimdGetAlignmentDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SimdReallocDelegate(IntPtr memory, CULong length);
}