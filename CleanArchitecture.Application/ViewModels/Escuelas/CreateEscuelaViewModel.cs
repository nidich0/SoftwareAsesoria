using System;

namespace CleanArchitecture.Application.ViewModels.Escuelas;

public sealed record CreateEscuelaViewModel(string Name, Guid FacultadId);