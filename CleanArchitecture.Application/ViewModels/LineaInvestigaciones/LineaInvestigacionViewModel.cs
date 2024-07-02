using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Application.ViewModels.LineaInvestigaciones;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.ViewModels.LineaInvestigaciones;

public sealed class LineaInvestigacionViewModel
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static LineaInvestigacionViewModel FromLineaInvestigacion(LineaInvestigacion lineainvestigacion)
    {
        return new LineaInvestigacionViewModel
        {
            Id = lineainvestigacion.Id,
            Nombre = lineainvestigacion.Nombre,
            //Users = lineainvestigacion.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}