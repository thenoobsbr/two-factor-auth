using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests;

public class TwoFactorAuthSecretTests
{
    [Fact]
    public void GivenSecret_WhenCreate_ThenShouldReturnValidSecret()
    {
        var secret = new TwoFactorAuthHashidSecret();

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
        var action = () => new TwoFactorAuthSecretStub(secretText);

        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenSecret_WhenReadAsString_ThenShouldMaskedValue()
    {
        var secret = new TwoFactorAuthHashidSecret();

        secret.ToString().Should().MatchRegex("^\\w{6}\\*{22}\\w{4}$");
    }

    [Fact]
    public void GivenSecret_WhenCompareSameValue_ThenShouldBeTrue()
    {
        var secret = new TwoFactorAuthHashidSecret();
        var secret2 = new TwoFactorAuthSecretStub(secret.Value);

        secret.Should().Be(secret2);
    }
    
    [Fact]
    public void GivenSecret_WhenSetToString_ThenShouldImplicitConvert()
    {
        var secret = new TwoFactorAuthHashidSecret();

        string secretValue = secret;
        secretValue.Should().Be(secret.Value);
    }

    class TwoFactorAuthSecretStub : TwoFactorAuthSecret
    {
        public TwoFactorAuthSecretStub(string value) : base(value)
        {
        }
    }
}
