using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;
using MediatR;

namespace CleanArchitecture.Application.Queries.Periodos.GetAll;

public sealed record GetAllPeriodosQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<PeriodoViewModel>>;