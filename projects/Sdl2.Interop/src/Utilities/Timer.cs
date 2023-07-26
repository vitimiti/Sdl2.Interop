using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop.Utilities;

/// <summary>The SDL timer class.</summary>
/// <remarks>This class is available since SDL 2.0.0.</remarks>
public sealed class Timer : IDisposable
{
    private readonly Sdl _sdl;

    internal Timer(Sdl sdl, int id)
    {
        _sdl = sdl;
        Id = id;
    }

    /// <summary>A <see cref="int" /> with the timer ID.</summary>
    public int Id { get; }

    /// <summary>The dispose mechanism of the <see cref="Timer" /> class.</summary>
    public void Dispose()
    {
        _ = Common.GetExport<TimerDelegates.RemoveTimerDelegate>(_sdl, "SDL_RemoveTimer", new Version(2, 0, 0))(Id);
    }
}