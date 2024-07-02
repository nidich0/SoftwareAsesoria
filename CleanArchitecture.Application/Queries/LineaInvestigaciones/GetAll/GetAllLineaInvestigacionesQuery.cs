using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineaInvestigaciones;
using MediatR;

namespace CleanArchitecture.Application.Queries.LineaInvestigaciones.GetAll;

public sealed record GetAllLineaInvestigacionesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<LineaInvestigacionViewModel>>;