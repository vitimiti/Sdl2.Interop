using System.Runtime.InteropServices;

namespace Sdl2.Interop.NativeDelegates;

internal static class PowerDelegates
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate Sdl.PowerState GetPowerInfoDelegate(out int seconds, out int percent);
}