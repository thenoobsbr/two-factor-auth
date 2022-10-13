using HashidsNet;
using TheNoobs.TwoFactorAuth.Abstractions;

namespace TheNoobs.TwoFactorAuth;

public sealed class TwoFactorAuthHashidSecret : TwoFactorAuthSecret
{
    private static readonly Random _random = new();

    public TwoFactorAuthHashidSecret() : base(GenerateCode())
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