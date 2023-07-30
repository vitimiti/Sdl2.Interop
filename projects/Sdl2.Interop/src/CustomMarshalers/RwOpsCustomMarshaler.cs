using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop.CustomMarshalers;

internal sealed class RwOpsCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private RwOpsCustomMarshaler(bool isAllocated)
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

        if (ManagedObj is not RwOps rwOps)
        {
            throw new ArgumentException(
                $"{nameof(ManagedObj)} should be of type {typeof(RwOps)} but was of type {ManagedObj.GetType()} instead");
        }

        rwOps.Dispose();
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (!_isAllocated)
        {
            Common.GetExport<RwOpsDelegates.FreeRwDelegate>(Sdl.GetInstance(), "SDL_FreeRW", new Version(2, 0, 0))(
                pNativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<RwOps>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
        return ManagedObj is RwOps rwOps
            ? rwOps.DangerousGetHandle()
            : throw new ArgumentException(
                $"{nameof(ManagedObj)} should be of type {typeof(RwOps)} but was of type {ManagedObj.GetType()} instead");
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        return RwOps.ToManaged(Sdl.GetInstance(), pNativeData);
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new RwOpsCustomMarshaler(true),
            _ => s_defaultInstance ??= new RwOpsCustomMarshaler(false)
        };
    }
}