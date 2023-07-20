using Sdl2.Interop.Internal;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Get the name of the platform.</summary>
    /// <value>
    ///     A <see cref="string" /> with the name of the platform. If the correct platform name is not available, returns a
    ///     <see cref="string" /> beginning with the text "Unknown".
    /// </value>
    /// <remarks>
    ///     <para>
    ///         Here are the names returned for some (but not all) supported platforms:
    ///         <list type="bullet">
    ///             <item>"Windows"</item>
    ///             <item>"Mac OS X"</item>
    ///             <item>"Linux"</item>
    ///             <item>"iOS"</item>
    ///             <item>"Android"</item>
    ///         </list>
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    public string Platform =>
        Common.GetExport<PlatformDelegates.GetPlatformDelegate>(this, "SDL_GetPlatform", new Version(2, 0, 0))();
}