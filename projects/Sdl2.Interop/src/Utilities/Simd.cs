using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop.Utilities;

/// <summary>A class to safely represent a block of SIMD memory.</summary>
/// <remarks>This class is available since SDL 2.0.10.</remarks>
public sealed class Simd : SafeBuffer
{
    private readonly Sdl _sdl;
    private IntPtr _handle;

    internal Simd(Sdl sdl, IntPtr ptr, uint length) : base(true)
    {
        _sdl = sdl;
        _handle = ptr;
        Initialize(length);
    }

    /// <summary>Safely dispose of the internal handle.</summary>
    /// <returns><see langword="true" /> when the internal handle was successfully disposed of.</returns>
    protected override bool ReleaseHandle()
    {
        if (_handle != IntPtr.Zero)
        {
            Common.GetExport<CpuInformationDelegates.SimdFreeDelegate>(_sdl, "SDL_SIMDFree", new Version(2, 0, 10))(
                _handle);
        }

        return handle == IntPtr.Zero;
    }

    /// <summary>Reallocate the memory from the <see cref="Simd" /> class.</summary>
    /// <param name="length">
    ///     A <see cref="uint" /> with the length, in bytes, of the block to allocated. The actual allocated
    ///     block might be larger due to padding, etc. Passing 0 will return a valid <see cref="Simd" /> class, assuming the
    ///     system isn't out of memory.
    /// </param>
    /// <returns>A new <see cref="Simd" /> class with the reallocated memory.</returns>
    /// <exception cref="NativeException">
    ///     When SDL is unable to reallocate the memory and the internal handle results in
    ///     <see cref="IntPtr.Zero" />.
    /// </exception>
    /// <remarks>This function is available since SDL 2.0.14.</remarks>
    public void Reallocate(uint length)
    {
        _handle = Common.GetExport<CpuInformationDelegates.SimdReallocDelegate>(_sdl, "SDL_SIMDRealloc",
            new Version(2, 0, 14))(_handle, new CULong(length));

        Initialize(length);
    }

    /// <summary>Retrieve the internal handle.</summary>
    /// <returns>An <see cref="IntPtr" /> with the internal handle.</returns>
    /// <remarks>This is dangerous and there is no safety guarantees if you access a disposed handle.</remarks>
    public IntPtr GetDangerousHandle()
    {
        return _handle;
    }
}