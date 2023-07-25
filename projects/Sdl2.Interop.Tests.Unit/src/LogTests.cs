using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Sdl2.Interop.Utilities;

using Xunit;

namespace Sdl2.Interop.Tests.Unit;

[Collection(Collections.Library)]
public class LogTests
{
    private readonly SdlFixture _fixture;
    private readonly string _logText;

    public LogTests(SdlFixture fixture)
    {
        _fixture = fixture;
        _logText = "Test";
    }

    private static void Callback(object userData, int category, Sdl.LogPriority priority, string message)
    {
    }

    [Fact]
    public void SdlLogSetOutputFunction_ShouldSetLogOutputFunction_WhenUserDataIsNull()
    {
        _fixture.Sdl.LogSetOutputFunction(Callback, new NullVoidPointer());
        _fixture.Sdl.LogGetOutputFunction(out _, out object userData);
        Assert.Null(userData);
    }

    [Fact]
    public void SdlLogSetOutputFunction_ShouldSetLogOutputFunction_WhenUserDataIsNotNull()
    {
        _fixture.Sdl.LogSetOutputFunction(Callback, _logText);
        _fixture.Sdl.LogGetOutputFunction(out _, out object userData);
        Assert.NotNull(userData);
        Assert.True(userData is string);
        Assert.Equal(_logText, userData as string);
    }

    [Fact]
    public void SdlLogResetPriorities_ShouldResetAllPriorities_WhenSdlLogSetAllPriorityWasCalled()
    {
        _fixture.Sdl.LogSetAllPriority(Sdl.LogPriority.Error);
        Array values = Enum.GetValues<MyCategories>();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Sdl.LogPriority.Error, _fixture.Sdl.LogGetPriority(category));
        }

        _fixture.Sdl.LogResetPriorities();
        foreach (MyCategories category in values)
        {
            Assert.Equal(Sdl.LogPriority.Critical, _fixture.Sdl.LogGetPriority(category));
        }
    }

    [Theory]
    [MemberData(nameof(LogPriorityData))]
    private void SdlLogSetPriority_ShouldSetALogPriority_WhenPassedACategoryAndPriority(MyCategories category,
        Sdl.LogPriority priority)
    {
        _fixture.Sdl.LogSetPriority(category, priority);
        Assert.Equal(priority, _fixture.Sdl.LogGetPriority(category));
        _fixture.Sdl.LogResetPriorities();
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLog_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.Log(_logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogVerbose_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Verbose, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogVerbose(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogDebug_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Debug, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogDebug(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogInformation_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogInformation(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogWarning_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Warning, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogWarning(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogError_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Error, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogError(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogCritical_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Critical, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogCritical(Sdl.LogCategory.Application, _logText);
    }

    [Fact]
    [SuppressMessage("ReSharper", "ParameterOnlyUsedForPreconditionCheck.Local",
        Justification = "Required for assertions")]
    public void SdlLogMessage_ShouldWork_WhenPassedMessage()
    {
        _fixture.Sdl.LogSetOutputFunction((data, category, priority, message) =>
        {
            Assert.Null(data);
            Assert.Equal(Sdl.LogCategory.Application, (Sdl.LogCategory)category);
            Assert.Equal(Sdl.LogPriority.Information, priority);
            Assert.Equal(_logText, message);
        }, new NullVoidPointer());

        _fixture.Sdl.LogMessage(Sdl.LogCategory.Application, Sdl.LogPriority.Information, _logText);
    }

    public static IEnumerable<object[]> LogPriorityData()
    {
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Critical };
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Debug };
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Error };
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Information };
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Verbose };
        yield return new object[] { MyCategories.First, Sdl.LogPriority.Warning };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Critical };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Debug };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Error };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Information };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Verbose };
        yield return new object[] { MyCategories.Second, Sdl.LogPriority.Warning };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Critical };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Debug };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Error };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Information };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Verbose };
        yield return new object[] { MyCategories.Third, Sdl.LogPriority.Warning };
    }

    private enum MyCategories
    {
        First = Sdl.LogCategory.Custom,
        Second,
        Third
    }
}