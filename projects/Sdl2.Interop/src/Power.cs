using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Get the current power supply details.</summary>
    /// <param name="batterySecondsLeft">
    ///     An <see langword="out" /> parameter with an <see cref="int" /> filled with the seconds
    ///     of battery life left. Will return -1 if we can't determine a value, or we're not running on a battery.
    /// </param>
    /// <param name="batteryPercentageLeft">
    ///     An <see langword="out" /> parameter with an <see cref="int" /> filled with the
    ///     percentage of battery life left, between 0 and 100. Will return -1 if we can't determine a value, or we're not
    ///     running on a battery.
    /// </param>
    /// <returns>The <see cref="PowerState" /> representing the current battery state.</returns>
    /// <remarks>
    ///     <para>
    ///         You should never take a battery status as absolute truth. Batteries (especially failing batteries) are
    ///         delicate hardware, and the values reported here are best estimates based on what that hardware reports. It's
    ///         not uncommon for older batteries to lose stored power much faster than it reports, or completely drain when
    ///         reporting it has 20 percent left, etc.
    ///     </para>
    ///     <para>
    ///         Battery status can change at any time; if you are concerned with power state, you should call this function
    ///         frequently, and perhaps ignore changes until they seem to be stable for a few seconds.
    ///     </para>
    ///     <para>It's possible a platform can only report battery percentage or time left but not both.</para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public PowerState GetPowerInformation(out int batterySecondsLeft, out int batteryPercentageLeft)
    {
        return Common.GetExport<PowerDelegates.GetPowerInfoDelegate>(this, "SDL_GetPowerInfo", new Version(2, 0, 0))(
            out batterySecondsLeft, out batteryPercentageLeft);
    }
}