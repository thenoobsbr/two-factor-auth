namespace TheNoobs.TwoFactorAuth.Abstractions;

public record TwoFactorAuthLabel
{
    public TwoFactorAuthLabel(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Label cannot be null or whitespace.", nameof(value));
        }

        if (value.Length > 20)
        {
            throw new ArgumentException("Label cannot be longer than 20 characters.", nameof(value));
        }

        Value = value;
    }
    public string Value { get; }

    public override string ToString()
    {
        return  Value;
    }
}