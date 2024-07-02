using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.Citas;

public sealed class CitaViewModel
{
    public Guid Id { get; set; }
    public string EventoId { get; set; } = String.Empty;
    public Guid AsesorUserId { get; set; } = Guid.Empty;
    public Guid AsesoradoUserId { get; set; } = Guid.Empty;


    public static CitaViewModel FromCita(Cita cita)
    {
        return new CitaViewModel
        {
            Id = cita.Id,
            EventoId = cita.EventoId,
            AsesorUserId = cita.AsesorUserId,
            AsesoradoUserId = cita.AsesoradoUserId
        };
    }
}