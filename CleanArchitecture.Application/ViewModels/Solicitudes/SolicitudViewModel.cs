using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.Users;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.ViewModels.Solicitudes;

public sealed class SolicitudViewModel
{
    public Guid Id { get; set; }
    public Guid AsesoradoUserId { get; set; } = Guid.Empty;

    public Guid AsesorUserId { get; set; } = Guid.Empty;
    public string NumeroTesis { get; set; } = string.Empty;
    public string Afinidad { get; set; } = string.Empty;
    public SolicitudStatus Estado { get; set; }
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static SolicitudViewModel FromSolicitud(Solicitud solicitud)
    {
        return new SolicitudViewModel
        {
            Id = solicitud.Id,
            AsesoradoUserId = solicitud.AsesoradoUserId,
            AsesorUserId = solicitud.AsesorUserId,
            NumeroTesis = solicitud.NumeroTesis,
            Afinidad = solicitud.Afinidad,
            Estado = solicitud.Estado,
            //Users = solicitud.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}