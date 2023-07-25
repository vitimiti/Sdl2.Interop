using Sdl2.Interop;
using Sdl2.Interop.Utilities;

// Initialize the library.
using Sdl sdl = Sdl.GetInstance();

// Set the log priorities.
sdl.LogSetPriority(MyCategories.Library, Sdl.LogPriority.Information);
sdl.LogSetPriority(MyCategories.Subsystem, Sdl.LogPriority.Information);
sdl.LogSetPriority(MyCategories.Cpu, Sdl.LogPriority.Verbose);
sdl.LogSetPriority(MyCategories.Power, Sdl.LogPriority.Information);

// Set the log output function.
sdl.LogSetOutputFunction((data, category, priority, message) =>
{
    DateTime dateTime = (DateTime)data;

    Console.ForegroundColor = priority switch
    {
        Sdl.LogPriority.Verbose => ConsoleColor.DarkGreen,
        Sdl.LogPriority.Debug => ConsoleColor.Green,
        Sdl.LogPriority.Information => ConsoleColor.Blue,
        Sdl.LogPriority.Warning => ConsoleColor.Yellow,
        Sdl.LogPriority.Error => ConsoleColor.Red,
        Sdl.LogPriority.Critical => ConsoleColor.DarkRed,
        _ => throw new ArgumentOutOfRangeException(nameof(priority), priority, null)
    };

    Console.WriteLine(
        $"[{dateTime.Year:0000}/{dateTime.Month:00}/{dateTime.Day:00} - {dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}] [{(MyCategories)category}] - {message}");

    Console.ResetColor();
}, DateTime.Now);

// Get the library version and running platform.
sdl.LogInformation(MyCategories.Library, $"Running SDLv {sdl.Version} [{sdl.Revision}] on {sdl.Platform}");

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
sdl.LogVerbose(MyCategories.Cpu, $"{"Has LASX",leftAlignment} {sdl.HasLasx,rightAlignment}");

// Print power information.
sdl.LogInformation(MyCategories.Power, "Power information:");
Sdl.PowerState powerState = sdl.GetPowerInformation(out int batterySecondsLeft, out int batteryPercentageLeft);
sdl.LogInformation(MyCategories.Power, $"{"Power state",leftAlignment} {powerState,rightAlignment}");
sdl.LogInformation(MyCategories.Power, "Battery information:");
sdl.LogInformation(MyCategories.Power,
    $"{"Seconds Left",leftAlignment} {(batterySecondsLeft == -1 ? "Unknown" : $"{batterySecondsLeft}s"),rightAlignment}");

sdl.LogInformation(MyCategories.Power,
    $"{"Percentage Left",leftAlignment} {(batteryPercentageLeft == -1 ? "Unknown" : $"{batteryPercentageLeft}%"),rightAlignment}");

// Initialize SDL safely.
using Subsystems subsystems = sdl.Initialize(Sdl.InitializeFlags.Video);

// Show the initialized subsystems.
sdl.LogInformation(MyCategories.Subsystem, $"Initialized subsystems [{sdl.WasInitialized()}]");

internal enum MyCategories
{
    Library = Sdl.LogCategory.Custom,
    Subsystem,
    Cpu,
    Power
}