using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class Calendario : Entity
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set;}
    public string UserUri { get; set; }
    public string? EventType {  get; set; }
    public string? EventsPageToken { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
        public Calendario(
            Guid id, 
            Guid userId, 
            string accessToken, 
            DateTime accessTokenExpiration, 
            string refreshToken, 
            DateTime refreshTokenExpiration, 
            string userUri, 
            string? eventType = null, 
            string? eventsPageToken = null) : base(id)
    {
        UserId = userId;
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
        UserUri = userUri;
        EventType = eventType;
        EventsPageToken = eventsPageToken;
    }
}