using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace TheNoobs.TwoFactorAuth.Exceptions;

[Serializable]
public class TwoFactorAuthCodeInvalidException : Exception
{
    public TwoFactorAuthCodeInvalidException(string message) : base(message)
    {
    }
    
    [ExcludeFromCodeCoverage]
    protected TwoFactorAuthCodeInvalidException(SerializationInfo info, StreamingContext context)
    {
    }
}