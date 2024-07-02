using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;

namespace CleanArchitecture.Application.Interfaces;

public interface IPeriodoService
{
    public Task<Guid> CreatePeriodoAsync(CreatePeriodoViewModel periodo);
    public Task UpdatePeriodoAsync(UpdatePeriodoViewModel periodo);
    public Task DeletePeriodoAsync(Guid periodoId);
    public Task<PeriodoViewModel?> GetPeriodoByIdAsync(Guid periodoId);

    public Task<PagedResult<PeriodoViewModel>> GetAllPeriodosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}