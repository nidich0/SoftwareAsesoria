using CleanArchitecture.Domain.Enums;
using System;

namespace CleanArchitecture.Application.ViewModels.Citas;

public sealed record CreateCitaViewModel(
    string EventoId, 
    Guid AsesorUserId, 
    string AsesoradoEmail,
    CitaEstado Estado = CitaEstado.Programado);