using System;

using FluentAssertions;

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
        Func<string> requestAction = () => _fixture.Sdl.ClipboardText = _testText;
        requestAction.Should().Throw<NativeException>()
            .WithMessage("Video subsystem must be initialized to set clipboard text");
    }

    [Fact]
    public void SdlClipboardText_ShouldReturnEmptyString_WhenStringEmpty()
    {
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            string result = _fixture.Sdl.ClipboardText;
            result.Should().BeEmpty();
        }
    }

    [Fact]
    public void SdlClipboardText_ShouldBeSet_WhenGivenString()
    {
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            _fixture.Sdl.ClipboardText = _testText;
            _fixture.Sdl.ClipboardText.Should().Be(_testText);
        }
    }

    [Fact]
    public void SdlHasClipboardText_ShouldWork_WhenClipboardHasTextOrNot()
    {
        using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            _fixture.Sdl.HasClipboardText.Should().BeFalse();
            _fixture.Sdl.ClipboardText = _testText;
            _fixture.Sdl.HasClipboardText.Should().BeTrue();
        }
    }

    [Fact]
    public void SdlPrimarySelectionText_ShouldThrowException_WhenVideoNotInitialized()
    {
        void Lambda<TException>(string message) where TException : Exception
        {
            Func<string> requestAction = () => _fixture.Sdl.PrimarySelectionText = _testText;
            requestAction.Should().Throw<TException>().WithMessage(message);
        }

        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            Lambda<NativeException>("Video subsystem must be initialized to set primary selection text");
        }
        else
        {
            Lambda<MinimumVersionRequiredNotMatchedException>(
                $"The SDL2 export 'SDL_GetPrimarySelectionText' expected at least SDL v{new Version(2, 26, 0)} but v{_fixture.Sdl.Version} was found.");
        }
    }

    [Fact]
    public void SdlPrimarySelectionText_ShouldReturnEmptyString_WhenStringEmpty()
    {
        if (_fixture.Sdl.Version >= new Version(2, 26, 0))
        {
            using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetPrimarySelectionText();
                _fixture.Sdl.PrimarySelectionText.Should().BeEmpty();
            }
        }
        else
        {
            Action requestAction = () =>
            {
                using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
                {
                    ResetPrimarySelectionText();
                    _fixture.Sdl.PrimarySelectionText.Should().BeEmpty();
                }
            };

            requestAction.Should().Throw<MinimumVersionRequiredNotMatchedException>().WithMessage(
                $"The SDL2 export 'SDL_GetPrimarySelectionText' expected at least SDL v{new Version(2, 26, 0)} but v{_fixture.Sdl.Version} was found.");
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
                _fixture.Sdl.PrimarySelectionText.Should().Be(_testText);
            }
        }
        else
        {
            Action requestAction = () =>
            {
                using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
                {
                    ResetPrimarySelectionText();
                    _fixture.Sdl.PrimarySelectionText = _testText;
                }
            };

            requestAction.Should().Throw<MinimumVersionRequiredNotMatchedException>().WithMessage(
                $"The SDL2 export 'SDL_SetPrimarySelectionText' expected at least SDL v{new Version(2, 26, 0)} but v{_fixture.Sdl.Version} was found.");
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
                _fixture.Sdl.HasPrimarySelectionText.Should().BeFalse();
                _fixture.Sdl.PrimarySelectionText = _testText;
                _fixture.Sdl.HasPrimarySelectionText.Should().BeTrue();
            }
        }
        else
        {
            Action requestAction = () =>
            {
                using (_fixture.Sdl.Initialize(Sdl.InitializeFlags.Video))
                {
                    ResetPrimarySelectionText();
                    _fixture.Sdl.PrimarySelectionText = _testText;
                }
            };

            requestAction.Should().Throw<MinimumVersionRequiredNotMatchedException>().WithMessage(
                $"The SDL2 export 'SDL_HasPrimarySelectionText' expected at least SDL v{new Version(2, 26, 0)} but v{_fixture.Sdl.Version} was found.");
        }
    }
}