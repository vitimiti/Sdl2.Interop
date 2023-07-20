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

    public void CleanUpManagedData(object managedObj)
    {
        if (managedObj is IDisposable disposable && !_isAllocated)
        {
            disposable.Dispose();
        }
    }

    public void CleanUpNativeData(IntPtr nativeData)
    {
        if (_isAllocated)
        {
            return;
        }

        GCHandle handle = GCHandle.FromIntPtr(nativeData);
        handle.Free();
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<object>();
    }

    public IntPtr MarshalManagedToNative(object managedObj)
    {
        if (managedObj is NullVoidPointer)
        {
            return IntPtr.Zero;
        }

        GCHandle handle = GCHandle.Alloc(managedObj);
        return (IntPtr)handle;
    }

    public object MarshalNativeToManaged(IntPtr nativeData)
    {
        if (nativeData == IntPtr.Zero)
        {
            return new NullVoidPointer();
        }

        GCHandle handle = GCHandle.FromIntPtr(nativeData);
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