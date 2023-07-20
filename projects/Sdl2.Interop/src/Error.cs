using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop;

public partial class Sdl
{
    internal string LastError =>
        Common.GetExport<ErrorDelegates.GetErrorDelegate>(this, "SDL_GetError", new Version(2, 0, 0))();
}