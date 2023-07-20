using System;
using System.Diagnostics.CodeAnalysis;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.UnitTests;

public class LogTests : IDisposable
{
    private readonly string _logText;
    private readonly Sdl _sdl;

    public LogTests()
    {
        _sdl = new Sdl();
        _logText = "Test";
    }

    public void Dispose()
    {
        _sdl.Dispose();
    }

    private static void Callback(object userData, int category, Sdl.LogPriority priority, string message)
    {
    }

    [Fact]
    public void SettingLogOutputFunctionCallbackWithEmptyObjectWorks()
    {
        _sdl.LogSetOutputFunction(Callback, new NullVoidPointer());
        _sdl.LogGetOutputFunction(out _, out object userData);
        Assert.Null(userData);
    }

    [Fact]
    public void SettingLogOutputFunctionCallbackWithValidObjectWorks()
    {
        _sdl.LogSetOutputFunction(Callback, _logText);
        _sdl.LogGetOutputFunction(out _, out object userData);
        Assert.True(userData is string);
        Assert.Equal(_logText, userData as string);
    }

    [Fact]
    public void SettingAndResettingAllLogPrioritiesWorks()
    {
        _sdl.LogSetAllPriority(Sdl.LogPriority.Error);
        Array values = Enum.GetValues<MyCategories>();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Sdl.LogPriority.Error, _sdl.LogGetPriority(category));
        }

        _sdl.LogResetPriorities();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Sdl.LogPriority.Critical, _sdl.LogGetPriority(category));
        }
    }

    [Fact]
    private void SettingOneLogPriorityWorks()
    {
        _sdl.LogSetPriority(MyCategories.First, Sdl.LogPriority.Error);
        Array values = Enum.GetValues<MyCategories>();
        foreach (MyCategories category in values)
        {
            switch (category)
            {
                case MyCategories.First:
                    Assert.Equal(Sdl.LogPriority.Error, _sdl.LogGetPriority(category));
                    break;
                case MyCategories.Second:
                case MyCategories.Third:
                default:
                    Assert.Equal(Sdl.LogPriority.Critical, _sdl.LogGetPriority(category));
                    break;
            }
        }

        _sdl.LogResetPriorities();
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.Log(_logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogVerboseWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Verbose, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogVerbose(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogDebugWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Debug, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogDebug(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogInformationWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogInformation(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogWarningWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Warning, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogWarning(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogErrorWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Error, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogError(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogCriticalWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Critical, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogCritical(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void FunctionLogMessageWorks()
    {
        _sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _sdl.LogMessage(Sdl.LogCategory.Application, Sdl.LogPriority.Information, _logText);
    }

    private enum MyCategories
    {
        First = Sdl.LogCategory.Custom,
        Second,
        Third
    }
}