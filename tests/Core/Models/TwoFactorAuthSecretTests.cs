using System;
using FluentAssertions;
using TRTwoFactorAuth.Abstractions.Models;
using TRTwoFactorAuth.Core.Models;
using Xunit;

namespace TRTwoFactorAuth.Core.Tests.Models;

public class TwoFactorAuthSecretTests
{
    [Fact]
    public void GivenSecret_WhenCreate_ThenShouldReturnValidSecret()
    {
        var secret = new HashidTwoFactorAuthSecret();

        secret.Should().NotBeNull();
        secret.Value.Should().HaveLength(32);
        secret.Value.Should().MatchRegex("^[a-zA-Z2-7]+$");
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("234567234567234567234567234567234")]
    [InlineData("23456723456723456723456723456728")]
    public void GivenSecret_WhenCreate_AndArgumentInvalid_ThenShouldThrow(string secretText)
    {
        var action = () => new TwoFactorAuthSecret(secretText);

        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void GivenSecret_WhenReadAsString_ThenShouldMaskedValue()
    {
        var secret = new HashidTwoFactorAuthSecret();

        secret.ToString().Should().MatchRegex("^\\w{6}\\*{22}\\w{4}$");
    }
    
}