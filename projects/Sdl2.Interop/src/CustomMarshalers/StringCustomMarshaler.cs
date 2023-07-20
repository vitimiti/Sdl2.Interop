using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Sdl2.Interop.CustomMarshalers;

internal class StringCustomMarshaler : ICustomMarshaler
{
    private static ICustomMarshaler? s_defaultInstance;
    private static ICustomMarshaler? s_allocatedInstance;

    private readonly bool _isAllocated;

    private StringCustomMarshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public void CleanUpManagedData(object ManagedObj)
    {
        // Don't do anything.
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (!_isAllocated)
        {
            Marshal.FreeCoTaskMem(pNativeData);
        }
    }

    public int GetNativeDataSize()
    {
        return Marshal.SizeOf<string>();
    }

    [SuppressMessage("ReSharper", "InconsistentNaming",
        Justification = "The ICustomMarshaler interface is implemented like this")]
    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
        return ManagedObj is string str
            ? Marshal.StringToCoTaskMemUTF8(str)
            : throw new ArgumentException(
                $"{nameof(ManagedObj)} should be of type {typeof(string)} but was of type {ManagedObj.GetType()} instead");
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        return Marshal.PtrToStringUTF8(pNativeData) ?? string.Empty;
    }

    public static ICustomMarshaler GetInstance(string cookie)
    {
        return cookie switch
        {
            Cookies.LeaveAllocated => s_allocatedInstance ??= new StringCustomMarshaler(true),
            _ => s_defaultInstance ??= new StringCustomMarshaler(false)
        };
    }
}