using Sdl2.Interop;
using Sdl2.Interop.Utilities;

// Initialize the library.
using Sdl sdl = new();

// Set the log priorities.
sdl.LogSetPriority(MyCategories.Library, Sdl.LogPriority.Information);
sdl.LogSetPriority(MyCategories.Subsystem, Sdl.LogPriority.Information);
sdl.LogSetPriority(MyCategories.Cpu, Sdl.LogPriority.Verbose);

// Set the log output function.
sdl.LogSetOutputFunction((data, category, priority, message) =>
{
    DateTime dateTime = (DateTime)data;

    switch (priority)
    {
        case Sdl.LogPriority.Verbose:
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            break;
        case Sdl.LogPriority.Debug:
            Console.ForegroundColor = ConsoleColor.Green;
            break;
        case Sdl.LogPriority.Information:
            Console.ForegroundColor = ConsoleColor.Blue;
            break;
        case Sdl.LogPriority.Warning:
            Console.ForegroundColor = ConsoleColor.Yellow;
            break;
        case Sdl.LogPriority.Error:
            Console.ForegroundColor = ConsoleColor.Red;
            break;
        case Sdl.LogPriority.Critical:
            Console.ForegroundColor = ConsoleColor.DarkRed;
            break;
        default:
            throw new ArgumentOutOfRangeException(nameof(priority), priority, null);
    }

    Console.WriteLine(
        $"[{dateTime.Year:0000}/{dateTime.Month:00}/{dateTime.Day:00} - {dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}] [{(MyCategories)category}] - {message}");

    Console.ResetColor();
}, DateTime.Now);

// Get the library version and running platform.
sdl.LogInformation(MyCategories.Library, $"Running SDLv {sdl.Version} [{sdl.Revision}] on {sdl.Platform}\n");

// Print system information.
const int leftAlignment = -20;
const int rightAlignment = -10;
sdl.LogVerbose(MyCategories.Cpu, "System Information:");
sdl.LogVerbose(MyCategories.Cpu, $"{"CPU Count",leftAlignment} {sdl.CpuCount,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"CPU Cache Line Size",leftAlignment} {$"{sdl.CpuCacheLineSize}B",rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"System RAM",leftAlignment} {$"{sdl.SystemRam / 1024.0:N1}GiB",rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"SIMD Alignment",leftAlignment} {$"{sdl.SimdAlignment}B",rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has RDTSC",leftAlignment} {sdl.HasRdtsc,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has AltiVec",leftAlignment} {sdl.HasAltiVec,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has MMX",leftAlignment} {sdl.HasMmx,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has 3DNow!",leftAlignment} {sdl.Has3DNow,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has SSE",leftAlignment} {sdl.HasSse,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has SSE2",leftAlignment} {sdl.HasSse2,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has SSE3",leftAlignment} {sdl.HasSse3,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has SSE4.1",leftAlignment} {sdl.HasSse41,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has SSE4.2",leftAlignment} {sdl.HasSse42,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has AVX",leftAlignment} {sdl.HasAvx,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has AVX2",leftAlignment} {sdl.HasAvx2,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has AVX-512F",leftAlignment} {sdl.HasAvx512F,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has ARM SIMD",leftAlignment} {sdl.HasArmSimd,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has NEON",leftAlignment} {sdl.HasNeon,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has LSX",leftAlignment} {sdl.HasLsx,rightAlignment}");
sdl.LogVerbose(MyCategories.Cpu, $"{"Has LASX",leftAlignment} {sdl.HasLasx,rightAlignment}\n");

// Initialize SDL safely.
using Subsystems subsystems = sdl.Initialize(Sdl.InitializeFlags.Video);

// Show the initialized subsystems.
sdl.LogInformation(MyCategories.Subsystem, $"Initialized subsystems [{sdl.WasInitialized()}]");

internal enum MyCategories
{
    Library = Sdl.LogCategory.Custom,
    Subsystem,
    Cpu
}