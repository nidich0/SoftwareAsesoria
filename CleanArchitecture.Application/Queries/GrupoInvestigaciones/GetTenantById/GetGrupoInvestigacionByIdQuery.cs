using System;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using MediatR;

namespace CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetGrupoInvestigacionById;

public sealed record GetGrupoInvestigacionByIdQuery(Guid GrupoInvestigacionId) : IRequest<GrupoInvestigacionViewModel?>;