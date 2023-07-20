namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>The predefined log categories.</summary>
    /// <remarks>
    ///     By default the <see cref="Application" /> category is enabled at the
    ///     <see cref="Sdl.LogPriority.Information" /> level, the <see cref="Assert" /> category is enabled at the
    ///     <see cref="Sdl.LogPriority.Warning" /> level, <see cref="Test" /> is enabled at the
    ///     <see cref="Sdl.LogPriority.Verbose" /> level and all other categories are enabled at the
    ///     <see cref="Sdl.LogPriority.Critical" /> level.
    /// </remarks>
    public enum LogCategory
    {
        /// <summary>The application category.</summary>
        Application,

        /// <summary>The error category.</summary>
        Error,

        /// <summary>The assert category.</summary>
        Assert,

        /// <summary>The system category.</summary>
        System,

        /// <summary>The audio category.</summary>
        Audio,

        /// <summary>The video category.</summary>
        Video,

        /// <summary>The render category.</summary>
        Render,

        /// <summary>The input category.</summary>
        Input,

        /// <summary>The test category.</summary>
        Test,

        /// <summary>Reserved for future SDL library use.</summary>
        Reserved1,

        /// <inheritdoc cref="Reserved1" />
        Reserved2,

        /// <inheritdoc cref="Reserved1" />
        Reserved3,

        /// <inheritdoc cref="Reserved1" />
        Reserved4,

        /// <inheritdoc cref="Reserved1" />
        Reserved5,

        /// <inheritdoc cref="Reserved1" />
        Reserved6,

        /// <inheritdoc cref="Reserved1" />
        Reserved7,

        /// <inheritdoc cref="Reserved1" />
        Reserved8,

        /// <inheritdoc cref="Reserved1" />
        Reserved9,

        /// <inheritdoc cref="Reserved1" />
        Reserved10,

        /// <summary>Beyond this point is reserved for application use.</summary>
        /// <example>
        ///     <code>
        /// enum MyAppCategory {
        ///     Awesome1 = Sdl.LogCategory.Custom,
        ///     Awesome2,
        ///     Awesome3,
        ///     ...
        /// };
        ///     </code>
        /// </example>
        Custom
    }
}