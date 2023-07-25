using System;

using Sdl2.Interop.Exceptions;

using Xunit;

namespace Sdl2.Interop.Tests.Unit;

[Collection(Collections.Library)]
public class ClipboardTests
{
    private readonly SdlFixture _fixture;
    private readonly string _testText;

    public ClipboardTests(SdlFixture fixture)
    {
        _fixture = fixture;
        _testText = "Test";
    }

    private void ResetClipboardText()
    {
        _fixture.Sdl.ClipboardText = string.Empty;
    }

    private void ResetPrimarySelectionText()
    {
        _fixture.Sdl.PrimarySelectionText = string.Empty;
    }

    [Fact]
    public void SdlClipboardText_ShouldThrowException_WhenVideoNotInitialized()
    {
        Assert.Throws<NativeException>(() =>
        {
            _fixture.Sdl.ClipboardText = _testText;
        });
    }

    [Fact]
    public void SdlClipboardText_ShouldThrowException_WhenStringEmpty()
    {
        Assert.Throws<NativeException>(() =>
        {
            using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetClipboardText();
                return _fixture.Sdl.ClipboardText;
            }
        });
    }

    [Fact]
    public void SdlClipboardText_ShouldBeSet_WhenGivenString()
    {
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            _fixture.Sdl.ClipboardText = _testText;
            Assert.Equal(_testText, _fixture.Sdl.ClipboardText);
        }
    }

    [Fact]
    public void SdlHasClipboardText_ShouldWork_WhenClipboardHasTextOrNot()
    {
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            Assert.False(_fixture.Sdl.HasClipboardText);
            _fixture.Sdl.ClipboardText = _testText;
            Assert.True(_fixture.Sdl.HasClipboardText);
        }
    }

    [Fact]
    public void SdlPrimarySelectionText_ShouldThrowException_WhenVideoNotInitialized()
    {
        void Lambda<TException>() where TException : Exception
        {
            Assert.Throws<TException>(() =>
            {
                _fixture.Sdl.PrimarySelectionText = _testText;
            });
        }

        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            Lambda<NativeException>();
        }
        else
        {
            Lambda<MinimumVersionRequiredNotMatchedException>();
        }
    }

    [Fact]
    public void SdlPrimarySelectionText_ShouldThrowException_WhenStringEmpty()
    {
        void Lambda<TException>() where TException : Exception
        {
            Assert.Throws<TException>(() =>
            {
                using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
                {
                    ResetPrimarySelectionText();
                    return _fixture.Sdl.PrimarySelectionText;
                }
            });
        }

        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            Lambda<NativeException>();
        }
        else
        {
            Lambda<MinimumVersionRequiredNotMatchedException>();
        }
    }

    [Fact]
    public void SdlPrimarySelectionText_ShouldBeSet_WhenGivenString()
    {
        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetPrimarySelectionText();
                _fixture.Sdl.PrimarySelectionText = _testText;
                Assert.Equal(_testText, _fixture.Sdl.PrimarySelectionText);
            }
        }
        else
        {
            Assert.Throws<MinimumVersionRequiredNotMatchedException>(() =>
                _fixture.Sdl.PrimarySelectionText = _testText);
        }
    }

    [Fact]
    public void SdlHasPrimarySelectionText_ShouldWork_WhenClipboardHasTextOrNot()
    {
        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetPrimarySelectionText();
                Assert.False(_fixture.Sdl.HasPrimarySelectionText);
                _fixture.Sdl.PrimarySelectionText = _testText;
                Assert.True(_fixture.Sdl.HasPrimarySelectionText);
            }
        }
        else
        {
            Assert.Throws<MinimumVersionRequiredNotMatchedException>(() =>
                _ = _fixture.Sdl.HasPrimarySelectionText);
        }
    }
}