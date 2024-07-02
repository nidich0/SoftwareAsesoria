using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Calendarios;

namespace CleanArchitecture.Application.Interfaces;

public interface ICalendarioService
{
    public Task<Guid> CreateCalendarioAsync(CreateCalendarioViewModel calendario);
    public Task UpdateCalendarioAsync(UpdateCalendarioViewModel calendario);
    public Task DeleteCalendarioAsync(Guid calendarioId);
    public Task VincularCalendarioAsync(Guid calendarioId, string calendarioName);
    public Task SincronizarCalendarioAsync(Guid calendarioId);
    public Task<CalendarioViewModel?> GetCalendarioByIdAsync(Guid calendarioId);
    public Task<string?> GetAccessTokenAsync(Guid userId);
    public Task<PagedResult<CalendarioViewModel>> GetAllCalendariosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}