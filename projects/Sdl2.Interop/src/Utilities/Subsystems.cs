using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop.Utilities;

/// <summary>The wrapper class to manage SDL subsystems.</summary>
/// <remarks>
///     <para>This class MUST be initialized through <see cref="Sdl.Initialize" />.</para>
///     <para>This class implements the <see cref="IDisposable" /> interface.</para>
/// </remarks>
public sealed class Subsystems : IDisposable
{
    private readonly Sdl _sdl;

    internal Subsystems(Sdl sdl)
    {
        _sdl = sdl;
    }

    /// <summary>Terminate the SDL subsystems.</summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        Common.GetExport<SdlDelegates.QuitDelegate>(_sdl, "SDL_Quit", new Version(2, 0, 0))();
    }

    /// <summary>The <see cref="Subsystems" /> destructor.</summary>
    ~Subsystems()
    {
        Dispose();
    }

    /// <summary>Initialize SDL subsystems after first initialization.</summary>
    /// <param name="flags">The <see cref="Sdl.InitializeFlags" /> flags to initialize.</param>
    /// <exception cref="NativeException">When SDL is unable to initialize.</exception>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    public void Start(Sdl.InitializeFlags flags)
    {
        int errorCode =
            Common.GetExport<SdlDelegates.InitSubSystemDelegate>(_sdl, "SDL_InitSubSystem",
                new Version(2, 0, 0))(Common.RemoveUnusedInitFlags(_sdl, flags));

        if (errorCode < 0)
        {
            throw new NativeException(_sdl.LastError, errorCode);
        }
    }

    /// <summary>Stop SDL subsystems after first initialization.</summary>
    /// <param name="flags">The <see cref="Sdl.InitializeFlags" /> flags to stop.</param>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    public void Stop(Sdl.InitializeFlags flags)
    {
        Common.GetExport<SdlDelegates.QuitSubSystemDelegate>(_sdl, "SDL_QuitSubSystem", new Version(2, 0, 0))(
            Common.RemoveUnusedInitFlags(_sdl, flags));
    }
}