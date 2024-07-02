using System;

namespace CleanArchitecture.Shared.Escuela;

public sealed record EscuelaViewModel(
    Guid Id,
    string Name);