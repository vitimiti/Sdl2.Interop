using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.SafeBuffers;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Create a <see cref="RwOps" /> class for reading from and/or writing to a named file.</summary>
    /// <param name="file">A <see cref="string" /> with the file to open.</param>
    /// <param name="access">A <see cref="FileAccess" /> with the access permissions to the <paramref name="file" />.</param>
    /// <param name="mode">A <see cref="FileMode" /> with the mode permissions to the <paramref name="file" />.</param>
    /// <param name="isBinary">Whether the <paramref name="file" /> is binary or not.</param>
    /// <returns>A <see cref="RwOps" /> from the given <paramref name="file" />.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     When the combination of <paramref name="access" /> and
    ///     <paramref name="mode" /> is invalid.
    /// </exception>
    /// <exception cref="RwOps">When SDL is unable to create the internal handle.</exception>
    /// <remarks>
    ///     <para>
    ///         The available combinations for the <paramref name="access" /> and <paramref name="mode" /> are limited and the
    ///         only
    ///         valid options are:
    ///         <code>
    /// var valid1 = new Operations("filePath", FileAccess.Read, FileMode.Open, false);              // Equivalent to "r"
    /// var valid2 = new Operations("filePath", FileAccess.Write, FileMode.OpenOrCreate, false);     // Equivalent to "w"
    /// var valid3 = new Operations("filePath", FileAccess.Write, FileMode.Append, false);           // Equivalent to "a"
    /// var valid4 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Open, false);         // Equivalent to "r+"
    /// var valid5 = new Operations("filePath", FileAccess.ReadWrite, FileMode.OpenOrCreate, false); // Equivalent to "w+"
    /// var valid6 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Append, false);       // Equivalent to "a+"
    /// var valid7 = new Operations("filePath", FileAccess.Read, FileMode.Open, true);               // Equivalent to "rb"
    /// var valid8 = new Operations("filePath", FileAccess.Write, FileMode.OpenOrCreate, true);      // Equivalent to "wb"
    /// var valid9 = new Operations("filePath", FileAccess.Write, FileMode.Append, true);            // Equivalent to "ab"
    /// var valid10 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Open, true);         // Equivalent to "r+b"
    /// var valid11 = new Operations("filePath", FileAccess.ReadWrite, FileMode.OpenOrCreate, true); // Equivalent to "w+b"
    /// var valid12 = new Operations("filePath", FileAccess.ReadWrite, FileMode.Append, true);       // Equivalent to "a+b"
    ///         </code>
    ///     </para>
    ///     <para>
    ///         This function supports Unicode filenames, but they must be encoded in UTF-8 format, regardless of the
    ///         underlying operating system.
    ///     </para>
    ///     <para>As a fallback, this will transparently open a matching filename in an Android app's `assets`.</para>
    ///     <para>Closing the operations will close the file handle SDL is holding internally.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="RwOps.Close" />
    /// <seealso cref="RwFromConstantMemory" />
    /// <seealso cref="RwFromFileStream" />
    /// <seealso cref="RwFromMemory" />
    /// <seealso cref="RwOps.Read" />
    /// <seealso cref="RwOps.Seek" />
    /// <seealso cref="RwOps.Tell" />
    /// <seealso cref="RwOps.Write" />
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "The API requires this is public")]
    public RwOps RwFromFile(string file, FileAccess access, FileMode mode, bool isBinary)
    {
        string modeStr = access switch
        {
            FileAccess.Read when mode is FileMode.Open => "r",
            FileAccess.Write when mode is FileMode.OpenOrCreate => "w",
            FileAccess.Write when mode is FileMode.Append => "a",
            FileAccess.ReadWrite when mode is FileMode.Open => "r+",
            FileAccess.ReadWrite when mode is FileMode.OpenOrCreate => "w+",
            FileAccess.ReadWrite when mode is FileMode.Append => "a+",
            _ => throw new ArgumentOutOfRangeException(nameof(access), access, null)
        };

        if (isBinary)
        {
            modeStr += "b";
        }

        IntPtr handle =
            Common.GetExport<RwOpsDelegates.RwFromFileDelegate>(this, "SDL_RWFromFile", new Version(2, 0, 0))(file,
                modeStr);

        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        return new RwOps(this, handle);
    }

    /// <summary>Create a <see cref="RwOps" /> class from a <see cref="FileStream" />.</summary>
    /// <param name="fileStream">The <see cref="FileStream" /> to create the class from.</param>
    /// <param name="autoClose">
    ///     <see langword="true" /> to automatically close the <paramref name="fileStream" /> handle,
    ///     <see langword="false" /> otherwise.
    /// </param>
    /// <returns>A <see cref="RwOps" /> from the given <paramref name="fileStream" />.</returns>
    /// <exception cref="NativeException">When SDL is unable to create the internal handle.</exception>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="RwOps.Close" />
    /// <seealso cref="RwFromConstantMemory" />
    /// <seealso cref="RwFromFile" />
    /// <seealso cref="RwFromMemory" />
    /// <seealso cref="RwOps.Read" />
    /// <seealso cref="RwOps.Seek" />
    /// <seealso cref="RwOps.Tell" />
    /// <seealso cref="RwOps.Write" />
    public RwOps RwFromFileStream(FileStream fileStream, bool autoClose)
    {
        IntPtr handle =
            Common.GetExport<RwOpsDelegates.RwFromFp>(this, "SDL_RWFromFP", new Version(2, 0, 0))(
                fileStream.SafeFileHandle.DangerousGetHandle(), autoClose);

        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        return new RwOps(this, handle);
    }

    /// <summary>Create a <see cref="RwOps" /> using a read/write <see cref="MemoryBuffer" />.</summary>
    /// <param name="memory">The <see cref="MemoryBuffer" /> to use.</param>
    /// <returns>A new <see cref="RwOps" /> that uses the given read/write <paramref name="memory" /> buffer.</returns>
    /// <exception cref="NativeException">When SDL is unable to create the internal handle.</exception>
    /// <remarks>
    ///     <para>
    ///         The <paramref name="memory" /> buffer must be initialized first through
    ///         <see cref="SafeBuffer.Initialize(uint,uint)" />, <see cref="SafeBuffer.Initialize(ulong)" /> or
    ///         <see cref="SafeBuffer.Initialize{T}" />.
    ///     </para>
    ///     <para>
    ///         If you need to make sure SDL never writes to the <paramref name="memory" /> buffer, use
    ///         <see cref="RwFromConstantMemory" /> instead.
    ///     </para>
    ///     <para>his function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="RwOps.Close" />
    /// <seealso cref="RwFromConstantMemory" />
    /// <seealso cref="RwFromFile" />
    /// <seealso cref="RwFromFileStream" />
    /// <seealso cref="RwOps.Read" />
    /// <seealso cref="RwOps.Seek" />
    /// <seealso cref="RwOps.Tell" />
    /// <seealso cref="RwOps.Write" />
    public RwOps RwFromMemory(MemoryBuffer memory)
    {
        IntPtr handle =
            Common.GetExport<RwOpsDelegates.RwFromMemoryDelegate>(this, "SDL_RWFromMem", new Version(2, 0, 0))(memory,
                (int)memory.ByteLength);

        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        return new RwOps(this, handle);
    }

    /// <summary>Create a <see cref="RwOps" /> using a read-only <see cref="MemoryBuffer" />.</summary>
    /// <param name="memory">The <see cref="MemoryBuffer" /> to use.</param>
    /// <returns>A new <see cref="RwOps" /> that uses the given read-only <paramref name="memory" /> buffer.</returns>
    /// <exception cref="NativeException">When SDL is unable to create the internal handle.</exception>
    /// <remarks>
    ///     <para>If you need to write to the <paramref name="memory" /> buffer, use <see cref="RwFromMemory" /> instead.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="RwOps.Close" />
    /// <seealso cref="RwFromFile" />
    /// <seealso cref="RwFromFileStream" />
    /// <seealso cref="RwFromMemory" />
    /// <seealso cref="RwOps.Read" />
    /// <seealso cref="RwOps.Seek" />
    /// <seealso cref="RwOps.Tell" />
    /// <seealso cref="RwOps.Write" />
    public RwOps RwFromConstantMemory(MemoryBuffer memory)
    {
        IntPtr handle =
            Common.GetExport<RwOpsDelegates.RwFromConstantMemoryDelegate>(this, "SDL_RWFromConstMem",
                new Version(2, 0, 0))(memory, (int)memory.ByteLength);

        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        return new RwOps(this, handle);
    }

    /// <summary>Use this function to allocate an empty, unpopulated <see cref="RwOps" /> class.</summary>
    /// <returns>An empty, unpopulated <see cref="RwOps" /> class.</returns>
    /// <exception cref="NativeException">When SDL is unable to create the internal handle.</exception>
    /// <remarks>
    ///     <para>
    ///         Applications do not need to use this function unless they are providing their own <see cref="RwOps" />
    ///         implementation. If you just need a <see cref="RwOps" /> to read/write a common data source, you should use the
    ///         built-in implementations in SDL, like <see cref="RwFromFile" /> or <see cref="RwFromMemory" />, etc.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="RwOps.Dispose" />
    public RwOps AllocateRw()
    {
        IntPtr handle = Common.GetExport<RwOpsDelegates.AllocRwDelegate>(this, "SDL_AllocRW", new Version(2, 0, 0))();
        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        return new RwOps(this, handle);
    }

    /// <summary>Load all the data from an SDL data stream.</summary>
    /// <param name="source">The <see cref="RwOps" /> data stream to load the data from.</param>
    /// <param name="freeSource">Whether to free the <paramref name="source" /> automatically or not.</param>
    /// <returns>A new <see cref="MemoryBuffer" /> with the data from the <paramref name="source" />.</returns>
    /// <exception cref="NativeException">When SDL is unable to load the data.</exception>
    /// <remarks>
    ///     <para>The data is allocated with a zero byte at the end (null terminated) for convenience.</para>
    ///     <para>This function is available since SDL 2.0.6.</para>
    /// </remarks>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "The API requires this is public")]
    public MemoryBuffer LoadFileRw(RwOps source, bool freeSource)
    {
        IntPtr handle =
            Common.GetExport<RwOpsDelegates.LoadFileRwDelegate>(this, "SDL_LoadFile_RW", new Version(2, 0, 6))(source,
                out CULong dataSize, freeSource);

        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        MemoryBuffer memory = new(handle, true);
        if (memory.ByteLength != dataSize.Value)
        {
            memory.Initialize(dataSize.Value + 1);
        }

        return memory;
    }

    /// <summary>Load all the data from a file path.</summary>
    /// <param name="file">A <see cref="string" /> with the file path.</param>
    /// <returns>A new <see cref="MemoryBuffer" /> with the data from the <paramref name="file" /> path.</returns>
    /// <exception cref="NativeException">When SDL is unable to load the data.</exception>
    /// <remarks>
    ///     <para>The data is allocated with a zero byte at the end (null terminated) for convenience.</para>
    ///     <para>
    ///         Prior to SDL 2.0.10, this function was a wrapper around <see cref="LoadFileRw" />. Since then, it calls the
    ///         native SDL function.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.6.</para>
    /// </remarks>
    public MemoryBuffer LoadFile(string file)
    {
        IntPtr handle;
        CULong dataSize;

        if (Version < new Version(2, 0, 10))
        {
            MemoryBuffer tempMemory = LoadFileRw(RwFromFile(file, FileAccess.Read, FileMode.Open, true), true);
            dataSize = new CULong((uint)tempMemory.ByteLength);
            handle = tempMemory.DangerousGetHandle();
        }
        else
        {
            handle = Common.GetExport<RwOpsDelegates.LoadFileDelegate>(this, "SDL_LoadFile", new Version(2, 0, 10))(
                file,
                out dataSize);
        }


        if (handle == IntPtr.Zero)
        {
            throw new NativeException(LastError);
        }

        MemoryBuffer memory = new(handle, true);
        if (memory.ByteLength != dataSize.Value)
        {
            memory.Initialize(dataSize.Value);
        }

        return memory;
    }
}