using Microsoft.Win32.SafeHandles;

namespace Sdl2.Interop.SafeHandles;

internal sealed class SimdSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SimdSafeHandle(IntPtr ptr, bool ownsHandle) : base(ownsHandle)
    {
        handle = ptr;
    }

    protected override bool ReleaseHandle()
    {
        throw new NotImplementedException();
    }
}