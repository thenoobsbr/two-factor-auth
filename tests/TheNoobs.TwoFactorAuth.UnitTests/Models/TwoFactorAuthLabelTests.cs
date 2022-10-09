using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions.Models;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests.Models;

public class TwoFactorAuthLabelTests
{
    [Theory]
    [InlineData("label")]
    [InlineData("space label")]
    public void GivenTwoFactoryAuthLabel_WhenCreate_ThenShouldReturn(string labelText)
    {
        var label = new TwoFactorAuthLabel(labelText);

        label.Should().NotBeNull();
        label.ToString().Should().Be(labelText);
    }
    
    [Theory]
    [InlineData(" ")]
    [InlineData("a really very long label")]
    public void GivenTwoFactoryAuthLabel_WhenCreate_AndArgumentInvalid_ThenShouldThrow(string labelText)
    {
        var action = () => new TwoFactorAuthLabel(labelText);

        action.Should().Throw<ArgumentException>();
    }
}
