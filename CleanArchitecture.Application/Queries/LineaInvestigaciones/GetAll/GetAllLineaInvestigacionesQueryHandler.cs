using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.LineaInvestigaciones;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.LineaInvestigaciones.GetAll;

public sealed class GetAllLineaInvestigacionesQueryHandler :
    IRequestHandler<GetAllLineaInvestigacionesQuery, PagedResult<LineaInvestigacionViewModel>>
{
    private readonly ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion> _sortingExpressionProvider;
    private readonly ILineaInvestigacionRepository _lineainvestigacionRepository;

    public GetAllLineaInvestigacionesQueryHandler(
        ILineaInvestigacionRepository lineainvestigacionRepository,
        ISortingExpressionProvider<LineaInvestigacionViewModel, LineaInvestigacion> sortingExpressionProvider)
    {
        _lineainvestigacionRepository = lineainvestigacionRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<LineaInvestigacionViewModel>> Handle(
        GetAllLineaInvestigacionesQuery request,
        CancellationToken cancellationToken)
    {
        var lineainvestigacionesQuery = _lineainvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            lineainvestigacionesQuery = lineainvestigacionesQuery.Where(lineainvestigacion =>
                lineainvestigacion.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await lineainvestigacionesQuery.CountAsync(cancellationToken);

        lineainvestigacionesQuery = lineainvestigacionesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var lineainvestigaciones = await lineainvestigacionesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(lineainvestigacion => LineaInvestigacionViewModel.FromLineaInvestigacion(lineainvestigacion))
            .ToListAsync(cancellationToken);

        return new PagedResult<LineaInvestigacionViewModel>(
            totalCount, lineainvestigaciones, request.Query.Page, request.Query.PageSize);
    }
}