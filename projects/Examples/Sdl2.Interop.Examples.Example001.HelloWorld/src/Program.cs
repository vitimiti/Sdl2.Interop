using Sdl2.Interop;
using Sdl2.Interop.Utilities;

// Initialize the library.
using Sdl sdl = new();

// Get the library version and running platform.
Console.WriteLine($"Running SDLv {sdl.Version} [{sdl.Revision}] on {sdl.Platform}\n");

// Print system information.
const int leftAlignment = 20;
const int rightAlignment = -10;
Console.WriteLine("System Information:");
Console.WriteLine($"{"CPU Count",leftAlignment} {sdl.CpuCount,rightAlignment}");
Console.WriteLine($"{"CPU Cache Line Size",leftAlignment} {$"{sdl.CpuCacheLineSize}B",rightAlignment}");
Console.WriteLine($"{"System RAM",leftAlignment} {$"{sdl.SystemRam / 1024.0:N1}GiB",rightAlignment}");
Console.WriteLine($"{"SIMD Alignment",leftAlignment} {$"{sdl.SimdAlignment}B",rightAlignment}");
Console.WriteLine($"{"Has RDTSC",leftAlignment} {sdl.HasRdtsc,rightAlignment}");
Console.WriteLine($"{"Has AltiVec",leftAlignment} {sdl.HasAltiVec,rightAlignment}");
Console.WriteLine($"{"Has MMX",leftAlignment} {sdl.HasMmx,rightAlignment}");
Console.WriteLine($"{"Has 3DNow!",leftAlignment} {sdl.Has3DNow,rightAlignment}");
Console.WriteLine($"{"Has SSE",leftAlignment} {sdl.HasSse,rightAlignment}");
Console.WriteLine($"{"Has SSE2",leftAlignment} {sdl.HasSse2,rightAlignment}");
Console.WriteLine($"{"Has SSE3",leftAlignment} {sdl.HasSse3,rightAlignment}");
Console.WriteLine($"{"Has SSE4.1",leftAlignment} {sdl.HasSse41,rightAlignment}");
Console.WriteLine($"{"Has SSE4.2",leftAlignment} {sdl.HasSse42,rightAlignment}");
Console.WriteLine($"{"Has AVX",leftAlignment} {sdl.HasAvx,rightAlignment}");
Console.WriteLine($"{"Has AVX2",leftAlignment} {sdl.HasAvx2,rightAlignment}");
Console.WriteLine($"{"Has AVX-512F",leftAlignment} {sdl.HasAvx512F,rightAlignment}");
Console.WriteLine($"{"Has ARM SIMD",leftAlignment} {sdl.HasArmSimd,rightAlignment}");
Console.WriteLine($"{"Has NEON",leftAlignment} {sdl.HasNeon,rightAlignment}");
Console.WriteLine($"{"Has LSX",leftAlignment} {sdl.HasLsx,rightAlignment}");
Console.WriteLine($"{"Has LASX",leftAlignment} {sdl.HasLasx,rightAlignment}\n");

// Initialize SDL safely.
using Subsystems subsystems = sdl.Initialize(SdlInit.Video);

// Show the initialized subsystems.
Console.WriteLine($"Initialized subsystems [{sdl.WasInitialized()}]");