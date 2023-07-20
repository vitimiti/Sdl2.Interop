using Sdl2.Interop.Internal;
using Sdl2.Interop.Interop;
using Sdl2.Interop.NativeDelegates;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>Get the version of SDL that is linked against your program.</summary>
    /// <returns>A <see cref="Version" /> that contains the version information.</returns>
    /// <remarks>
    ///     <para>This function may be called safely at any time, even before <see cref="Initialize" />.</para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Revision" />
    public Version Version
    {
        get
        {
            Common.GetExport<NativeVersionDelegates.GetVersionDelegate>(this, "SDL_GetVersion", new Version(2, 0, 0))(
                out SdlVersion thisVersion);

            return new Version(thisVersion.Major, thisVersion.Minor, thisVersion.Patch);
        }
    }

    /// <summary>Get the code revision of SDL that is linked against your program.</summary>
    /// <returns>An arbitrary <see cref="string" />, uniquely identifying the exact revision of the SDL library in use.</returns>
    /// <remarks>
    ///     <para>
    ///         The revision is arbitrary string (a hash value) uniquely identifying the exact revision of the SDL library in
    ///         use, and is only useful in comparing against other revisions. It is NOT an incrementing number.
    ///     </para>
    ///     <para>If SDL wasn't built from a git repository with the appropriate tools, this will return an empty string.</para>
    ///     <para>Prior to SDL 2.0.16, before development moved to GitHub, this returned a hash for a Mercurial repository.</para>
    ///     <para>
    ///         You shouldn't use this function for anything but logging it for debugging purposes. The string is not
    ///         intended to be reliable in any way.
    ///     </para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Version" />
    public string Revision =>
        Common.GetExport<NativeVersionDelegates.GetRevisionDelegate>(this, "SDL_GetRevision",
            new Version(2, 0, 0))();

    /// <summary>Obsolete function, do not use.</summary>
    /// <returns>Zero, always, in modern SDL releases.</returns>
    /// <remarks>
    ///     <para>
    ///         When SDL was hosted in a Mercurial repository, and was built carefully, this would return the revision number
    ///         that the build was created from. This number was not reliable for several reasons, but more importantly, SDL is
    ///         now hosted in a git repository, which does not offer numbers at all, only hashes. This function only ever
    ///         returns zero now. Don't use it.
    ///     </para>
    ///     <para>Before SDL 2.0.16, this might have returned an unreliable, but non-zero number.</para>
    ///     <para>Use <see cref="Revision" /> instead; if SDL was carefully built, it will return a git hash.</para>
    ///     <para>This property is available since SDL 2.0.0.</para>
    /// </remarks>
    /// <seealso cref="Revision" />
    [Obsolete("Use GetRevision() instead; if SDL was carefully built, it will return a git hash.", false)]
    public int RevisionNumber =>
        Common.GetExport<NativeVersionDelegates.GetRevisionNumberDelegate>(this, "SDL_GetRevisionNumber",
            new Version(2, 0, 0))();
}