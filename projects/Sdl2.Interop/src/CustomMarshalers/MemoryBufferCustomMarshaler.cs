using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.SafeBuffers;

namespace Sdl2.Interop.CustomMarshalers;

internal sealed class MemoryBufferCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private MemoryBufferCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public void CleanUpManagedData(object ManagedObj)
    {
        if (_isAllocated)
        {
            return;
        }

        if (ManagedObj is not MemoryBuffer memory)
        {
            throw new ArgumentException(
                $"{nameof(ManagedObj)} should be of type {typeof(MemoryBuffer)} but was of type {ManagedObj.GetType()} instead");
        }

        memory.Dispose();
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (!_isAllocated)
        {
            Common.GetExport<StdIncDelegates.FreeDelegate>(Sdl.GetInstance(), "SDL_free", new Version(2, 0, 0))(
                pNativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<MemoryBuffer>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
        return ManagedObj is MemoryBuffer memory
            ? memory.DangerousGetHandle()
            : throw new ArgumentException(
                $"{nameof(ManagedObj)} should be of type {typeof(MemoryBuffer)} but was of type {ManagedObj.GetType()} instead");
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        return new MemoryBuffer(pNativeData, true);
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new MemoryBufferCustomMarshaler(true),
            _ => s_defaultInstance ??= new MemoryBufferCustomMarshaler(false)
        };
    }
}