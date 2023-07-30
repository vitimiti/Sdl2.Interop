using System.Runtime.InteropServices;

using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop.SafeBuffers;

/// <summary>A class to represent a memory buffer.</summary>
/// <remarks>This class inherits from and implements the <see cref="SafeBuffer" /> class.</remarks>
public sealed class MemoryBuffer : SafeBuffer
{
    /// <summary>Create a new <see cref="MemoryBuffer" /> class.</summary>
    /// <param name="ownsHandle"><see langword="true" /> if the class owns the internal handle.</param>
    public MemoryBuffer(bool ownsHandle) : base(ownsHandle)
    {
    }

    internal MemoryBuffer(IntPtr newHandle, bool ownsHandle) : base(ownsHandle)
    {
        handle = newHandle;
    }

    /// <summary>The safe way to free the memory buffer.</summary>
    /// <returns><see langword="true" /> if the memory buffer was successfully cleared.</returns>
    protected override bool ReleaseHandle()
    {
        if (handle == IntPtr.Zero)
        {
            return handle == IntPtr.Zero;
        }

        Common.GetExport<StdIncDelegates.FreeDelegate>(Sdl.GetInstance(), "SDL_free", new Version(2, 0, 0))(handle);
        handle = IntPtr.Zero;
        return handle == IntPtr.Zero;
    }
}