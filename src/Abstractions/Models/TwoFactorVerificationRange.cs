using System;

namespace TRTwoFactorAuth.Abstractions.Models;

public record TwoFactorVerificationRange
{
    public TwoFactorVerificationRange(int rangeStart, int rangeEnd)
    {
        ValidateRange(rangeStart, rangeEnd);
        Start = rangeStart;
        End = rangeEnd;
    }
    
    public TwoFactorVerificationRange(int range) : this(range * -1, range)
    {
    }

    public static TwoFactorVerificationRange Default => new(2);

    private static void ValidateRange(int rangeStart, int rangeEnd)
    {
        if (rangeStart > 0)
        {
            throw new ArgumentException("Range start must be zero or less.", nameof(rangeStart));
        }

        if (rangeEnd < 0)
        {
            throw new ArgumentException("Range end must be zero or more.", nameof(rangeEnd));
        }

        if (rangeStart < -5)
        {
            throw new ArgumentException("Range start must be within -5 to 0.", nameof(rangeStart));
        }

        if (rangeEnd > 5)
        {
            throw new ArgumentException("Range end must be within 0 to 5.", nameof(rangeEnd));
        }
    }

    public int End { get; }

    public int Start { get; }
}