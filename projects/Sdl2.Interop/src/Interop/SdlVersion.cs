using System.Runtime.InteropServices;

namespace Sdl2.Interop.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct SdlVersion
{
    public readonly byte Major;
    public readonly byte Minor;
    public readonly byte Patch;
}