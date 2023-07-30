using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;
using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.Interop;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.SafeBuffers;

namespace Sdl2.Interop.Utilities;

/// <summary>The read/write operation class.</summary>
/// <remarks>This class is available since SDL 2.0.0.</remarks>
public sealed class RwOps : IDisposable
{
    /// <summary>Close and free a <see cref="RwOps" />.</summary>
    /// <param name="context">The <see cref="RwOps" /> to close.</param>
    /// <returns>0 on success and -1 on error when flushing data.</returns>
    /// <remarks>This delegate is available since SDL 2.0.0.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int CloseDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps context);

    /// <summary>
    ///     Read up to <paramref name="maximumNumberOfObjects" /> objects each of size <paramref name="objectSize" /> from the
    ///     data stream to the area pointed at by <paramref name="memory" />.
    /// </summary>
    /// <param name="context">The <see cref="RwOps" /> to read.</param>
    /// <param name="memory">The <see cref="MemoryBuffer" /> to read into.</param>
    /// <param name="objectSize">The size, as a native <see cref="CULong" />, of each object to read.</param>
    /// <param name="maximumNumberOfObjects">The number, as a native <see cref="CULong" />, of objects to read.</param>
    /// <returns>A native <see cref="CULong" /> with the number of objects read, or 0 at error or end of file.</returns>
    /// <remarks>This delegate is available since SDL 2.0.0.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong ReadDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer memory, CULong objectSize, CULong maximumNumberOfObjects);

    /// <summary>Seek to <paramref name="offset" /> relative to <paramref name="whence" />.</summary>
    /// <param name="context">The <see cref="RwOps" /> to seek in.</param>
    /// <param name="offset">A <see cref="long" /> with the offset to seek to.</param>
    /// <param name="whence">A <see cref="SeekOrigin" /> to seek relative to.</param>
    /// <returns>A <see cref="long" /> with the final offset of the stream or -1 on error.</returns>
    /// <remarks>This delegate is available since SDL 2.0.0.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SeekDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps context, long offset, SeekOrigin whence);

    /// <summary>Return the size of the file in this <see cref="RwOps" /> file.</summary>
    /// <param name="context">The <see cref="RwOps" /> to get the size from.</param>
    /// <returns>A <see cref="long" /> with the size of the file.</returns>
    /// <remarks>This delegate is available since SDL 2.0.0.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long SizeDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps context);

    /// <summary>
    ///     Write exactly <paramref name="numberOfObjects" /> objects each of size <paramref name="objectSize" /> from the
    ///     area pointed at by <paramref name="memory" />.
    /// </summary>
    /// <param name="context">The <see cref="RwOps" /> to write into.</param>
    /// <param name="memory">The <see cref="MemoryBuffer" /> to write.</param>
    /// <param name="objectSize">A native <see cref="CULong" /> with the object size.</param>
    /// <param name="numberOfObjects">A native <see cref="CULong" /> with the number of objects.</param>
    /// <returns>A native <see cref="CULong" /> with the number of objects written, or 0 at error or end of file.</returns>
    /// <remarks>This delegate is available since SDL 2.0.0.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CULong WriteDelegate(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(RwOpsCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        RwOps context,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(MemoryBufferCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        MemoryBuffer memory, CULong objectSize, CULong numberOfObjects);

    private readonly IntPtr _handle;

    private readonly Sdl _sdl;

    internal RwOps(Sdl sdl, IntPtr handle)
    {
        _sdl = sdl;
        _handle = handle;
    }

    /// <summary>The current <see cref="SizeDelegate" /> or <see langword="null" /> if none is set.</summary>
    /// <remarks>
    ///     <para>
    ///         This is expected to be <see langword="null" /> by default if the <see cref="RwOps" /> was created with
    ///         <see cref="Sdl.AllocateRw" />.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global",
        Justification = "The API requires this to be settable.")]
    public SizeDelegate? CurrentSizeDelegate { get; set; }

    /// <summary>The current <see cref="SeekDelegate" /> or <see langword="null" /> if none is set.</summary>
    /// <remarks>
    ///     <para>
    ///         This is expected to be <see langword="null" /> by default if the <see cref="RwOps" /> was created with
    ///         <see cref="Sdl.AllocateRw" />.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global",
        Justification = "The API requires this to be settable.")]
    public SeekDelegate? CurrentSeekDelegate { get; set; }

    /// <summary>The current <see cref="ReadDelegate" /> or <see langword="null" /> if none is set.</summary>
    /// <remarks>
    ///     <para>
    ///         This is expected to be <see langword="null" /> by default if the <see cref="RwOps" /> was created with
    ///         <see cref="Sdl.AllocateRw" />.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global",
        Justification = "The API requires this to be settable.")]
    public ReadDelegate? CurrentReadDelegate { get; set; }

    /// <summary>The current <see cref="WriteDelegate" /> or <see langword="null" /> if none is set.</summary>
    /// <remarks>
    ///     <para>
    ///         This is expected to be <see langword="null" /> by default if the <see cref="RwOps" /> was created with
    ///         <see cref="Sdl.AllocateRw" />.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global",
        Justification = "The API requires this to be settable.")]
    public WriteDelegate? CurrentWriteDelegate { get; set; }

    /// <summary>The current <see cref="CloseDelegate" /> or <see langword="null" /> if none is set.</summary>
    /// <remarks>
    ///     <para>
    ///         This is expected to be <see langword="null" /> by default if the <see cref="RwOps" /> was created with
    ///         <see cref="Sdl.AllocateRw" />.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Global",
        Justification = "The API requires this to be settable.")]
    public CloseDelegate? CurrentCloseDelegate { get; set; }

    /// <summary>The <see cref="Sdl.RwOpsType" /> of the current <see cref="RwOps" /> stream.</summary>
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",
        Justification = "The API requires this to be gettable and settable.")]
    public Sdl.RwOpsType Type { get; set; }

    /// <summary>Read/write a <see cref="byte" /> from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="byte" />.</exception>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    public byte Unsigned8
    {
        get => Common.GetExport<RwOpsDelegates.ReadU8Delegate>(_sdl, "SDL_ReadU8", new Version(2, 0, 0))(_handle);
        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteU8Delegate>(_sdl, "SDL_WriteU8", new Version(2, 0, 0))(_handle,
                    value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 16 bits of little-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="ushort" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public ushort LittleEndian16
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadLittleEndian16Delegate>(_sdl, "SDL_ReadLE16", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteLittleEndian16Delegate>(_sdl, "SDL_WriteLE16",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 16 bits of big-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="ushort" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public ushort BigEndian16
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadBigEndian16Delegate>(_sdl, "SDL_ReadBE16", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteBigEndian16Delegate>(_sdl, "SDL_WriteBE16",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 32 bits of little-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="uint" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public uint LittleEndian32
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadLittleEndian32Delegate>(_sdl, "SDL_ReadLE32", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteLittleEndian32Delegate>(_sdl, "SDL_WriteLE32",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 32 bits of big-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="uint" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public uint BigEndian32
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadBigEndian32Delegate>(_sdl, "SDL_ReadBE32", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteBigEndian32Delegate>(_sdl, "SDL_WriteBE32",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 64 bits of little-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="ulong" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public ulong LittleEndian64
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadLittleEndian64Delegate>(_sdl, "SDL_ReadLE64", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteLittleEndian64Delegate>(_sdl, "SDL_WriteLE64",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary>Read/write 64 bits of big-endian data from the <see cref="RwOps" /> stream.</summary>
    /// <exception cref="NativeException">When SDL is unable to write the given <see cref="ulong" />.</exception>
    /// <remarks>
    ///     <para>SDL byteswaps the data only if necessary, so the data returned will be in the native byte order.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public ulong BigEndian64
    {
        get =>
            Common.GetExport<RwOpsDelegates.ReadBigEndian64Delegate>(_sdl, "SDL_ReadBE64", new Version(2, 0, 0))(
                _handle);

        set
        {
            CULong errorCode =
                Common.GetExport<RwOpsDelegates.WriteBigEndian64Delegate>(_sdl, "SDL_WriteBE64",
                    new Version(2, 0, 0))(_handle, value);

            if (errorCode.Value == 0)
            {
                throw new NativeException(_sdl.LastError);
            }
        }
    }

    /// <summary><see langword="true" /> when the internal handle is <see cref="IntPtr.Zero" />.</summary>
    public bool IsInvalid => _handle == IntPtr.Zero;

    /// <summary>Safely dispose the internal stream handle.</summary>
    public void Dispose()
    {
        Common.GetExport<RwOpsDelegates.FreeRwDelegate>(_sdl, "SDL_FreeRw", new Version(2, 0, 0))(_handle);
        GC.SuppressFinalize(this);
    }

    internal static RwOps ToManaged(Sdl sdl, IntPtr handle)
    {
        SdlRwOps sdlRwOps = new();
        Marshal.PtrToStructure(handle, sdlRwOps);
        return new RwOps(sdl, handle)
        {
            CurrentSizeDelegate =
                sdlRwOps.Size == IntPtr.Zero
                    ? null
                    : Marshal.GetDelegateForFunctionPointer<SizeDelegate>(sdlRwOps.Size),
            CurrentSeekDelegate =
                sdlRwOps.Seek == IntPtr.Zero
                    ? null
                    : Marshal.GetDelegateForFunctionPointer<SeekDelegate>(sdlRwOps.Seek),
            CurrentReadDelegate =
                sdlRwOps.Read == IntPtr.Zero
                    ? null
                    : Marshal.GetDelegateForFunctionPointer<ReadDelegate>(sdlRwOps.Read),
            CurrentWriteDelegate =
                sdlRwOps.Write == IntPtr.Zero
                    ? null
                    : Marshal.GetDelegateForFunctionPointer<WriteDelegate>(sdlRwOps.Write),
            CurrentCloseDelegate =
                sdlRwOps.Close == IntPtr.Zero
                    ? null
                    : Marshal.GetDelegateForFunctionPointer<CloseDelegate>(sdlRwOps.Close),
            Type = sdlRwOps.Type
        };
    }

    /// <summary>Use this function to get the size of the data stream in the <see cref="RwOps" />.</summary>
    /// <returns>A <see cref="long" /> with the size of the data stream in the <see cref="RwOps" />.</returns>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentSizeDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL is unable to get the size.</exception>
    /// <remarks>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentSizeDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public long Size()
    {
        if (CurrentSizeDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentSizeDelegate)} is null and must be set before being accessed");
        }

        long result = _sdl.Version < new Version(2, 0, 10)
            ? CurrentSizeDelegate(this)
            : Common.GetExport<RwOpsDelegates.RwSizeDelegate>(_sdl, "SDL_RWsize", new Version(2, 0, 10))(_handle);

        return result < 0 ? throw new NativeException(_sdl.LastError, (int)result) : result;
    }

    /// <summary>Seek within a <see cref="RwOps" /> data stream.</summary>
    /// <param name="offset">
    ///     A <see cref="long" /> with the offset, in bytes, relative to the <paramref name="whence" />
    ///     location. It can be negative.
    /// </param>
    /// <param name="whence">A <see cref="SeekOrigin" /> to seek from.</param>
    /// <returns>The final offset in the data stream after the seek.</returns>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentSeekDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL fails to get the final offset in the data stream after the seek.</exception>
    /// ///
    /// <remarks>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentSeekDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Close" />
    /// <seealso cref="Sdl.RwFromConstantMemory" />
    /// <seealso cref="Sdl.RwFromFile" />
    /// <seealso cref="Sdl.RwFromFileStream" />
    /// <seealso cref="Sdl.RwFromMemory" />
    /// <seealso cref="Read" />
    /// <seealso cref="Tell" />
    /// <seealso cref="Write" />
    public long Seek(long offset, SeekOrigin whence)
    {
        if (CurrentSeekDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentSeekDelegate)} is null and must be set before being accessed");
        }

        long result = _sdl.Version < new Version(2, 0, 10)
            ? CurrentSeekDelegate(this, offset, whence)
            : Common.GetExport<RwOpsDelegates.RwSeekDelegate>(_sdl, "SDL_RWseek", new Version(2, 0, 10))(_handle,
                offset, whence);

        return result < 0 ? throw new NativeException(_sdl.LastError, (int)result) : result;
    }

    /// <summary>Determine the current read/write offset in a <see cref="RwOps" /> data stream.</summary>
    /// <returns>The current offset in the stream.</returns>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentSeekDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL fails to get the current offset in the data stream.</exception>
    /// <remarks>
    ///     <para>
    ///         <see cref="Tell" /> is a wrapper function that calls <see cref="Seek" /> with an offset of 0 bytes and a
    ///         whence of <see cref="SeekOrigin.Current" />.
    ///     </para>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentSeekDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Close" />
    /// <seealso cref="Sdl.RwFromConstantMemory" />
    /// <seealso cref="Sdl.RwFromFile" />
    /// <seealso cref="Sdl.RwFromFileStream" />
    /// <seealso cref="Sdl.RwFromMemory" />
    /// <seealso cref="Read" />
    /// <seealso cref="Seek" />
    /// <seealso cref="Write" />
    public long Tell()
    {
        if (CurrentSeekDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentSeekDelegate)} is null and must be set before being accessed");
        }

        long result = _sdl.Version < new Version(2, 0, 10)
            ? CurrentSeekDelegate(this, 0, SeekOrigin.Current)
            : Common.GetExport<RwOpsDelegates.RwTellDelegate>(_sdl, "SDL_RWtell", new Version(2, 0, 10))(_handle);

        return result < 0 ? throw new NativeException(_sdl.LastError, (int)result) : result;
    }

    /// <summary>Read from a data source.</summary>
    /// <param name="memory">A <see cref="MemoryBuffer" /> with the data to read into.</param>
    /// <param name="sizeOfObjects">A <see cref="uint" /> with the size of each element to read.</param>
    /// <param name="numberOfObjects">A <see cref="uint" /> with the number of elements to read.</param>
    /// <returns>The number of objects read.</returns>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentReadDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL fails to get the number of objects read or when reaching the end of file.</exception>
    /// <remarks>
    ///     <para>
    ///         This function reads up to <paramref name="numberOfObjects" /> objects each of size
    ///         <paramref name="sizeOfObjects" /> from the data source to the area pointed at by <paramref name="memory" />.
    ///         This function may read less objects than requested.
    ///     </para>
    ///     <para>
    ///         Throwing the <see cref="NativeException" /> may be normal if reaching the end of file and you may want to
    ///         manage this exception in a try/catch block.
    ///     </para>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentReadDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Close" />
    /// <seealso cref="Sdl.RwFromConstantMemory" />
    /// <seealso cref="Sdl.RwFromFile" />
    /// <seealso cref="Sdl.RwFromFileStream" />
    /// <seealso cref="Sdl.RwFromMemory" />
    /// <seealso cref="Seek" />
    /// <seealso cref="Write" />
    public uint Read(MemoryBuffer memory, uint sizeOfObjects, uint numberOfObjects)
    {
        if (CurrentReadDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentReadDelegate)} is null and must be set before being accessed");
        }

        memory.Initialize(numberOfObjects, sizeOfObjects);
        CULong result = _sdl.Version < new Version(2, 0, 10)
            ? CurrentReadDelegate(this, memory, new CULong(sizeOfObjects), new CULong(numberOfObjects))
            : Common.GetExport<RwOpsDelegates.RwReadDelegate>(_sdl, "SDL_RWread", new Version(2, 0, 10))(_handle,
                memory, new CULong(sizeOfObjects), new CULong(numberOfObjects));

        return result.Value == 0 ? throw new NativeException(_sdl.LastError) : (uint)result.Value;
    }

    /// <summary>Write into a data source.</summary>
    /// <param name="memory">A <see cref="MemoryBuffer" /> with the data to write from.</param>
    /// <param name="sizeOfObjects">A <see cref="uint" /> with the size of each element to write.</param>
    /// <param name="numberOfObjects">A <see cref="uint" /> with the number of elements to write.</param>
    /// <returns>The number of objects written.</returns>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentWriteDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL fails to write the requested number of objects.</exception>
    /// <remarks>
    ///     <para>
    ///         This function writes exactly <paramref name="numberOfObjects" /> objects each of size
    ///         <paramref name="sizeOfObjects" /> from the data source to the area pointed at by <paramref name="memory" />.
    ///     </para>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentWriteDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Close" />
    /// <seealso cref="Sdl.RwFromConstantMemory" />
    /// <seealso cref="Sdl.RwFromFile" />
    /// <seealso cref="Sdl.RwFromFileStream" />
    /// <seealso cref="Sdl.RwFromMemory" />
    /// <seealso cref="Read" />
    /// <seealso cref="Seek" />
    public uint Write(MemoryBuffer memory, uint sizeOfObjects, uint numberOfObjects)
    {
        if (CurrentWriteDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentWriteDelegate)} is null and must be set before being accessed");
        }

        CULong result = _sdl.Version < new Version(2, 0, 10)
            ? CurrentWriteDelegate(this, memory, new CULong(sizeOfObjects), new CULong(numberOfObjects))
            : Common.GetExport<RwOpsDelegates.RwWriteDelegate>(_sdl, "SDL_RWwrite", new Version(2, 0, 10))(_handle,
                memory, new CULong(sizeOfObjects), new CULong(numberOfObjects));

        return result.Value < numberOfObjects ? throw new NativeException(_sdl.LastError) : (uint)result.Value;
    }

    /// <summary>Close and free the allocated <see cref="RwOps" />.</summary>
    /// <exception cref="ArgumentNullException">When the <see cref="CurrentCloseDelegate" /> is set to <see langword="null" />.</exception>
    /// <exception cref="NativeException">When SDL fails to close the data stream.</exception>
    /// <remarks>
    ///     <para>
    ///         This closes and cleans up the <see cref="RwOps" /> structure in an equivalent way to calling
    ///         <see cref="Dispose" /> and will leave the internal handle as <see cref="IntPtr.Zero" />.
    ///     </para>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function called the <see cref="CurrentCloseDelegate" /> directly. Afterwards, it
    ///         uses the internal SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Sdl.RwFromConstantMemory" />
    /// <seealso cref="Sdl.RwFromFile" />
    /// <seealso cref="Sdl.RwFromFileStream" />
    /// <seealso cref="Sdl.RwFromMemory" />
    /// <seealso cref="Read" />
    /// <seealso cref="Seek" />
    /// <seealso cref="Write" />
    public void Close()
    {
        if (CurrentCloseDelegate is null)
        {
            throw new ArgumentNullException(
                $"{nameof(CurrentCloseDelegate)} is null and must be set before being accessed");
        }

        int errorCode = _sdl.Version < new Version(2, 0, 10)
            ? CurrentCloseDelegate(this)
            : Common.GetExport<RwOpsDelegates.RwCloseDelegate>(_sdl, "SDL_RWclose", new Version(2, 0, 10))(_handle);

        if (errorCode < 0)
        {
            throw new NativeException(_sdl.LastError);
        }
    }

    /// <summary>Retrieve the internal handle.</summary>
    /// <returns>An <see cref="IntPtr" /> with the internal handle.</returns>
    public IntPtr DangerousGetHandle()
    {
        return _handle;
    }

    /// <summary>The <see cref="RwOps" /> destructor.</summary>
    ~RwOps()
    {
        Dispose();
    }
}