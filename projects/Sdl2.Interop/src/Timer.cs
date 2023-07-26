using System.Runtime.InteropServices;

using Sdl2.Interop.CustomMarshalers;
using Sdl2.Interop.Exceptions;
using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;
using Sdl2.Interop.Utilities;

using Timer = Sdl2.Interop.Utilities.Timer;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Function prototype for the timer callback function.</summary>
    /// <param name="interval">A <see cref="uint" /> with the timer interval in milliseconds.</param>
    /// <param name="parameter">
    ///     An <see cref="object" /> with the timer callback parameter or a <see cref="NullVoidPointer" />
    ///     to pass no parameters.
    /// </param>
    /// <returns>A <see cref="uint" /> with the next timer interval or 0 to cancel the timer.</returns>
    /// <remarks>
    ///     The callback function is passed the current timer interval and returns the next timer interval. If the
    ///     returned value is the same as the one passed in, the periodic alarm continues, otherwise a new alarm is scheduled.
    ///     If the callback returns 0, the periodic alarm is cancelled.
    /// </remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate uint TimerCallback(uint interval,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(VoidPointerCustomMarshaler),
            MarshalCookie = Cookies.LeaveAllocated)]
        object parameter);

    /// <summary>Get the number of milliseconds since SDL library initialization.</summary>
    /// <remarks>
    ///     <para>This value wraps if the program runs for more than ~49 days.</para>
    ///     <para>
    ///         This function is not recommended as of SDL 2.0.18; use <see cref="Ticks64" /> instead, where the value
    ///         doesn't wrap every ~49 days. There are places in SDL where we provide a 32-bit timestamp that can not change
    ///         without breaking binary compatibility, though, so this function isn't officially deprecated.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public uint Ticks =>
        Common.GetExport<TimerDelegates.GetTicksDelegate>(this, "SDL_GetTicks", new Version(2, 0, 0))();

    /// <summary>Get the number of milliseconds since SDL library initialization.</summary>
    /// <remarks>
    ///     <para>
    ///         Note that you should not use the <see cref="TicksPassed" /> function with values returned by this function,
    ///         as that function does clever math to compensate for the 32-bit overflow every ~49 days that
    ///         <see cref="Ticks" /> suffers from. 64-bit values from this function can be safely compared directly.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.18.</para>
    /// </remarks>
    /// <example>
    ///     For example, if you want to wait 100 ms, you could do this:
    ///     <code>
    /// const ulong timeOut = sdl.Ticks64 + 100;
    /// while (sdl.Ticks64 <![CDATA[<]]> timeOut)
    /// {
    ///     // Do work until timeout has elapsed
    /// }
    ///     </code>
    /// </example>
    public ulong Ticks64 =>
        Common.GetExport<TimerDelegates.GetTicks64Delegate>(this, "SDL_GetTicks64", new Version(2, 0, 18))();

    /// <summary>Get the current value of the high resolution counter.</summary>
    /// <remarks>
    ///     <para>This function is typically used for profiling.</para>
    ///     <para>
    ///         The counter values are only meaningful relative to each other. Differences between values can be converted to
    ///         times by using <see cref="PerformanceFrequency" />.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="PerformanceFrequency" />
    public ulong PerformanceCounter =>
        Common.GetExport<TimerDelegates.GetPerformanceCounterDelegate>(this, "SDL_GetPerformanceCounter",
            new Version(2, 0, 0))();

    /// <summary>Get the count per second of the high resolution counter.</summary>
    /// <remarks>This function is available since SDL 2.0.0.</remarks>
    /// <seealso cref="PerformanceCounter" />
    public ulong PerformanceFrequency =>
        Common.GetExport<TimerDelegates.GetPerformanceFrequencyDelegate>(this, "SDL_GetPerformanceFrequency",
            new Version(2, 0, 0))();

    /// <summary>
    ///     Compare 32-bit SDL ticks values, and return true if <paramref name="current" /> has passed
    ///     <paramref name="timeOut" />.
    /// </summary>
    /// <param name="current">A <see cref="uint" /> with the current 32-bit SDL ticks values.</param>
    /// <param name="timeOut">A <see cref="uint" /> with the target 32-bit SDL ticks values.</param>
    /// <returns>
    ///     <see langword="true" /> when the <paramref name="current" /> ticks have passed the <paramref name="timeOut" />
    ///     .
    /// </returns>
    /// <remarks>
    ///     <para>
    ///         This should be used with results from <see cref="Ticks" />, as this function attempts to deal with the 32-bit
    ///         counter wrapping back to zero every ~49 days, but should <b>not</b> be used with <see cref="Ticks64" />, which
    ///         does not have that problem.
    ///     </para>
    ///     <para>
    ///         Note that this does not handle tick differences greater than 2^31 so take care when using the example kind of
    ///         code with large timeout delays (tens of days).
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <example>
    ///     For example, with <see cref="Ticks" />, if you want to wait 100 ms, you could do this:
    ///     <code>
    /// const uint timeOut = sdl.Ticks + 100;
    /// while (!sdl.TicksPassed(sdl.Ticks, timeOut))
    /// {
    ///     // Do work until timeout has elapsed
    /// }
    ///     </code>
    /// </example>
    public bool TicksPassed(uint current, uint timeOut)
    {
        return (int)(timeOut - current) <= 0;
    }

    /// <summary>Wait a specified number of milliseconds before returning.</summary>
    /// <param name="milliseconds">A <see cref="uint" /> with the milliseconds to delay.</param>
    /// <remarks>
    ///     <para>
    ///         This function waits a specified number of milliseconds before returning. It waits at least the specified
    ///         time, but possibly longer due to OS scheduling.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public void Delay(uint milliseconds)
    {
        Common.GetExport<TimerDelegates.DelayDelegate>(this, "SDL_Delay", new Version(2, 0, 0))(milliseconds);
    }

    /// <summary>Call a callback function at a future time.</summary>
    /// <param name="interval">
    ///     A <see cref="uint" /> with the timer delay, in milliseconds, passed to
    ///     <paramref name="callback" />.
    /// </param>
    /// <param name="callback">
    ///     A <see cref="TimerCallback" /> function to call when the specified <paramref name="interval" />
    ///     elapses.
    /// </param>
    /// <param name="parameter">An <see cref="object" /> that is passed to <paramref name="callback" />.</param>
    /// <returns>A new <see cref="Timer" /> class.</returns>
    /// <exception cref="NativeException">When SDL is unable to create a new <see cref="Timer" />.</exception>
    /// <remarks>
    ///     <para>If you use this function, you must pass <see cref="InitializeFlags.Timer" /> to <see cref="Initialize" />.</para>
    ///     <para>
    ///         The callback function is passed the current timer interval and the user supplied parameter from the
    ///         <see cref="AddTimer" /> call and should return the next timer interval. If the value returned from the callback
    ///         is 0, the timer is canceled.
    ///     </para>
    ///     <para>The callback is run on a separate thread.</para>
    ///     <para>
    ///         Timers take into account the amount of time it took to execute the callback. For example, if the callback
    ///         took 250ms to execute and returned 1000ms, the timer would only wait another 750ms before its next iteration.
    ///     </para>
    ///     <para>
    ///         Timing may be inexact due to OS scheduling. Be sure to note the current time with <see cref="Ticks" /> or
    ///         <see cref="PerformanceCounter" /> in case your callback needs to adjust for variances.
    ///     </para>
    ///     <para>This function is available since SDL 2.0.0.</para>
    /// </remarks>
    public Timer AddTimer(uint interval, TimerCallback callback, object parameter)
    {
        int result =
            Common.GetExport<TimerDelegates.AddTimerDelegate>(this, "SDL_AddTimer", new Version(2, 0, 0))(interval,
                callback, parameter);

        if (result <= 0)
        {
            throw new NativeException(LastError, result);
        }

        return new Timer(this, result);
    }
}