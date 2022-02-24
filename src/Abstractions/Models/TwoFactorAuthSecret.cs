using System;
using System.Text.RegularExpressions;

namespace TRTwoFactorAuth.Abstractions.Models;

public class TwoFactorAuthSecret
{
    protected const string ALPHABET = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
    protected const int LENGTH = 32;
    
    public TwoFactorAuthSecret(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length != LENGTH)
        {
            throw new ArgumentException("Secret must be 32 characters long", nameof(value));
        }

        if (!Regex.IsMatch(value, $"^[{ALPHABET}]+$", RegexOptions.Compiled))
        {
            throw new ArgumentException($"Secret must only contain characters from the alphabet \"{ALPHABET}\": \"{value}\"", nameof(value));
        }
        
        Value = value;
    }
    
    public string Value { get; }

    public static implicit operator string(TwoFactorAuthSecret secret) => secret.Value;

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return !ReferenceEquals(obj, null) &&
               (ReferenceEquals(this, obj) ||
                obj is TwoFactorAuthSecret other &&
                GetHashCode() == other.GetHashCode());
    }

    public override string ToString()
    {
        return Value.Substring(0, 6) + string.Empty.PadLeft(Value.Length - 10, '*') + Value.Substring(Value.Length - 4);
    }
}