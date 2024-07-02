using System;

namespace CleanArchitecture.Application.ViewModels.LineaInvestigaciones;

public sealed record UpdateLineaInvestigacionViewModel(
    Guid Id,
    string Nombre);