using System;
using System.Diagnostics.CodeAnalysis;

namespace Sdl2.Interop.Tests.Unit;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "This is how you define a fixture in xUnit.net")]
public class SdlFixture : IDisposable
{
    public SdlFixture()
    {
        Sdl = Sdl.GetInstance();
    }

    public Sdl Sdl { get; }

    public void Dispose()
    {
        Sdl.Dispose();
    }
}