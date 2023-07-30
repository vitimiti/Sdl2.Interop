using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2.Interop.Utilities;

namespace Sdl2.Interop.CustomMarshalers;

internal sealed class VoidPointerCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private VoidPointerCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public void CleanUpManagedData(object ManagedObj)
    {
        if (ManagedObj is IDisposable disposable && !_isAllocated)
        {
            disposable.Dispose();
        }
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (_isAllocated)
        {
            return;
        }

        GCHandle handle = GCHandle.FromIntPtr(pNativeData);
        handle.Free();
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<object>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
        if (ManagedObj is NullVoidPointer)
        {
            return IntPtr.Zero;
        }

        GCHandle handle = GCHandle.Alloc(ManagedObj);
        return (IntPtr)handle;
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        if (pNativeData == IntPtr.Zero)
        {
            return new NullVoidPointer();
        }

        GCHandle handle = GCHandle.FromIntPtr(pNativeData);
        if (!handle.IsAllocated)
        {
            return new NullVoidPointer();
        }

        return handle.Target ?? new NullVoidPointer();
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new VoidPointerCustomMarshaler(true),
            _ => s_defaultInstance ??= new VoidPointerCustomMarshaler(false)
        };
    }
}