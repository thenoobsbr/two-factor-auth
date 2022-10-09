using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions.Models;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests.Models;

public class TwoFactorIssuerLabelTests
{
    [Theory]
    [InlineData("issuer")]
    [InlineData("issuer label")]
    public void GivenTwoFactoryAuthIssuer_WhenCreate_ThenShouldReturn(string issuerText)
    {
        var issuer = new TwoFactorAuthIssuer(issuerText);

        issuer.Should().NotBeNull();
        issuer.ToString().Should().Be(issuerText);
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("a really very long issuer")]
    public void GivenTwoFactoryAuthIssuer_WhenCreate_AndArgumentInvalid_ThenShouldThrow(string issuerText)
    {
        var action = () => new TwoFactorAuthIssuer(issuerText);

        action.Should().Throw<ArgumentException>();
    }
}
