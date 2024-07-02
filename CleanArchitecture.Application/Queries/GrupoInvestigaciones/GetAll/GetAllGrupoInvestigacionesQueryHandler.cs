using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.GrupoInvestigaciones;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Queries.GrupoInvestigaciones.GetAll;

public sealed class GetAllGrupoInvestigacionesQueryHandler :
    IRequestHandler<GetAllGrupoInvestigacionesQuery, PagedResult<GrupoInvestigacionViewModel>>
{
    private readonly ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion> _sortingExpressionProvider;
    private readonly IGrupoInvestigacionRepository _grupoinvestigacionRepository;

    public GetAllGrupoInvestigacionesQueryHandler(
        IGrupoInvestigacionRepository grupoinvestigacionRepository,
        ISortingExpressionProvider<GrupoInvestigacionViewModel, GrupoInvestigacion> sortingExpressionProvider)
    {
        _grupoinvestigacionRepository = grupoinvestigacionRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<GrupoInvestigacionViewModel>> Handle(
        GetAllGrupoInvestigacionesQuery request,
        CancellationToken cancellationToken)
    {
        var grupoinvestigacionesQuery = _grupoinvestigacionRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            //.Include(x => x.Users.Where(y => request.IncludeDeleted || !y.Deleted))
            .Where(x => request.IncludeDeleted || !x.Deleted);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            grupoinvestigacionesQuery = grupoinvestigacionesQuery.Where(grupoinvestigacion =>
                grupoinvestigacion.Nombre.Contains(request.SearchTerm));
        }

        var totalCount = await grupoinvestigacionesQuery.CountAsync(cancellationToken);

        grupoinvestigacionesQuery = grupoinvestigacionesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var grupoinvestigaciones = await grupoinvestigacionesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(grupoinvestigacion => GrupoInvestigacionViewModel.FromGrupoInvestigacion(grupoinvestigacion))
            .ToListAsync(cancellationToken);

        return new PagedResult<GrupoInvestigacionViewModel>(
            totalCount, grupoinvestigaciones, request.Query.Page, request.Query.PageSize);
    }
}