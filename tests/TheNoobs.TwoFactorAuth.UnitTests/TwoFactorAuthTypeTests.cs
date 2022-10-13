using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests;

public class TwoFactorAuthTypeTests
{
    [Fact]
    public void GivenTwoFactorAuthType_WhenCreateTOTP_ThenReturn()
    {
        var twoFactorAuthType = TwoFactorAuthType.TimeBasedOneTimePassword;

        twoFactorAuthType.Should().NotBeNull();
        twoFactorAuthType.Value.Should().Be("totp");
        twoFactorAuthType.ToString().Should().Be("totp");
    }

    [Fact]
    public void GivenTwoFactorAuthType_WhenCreateHOTP_ThenShouldThrowNotSupported()
    {
        var action = () => TwoFactorAuthType.HashBasedOneTimePassword;

        action.Should().Throw<NotSupportedException>();
    }
}
