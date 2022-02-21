[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=tiagor87_Tr2FA&metric=coverage)](https://sonarcloud.io/dashboard?id=tiagor87_Tr2FA)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tiagor87_Tr2FA&metric=alert_status)](https://sonarcloud.io/dashboard?id=tiagor87_Tr2FA)
[![NuGet](https://buildstats.info/nuget/TRTwoFactorAuth.Core)](http://www.nuget.org/packages/TRTwoFactorAuth.Core)

# TR2FA

Simplifica a utilização da autenticação em dois fatores.

## Como usar

### Requisitos

É necessário implementar uma interface **IQRCodeGenerator**.
> Recomendo a utilização da QRCoder

### Registro

Instâncie a classe **TwoFactorAuthService**:

```c#
IQRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
ITwoFactorAuthService service = new TwoFactorAuthService(qrCodeGenerator);
```

Ou a registre em seu IOC:
```c#
services.AddSingleton<IQRCodeGenerator, YourOwnQRCodeGenerator>();
services.AddSingleton<ITwoFactorAuthService, TwoFactorAuthService>();
```

> Para simplificar testes de unidade, o serviço possui a interface **ITwoFactorAuthService**.

### Chamadas

Existem três comportamentos possíveis no serviço, são eles:

- **GenerateQrCode:** Esse método gera um QRCode para inserir o chave no autenticador;
- **ValidateCode:** Responsável por válidar um código fornecido;
- **GetNextCode:** Retorna o próximo código disponível.

#### GenerateQrCode

```c#
TwoFactorAuthIssuer twoFactorAuthIssuer = new TwoFactorAuthIssuer("<qualquer valor até 20 caracteres>");
TwoFactorAuthLabel twoFactorAuthLabel = new TwoFactorAuthLabel("<qualquer valor até 20 caracteres>");
TwoFactorAuthSecret twoFactorAuthSecret = new TwoFactorAuthSecret("<segredo em base 32 segundo RFC 3548>");

var qrCodeStream = await service.GenerateQrCodeAsync(twoFactorAuthIssuer, twoFactorAuthLabel, twoFactorAuthSecret, cancellationToken);
```
[Leia mais sobre a RFC3548](http://tools.ietf.org/html/rfc3548)

Se não quiser gerar seu próprio secret, você poderá usar o tipo **HashidTwoFactorAuthSecret**.
```c#
TwoFactorAuthSecret twoFactorAuthSecret = new HashidTwoFactorAuthSecret();
```

#### ValidateCode

```c#
TwoFactorAuthCode code = new TwoFactorAuthCode("<Código de 6 dígitos>");
TwoFactorAuthSecret secret = new TwoFactorAuthSecret("<Mesmo segredo utilizado na geração do QRCode>");
TwoFactorVerificationRange range = new TwoFactorVerificationRange(<Valor de 0-5 indicando a janela de códigos verificado>)

try {
  await service.ValidateQRCodeAsync(code, secret, range, cancellationToken);
}
catch (TwoFactorAuthCodeInvalidException ex) {
  // Tratamento para código inválido
}
```

> **TwoFactorVerificationRange** permite selecionar uma janela de tempo para verificação de códigos:
> Imagine que o parâmetro 1 é fornecido:
> Ao validar o código, aceitaremos como válido, caso seja anterior ao atual (-1), ou posterior ao atual (+1),
> tendo em vista que os códigos são gerados com base no tempo, é pode ocorrer uma pequena dessincronização entre cliente e servidor.

#### GetNextCode

```c#
TwoFactorAuthSecret secret = new TwoFactorAuthSecret("<Mesmo segredo utilizado na geração do QRCode>");

TwoFactorAuthCode code = await service.GetNextCodeAsync(secret, cancellationToken);
```