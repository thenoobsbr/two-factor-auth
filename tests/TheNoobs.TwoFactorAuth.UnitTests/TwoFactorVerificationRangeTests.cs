using System;
using FluentAssertions;
using TheNoobs.TwoFactorAuth.Abstractions;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests;

public class TwoFactorVerificationRangeTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(-1, 1)]
    [InlineData(-2, 2)]
    [InlineData(-3, 3)]
    [InlineData(-4, 4)]
    [InlineData(-5, 5)]
    public void GivenTwoFactorVerificationRange_WhenCreate_ThenShoudReturn(int start, int end)
    {
        var range = new TwoFactorVerificationRange(start, end);

        range.Should().NotBeNull();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GivenTwoFactorVerificationRange_WhenCreateWithRange_ThenShoudReturn(int rangeValue)
    {
        var range = new TwoFactorVerificationRange(rangeValue);

        range.Should().NotBeNull();
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(-1, -1)]
    [InlineData(-6, 5)]
    [InlineData(-5, 6)]
    public void GivenTwoFactorVerificationRange_WhenCreate_AndArgumentInvalid_ThenShoudThrow(int start, int end)
    {
        var action = () => new TwoFactorVerificationRange(start, end);

        action.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void GivenTwoFactorVerificationRange_WhenCreateWithRange_AndArgumentInvalid_ThenShoudThrow(int range)
    {
        var action = () => new TwoFactorVerificationRange(range);

        action.Should().Throw<ArgumentException>();
    }
}
