﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;

public sealed record UpdateGrupoInvestigacionViewModel(
    Guid Id,
    string Nombre)
{
}
