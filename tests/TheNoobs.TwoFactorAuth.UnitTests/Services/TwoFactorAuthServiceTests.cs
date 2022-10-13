using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using TheNoobs.TwoFactorAuth.Abstractions;
using TheNoobs.TwoFactorAuth.Abstractions.Services;
using TheNoobs.TwoFactorAuth.Abstractions.Utilities;
using TheNoobs.TwoFactorAuth.Exceptions;
using TheNoobs.TwoFactorAuth.Services;
using Xunit;

namespace TheNoobs.TwoFactorAuth.UnitTests.Services;

public class TwoFactorAuthServiceTests
{
    private readonly IQrCodeGenerator _qrCodeGenerator;
    private readonly ITwoFactorAuthService _sut;

    public TwoFactorAuthServiceTests()
    {
        _qrCodeGenerator = Substitute.For<IQrCodeGenerator>();
        _sut = new TwoFactorAuthService(_qrCodeGenerator);
    }
    
    [Fact]
    public async Task GivenTwoFactorAuthService_WhenGenerateQrCode_ThenShouldReturnQrCodeStream()
    {
        var issuer = new TwoFactorAuthIssuer("issuer");
        var label = new TwoFactorAuthLabel("label");
        var secret = new TwoFactorAuthHashidSecret();

        _qrCodeGenerator.GenerateAsync(Arg.Any<Uri>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Stream>(new MemoryStream()));

        var qrCodeStream = await _sut.GenerateQrCodeAsync(issuer, label, secret);

        qrCodeStream.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenTwoFactorAuthService_WhenGetNextCode_ThenShouldReturnValidCode()
    {
        var secret = new TwoFactorAuthHashidSecret();
        
        var code = await _sut.GetNextCodeAsync(secret);

        var action = () => _sut.ValidateCodeAsync(code, secret);
        
        await action.Should().NotThrowAsync();
    }
    
    [Fact]
    public async Task GivenTwoFactorAuthService_WhenValidateInvalidCode_ThenShouldThrow()
    {
        var secret = new TwoFactorAuthHashidSecret();

        var action = () => _sut.ValidateCodeAsync(new TwoFactorAuthCode("000000"), secret);
        
        await action.Should().ThrowAsync<TwoFactorAuthCodeInvalidException>();
    }
    
    [Fact]
    public async Task GivenTwoFactorAuthService_WhenValidateInvalidCode_ThenShouldNotThrow()
    {
        var secret = new TwoFactorAuthHashidSecret();

        var code = await _sut.GetNextCodeAsync(secret);

        var action = () => _sut.ValidateCodeAsync(code, secret);
        
        await action.Should().NotThrowAsync();
    }
}
