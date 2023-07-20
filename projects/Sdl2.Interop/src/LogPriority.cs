namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>The predefined log priorities.</summary>
    public enum LogPriority
    {
        /// <summary>The verbose priority.</summary>
        Verbose = 1,

        /// <summary>The debug priority.</summary>
        Debug,

        /// <summary>The information priority.</summary>
        Information,

        /// <summary>The warning priority.</summary>
        Warning,

        /// <summary>The error priority.</summary>
        Error,

        /// <summary>The critical priority.</summary>
        Critical
    }
}