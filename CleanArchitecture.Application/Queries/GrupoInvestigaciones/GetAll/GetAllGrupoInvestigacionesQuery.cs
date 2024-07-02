using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using MediatR;

namespace CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetAll;

public sealed record GetAllGrupoInvestigacionesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<GrupoInvestigacionViewModel>>;