using System;

using Sdl2.Interop.Exceptions;

using Xunit;

namespace Sdl2.Interop.UnitTests;

[Collection(Collections.Library)]
public class ClipboardTests : IDisposable
{
    private readonly Sdl _sdl;
    private readonly string _testText;

    public ClipboardTests()
    {
        _sdl = new Sdl();
        _testText = "Test";
    }

    public void Dispose()
    {
        _sdl.Dispose();
    }

    private void ResetClipboardText()
    {
        _sdl.ClipboardText = string.Empty;
    }

    private void ResetPrimarySelectionText()
    {
        _sdl.PrimarySelectionText = string.Empty;
    }

    [Fact]
    public void SettingClipboardTextWithoutVideoSubsystemThrowsException()
    {
        Assert.Throws<NativeException>(() =>
        {
            _sdl.ClipboardText = _testText;
        });
    }

    [Fact]
    public void GettingEmptyClipboardTextThrowsException()
    {
        Assert.Throws<NativeException>(() =>
        {
            using (_sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetClipboardText();
                return _sdl.ClipboardText;
            }
        });
    }

    [Fact]
    public void SettingClipboardTextWorks()
    {
        using (_sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            _sdl.ClipboardText = _testText;
            Assert.Equal(_testText, _sdl.ClipboardText);
        }
    }

    [Fact]
    public void CheckingClipboardHasTextWorks()
    {
        using (_sdl.Initialize(Sdl.InitializeFlags.Video))
        {
            ResetClipboardText();
            Assert.False(_sdl.HasClipboardText);
            _sdl.ClipboardText = _testText;
            Assert.True(_sdl.HasClipboardText);
        }
    }

    [Fact]
    public void SettingPrimarySelectionTextWithoutVideoSubsystemThrowsException()
    {
        void Lambda<TException>() where TException : Exception
        {
            Assert.Throws<TException>(() =>
            {
                _sdl.PrimarySelectionText = _testText;
            });
        }

        ;

        if (_sdl.Version >= new Version(2, 26, 0))
        {
            Lambda<NativeException>();
        }
        else
        {
            Lambda<MinimumVersionRequiredNotMatchedException>();
        }
    }

    [Fact]
    public void GettingEmptyPrimarySelectionTextThrowsException()
    {
        void Lambda<TException>() where TException : Exception
        {
            Assert.Throws<TException>(() =>
            {
                using (_sdl.Initialize(Sdl.InitializeFlags.Video))
                {
                    ResetPrimarySelectionText();
                    return _sdl.PrimarySelectionText;
                }
            });
        }

        if (_sdl.Version >= new Version(2, 26, 0))
        {
            Lambda<NativeException>();
        }
        else
        {
            Lambda<MinimumVersionRequiredNotMatchedException>();
        }
    }

    [Fact]
    public void SettingPrimarySelectionTextWorks()
    {
        if (_sdl.Version >= new Version(2, 26, 0))
        {
            using (_sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetPrimarySelectionText();
                _sdl.PrimarySelectionText = _testText;
                Assert.Equal(_testText, _sdl.PrimarySelectionText);
            }
        }
        else
        {
            Assert.Throws<MinimumVersionRequiredNotMatchedException>(() =>
                _sdl.PrimarySelectionText = _testText);
        }
    }

    [Fact]
    public void CheckingPrimarySelectionHasTextWorks()
    {
        if (_sdl.Version >= new Version(2, 26, 0))
        {
            using (_sdl.Initialize(Sdl.InitializeFlags.Video))
            {
                ResetPrimarySelectionText();
                Assert.False(_sdl.HasPrimarySelectionText);
                _sdl.PrimarySelectionText = _testText;
                Assert.True(_sdl.HasPrimarySelectionText);
            }
        }
        else
        {
            Assert.Throws<MinimumVersionRequiredNotMatchedException>(() =>
                _ = _sdl.HasPrimarySelectionText);
        }
    }
}