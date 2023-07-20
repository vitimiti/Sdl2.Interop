using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.Utilities;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Get the number of CPU cores available.</summary>
    /// <returns>
    ///     An <see cref="int" /> with the total number of logical CPU cores. On CPUs that include technologies such as
    ///     hyperthreading, the number of logical cores may be more than the number of physical cores.
    /// </returns>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    public int CpuCount =>
        Common.GetExport<CpuInformationDelegates.GetCpuCountDelegate>(this, "SDL_GetCPUCount", new Version(2, 0, 0))();

    /// <summary>Determine the L1 cache line size of the CPU.</summary>
    /// <returns>An <see cref="int" /> with the L1 cache line size of the CPU, in bytes.</returns>
    /// <remarks>
    ///     <para>This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public int CpuCacheLineSize =>
        Common.GetExport<CpuInformationDelegates.GetCpuCacheLineSize>(this, "SDL_GetCPUCacheLineSize",
            new Version(2, 0, 0))();

    /// <summary>Get the amount of RAM configured in the system.</summary>
    /// <returns>An <see cref="int" /> with the amount of RAM configured in the system in MiB.</returns>
    /// <remarks>This function is available since SDL 2.0.1.</remarks>
    public int SystemRam =>
        Common.GetExport<CpuInformationDelegates.GetSystemRamDelegate>(this, "SDL_GetSystemRAM",
            new Version(2, 0, 1))();

    /// <summary>Determine whether the CPU has the RDTSC instruction.</summary>
    /// <returns>SDL_TRUE if the CPU has the RDTSC instruction or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasRdtsc =>
        Common.GetExport<CpuInformationDelegates.HasRdtsc>(this, "SDL_HasRDTSC", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has AltiVec features.</summary>
    /// <returns>SDL_TRUE if the CPU has AltiVec features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using PowerPC instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasAltiVec =>
        Common.GetExport<CpuInformationDelegates.HasAltiVec>(this, "SDL_HasAltiVec", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has MMX features.</summary>
    /// <returns>SDL_TRUE if the CPU has MMX features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasMmx => Common.GetExport<CpuInformationDelegates.HasMmx>(this, "SDL_HasMMX", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has 3DNow! features.</summary>
    /// <returns>SDL_TRUE if the CPU has 3DNow! features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using AMD instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool Has3DNow =>
        Common.GetExport<CpuInformationDelegates.Has3DNow>(this, "SDL_Has3DNow", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has SSE features.</summary>
    /// <returns>SDL_TRUE if the CPU has SSE features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasSse => Common.GetExport<CpuInformationDelegates.HasSse>(this, "SDL_HasSSE", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has SSE2 features.</summary>
    /// <returns>SDL_TRUE if the CPU has SSE2 features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasSse2 =>
        Common.GetExport<CpuInformationDelegates.HasSse2>(this, "SDL_HasSSE2", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has SSE3 features.</summary>
    /// <returns>SDL_TRUE if the CPU has SSE3 features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasSse3 =>
        Common.GetExport<CpuInformationDelegates.HasSse3>(this, "SDL_HasSSE3", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has SSE4.1 features.</summary>
    /// <returns>SDL_TRUE if the CPU has SSE4.1 features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse42" />
    public bool HasSse41 =>
        Common.GetExport<CpuInformationDelegates.HasSse41>(this, "SDL_HasSSE41", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has SSE4.2 features.</summary>
    /// <returns>SDL_TRUE if the CPU has SSE4.2 features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    public bool HasSse42 =>
        Common.GetExport<CpuInformationDelegates.HasSse42>(this, "SDL_HasSSE42", new Version(2, 0, 0))();

    /// <summary>Determine whether the CPU has AVX features.</summary>
    /// <returns>SDL_TRUE if the CPU has AVX features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.2.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx2" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasAvx => Common.GetExport<CpuInformationDelegates.HasAvx>(this, "SDL_HasAVX", new Version(2, 0, 2))();

    /// <summary>Determine whether the CPU has AVX2 features.</summary>
    /// <returns>SDL_TRUE if the CPU has AVX2 features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.4.</para>
    /// </remarks>
    /// <seealso cref="Has3DNow" />
    /// <seealso cref="HasAltiVec" />
    /// <seealso cref="HasAvx" />
    /// <seealso cref="HasMmx" />
    /// <seealso cref="HasRdtsc" />
    /// <seealso cref="HasSse" />
    /// <seealso cref="HasSse2" />
    /// <seealso cref="HasSse3" />
    /// <seealso cref="HasSse41" />
    /// <seealso cref="HasSse42" />
    public bool HasAvx2 =>
        Common.GetExport<CpuInformationDelegates.HasAvx2>(this, "SDL_HasAVX2", new Version(2, 0, 4))();

    /// <summary>Determine whether the CPU has AVX-512F (foundation) features.</summary>
    /// <returns>SDL_TRUE if the CPU has AVX-512F features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using Intel instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.9.</para>
    /// </remarks>
    /// <seealso cref="HasAvx" />
    public bool HasAvx512F =>
        Common.GetExport<CpuInformationDelegates.HasAvx512F>(this, "SDL_HasAVX512F", new Version(2, 0, 9))();

    /// <summary>Determine whether the CPU has ARM SIMD (ARMv6) features.</summary>
    /// <returns>SDL_TRUE if the CPU has ARM SIMD features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This is different from ARM NEON, which is a different instruction set.</para>
    ///     <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.12.</para>
    /// </remarks>
    /// <seealso cref="HasNeon" />
    public bool HasArmSimd =>
        Common.GetExport<CpuInformationDelegates.HasArmSimd>(this, "SDL_HasARMSIMD", new Version(2, 0, 12))();

    /// <summary>Determine whether the CPU has NEON (ARM SIMD) features.</summary>
    /// <returns>SDL_TRUE if the CPU has ARM NEON features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using ARM instruction sets.</para>
    ///     <para>This function is available since SDL 2.0.6.</para>
    /// </remarks>
    public bool HasNeon =>
        Common.GetExport<CpuInformationDelegates.HasNeon>(this, "SDL_HasNEON", new Version(2, 0, 6))();

    /// <summary>Determine whether the CPU has LSX (LOONGARCH SIMD) features.</summary>
    /// <returns>SDL_TRUE if the CPU has LOONGARCH LSX features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using LOONGARCH instruction sets.</para>
    ///     <para>This function is available since SDL 2.24.0.</para>
    /// </remarks>
    public bool HasLsx =>
        Common.GetExport<CpuInformationDelegates.HasLsx>(this, "SDL_HasLSX", new Version(2, 24, 0))();

    /// <summary>Determine whether the CPU has LASX (LOONGARCH SIMD) features.</summary>
    /// <returns>SDL_TRUE if the CPU has LOONGARCH LASX features or SDL_FALSE if not.</returns>
    /// <remarks>
    ///     <para>This always returns false on CPUs that aren't using LOONGARCH instruction sets.</para>
    ///     <para>This function is available since SDL 2.24.0.</para>
    /// </remarks>
    public bool HasLasx =>
        Common.GetExport<CpuInformationDelegates.HasLasx>(this, "SDL_HasLASX", new Version(2, 24, 0))();

    /// <summary>Report the alignment this system needs for SIMD allocations.</summary>
    /// <returns>A <see cref="uint" /> with the alignment in bytes needed for available, known SIMD instructions.</returns>
    /// <remarks>
    ///     <para>
    ///         This will return the minimum number of bytes to which a pointer must be aligned to be compatible with SIMD
    ///         instructions on the current machine. For example, if the machine supports SSE only, it will return 16, but if
    ///         it supports AVX-512F, it'll return 64 (etc). This only reports values for instruction sets SDL knows about, so
    ///         if your SDL build doesn't have <see cref="HasAvx512F" />, then it might return 16 for the SSE support it sees
    ///         and not 64 for the AVX-512 instructions that exist but SDL doesn't know about. Plan accordingly.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.10.</para>
    /// </remarks>
    public uint SimdAlignment =>
        (uint)Common.GetExport<CpuInformationDelegates.SimdGetAlignment>(this, "SDL_SIMDGetAlignment",
            new Version(2, 0, 10))().Value;

    /// <summary>Allocate memory in a SIMD-friendly way.</summary>
    /// <param name="length">
    ///     A <see cref="uint" /> with the length, in bytes, of the block to allocate. The actual allocated
    ///     block might be larger due to padding, etc.
    /// </param>
    /// <returns>A new <see cref="Simd" /> class with a newly allocated block of memory.</returns>
    /// <exception cref="NativeException">When SDL is unable to allocate the block of memory.</exception>
    /// <remarks>
    ///     <para>
    ///         This will allocate a block of memory that is suitable for use with SIMD instructions. Specifically, it will
    ///         be properly aligned and padded for the system's supported vector instructions.
    ///     </para>
    ///     <para>
    ///         The memory returned will be padded such that it is safe to read or write an incomplete vector at the end of
    ///         the memory block. This can be useful so you don't have to drop back to a scalar fallback at the end of your
    ///         SIMD processing loop to deal with the final elements without overflowing the allocated buffer.
    ///     </para>
    ///     <para>
    ///         Note that SDL will only deal with SIMD instruction sets it is aware of; for example, SDL 2.0.8 knows that SSE
    ///         wants 16-byte vectors (<see cref="HasSse" />), and AVX2 wants 32 bytes (<see cref="HasAvx2" />), but doesn't
    ///         know that AVX-512 wants 64. To be clear: if you can't decide to use an instruction set with an Sdl.Has*
    ///         property, don't use that instruction set with memory allocated through here.
    ///     </para>
    ///     <para>
    ///         <see cref="SimdAllocate" /> with a <paramref name="length" /> of 0 will return a valid <see cref="Simd" />
    ///         class, assuming the system isn't out of memory, but you only own zero bytes of that buffer.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.10.</para>
    /// </remarks>
    /// <seealso cref="SimdAlignment" />
    /// <seealso cref="Simd.Reallocate" />
    public Simd SimdAllocate(uint length)
    {
        return new Simd(this,
            Common.GetExport<CpuInformationDelegates.SimdAlloc>(this, "SDL_SIMDAlloc", new Version(2, 0, 10))(
                new CULong(length)));
    }
}