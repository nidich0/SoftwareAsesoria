using System;

namespace CleanArchitecture.Application.ViewModels.Periodos;

public sealed record UpdatePeriodoViewModel(
    Guid Id,
    DateOnly FechaInicio,
    DateOnly FechaFinal,
    string Nombre);