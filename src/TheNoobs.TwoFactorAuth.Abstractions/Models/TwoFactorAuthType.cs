using System;

namespace TheNoobs.TwoFactorAuth.Abstractions.Models;

public record TwoFactorAuthType
{
    private const string TOTP = "totp";
    
    private TwoFactorAuthType(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static TwoFactorAuthType TimeBasedOneTimePassword => new(TOTP);
    public static TwoFactorAuthType HashBasedOneTimePassword => throw new NotSupportedException("HOTP is not supported yet");
    
    public override string ToString()
    {
        return Value;
    }
}