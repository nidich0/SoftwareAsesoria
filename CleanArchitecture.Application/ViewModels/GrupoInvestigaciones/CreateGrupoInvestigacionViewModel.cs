using System;

namespace CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;

public sealed record CreateGrupoInvestigacionViewModel(string Nombre, Guid TenantId);
