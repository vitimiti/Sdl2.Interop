using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class CpuInformationDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetCpuCacheLineSize();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetCpuCountDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int GetSystemRamDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool Has3DNow();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAltiVec();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasArmSimd();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvx();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvx2();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasAvx512F();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasLasx();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasLsx();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasMmx();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasNeon();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasRdtsc();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse2();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse3();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse41();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    public delegate bool HasSse42();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SimdAlloc(CULong length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void SimdFree(IntPtr ptr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong SimdGetAlignment();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr SimdRealloc(IntPtr memory, CULong length);
}