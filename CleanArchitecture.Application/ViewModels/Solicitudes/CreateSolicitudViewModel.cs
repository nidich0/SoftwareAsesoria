using System;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Application.ViewModels.Solicitudes;
public sealed record CreateSolicitudViewModel(
    Guid AsesoradoUserId, 
    Guid AsesorUserId,
    string NumeroTesis, 
    string Afinidad,
    string Mensaje);