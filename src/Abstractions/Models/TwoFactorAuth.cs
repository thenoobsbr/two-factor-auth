namespace TRTwoFactorAuth.Abstractions.Models;

public class TwoFactorAuth
{
    public TwoFactorAuth(TwoFactorAuthType type, TwoFactorAuthIssuer issuer, TwoFactorAuthLabel label, TwoFactorAuthSecret secret)
    {
        Type = type;
        Issuer = issuer;
        Label = label;
        Secret = secret;
    }

    public Uri GenerateUri()
    {
        const string URI = "otpauth://totp/{0}?secret={1}&issuer={2}";
        var qrCodeUri = string.Format(
            URI,
            Uri.EscapeDataString($"{Issuer.Value}:{Label.Value}"),
            Secret.Value,
            Uri.EscapeDataString(Issuer.Value));
        return new Uri(qrCodeUri);
    }

    public TwoFactorAuthType Type { get; }
    public TwoFactorAuthIssuer Issuer { get; }
    public TwoFactorAuthLabel Label { get; }
    public TwoFactorAuthSecret Secret { get; }
}