using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.Queries.Periodos.GetAll;
using CleanArchitecture.Application.Queries.Periodos.GetPeriodoById;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Application.ViewModels.Sorting;
using CleanArchitecture.Application.ViewModels.Periodos;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Commands.Periodos.CreatePeriodo;
using CleanArchitecture.Domain.Commands.Periodos.DeletePeriodo;
using CleanArchitecture.Domain.Commands.Periodos.UpdatePeriodo;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Extensions;
using CleanArchitecture.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitecture.Application.Services;

public sealed class PeriodoService : IPeriodoService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public PeriodoService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreatePeriodoAsync(CreatePeriodoViewModel periodo)
    {
        var periodoId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreatePeriodoCommand(
            periodoId,
            periodo.FechaInicio,
            periodo.FechaFinal,
            periodo.Nombre));

        return periodoId;
    }

    public async Task UpdatePeriodoAsync(UpdatePeriodoViewModel periodo)
    {
        await _bus.SendCommandAsync(new UpdatePeriodoCommand(
            periodo.Id,
            periodo.FechaInicio,
            periodo.FechaFinal,
            periodo.Nombre));
    }

    public async Task DeletePeriodoAsync(Guid periodoId)
    {
        await _bus.SendCommandAsync(new DeletePeriodoCommand(periodoId));
    }

    public async Task<PeriodoViewModel?> GetPeriodoByIdAsync(Guid periodoId)
    {
        var cachedPeriodo = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Periodo>(periodoId),
            async () => await _bus.QueryAsync(new GetPeriodoByIdQuery(periodoId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedPeriodo;
    }

    public async Task<PagedResult<PeriodoViewModel>> GetAllPeriodosAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllPeriodosQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}