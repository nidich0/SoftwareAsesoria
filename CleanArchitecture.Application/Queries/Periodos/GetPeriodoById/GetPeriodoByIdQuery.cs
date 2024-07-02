using System;
using CleanArchitecture.Application.ViewModels.Periodos;
using MediatR;

namespace CleanArchitecture.Application.Queries.Periodos.GetPeriodoById;

public sealed record GetPeriodoByIdQuery(Guid PeriodoId) : IRequest<PeriodoViewModel?>;