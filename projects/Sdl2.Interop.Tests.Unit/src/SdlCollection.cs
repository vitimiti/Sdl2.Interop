using Xunit;

namespace Sdl2.Interop.Tests.Unit;

[CollectionDefinition(Collections.Library, DisableParallelization = true)]
public class SdlCollection : ICollectionFixture<SdlFixture>
{
}