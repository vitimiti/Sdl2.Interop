namespace Sdl2.Interop;

/// <summary>These are the flags which may be passed to <see cref="Sdl.Init" />.</summary>
/// <remarks>You should specify the subsystems which you will be using in your application.</remarks>
[Flags]
public enum SdlInit : uint
{
    /// <summary>No subsystems.</summary>
    None = 0x00000000u,

    /// <summary>The timer subsystem.</summary>
    Timer = 0x00000001U,

    /// <summary>The audio subsystem.</summary>
    /// <remarks>This implies the <see cref="Events" /> subsystem.</remarks>
    Audio = 0x00000010U,

    /// <summary>The video subsystem.</summary>
    /// <remarks>This implies the <see cref="Events" /> subsystem.</remarks>
    Video = 0x00000020U,

    /// <summary>The joystick subsystem.</summary>
    /// <remarks>This implies the <see cref="Events" /> subsystem.</remarks>
    Joystick = 0x00000200U,

    /// <summary>The haptic subsystem.</summary>
    Haptic = 0x00001000U,

    /// <summary>The game controller subsystem.</summary>
    /// <remarks>This implies the <see cref="Joystick" /> and <see cref="Events" /> subsystems.</remarks>
    GameController = 0x00002000U,

    /// <summary>The events subsystem.</summary>
    Events = 0x00004000U,

    /// <summary>The sensor subsystem.</summary>
    /// <remarks>
    ///     <para>This implies the <see cref="Events" /> subsystem.</para>
    ///     <para>This subsystem is available since SDL 2.0.9 and will be ignored on previous versions.</para>
    /// </remarks>
    Sensor = 0x00008000U,

    /// <summary>For compatibility, this flag will be ignored.</summary>
    /// <remarks>If using SDL before 2.0.4, this will cause SDL to not catch fatal signals.</remarks>
    [Obsolete("For compatibility, this flag will be ignored.", false)]
    NoParachute = 0x00100000U,

    /// <summary>Initialize all subsystems.</summary>
    /// <remarks>
    ///     <para>This never includes <see cref="NoParachute" />.</para>
    ///     <para>In versions before 2.0.9 this does NOT include the <see cref="Sensor" /> subsystem.</para>
    /// </remarks>
    Everything = Timer | Audio | Video | Events | Joystick | Haptic | GameController | Sensor
}