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

    public static SdlInit RemoveUnusedInitFlags(Sdl sdl, SdlInit flags)
    {
        SdlInit finalFlags = flags;
#pragma warning disable CS0618
        if (sdl.Version > new Version(2, 0, 4) && flags.HasFlag(SdlInit.NoParachute))
        {
            finalFlags &= ~SdlInit.NoParachute;
#pragma warning restore CS0618
        }

        if (sdl.Version < new Version(2, 0, 9) && flags.HasFlag(SdlInit.Sensor))
        {
            finalFlags &= ~SdlInit.Sensor;
        }

        return finalFlags;
    }
}