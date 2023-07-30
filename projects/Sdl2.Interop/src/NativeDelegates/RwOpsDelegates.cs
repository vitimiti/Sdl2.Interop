using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;
using Sdl2.Interop.SafeBuffers;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop.NativeDelegates;

internal static class RwOpsDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr AllocRwDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FreeRwDelegate(IntPtr area);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr LoadFileDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string file,
        out CULong dataSize);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr LoadFileRwDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps src, out CULong dataSize, [MarshalAs(UnmanagedType.I1)] bool freeSrc);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort ReadBigEndian16Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint ReadBigEndian32Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong ReadBigEndian64Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ushort ReadLittleEndian16Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint ReadLittleEndian32Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ulong ReadLittleEndian64Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate byte ReadU8Delegate(IntPtr src);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int RwCloseDelegate(IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr RwFromConstantMemoryDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer constantMemory, int size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public delegate IntPtr RwFromFileDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string file,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(StringCustomMarshaler))]
        string mode);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr RwFromFp(IntPtr fp, [MarshalAs(UnmanagedType.I1)] bool autoClose);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr RwFromMemoryDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer memory, int size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong RwReadDelegate(IntPtr context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer memory, CULong size, CULong maximumNumber);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long RwSeekDelegate(IntPtr context, long offset, SeekOrigin whence);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long RwSizeDelegate(IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long RwTellDelegate(IntPtr context);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong RwWriteDelegate(IntPtr context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer memory, CULong size, CULong number);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteBigEndian16Delegate(IntPtr dst, ushort value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteBigEndian32Delegate(IntPtr dst, uint value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteBigEndian64Delegate(IntPtr dst, ulong value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteLittleEndian16Delegate(IntPtr dst, ushort value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteLittleEndian32Delegate(IntPtr dst, uint value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteLittleEndian64Delegate(IntPtr dst, ulong value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteU8Delegate(IntPtr dst, byte value);
}