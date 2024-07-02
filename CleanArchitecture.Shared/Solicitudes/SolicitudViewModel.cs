using System;

namespace CleanArchitecture.Shared.Solicitud;

public sealed record SolicitudViewModel(
    Guid Id,
    string NumeroTesis,
    string Afinidad,
    bool IsDeleted);