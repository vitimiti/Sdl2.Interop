using FluentAssertions;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.Tests.Unit;

[Collection(Collections.Library)]
public class SimdTests
{
    private readonly SdlFixture _fixture;
    private readonly uint _length;

    public SimdTests(SdlFixture fixture)
    {
        _fixture = fixture;
        _length = _fixture.Sdl.SimdAlignment;
    }

    [Fact]
    public void SdlSimdAllocate_ShouldAllocateMemory_WhenGivenALength()
    {
        using Simd simd = _fixture.Sdl.SimdAllocate(_length);
        simd.ByteLength.Should().Be(_length);
    }

    [Fact]
    public void SdlSimdReallocate_ShouldReallocateMemory_WhenGivenANewLength()
    {
        using Simd simd = _fixture.Sdl.SimdAllocate(_length);
        simd.Reallocate(_length / 2);
        simd.ByteLength.Should().Be(_length / 2);
    }
}