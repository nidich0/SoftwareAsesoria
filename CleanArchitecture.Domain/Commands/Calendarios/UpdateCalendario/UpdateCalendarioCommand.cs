using System;

namespace CleanArchitecture.Domain.Commands.Calendarios.UpdateCalendario;

public sealed class UpdateCalendarioCommand : CommandBase
{
    private static readonly UpdateCalendarioCommandValidation s_validation = new();

    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    public string UserUri { get; set; }
    public string? EventType { get; set; }
    public string? EventsPageToken { get; set; }

    public UpdateCalendarioCommand(
        Guid calendarioId, 
        string accessToken, 
        DateTime accessTokenExpiration, 
        string refreshToken, 
        DateTime refreshTokenExpiration, 
        string userUri, 
        string? eventType, 
        string? eventsPageToken) : base(calendarioId)
    {
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
        UserUri = userUri;
        EventType = eventType;
        EventsPageToken = eventsPageToken;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}