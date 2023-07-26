using FluentAssertions;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.Tests.Unit;

[Collection(Collections.Library)]
public class TimerTests
{
    private readonly SdlFixture _fixture;

    public TimerTests(SdlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void AddTimer_ShouldCreateATimer_WhenGivenNoParameters()
    {
        const uint expectedInterval = 1U;
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Timer))
        {
            using (_fixture.Sdl.AddTimer(expectedInterval, (interval, parameter) =>
                   {
                       interval.Should().Be(expectedInterval);
                       parameter.Should().BeNull();
                       return 0;
                   }, new NullVoidPointer()))
            {
                _fixture.Sdl.Delay(2);
            }
        }
    }

    [Fact]
    public void AddTimer_ShouldCreateATimer_WhenGivenAParameter()
    {
        const uint expectedInterval = 1U;
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Timer))
        {
            using (_fixture.Sdl.AddTimer(expectedInterval, (interval, parameter) =>
                   {
                       interval.Should().Be(expectedInterval);
                       parameter.Should().NotBeNull();
                       parameter.As<string>().Should().Be("Test");
                       return 0;
                   }, "Test"))
            {
                _fixture.Sdl.Delay(2);
            }
        }
    }
}