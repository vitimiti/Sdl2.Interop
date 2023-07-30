using Sdl2.Interop.Utilities;

namespace Sdl2.Interop;

public partial class Sdl
{
    /// <summary>The types of <see cref="RwOps" /></summary>
    public enum RwOpsType : uint
    {
        /// <summary>Unknown stream type.</summary>
        Unknown,

        /// <summary>Win32 file.</summary>
        WinFile,

        /// <summary>Stdio file.</summary>
        StdFile,

        /// <summary>Android asset.</summary>
        JniFile,

        /// <summary>Memory stream.</summary>
        Memory,

        /// <summary>Read-Only memory stream.</summary>
        MemoryReadOnly
    }
}