using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop.Utilities;

/// <summary>A class to safely represent a block of SIMD memory.</summary>
/// <remarks>This class is available since SDL 2.0.10.</remarks>
public sealed class Simd : IDisposable
{
    private readonly Sdl _sdl;
    private IntPtr _handle;

    internal Simd(Sdl sdl, IntPtr ptr, uint length)
    {
        _sdl = sdl;
        _handle = ptr;
        if (ptr == IntPtr.Zero)
        {
            throw new NativeException(_sdl.LastError);
        }

        Length = length;
    }

    /// <summary>Gets a value that indicates whether the handle is invalid.</summary>
    /// <returns><see langword="true" /> if the handle is not valid; otherwise, <see langword="false" />.</returns>
    public bool IsInvalid => _handle == IntPtr.Zero || _handle == new IntPtr(-1);

    /// <summary>The length, in bytes, of the block to allocated.</summary>
    /// <returns>A <see cref="uint" /> with the length, in bytes.</returns>
    /// <remarks>
    ///     <para>The actual allocated block might be larger due to padding, etc.</para>
    ///     <para>A <see cref="Length" /> of 0 means you own nothing of the internal handle.</para>
    /// </remarks>
    public uint Length { get; private set; }

    /// <summary>The method to safely dispose the internal handle.</summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    private void ReleaseUnmanagedResources()
    {
        if (_handle != IntPtr.Zero)
        {
            Common.GetExport<CpuInformationDelegates.SimdFreeDelegate>(_sdl, "SDL_SIMDFree", new Version(2, 0, 10))(
                _handle);
        }
    }

    /// <summary>The class destructor.</summary>
    ~Simd()
    {
        Dispose();
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

        Length = length;
    }

    /// <summary>Retrieve the internal handle.</summary>
    /// <returns>An <see cref="IntPtr" /> with the internal handle.</returns>
    /// <remarks>This is dangerous and there is no safety guarantees if you access a disposed handle.</remarks>
    public IntPtr GetDangerousHandle()
    {
        return _handle;
    }
}