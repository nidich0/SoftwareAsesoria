using System;

namespace CleanArchitecture.Shared.Events.Calendario;

public sealed class CalendarioUpdatedEvent : DomainEvent
{
    public string AccessToken { get; }
    public DateTime AccessTokenExpiration { get; }
    public string RefreshToken { get; }
    public DateTime RefreshTokenExpiration { get; }
    public string UserUri { get; }
    public string? EventType { get; }
    public string? EventsPageToken { get; }

    public CalendarioUpdatedEvent(Guid calendarioId, string accessToken, DateTime accessTokenExpiration, string refreshToken, DateTime refreshTokenExpiration, string userUri, string? eventType, string? eventsPageToken) : base(calendarioId)
    {
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
        UserUri = userUri;
        EventType = eventType;
        EventsPageToken = eventsPageToken;
    }
}