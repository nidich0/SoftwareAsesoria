using System;

namespace CleanArchitecture.Application.ViewModels.Calendarios;

public sealed record CreateCalendarioViewModel(
    string AccessToken, 
    DateTime AccessTokenExpiration, 
    string RefreshToken, 
    DateTime RefreshTokenExpiration);