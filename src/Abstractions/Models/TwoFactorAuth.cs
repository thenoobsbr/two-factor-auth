using System;

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
        const string SCHEME = "otpauth";
        const string HOST = "totp";

        return new UriBuilder(
            SCHEME,
            HOST,
            0,
            Uri.EscapeDataString($"{Issuer.Value}:{Label.Value}"),
            $"?secret={Secret.Value}&issuer={Uri.EscapeDataString(Issuer.Value)}")
            .Uri;
    }

    public TwoFactorAuthType Type { get; }
    public TwoFactorAuthIssuer Issuer { get; }
    public TwoFactorAuthLabel Label { get; }
    public TwoFactorAuthSecret Secret { get; }
}