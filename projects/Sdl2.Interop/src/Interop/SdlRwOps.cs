using System.Runtime.InteropServices;

namespace Sdl2.Interop.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct SdlRwOps
{
    public IntPtr Size;
    public IntPtr Seek;
    public IntPtr Read;
    public IntPtr Write;
    public IntPtr Close;
    public Sdl.RwOpsType Type;
}