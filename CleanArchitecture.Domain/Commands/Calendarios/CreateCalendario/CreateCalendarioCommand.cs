using System;

namespace CleanArchitecture.Domain.Commands.Calendarios.CreateCalendario;

public sealed class CreateCalendarioCommand : CommandBase
{
    private static readonly CreateCalendarioCommandValidation s_validation = new();

    public string AccessToken { get; }
    public DateTime AccessTokenExpiration { get; }
    public string RefreshToken { get; }
    public DateTime RefreshTokenExpiration { get; }

    public CreateCalendarioCommand(
        Guid calendarioId,
        string accessToken, 
        DateTime accessTokenExpiration, 
        string refreshToken, 
        DateTime refreshTokenExpiration) : base(calendarioId)
    {
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}