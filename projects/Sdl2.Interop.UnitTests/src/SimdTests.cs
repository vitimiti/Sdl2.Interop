using System;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.UnitTests;

[Collection(Collections.Library)]
public class SimdTests : IDisposable
{
    private readonly uint _length;
    private readonly Sdl _sdl;

    public SimdTests()
    {
        _sdl = new Sdl();
        _length = _sdl.SimdAlignment;
    }

    public void Dispose()
    {
        _sdl.Dispose();
    }

    [Fact]
    public void AllocatingMemoryWorks()
    {
        using Simd simd = _sdl.SimdAllocate(_length);
        Assert.Equal(_length, simd.Length);
    }

    [Fact]
    public void ReallocatingMemoryWorks()
    {
        using Simd simd = _sdl.SimdAllocate(_length);
        simd.Reallocate(_length / 2);
        Assert.Equal(_length / 2, simd.Length);
    }
}