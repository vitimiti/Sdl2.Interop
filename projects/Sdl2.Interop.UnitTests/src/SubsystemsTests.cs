using System;
using System.Collections.Generic;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.UnitTests;

public class SubsystemsTests
{
    private readonly Sdl _sdl;

    public SubsystemsTests()
    {
        _sdl = new Sdl();
    }

    [Theory]
    [MemberData(nameof(SubsystemsData))]
    public void InitializingWorks(SdlInit expected, SdlInit actual)
    {
        using (Subsystems subsystems = _sdl.Init(actual))
        {
            Assert.Equal(expected, _sdl.WasInit());
        }

        Assert.Equal(SdlInit.None, _sdl.WasInit());
    }

    [Theory]
    [MemberData(nameof(SubsystemsData))]
    public void InitializingPostInitialInitializationWorks(SdlInit expected, SdlInit actual)
    {
        using (Subsystems subsystems = _sdl.Init(SdlInit.Timer))
        {
            subsystems.Stop(SdlInit.Timer);
            Assert.Equal(SdlInit.None, _sdl.WasInit());
            subsystems.Start(actual);
            Assert.Equal(expected, _sdl.WasInit());
        }

        Assert.Equal(SdlInit.None, _sdl.WasInit());
    }

    [Theory]
    [MemberData(nameof(SubsystemsData))]
    public void StoppingPostInitializationWorks(SdlInit expected, SdlInit actual)
    {
        using (Subsystems subsystems = _sdl.Init(actual))
        {
            Assert.Equal(expected, _sdl.WasInit());
            subsystems.Stop(actual);
            Assert.Equal(SdlInit.None, _sdl.WasInit());
        }

        Assert.Equal(SdlInit.None, _sdl.WasInit());
    }

    public static IEnumerable<object[]> SubsystemsData()
    {
        yield return new object[] { SdlInit.Timer, SdlInit.Timer };
        yield return new object[] { SdlInit.Audio | SdlInit.Events, SdlInit.Audio };
        yield return new object[] { SdlInit.Video | SdlInit.Events, SdlInit.Video };
        yield return new object[] { SdlInit.Joystick | SdlInit.Events, SdlInit.Joystick };
        yield return new object[] { SdlInit.Haptic, SdlInit.Haptic };
        yield return new object[]
        {
            SdlInit.GameController | SdlInit.Joystick | SdlInit.Events, SdlInit.GameController
        };
        yield return new object[] { SdlInit.Events, SdlInit.Events };

        using Sdl sdl = new();
        if (sdl.Version >= new Version(2, 0, 9))
        {
            yield return new object[] { SdlInit.Sensor | SdlInit.Events, SdlInit.Sensor };
        }

        if (sdl.Version < new Version(2, 0, 4))
        {
#pragma warning disable CS0618
            yield return new object[] { SdlInit.NoParachute, SdlInit.NoParachute };
#pragma warning restore CS0618
        }

        yield return new object[]
        {
            sdl.Version >= new Version(2, 0, 9)
                ? SdlInit.Timer | SdlInit.Audio | SdlInit.Video | SdlInit.Events | SdlInit.Joystick |
                  SdlInit.Haptic | SdlInit.GameController | SdlInit.Sensor
                : SdlInit.Timer | SdlInit.Audio | SdlInit.Video | SdlInit.Events | SdlInit.Joystick |
                  SdlInit.Haptic | SdlInit.GameController,
            SdlInit.Everything
        };
    }
}