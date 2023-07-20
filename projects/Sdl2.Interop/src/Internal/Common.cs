using System.Runtime.InteropServices;

using Sdl2.Interop.Exceptions;

namespace Sdl2.Interop.Internal;

internal static class Common
{
    public static TDelegate GetExport<TDelegate>(Sdl sdl, string name, Version minimumVersionRequired)
    {
        try
        {
            return Marshal.GetDelegateForFunctionPointer<TDelegate>(NativeLibrary.GetExport(sdl.Handle, name));
        }
        catch (ArgumentNullException e)
        {
            throw new MinimumVersionRequiredNotMatchedException(
                $"The SDL2 export '{name}' expected at least SDL v{minimumVersionRequired} but v{sdl.Version} was found.",
                e);
        }
    }

    public static Sdl.InitializeFlags RemoveUnusedInitFlags(Sdl sdl, Sdl.InitializeFlags flags)
    {
        Sdl.InitializeFlags finalFlags = flags;
#pragma warning disable CS0618
        if (sdl.Version > new Version(2, 0, 4) && flags.HasFlag(Sdl.InitializeFlags.NoParachute))
        {
            finalFlags &= ~Sdl.InitializeFlags.NoParachute;
#pragma warning restore CS0618
        }

        if (sdl.Version < new Version(2, 0, 9) && flags.HasFlag(Sdl.InitializeFlags.Sensor))
        {
            finalFlags &= ~Sdl.InitializeFlags.Sensor;
        }

        return finalFlags;
    }
}