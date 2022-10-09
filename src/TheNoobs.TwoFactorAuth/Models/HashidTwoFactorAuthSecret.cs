using HashidsNet;
using TheNoobs.TwoFactorAuth.Abstractions.Models;

namespace TheNoobs.TwoFactorAuth.Models;

public sealed class HashidTwoFactorAuthSecret : TwoFactorAuthSecret
{
    private static readonly Random _random = new();

    public HashidTwoFactorAuthSecret() : base(GenerateCode())
    {
    }

    private static string GenerateCode()
    {
        const int MAX_VALUE = 100;
        var salt = Guid.NewGuid().ToString();
        var hashids = new Hashids(salt, LENGTH, ALPHABET);
        var numbers = Enumerable
            .Range(0, 4)
            .Select(_ => _random.Next(MAX_VALUE))
            .ToList();
        return hashids.Encode(numbers);
    }
}