using System;
using FluentAssertions;
using TRTwoFactorAuth.Abstractions.Models;
using TRTwoFactorAuth.Core.Models;
using Xunit;

namespace TRTwoFactorAuth.Core.Tests.Models;

public class TwoFactorAuthTests
{
    [Fact]
    public void GivenTwoFactorAuth_WhenCreate_ThenShouldReturn()
    {
        var issuer = new TwoFactorAuthIssuer("issuer");
        var label = new TwoFactorAuthLabel("label");
        var secret = new HashidTwoFactorAuthSecret();
        var twoFactorAuth = new TwoFactorAuth(TwoFactorAuthType.TimeBasedOneTimePassword, issuer, label, secret);

        twoFactorAuth.Should().NotBeNull();
        twoFactorAuth.Type.Should().Be(TwoFactorAuthType.TimeBasedOneTimePassword);
        twoFactorAuth.Issuer.Should().Be(issuer);
        twoFactorAuth.Label.Should().Be(label);
        twoFactorAuth.Secret.Should().Be(secret);
    }
    
    [Theory]
    [InlineData("issuer", "label")]
    [InlineData("space issuer", "space label")]
    public void GivenTwoFactorAuth_WhenGenerateUri_ThenShouldReturnCorrectUri(string issuerText, string labelText)
    {
        var issuer = new TwoFactorAuthIssuer(issuerText);
        var label = new TwoFactorAuthLabel(labelText);
        var secret = new HashidTwoFactorAuthSecret();
        
        var twoFactorAuth = new TwoFactorAuth(TwoFactorAuthType.TimeBasedOneTimePassword, issuer, label, secret);
        var uri = twoFactorAuth.GenerateUri();

        uri.Should().NotBeNull();
        uri.Scheme.Should().Be("otpauth");
        uri.Host.Should().Be("totp");
        uri.AbsolutePath.Should().Be($"/{Uri.EscapeDataString($"{issuerText}:{labelText}")}");
        uri.Query.Should().Be($"?secret={secret.Value}&issuer={Uri.EscapeDataString(issuer.Value)}");
    }
}