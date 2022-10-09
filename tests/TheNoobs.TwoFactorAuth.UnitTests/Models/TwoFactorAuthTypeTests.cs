using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions.Models;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests.Models;

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
