namespace TRTwoFactorAuth.Core.Exceptions;

public class TwoFactorAuthCodeInvalidException : Exception
{
    public TwoFactorAuthCodeInvalidException(string message) : base(message)
    {
    }
}