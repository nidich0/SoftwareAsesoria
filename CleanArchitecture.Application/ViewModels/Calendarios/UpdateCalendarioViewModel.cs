using System;

namespace CleanArchitecture.Application.ViewModels.Calendarios;

public sealed record UpdateCalendarioViewModel(
    Guid Id, 
    string AccessToken, 
    DateTime AccessTokenExpiration, 
    string RefreshToken, 
    DateTime RefreshTokenExpiration, 
    string UserUri, 
    string? EventType, 
    string? EventsPageToken);