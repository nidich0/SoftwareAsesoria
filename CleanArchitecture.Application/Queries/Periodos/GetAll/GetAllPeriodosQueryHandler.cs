using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.Periodos.GetAll;

public sealed class GetAllPeriodosQueryHandler :
    IRequestHandler<GetAllPeriodosQuery, PagedResult<PeriodoViewModel>>
{
    private readonly ISortingExpressionProvider<PeriodoViewModel, Periodo> _sortingExpressionProvider;
    private readonly IPeriodoRepository _periodoRepository;

    public GetAllPeriodosQueryHandler(
        IPeriodoRepository periodoRepository,
        ISortingExpressionProvider<PeriodoViewModel, Periodo> sortingExpressionProvider)
    {
        _periodoRepository = periodoRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<PeriodoViewModel>> Handle(
        GetAllPeriodosQuery request,
        CancellationToken cancellationToken)
    {
        var periodosQuery = _periodoRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            periodosQuery = periodosQuery.Where(periodo =>
                periodo.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await periodosQuery.CountAsync(cancellationToken);

        periodosQuery = periodosQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var periodos = await periodosQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(periodo => PeriodoViewModel.FromPeriodo(periodo))
            .ToListAsync(cancellationToken);

        return new PagedResult<PeriodoViewModel>(
            totalCount, periodos, request.Query.Page, request.Query.PageSize);
    }
}