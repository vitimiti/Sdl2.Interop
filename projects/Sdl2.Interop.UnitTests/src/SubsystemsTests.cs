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
    public void InitializingWorks(Sdl.InitializeFlags expected, Sdl.InitializeFlags actual)
    {
        using (Subsystems subsystems = _sdl.Initialize(actual))
        {
            Assert.Equal(expected, _sdl.WasInitialized());
        }

        Assert.Equal(Sdl.InitializeFlags.None, _sdl.WasInitialized());
    }

    [Theory]
    [MemberData(nameof(SubsystemsData))]
    public void InitializingPostInitialInitializationWorks(Sdl.InitializeFlags expected, Sdl.InitializeFlags actual)
    {
        using (Subsystems subsystems = _sdl.Initialize(Sdl.InitializeFlags.Timer))
        {
            subsystems.Stop(Sdl.InitializeFlags.Timer);
            Assert.Equal(Sdl.InitializeFlags.None, _sdl.WasInitialized());
            subsystems.Start(actual);
            Assert.Equal(expected, _sdl.WasInitialized());
        }

        Assert.Equal(Sdl.InitializeFlags.None, _sdl.WasInitialized());
    }

    [Theory]
    [MemberData(nameof(SubsystemsData))]
    public void StoppingPostInitializationWorks(Sdl.InitializeFlags expected, Sdl.InitializeFlags actual)
    {
        using (Subsystems subsystems = _sdl.Initialize(actual))
        {
            Assert.Equal(expected, _sdl.WasInitialized());
            subsystems.Stop(actual);
            Assert.Equal(Sdl.InitializeFlags.None, _sdl.WasInitialized());
        }

        Assert.Equal(Sdl.InitializeFlags.None, _sdl.WasInitialized());
    }

    public static IEnumerable<object[]> SubsystemsData()
    {
        yield return new object[] { Sdl.InitializeFlags.Timer, Sdl.InitializeFlags.Timer };
        yield return new object[] { Sdl.InitializeFlags.Audio | Sdl.InitializeFlags.Events, Sdl.InitializeFlags.Audio };
        yield return new object[] { Sdl.InitializeFlags.Video | Sdl.InitializeFlags.Events, Sdl.InitializeFlags.Video };
        yield return new object[]
        {
            Sdl.InitializeFlags.Joystick | Sdl.InitializeFlags.Events, Sdl.InitializeFlags.Joystick
        };
        yield return new object[] { Sdl.InitializeFlags.Haptic, Sdl.InitializeFlags.Haptic };
        yield return new object[]
        {
            Sdl.InitializeFlags.GameController | Sdl.InitializeFlags.Joystick | Sdl.InitializeFlags.Events,
            Sdl.InitializeFlags.GameController
        };
        yield return new object[] { Sdl.InitializeFlags.Events, Sdl.InitializeFlags.Events };

        using Sdl sdl = new();
        if (sdl.Version >= new Version(2, 0, 9))
        {
            yield return new object[]
            {
                Sdl.InitializeFlags.Sensor | Sdl.InitializeFlags.Events, Sdl.InitializeFlags.Sensor
            };
        }

        if (sdl.Version < new Version(2, 0, 4))
        {
#pragma warning disable CS0618
            yield return new object[] { Sdl.InitializeFlags.NoParachute, Sdl.InitializeFlags.NoParachute };
#pragma warning restore CS0618
        }

        yield return new object[]
        {
            sdl.Version >= new Version(2, 0, 9)
                ? Sdl.InitializeFlags.Timer | Sdl.InitializeFlags.Audio | Sdl.InitializeFlags.Video |
                  Sdl.InitializeFlags.Events |
                  Sdl.InitializeFlags.Joystick |
                  Sdl.InitializeFlags.Haptic | Sdl.InitializeFlags.GameController | Sdl.InitializeFlags.Sensor
                : Sdl.InitializeFlags.Timer | Sdl.InitializeFlags.Audio | Sdl.InitializeFlags.Video |
                  Sdl.InitializeFlags.Events |
                  Sdl.InitializeFlags.Joystick |
                  Sdl.InitializeFlags.Haptic | Sdl.InitializeFlags.GameController,
            Sdl.InitializeFlags.Everything
        };
    }
}