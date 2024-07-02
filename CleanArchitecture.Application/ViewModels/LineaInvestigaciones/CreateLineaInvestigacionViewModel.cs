using System;

namespace CleanArchitecture.Application.ViewModels.LineaInvestigaciones;

public sealed record CreateLineaInvestigacionViewModel(string Nombre, Guid FacultadId, Guid GrupoInvestigacionId);