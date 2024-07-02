﻿using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;

public sealed class GrupoInvestigacionViewModel
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static GrupoInvestigacionViewModel FromGrupoInvestigacion(GrupoInvestigacion grupoinvestigacion)
    {
        return new GrupoInvestigacionViewModel
        {
            Id = grupoinvestigacion.Id,
            Nombre = grupoinvestigacion.Nombre
        };
    }
}