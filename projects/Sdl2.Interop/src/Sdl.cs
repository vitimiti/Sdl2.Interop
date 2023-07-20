using System.Reflection;
using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop;

/// <summary>Load the native SDL library.</summary>
/// <remarks>
///     <para>This is a partial class and the sum of all the partial units will complete the functionality from SDL.</para>
///     <para>This class implements the <see cref="IDisposable" /> interface.</para>
/// </remarks>
public partial class Sdl : IDisposable
{
    /// <summary>Create a new <see cref="Sdl" /> class with default library names.</summary>
    public Sdl()
    {
        Handle = NativeLibrary.Load(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "SDL2.dll" :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "libSDL2.dylib" : "libSDL2.so",
            Assembly.GetCallingAssembly(), DllImportSearchPath.AssemblyDirectory);
    }

    /// <summary>Create a new <see cref="Sdl" /> class with a custom library path name.</summary>
    /// <param name="libraryPath">A <see cref="string" /> with the library path and name.</param>
    public Sdl(string libraryPath)
    {
        Handle = NativeLibrary.Load(libraryPath, Assembly.GetCallingAssembly(), DllImportSearchPath.AssemblyDirectory);
    }

    internal IntPtr Handle { get; }

    /// <summary>Clean the internal unmanaged data.</summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        NativeLibrary.Free(Handle);
    }

    /// <summary>The <see cref="Sdl" /> destructor.</summary>
    ~Sdl()
    {
        ReleaseUnmanagedResources();
    }

    // TODO: Change native function signatures to C# ones.
    /// <summary>Initialize the SDL library.</summary>
    /// <param name="flags">The subsystem initialization flags from <see cref="InitializeFlags" />.</param>
    /// <returns>A new <see cref="Subsystems" /> class.</returns>
    /// <exception cref="NativeException">When SDL is unable to initialize with the given subsystem <paramref name="flags" />.</exception>
    /// <remarks>
    ///     <para>
    ///         The file I/O (for example: SDL_RWFromFile) and threading (SDL_CreateThread) subsystems are initialized by
    ///         default. Message boxes (SDL_ShowSimpleMessageBox) also attempt to work without initializing the video
    ///         subsystem, in hopes of being useful in showing an error dialog when <see cref="Initialize" /> fails. You must
    ///         specifically initialize other subsystems if you use them in your application.
    ///     </para>
    ///     <para>Logging (such as SDL_Log) works without initialization, too.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public Subsystems Initialize(InitializeFlags flags)
    {
        int errorCode =
            Common.GetExport<SdlDelegates.InitDelegate>(this, "SDL_Init", new Version(2, 0, 0))(
                Common.RemoveUnusedInitFlags(this, flags));

        if (errorCode < 0)
        {
            throw new NativeException(LastError, errorCode);
        }

        return new Subsystems(this);
    }

    /// <summary>Get the subsystem flags that are initialized.</summary>
    /// <returns>A <see cref="InitializeFlags" /> <see cref="Enum" /> with the initialized subsystem flags.</returns>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    public InitializeFlags WasInitialized()
    {
        return Common.GetExport<SdlDelegates.WasInitDelegate>(this, "SDL_WasInit", new Version(2, 0, 0))(
            InitializeFlags.None);
    }
}