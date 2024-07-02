using System;

namespace CleanArchitecture.Application.ViewModels.Periodos;

public sealed record CreatePeriodoViewModel(
    DateOnly FechaInicio,
    DateOnly FechaFinal,
    string Nombre);