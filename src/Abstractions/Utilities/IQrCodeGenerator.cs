using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TRTwoFactorAuth.Abstractions.Utilities;

public interface IQrCodeGenerator
{
    Task<Stream> GenerateAsync(Uri uri, CancellationToken cancellationToken = default);
}