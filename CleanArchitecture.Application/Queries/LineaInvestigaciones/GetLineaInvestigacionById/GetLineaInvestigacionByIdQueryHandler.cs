using System;
using CleanArchitecture.Application.ViewModels.LineaInvestigaciones;
using MediatR;

namespace CleanArchitecture.Application.Queries.LineaInvestigaciones.GetLineaInvestigacionById;

public sealed record GetLineaInvestigacionByIdQuery(Guid LineaInvestigacionId) : IRequest<LineaInvestigacionViewModel?>;