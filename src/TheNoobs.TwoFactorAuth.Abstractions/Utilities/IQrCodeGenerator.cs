﻿namespace TheNoobs.TwoFactorAuth.Abstractions.Utilities;

public interface IQrCodeGenerator
{
    Task<Stream> GenerateAsync(Uri uri, CancellationToken cancellationToken = default);
}