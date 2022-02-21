using System.Text.RegularExpressions;

namespace TRTwoFactorAuth.Abstractions.Models;

public record TwoFactorAuthCode
{
    public string Value { get; }

    public TwoFactorAuthCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        }
        
        if (value.Length != 6)
        {
            throw new ArgumentException("Value must be 6 characters long.", nameof(value));
        }

        if (!Regex.IsMatch(value, "^\\d+$"))
        {
            throw new ArgumentException("Value must be numeric.");
        }
        
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}