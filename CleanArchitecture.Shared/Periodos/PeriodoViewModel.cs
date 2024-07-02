using System;

namespace CleanArchitecture.Shared.Periodos;

public sealed record PeriodoViewModel(
    Guid Id,
    DateOnly FechaInicio,
    DateOnly FechaFinal,
    string Nombre);